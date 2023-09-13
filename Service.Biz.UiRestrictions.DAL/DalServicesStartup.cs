using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Service.Biz.UiRestrictions.DAL;

public static class DalServicesStartup
{
    public static IServiceCollection AddStorage(this IServiceCollection services, IConfiguration configuration, bool isTestEnvironment)
    {
        services.AddDbContextFactory<BizUiRestrictionsDataContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("db.biz_ui_restrictions"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
                options.EnableSensitiveDataLogging(isTestEnvironment);
            })
            .AddEntityFrameworkNpgsql();
        services.AddScoped<IDbManager, DbManager>();
        return services;
    }
}