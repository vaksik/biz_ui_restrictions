using FluentAssertions;
using Module.Logging.Core;
using Service.Biz.UiRestrictions.BLL.Dto;
using Service.Biz.UiRestrictions.BLL.Infrastructure;
using Service.Biz.UiRestrictions.BLL.Services;
using Service.Biz.UiRestrictions.DAL.Entities;

namespace Service.Biz.UiRestrictions.Test;


public class FeatureRestrictionManagerServiceTests
{
    private readonly InMemoryDbContextFactory _inMemoryDbContext;
    private readonly ILoggerFactory _logger;

    
    public FeatureRestrictionManagerServiceTests()
    {
        // Настройка InMemoryDatabase
        _inMemoryDbContext = new InMemoryDbContextFactory();
        _logger = LoggingManager.Factory;
        _inMemoryDbContext.CleanUpDatabase();
    }
    
    public static IEnumerable<object[]> GetPlaziusFeatureEnumValues =>
        Enum.GetValues(typeof(PlaziusFeatureEnumDto))
            .Cast<PlaziusFeatureEnumDto>()
            .ToArray()
            .ExcludeDynamicFeatures()
            .Select(x=> new object[]{x});
    
    [Theory]
    [MemberData(nameof(GetPlaziusFeatureEnumValues))]
    public async Task GetMergedOrganizationsRestrictionsAsync_GetByOrganizationIds_ReturnsMergedOrganizationsRestrictions(PlaziusFeatureEnumDto feature)
    {
        // Arrange
        var organizationIds = new[] { Guid.NewGuid() };
        var features = new[] { feature };
        var restrictionProduct1 = new Product { Id = 1, Level = 1, Code = "Test1"};
        var restrictionProduct2 = new Product { Id = 2, Level = 2, Code = "Test2"};

        var featureEntity = new Feature { Id = 1, Name = feature.ToValueForSearch() };

        var restriction1 = new ProductFeatureRestriction
        {
            FeatureId = 1,
            ProductId = restrictionProduct1.Id,
            AccessType = AccessType.Disable,
            AccessRestrictionType = AccessRestrictionType.TariffNotEnough,
            Feature = featureEntity,
            Product = restrictionProduct1,
            Detail = "details"
        };
        var restriction2 = new ProductFeatureRestriction
        {
            FeatureId = 1,
            ProductId = restrictionProduct2.Id,
            AccessType = AccessType.Hidden,
            AccessRestrictionType = AccessRestrictionType.TariffNotEnough,
            Feature = featureEntity,
            Product = restrictionProduct2,
            Detail = "details2"
        };
        await using var dbContext = await _inMemoryDbContext.CreateDbContextAsync();
        dbContext.Products.AddRange(restrictionProduct1, restrictionProduct2);
        dbContext.Features.Add(featureEntity);
        dbContext.ProductFeatureAccesses.AddRange(restriction1, restriction2);
        
        dbContext.OrganizationsProducts.Add(new OrganizationProduct
            { ProductId = restrictionProduct1.Id, OrganizationId = organizationIds[0] });
        await dbContext.SaveChangesAsync();
        
        var service = new FeatureRestrictionManagerService(_inMemoryDbContext, _logger);

        // Act
        var result =
            await service.GetMergedOrganizationsRestrictionsAsync(null, organizationIds, features, CancellationToken.None);

        // Assert
        var expectations =  new List<FeatureRestrictionDto>
        {
            restriction1.ToDto()
        };
        result.Should().BeEquivalentTo(expectations);
    }

    [Theory]
    [MemberData(nameof(GetPlaziusFeatureEnumValues))]
    public async Task GetMergedOrganizationsRestrictionsAsync_GetByNetworkId_ReturnsMergedOrganizationsRestrictions(PlaziusFeatureEnumDto feature)
    {
        // Arrange
        var networkId = Guid.NewGuid();
        var features = new[] { feature };
        var restrictionProduct1 = new Product { Id = 1, Level = 1, Code = "Test1"};
        var restrictionProduct2 = new Product { Id = 2, Level = 2, Code = "Test2"};

        var featureEntity = new Feature { Id = 1, Name = feature.ToValueForSearch() };

        var restriction1 = new ProductFeatureRestriction
        {
            FeatureId = 1,
            ProductId = restrictionProduct1.Id,
            AccessType = AccessType.Disable,
            AccessRestrictionType = AccessRestrictionType.TariffNotEnough,
            Feature = featureEntity,
            Product = restrictionProduct1,
            Detail = "details"
        };
        var restriction2 = new ProductFeatureRestriction
        {
            FeatureId = 1,
            ProductId = restrictionProduct2.Id,
            AccessType = AccessType.Hidden,
            AccessRestrictionType = AccessRestrictionType.TariffNotEnough,
            Feature = featureEntity,
            Product = restrictionProduct2,
            Detail = "details2"
        };
        await using var dbContext = await _inMemoryDbContext.CreateDbContextAsync();
        var t = dbContext.Products.ToArray();
        dbContext.Products.AddRange(restrictionProduct1, restrictionProduct2);
        dbContext.Features.Add(featureEntity);
        dbContext.ProductFeatureAccesses.AddRange(restriction1, restriction2);

        dbContext.OrganizationsProducts.AddRange(
            new OrganizationProduct
                { ProductId = restrictionProduct1.Id, OrganizationId = Guid.NewGuid(), NetworkId = networkId },
            new OrganizationProduct
                { ProductId = restrictionProduct2.Id, OrganizationId = Guid.NewGuid(), NetworkId = networkId });
        await dbContext.SaveChangesAsync();
        
        var service = new FeatureRestrictionManagerService(_inMemoryDbContext, _logger);

        // Act
        var result =
            await service.GetMergedOrganizationsRestrictionsAsync(networkId, Array.Empty<Guid>(), features, CancellationToken.None);

        // Assert
        var expectations =  new List<FeatureRestrictionDto>
        {
            restriction2.ToDto()
        };
        result.Should().BeEquivalentTo(expectations);
    }

