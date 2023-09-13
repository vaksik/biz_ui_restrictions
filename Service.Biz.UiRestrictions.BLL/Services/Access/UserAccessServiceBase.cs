using System.Security.Principal;
using Api.Biz.Contracts.Http;
using Module.Common.Exceptions;

namespace Service.Biz.UiRestrictions.BLL.Services.Access;

public abstract class UserAccessServiceBase<TId, TAccessEnum> : IUserAccessService<TId, TAccessEnum> where TAccessEnum : Enum
{
    protected readonly IPermissionsClient _permissionsClient;

    protected UserAccessServiceBase(IPermissionsClient permissionsClient)
    {
        _permissionsClient = permissionsClient;
    }

    public Task AssertAccessAsync(IIdentity userIdentity, IEnumerable<TAccessEnum> accessTypes)
    {
        return AssertAccessAsync(userIdentity, default, accessTypes);
    }

    public async Task AssertAccessAsync(IIdentity userIdentity, TId subjectId, IEnumerable<TAccessEnum> accessTypes)
    {
        foreach (var accessType in accessTypes)
        {
            var haveAccess = await HaveUserAccessAsync(userIdentity, subjectId, accessType);

            if (!haveAccess)
                throw new LocalizableException($"Пользователь не имеет прав на выполнение действия {accessType.ToString()}");
        }
    }

    protected abstract Task<bool> HaveUserAccessAsync(IIdentity userIdentity, TId subjectId, TAccessEnum accessType);
}