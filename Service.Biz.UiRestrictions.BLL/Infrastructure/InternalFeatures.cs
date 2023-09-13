using Service.Biz.UiRestrictions.BLL.Infrastructure.Attributes;

namespace Service.Biz.UiRestrictions.BLL.Infrastructure;

public enum InternalFeatures
{ 
        Unknown = 0,
        #region База гостей
        [DataView("guest_base")]
        GuestBase,
        [DataView("guest_base_import")]
        GuestBaseImport,
        [DataView("guest_card_add")]
        GuestCardAdd,
        [DataView("guest_card_delete")]
        GuestCardDelete,
        [DataView("guest_card_recover")]
        GuestCardRecover,
        [DataView("guest_card_edit")]
        GuestCardEdit,
        [DataView("guest_card_benefit")]
        GuestCardBenefit,
        [DataView("guest_card_orders_history")]
        GuestCardOrdersHistory,
        [DataView("pluzius_guests_tg_bot")]
        PluziusGuestsTgBot,
        [DataView("pluzius_guests_wallet")]
        PluziusGuestsWallet,
        #endregion
        #region Приложение Plazius для гостей
        [DataView("pluzius_guests_app")]
        PluziusGuestsApp,
        [DataView("pluzius_guests_app_payment")]
        PluziusGuestsAppPayment,
        [DataView("pluzius_guests_app_feedback")]
        PluziusGuestsAppFeedback,
        [DataView("pluzius_guests_app_preorder")]
        PluziusGuestsAppPreorder,
        [DataView("pluzius_guests_app_tips")]
        PluziusGuestsAppTips,
        #endregion
        #region Теги
        [DataView("tag")]
        Tag,
        [DataView("tag_add")]
        TagAdd,
        [DataView("tag_delete")]
        TagDelete,
        [DataView("tag_edit")]
        TagEdit,
        [DataView("guest_base_tag_assignment")]
        GuestBaseTagAssignment,
        [DataView("guest_base_tag_delete")] 
        GuestBaseTagDelete,
        [DataView("guest_card_tag_assignment")]
        GuestCardTagAssignment,
        [DataView("guest_base_tag_ab_divide")
        ]GuestBaseTagAbDivide,
        [DataView("tag_automatic")]
        TagAutomatic,
        [DataView("tag_automatic_assignment")]
        TagAutomaticAssignment,
        [DataView("stock_basic_tag_automatic_assignment")]
        StockBasicTagAutomaticAssignment,
        [DataView("stock_tag_trigger")]
        StockTagTrigger,
        [DataView("communications_tag_trigger")]
        CommunicationsTagTrigger,
        
