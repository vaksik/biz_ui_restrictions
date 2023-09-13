using Microsoft.EntityFrameworkCore;
using Service.Biz.UiRestrictions.BLL.Dto;
using Service.Biz.UiRestrictions.BLL.Infrastructure;
using Service.Biz.UiRestrictions.DAL;

namespace Service.Biz.UiRestrictions.Test;

public class InMemoryDbContextFactory : IDbContextFactory<BizUiRestrictionsDataContext>
{
    public BizUiRestrictionsDataContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<BizUiRestrictionsDataContext>()
            .UseInMemoryDatabase(databaseName: "BizUiRestrictionsTestDb").Options;
        return new BizUiRestrictionsDataContext(options);
    }

    public void CleanUpDatabase()
    {
        using var context = this.CreateDbContext();
        context.Database.EnsureDeleted();
    }

    public Task<BizUiRestrictionsDataContext> CreateDbContextAsync(CancellationToken cancellationToken = default)
        => Task.FromResult(CreateDbContext());
}