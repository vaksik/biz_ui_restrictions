using Service.Biz.UiRestrictions.BLL.Dto;
using Service.Biz.UiRestrictions.Dto;
using Service.Biz.UiRestrictions.Dto.WebApi;

namespace Service.Biz.UiRestrictions.BLL.Services;

public abstract class DynamicFeatureRestrictionsFactory
{
    public static IEnumerable<FeatureRestrictionDto> Create(IEnumerable<FeatureRestrictionDto> constRestrictions)
    {
        var dynamicRestrictions = new List<FeatureRestrictionDto>();
        var dynamicRestrictionSample = new Dictionary<PlaziusFeatureEnumDto, PlaziusFeatureEnumDto>
        {
            { PlaziusFeatureEnumDto.StockPromoBonuses, PlaziusFeatureEnumDto.StockPromoBonusesActivation },
            { PlaziusFeatureEnumDto.StockCorporateCatering, PlaziusFeatureEnumDto.StockCorporateCateringActivation },
            { PlaziusFeatureEnumDto.StockBasic, PlaziusFeatureEnumDto.StockBasicActivation },
            { PlaziusFeatureEnumDto.StockHappyHour, PlaziusFeatureEnumDto.StockHappyHourActivation },
            { PlaziusFeatureEnumDto.StockBirthday, PlaziusFeatureEnumDto.StockBirthdayActivation },
            { PlaziusFeatureEnumDto.StockRegistrationBonus, PlaziusFeatureEnumDto.StockRegistrationBonusActivation },
            { PlaziusFeatureEnumDto.StockStamps, PlaziusFeatureEnumDto.StockStampsActivation },
            { PlaziusFeatureEnumDto.StockNDishPresent, PlaziusFeatureEnumDto.StockNDishPresentActivation },
            { PlaziusFeatureEnumDto.StockBringFriend, PlaziusFeatureEnumDto.StockBringFriendActivation },
            { PlaziusFeatureEnumDto.StockSpecialPriceCombo, PlaziusFeatureEnumDto.StockSpecialPriceComboActivation },
            { PlaziusFeatureEnumDto.StockComboDiscount, PlaziusFeatureEnumDto.StockComboDiscountActivation },
            { PlaziusFeatureEnumDto.StockSeasonPass, PlaziusFeatureEnumDto.StockSeasonPassActivation },
            { PlaziusFeatureEnumDto.StockInformationCampaign, PlaziusFeatureEnumDto.StockInformationCampaignActivation }
        };

        foreach (var restriction in constRestrictions)
        {
            if (!dynamicRestrictionSample.TryGetValue(restriction.Feature, out var activationFeature)) continue;
            var dynamicRestriction = new FeatureRestrictionDto
            {
                Feature = activationFeature,
                AccessType = AccessTypeEnumDto.Disable,
                AccessRestrictionType = AccessRestrictionTypeEnumDto.TariffNotEnough
            };
            dynamicRestrictions.Add(dynamicRestriction);
        }

        return dynamicRestrictions;
    }
}