        [DataView("reports_tag_filter_trigger")]
        ReportsTagFilterTrigger,
        #endregion
        #region Коммуникации
        [DataView("communications")]
        Communications,
        [DataView("communications_sms")]
        CommunicationsSms,
        [DataView("communications_mail")]
        CommunicationsMail,
        [DataView("communications_mail_constructor")]
        CommunicationsMailConstructor,
        [DataView("communications_wallet")]
        CommunicationsWallet,
        [DataView("rank")]
        Rank,
        [DataView("check_footer_guest_pl")]
        CheckFooterGuestPl,
        [DataView("check_footer_guest_anonymous")]
        CheckFooterGuestAnonymous,
        #endregion
        #region Отчеты
        [DataView("reports_feedback")]
        ReportsFeedback,
        [DataView("reports_operations_total")]
        ReportsOperationsTotal,
        [DataView("reports_network_bonuses_balance")]
        ReportsNetworkBonusesBalance,
        [DataView("reports_waiters_rating")]
        ReportsWaitersRating,
        [DataView("reports_guests_activity")]
        ReportsGuestsActivity,
        [DataView("reports_cohort_analysis")]
        ReportsCohortAnalysis,
        [DataView("reports_ab_analysis")]
        ReportsAbAnalysis,
        [DataView("reports_guests_privileges")]
        ReportsGuestsPrivileges,
        [DataView("reports_restaurants_efficiency")]
        ReportsRestaurantsEfficiency,
        [DataView("reports_stock_efficiency")]
        ReportsStockEfficiency,
        [DataView("reports_rfm_common_values")]
        ReportsRfmCommonValues,
        [DataView("reports_rfm_guests")]
        ReportsRfmGuests,
        [DataView("dashboard_operational_sales")]
        DashboardOperationalSales,
        [DataView("dashboard_loyalty")]
        DashboardLoyalty,
        [DataView("dashboard_preorder")]
        DashboardPreorder,
        [DataView("dashboard_wallet")]
        DashboardWallet,
        [DataView("reports_operation_log")]
        ReportsOperationLog,
        [DataView("reports_operation_tips")]
        ReportsOperationTips,
        [DataView("reports_communications_efficiency")]
        ReportsCommunicationsEfficiency,
        [DataView("reports_message_sending_log")]
        ReportsMessageSendingLog,
        [DataView("reports_antifraud_restaurants")]
        ReportsAntifraudRestaurants,
        [DataView("reports_antifraud_waiters")]
        ReportsAntifraudWaiters,
        [DataView("reports_dangerous_operations")]
        ReportsDangerousOperations,
        [DataView("notification_dangerous_operations")]
        NotificationDangerousOperations,
        [DataView("notification_feedback")]
        NotificationFeedback,
        #endregion
        #region Антифрод
        [DataView("antifraud_settings")]
        AntifraudSettings,
        [DataView("antifraud_grey_list")]
        AntifraudGreyList,
        [DataView("antifraud_white_list")]
        AntifraudWhiteList,
        [DataView("antifraud_black_list")]
        AntifraudBlackList,
        [DataView("antifraud_block_list")]
        AntifraudBlockList,
        #endregion
        #region Настройки программы лояльности
        [DataView("settings_pl_dishes_coefficients")]
        SettingsPlDishesCoefficients,
        #endregion
        #region Акции
        [DataView("stock")]
        Stock,
        [DataView("stock_promo_bonuses")]
        StockPromoBonuses,
        [DataView("stock_corporate_catering")]
        StockCorporateCatering,
        [DataView("stock_basic")]
        StockBasic,
        [DataView("stock_happy_hour")]
        StockHappyHour,
        [DataView("stock_birthday")]
        StockBirthday,
        [DataView("stock_registration_bonus")]
        StockRegistrationBonus,
        [DataView("stock_stamps")]
        StockStamps,
        [DataView("stock_n_dish_present")]
        StockNDishPresent,
        [DataView("stock_bring_friend")]
        StockBringFriend,
        [DataView("stock_special_price_combo")]
        StockSpecialPriceCombo,
        [DataView("stock_combo_discount")]
        StockComboDiscount,
        [DataView("stock_season_pass")]
        StockSeasonPass,
        [DataView("stock_information_campaign")]
        StockInformationCampaign,
        [DataView("stock_basic_promo_code")]
        StockBasicPromoCode,
        #endregion
        
        #region Типы привилегий в акциях (Базовая акция, Счастливый час, День рождения, Бонус за регистрацию)
        [DataView("stock_check_discount_сurrency")]
        StockCheckDiscountCurrency,
        [DataView("stock_check_discount_percent")]
        StockCheckDiscountPercent,
        [DataView("stock_dish_discount_сurrency")]
        StockDishDiscountCurrency,
        [DataView("stock_dish_discount_percent")]
        StockDishDiscountPercent,
        [DataView("stock_dish_present")]
        StockDishPresent,
        [DataView("stock_dish_special_price")]
        StockDishSpecialPrice,
        [DataView("stock_bonus_points")]
        StockBonusPoints,
        [DataView("stock_check_bonus_points")]
        StockCheckBonusPoints,
        [DataView("stock_dish_bonus_points")]
        StockDishBonusPoints,
        [DataView("stock_rank_bonus_points")]
        StockRankBonusPoints,
        #endregion

        #region dynamic
        [DataView("stock_promo_bonuses_activation")]
        StockPromoBonusesActivation,
        [DataView("stock_corporate_catering_activation")]
        StockCorporateCateringActivation,
        [DataView("stock_basic_activation")]
        StockBasicActivation,
        [DataView("stock_happy_hour_activation")]
        StockHappyHourActivation,
        [DataView("stock_birthday_activation")]
        StockBirthdayActivation,
        [DataView("stock_registration_bonus_activation")]
        StockRegistrationBonusActivation,
        [DataView("stock_stamps_activation")]
        StockStampsActivation,
        [DataView("stock_n_dish_present_activation")]
        StockNDishPresentActivation,
        [DataView("stock_bring_friend_activation")]
        StockBringFriendActivation,
        [DataView("stock_special_price_combo_activation")]
        StockSpecialPriceComboActivation,
        [DataView("stock_combo_discount_activation")]
        StockComboDiscountActivation,
        [DataView("stock_season_pass_activation")]
        StockSeasonPassActivation,
        [DataView("stock_information_campaign_activation")]
        StockInformationCampaignActivation,
        #endregion
        
        [DataView("dashboard_trends")]
        DashboardTrends
}