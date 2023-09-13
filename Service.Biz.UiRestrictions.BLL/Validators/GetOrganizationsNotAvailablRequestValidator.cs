using FluentValidation;
using Service.Biz.UiRestrictions.Dto;
using Service.Biz.UiRestrictions.Dto.WebApi;

namespace Service.Biz.UiRestrictions.BLL.Validators;

public class GetOrganizationsNotAvailablRequestValidator : AbstractValidator<GetOrganizationsNotAvailableRequestDto>
{
    public GetOrganizationsNotAvailablRequestValidator()
    {
        RuleFor(dto => dto.OrganizationIds).NotNull().NotEmpty().When(dto => dto.NetworkIds is null || !dto.NetworkIds.Any());
        RuleFor(dto => dto.PlaziusFeature).NotNull().NotEmpty().When(dto => dto.OrganizationIds is null || !dto.OrganizationIds.Any());
    }
}