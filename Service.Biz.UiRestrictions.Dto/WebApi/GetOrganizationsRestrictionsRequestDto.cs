using System;

namespace Service.Biz.UiRestrictions.Dto.WebApi
{

    public class GetOrganizationsRestrictionsRequestDto
    {
        public Guid[] OrganizationIds { get; set; } = null!;

        public string[] Features { get; set; } = null!;
    }
}