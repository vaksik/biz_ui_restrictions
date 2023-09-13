
using Service.Biz.UiRestrictions.BLL.Infrastructure.Attributes;

namespace Service.Biz.UiRestrictions.BLL.Dto
{
    public enum PlaziusFeatureEnumDto
    {
        #region База гостей
        [DataView("GUEST_BASE")]
        GuestBase,
        [DataView("GUEST_BASE_IMPORT")]
        GuestBaseImport,
        [DataView("GUEST_CARD_ADD")]
        GuestCardAdd,
        [DataView("GUEST_CARD_DELETE")]
        GuestCardDelete,
        [DataView("GUEST_CARD_RECOVER")]
        GuestCardRecover,
        [DataView("GUEST_CARD_EDIT")]
        GuestCardEdit,
        [DataView("GUEST_CARD_BENEFIT")]
        GuestCardBenefit,
        [DataView("GUEST_CARD_ORDERS_HISTORY")]
        GuestCardOrdersHistory,
        [DataView("PLUZIUS_GUESTS_TG_BOT")]
        PluziusGuestsTgBot,
        [DataView("PLUZIUS_GUESTS_WALLET")]
        PluziusGuestsWallet,

        #endregion

        #region Приложение Plazius для гостей
        [DataView("PLUZIUS_GUESTS_APP")]
        PluziusGuestsApp,
        [DataView("PLUZIUS_GUESTS_APP_PAYMENT")]
        PluziusGuestsAppPayment,
        [DataView("PLUZIUS_GUESTS_APP_FEEDBACK")]
        PluziusGuestsAppFeedback,
        [DataView("PLUZIUS_GUESTS_APP_PREORDER")]
        PluziusGuestsAppPreorder,
        [DataView("PLUZIUS_GUESTS_APP_TIPS")]
        PluziusGuestsAppTips,

        #endregion

        #region Теги

        [DataView("TAG")]
        Tag,
        [DataView("TAG_ADD")]
        TagAdd,
        [DataView("TAG_DELETE")]
        TagDelete,
        [DataView("TAG_EDIT")]
        TagEdit,
        [DataView("GUEST_BASE_TAG_ASSIGNMENT")]
        GuestBaseTagAssignment,
        
        [DataView("GUEST_BASE_TAG_DELETE")]
        GuestBaseTagDelete,
        [DataView("GUEST_CARD_TAG_ASSIGNMENT")]
        GuestCardTagAssignment,
        [DataView("GUEST_BASE_TAG_AB_DIVIDE")]
        GuestBaseTagAbDivide,
        [DataView("TAG_AUTOMATIC")]
        TagAutomatic,
        [DataView("TAG_AUTOMATIC_ASSIGNMENT")]
        TagAutomaticAssignment,
        [DataView("STOCK_BASIC_TAG_AUTOMATIC_ASSIGNMENT")]
        StockBasicTagAutomaticAssignment,
        [DataView("STOCK_TAG_TRIGGER")]
        StockTagTrigger,
        [DataView("COMMUNICATIONS_TAG_TRIGGER")]
        CommunicationsTagTrigger,
        [DataView("REPORTS_TAG_FILTER_TRIGGER")]
        ReportsTagFilterTrigger,

        #endregion

        #region Коммуникации

        [DataView("COMMUNICATIONS")]
        Communications,
        [DataView("COMMUNICATIONS_SMS")]
        CommunicationsSms,
        [DataView("COMMUNICATIONS_MAIL")]
        CommunicationsMail,
        [DataView("COMMUNICATIONS_MAIL_CONSTRUCTOR")]
        CommunicationsMailConstructor,
        [DataView("COMMUNICATIONS_WALLET")]
        CommunicationsWallet,
        [DataView("RANK")]
        Rank,
        [DataView("CHECK_FOOTER_GUEST_PL")]
        CheckFooterGuestPl,
        [DataView("CHECK_FOOTER_GUEST_ANONYMOUS")]
        CheckFooterGuestAnonymous,

        #endregion

        #region Отчеты

        [DataView("REPORTS_FEEDBACK")]
        ReportsFeedback,
        [DataView("REPORTS_OPERATIONS_TOTAL")]
        ReportsOperationsTotal,
        [DataView("REPORTS_NETWORK_BONUSES_BALANCE")]
        ReportsNetworkBonusesBalance,
        [DataView("REPORTS_WAITERS_RATING")]
        ReportsWaitersRating,
        [DataView("REPORTS_GUESTS_ACTIVITY")]
        ReportsGuestsActivity,
        [DataView("REPORTS_COHORT_ANALYSIS")]
        ReportsCohortAnalysis,
        [DataView("REPORTS_AB_ANALYSIS")]
        ReportsAbAnalysis,
        [DataView("REPORTS_GUESTS_PRIVILEGES")]
        ReportsGuestsPrivileges,
        [DataView("REPORTS_RESTAURANTS_EFFICIENCY")]
        ReportsRestaurantsEfficiency,
        [DataView("REPORTS_STOCK_EFFICIENCY")]
        ReportsStockEfficiency,
        [DataView("REPORTS_RFM_COMMON_VALUES")]
        ReportsRfmCommonValues,
        [DataView("REPORTS_RFM_GUESTS")]
        ReportsRfmGuests,
        [DataView("DASHBOARD_OPERATIONAL_SALES")]
        DashboardOperationalSales,
        [DataView("DASHBOARD_LOYALTY")]
        DashboardLoyalty,
        [DataView("DASHBOARD_PREORDER")]
        DashboardPreorder,
        [DataView("DASHBOARD_WALLET")]
        DashboardWallet,
        [DataView("REPORTS_OPERATION_LOG")]
        ReportsOperationLog,
        [DataView("REPORTS_OPERATION_TIPS")]
        ReportsOperationTips,
        [DataView("REPORTS_COMMUNICATIONS_EFFICIENCY")]
        ReportsCommunicationsEfficiency,
        [DataView("REPORTS_MESSAGE_SENDING_LOG")]
        ReportsMessageSendingLog,
        [DataView("REPORTS_ANTIFRAUD_RESTAURANTS")]
        ReportsAntifraudRestaurants,
        [DataView("REPORTS_ANTIFRAUD_WAITERS")]
        ReportsAntifraudWaiters,
        [DataView("REPORTS_DANGEROUS_OPERATIONS")]
        ReportsDangerousOperations,
        [DataView("NOTIFICATION_DANGEROUS_OPERATIONS")]
        NotificationDangerousOperations,
        [DataView("NOTIFICATION_FEEDBACK")]
        NotificationFeedback,

