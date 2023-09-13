using Service.Biz.UiRestrictions.BLL.Dto;
using Service.Biz.UiRestrictions.BLL.Infrastructure;
using Service.Biz.UiRestrictions.DAL.Entities;
using Service.Biz.UiRestrictions.Dto;
using Service.Biz.UiRestrictions.Dto.WebApi;
using Service.Biz.UiRestrictions.GraphQL.Dto;

namespace Service.Biz.UiRestrictions.GraphQL.Mappers;

public static class UiRestrictionsMapper
{
    public static UiRestrictionsResponseDto ToDto(this IEnumerable<FeatureRestrictionDto> feature)
    {
        var dto = new UiRestrictionsResponseDto { Restrictions = new List<UiRestrictionDto>() };
        dto.Restrictions = feature.ToList().ConvertAll(x => new UiRestrictionDto
        {
            Feature = x.Feature,
            AccessType = x.AccessType,
            AccessRestrictions = new List<AccessRestrictionDto>
            {
                new()
                {
                    AccessRestrictionType = x.AccessRestrictionType,
                    Details = x.Details
                }
            }
        });
        return dto;
    } 
}