using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Service.Biz.UiRestrictions.BLL.Dto;
using Service.Biz.UiRestrictions.BLL.Infrastructure;
using Service.Biz.UiRestrictions.BLL.Infrastructure.Extensions;
using Service.Biz.UiRestrictions.BLL.Services;
using Service.Biz.UiRestrictions.Dto.WebApi;
using Service.Biz.UiRestrictions.Host.Mappers;

namespace Service.Biz.UiRestrictions.Host.Controllers;

[Route("api/v1/features")]
public class FeatureRestrictionsController  : Controller
{
    private readonly IFeatureRestrictionManagerService _featureRestrictionManagerService;
    private readonly IOrganizationService _organizationService;

    private readonly IValidator<GetOrganizationsNotAvailableRequestDto> _organizationsAccessRequestValidator;
    private readonly IValidator<GetOrganizationsRestrictionsRequestDto> _organizationsRestrictionsRequestValidator;

    public FeatureRestrictionsController(IFeatureRestrictionManagerService featureRestrictionManagerService,
        IOrganizationService organizationService,
        IValidator<GetOrganizationsNotAvailableRequestDto> organizationsAccessRequestValidator,
        IValidator<GetOrganizationsRestrictionsRequestDto> organizationsRestrictionsRequestValidator)
    {
        _featureRestrictionManagerService = featureRestrictionManagerService;
        _organizationService = organizationService;
        _organizationsAccessRequestValidator = organizationsAccessRequestValidator;
        _organizationsRestrictionsRequestValidator = organizationsRestrictionsRequestValidator;
    }

    [HttpPost, Route("organizations/notAvailable")]
    public async Task<OrganizationsNotAvailableResponseDto> HaveNotAccessToFeatureAsync([FromBody]GetOrganizationsNotAvailableRequestDto request,
        CancellationToken cancellationToken)
    {
        await _organizationsAccessRequestValidator.ThrowIfInvalidAsync(request, cancellationToken);
        var featureEnum = FeatureHelper.GetEnumValueFromDataViewValue<PlaziusFeatureEnumDto>(request.PlaziusFeature);
        
        var resolveOrganizations =
            await _organizationService.GetOrganizationsWithProductAsync(request.NetworkIds, request.OrganizationIds,
                cancellationToken);
        
        var notAvailableOrganizations = await _featureRestrictionManagerService.HaveNotAccessToFeatureAsync(
            resolveOrganizations.ToArray(),
            featureEnum, cancellationToken);
        return new OrganizationsNotAvailableResponseDto { NotAvailableOrganizations = notAvailableOrganizations.ToArray() };
    }

    [HttpPost, Route("organizations/restrictions")]
    public async Task<OrganizationsRestrictionsResponseDto> GetOrganizationsRestrictionsAsync([FromBody]GetOrganizationsRestrictionsRequestDto request,
        CancellationToken cancellationToken = default)
    {
        await _organizationsRestrictionsRequestValidator.ThrowIfInvalidAsync(request, cancellationToken);
       
        var featureEnums = request.Features.Select(FeatureHelper.GetEnumValueFromDataViewValue<PlaziusFeatureEnumDto>).ToArray();
        
        var organizationsRestrictions = await _featureRestrictionManagerService.GetOrganizationsRestrictionsAsync(
            request.OrganizationIds,
            featureEnums, cancellationToken);
        
        return organizationsRestrictions.ToDto();
    }
}