    [Fact]
    public async Task GetMergedOrganizationsRestrictionsAsync_GetByEmptyOrgsOrNullNetworkId_ReturnsEmptyRestrictions()
    {
        //arrange
        var service = new FeatureRestrictionManagerService(_inMemoryDbContext, _logger);
        //act 
        var result = await
            service.GetMergedOrganizationsRestrictionsAsync(null, Array.Empty<Guid>(), new[]
            {
                PlaziusFeatureEnumDto.Communications
            }, CancellationToken.None);
        //assert
        result.Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(GetPlaziusFeatureEnumValues))]
    public async Task HaveNotAccessToFeatureAsync_ReturnsOrganizationWithoutAccess(PlaziusFeatureEnumDto feature)
    {
        //arrange
        var organizationIds = new[] { Guid.NewGuid(), Guid.NewGuid() };
        var networkIds = new[] { Guid.NewGuid(), Guid.NewGuid() };
        var product1 = new Product { Id = 1, Level = 1, Code = "Test1"};
        var product2 = new Product { Id = 2, Level = 2, Code = "Test2"};
       
        var featureEntity = new Feature { Id = 1, Name = feature.ToValueForSearch() };

        var restriction1 = new ProductFeatureRestriction
        {
            FeatureId = 1,
            ProductId = product1.Id,
            AccessType = AccessType.Disable,
            AccessRestrictionType = AccessRestrictionType.TariffNotEnough,
            Feature = featureEntity,
            Product = product1,
            Detail = "details"
        };
        await using var dbContext = await _inMemoryDbContext.CreateDbContextAsync();
        dbContext.Products.AddRange(product1, product2);
        dbContext.Features.Add(featureEntity);
        dbContext.ProductFeatureAccesses.AddRange(restriction1);
        
        dbContext.OrganizationsProducts.AddRange(new OrganizationProduct
            { ProductId = product1.Id, OrganizationId = organizationIds[0], NetworkId = networkIds[0]},
            new OrganizationProduct
                { ProductId = product2.Id, OrganizationId = organizationIds[1], NetworkId = networkIds[1]});
        await dbContext.SaveChangesAsync();
        var service = new FeatureRestrictionManagerService(_inMemoryDbContext, _logger);
        //act

        var result = await service.HaveNotAccessToFeatureAsync(organizationIds, feature, CancellationToken.None);
        
        //assert
        result.Should().HaveCount(1);
        result.First().OrganizationId.Should().Be(organizationIds[0]);
        result.First().NetworkId.Should().Be(networkIds[0]);
    }

    [Theory]
    [MemberData(nameof(GetPlaziusFeatureEnumValues))]
    public async Task HaveNotAccessToFeatureAsync_WhenAllOrganizationsHaveAccess_ReturnsEmpty(PlaziusFeatureEnumDto feature)
    {
        //arrange
        var organizationIds = new[] { Guid.NewGuid(), Guid.NewGuid() };
        var networkIds = new[] { Guid.NewGuid(), Guid.NewGuid() };
        var product1 = new Product { Id = 1, Level = 1, Code = "Test1"};
        var product2 = new Product { Id = 2, Level = 2, Code = "Test2"};
        var featureEntity = new Feature { Id = 1, Name = feature.ToValueForSearch() };
        await using var dbContext = await _inMemoryDbContext.CreateDbContextAsync();
        dbContext.Products.AddRange(product1, product2);
        dbContext.Features.Add(featureEntity);
        dbContext.OrganizationsProducts.AddRange(new OrganizationProduct
                { ProductId = product1.Id, OrganizationId = organizationIds[0], NetworkId = networkIds[0]},
            new OrganizationProduct
                { ProductId = product2.Id, OrganizationId = organizationIds[1], NetworkId = networkIds[1]});
        await dbContext.SaveChangesAsync();
        var service = new FeatureRestrictionManagerService(_inMemoryDbContext, _logger);
        //act

        var result = await service.HaveNotAccessToFeatureAsync(organizationIds, feature, CancellationToken.None);
        
        //assert
        result.Should().BeEmpty();
    }

}