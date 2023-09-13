using Service.Biz.UiRestrictions.BLL.Dto;
using Service.Biz.UiRestrictions.DAL.Entities;
using Service.Biz.UiRestrictions.Dto;
using Service.Biz.UiRestrictions.Dto.WebApi;

namespace Service.Biz.UiRestrictions.BLL.Infrastructure;

public static class Mapper
{

    public static FeatureRestrictionDto ToDto(this ProductFeatureRestriction source)
    {
        return new FeatureRestrictionDto
        {
            AccessType = source.AccessType.ToDto(),
            Feature = source.Feature.ToDto(),
            AccessRestrictionType = source.AccessRestrictionType.ToDto(),
            Details = source.Detail
        };
    }

    private static AccessTypeEnumDto ToDto(this AccessType source)
    {
        return source switch
        {
            AccessType.Disable => AccessTypeEnumDto.Disable,
            AccessType.Hidden => AccessTypeEnumDto.Hidden,
            _ => throw new ArgumentOutOfRangeException(nameof(source), source, null)
        };
    }

    private static AccessRestrictionTypeEnumDto ToDto(this AccessRestrictionType source)
    {
        return source switch
        {
            AccessRestrictionType.TariffNotEnough => AccessRestrictionTypeEnumDto.TariffNotEnough,
            _ => throw new ArgumentOutOfRangeException(nameof(source), source, null)
        };
    }

