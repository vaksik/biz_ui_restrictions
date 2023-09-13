using Microsoft.EntityFrameworkCore;
using Module.Logging.Core;
using Service.Biz.UiRestrictions.DAL;
using Service.Biz.UiRestrictions.DAL.Extensions;

namespace Service.Biz.UiRestrictions.BLL.Services;

public class OrganizationService : IOrganizationService
{
    private readonly IDbContextFactory<BizUiRestrictionsDataContext> _dbContextFactory;
    private readonly ILogger _logger;

    public OrganizationService(IDbContextFactory<BizUiRestrictionsDataContext> dbContextFactory, ILoggerFactory loggerFactory)
    {
        _dbContextFactory = dbContextFactory;
        _logger = loggerFactory.GetLogger(GetType());
    }
    public async Task<IEnumerable<Guid>> GetOrganizationsWithProductAsync(Guid[]? networkIds, Guid[]? organizationIds, CancellationToken cancellationToken = default)
    {
        var organizations = new List<Guid>();
        await using var dataContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        if (networkIds is { Length: > 0 })
        {
            var networkOrganizations = await dataContext.OrganizationsProducts
                .Where( x => x.NetworkId != null && networkIds!.Contains(x.NetworkId.Value))
                .Select(x => x.OrganizationId).ToArrayAsync(cancellationToken);
            organizations.AddRange(networkOrganizations);
        }

        if (organizationIds is { Length: > 0 })
        {
            var singleOrganizations =  await dataContext.OrganizationsProducts
                .WhereIf(organizationIds is { Length: > 0 }, x =>  organizationIds!.Contains(x.OrganizationId))
                .Select(x => x.OrganizationId).ToArrayAsync(cancellationToken);
            organizations.AddRange(organizationIds);
        }

        return organizations.Distinct();
    }

    public async Task UpdateOrganizationProductAsync(Guid organizationId, string productCode, Guid? networkId, CancellationToken cancellationToken = default)
    {
        await using var dataContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        await dataContext.OrganizationsProducts.Where(o => o.OrganizationId == organizationId).ExecuteDeleteAsync(cancellationToken);

        var product = await dataContext.Products.SingleOrDefaultAsync(p => p.Code == productCode, cancellationToken);
        if (product == null)
            return;

        await dataContext.OrganizationsProducts.AddAsync(new DAL.Entities.OrganizationProduct
        {
            OrganizationId = organizationId,
            ProductId = product.Id,
            NetworkId = networkId,
        }, cancellationToken);
        await dataContext.SaveChangesAsync(cancellationToken);
    }
}