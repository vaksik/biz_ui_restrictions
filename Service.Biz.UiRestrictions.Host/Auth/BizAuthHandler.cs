using System.Security.Claims;
using System.Text.Encodings.Web;
using Api.Biz.Contracts.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Service.Biz.UiRestrictions.Host.Auth;

internal class BizAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private const string ProxiedUserNameHeaderName = "BizUserName";
        private readonly IIikoBizAccountsClient accountsClient;

        public BizAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder,
            ISystemClock clock, IIikoBizAccountsClient accountsClient) : base(options, logger, encoder, clock)
        {
            this.accountsClient = accountsClient;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (TryGetBizUserNameHeader(out var userName))
            {
                try
                {
                    var ticket = await Authenticate(Context, userName);
                    // todo Logging.Global.SetTag(LoggingConsts.TagKeys.AuthUserName, Context.User.Identity.Name);
                    return AuthenticateResult.Success(ticket);
                }
                catch (Exception e)
                {
                    //todo Logging.Global.SetTag(MonitoringConsts.TagKeys.HttpRequest, Context.Request.ToJson());
                    Logger.LogWarning(e, "Authentication Biz.WebApi error occurred");
                    return AuthenticateResult.Fail(e);
                }
            }

            return AuthenticateResult.Fail("No auth data in request");
        }
        
        private async Task<AuthenticationTicket> Authenticate(HttpContext context, string? userName)
        {
            var permissions = await accountsClient.GetUserPermissions(userName);
            
            var identity = new ClaimsIdentity(AuthHandlerHelper.GetClaims(permissions, userName), "Basic");
            
            context.User = new ClaimsPrincipal(identity);
            
            return new AuthenticationTicket(context.User, nameof(BizAuthHandler));
        }

        private bool TryGetBizUserNameHeader(out string? apiKey)
        {
            apiKey = null;
            
            if (Context.Request.Headers.ContainsKey(ProxiedUserNameHeaderName))
            {
                apiKey = Uri.UnescapeDataString(Context.Request.Headers[ProxiedUserNameHeaderName]);
                return true;
            }

            return !string.IsNullOrWhiteSpace(apiKey);
        }
    }