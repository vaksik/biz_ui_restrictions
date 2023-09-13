using FluentValidation;
using Service.Biz.UiRestrictions.Dto;
using Service.Biz.UiRestrictions.Dto.WebApi;

namespace Service.Biz.UiRestrictions.BLL.Validators;

public class GetOrganizationsRestrictionsRequestValidator : AbstractValidator<GetOrganizationsRestrictionsRequestDto>
{
    public GetOrganizationsRestrictionsRequestValidator()
    {
        RuleFor(dto => dto.OrganizationIds).NotNull().NotEmpty();
        RuleFor(dto => dto.Features).NotNull();
    }
}