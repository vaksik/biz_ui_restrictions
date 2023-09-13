#nullable enable
using System;

namespace Service.Biz.UiRestrictions.Dto.WebApi
{
    public class GetOrganizationsNotAvailableRequestDto
    {
        public Guid[]? OrganizationIds { get; set; }

        public Guid[]? NetworkIds { get; set; }
        public string PlaziusFeature { get; set; } = null!;
    }
}