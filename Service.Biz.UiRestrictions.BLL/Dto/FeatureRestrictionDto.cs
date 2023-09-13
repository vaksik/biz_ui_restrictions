#nullable enable
namespace Service.Biz.UiRestrictions.BLL.Dto
{
    public class FeatureRestrictionDto
    {
        public PlaziusFeatureEnumDto Feature { get; set; }

        public AccessTypeEnumDto AccessType { get; set; }

        public AccessRestrictionTypeEnumDto AccessRestrictionType { get; set; }

        public string? Details { get; set; }
    }
}