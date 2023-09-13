using System.Reflection;
using JetBrains.Annotations;
using Service.Biz.UiRestrictions.BLL.Dto;
using Service.Biz.UiRestrictions.BLL.Infrastructure.Attributes;
using Service.Biz.UiRestrictions.BLL.Infrastructure.Extensions;
using Service.Biz.UiRestrictions.Dto;

// ReSharper disable InconsistentNaming

namespace Service.Biz.UiRestrictions.BLL.Infrastructure;

public static class FeatureHelper
{
    /// <summary>
    /// Приведение 
    /// </summary>
    /// <param name="features"></param>
    /// <returns></returns>
    public static string[] ToArrayForSearch(this PlaziusFeatureEnumDto[] features)
    {
        return !features.Any() ? 
            Enum.GetValues(typeof(InternalFeatures)).Cast<InternalFeatures>().Where(x=>x != InternalFeatures.Unknown)
                .Select(x=> x.GetDataViewStringValue()).ToArray()
            : 
            features.ExcludeDynamicFeatures().Select(f => f.ToValueForSearch()).ToArray();
    }

    public static string ToValueForSearch(this PlaziusFeatureEnumDto feature) =>
        feature.ToInternalFeature().GetDataViewStringValue();
    
    public static T GetEnumValueFromDataViewValue<T>(string stringValue) where T : Enum
    {
        var enumType = typeof(T);

        foreach (var field in enumType.GetFields(BindingFlags.Static | BindingFlags.Public))
        {
            var attribute = field.GetCustomAttribute<DataViewAttribute>();
            if (attribute != null && attribute.Value == stringValue)
            {
                return (T)field.GetValue(null);
            }
        }
        throw new ArgumentException($"No enum value with the provided string value '{stringValue}' was found.");
    }
    
    public static PlaziusFeatureEnumDto[] ExcludeDynamicFeatures(this PlaziusFeatureEnumDto[] features)
    {
        var dynamicFeatures = new[]
        {
            PlaziusFeatureEnumDto.StockPromoBonusesActivation,
            PlaziusFeatureEnumDto.StockCorporateCateringActivation,
            PlaziusFeatureEnumDto.StockBasicActivation,
            PlaziusFeatureEnumDto.StockHappyHourActivation,
            PlaziusFeatureEnumDto.StockBirthdayActivation,
            PlaziusFeatureEnumDto.StockRegistrationBonusActivation,
            PlaziusFeatureEnumDto.StockStampsActivation,
            PlaziusFeatureEnumDto.StockNDishPresentActivation,
            PlaziusFeatureEnumDto.StockBringFriendActivation,
            PlaziusFeatureEnumDto.StockSpecialPriceComboActivation,
            PlaziusFeatureEnumDto.StockComboDiscountActivation,
            PlaziusFeatureEnumDto.StockSeasonPassActivation,
            PlaziusFeatureEnumDto.StockInformationCampaignActivation
        };
        return features.Where(x => !dynamicFeatures.Contains(x)).ToArray();
    }
}