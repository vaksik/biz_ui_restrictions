using FluentAssertions;
using Module.Logging.Core;
using Service.Biz.UiRestrictions.BLL.Services;
using Service.Biz.UiRestrictions.DAL.Entities;

namespace Service.Biz.UiRestrictions.Test;

public class OrganizationServiceTests
{
    private readonly InMemoryDbContextFactory _inMemoryDbContext;
    private readonly ILoggerFactory _logger;

    public OrganizationServiceTests()
    {
        // Настройка InMemoryDatabase
        _inMemoryDbContext = new InMemoryDbContextFactory();
        _logger = LoggingManager.Factory; 
    }

    [Fact]
    public async Task GetOrganizationsWithProductAsync_WithNetworkIds_ReturnsOrganizations()
    {
        // Arrange
        var organizationId = Guid.NewGuid();
        var networkId = Guid.NewGuid();
        var networkIds = new[] { networkId };
        var organizationProduct = new OrganizationProduct { ProductId = 1, OrganizationId = organizationId, NetworkId = networkId };
        var organizationsProducts = new List<OrganizationProduct> { organizationProduct };
        await using var dbContext = await _inMemoryDbContext.CreateDbContextAsync();
        dbContext.OrganizationsProducts.AddRange(organizationsProducts);
        await dbContext.SaveChangesAsync();

        var service = new OrganizationService(_inMemoryDbContext, _logger);

        // Act
        var result = await service.GetOrganizationsWithProductAsync(networkIds, null, CancellationToken.None);

        // Assert
        result.Should().HaveCount(1);
        result.First().Should().Be(organizationId);
        
    }

    [Fact]
    public async Task GetOrganizationsWithProductAsync_WithOrganizationIds_ReturnsOrganizations()
    {
        // Arrange
        var organizationId = Guid.NewGuid();
        var organizationIds = new[] { organizationId };
        var organizationProduct = new OrganizationProduct { ProductId = 1, OrganizationId = organizationId };
        var organizationsProducts = new List<OrganizationProduct> { organizationProduct };
    
        await using var dbContext = await _inMemoryDbContext.CreateDbContextAsync();
        dbContext.OrganizationsProducts.AddRange(organizationsProducts);
        await dbContext.SaveChangesAsync();

        var service = new OrganizationService(_inMemoryDbContext, _logger);

        // Act
        var result = await service.GetOrganizationsWithProductAsync(null, organizationIds, CancellationToken.None);

        // Assert
        result.Should().HaveCount(1);
        result.First().Should().Be(organizationId);
    }
    
    [Fact]
    public async Task GetOrganizationsWithProductAsync_WithNetworkAndOrganizationIds_ReturnsOrganizations()
    {
        // Arrange
        var organizationFirstId = Guid.NewGuid();
        var organizationSecondId = Guid.NewGuid();
        var networkId = Guid.NewGuid();
        var networkIds = new[] { networkId };
        var networkOrganizationProduct = new OrganizationProduct { ProductId = 1, OrganizationId = organizationFirstId, NetworkId = networkId };
        var organizationIds = new[] { organizationSecondId };
        var organizationProduct = new OrganizationProduct { ProductId = 1, OrganizationId = organizationSecondId };
        
        var organizationsProducts = new List<OrganizationProduct> { networkOrganizationProduct, organizationProduct };
        await using var dbContext = await _inMemoryDbContext.CreateDbContextAsync();
        dbContext.OrganizationsProducts.AddRange(organizationsProducts);
        await dbContext.SaveChangesAsync();

        var service = new OrganizationService(_inMemoryDbContext, _logger);

        // Act
        var result = await service.GetOrganizationsWithProductAsync(networkIds, organizationIds, CancellationToken.None);

        // Assert
        result.Should().HaveCount(2);
        result.Should().BeEquivalentTo(organizationsProducts.Select(x=>x.OrganizationId));
        
    }
    
    [Fact]
    public async Task GetOrganizationsWithProductAsync_WithEmptyInput_ReturnsEmpty()
    {
        // Arrange
        var service = new OrganizationService(_inMemoryDbContext, _logger);
        // Act
        var result = await service.GetOrganizationsWithProductAsync(null, null, CancellationToken.None);
        // Assert
        result.Should().BeEmpty();
    }
}