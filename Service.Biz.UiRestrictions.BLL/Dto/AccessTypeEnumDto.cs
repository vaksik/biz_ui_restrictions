using Service.Biz.UiRestrictions.BLL.Infrastructure.Attributes;

namespace Service.Biz.UiRestrictions.BLL.Dto
{

    public enum AccessTypeEnumDto
    {
        [DataView("DISABLE")]
        Disable,
        [DataView("HIDDEN")]
        Hidden
    }
}