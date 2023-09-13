using Service.Biz.UiRestrictions.BLL.Dto;
using Service.Biz.UiRestrictions.Dto;

namespace Service.Biz.UiRestrictions.BLL.Services;

public interface IFeatureRestrictionManagerService
{
    Task<IEnumerable<FeatureRestrictionDto>> GetMergedOrganizationsRestrictionsAsync(Guid? organizationNetworkId, Guid [] organizationIds,
        PlaziusFeatureEnumDto[] features, CancellationToken cancellationToken = default);

    Task<IEnumerable<OrganizationDto>> HaveNotAccessToFeatureAsync(Guid[] organizationIds, PlaziusFeatureEnumDto feature,
        CancellationToken cancellationToken = default);

    Task<Dictionary<Guid, IEnumerable<FeatureRestrictionDto>>> GetOrganizationsRestrictionsAsync(Guid[] organizationIds,
        PlaziusFeatureEnumDto[] features, CancellationToken cancellationToken = default);
    
   

}