using Service.Biz.UiRestrictions.BLL.Dto;
using Service.Biz.UiRestrictions.Dto;

namespace Service.Biz.UiRestrictions.GraphQL.Filters;

public class FeatureFilter
{
    public List<PlaziusFeatureEnumDto> Features { get; set; }
}