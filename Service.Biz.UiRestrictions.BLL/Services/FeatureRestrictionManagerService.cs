using Microsoft.EntityFrameworkCore;
using Service.Biz.UiRestrictions.BLL.Dto;
using Service.Biz.UiRestrictions.BLL.Infrastructure;
using Service.Biz.UiRestrictions.DAL;
using Service.Biz.UiRestrictions.DAL.Entities;
using Service.Biz.UiRestrictions.DAL.Extensions;
using Service.Biz.UiRestrictions.Dto;
using ILogger = Module.Logging.Core.ILogger;
using ILoggerFactory = Module.Logging.Core.ILoggerFactory;

namespace Service.Biz.UiRestrictions.BLL.Services;

public class FeatureRestrictionManagerService : IFeatureRestrictionManagerService
{
    private readonly IDbContextFactory<BizUiRestrictionsDataContext> _dbContextFactory;
    private readonly ILogger _logger;

    public FeatureRestrictionManagerService(IDbContextFactory<BizUiRestrictionsDataContext> dbContextFactory, ILoggerFactory loggerFactory)
    {
        _dbContextFactory = dbContextFactory;
        _logger = loggerFactory.GetLogger(GetType());
    }
    
    public async Task<IEnumerable<FeatureRestrictionDto>> GetMergedOrganizationsRestrictionsAsync(Guid? organizationNetworkId, Guid [] organizationIds, 
        PlaziusFeatureEnumDto[] features, CancellationToken cancellationToken = default)
    {
        var allRestrictions =  new List<FeatureRestrictionDto>();
        if (!organizationIds.Any() && organizationNetworkId is null)
            return allRestrictions;
        await using var dataContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var restrictionProduct =
            await GetMostSignificantProductAsync(dataContext, 
                new MostSignificantProductFilter(organizationIds, organizationNetworkId), cancellationToken);
           
        if (restrictionProduct is null)
            return allRestrictions;
        
        var restrictions =  await GetProductConstRestrictionsAsync(dataContext, restrictionProduct.Id,
            features.ToArrayForSearch(), cancellationToken);
        allRestrictions.AddRange(GetStaticAndDynamicRestrictions(restrictions));
        return allRestrictions;
    }

    public async Task<IEnumerable<OrganizationDto>> HaveNotAccessToFeatureAsync(Guid[] organizationIds, PlaziusFeatureEnumDto feature,
        CancellationToken cancellationToken = default)
    {
        if (!organizationIds.Any()) return Enumerable.Empty<OrganizationDto>();
        
        await using var dataContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        var productsWithoutAccessToFeature = await dataContext.ProductFeatureAccesses
            .Include(f => f.Feature)
            .Where(x => x.Feature.Name == feature.ToValueForSearch())
            .Select(x => x.Product.Id).Distinct().ToArrayAsync(cancellationToken);
        
        return await dataContext.OrganizationsProducts
            .Where(x => productsWithoutAccessToFeature.Contains(x.ProductId))
            .Where(x => organizationIds.Contains(x.OrganizationId))
            .Select(x=> 
                new OrganizationDto{ NetworkId = x.NetworkId, OrganizationId = x.OrganizationId})
            .ToArrayAsync(cancellationToken: cancellationToken);
    }
    
    public async Task<Dictionary<Guid, IEnumerable<FeatureRestrictionDto>>> GetOrganizationsRestrictionsAsync(Guid[] organizationIds, PlaziusFeatureEnumDto[] features,
        CancellationToken cancellationToken = default)
    {
        var organizationsRestrictions = new Dictionary<Guid, IEnumerable<FeatureRestrictionDto>>();
        if (!organizationIds.Any()) return organizationsRestrictions;
        await using var dataContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        var productsOrganizations = await dataContext.OrganizationsProducts
            .Where(x => organizationIds.Contains(x.OrganizationId))
            .Select(x => new { x.ProductId, x.OrganizationId })
            .ToListAsync(cancellationToken);
        
        var productRestrictionsMap = await dataContext.ProductFeatureAccesses
            .Include(x=>x.Feature)
            .Where(x => productsOrganizations.Select(m => m.ProductId).Distinct().Contains(x.ProductId))
            .WhereIf(features.Any(),x => features.ToArrayForSearch().Contains(x.Feature.Name))
            .GroupBy(x => x.ProductId)
            .ToDictionaryAsync(k => k.Key, v => v.ToArray(), cancellationToken: cancellationToken);

        foreach (var organizationProductMap in productsOrganizations.ToDictionary(k=>k.OrganizationId, v=>v.ProductId))
        {
            if (productRestrictionsMap.TryGetValue(organizationProductMap.Value, out var restrictions))
            {
                organizationsRestrictions.TryAdd(organizationProductMap.Key, GetStaticAndDynamicRestrictions(restrictions));
            }
        }
        return organizationsRestrictions;
    }

    private FeatureRestrictionDto[] GetStaticAndDynamicRestrictions(IEnumerable<ProductFeatureRestriction> restrictions)
    {
        var allRestrictions = restrictions.Select(x => x.ToDto()).ToList();
        //todo перешли на использование статической фичи при определении ограничений для динамических элементов ui
        //allRestrictions.AddRange(DynamicFeatureRestrictionsFactory.Create(allRestrictions));
        return allRestrictions.ToArray();
    }
    private async Task<Product?> GetMostSignificantProductAsync(BizUiRestrictionsDataContext dataContext, MostSignificantProductFilter filter, CancellationToken cancellationToken = default)
    {
        return await dataContext.OrganizationsProducts.Include(x => x.Product)
            .WhereIf(filter.NetworkId is not null, x => x.NetworkId == filter.NetworkId)
            .WhereIf(filter.OrganizationIds.Any(), x => filter.OrganizationIds.Contains(x.OrganizationId))
            .Select(x => x.Product)
            .OrderByDescending(x => x.Level)
            .FirstOrDefaultAsync(cancellationToken);
    }

    private record MostSignificantProductFilter(Guid[] OrganizationIds, Guid? NetworkId);
    
    private async Task<IEnumerable<ProductFeatureRestriction>> GetProductConstRestrictionsAsync(BizUiRestrictionsDataContext dbContext, int productId,
        string[] features, CancellationToken cancellationToken = default)
    {
        return await dbContext.ProductFeatureAccesses.Include(x => x.Feature)
            .Where(x => x.ProductId == productId)
            .WhereIf(features.Any(),x => features.Contains(x.Feature.Name)).ToListAsync(cancellationToken);
    }
    
}