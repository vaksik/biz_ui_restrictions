#nullable enable
using System;

namespace Service.Biz.UiRestrictions.Dto.WebApi
{
    public class OrganizationRestrictionDto
    {
        public Guid OrganizationId { get; set; }
        public string Feature { get; set; } = null!;

        public string AccessType { get; set; } = null!;

        public string AccessRestrictionType { get; set; } = null!;

        public string? Details { get; set; }
    }
}