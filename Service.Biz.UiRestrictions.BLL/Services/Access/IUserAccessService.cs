using System.Security.Principal;

namespace Service.Biz.UiRestrictions.BLL.Services.Access;

public interface IUserAccessService<in TId, in TAccessEnum> where TAccessEnum : Enum
{
    Task AssertAccessAsync(IIdentity userIdentity, IEnumerable<TAccessEnum> accessTypes);

    Task AssertAccessAsync(IIdentity userIdentity, TId subjectId, IEnumerable<TAccessEnum> accessTypes);
}