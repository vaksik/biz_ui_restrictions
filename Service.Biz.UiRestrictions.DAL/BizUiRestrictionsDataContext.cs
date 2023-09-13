using System.Data.Common;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Service.Biz.UiRestrictions.DAL.Entities;

namespace Service.Biz.UiRestrictions.DAL;

//dotnet ef migrations add InitialCreate --project Service.Biz.UiRestrictions.DAL/Service.Biz.UiRestrictions.DAL.csproj --startup-project Service.Biz.UiRestrictions.Host/Service.Biz.UiRestrictions.Host.csproj --context BizUiRestrictionsDataContext

public class BizUiRestrictionsDataContext : DbContext
{
    public DbSet<Feature> Features { get; set; } = default!;

    public DbSet<Product> Products { get; set; } = default!;

    public DbSet<ProductFeatureRestriction> ProductFeatureAccesses { get; set; } = default!;

    public DbSet<OrganizationProduct> OrganizationsProducts { get; set; } = default!;

    static BizUiRestrictionsDataContext()
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<AccessType>();
        NpgsqlConnection.GlobalTypeMapper.MapEnum<AccessRestrictionType>();
    }
    public BizUiRestrictionsDataContext(DbContextOptions<BizUiRestrictionsDataContext> options)
        : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            options.UseNpgsql("A FALLBACK CONNECTION STRING");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.HasPostgresEnum<AccessType>();
        modelBuilder.HasPostgresEnum<AccessRestrictionType>();
       
    }
}