using Service.Biz.UiRestrictions.BLL.Dto;
using Service.Biz.UiRestrictions.BLL.Infrastructure.Extensions;
using Service.Biz.UiRestrictions.Dto;
using Service.Biz.UiRestrictions.Dto.WebApi;

namespace Service.Biz.UiRestrictions.Host.Mappers;

public static class OrganizationsRestrictionsResponseMapper
{
    public static OrganizationsRestrictionsResponseDto ToDto(this Dictionary<Guid,IEnumerable<FeatureRestrictionDto>> restrictions)
    {
        var response = new OrganizationsRestrictionsResponseDto
        {
            OrganizationRestrictions = new List<OrganizationRestrictionDto>()
        };
        foreach (var restriction in restrictions)
        {
            response.OrganizationRestrictions.AddRange(restriction.Value.ToList()
                .ConvertAll(x=> new OrganizationRestrictionDto
                {
                    OrganizationId = restriction.Key,
                    AccessRestrictionType = x.AccessRestrictionType.GetDataViewStringValue(),
                    AccessType = x.AccessType.GetDataViewStringValue(),
                    Details = x.Details,
                    Feature = x.Feature.GetDataViewStringValue()
                }));
        }
        return response;
    }
}