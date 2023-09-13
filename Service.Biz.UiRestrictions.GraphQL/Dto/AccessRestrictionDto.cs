using Service.Biz.UiRestrictions.BLL.Dto;
using Service.Biz.UiRestrictions.Dto;

namespace Service.Biz.UiRestrictions.GraphQL.Dto;

public class AccessRestrictionDto
{
    public AccessRestrictionTypeEnumDto AccessRestrictionType { get; set; }
    
    public string? Details { get; set; }
}