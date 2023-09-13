using System.Security.Claims;
using Api.Biz.Contracts.Http.Dto.Data.Accounts;

namespace Service.Biz.UiRestrictions.Host.Auth;

public class AuthHandlerHelper
{
    public static Claim[] GetClaims(IikoBizAccessInfoDto permissions, string? name)
    {
        return  new[]
        {
            new Claim(ClaimTypes.NameIdentifier, permissions.Id.ToString()),
            new Claim(ClaimTypes.Name, name),
            new Claim(ClaimTypes.Role, permissions.GlobalRole.ToString())
        };
    }
}