        #endregion

        #region Антифрод

        [DataView("ANTIFRAUD_SETTINGS")]
        AntifraudSettings,
        [DataView("ANTIFRAUD_GREY_LIST")]
        AntifraudGreyList,
        [DataView("ANTIFRAUD_WHITE_LIST")]
        AntifraudWhiteList,
        [DataView("ANTIFRAUD_BLACK_LIST")]
        AntifraudBlackList,
        [DataView("ANTIFRAUD_BLOCK_LIST")]
        AntifraudBlockList,

        #endregion

        #region Настройки программы лояльности

        [DataView("SETTINGS_PL_DISHES_COEFFICIENTS")]
        SettingsPlDishesCoefficients,

        #endregion

        #region Акции

        [DataView("STOCK")]
        Stock,
        [DataView("STOCK_PROMO_BONUSES")]
        StockPromoBonuses,
        [DataView("STOCK_CORPORATE_CATERING")]
        StockCorporateCatering,
        [DataView("STOCK_BASIC")]
        StockBasic,
        [DataView("STOCK_HAPPY_HOUR")]
        StockHappyHour,
        [DataView("STOCK_BIRTHDAY")]
        StockBirthday,
        [DataView("STOCK_REGISTRATION_BONUS")]
        StockRegistrationBonus,
        [DataView("STOCK_STAMPS")]
        StockStamps,
        [DataView("STOCK_N_DISH_PRESENT")]
        StockNDishPresent,
        [DataView("STOCK_BRING_FRIEND")]
        StockBringFriend,
        [DataView("STOCK_SPECIAL_PRICE_COMBO")]
        StockSpecialPriceCombo,
        [DataView("STOCK_COMBO_DISCOUNT")]
        StockComboDiscount,
        [DataView("STOCK_SEASON_PASS")]
        StockSeasonPass,
        [DataView("STOCK_INFORMATION_CAMPAIGN")]
        StockInformationCampaign,
        [DataView("STOCK_BASIC_PROMO_CODE")]
        StockBasicPromoCode,

        #endregion

        #region Типы привилегий в акциях (Базовая акция, Счастливый час, День рождения, Бонус за регистрацию)

        [DataView("STOCK_CHECK_DISCOUNT_CURRENCY")]
        StockCheckDiscountCurrency,
        [DataView("STOCK_CHECK_DISCOUNT_PERCENT")]
        StockCheckDiscountPercent,
        [DataView("STOCK_DISH_DISCOUNT_CURRENCY")]
        StockDishDiscountCurrency,
        [DataView("STOCK_DISH_DISCOUNT_PERCENT")]
        StockDishDiscountPercent,
        [DataView("STOCK_DISH_PRESENT")]
        StockDishPresent,
        [DataView("STOCK_DISH_SPECIAL_PRICE")]
        StockDishSpecialPrice,
        [DataView("STOCK_BONUS_POINTS")]
        StockBonusPoints,
        
        [DataView("STOCK_CHECK_BONUS_POINTS")]
        StockCheckBonusPoints,
        [DataView("STOCK_DISH_BONUS_POINTS")]
        StockDishBonusPoints,
        [DataView("STOCK_RANK_BONUS_POINTS")]
        StockRankBonusPoints,

        #endregion

        #region dynemic

        [DataView("STOCK_PROMO_BONUSES_ACTIVATION")]
        StockPromoBonusesActivation,
        [DataView("STOCK_CORPORATE_CATERING_ACTIVATION")]
        StockCorporateCateringActivation,
        [DataView("STOCK_BASIC_ACTIVATION")]
        StockBasicActivation,
        [DataView("STOCK_HAPPY_HOUR_ACTIVATION")]
        StockHappyHourActivation,
        [DataView("STOCK_BIRTHDAY_ACTIVATION")]
        StockBirthdayActivation,
        [DataView("STOCK_REGISTRATION_BONUS_ACTIVATION")]
        StockRegistrationBonusActivation,
        [DataView("STOCK_STAMPS_ACTIVATION")]
        StockStampsActivation,
        [DataView("STOCK_N_DISH_PRESENT_ACTIVATION")]
        StockNDishPresentActivation,
        [DataView("STOCK_BRING_FRIEND_ACTIVATION")]
        StockBringFriendActivation,
        [DataView("STOCK_SPECIAL_PRICE_COMBO_ACTIVATION")]
        StockSpecialPriceComboActivation,
        [DataView("STOCK_COMBO_DISCOUNT_ACTIVATION")]
        StockComboDiscountActivation,
        [DataView("STOCK_SEASON_PASS_ACTIVATION")]
        StockSeasonPassActivation,
        [DataView("STOCK_INFORMATION_CAMPAIGN_ACTIVATION")]
        StockInformationCampaignActivation,
        
        #endregion
        [DataView("DASHBOARD_TRENDS")]
        DashboardTrends
    }
}