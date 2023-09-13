using Api.Biz.Contracts.Http;
using Api.Biz.Contracts.Http.Client;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Biz.UiRestrictions.BLL.Options;
using Service.Biz.UiRestrictions.BLL.Services;
using Service.Biz.UiRestrictions.BLL.Services.Access;
using Service.Biz.UiRestrictions.BLL.Services.Access.Enumerations;
using Service.Biz.UiRestrictions.BLL.Validators;
using Service.Biz.UiRestrictions.Dto.WebApi;

namespace Service.Biz.UiRestrictions.BLL;

public static class BllServicesStartup
{
    private const int PermissionsClientServiceLifeTimePerMinutes = 20;
    private const int IikoBizAccountsClientServiceLifeTimePerMinutes = 20;

    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        
        var externalServicesOptions = configuration.GetSection("ExternalServiceOptions").Get<ExternalServiceOptions>()!;
        var cardServiceBizApiHost =
            externalServicesOptions.DomainServices!.Single(s => s.Name == "CardServiceBizApi").Host;
        services.AddSingleton<IPermissionsClient>(_ => new PermissionsClient(
            cardServiceBizApiHost,
            TimeSpan.FromMinutes(PermissionsClientServiceLifeTimePerMinutes)));

        services.AddSingleton<IIikoBizAccountsClient>(_ => new IikoBizAccountsClient(
            cardServiceBizApiHost,
            TimeSpan.FromMinutes(IikoBizAccountsClientServiceLifeTimePerMinutes)));
        
        services.AddScoped<IFeatureRestrictionManagerService, FeatureRestrictionManagerService>();
        services.AddSingleton<IOrganizationService, OrganizationService>();
        services.AddSingleton<IUserAccessService<Guid, OrganizationMainDataAccess>, OrganizationUserAccessService>();
        services.AddValidators();

        return services;
    }
    
    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<GetOrganizationsNotAvailableRequestDto>, GetOrganizationsNotAvailablRequestValidator>();
        services.AddScoped<IValidator<GetOrganizationsRestrictionsRequestDto>,GetOrganizationsRestrictionsRequestValidator>();
        return services;
    }
}