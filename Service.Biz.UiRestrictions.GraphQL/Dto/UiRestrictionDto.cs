using Service.Biz.UiRestrictions.BLL.Dto;
using Service.Biz.UiRestrictions.Dto;

namespace Service.Biz.UiRestrictions.GraphQL.Dto;

public class UiRestrictionDto
{
    public PlaziusFeatureEnumDto Feature { get; set; }
    
    public AccessTypeEnumDto AccessType { get; set; }

    public List<AccessRestrictionDto> AccessRestrictions { get; set; } = null!;
}