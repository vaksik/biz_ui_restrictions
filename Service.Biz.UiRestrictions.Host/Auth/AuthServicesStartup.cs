using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Service.Biz.UiRestrictions.Host.Auth;

internal static class AuthServicesStartup
{
    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services)
    {
        var authSchemas = new List<string> { nameof(BizAuthHandler) };
        
        services.AddAuthentication(nameof(BizAuthHandler))
            .AddScheme<AuthenticationSchemeOptions, BizAuthHandler>(nameof(BizAuthHandler), options => { });
        
        services.AddAuthorization(options =>
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddAuthenticationSchemes(authSchemas.ToArray())
                .Build()
        );
        return services;
    }
}