using System.Security.Principal;
using Api.Biz.Contracts.Http;
using Api.Biz.Contracts.Http.Dto.Data.Accounts;
using Service.Biz.UiRestrictions.BLL.Services.Access.Enumerations;
using Service.Biz.UiRestrictions.BLL.Services.Access.Extensions;

namespace Service.Biz.UiRestrictions.BLL.Services.Access;

public class OrganizationUserAccessService: UserAccessServiceBase<Guid, OrganizationMainDataAccess>
{
    public OrganizationUserAccessService(IPermissionsClient permissionsClient) : base(permissionsClient) { }

    protected override async  Task<bool> HaveUserAccessAsync(IIdentity userIdentity, Guid organizationOrNetworkId, OrganizationMainDataAccess subject)
    {
        switch (subject)
        {
            case OrganizationMainDataAccess.ViewSingle:
            {
                await _permissionsClient.AssertPermissions(userIdentity.Name, organizationOrNetworkId, BizPermissionsDto.All);
                return true;
            }

            case OrganizationMainDataAccess.Update:
            {
                await _permissionsClient.AssertPermissions(userIdentity.Name, organizationOrNetworkId, BizPermissionsDto.EditSettings);
                return true;
            }

            case OrganizationMainDataAccess.CreateNew:
            {
                return userIdentity.GetRole() == BizRolesDto.Admin
                       || userIdentity.GetRole() == BizRolesDto.SpecialSaleCenterPartner;
            }
            default:
                throw new ArgumentOutOfRangeException(nameof(subject), subject, null);
        }
    }
}