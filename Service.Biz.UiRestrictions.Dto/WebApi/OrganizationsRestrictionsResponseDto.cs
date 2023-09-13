using System;
using System.Collections.Generic;

namespace Service.Biz.UiRestrictions.Dto.WebApi
{
   public class OrganizationsRestrictionsResponseDto
   {
      public List<OrganizationRestrictionDto> OrganizationRestrictions { get; set; } = null!;
   }
}