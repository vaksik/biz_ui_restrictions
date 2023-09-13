using System.Security.Claims;
using System.Security.Principal;
using Api.Biz.Contracts.Http.Dto.Data.Accounts;

namespace Service.Biz.UiRestrictions.BLL.Services.Access.Extensions;

public static class IdentityExtensions
{
    public static BizRolesDto GetRole(this IIdentity source)
    {
        if (source == null)
            throw new NullReferenceException();
        var claimIdentity = (ClaimsIdentity)source;
        var roleClaim = claimIdentity.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Role);
        if (roleClaim == null)
            throw new Exception("Role claim is null");
        if (!Enum.TryParse(roleClaim.Value, out BizRolesDto result))
            throw new Exception("Role claim isn`t parsed");
        return result;
    }
}