    private static PlaziusFeatureEnumDto ToDto(this Feature source)
{
    var feature = FeatureHelper.GetEnumValueFromDataViewValue<InternalFeatures>(source.Name);
    return feature switch
    {
        InternalFeatures.GuestBase => PlaziusFeatureEnumDto.GuestBase,
        InternalFeatures.GuestBaseImport => PlaziusFeatureEnumDto.GuestBaseImport,
        InternalFeatures.GuestCardAdd => PlaziusFeatureEnumDto.GuestCardAdd,
        InternalFeatures.GuestCardDelete => PlaziusFeatureEnumDto.GuestCardDelete,
        InternalFeatures.GuestCardRecover => PlaziusFeatureEnumDto.GuestCardRecover,
        InternalFeatures.GuestCardEdit => PlaziusFeatureEnumDto.GuestCardEdit,
        InternalFeatures.GuestCardBenefit => PlaziusFeatureEnumDto.GuestCardBenefit,
        InternalFeatures.GuestCardOrdersHistory => PlaziusFeatureEnumDto.GuestCardOrdersHistory,
        InternalFeatures.PluziusGuestsTgBot => PlaziusFeatureEnumDto.PluziusGuestsTgBot,
        InternalFeatures.PluziusGuestsWallet => PlaziusFeatureEnumDto.PluziusGuestsWallet,
        InternalFeatures.PluziusGuestsApp => PlaziusFeatureEnumDto.PluziusGuestsApp,
        InternalFeatures.PluziusGuestsAppPayment => PlaziusFeatureEnumDto.PluziusGuestsAppPayment,
        InternalFeatures.PluziusGuestsAppFeedback => PlaziusFeatureEnumDto.PluziusGuestsAppFeedback,
        InternalFeatures.PluziusGuestsAppPreorder => PlaziusFeatureEnumDto.PluziusGuestsAppPreorder,
        InternalFeatures.PluziusGuestsAppTips => PlaziusFeatureEnumDto.PluziusGuestsAppTips,
        InternalFeatures.Tag => PlaziusFeatureEnumDto.Tag,
        InternalFeatures.TagAdd => PlaziusFeatureEnumDto.TagAdd,
        InternalFeatures.TagDelete => PlaziusFeatureEnumDto.TagDelete,
        InternalFeatures.TagEdit => PlaziusFeatureEnumDto.TagEdit,
        InternalFeatures.GuestBaseTagAssignment => PlaziusFeatureEnumDto.GuestBaseTagAssignment,
        InternalFeatures.GuestBaseTagDelete => PlaziusFeatureEnumDto.GuestBaseTagDelete,
        InternalFeatures.GuestCardTagAssignment => PlaziusFeatureEnumDto.GuestCardTagAssignment,
        InternalFeatures.GuestBaseTagAbDivide => PlaziusFeatureEnumDto.GuestBaseTagAbDivide,
        InternalFeatures.TagAutomatic => PlaziusFeatureEnumDto.TagAutomatic,
        InternalFeatures.TagAutomaticAssignment => PlaziusFeatureEnumDto.TagAutomaticAssignment,
        InternalFeatures.StockBasicTagAutomaticAssignment => PlaziusFeatureEnumDto.StockBasicTagAutomaticAssignment,
        InternalFeatures.StockTagTrigger => PlaziusFeatureEnumDto.StockTagTrigger,
        InternalFeatures.CommunicationsTagTrigger => PlaziusFeatureEnumDto.CommunicationsTagTrigger,
        InternalFeatures.ReportsTagFilterTrigger => PlaziusFeatureEnumDto.ReportsTagFilterTrigger,
        InternalFeatures.Communications => PlaziusFeatureEnumDto.Communications,
        InternalFeatures.CommunicationsSms => PlaziusFeatureEnumDto.CommunicationsSms,
        InternalFeatures.CommunicationsMail => PlaziusFeatureEnumDto.CommunicationsMail,
        InternalFeatures.CommunicationsMailConstructor => PlaziusFeatureEnumDto.CommunicationsMailConstructor,
        InternalFeatures.CommunicationsWallet => PlaziusFeatureEnumDto.CommunicationsWallet,
        InternalFeatures.Rank => PlaziusFeatureEnumDto.Rank,
        InternalFeatures.CheckFooterGuestPl => PlaziusFeatureEnumDto.CheckFooterGuestPl,
        InternalFeatures.CheckFooterGuestAnonymous => PlaziusFeatureEnumDto.CheckFooterGuestAnonymous,
        InternalFeatures.ReportsFeedback => PlaziusFeatureEnumDto.ReportsFeedback,
        InternalFeatures.ReportsOperationsTotal => PlaziusFeatureEnumDto.ReportsOperationsTotal,
        InternalFeatures.ReportsNetworkBonusesBalance => PlaziusFeatureEnumDto.ReportsNetworkBonusesBalance,
        InternalFeatures.ReportsWaitersRating => PlaziusFeatureEnumDto.ReportsWaitersRating,
        InternalFeatures.ReportsGuestsActivity => PlaziusFeatureEnumDto.ReportsGuestsActivity,
        InternalFeatures.ReportsCohortAnalysis => PlaziusFeatureEnumDto.ReportsCohortAnalysis,
        InternalFeatures.ReportsAbAnalysis => PlaziusFeatureEnumDto.ReportsAbAnalysis,
        InternalFeatures.ReportsGuestsPrivileges => PlaziusFeatureEnumDto.ReportsGuestsPrivileges,
        InternalFeatures.ReportsRestaurantsEfficiency => PlaziusFeatureEnumDto.ReportsRestaurantsEfficiency,
        InternalFeatures.ReportsStockEfficiency => PlaziusFeatureEnumDto.ReportsStockEfficiency,
        InternalFeatures.ReportsRfmCommonValues => PlaziusFeatureEnumDto.ReportsRfmCommonValues,
        InternalFeatures.ReportsRfmGuests => PlaziusFeatureEnumDto.ReportsRfmGuests,
        InternalFeatures.DashboardOperationalSales => PlaziusFeatureEnumDto.DashboardOperationalSales,
        InternalFeatures.DashboardLoyalty => PlaziusFeatureEnumDto.DashboardLoyalty,
        InternalFeatures.DashboardPreorder => PlaziusFeatureEnumDto.DashboardPreorder,
        InternalFeatures.DashboardWallet => PlaziusFeatureEnumDto.DashboardWallet,
        InternalFeatures.ReportsOperationLog => PlaziusFeatureEnumDto.ReportsOperationLog,
        InternalFeatures.ReportsOperationTips => PlaziusFeatureEnumDto.ReportsOperationTips,
        InternalFeatures.ReportsCommunicationsEfficiency => PlaziusFeatureEnumDto.ReportsCommunicationsEfficiency,
        InternalFeatures.ReportsMessageSendingLog => PlaziusFeatureEnumDto.ReportsMessageSendingLog,
        InternalFeatures.ReportsAntifraudRestaurants => PlaziusFeatureEnumDto.ReportsAntifraudRestaurants,
        InternalFeatures.ReportsAntifraudWaiters => PlaziusFeatureEnumDto.ReportsAntifraudWaiters,
        InternalFeatures.ReportsDangerousOperations => PlaziusFeatureEnumDto.ReportsDangerousOperations,
        InternalFeatures.NotificationDangerousOperations => PlaziusFeatureEnumDto.NotificationDangerousOperations,
        InternalFeatures.NotificationFeedback => PlaziusFeatureEnumDto.NotificationFeedback,
        InternalFeatures.AntifraudSettings => PlaziusFeatureEnumDto.AntifraudSettings,
        InternalFeatures.AntifraudGreyList => PlaziusFeatureEnumDto.AntifraudGreyList,
        InternalFeatures.AntifraudWhiteList => PlaziusFeatureEnumDto.AntifraudWhiteList,
        InternalFeatures.AntifraudBlackList => PlaziusFeatureEnumDto.AntifraudBlackList,
        InternalFeatures.AntifraudBlockList => PlaziusFeatureEnumDto.AntifraudBlockList,
        InternalFeatures.SettingsPlDishesCoefficients => PlaziusFeatureEnumDto.SettingsPlDishesCoefficients,
        InternalFeatures.Stock => PlaziusFeatureEnumDto.Stock,
        InternalFeatures.StockPromoBonuses => PlaziusFeatureEnumDto.StockPromoBonuses,
        InternalFeatures.StockCorporateCatering => PlaziusFeatureEnumDto.StockCorporateCatering,
        InternalFeatures.StockBasic => PlaziusFeatureEnumDto.StockBasic,
        InternalFeatures.StockHappyHour => PlaziusFeatureEnumDto.StockHappyHour,
        InternalFeatures.StockBirthday => PlaziusFeatureEnumDto.StockBirthday,
        InternalFeatures.StockRegistrationBonus => PlaziusFeatureEnumDto.StockRegistrationBonus,
        InternalFeatures.StockStamps => PlaziusFeatureEnumDto.StockStamps,
        InternalFeatures.StockNDishPresent => PlaziusFeatureEnumDto.StockNDishPresent,
        InternalFeatures.StockBringFriend => PlaziusFeatureEnumDto.StockBringFriend,
        InternalFeatures.StockSpecialPriceCombo => PlaziusFeatureEnumDto.StockSpecialPriceCombo,
        InternalFeatures.StockComboDiscount => PlaziusFeatureEnumDto.StockComboDiscount,
        InternalFeatures.StockSeasonPass => PlaziusFeatureEnumDto.StockSeasonPass,
        InternalFeatures.StockInformationCampaign => PlaziusFeatureEnumDto.StockInformationCampaign,
        InternalFeatures.StockCheckDiscountCurrency => PlaziusFeatureEnumDto.StockCheckDiscountCurrency,
        InternalFeatures.StockCheckDiscountPercent => PlaziusFeatureEnumDto.StockCheckDiscountPercent,
        InternalFeatures.StockDishDiscountCurrency => PlaziusFeatureEnumDto.StockDishDiscountCurrency,
        InternalFeatures.StockDishDiscountPercent => PlaziusFeatureEnumDto.StockDishDiscountPercent,
        InternalFeatures.StockDishPresent => PlaziusFeatureEnumDto.StockDishPresent,
        InternalFeatures.StockDishSpecialPrice => PlaziusFeatureEnumDto.StockDishSpecialPrice,
        InternalFeatures.StockBonusPoints => PlaziusFeatureEnumDto.StockBonusPoints,
        InternalFeatures.StockCheckBonusPoints => PlaziusFeatureEnumDto.StockCheckBonusPoints,
        InternalFeatures.StockDishBonusPoints => PlaziusFeatureEnumDto.StockDishBonusPoints,
        InternalFeatures.StockRankBonusPoints => PlaziusFeatureEnumDto.StockRankBonusPoints,
        InternalFeatures.StockPromoBonusesActivation => PlaziusFeatureEnumDto.StockPromoBonusesActivation,
        InternalFeatures.StockCorporateCateringActivation => PlaziusFeatureEnumDto.StockCorporateCateringActivation,
        InternalFeatures.StockBasicActivation => PlaziusFeatureEnumDto.StockBasicActivation,
        InternalFeatures.StockHappyHourActivation => PlaziusFeatureEnumDto.StockHappyHourActivation,
        InternalFeatures.StockBirthdayActivation => PlaziusFeatureEnumDto.StockBirthdayActivation,
        InternalFeatures.StockRegistrationBonusActivation => PlaziusFeatureEnumDto.StockRegistrationBonusActivation,
        InternalFeatures.StockStampsActivation => PlaziusFeatureEnumDto.StockStampsActivation,
        InternalFeatures.StockNDishPresentActivation => PlaziusFeatureEnumDto.StockNDishPresentActivation,
        InternalFeatures.StockBringFriendActivation => PlaziusFeatureEnumDto.StockBringFriendActivation,
        InternalFeatures.StockSpecialPriceComboActivation => PlaziusFeatureEnumDto.StockSpecialPriceComboActivation,
        InternalFeatures.StockComboDiscountActivation => PlaziusFeatureEnumDto.StockComboDiscountActivation,
        InternalFeatures.StockSeasonPassActivation => PlaziusFeatureEnumDto.StockSeasonPassActivation,
        InternalFeatures.StockInformationCampaignActivation => PlaziusFeatureEnumDto.StockInformationCampaignActivation,
        InternalFeatures.DashboardTrends => PlaziusFeatureEnumDto.DashboardTrends,
        InternalFeatures.StockBasicPromoCode => PlaziusFeatureEnumDto.StockBasicPromoCode,
        _ => throw new ArgumentOutOfRangeException()
    };
}
public static InternalFeatures ToInternalFeature(this PlaziusFeatureEnumDto plaziusFeature)
{
    return plaziusFeature switch
    {
        PlaziusFeatureEnumDto.GuestBase => InternalFeatures.GuestBase,
        PlaziusFeatureEnumDto.GuestBaseImport => InternalFeatures.GuestBaseImport,
        PlaziusFeatureEnumDto.GuestCardAdd => InternalFeatures.GuestCardAdd,
        PlaziusFeatureEnumDto.GuestCardDelete => InternalFeatures.GuestCardDelete,
        PlaziusFeatureEnumDto.GuestCardRecover => InternalFeatures.GuestCardRecover,
        PlaziusFeatureEnumDto.GuestCardEdit => InternalFeatures.GuestCardEdit,
        PlaziusFeatureEnumDto.GuestCardBenefit => InternalFeatures.GuestCardBenefit,
        PlaziusFeatureEnumDto.GuestCardOrdersHistory => InternalFeatures.GuestCardOrdersHistory,
        PlaziusFeatureEnumDto.PluziusGuestsTgBot => InternalFeatures.PluziusGuestsTgBot,
        PlaziusFeatureEnumDto.PluziusGuestsWallet => InternalFeatures.PluziusGuestsWallet,
        PlaziusFeatureEnumDto.PluziusGuestsApp => InternalFeatures.PluziusGuestsApp,
        PlaziusFeatureEnumDto.PluziusGuestsAppPayment => InternalFeatures.PluziusGuestsAppPayment,
        PlaziusFeatureEnumDto.PluziusGuestsAppFeedback => InternalFeatures.PluziusGuestsAppFeedback,
        PlaziusFeatureEnumDto.PluziusGuestsAppPreorder => InternalFeatures.PluziusGuestsAppPreorder,
        PlaziusFeatureEnumDto.PluziusGuestsAppTips => InternalFeatures.PluziusGuestsAppTips,
        PlaziusFeatureEnumDto.Tag => InternalFeatures.Tag,
        PlaziusFeatureEnumDto.TagAdd => InternalFeatures.TagAdd,
        PlaziusFeatureEnumDto.TagDelete => InternalFeatures.TagDelete,
        PlaziusFeatureEnumDto.TagEdit => InternalFeatures.TagEdit,
        PlaziusFeatureEnumDto.GuestBaseTagAssignment => InternalFeatures.GuestBaseTagAssignment,
        PlaziusFeatureEnumDto.GuestBaseTagDelete => InternalFeatures.GuestBaseTagDelete,
        PlaziusFeatureEnumDto.GuestCardTagAssignment => InternalFeatures.GuestCardTagAssignment,
        PlaziusFeatureEnumDto.GuestBaseTagAbDivide => InternalFeatures.GuestBaseTagAbDivide,
        PlaziusFeatureEnumDto.TagAutomatic => InternalFeatures.TagAutomatic,
        PlaziusFeatureEnumDto.TagAutomaticAssignment => InternalFeatures.TagAutomaticAssignment,
        PlaziusFeatureEnumDto.StockBasicTagAutomaticAssignment => InternalFeatures.StockBasicTagAutomaticAssignment,
        PlaziusFeatureEnumDto.StockTagTrigger => InternalFeatures.StockTagTrigger,
        PlaziusFeatureEnumDto.CommunicationsTagTrigger => InternalFeatures.CommunicationsTagTrigger,
        PlaziusFeatureEnumDto.ReportsTagFilterTrigger => InternalFeatures.ReportsTagFilterTrigger,
        PlaziusFeatureEnumDto.Communications => InternalFeatures.Communications,
        PlaziusFeatureEnumDto.CommunicationsSms => InternalFeatures.CommunicationsSms,
        PlaziusFeatureEnumDto.CommunicationsMail => InternalFeatures.CommunicationsMail,
        PlaziusFeatureEnumDto.CommunicationsMailConstructor => InternalFeatures.CommunicationsMailConstructor,
        PlaziusFeatureEnumDto.CommunicationsWallet => InternalFeatures.CommunicationsWallet,
        PlaziusFeatureEnumDto.Rank => InternalFeatures.Rank,
        PlaziusFeatureEnumDto.CheckFooterGuestPl => InternalFeatures.CheckFooterGuestPl,
        PlaziusFeatureEnumDto.CheckFooterGuestAnonymous => InternalFeatures.CheckFooterGuestAnonymous,
        PlaziusFeatureEnumDto.ReportsFeedback => InternalFeatures.ReportsFeedback,
        PlaziusFeatureEnumDto.ReportsOperationsTotal => InternalFeatures.ReportsOperationsTotal,
        PlaziusFeatureEnumDto.ReportsNetworkBonusesBalance => InternalFeatures.ReportsNetworkBonusesBalance,
        PlaziusFeatureEnumDto.ReportsWaitersRating => InternalFeatures.ReportsWaitersRating,
        PlaziusFeatureEnumDto.ReportsGuestsActivity => InternalFeatures.ReportsGuestsActivity,
        PlaziusFeatureEnumDto.ReportsCohortAnalysis => InternalFeatures.ReportsCohortAnalysis,
        PlaziusFeatureEnumDto.ReportsAbAnalysis => InternalFeatures.ReportsAbAnalysis,
        PlaziusFeatureEnumDto.ReportsGuestsPrivileges => InternalFeatures.ReportsGuestsPrivileges,
        PlaziusFeatureEnumDto.ReportsRestaurantsEfficiency => InternalFeatures.ReportsRestaurantsEfficiency,
        PlaziusFeatureEnumDto.ReportsStockEfficiency => InternalFeatures.ReportsStockEfficiency,
        PlaziusFeatureEnumDto.ReportsRfmCommonValues => InternalFeatures.ReportsRfmCommonValues,
        PlaziusFeatureEnumDto.ReportsRfmGuests => InternalFeatures.ReportsRfmGuests,
        PlaziusFeatureEnumDto.DashboardOperationalSales => InternalFeatures.DashboardOperationalSales,
        PlaziusFeatureEnumDto.DashboardLoyalty => InternalFeatures.DashboardLoyalty,
        PlaziusFeatureEnumDto.DashboardPreorder => InternalFeatures.DashboardPreorder,
        PlaziusFeatureEnumDto.DashboardWallet => InternalFeatures.DashboardWallet,
        PlaziusFeatureEnumDto.ReportsOperationLog => InternalFeatures.ReportsOperationLog,
        PlaziusFeatureEnumDto.ReportsOperationTips => InternalFeatures.ReportsOperationTips,
        PlaziusFeatureEnumDto.ReportsCommunicationsEfficiency => InternalFeatures.ReportsCommunicationsEfficiency,
        PlaziusFeatureEnumDto.ReportsMessageSendingLog => InternalFeatures.ReportsMessageSendingLog,
        PlaziusFeatureEnumDto.ReportsAntifraudRestaurants => InternalFeatures.ReportsAntifraudRestaurants,
        PlaziusFeatureEnumDto.ReportsAntifraudWaiters => InternalFeatures.ReportsAntifraudWaiters,
        PlaziusFeatureEnumDto.ReportsDangerousOperations => InternalFeatures.ReportsDangerousOperations,
        PlaziusFeatureEnumDto.NotificationDangerousOperations => InternalFeatures.NotificationDangerousOperations,
        PlaziusFeatureEnumDto.NotificationFeedback => InternalFeatures.NotificationFeedback,
        PlaziusFeatureEnumDto.AntifraudSettings => InternalFeatures.AntifraudSettings,
        PlaziusFeatureEnumDto.AntifraudGreyList => InternalFeatures.AntifraudGreyList,
        PlaziusFeatureEnumDto.AntifraudWhiteList => InternalFeatures.AntifraudWhiteList,
        PlaziusFeatureEnumDto.AntifraudBlackList => InternalFeatures.AntifraudBlackList,
        PlaziusFeatureEnumDto.AntifraudBlockList => InternalFeatures.AntifraudBlockList,
        PlaziusFeatureEnumDto.SettingsPlDishesCoefficients => InternalFeatures.SettingsPlDishesCoefficients,
        PlaziusFeatureEnumDto.Stock => InternalFeatures.Stock,
        PlaziusFeatureEnumDto.StockPromoBonuses => InternalFeatures.StockPromoBonuses,
        PlaziusFeatureEnumDto.StockCorporateCatering => InternalFeatures.StockCorporateCatering,
        PlaziusFeatureEnumDto.StockBasic => InternalFeatures.StockBasic,
        PlaziusFeatureEnumDto.StockHappyHour => InternalFeatures.StockHappyHour,
        PlaziusFeatureEnumDto.StockBirthday => InternalFeatures.StockBirthday,
        PlaziusFeatureEnumDto.StockRegistrationBonus => InternalFeatures.StockRegistrationBonus,
        PlaziusFeatureEnumDto.StockStamps => InternalFeatures.StockStamps,
        PlaziusFeatureEnumDto.StockNDishPresent => InternalFeatures.StockNDishPresent,
        PlaziusFeatureEnumDto.StockBringFriend => InternalFeatures.StockBringFriend,
        PlaziusFeatureEnumDto.StockSpecialPriceCombo => InternalFeatures.StockSpecialPriceCombo,
        PlaziusFeatureEnumDto.StockComboDiscount => InternalFeatures.StockComboDiscount,
        PlaziusFeatureEnumDto.StockSeasonPass => InternalFeatures.StockSeasonPass,
        PlaziusFeatureEnumDto.StockInformationCampaign => InternalFeatures.StockInformationCampaign,
        PlaziusFeatureEnumDto.StockCheckDiscountCurrency => InternalFeatures.StockCheckDiscountCurrency,
        PlaziusFeatureEnumDto.StockCheckDiscountPercent => InternalFeatures.StockCheckDiscountPercent,
        PlaziusFeatureEnumDto.StockDishDiscountCurrency => InternalFeatures.StockDishDiscountCurrency,
        PlaziusFeatureEnumDto.StockDishDiscountPercent => InternalFeatures.StockDishDiscountPercent,
        PlaziusFeatureEnumDto.StockDishPresent => InternalFeatures.StockDishPresent,
        PlaziusFeatureEnumDto.StockDishSpecialPrice => InternalFeatures.StockDishSpecialPrice,
        PlaziusFeatureEnumDto.StockBonusPoints => InternalFeatures.StockBonusPoints,
        PlaziusFeatureEnumDto.StockCheckBonusPoints => InternalFeatures.StockCheckBonusPoints,
        PlaziusFeatureEnumDto.StockDishBonusPoints => InternalFeatures.StockDishBonusPoints,
        PlaziusFeatureEnumDto.StockRankBonusPoints => InternalFeatures.StockRankBonusPoints,
        PlaziusFeatureEnumDto.DashboardTrends => InternalFeatures.DashboardTrends,
        PlaziusFeatureEnumDto.StockBasicPromoCode => InternalFeatures.StockBasicPromoCode,
        _ => InternalFeatures.Unknown
    };
}
    
}