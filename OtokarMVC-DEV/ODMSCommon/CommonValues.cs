namespace ODMSCommon
{
    public class CommonValues
    {
        public const string UserInfoSessionKey = "UserInfo";
        public const string TechnicionCookieKey = "TechOperationInfo";
        public const string UserPermissionsSessionKey = "UserPermissions";
        public const string UserMenuSessionKey = "UserMenu";
        public const string OtokarDatabase = "OtokarDMS_DB";
        public const string StartUpPageAfterLogin = "~/tester.aspx";
        public const string DefaultUndefinedResourceValue = "!undefined";
        //public const string DefaultControllerName = "Role";
        //public const string DefaultActionName = "RoleIndex";
        public const string DefaultSystemRoleControllerName = "ActiveAnnouncements";
        public const string DefaultSystemRoleActionName = "Index";
        public const string DefaultNonSystemRoleControllerName = "TechnicianOperation";
        public const string DefaultNonSystemRoleActionName = "TechnicianOperationIndex";
        public const string StockSearchPartCodesKey = "StockSearchPartCodes";
        public const string ExcelExtOld = ".xls";
        public const string ExcelExtNew = ".xlsx";
        public const string PhotoExtJPEG = ".jpeg";
        public const string PhotoExtJPG = ".jpg";
        public const string PhotoExtPNG = ".png";
        public const string PhotoExtGIF = ".gif";
        public const string PhotoExtTIF = ".tif";
        public const string ListPriceLabel = "L";
        public const string DealerPriceLabel = "D";
        public const string ClaimPriceLabel = "C";
        public const string ErrorLog = "ErrorLog";
        public const string ProcessReport = "İşlem Raporu";
        public const string ExcelContentType = "application/vnd.ms-excel";
        public const string EmptySpace = " ";
        public const string Minus = "-";
        public const string MinusWithSpace = " - ";
        public const string OpenParenthesis = " ( ";
        public const string CloseParenthesis = " ) ";
        public const string DownloadFileFormatCookieKey = "DownloadFileFormat";
        public const string DownloadFileIdCookieKey = "DownloadFileId";
        public const int WorkOrderStatusInvoiceFinishId = 1;//TODO:this must be change 
        public const string TableStart = "<table border=1>";
        public const string TableBodyStart = "<tbody>";
        public const string TableBodyEnd = "</body>";
        public const string TableEnd = "</table>";
        public const string NewLine = "</br>";
        public const string HeaderStart = "<th>";
        public const string HeaderEnd = "</th>";
        public const string RowStart = "<tr>";
        public const string RowEnd = "</tr>";
        public const string ColumnStart = "<td>";
        public const string ColumnEnd = "</td>";
        public const string Pipe = "||";
        public const string Slash = "/";
        public const string Dot = ".";
        public const string Comma = ",";

        #region Multi Language UserControl için değerler

        public static string TRG_CONFIRM_FROM_DEFAULT = "TRGDEFAULTCONFIRM";
        public static string TRG_PROMPT_FROM_DEFAULT = "TRGDEFAULTPROMPT";
        public static string Alert = "alert";
        public static string Confirmation = "confirmation";
        public static string Prompt = "prompt";
        public static string Notify = "notify";
        public static string Print = "print";

        public const string ConstantMissingMapperColumnName = "CREATE_DATE";

        #endregion Multi Language UserControl için değerler

        public enum PurchaseOrderGroupType
        {
             Dealer = 7,
             OtokarService = 8,
             CentralService = 11,
             VehicleSaleService = 12
        }
        public enum WorkOrderPickingStatus
        {
            NewRecord = 0,
            Started = 1,
            Completed = 2,
            Cancelled = 9
        }
        public enum PDIVehicleResultStatus
        {
            NewRecord = 0,
            WaitingForApproval = 1,
            Approved = 2,
            Cancelled = 3
        }
        public enum LabourTechnicianStatus
        {
            Waiting = 0,
            NotStarted = 1,
            Cancelled = -1,
            Continued = 2,
            Paused = 3,
            Completed = 4
        }
        public enum CustomerSparePartDiscount
        {
            ServiceDealer = 1, Customer = 2
        }
        public enum FirmAction
        {
            NewRecord = 0,
            Dismantled = 1,
            Service = 2,
            SendToSupplier = 3,
            Lost = 9
        }
        public enum WorkOrderCardVehicleLeaveResult
        {
            Success = 0,
            ProcessTypeNotSet = 1,
            DismentledPartsNotSet = 2,
            IncompletedLaboursExists = 3,
            NotPickedPartsExists = 4,
            PdiResultsNotSet = 5,
            PickingIsNotFinished = 6,
            EmptyWorkOrderDeatail = 7,
            EmptyTechDesc = 8,
            NoDetailExists = 9,
            WaitingPreApproval = 10,
            NullKm = 11,
            RequiredPartForGuarantee= 12,
            CouponMaintenanceKmControl = 13
        }
        public enum ProposalCardVehicleLeaveResult
        {
            Success = 0,
            ProcessTypeNotSet = 1,
            DismentledPartsNotSet = 2,
            IncompletedLaboursExists = 3,
            NotPickedPartsExists = 4,
            PdiResultsNotSet = 5,
            PickingIsNotFinished = 6,
            EmptyProposalDeatail = 7,
            EmptyTechDesc = 8,
            NoDetailExists = 9,
            WaitingPreApproval = 10
        }
        public enum MessageSeverity
        {
            Success = 0,
            Fail = 1
        }

        public enum BalanceType
        {
            Balance = 1,
            NoBalance = 2
        }

        public enum ObjectSearchType
        {
            Customer = 1,
            Vehicle = 2,
            Appointment = 3,
            Fleet = 4,
            PurchaseOrder = 5,
            AppointmentIndicatorSubCategory = 6
        }

        public enum PurchaseOrderDetailStatus
        {
            Open = 0,
            Closed = 1,
            Cancelled = 2,
            NewRecord = -1
        }

        public class GeneralParameters
        {
            public const string WarrantyPreApprovalMinAmountId = "WARRANTY_PRE_APPROVAL_MIN_AMOUNT";
            public const string StockType = "STOCK_TYPE";
            public const string OtokarCustomerId = "OTOKAR_CUSTOMER_ID";
            public const string CampaignStockType = "CAMPAIGN_STOCK_TYPE";
            public const string CampaignPurchaseOrderType = "CAMPAIGN_PO_TYPE";
            public const string ScrapReasonIsDescRequired = "SCRAP_REASON_OTHER_DESC_REQUIRED";
            public const string MaxSplitIteration = "MAX_SPLIT_ITERATION";
            public const string FreePoType = "FREE_PO_PO_TYPE";
            public const string OtokarUrgentPOMailList = "OTOKAR_URGENT_PO_MAIL_LIST";
            public const string OtokarRedirectedUrgentPOMailList = "OTOKAR_REDIRECTED_URGENT_PO_MAIL_LIST";
            public const string AllowStockPickQuantityEdit = "ALLOW_STOCK_PICK_QUANTITY_EDIT";
            public const string CaptchaDisplay = "MAX_RECOVERY_SCREEN_TRY_COUNT_BY_CAPTHCA";
            public const string AutoRecoveryBlockMinute = "AUTO_RECOVERY_SCREEN_BLOCK_MINUTE";
            public const string AutoRecoveryBlockCount = "AUTO_RECOVERY_SCREEN_BLOCK_COUNT";
            public const string CatalogLink = "CATALOG_LINK";
            public const string CatalogAdminLink = "CATALOG_ADMIN_LINK";
            public const string DealerRegionAbroad = "DEALER_REGION_ABROAD";
            public const string AppointmentInterval = "APPOINTMENT_INTERVAL";

            /// <summary>
            /// Gif Onaya gönderilen kayıtlarda hata olduğunda mail atılacaklar listesi
            /// </summary>
            public const string GosFailEmailList = "GOS_FAIL_MAIL_LIST";
        }


        public class LookupKeys
        {
            public const string StatusLookup = "MSG_STATUS";
            public const string SexLookup = "USER_SEX";
            public const string MaritalStatusLookup = "MARITAL_STAT";
            public const string Unit = "UNIT";
            public const string YesNo = "YESNO";
            public const string CustomerGroupLookup = "CUST_GROUP";
            public const string DealerClassLookup = "DEALER_CLASS";
            public const string CustomerTypeLookup = "CUST_TYPE";
            public const string GoverntmentTypeLookup = "GOVERN_TYPE";
            public const string CompanyTypeLookup = "COMPANY_TYPE";
            public const string CustContTypeLookup = "CUST_CONTTYPE";
            public const string AddressTypeLookup = "ADDRESS_TYPE";
            public const string MaintenanceTypeLookup = "MAINTENANCE_TYPE";
            public const string SupplyTypeLookup = "SUPPLY_TYPE";
            public const string StockLocationLookup = "STOCK_LOCATION";
            public const string CampaignRequestStatusLookup = "CAMPAIGN_REQ_STATUS";
            public const string CampaignRequestSupplyTypeLookup = "CAMP_REQ_SUPP_TYPE";
            public const string SaleStatusLookup = "PARTSALE_STATUS";
            public const string SaleStatusDetailLookup = "SP_SALE_DET_STATUS";
            public const string PurchaseOrderDetailStatusLookup = "PO_DET_STATUS";
            public const string FixAssetStatus = "FIX_ASSET_STATUS";
            public const string FleetRequestStatus = "FLEET_REQUEST_STATUS";
            public const string LabourTechnicianStatus = "LABOUR_TECH_STATUS";
            public const string StorageType = "WAREHOUSE_TYPE";
            public const string PurchaseOrderStatus = "PO_STATUS";
            public const string SupplyPortLookup = "SUPPLY_PORT";
            public const string CycleCountLookupStatus = "CYCLE_COUNT_STATUS";
            public const string CycleCountDiffStatus = "STOCK_DIFF_STATUS";
            public const string DeliveryStatus = "DELIVERY_STATUS";
            public const string WarrantyStatus = "WARRANTY_STATUS";
            public const string ClaimPartAction = "CLAIM_PART_ACTION";
            public const string VehicleResultStatus = "VEHC_RESULT_STATUS";
            public const string GIFCategory = "GIF_CATEGORY";
            public const string TenderSaleStatus = "TENDER_SALE_STATUS";
            public const string ScrapReason = "SCRAP_REASON";
            public const string TransactionType = "TRANSACTION_TYPE";
            public const string StockBlockStatusLookup = "STOCK_BLOCK_STATUS";
            public const string ZDMSTeklifRaporu = "ZDMS_TEKLIF_RAPORU";
            public const string AppointmentStatusLookup = "APPOINTMENT_STATUS";
            public const string PartSaleOrgTypeLookup = "PARTSALE_ORGTYPE";
            public const string SaleOrderStatus = "SALEORDERMST_STATUS";
            public const string SaleOrderDetailStatus = "SALEORDERDET_STATUS";
            public const string SaleTypeLookup = "SALE_TYPE";
            public const string PickSource = "PICK_SOURCE";
            public const string InvoiceType = "INVOICE_TYPE";
            public const string POLocation = "PO_LOCATION";
            public const string AppointmentInterval = "APPOINTMENT_INTERVAL";
        }

        public class PermissionCodes
        {
            public const string PermissionCodeTest = "Placesholder";

            public class DealerGuaranteeControlList
            {
                public const string Index = "PERM_DEALER_GUARANTEE_CONTROL_LIST_INDEX";
            }
            public class FavoriteScreen
            {
                public const string FavoriteScreenIndex = "PERM_FAVORITE_SCREEN_INDEX";
                public const string FavoriteScreenSave = "PERM_FAVORITE_SCREEN_SAVE";
            }
            public class PartWithModelPairing
            {
                public const string PartWithModelPairingIndex = "PERM_PART_WITH_MODEL_PAIRING_INDEX";
                public const string PartWithModelPairingCreate = "PERM_PART_WITH_MODEL_PAIRING_CREATE";
                public const string PartWithModelPairingExcel = "PERM_PART_WITH_MODEL_PAIRING_EXCEL";
            }
            public class WebServiceLogs
            {
                public const string InvoiceList = "PERM_INVOICE_SERVICE_LOGS_INDEX";
            }
            public class Reports
            {
                public const string WorkOrderPerformanceReportIndex = "PERM_WORK_ORDER_PERFORMANCE_REPORT_INDEX";
                public const string WorkOrderPerformanceExcelExport = "PERM_WORK_ORDER_PERFORMANCE_REPORT_EXCEL_EXPORT";

                public const string GuaranteeReportIndex = "PERM_GUARANTEE_REPORT_INDEX";
                public const string GuaranteeExcelExport = "PERM_GUARANTEE_REPORT_EXCEL_EXPORT";

                public const string WorkOrderDetailReportIndex = "PERM_WORK_ORDER_DETAIL_REPORT_INDEX";
                public const string WorkOrderDetailExcelExport = "PERM_WORK_ORDER_DETAIL_REPORT_EXCEL_EXPORT";

                public const string WorkOrderMaintReportIndex = "PERM_WORK_ORDER_MAINT_REPORT_INDEX";
                public const string WorkOrderMaintExcelExport = "PERM_WORK_ORDER_MAINT_REPORT_EXCEL_EXPORT";

                public const string PurchaseOrderReportIndex = "PERM_PURCHASE_ORDER_REPORT_INDEX";
                public const string PurchaseOrderExcelExport = "PERM_PURCHASE_ORDER_REPORT_EXCEL_EXPORT";

                public const string PartExchangeReportIndex = "PERM_PART_EXCHANGE_REPORT_INDEX";
                public const string PartExchangeExcelExport = "PERM_PART_EXCHANGE_REPORT_EXCEL_EXPORT";

                public const string CycleCountResultReportIndex = "PERM_CYCLE_COUNT_RESULT_REPORT_INDEX";
                public const string CycleCountResultExcelExport = "PERM_CYCLE_COUNT_RESULT_REPORT_EXCEL_EXPORT";

                public const string WorkOrderPartHistoryReportIndex = "PERM_WO_PART_HISTORY_INDEX";
                public const string WorkOrderPartHistoryExcelExport = "PERM_WO_PART_HISTORY_EXCEL_EXPORT";

                public const string SentPartUsageReportIndex = "PERM_SENT_PART_USAGE_REPORT_INDEX";
                public const string SentPartUsageExcelExport = "PERM_SENT_PART_USAGE_REPORT_EXCEL_EXPORT";

                public const string CarServiceDurationReportIndex = "PERM_CAR_SERVICE_DURATION_REPORT_INDEX";
                public const string CarServiceDurationExcelExport = "PERM_CAR_SERVICE_DURATION_REPORT_EXCEL_EXPORT";

                public const string LaborCostPerVehicleReportIndex = "PERM_LABOR_COST_PER_VEHICLE_INDEX";
                public const string LaborCostPerVehicleExcelExport = "PERM_LABOR_COST_PER_VEHICLE_EXCEL_EXPORT";

                public const string KilometerDistributionReportIndex = "PERM_KILOMETER_DISTRIBUTION_REPORT_INDEX";
                public const string KilometerDistributionExcelExport = "PERM_KILOMETER_DISTRIBUTION_REPORT_EXCEL_EXPORT";

                public const string CampaignSummaryReportIndex = "PERM_COMPAIGN_SUMMARY_REPORT_INDEX";
                public const string CampaignSummaryExcelExport = "PERM_COMPAIGN_SUMMARY_REPORT_EXCEL_EXPORT";

                public const string PartStockReportIndex = "PERM_PART_STOCK_REPORT_INDEX";
                public const string PartStockExcelExport = "PERM_PART_STOCK_REPORT_EXCEL_EXPORT";

                public const string ChargePerCarReportIndex = "PERM_PART_CHARGE_PER_CAR_REPORT_INDEX";
                public const string ChargePerCarExcelExport = "PERM_PART_CHARGE_PER_CAR_REPORT_EXCEL_EXPORT";

                public const string PartStockActivityReportIndex = "PART_STOCK_ACTIVITY_REPORT_INDEX";
                public const string PartStockActivityExcelExport = "PART_STOCK_ACTIVITY_REPORT_EXCEL_EXPORT";

                public const string SparePartControlReportIndex = "SPARE_PART_CONTROL_REPORT_INDEX";
                public const string SparePartControlExcelExport = "SPARE_PART_CONTROL_REPORT_EXCEL_EXPORT";

                public const string WorkOrderProcessTypesTotalReportIndex = "WORKORDER_PROCESS_TYPE_TOTAL_REPORT_INDEX";
                public const string WorkOrderProcessTypesTotalExcelExport = "WORKORDER_PROCESS_TYPE_TOTAL_REPORT_EXCEL_EXPORT";

                public const string FixAssetInventoryReportIndex = "FIX_ASSET_INVENTORY_REPORT_INDEX";
                public const string FixAssetInventoryExcelExport = "FIX_ASSET_INVENTORY_REPORT_EXCEL_EXPORT";

                public const string SaleReportIndex = "SALE_REPORT_INDEX";
                public const string SaleExcelExport = "SALE_REPORT_EXCEL_EXPORT";

                public const string WorkOrderPartsTotalReportIndex = "WORKORDER_PARTS_TOTAL_REPORT_INDEX";
                public const string WorkOrderPartsTotalExcelExport = "WORKORDER_PARTS_TOTAL_REPORT_EXCEL_EXPORT";

                public const string InvoiceInfoReportIndex = "PERM_INVOICE_INFO_REPORT_INDEX";
                public const string InvoiceInfoExcelExport = "PERM_INVOICE_INFO_REPORT_EXCEL_EXPORT";

                public const string PersonnelInfoReportIndex = "PERM_PERSONNEL_INFO_REPORT_INDEX";
                public const string PersonnelInfoExcelExport = "PERM_PERSONNEL_INFO_REPORT_EXCEL_EXPORT";

                public const string SshReportIndex = "PERM_SSH_REPORT_INDEX";
                public const string SshExcelExport = "PERM_SSH_EXCEL_EXPORT";

                public const string FleetPeriodReportIndex = "PERM_FLEET_PERIOD_REPORT_INDEX";
                public const string FleetPeriodReportExcelExport = "PERM_FLEET_PERIOD_REPORT_EXCEL_EXPORT";

                public const string AlternatePartReportIndex = "PERM_ALTERNATE_PART_REPORT_INDEX";
                public const string AlternatePartExcelExport = "PERM_ALTERNATE_PART_REPORT_EXCEL_EXPORT";
            }

            public class DealerPurchaseOrderPartConfirm
            {
                public const string DealerPurchaseOrderPartConfirmIndex = "PERM_DEALER_PURCHASE_ORDER_PART_CONFIRM_INDEX";
                public const string DealerPurchaseOrderPartConfirmSave = "PERM_DEALER_PURCHASE_ORDER_PART_CONFIRM_SAVE";
            }
            public class Title
            {
                public const string TitleIndex = "PERM_TITLE_INDEX";
                public const string TitleList = "PERM_TITLE_INDEX_LIST";
                public const string TitleCreate = "PERM_TITLE_CREATE";
                public const string TitleUpdate = "PERM_TITLE_UPDATE";
                public const string TitleDelete = "PERM_TITLE_DELETE";
            }
            public class Role
            {
                public const string RoleTypeIndex = "PERM_ROLE_INDEX";
                public const string RoleTypeCreate = "PERM_ROLE_CREATE";
                public const string RoleTypeUpdate = "PERM_ROLE_UPDATE";
                public const string RoleTypeDelete = "PERM_ROLE_DELETE";
                public const string RoleTypeDetails = "PERM_ROLE_DETAILS";
            }
            public class SaleOrder
            {
                public const string SaleOrderRemainingIndex = "PERM_SALE_ORDER_REMAINING_INDEX";
            }
            public class GuaranteeRequestApprove
            {
                public const string GuaranteeRequestApproveIndex = "PERM_GUARANTEE_REQUEST_APPROVE_INDEX";
            }
            public class SparePartSaleOrderDetail
            {
                public const string SparePartSaleOrderDetailIndex = "PERM_SPARE_PART_SALE_ORDER_DETAIL_INDEX";
                public const string SparePartSaleOrderDetailCreate = "PERM_SPARE_PART_SALE_ORDER_DETAIL_CREATE";
                public const string SparePartSaleOrderDetailUpdate = "PERM_SPARE_PART_SALE_ORDER_DETAIL_UPDATE";
                public const string SparePartSaleOrderDetailDelete = "PERM_SPARE_PART_SALE_ORDER_DETAIL_DELETE";
                public const string SparePartSaleOrderDetailDetails = "PERM_SPARE_PART_SALE_ORDER_DETAIL_DETAILS";
            }

            public class GuaranteeRequestApproveDetail
            {
                public const string GuaranteeRequestApproveDetailIndex = "PERM_GUARANTEE_REQUEST_APPROVE_DETAIL_INDEX";
                public const string GuaranteeRequestApproveDetailAction = "PERM_GUARANTEE_REQUEST_APPROVE_DETAIL_ACTION";
                public const string GuaranteeRequestApproveDetailReject = "PERM_GUARANTEE_REQUEST_APPROVE_DETAIL_REJECT";
            }

            public class Delivery
            {
                public const string DeliveryIndex = "PERM_DELIVERY_INDEX";
                public const string DeliveryCreate = "PERM_DELIVERY_CREATE";
                public const string DeliveryUpdate = "PERM_DELIVERY_UPDATE";
                public const string DeliveryDelete = "PERM_DELIVERY_DELETE";
                public const string DeliveryDetails = "PERM_DELIVERY_DETAILS";
            }



            public class RolePermission
            {
                public const string RolePermissionIndex = "PERM_ROLE_PERMISSION_INDEX";
                public const string RolePermissionSave = "PERM_ROLE_PERMISSION_SAVE";

            }

            public class DealerTechnicianGroupTechnician
            {
                public const string DealerTechnicianGroupTechnicianIndex = "PERM_DEALER_TECHNICIAN_MATCH_INDEX";
                public const string DealerTechnicianGroupTechnicianSave = "PERM_DEALER_TECHNICIAN_MATCH_SAVE";

            }
            public class OtokarStockSearh
            {
                public const string OtokarStockSeachIndex = "PERM_OTOKAR_STOCK_SEARCH_INDEX";
            }
            public class OtokarSparePartSaleInvoiceSearch
            {
                public const string OtokarSparePartSaleInvoiceSearchIndex = "PERM_OTOKAR_INVOICE_LIST_INDEX";
            }
            public class ActiveAnnouncements
            {
                public const string ActiveAnnouncementsIndex = "PERM_ACTIVE_ANNOUNCEMENTS_INDEX";
            }

            public class ActiveDealerChoice
            {
                public const string ActiveDealerChoiceIndex = "PERM_ACTIVE_DEALER_CHOICE_INDEX";
            }

            public class FleetSearch
            {
                public const string FleetSearchIndex = "PERM_FLEET_SEARCH_INDEX";
            }


            public class AnnouncementDealer
            {
                public const string AnnouncementDealerIndex = "PERM_ANNOUNCEMENT_DEALER_INDEX";
                public const string AnnouncementDealerSave = "PERM_ANNOUNCEMENT_DEALER_SAVE";

            }
            public class PdiGifApproveGroupMembers
            {
                public const string PdiGifApproveGroupMembersIndex = "PERM_PDI_GOS_APPROVE_GROUP_MEMBERS_INDEX";
                public const string PdiGifApproveGroupMembersSave = "PERM_PDI_GOS_APPROVE_GROUP_MEMBERS_SAVE";

            }
            public class PDIInvoiceList
            {
                public const string PDIInvoiceListIndex = "PERM_PDI_INVOICE_LIST_INDEX";
            }

            public class PDIVehicleResult
            {
                public const string PDIVehicleResultIndex = "PERM_PDI_VEHICLE_RESULT_INDEX";
                public const string PDIVehicleResultDetails = "PERM_PDI_VEHICLE_RESULT_DETAILS";
                public const string PDIVehicleResultConfirmIndex = "PERM_PDI_VEHICLE_RESULT_CONFIRM_INDEX";
                public const string PDIVehicleResultConfirmDetails = "PERM_PDI_VEHICLE_RESULT_CONFIRM_DETAILS";
                public const string PDIVehicleResultConfirmDetailsApprove = "PERM_PDI_VEHICLE_RESULT_CONFIRM_DETAILS_APPROVE";
                public const string PDIVehicleResultConfirmDetailsCancel = "PERM_PDI_VEHICLE_RESULT_CONFIRM_DETAILS_CANCEL";
            }
            public class GuaranteeAuthorityGroupMembers
            {
                public const string GuaranteeAuthorityGroupMembersIndex = "PERM_GUARANTEE_AUTHORITY_GROUP_MEMBERS_INDEX";
                public const string GuaranteeAuthorityGroupMembersSave = "PERM_GUARANTEE_AUTHORITY_GROUP_MEMBERS_SAVE";

            }
            public class PDIGOSApproveGroupVehicleModels
            {
                public const string PDIGOSApproveGroupVehicleModelsIndex = "PERM_PDI_GOS_APPROVE_GROUP_VEHICLE_MODELS_INDEX";
                public const string PDIGOSApproveGroupVehicleModelsSave = "PERM_PDI_GOS_APPROVE_GROUP_VEHICLE_MODELS_SAVE";

            }
            public class GuaranteeAuthorityGroupIndicatorTransTypes
            {
                public const string GuaranteeAuthorityGroupIndicatorTransTypesIndex = "PERM_GUARANTEE_AUTH_GRP_IND_TRS_TYP_INDEX";
                public const string GuaranteeAuthorityGroupIndicatorTransTypesSave = "PERM_GUARANTEE_AUTH_GRP_IND_TRS_TYP_SAVE";

            }
            public class PDIGOSApproveGroupIndicatorTransTypes
            {
                public const string PDIGOSApproveGroupIndicatorTransTypesIndex = "PERM_PDI_GOS_APPROVE_GROUP_IND_TRS_TYP_INDEX";
                public const string PDIGOSApproveGroupIndicatorTransTypesSave = "PERM_PDI_GOS_APPROVE_GROUP_IND_TRS_TYP_SAVE";

            }
            public class VatRatio
            {
                public const string VatRatioIndex = "PERM_VAT_RATIO_INDEX";
                public const string VatRatioCreate = "PERM_VAT_RATIO_CREATE";
                public const string VatRatioDelete = "PERM_VAT_RATIO_DELETE";
            }


            public class Appointment
            {
                public const string AppointmentIndex = "PERM_APPOINTMENT_INDEX";
                public const string AppointmentCreate = "PERM_APPOINTMENT_CREATE";
                public const string AppointmentUpdate = "PERM_APPOINTMENT_UPDATE";
                public const string AppointmentDetails = "PERM_APPOINTMENT_DETAILS";
                public const string AppointmentDelete = "PERM_APPOINTMENT_DELETE";
            }

            public class CustomerSparePartDiscount
            {
                public const string CustomerSparePartDiscountIndex = "PERM_CRM_CUSTOMER_SPAREPART_DISCOUNT_INDEX";
                public const string CustomerSparePartDiscountCreate = "PERM_CRM_CUSTOMER_SPAREPART_DISCOUNT_CREATE";
                public const string CustomerSparePartDiscountUpdate = "PERM_CRM_CUSTOMER_SPAREPART_DISCOUNT_UPDATE";
                public const string CustomerSparePartDiscountDetails = "PERM_CRM_CUSTOMER_SPAREPART_DISCOUNT_DETAILS";
                public const string CustomerSparePartDiscountDelete = "PERM_CRM_CUSTOMER_SPAREPART_DISCOUNT_DELETE";
            }

            public class GuaranteeAuthorityGroup
            {
                public const string GuaranteeAuthorityGroupIndex = "PERM_GUARATEE_AUTHORITY_GROUP_INDEX";
                public const string GuaranteeAuthorityGroupCreate = "PERM_GUARATEE_AUTHORITY_GROUP_CREATE";
                public const string GuaranteeAuthorityGroupUpdate = "PERM_GUARATEE_AUTHORITY_GROUP_UPDATE";
                public const string GuaranteeAuthorityGroupDetails = "PERM_GUARATEE_AUTHORITY_GROUP_DETAILS";
                public const string GuaranteeAuthorityGroupDelete = "PERM_GUARATEE_AUTHORITY_GROUP_DELETE";
            }

            public class PdiGifApproveGroup
            {
                public const string PdiGifApproveGroupIndex = "PERM_PDI_GIF_APPROVE_GROUP_INDEX";
                public const string PdiGifApproveGroupCreate = "PERM_PDI_GIF_APPROVE_GROUP_CREATE";
                public const string PdiGifApproveGroupUpdate = "PERM_PDI_GIF_APPROVE_GROUP_UPDATE";
                public const string PdiGifApproveGroupDetails = "PERM_PDI_GIF_APPROVE_GROUP_DETAILS";
                public const string PdiGifApproveGroupDelete = "PERM_PDI_GIF_APPROVE_GROUP_DELETE";
            }

            public class GuaranteeAuthorityLimitations
            {
                public const string GuaranteeAuthorityLimitationsIndex = "PERM_GUARATEE_AUTHORITY_LIMITATIONS_INDEX";
                public const string GuaranteeAuthorityLimitationsCreate = "PERM_GUARATEE_AUTHORITY_LIMITATIONS_CREATE";
                public const string GuaranteeAuthorityLimitationsUpdate = "PERM_GUARATEE_AUTHORITY_LIMITATIONS_UPDATE";
                public const string GuaranteeAuthorityLimitationsDelete = "PERM_GUARATEE_AUTHORITY_LIMITATIONS_DELETE";
            }



            public class AppointmentDetail
            {
                public const string AppointmentDetailIndex = "PERM_APPOINTMENT_DETAILS_INDEX";
                public const string AppointmentDetailCreate = "PERM_APPOINTMENT_DETAILS_CREATE";
                public const string AppointmentDetailUpdate = "PERM_APPOINTMENT_DETAILS_UPDATE";
                public const string AppointmentDetailDetails = "PERM_APPOINTMENT_DETAILS_DETAILS";
                public const string AppointmentDetailDelete = "PERM_APPOINTMENT_DETAILS_DELETE";
            }

            public class Bank
            {
                public const string BankIndex = "PERM_BANK_INDEX";
                public const string BankCreate = "PERM_BANK_CREATE";
                public const string BankUpdate = "PERM_BANK_UPDATE";
                public const string BankDelete = "PERM_BANK_DELETE";
                public const string BankDetails = "PERM_BANK_DETAILS";
            }
            public class WorkOrderCard
            {
                public const string WorkOrderCardIndex = "PERM_WORK_ORDER_CARD_INDEX";
                public const string WorkOrderCardUpdate = "PERM_WORK_ORDER_CARD_UPDATE";
                public const string CancelVehicleLeave = "PERM_WO_CANCEL_VEHICLE_LEAVE";
                public const string WorkOrderCardDeleteDetailItem = "PERM_WO_DELETE_DETAIL_ITEM";
                public const string WorkOrderCardCancel = "PERM_WORK_ORDER_CARD_CANCEL";
                public const string WorkOrderCardAddDetailItem = "PERM_WO_ADD_DETAIL_ITEM";
            }

            public class FleetRequestVehicleApprove
            {
                public const string FleetRequestVehicleApproveIndex = "PERM_FLEET_REQUEST_APPROVE_INDEX";
                public const string FleetRequestVehicleApproveUpdate = "PERM_FLEET_REQUEST_APPROVE_UPDATE";
            }

            public class PrivateMessage
            {
                public const string PrivateMessageIndex = "PERM_PRIVATE_MESSAGE_INDEX";
                public const string PrivateMessageSend = "PERM_PRIVATE_MESSAGE_SEND";
            }

            public class PriceListCountryMatch
            {
                public const string PriceListCountryMatchIndex = "PERM_PRICE_LIST_COUNTRY_MATCH_INDEX";
                public const string PriceListCountryMatchUpdate = "PERM_PRICE_LIST_COUNTRY_MATCH_SAVE";
            }

            public class Proficiency
            {
                public const string ProficiencyIndex = "PERM_PROFICIENCY_INDEX";
                public const string ProficiencyCreate = "PERM_PROFICIENCY_CREATE";
                public const string ProficiencyUpdate = "PERM_PROFICIENCY_UPDATE";
                public const string ProficiencyDelete = "PERM_PROFICIENCY_DELETE";
                public const string ProficiencyDetails = "PERM_PROFICIENCY_DETAILS";
            }

            public class Warehouse
            {
                public const string WarehouseIndex = "PERM_WAREHOUSE_INDEX";
                public const string WarehouseCreate = "PERM_WAREHOUSE_CREATE";
                public const string WarehouseUpdate = "PERM_WAREHOUSE_UPDATE";
                public const string WarehouseDelete = "PERM_WAREHOUSE_DELETE";
                public const string WarehouseDetails = "PERM_WAREHOUSE_DETAILS";
            }
            public class WorkOrder
            {
                public const string WorkOrderIndex = "PERM_WORK_ORDER_INDEX";
                public const string WorkOrderCreate = "PERM_WORK_ORDER_CREATE";
                public const string WorkOrderUpdate = "PERM_WORK_ORDER_UPDATE";
                public const string WorkOrderDelete = "PERM_WORK_ORDER_DELETE";
                public const string WorkOrderDetails = "PERM_WORK_ORDER_DETAILS";
                public const string WorkOrderExcelExport = "PERM_WORK_ORDER_EXCEL_EXPORT";
            }

            public class Workshop
            {
                public const string WorkshopIndex = "PERM_WORKSHOP_INDEX";
                public const string WorkshopCreate = "PERM_WORKSHOP_CREATE";
                public const string WorkshopUpdate = "PERM_WORKSHOP_UPDATE";
                public const string WorkshopDelete = "PERM_WORKSHOP_DELETE";
                public const string WorkshopDetails = "PERM_WORKSHOP_DETAILS";
            }

            public class WorkorderInvoicePayments
            {
                public const string WorkorderInvoicePaymentsIndex = "PERM_WORKODER_INVOICE_PAYMENTS_INDEX";
                public const string WorkorderInvoicePaymentsCreate = "PERM_WORKODER_INVOICE_PAYMENTS_CREATE";
                public const string WorkorderInvoicePaymentsUpdate = "PERM_WORKODER_INVOICE_PAYMENTS_UPDATE";
                public const string WorkorderInvoicePaymentsDelete = "PERM_WORKODER_INVOICE_PAYMENTS_DELETE";
                public const string WorkorderInvoicePaymentsDetails = "PERM_WORKODER_INVOICE_PAYMENTS_DETAILS";
            }
            public class WorkorderInvoice
            {
                public const string WorkOrderInvoiceIndex = "PERM_WORKODER_INVOICE_INDEX";
                public const string WorkOrderInvoiceCreate = "PERM_WORKODER_INVOICE_CREATE";
                public const string WorkOrderInvoiceUpdate = "PERM_WORKODER_INVOICE_UPDATE";
                public const string WorkOrderInvoiceDelete = "PERM_WORKODER_INVOICE_DELETE";
                public const string WorkOrderInvoiceDetails = "PERM_WORKODER_INVOICE_DETAILS";
            }
            public class Proposal
            {
                public const string ProposalIndex = "PERM_PROPOSAL_INDEX";
                public const string ProposalCreate = "PERM_PROPOSAL_CREATE";
                public const string ProposalUpdate = "PERM_PROPOSAL_UPDATE";
                public const string ProposalDelete = "PERM_PROPOSAL_DELETE";
                public const string ProposalDetails = "PERM_PROPOSAL_DETAILS";
                public const string ProposalExcelExport = "PERM_PROPOSAL_EXCEL_EXPORT";
            }
            public class ProposalCard
            {
                public const string ProposalCardIndex = "PERM_PROPOSAL_CARD_INDEX";
                public const string ProposalCardUpdate = "PERM_PROPOSAL_CARD_UPDATE";
                public const string CancelVehicleLeave = "PERM_PROPOSAL_CARD_CANCEL_VEHICLE_LEAVE";
                public const string ProposalCardCancel = "PERM_PROPOSAL_CARD_CANCEL_PROPOSAL";
            }
            public class WorkshopWorker
            {
                public const string WorkshopWorkerIndex = "PERM_WORKSHOP_WORKER_INDEX";
                public const string WorkshopWorkerCreate = "PERM_WORKSHOP_WORKER_CREATE";
                public const string WorkshopWorkerUpdate = "PERM_WORKSHOP_WORKER_UPDATE";
                public const string WorkshopWorkerDelete = "PERM_WORKSHOP_WORKER_DELETE";
                public const string WorkshopWorkerDetails = "PERM_WORKSHOP_WORKER_DETAILS";
            }

            public class Labour
            {
                public const string LabourIndex = "PERM_LABOUR_INDEX";
                public const string LabourCreate = "PERM_LABOUR_CREATE";
                public const string LabourUpdate = "PERM_LABOUR_UPDATE";
                public const string LabourDelete = "PERM_LABOUR_DELETE";
                public const string LabourDetails = "PERM_LABOUR_DETAILS";
                public const string LabourExcelExport = "PERM_LABOUR_EXCEL_EXPORT";
            }

            public class MaintenanceLabour
            {
                public const string MaintenanceLabourIndex = "PERM_MAINTLABOUR_INDEX";
                public const string MaintenanceLabourCreate = "PERM_MAINTLABOUR_CREATE";
                public const string MaintenanceLabourUpdate = "PERM_MAINTLABOUR_UPDATE";
                public const string MaintenanceLabourDelete = "PERM_MAINTLABOUR_DELETE";
                public const string MaintenanceLabourDetails = "PERM_MAINTLABOUR_DELETE";
                public const string MaintenanceLabourIndexExcelExport = "PERM_MAINTENANCE_LABOUR_INDEX_EXCEL_EXPORT";
            }

            public class LabourType
            {
                public const string LabourTypeIndex = "PERM_LABOURTYPE_INDEX";
                public const string LabourTypeCreate = "PERM_LABOURTYPE_CREATE";
                public const string LabourTypeUpdate = "PERM_LABOURTYPE_UPDATE";
                public const string LabourTypeDelete = "PERM_LABOURTYPE_DELETE";
                public const string LabourTypeDetails = "PERM_LABOURTYPE_DETAILS";
            }

            public class LabourDuration
            {
                public const string LabourDurationIndex = "PERM_LABOUR_DURATION_INDEX";
                public const string LabourDurationCreate = "PERM_LABOUR_DURATION_CREATE";
                public const string LabourDurationUpdate = "PERM_LABOUR_DURATION_UPDATE";
                public const string LabourDurationDelete = "PERM_LABOUR_DURATION_DELETE";
                public const string LabourDurationDetails = "PERM_LABOUR_DURATION_DETAILS";
            }

            public class LabourPrice
            {
                public const string LabourPriceIndex = "PERM_LABOURPRICE_INDEX";
                public const string LabourPriceCreate = "PERM_LABOURPRICE_CREATE";
                public const string LabourPriceUpdate = "PERM_LABOURPRICE_UPDATE";
                public const string LabourPriceDelete = "PERM_LABOURPRICE_DELETE";
                public const string LabourPriceDetails = "PERM_LABOURPRICE_DETAILS";
            }
            public class Supplier
            {
                public const string SupplierIndex = "PERM_SUPPLIER_INDEX";
                public const string SupplierCreate = "PERM_SUPPLIER_CREATE";
                public const string SupplierUpdate = "PERM_SUPPLIER_UPDATE";
                public const string SupplierDelete = "PERM_SUPPLIER_DELETE";
                public const string SupplierDetails = "PERM_SUPPLIER_DETAILS";
            }
            public class VehicleHistory
            {
                public const string VehicleHistoryIndex = "PERM_VEHICLEHISTORY_INDEX";
                public const string VehicleHistoryExcelExport = "PERM_VEHICLEHISTORY_EXCEL_EXPORT";
            }
            public class Vehicle
            {
                public const string VehicleIndex = "PERM_VEHICLE_INDEX";
                public const string VehicleCreate = "PERM_VEHICLE_CREATE";
                public const string VehicleUpdate = "PERM_VEHICLE_UPDATE";
                public const string VehicleDelete = "PERM_VEHICLE_DELETE";
                public const string VehicleDetails = "PERM_VEHICLE_DETAILS";
                public const string VehicleHistoryIndex = "PERM_VEHICLE_HISTORY_INDEX";
                public const string VehicleExcelExport = "PERM_VEHICLE_EXCEL_EXPORT";
            }
            public class VehicleGroup
            {
                public const string VehicleGroupIndex = "PERM_VEHICLE_GROUP_INDEX";
                public const string VehicleGroupUpdate = "PERM_VEHICLE_GROUP_UPDATE";
                public const string VehicleGroupDelete = "PERM_VEHICLE_GROUP_DELETE";
                public const string VehicleGroupDetails = "PERM_VEHICLE_GROUP_DETAILS";
            }
            public class VehicleModel
            {
                public const string VehicleModelIndex = "PERM_VEHICLE_MODEL_INDEX";
                public const string VehicleModelUpdate = "PERM_VEHICLE_MODEL_UPDATE";
                public const string VehicleModelDelete = "PERM_VEHICLE_MODEL_DELETE";
                public const string VehicleModelDetails = "PERM_VEHICLE_MODEL_DETAILS";
            }
            public class VehicleType
            {
                public const string VehicleTypeIndex = "PERM_VEHICLE_TYPE_INDEX";
                public const string VehicleTypeUpdate = "PERM_VEHICLE_TYPE_UPDATE";
                public const string VehicleTypeDelete = "PERM_VEHICLE_TYPE_DELETE";
                public const string VehicleTypeDetails = "PERM_VEHICLE_TYPE_DETAILS";
            }
            public class VehicleCode
            {
                public const string VehicleCodeIndex = "PERM_VEHICLE_CODE_INDEX";
                public const string VehicleCodeUpdate = "PERM_VEHICLE_CODE_UPDATE";
                public const string VehicleCodeDelete = "PERM_VEHICLE_CODE_DELETE";
                public const string VehicleCodeDetails = "PERM_VEHICLE_CODE_DETAILS";
            }
            public class DealerRegion
            {
                public const string DealerRegionIndex = "PERM_DEALER_REGION_INDEX";
                public const string DealerRegionCreate = "PERM_DEALER_REGION_CREATE";
                public const string DealerRegionUpdate = "PERM_DEALER_REGION_UPDATE";
                public const string DealerRegionDelete = "PERM_DEALER_REGION_DELETE";
                public const string DealerRegionDetails = "PERM_DEALER_REGION_DETAILS";
            }

            public class WebServiceError
            {
                public const string WebServiceErrorIndex = "PERM_WEB_SERVICE_ERROR_INDEX";

            }

            public class DealerRegionResponsible
            {
                public const string DealerRegionResponsibleIndex = "PERM_DEALER_REGION_RESPONSIBLE_INDEX";
                public const string DealerRegionResponsibleCreate = "PERM_DEALER_REGION_RESPONSIBLE_CREATE";
                public const string DealerRegionResponsibleUpdate = "PERM_DEALER_REGION_RESPONSIBLE_UPDATE";
                public const string DealerRegionResponsibleDelete = "PERM_DEALER_REGION_RESPONSIBLE_DELETE";
                public const string DealerRegionResponsibleDetails = "PERM_DEALER_REGION_RESPONSIBLE_DETAILS";
            }

            public class Dealer
            {
                public const string DealerIndex = "PERM_DEALER_INDEX";
                public const string DealerCreate = "PERM_DEALER_CREATE";
                public const string DealerUpdate = "PERM_DEALER_UPDATE";
                public const string DealerDelete = "PERM_DEALER_DELETE";
                public const string DealerDetails = "PERM_DEALER_DETAILS";
            }
            public class DealerVehicleGroups
            {
                public const string DealerVehicleGroupIndex = "PERM_DEALER_VEHICLEGROUP_INDEX";
                public const string DealerVehicleGroupSave = "PERM_DEALER_VEHICLEGROUP_SAVE";
                public const string DealerVehicleGroupDelete = "PERM_DEALER_VEHICLEGROUP_DEL";
            }
            public class DealerTechnicianGroup
            {
                public const string DealerTechnicianGroupIndex = "PERM_DEALER_TECH_GRP_INDEX";
                public const string DealerTechnicianGroupCreate = "PERM_DEALER_TECH_GRP_CREATE";
                public const string DealerTechnicianGroupDelete = "PERM_DEALER_TECH_GRP_DELETE";
                public const string DealerTechnicianGroupUpdate = "PERM_DEALER_TECH_GRP_UPDATE";
                public const string DealerTechnicianGroupDetails = "PERM_DEALER_TECH_GRP_DETAILS";
            }
            public class Permission
            {
                public const string PermissionIndex = "PERM_PERMISSION_INDEX";
                public const string PermissionCreate = "PERM_PERMISSION_CREATE";
                public const string PermissionUpdate = "PERM_PERMISSION_UPDATE";
                public const string PermissionDelete = "PERM_PERMISSION_DELETE";
                public const string PermissionDetails = "PERM_PERMISSION_DETAILS";
            }
            public class PartCostService
            {
                public const string PartCostServiceIndex = "PERM_PART_COST_SERVICE_INDEX";
            }
            public class ServiceCallSchedule
            {
                public const string ServiceCallScheduleIndex = "PERM_SERVICE_CALL_SCHEDULE_INDEX";
                public const string ServiceCallScheduleSelect = "PERM_SERVICE_CALL_SCHEDULE_SELECT";
                public const string ServiceCallScheduleUpdate = "PERM_SERVICE_CALL_SCHEDULE_UPDATE";
            }

            public class GeneralParameter
            {
                public const string GeneralParameterIndex = "PERM_GENERAL_PARAMETER_INDEX";
                public const string GeneralParameterUpdate = "PERM_GENERAL_PARAMETER_UPDATE";
            }

            public class User
            {
                public const string UserIndex = "PERM_USER_INDEX";
                public const string UserCreate = "PERM_USER_CREATE";
                public const string UserUpdate = "PERM_USER_UPDATE";
                public const string UserDelete = "PERM_USER_DELETE";
                public const string UserDetails = "PERM_USER_DETAILS";
                public const string DealerUserIndex = "PERM_DEALER_USER_INDEX";
                public const string DealerUserCreate = "PERM_DEALER_USER_CREATE";
                public const string DealerUserUpdate = "PERM_DEALER_USER_UPDATE";
                public const string DealerUserDelete = "PERM_DEALER_USER_DELETE";
                public const string DealerUserDetails = "PERM_DEALER_USER_DETAILS";
                public const string DealerUserConvert = "PERM_DEALER_USER_CONVERT";
            }

            public class Menu
            {
                public const string MenuIndex = "PERM_MENU_INDEX";
                public const string MenuUpdate = "PERM_MENU_UPDATE";
                public const string MenuDetails = "PERM_MENU_DETAILS";
            }

            public class SparePartBarcode
            {
                public const string SparePartBarcodeIndex = "PERM_SPARE_PART_BARCODE_INDEX";
            }

            public class SparePartType
            {
                public const string SparePartTypeIndex = "PERM_SPARE_PART_TYPE_INDEX";
                public const string SparePartTypeCreate = "PERM_SPARE_PART_TYPE_CREATE";
                public const string SparePartTypeUpdate = "PERM_SPARE_PART_TYPE_UPDATE";
                public const string SparePartTypeDelete = "PERM_SPARE_PART_TYPE_DELETE";
                public const string SparePartTypeDetails = "PERM_SPARE_PART_TYPE_DETAILS";
            }

            public class SparePartSaleDetail
            {
                public const string SparePartSaleDetailIndex = "PERM_SPARE_PART_SALE_DETAIL_INDEX";
                public const string SparePartSaleDetailCreate = "PERM_SPARE_PART_SALE_DETAIL_CREATE";
                public const string SparePartSaleDetailUpdate = "PERM_SPARE_PART_SALE_DETAIL_UPDATE";
                public const string SparePartSaleDetailDelete = "PERM_SPARE_PART_SALE_DETAIL_DELETE";
                public const string SparePartSaleDetailDetails = "PERM_SPARE_PART_SALE_DETAIL_DETAILS";
                public const string SparePartSaleDetailOrder = "PERM_SPARE_PART_SALE_DETAIL_ORDER";
            }
            public class SparePartSale
            {
                public const string SparePartSaleInvoice = "PERM_SPARE_PART_SALE_INVOICE";
                public const string SparePartSaleCollect = "PERM_SPARE_PART_SALE_COLLECT";
                public const string SparePartSaleCancelCollect = "PERM_SPARE_PART_SALE_CANCEL_COLLECT";
                public const string SparePartSaleWaybill = "PERM_SPARE_PART_SALE_WAYBILL";
                public const string SparePartSaleIndex = "PERM_SPARE_PART_SALE_INDEX";
                public const string SparePartSaleCreate = "PERM_SPARE_PART_SALE_CREATE";
                public const string SparePartSaleUpdate = "PERM_SPARE_PART_SALE_UPDATE";
                public const string SparePartSaleCancel = "PERM_SPARE_PART_SALE_CANCEL";
                public const string SparePartSaleDetails = "PERM_SPARE_PART_SALE_DETAILS";
                public const string SparePartSaleDelete = "PERM_SPARE_PART_SALE_DELETE";

                public const string OtokarSparePartSaleInvoice = "PERM_OTOKAR_SPARE_PART_SALE_INVOICE";
                public const string OtokarSparePartSaleCollect = "PERM_OTOKAR_SPARE_PART_SALE_COLLECT";
                public const string OtokarSparePartSaleIndex = "PERM_OTOKAR_SPARE_PART_SALE_INDEX";
                public const string OtokarSparePartSaleCreate = "PERM_OTOKAR_SPARE_PART_SALE_CREATE";
                public const string OtokarSparePartSaleUpdate = "PERM_OTOKAR_SPARE_PART_SALE_UPDATE";
                public const string OtokarSparePartSaleCancel = "PERM_OTOKAR_SPARE_PART_SALE_CANCEL";
                public const string OtokarSparePartSaleDetails = "PERM_OTOKAR_SPARE_PART_SALE_DETAILS";
            }
            public class Currency
            {
                public const string CurrencyIndex = "PERM_CURRENCY_INDEX";
                public const string CurrencyCreate = "PERM_CURRENCY_CREATE";
                public const string CurrencyUpdate = "PERM_CURRENCY_UPDATE";
                public const string CurrencyDelete = "PERM_CURRENCY_DELETE";
                public const string CurrencyDetails = "PERM_CURRENCY_DETAILS";
            }

            public class SparePart
            {
                public const string SparePartIndex = "PERM_SPARE_PART_INDEX";
                public const string SparePartCreate = "PERM_SPARE_PART_CREATE";
                public const string SparePartUpdate = "PERM_SPARE_PART_UPDATE";
                public const string SparePartDelete = "PERM_SPARE_PART_DELETE";
                public const string SparePartDetails = "PERM_SPARE_PART_DETAILS";
            }

            public class Rack
            {
                public const string RackIndex = "PERM_RACK_INDEX";
                public const string RackCreate = "PERM_RACK_CREATE";
                public const string RackUpdate = "PERM_RACK_UPDATE";
                public const string RackDelete = "PERM_RACK_DELETE";
                public const string RackDetails = "PERM_RACK_DETAILS";
            }

            public class DealerAccountInfo
            {
                public const string DealerAccountInfoIndex = "PERM_DealerAccountInfo_INDEX";
                public const string DealerAccountInfoCreate = "PERM_DealerAccountInfo_CREATE";
                public const string DealerAccountInfoUpdate = "PERM_DealerAccountInfo_UPDATE";
                public const string DealerAccountInfoDelete = "PERM_DealerAccountInfo_DELETE";
            }

            public class Customer
            {
                public const string CustomerIndex = "PERM_CUSTOMER_INDEX";
                public const string CustomerCreate = "PERM_CUSTOMER_CREATE";
                public const string CustomerUpdate = "PERM_CUSTOMER_UPDATE";
                public const string CustomerDelete = "PERM_CUSTOMER_DELETE";
                public const string CustomerDetails = "PERM_CUSTOMER_DETAILS";
                public const string CustomerExcelExport = "PERM_CUSTOMER_EXCEL_EXPORT";
            }

            public class CustomerAddress
            {
                public const string CustomerAddressIndex = "PERM_CUSTOMER_ADDRESS_INDEX";
                public const string CustomerAddressCreate = "PERM_CUSTOMER_ADDRESS_CREATE";
                public const string CustomerAddressUpdate = "PERM_CUSTOMER_ADDRESS_UPDATE";
                public const string CustomerAddressDelete = "PERM_CUSTOMER_ADDRESS_DELETE";
                public const string CustomerAddressDetails = "PERM_CUSTOMER_ADDRESS_DETAILS";
            }

            public class CustomerContact
            {
                public const string CustomerContactIndex = "PERM_CUSTOMER_CONTACT_INDEX";
                public const string CustomerContactCreate = "PERM_CUSTOMER_CONTACT_CREATE";
                public const string CustomerContactUpdate = "PERM_CUSTOMER_CONTACT_UPDATE";
                public const string CustomerContactDelete = "PERM_CUSTOMER_CONTACT_DELETE";
                public const string CustomerContactDetails = "PERM_CUSTOMER_CONTACT_DETAILS";
            }

            public class DealerGuaranteeRatio
            {
                public const string DealerGuaranteeRatioIndex = "PERM_DEALER_GUARANTEE_RATIO_INDEX";
                public const string DealerGuaranteeRatioUpdate = "PERM_DEALER_GUARANTEE_RATIO_UPDATE";
                public const string DealerGuaranteeRatioCreate = "PERM_DEALER_GUARANTEE_RATIO_CREATE";
            }

            public class StockTypeDetail
            {
                public const string StockTypeDetailIndex = "PERM_STOCK_TYPE_DETAIL_INDEX";
            }

            public class StockRackDetail
            {
                public const string EmptyStockRackDetailIndex = "PERM_EMPTY_STOCK_RACK_DETAIL_INDEX";
                public const string StockRackDetailIndex = "PERM_STOCK_RACK_DETAIL_INDEX";
                public const string StockRackTypeDetailIndex = "PERM_STOCK_RACK_TYPE_DETAIL_INDEX";
                public const string StockExchangeIndex = "PERM_STOCK_EXCHANGE_INDEX";
            }

            public class SparePartAssemble
            {
                public const string SparePartAssembleIndex = "PERM_SPARE_PART_ASSEMBLE_INDEX";
                public const string SparePartAssembleCreate = "PERM_SPARE_PART_ASSEMBLE_CREATE";
                public const string SparePartAssembleUpdate = "PERM_SPARE_PART_ASSEMBLE_UPDATE";
                public const string SparePartAssembleDelete = "PERM_SPARE_PART_ASSEMBLE_DELETE";
                public const string ChangeSplitPartUsage = "PERM_SPLIT_PART_USAGE_CHANGE";
            }

            public class AppointmentIndicatorFailureCode
            {
                public const string AppointmentIndicatorFailureCodeIndex = "PERM_APPOINTMENT_INDICATOR_FAILURE_CODE_INDEX";
                public const string AppointmentIndicatorFailureCodeCreate = "PERM_APPOINTMENT_INDICATOR_FAILURE_CODE_CREATE";
                public const string AppointmentIndicatorFailureCodeUpdate = "PERM_APPOINTMENT_INDICATOR_FAILURE_CODE_UPDATE";
                public const string AppointmentIndicatorFailureCodeDelete = "PERM_APPOINTMENT_INDICATOR_FAILURE_CODE_DELETE";
            }

            public class PDIControlDefinition
            {
                public const string PDIControlDefinitionIndex = "PERM_PDI_CONTROL_DEFINITION_INDEX";
                public const string PDIControlDefinitionCreate = "PERM_PDI_CONTROL_DEFINITION_CREATE";
                public const string PDIControlDefinitionUpdate = "PERM_PDI_CONTROL_DEFINITION_UPDATE";
                public const string PDIControlDefinitionDelete = "PERM_PDI_CONTROL_DEFINITION_DELETE";
                public const string PDIControlDefinitionDetails = "PERM_PDI_CONTROL_DEFINITION_DETAILS";
            }

            public class PDIControlPartDefinition
            {
                public const string PDIControlPartDefinitionIndex = "PERM_PDI_CONTROL_PART_DEFINITION_INDEX";
                public const string PDIControlPartDefinitionCreate = "PERM_PDI_CONTROL_PART_DEFINITION_CREATE";
                public const string PDIControlPartDefinitionDelete = "PERM_PDI_CONTROL_PART_DEFINITION_DELETE";
                public const string PDIControlPartDefinitionDetails = "PERM_PDI_CONTROL_PART_DEFINITION_DETAILS";
                public const string PDIControlPartDefinitionUpdate = "PERM_PDI_CONTROL_PART_DEFINITION_UPDATE";
            }

            public class PDIResultDefinition
            {
                public const string PDIResultDefinitionIndex = "PERM_PDI_RESULT_DEFINITION_INDEX";
                public const string PDIResultDefinitionCreate = "PERM_PDI_RESULT_DEFINITION_CREATE";
                public const string PDIResultDefinitionUpdate = "PERM_PDI_RESULT_DEFINITION_UPDATE";
                public const string PDIResultDefinitionDelete = "PERM_PDI_RESULT_DEFINITION_DELETE";
                public const string PDIResultDefinitionDetails = "PERM_PDI_RESULT_DEFINITION_DETAILS";
            }

            public class CriticalStockCard
            {
                public const string CriticalStockCardIndex = "PERM_CRITICAL_STOCK_CARD_INDEX";
                public const string CriticalStockCardCreate = "PERM_CRITICAL_STOCK_CARD_CREATE";
                public const string CriticalStockCardDelete = "PERM_CRITICAL_STOCK_CARD_DELETE";
                public const string CriticalStockCardExcel = "PERM_CRITICAL_STOCK_CARD_EXCEL";
            }

            public class CustomerDiscount
            {
                public const string CustomerDiscountIndex = "PERM_CUSTOMER_DISCOUNT_INDEX";
                public const string CustomerDiscountCreate = "PERM_CUSTOMER_DISCOUNT_CREATE";
                public const string CustomerDiscountUpdate = "PERM_CUSTOMER_DISCOUNT_UPDATE";
                public const string CustomerDiscountDelete = "PERM_CUSTOMER_DISCOUNT_DELETE";
                public const string CustomerDiscountDetails = "PERM_CUSTOMER_DISCOUNT_DETAILS";
            }
            public class WorkOrderBatchInvoice
            {
                public const string WorkOrderBatchInvoiceIndex = "PERM_WORK_ORDER_BATCH_INVOICE_INDEX";
            }

            public class WorkOrderInvoiceList
            {
                public const string WorkOrderInvoiceListIndex = "PERM_WORK_ORDER_INVOICE_LIST_INDEX";
                public const string WorkOrderInvoiceListDelete = "PERM_WORK_ORDER_INVOICE_LIST_DELETE";
            }

            public class ClaimWaybill
            {
                public const string ClaimWaybillIndex = "PERM_CLAIM_WAYBILL_INDEX";
                public const string ClaimWaybillDetails = "PERM_CLAIM_WAYBILL_DETAILS";
            }

            public class DeliveryList
            {
                public const string DeliveryListIndex = "PERM_DELIVERY_LIST_INDEX";
            }
            public class SparePartSaleOrder
            {
                public const string SparePartSaleOrderCollect = "PERM_SPARE_PART_SALE_ORDER_COLLECT";
                public const string SparePartSaleOrderIndex = "PERM_SPARE_PART_SALE_ORDER_INDEX";
                public const string SparePartSaleOrderCreate = "PERM_SPARE_PART_SALE_ORDER_CREATE";
                public const string SparePartSaleOrderUpdate = "PERM_SPARE_PART_SALE_ORDER_UPDATE";
                public const string SparePartSaleOrderCancel = "PERM_SPARE_PART_SALE_ORDER_CANCEL";
                public const string SparePartSaleOrderDetails = "PERM_SPARE_PART_SALE_ORDER_DETAILS";
                public const string SparePartSaleOrderDelete = "PERM_SPARE_PART_SALE_ORDER_DELETE";
                public const string SparePartSaleOrderCreateSaleOrder = "PERM_SPARE_PART_SALE_ORDER_CREATE_SO";
            }
            public class BreakdownDefinition
            {
                public const string BreakdownDefinitionIndex = "PERM_BREAKDOWN_DEFINITION_INDEX";
                public const string BreakdownDefinitionCreate = "PERM_BREAKDOWN_DEFINITION_CREATE";
                public const string BreakdownDefinitionUpdate = "PERM_BREAKDOWN_DEFINITION_UPDATE";
                public const string BreakdownDefinitionDelete = "PERM_BREAKDOWN_DEFINITION_DELETE";
                public const string BreakdownDefinitionDetails = "PERM_BREAKDOWN_DEFINITION_DETAILS";
            }

            public class PDIPartDefinition
            {
                public const string PDIPartDefinitionIndex = "PERM_PDI_PART_DEFINITION_INDEX";
                public const string PDIPartDefinitionCreate = "PERM_PDI_PART_DEFINITION_CREATE";
                public const string PDIPartDefinitionUpdate = "PERM_PDI_PART_DEFINITION_UPDATE";
                public const string PDIPartDefinitionDelete = "PERM_PDI_PART_DEFINITION_DELETE";
                public const string PDIPartDefinitionDetails = "PERM_PDI_PART_DEFINITION_DETAILS";
            }

            public class StockBlock
            {
                public const string StockBlockIndex = "PERM_STOCK_BLOCK_INDEX";
                public const string StockBlockCreate = "PERM_STOCK_BLOCK_CREATE";
                public const string StockBlockUpdate = "PERM_STOCK_BLOCK_UPDATE";
                public const string StockBlockDelete = "PERM_STOCK_BLOCK_DELETE";
            }

            public class PurchaseOrderSuggestion
            {
                public const string PurchaseOrderSuggestionIndex = "PERM_PURCHASE_ORDER_SUGGESTION_INDEX";
            }

            public class StockLocation
            {
                public const string StockLocationIndex = "PERM_STOCK_LOCATION_INDEX";
            }

            public class StockCardYearly
            {
                public const string StockCardYearlyIndex = "PERM_STOCK_CARD_YEARLY_INDEX";
            }

            public class PeriodicMaintControlList
            {
                public const string PeriodicMaintControlListIndex = "PERM_PERIODIC_MAINT_CONTROL_LIST_INDEX";
                public const string PeriodicMaintControlListCreate = "PERM_PERIODIC_MAINT_CONTROL_LIST_CREATE";
                public const string PeriodicMaintControlListDelete = "PERM_PERIODIC_MAINT_CONTROL_LIST_DELETE";
            }

            public class GuaranteeAuthorityGroupDealers
            {
                public const string GuaranteeAuthorityGroupDealersIndex = "PERM_GUARANTEE_AUTHORITY_GROUP_DEALERS_INDEX";
                public const string GuaranteeAuthorityGroupDealersSave = "PERM_GUARANTEE_AUTHORITY_GROUP_DEALERS_SAVE";
            }

            public class PDIGOSApproveGroupDealers
            {
                public const string PDIGOSApproveGroupDealersIndex = "PERM_PDI_GOS_APPROVE_GROUP_DEALERS_INDEX";
                public const string PDIGOSApproveGroupDealersSave = "PERM_PDI_GOS_APPROVE_GROUP_DEALERS_SAVE";
            }

            public class GuaranteeAuthorityGroupVehicle
            {
                public const string GuaranteeAuthorityGroupVehicleIndex = "PERM_GUARANTEE_AUTHORITY_GROUP_VEHICLE_INDEX";
                public const string GuaranteeAuthorityGroupVehicleSave = "PERM_GUARANTEE_AUTHORITY_GROUP_VEHICLE_SAVE";
            }

            public class DealerPurchaseOrderConfirm
            {
                public const string DealerPurchaseOrderConfirmIndex = "PERM_DEALER_PURCHASE_ORDER_CONFIRM_INDEX";
                public const string DealerPurchaseOrderConfirmSave = "PERM_DEALER_PURCHASE_ORDER_CONFIRM_SAVE";
            }

            public class Fleet
            {
                public const string FleetIndex = "PERM_FLEET_INDEX";
                public const string FleetCreate = "PERM_FLEET_CREATE";
                public const string FleetUpdate = "PERM_FLEET_UPDATE";
                public const string FleetDelete = "PERM_FLEET_DELETE";
            }

            public class FleetRequestConfirm
            {
                public const string FleetRequestConfirmIndex = "PERM_FLEET_REQUEST_CONFIRM_INDEX";
                public const string FleetRequestConfirmCreate = "PERM_FLEET_REQUEST_CONFIRM_CREATE";
            }

            public class FixAssetInventoryOutput
            {
                public const string FixAssetInventoryOutputIndex = "PERM_FIX_ASSET_INVENTORY_OUTPUT_INDEX";
                public const string FixAssetInventoryOutputCreate = "PERM_FIX_ASSET_INVENTORY_OUTPUT_CREATE";
                public const string FixAssetInventoryOutputUpdate = "PERM_FIX_ASSET_INVENTORY_OUTPUT_UPDATE";
            }

            public class AnnouncementRole
            {
                public const string AnnouncementRoleIndex = "PERM_ANNOUNCEMENT_ROLE_INDEX";
                public const string AnnouncementRoleUpdate = "PERM_ANNOUNCEMENT_ROLE_UPDATE";
            }

            public class PurchaseOrder
            {
                public const string PurchaseOrderIndex = "PERM_PURCHASE_ORDER_INDEX";
                public const string PurchaseOrderCreate = "PERM_PURCHASE_ORDER_CREATE";
                public const string PurchaseOrderUpdate = "PERM_PURCHASE_ORDER_UPDATE";
                public const string PurchaseOrderDelete = "PERM_PURCHASE_ORDER_DELETE";
                public const string PurchaseOrderDetails = "PERM_PURCHASE_ORDER_DETAILS";
                public const string PurchaseOrderSearchIndex = "PERM_PURCHASE_ORDER_SEARCH_INDEX";
                public const string PurchaseOrderCancel = "PERM_PURCHASE_ORDER_CANCEL";
            }

            public class ClaimDismantledParts
            {
                public const string ClaimDismantledPartsIndex = "PERM_CLAIM_SUPPLIER_INDEX";
                public const string ClaimDismantledPartsComplete = "PERM_CLAIM_SUPPLIER_COMPLETE";
            }

            public class ClaimSupplier
            {
                public const string ClaimSupplierIndex = "PERM_CLAIM_SUPPLIER_INDEX";
                public const string ClaimSupplierCreate = "PERM_CLAIM_SUPPLIER_CREATE";
                public const string ClaimSupplierUpdate = "PERM_CLAIM_SUPPLIER_UPDATE";
                public const string ClaimSupplierDelete = "PERM_CLAIM_SUPPLIER_DELETE";
                public const string ClaimSupplierExcel = "PERM_CLAIM_SUPPLIER_EXCEL";
            }

            public class SparePartCountryVatRatio
            {
                public const string SparePartCountryVatRatioIndex = "PERM_SPARE_PART_COUNTRY_VAT_RATIO_INDEX";
                public const string SparePartCountryVatRatioCreate = "PERM_SPARE_PART_COUNTRY_VAT_RATIO_CREATE";
                public const string SparePartCountryVatRatioUpdate = "PERM_SPARE_PART_COUNTRY_VAT_RATIO_UPDATE";
                public const string SparePartCountryVatRatioDelete = "PERM_SPARE_PART_COUNTRY_VAT_RATIO_DELETE";
            }

            public class CampaignRequest
            {
                public const string CampaignRequestIndex = "PERM_CAMPAIGN_REQUEST_INDEX";
                public const string CampaignRequestCreate = "PERM_CAMPAIGN_REQUEST_CREATE";
                public const string CampaignRequestUpdate = "PERM_CAMPAIGN_REQUEST_UPDATE";
                public const string CampaignRequestDelete = "PERM_CAMPAIGN_REQUEST_DELETE";
                public const string CampaignRequestDetails = "PERM_CAMPAIGN_REQUEST_DETAILS";
            }

            public class DealerClass
            {
                public const string DealerClassIndex = "PERM_DEALER_CLASS_INDEX";
                public const string DealerClassCreate = "PERM_DEALER_CLASS_CREATE";
                public const string DealerClassUpdate = "PERM_DEALER_CLASS_UPDATE";
                public const string DealerClassDelete = "PERM_DEALER_CLASS_DELETE";
            }

            public class DealerSaleSparepart
            {
                public const string DealerSaleSparepartIndex = "PERM_DEALER_SALE_SPAREPART_INDEX";
                public const string DealerSaleSparepartCreate = "PERM_DEALER_SALE_SPAREPART_CREATE";
                public const string DealerSaleSparepartUpdate = "PERM_DEALER_SALE_SPAREPART_UPDATE";
                public const string DealerSaleSparepartDelete = "PERM_DEALER_SALE_SPAREPART_DELETE";
                public const string DealerSaleSparepartDetails = "PERM_DEALER_SALE_SPAREPART_DETAILS";
            }

            public class Maintenance
            {
                public const string MaintenanceIndex = "PERM_MAINTENANCE_INDEX";
                public const string MaintenanceCreate = "PERM_MAINTENANCE_CREATE";
                public const string MaintenanceUpdate = "PERM_MAINTENANCE_UPDATE";
                public const string MaintenanceDelete = "PERM_MAINTENANCE_DELETE";
                public const string MaintenanceDetails = "PERM_MAINTENANCE_DETAILS";

                public const string MaintenanceIndexExcelExport = "PERM_MAINTENANCE_INDEX_EXCEL_EXPORT";

            }
            public class MaintenancePart
            {
                public const string MaintenancePartsIndex = "PERM_MAINTENANCE_PARTS_INDEX";
                public const string MaintenancePartsCreate = "PERM_MAINTENANCE_PARTS_CREATE";
                public const string MaintenancePartsUpdate = "PERM_MAINTENANCE_PARTS_UPDATE";
                public const string MaintenancePartsDelete = "PERM_MAINTENANCE_PARTS_DELETE";
                public const string MaintenancePartsDetails = "PERM_MAINTENANCE_PARTS_DETAILS";
                public const string MaintenancePartIndexExcelExport = "PERM_MAINTENANCE_PART_INDEX_EXCEL_EXPORT";
            }
            public class AppointmentDetailsParts
            {
                public const string AppointmentDetailsPartsIndex = "PERM_APPOINTMENT_DETAILS_PARTS_INDEX";
                public const string AppointmentDetailsPartsCreate = "PERM_APPOINTMENT_DETAILS_PARTS_CREATE";
                public const string AppointmentDetailsPartsUpdate = "PERM_APPOINTMENT_DETAILS_PARTS_UPDATE";
                public const string AppointmentDetailsPartsDelete = "PERM_APPOINTMENT_DETAILS_PARTS_DELETE";
                public const string AppointmentDetailsPartsDetails = "PERM_APPOINTMENT_DETAILS_PARTS_DETAILS";
            }
            public class Education
            {
                public const string EducationIndex = "PERM_EDUCATION_INDEX";
                public const string EducationCreate = "PERM_EDUCATION_CREATE";
                public const string EducationUpdate = "PERM_EDUCATION_UPDATE";
                public const string EducationDelete = "PERM_EDUCATION_DELETE";
                public const string EducationDetails = "PERM_EDUCATION_DETAILS";
            }

            public class Equipment
            {
                public const string EquipmentIndex = "PERM_EQUIPMENT_INDEX";
                public const string EquipmentSelect = "PERM_EQUIPMENT_SELECT";
                public const string EquipmentCreate = "PERM_EQUIPMENT_CREATE";
                public const string EquipmentUpdate = "PERM_EQUIPMENT_UPDATE";
                public const string EquipmentDelete = "PERM_EQUIPMENT_DELETE";
            }

            public class PurchaseOrderType
            {
                public const string PurchaseOrderTypeIndex = "PERM_PURCHASE_ORDER_TYPE_INDEX";
                public const string PurchaseOrderTypeSelect = "PERM_PURCHASE_ORDER_TYPE_SELECT";
                public const string PurchaseOrderTypeDetail = "PERM_PURCHASE_ORDER_TYPE_DETAIL";
                public const string PurchaseOrderTypeCreate = "PERM_PURCHASE_ORDER_TYPE_CREATE";
                public const string PurchaseOrderTypeUpdate = "PERM_PURCHASE_ORDER_TYPE_UPDATE";
                public const string PurchaseOrderTypeDelete = "PERM_PURCHASE_ORDER_TYPE_DELETE";
            }

            public class SparePartSAPUnit
            {
                public const string SparePartSAPUnitIndex = "PERM_SPARE_PART_SAP_UNIT_INDEX";
                public const string SparePartSAPUnitSelect = "PERM_SPARE_PART_SAP_UNIT_SELECT";
                public const string SparePartSAPUnitDetail = "PERM_SPARE_PART_SAP_UNIT_DETAIL";
                public const string SparePartSAPUnitCreate = "PERM_SPARE_PART_SAP_UNIT_CREATE";
                public const string SparePartSAPUnitUpdate = "PERM_SPARE_PART_SAP_UNIT_UPDATE";
                public const string SparePartSAPUnitDelete = "PERM_SPARE_PART_SAP_UNIT_DELETE";
            }

            public class LabourMainGroup
            {
                public const string LabourMainGroupIndex = "PERM_LABOUR_MAIN_GROUP_INDEX";
                public const string LabourMainGroupDetail = "PERM_LABOUR_MAIN_GROUP_DETAIL";
                public const string LabourMainGroupCreate = "PERM_LABOUR_MAIN_GROUP_CREATE";
                public const string LabourMainGroupUpdate = "PERM_LABOUR_MAIN_GROUP_UPDATE";
                public const string LabourMainGroupDelete = "PERM_LABOUR_MAIN_GROUP_DELETE";
            }

            public class LabourSubGroup
            {
                public const string LabourSubGroupIndex = "PERM_LABOUR_SUB_GROUP_INDEX";
                public const string LabourSubGroupDetail = "PERM_LABOUR_SUB_GROUP_DETAIL";
                public const string LabourSubGroupCreate = "PERM_LABOUR_SUB_GROUP_CREATE";
                public const string LabourSubGroupUpdate = "PERM_LABOUR_SUB_GROUP_UPDATE";
                public const string LabourSubGroupDelete = "PERM_LABOUR_SUB_GROUP_DELETE";
            }


            public class PurchaseOrderGroup
            {
                public const string PurchaseOrderGroupIndex = "PERM_PURCHASE_ORDER_GROUP_INDEX";
                public const string PurchaseOrderGroupSelect = "PERM_PURCHASE_ORDER_GROUP_SELECT";
                public const string PurchaseOrderGroupDetail = "PERM_PURCHASE_ORDER_GROUP_DETAIL";
                public const string PurchaseOrderGroupCreate = "PERM_PURCHASE_ORDER_GROUP_CREATE";
                public const string PurchaseOrderGroupUpdate = "PERM_PURCHASE_ORDER_GROUP_UPDATE";
                public const string PurchaseOrderGroupDelete = "PERM_PURCHASE_ORDER_GROUP_DELETE";
            }

            public class PurchaseOrderMatch
            {
                public const string PurchaseOrderMatchIndex = "PERM_PURCHASE_ORDER_MATCH_INDEX";
                public const string PurchaseOrderMatchSelect = "PERM_PURCHASE_ORDER_MATCH_SELECT";
                public const string PurchaseOrderMatchDetail = "PERM_PURCHASE_ORDER_MATCH_DETAIL";
                public const string PurchaseOrderMatchCreate = "PERM_PURCHASE_ORDER_MATCH_CREATE";
                public const string PurchaseOrderMatchUpdate = "PERM_PURCHASE_ORDER_MATCH_UPDATE";
                public const string PurchaseOrderMatchDelete = "PERM_PURCHASE_ORDER_MATCH_DELETE";
            }

            public class PurchaseOrderGroupRelation
            {
                public const string PurchaseOrderGroupRelationIndex = "PERM_PURCHASE_ORDER_GROUP_RELATION_INDEX";
                public const string PurchaseOrdeGroupRelationSelect = "PERM_PURCHASE_ORDER_GROUP_RELATION_SELECT";
                public const string PurchaseOrderGroupRelationCreate = "PERM_PURCHASE_ORDER_GROUP_RELATION_CREATE";
            }


            public class FleetPartPartial
            {
                public const string FleetPartPartialIndex = "PERM_FLEETPARTPARTIAL_INDEX";
                public const string FleetPartPartialSelect = "PERM_FLEETPARTPARTIAL_SELECT";
                public const string FleetPartPartialCreate = "PERM_FLEETPARTPARTIAL_CREATE";
                public const string FleetPartPartialDelete = "PERM_FLEETPARTPARTIAL_DELETE";
            }

            public class TechnicianOperationControl
            {
                public const string TechnicianOperationControlIndex = "PERM_TECHNICIANOPERATIONCONTROL_INDEX";
            }

            public class ClaimDismantledPartDelivery
            {
                public const string ClaimDismantledPartDeliveryIndex = "PERM_CLAIM_DISMANTLED_PART_DELIVERY_INDEX";
                public const string ClaimDismantledPartDeliveryCreate = "PERM_CLAIM_DISMANTLED_PART_DELIVERY_CREATE";
                public const string ClaimDismantledPartDeliveryUpdate = "PERM_CLAIM_DISMANTLED_PART_DELIVERY_UPDATE";
                public const string ClaimDismantledPartDeliveryDelete = "PERM_CLAIM_DISMANTLED_PART_DELIVERY_DELETE";
                public const string ClaimDismantledPartDeliveryDetails = "PERM_CLAIM_DISMANTLED_PART_DELIVERY_DETAILS";
            }

            public class EducationRequest
            {
                public const string EducationRequestIndex = "PERM_EDUCATION_REQUEST_INDEX";
                public const string EducationRequestCreate = "PERM_EDUCATION_REQUEST_CREATE";
                public const string EducationRequestUpdate = "PERM_EDUCATION_REQUEST_UPDATE";
                public const string EducationRequestDelete = "PERM_EDUCATION_REQUEST_DELETE";
                public const string EducationRequestDetails = "PERM_EDUCATION_REQUEST_DETAILS";
            }
            public class FleetRequest
            {
                public const string FleetRequestIndex = "PERM_FLEET_REQUEST_INDEX";
                public const string FleetRequestCreate = "PERM_FLEET_REQUEST_CREATE";
                public const string FleetRequestUpdate = "PERM_FLEET_REQUEST_UPDATE";
                public const string FleetRequestDelete = "PERM_FLEET_REQUEST_DELETE";
                public const string FleetRequestDetails = "PERM_FLEET_REQUEST_DETAILS";
            }
            public class EducationType
            {
                public const string EducationTypeIndex = "PERM_EDUCATION_TYPE_INDEX";
                public const string EducationTypeCreate = "PERM_EDUCATION_TYPE_CREATE";
                public const string EducationTypeUpdate = "PERM_EDUCATION_TYPE_UPDATE";
                public const string EducationTypeDelete = "PERM_EDUCATION_TYPE_DELETE";
                public const string EducationTypeDetails = "PERM_EDUCATION_TYPE_DETAILS";
            }

            public class CustomerSearch
            {
                public const string CustomerSearchIndex = "PERM_CUSTOMER_SEARCH";
            }

            public class AppointmentIndicatorSubCategorySearch
            {
                public const string AppointmentIndicatorSubCategorySearchIndex = "PERM_APP_IND_SUB_CAT_SEARCH";
            }

            public class AppointmentDetailsLabours
            {
                public const string AppointmentDetailsLaboursIndex = "PERM_APPOINTMENT_DETAILS_LABOURS_INDEX";
                public const string AppointmentDetailsLaboursCreate = "PERM_APPOINTMENT_DETAILS_LABOURS_CREATE";
                public const string AppointmentDetailsLaboursUpdate = "PERM_APPOINTMENT_DETAILS_LABOURS_UPDATE";
                public const string AppointmentDetailsLaboursDelete = "PERM_APPOINTMENT_DETAILS_LABOURS_DELETE";
            }
            public class VehicleNotes
            {
                public const string VehicleNotesIndex = "PERM_VEHICLE_NOTES_INDEX";
                public const string VehicleNotesCreate = "PERM_VEHICLE_NOTES_CREATE";
                public const string VehicleNotesUpdate = "PERM_VEHICLE_NOTES_UPDATE";
                public const string VehicleNotesDelete = "PERM_VEHICLE_NOTES_DELETE";
            }

            public class CampaignRequestOrders
            {
                public const string CampaignRequestOrdersIndex = "PERM_CAMPAIGN_REQUEST_ORDER_INDEX";
                public const string CampaignRequestOrdersDelete = "PERM_CAMPAIGN_REQUEST_ORDER_DELETE";
            }

            public class VehicleNoteApprove
            {
                public const string VehicleNoteApproveIndex = "PERM_VEHICLE_NOTE_APPROVE_INDEX";
                public const string VehicleNoteApproveApprove = "PERM_VEHICLE_NOTE_APPROVE";
                public const string VehicleNoteApproveDelete = "PERM_VEHICLE_NOTE_APPROVE_DELETE";
            }
            public class VehicleSearch
            {
                public const string VehicleSearchIndex = "PERM_VEHICLE_SEARCH";
            }
            public class AppointmentSearch
            {
                public const string AppointmentSearchIndex = "PERM_APPOINTMENT_SEARCH";
            }
            public class EducationDates
            {
                public const string EducationDatesIndex = "PERM_EDUCATION_DATES_INDEX";
                public const string EducationDatesCreate = "PERM_EDUCATION_DATES_CREATE";
                public const string EducationDatesUpdate = "PERM_EDUCATION_DATES_UPDATE";
                public const string EducationDatesDelete = "PERM_EDUCATION_DATES_DELETE";
                public const string EducationDatesDetails = "PERM_EDUCATION_DATES_DETAILS";
            }

            public class FleetVehicle
            {
                public const string FleetVehicleIndex = "PERM_FLEET_VEHICLE_INDEX";
                public const string FleetVehicleCreate = "PERM_FLEET_VEHICLE_CREATE";
                public const string FleetVehicleUpdate = "PERM_FLEET_VEHICLE_UPDATE";
                public const string FleetVehicleDelete = "PERM_FLEET_VEHICLE_DELETE";
                public const string FleetVehicleDetails = "PERM_FLEET_VEHICLE_DETAILS";
            }

            public class FleetRequestVehicle
            {
                public const string FleetRequestVehicleIndex = "PERM_FLEET_REQUEST_VEHICLE_INDEX";
                public const string FleetRequestVehicleCreate = "PERM_FLEET_REQUEST_VEHICLE_CREATE";
                public const string FleetRequestVehicleUpdate = "PERM_FLEET_REQUEST_VEHICLE_UPDATE";
                public const string FleetRequestVehicleDelete = "PERM_FLEET_REQUEST_VEHICLE_DELETE";
                public const string FleetRequestVehicleDetails = "PERM_FLEET_REQUEST_VEHICLE_DETAILS";
            }

            public class VehicleBodywork
            {
                public const string VehicleBodyworkIndex = "PERM_VEHICLE_BODYWORK_INDEX";
                public const string VehicleBodyworkCreate = "PERM_VEHICLE_BODYWORK_CREATE";
                public const string VehicleBodyworkUpdate = "PERM_VEHICLE_BODYWORK_UPDATE";
                public const string VehicleBodyworkDetails = "PERM_VEHICLE_BODYWORK_DETAILS";
                public const string VehicleBodyworkDelete = "PERM_VEHICLE_BODYWORK_DELETE";
            }
            public class MaintenanceAppointment
            {
                public const string MaintenanceAppointmentIndex = "PERM_MAINTENANCE_APPOINTMENT_INDEX";
                public const string MaintenanceAppointmentCreate = "PERM_MAINTENANCE_APPOINTMENT_CREATE";
            }
            public class EducationContributers
            {
                public const string EducationContributersCreate = "PERM_EDUCATION_CONTRIBUTERS_CREATE";
                public const string EducationContributersIndex = "PERM_EDUCATION_CONTRIBUTERS_INDEX";
                public const string EducationContributersDelete = "PERM_EDUCATION_CONTRIBUTERS_DELETE";
            }
            public class FixAssetInventory
            {
                public const string FixAssetInventoryCreate = "PERM_FIX_ASSET_INVENTORY_CREATE";
                public const string FixAssetInventoryIndex = "PERM_FIX_ASSET_INVENTORY_INDEX";
                public const string FixAssetInventoryUpdate = "PERM_FIX_ASSET_INVENTORY_UPDATE";
                public const string FixAssetInventoryDetails = "PERM_FIX_ASSET_INVENTORY_DETAILS";
            }
            public class Campaign
            {
                public const string CampaignCreate = "PERM_CAMPAIGN_CREATE";
                public const string CampaignIndex = "PERM_CAMPAIGN_INDEX";
                public const string CampaignDelete = "PERM_CAMPAIGN_DELETE";
                public const string CampaignUpdate = "PERM_CAMPAIGN_UPDATE";
                public const string CampaignDetails = "PERM_CAMPAIGN_DETAILS";
            }
            public class CampaignPart
            {
                public const string CampaignPartCreate = "PERM_CAMPAIGN_PART_CREATE";
                public const string CampaignPartIndex = "PERM_CAMPAIGN_PART_INDEX";
                public const string CampaignPartDelete = "PERM_CAMPAIGN_PART_DELETE";
                public const string CampaignPartUpdate = "PERM_CAMPAIGN_PART_UPDATE";
                public const string CampaignPartDetails = "PERM_CAMPAIGN_PART_DETAILS";
            }
            public class SparePartGuaranteeAuthorityNeed
            {
                public const string SparePartGuaranteeAuthorityNeedIndex = "PERM_SPARE_PART_GUARANTEE_AUTHORITY_NEED_INDEX";
                public const string SparePartGuaranteeAuthorityNeedDelete = "PERM_SPARE_PART_GUARANTEE_AUTHORITY_NEED_DELETE";
            }
            public class CampaignVehicle
            {
                public const string CampaignVehicleCreate = "PERM_CAMPAIGN_VEHICLE_CREATE";
                public const string CampaignVehicleIndex = "PERM_CAMPAIGN_VEHICLE_INDEX";
                public const string CampaignVehicleDelete = "PERM_CAMPAIGN_VEHICLE_DELETE";
                public const string CampaignVehicleUpdate = "PERM_CAMPAIGN_VEHICLE_UPDATE";
                public const string CampaignVehicleDetails = "PERM_CAMPAIGN_VEHICLE_DETAILS";
            }
            public class CampaignDocument
            {
                public const string CampaignDocumentCreate = "PERM_CAMPAIGN_DOCUMENT_CREATE";
                public const string CampaignDocumentIndex = "PERM_CAMPAIGN_DOCUMENT_INDEX";
                public const string CampaignDocumentDelete = "PERM_CAMPAIGN_DOCUMENT_DELETE";
                public const string CampaignDocumentUpdate = "PERM_CAMPAIGN_DOCUMENT_UPDATE";
                public const string CampaignDocumentDetails = "PERM_CAMPAIGN_DOCUMENT_DETAILS";
            }

            public class Defect
            {
                public const string DefectCreate = "PERM_DEFECT_CREATE";
                public const string DefectIndex = "PERM_DEFECT_INDEX";
                public const string DefectDelete = "PERM_DEFECT_DELETE";
                public const string DefectUpdate = "PERM_DEFECT_UPDATE";
                public const string DefectDetails = "PERM_DEFECT_DETAILS";
            }

            public class HolidayDate
            {
                public const string HolidayDateCreate = "PERM_HOLIDAY_DATE_CREATE";
                public const string HolidayDateIndex = "PERM_HOLIDAY_DATE_INDEX";
                public const string HolidayDateDelete = "PERM_HOLIDAY_DATE_DELETE";
                public const string HolidayDateUpdate = "PERM_HOLIDAY_DATE_UPDATE";
                public const string HolidayDateDetails = "PERM_HOLIDAY_DATE_DETAILS";
            }

            public class Contract
            {
                public const string ContractCreate = "PERM_CONTRACT_CREATE";
                public const string ContractIndex = "PERM_CONTRACT_INDEX";
                public const string ContractDelete = "PERM_CONTRACT_DELETE";
                public const string ContractUpdate = "PERM_CONTRACT_UPDATE";
                public const string ContractDetails = "PERM_CONTRACT_DETAILS";
            }

            public class ContractPart
            {
                public const string ContractPartCreate = "PERM_CONTRACT_PART_CREATE";
                public const string ContractPartIndex = "PERM_CONTRACT_PART_INDEX";
                public const string ContractPartDelete = "PERM_CONTRACT_PART_DELETE";
                public const string ContractPartUpdate = "PERM_CONTRACT_PART_UPDATE";
                public const string ContractPartDetails = "PERM_CONTRACT_PART_DETAILS";
            }
            public class CampaignLabour
            {
                public const string CampaignLabourCreate = "PERM_CAMPAIGN_LABOUR_CREATE";
                public const string CampaignLabourIndex = "PERM_CAMPAIGN_LABOUR_INDEX";
                public const string CampaignLabourDelete = "PERM_CAMPAIGN_LABOUR_DELETE";
                public const string CampaignLabourUpdate = "PERM_CAMPAIGN_LABOUR_UPDATE";
                public const string CampaignLabourDetails = "PERM_CAMPAIGN_LABOUR_DETAILS";
            }
            public class WorkHour
            {
                public const string WorkHourCreate = "PERM_WORK_HOUR_CREATE";
                public const string WorkHourIndex = "PERM_WORK_HOUR_INDEX";
                public const string WorkHourDelete = "PERM_WORK_HOUR_DELETE";
                public const string WorkHourUpdate = "PERM_WORK_HOUR_UPDATE";
                public const string WorkHourDetails = "PERM_WORK_HOUR_DETAILS";
            }
            public class WorkOrderDocuments
            {
                public const string WorkOrderDocumentIndex = "PERM_WORK_ORDER_DOCUMENTS_INDEX";
                public const string WorkOrderDocumentCreate = "PERM_WORK_ORDER_DOCUMENTS_CREATE";
                public const string WorkOrderDocumentDelete = "PERM_WORK_ORDER_DOCUMENTS_DELETE";
                public const string WorkOrderDocumentDownload = "PERM_WORK_ORDER_DOCUMENTS_DOWNLOAD";
            }
            public class ProposalDocuments
            {
                public const string ProposalDocumentIndex = "PERM_PROPOSAL_DOCUMENTS_INDEX";
                public const string ProposalDocumentCreate = "PERM_PROPOSAL_DOCUMENTS_CREATE";
                public const string ProposalDocumentDelete = "PERM_PROPOSAL_DOCUMENTS_DELETE";
                public const string ProposalDocumentDownload = "PERM_PROPOSAL_DOCUMENTS_DOWNLOAD";
            }
            public class AppointmentIndicatorMaint
            {
                public const string AppointmentIndicatorMaintIndex = "PERM_APPOINTMENT_INDICATOR_MAINT_INDEX";
                public const string AppointmentIndicatorMaintCreate = "PERM_APPOINTMENT_INDICATOR_MAINT_CREATE";
                public const string AppointmentIndicatorMaintDelete = "PERM_APPOINTMENT_INDICATOR_MAINT_DELETE";
                public const string AppointmentIndicatorMaintUpdate = "PERM_APPOINTMENT_INDICATOR_MAINT_UPDATE";
                public const string AppointmentIndicatorMaintPartChange = "PERM_APPOINTMENT_INDICATOR_MAINT_PARTCHANGE";
            }
            public class WorkOrderListInvoices
            {
                public const string WorkOrderListInvoicesIndex = "PERM_WORK_ORDER_LIST_INVOICES_INDEX";
            }
            public class CycleCount
            {
                public const string CycleCountIndex = "PERM_CYCLE_COUNT_INDEX";
                public const string CycleCountCreate = "PERM_CYCLE_COUNT_CREATE";
                public const string CycleCountUpdate = "PERM_CYCLE_COUNT_UPDATE";
                public const string CycleCountStart = "PERM_CYCLE_COUNT_START";
                public const string CycleCountCancel = "PERM_CYCLE_COUNT_CANCEL";
                public const string CycleCountApprove = "PERM_CYCLE_COUNT_APPROVE";
                public const string CycleCountWithoutChargeCampaign = "PERM_CYCLE_COUNT_WITHOUTCHARGECAMPAIGN";
            }

            public class CycleCountList
            {
                public const string CycleCountListIndex = "PERM_CYCLE_COUNT_LIST_INDEX";
            }

            public class CycleCountPlan
            {
                public const string CycleCountPlanIndex = "PERM_CYCLE_COUNT_PLAN_INDEX";
                public const string CycleCountPlanCreate = "PERM_CYCLE_COUNT_PLAN_CREATE";
                public const string CycleCountPlanDelete = "PERM_CYCLE_COUNT_PLAN_DELETE";
            }
            public class CycleCountResult
            {
                public const string CycleCountResultIndex = "PERM_CYCLE_COUNT_RESULT_INDEX";
                public const string CycleCountResultCreate = "PERM_CYCLE_COUNT_RESULT_CREATE";
                public const string CycleCountResultDelete = "PERM_CYCLE_COUNT_RESULT_DELETE";
                public const string CycleCountResultSave = "PERM_CYCLE_COUNT_RESULT_SAVE";
                public const string CycleCountResultSaveAndSendApprove = "PERM_CYCLE_COUNT_RESULT_SAVEANDSENDAPPROVE";
            }
            public class CycleCountStockDiff
            {
                public const string CycleCountStockDiffCreate = "PERM_CYCLE_COUNT_STOCK_DIFF_CREATE";
                public const string CycleCountStockDiffIndex = "PERM_CYCLE_COUNT_STOCK_DIFF_INDEX";
                public const string CycleCountStockDiffDelete = "PERM_CYCLE_COUNT_STOCK_DIFF_DELETE";
                public const string CycleCountStockDiffUpdate = "PERM_CYCLE_COUNT_STOCK_DIFF_UPDATE";
                public const string CycleCountStockDiffDetails = "PERM_CYCLE_COUNT_STOCK_DIFF_DETAILS";
                public const string CycleCountStockDiffSearch = "PERM_CYCLE_COUNT_STOCK_DIFF_SEARCH";
            }
            public class DamagedItemDispose
            {
                public const string DamagedItemDisposeCreate = "PERM_DAMAGED_ITEM_DISPOSE_CREATE";
                public const string DamagedItemDisposeIndex = "PERM_DAMAGED_ITEM_DISPOSE_INDEX";
                public const string DamagedItemDisposeDetails = "PERM_DAMAGED_ITEM_DISPOSE_DETAILS";
            }
            public class Scrap
            {
                public const string ScrapCreate = "PERM_SCRAP_CREATE";
                public const string ScrapIndex = "PERM_SCRAP_INDEX";
                public const string ScrapDetails = "PERM_SCRAP_DETAILS";
                public const string ScrapDelete = "PERM_SCRAP_DELETE";
                public const string ScrapUpdate = "PERM_SCRAP_UPDATE";
            }
            public class ScrapDetail
            {
                public const string ScrapDetailCreate = "PERM_SCRAP_DETAIL_CREATE";
                public const string ScrapDetailIndex = "PERM_SCRAP_DETAIL_INDEX";
                public const string ScrapDetailDetails = "PERM_SCRAP_DETAIL_DETAILS";
                public const string ScrapDetailDelete = "PERM_SCRAP_DETAIL_DELETE";
                public const string ScrapDetailUpdate = "PERM_SCRAP_DETAIL_UPDATE";
            }
            public class DealerStartupInventoryLevel
            {
                public const string DealerStartupInventoryLevelCreate = "PERM_DEALER_STARTUP_INVENTORY_LEVEL_CREATE";
                public const string DealerStartupInventoryLevelIndex = "PERM_DEALER_STARTUP_INVENTORY_LEVEL_INDEX";
                public const string DealerStartupInventoryLevelDelete = "PERM_DEALER_STARTUP_INVENTORY_LEVEL_DELETE";
                public const string DealerStartupInventoryLevelUpdate = "PERM_DEALER_STARTUP_INVENTORY_LEVEL_UPDATE";
                public const string DealerStartupInventoryLevelDetails = "PERM_DEALER_STARTUP_INVENTORY_LEVEL_DETAILS";
            }
            public class CriticalStockQuantity
            {
                public const string CriticalStockQuantityIndex = "PERM_CRITICAL_STOCK_QUANTITY_INDEX";
            }
            public class StockCard
            {
                public const string StockCardSearch = "PERM_STOCK_CARD_SEARCH";
                public const string StockCardIndex = "PERM_STOCK_CARD_INDEX";
                public const string StockCardCreate = "PERM_STOCK_CARD_CREATE";
                public const string StockCardUpdate = "PERM_STOCK_CARD_UPDATE";
                public const string StockCardDetails = "PERM_STOCK_CARD_DETAILS";
                public const string StockCardPriceList = "PERM_STOCK_CARD_PRICE_LIST";
            }
            public class VatRatioExp
            {
                public const string VatRatioExpIndex = "PERM_VAT_RATIO_EXPL_INDEX";
                public const string VatRatioExpCreate = "PERM_VAT_RATIO_EXPL_CREATE";
                public const string VatRatioExpDelete = "PERM_VAT_RATIO_EXPL_DELETE";
                public const string VatRatioExpUpdate = "PERM_VAT_RATIO_EXPL_UPDATE";
                public const string VatRatioExpDetails = "PERM_VAT_RATIO_EXPL_DETAILS";
            }
            public class AppointmentIndicatorMainCategory
            {
                public const string AppointmentIndicatorMainCategoryDelete = "PERM_APPOINTMENT_INDICATOR_MAIN_CATEGORY_DELETE";
                public const string AppointmentIndicatorMainCategoryIndex = "PERM_APPOINTMENT_INDICATOR_MAIN_CATEGORY_INDEX";
                public const string AppointmentIndicatorMainCategoryCreate = "PERM_APPOINTMENT_INDICATOR_MAIN_CATEGORY_CREATE";
                public const string AppointmentIndicatorMainCategoryUpdate = "PERM_APPOINTMENT_INDICATOR_MAIN_CATEGORY_UPDATE";
                public const string AppointmentIndicatorMainCategoryDetails = "PERM_APPOINTMENT_INDICATOR_MAIN_CATEGORY_DETAILS";
            }
            public class AppointmentIndicatorCategory
            {
                public const string AppointmentIndicatorCategoryDelete = "PERM_APPOINTMENT_INDICATOR_CATEGORY_DELETE";
                public const string AppointmentIndicatorCategoryIndex = "PERM_APPOINTMENT_INDICATOR_CATEGORY_INDEX";
                public const string AppointmentIndicatorCategoryCreate = "PERM_APPOINTMENT_INDICATOR_CATEGORY_CREATE";
                public const string AppointmentIndicatorCategoryUpdate = "PERM_APPOINTMENT_INDICATOR_CATEGORY_UPDATE";
                public const string AppointmentIndicatorCategoryDetails = "PERM_APPOINTMENT_INDICATOR_CATEGORY_DETAILS";
            }
            public class AppointmentIndicatorSubCategory
            {
                public const string AppointmentIndicatorSubCategoryDelete = "PERM_APPOINTMENT_INDICATOR_SUB_CATEGORY_DELETE";
                public const string AppointmentIndicatorSubCategoryIndex = "PERM_APPOINTMENT_INDICATOR_SUB_CATEGORY_INDEX";
                public const string AppointmentIndicatorSubCategoryCreate = "PERM_APPOINTMENT_INDICATOR_SUB_CATEGORY_CREATE";
                public const string AppointmentIndicatorSubCategoryUpdate = "PERM_APPOINTMENT_INDICATOR_SUB_CATEGORY_UPDATE";
                public const string AppointmentIndicatorSubCategoryDetails = "PERM_APPOINTMENT_INDICATOR_SUB_CATEGORY_DETAILS";
            }
            public class DealerSaleChannelDiscountRatio
            {
                public const string DealerSaleChannelDiscountRatioDelete = "PERM_DEALER_SALE_CHANNEL_DISCOUNT_RATIO_DELETE";
                public const string DealerSaleChannelDiscountRatioIndex = "PERM_DEALER_SALE_CHANNEL_DISCOUNT_RATIO_INDEX";
                public const string DealerSaleChannelDiscountRatioCreate = "PERM_DEALER_SALE_CHANNEL_DISCOUNT_RATIO_CREATE";
                public const string DealerSaleChannelDiscountRatioUpdate = "PERM_DEALER_SALE_CHANNEL_DISCOUNT_RATIO_UPDATE";
                public const string DealerSaleChannelDiscountRatioDetails = "PERM_DEALER_SALE_CHANNEL_DISCOUNT_RATIO_DETAILS";
            }

            public class UserRole
            {
                public const string UserRoleIndex = "PERM_USER_ROLE_INDEX";
                public const string UserRoleSave = "PERM_USER_ROLE_SAVE";
            }
            public class CountryVatRatio
            {
                public const string CountryVatRatioIndex = "PERM_COUNTRY_VAT_RATIO_INDEX";
                public const string CountryVatRatioCreate = "PERM_COUNTRY_VAT_RATIO_CREATE";
                public const string CountryVatRatioDelete = "PERM_COUNTRY_VAT_RATIO_DELETE";
                public const string CountryVatRatioUpdate = "PERM_COUNTRY_VAT_RATIO_UPDATE";
                public const string CountryVatRatioDetails = "PERM_COUNTRY_VAT_RATIO_DETAILS";
            }
            public class LabourCountryVatRatio
            {
                public const string LabourCountryVatRatioIndex = "PERM_LABOUR_COUNTRY_VAT_RATIO_INDEX";
                public const string LabourCountryVatRatioCreate = "PERM_LABOUR_COUNTRY_VAT_RATIO_CREATE";
                public const string LabourCountryVatRatioDelete = "PERM_LABOUR_COUNTRY_VAT_RATIO_DELETE";
                public const string LabourCountryVatRatioUpdate = "PERM_LABOUR_COUNTRY_VAT_RATIO_UPDATE";
                public const string LabourCountryVatRatioDetails = "PERM_LABOUR_COUNTRY_VAT_RATIO_DETAILS";
            }
            public class CampaignRequestApprove
            {
                public const string CampaignRequestApproveIndex = "PERM_CAMPAIGN_REQUEST_APPROVE_INDEX";
                public const string CampaignRequestApproveUpdate = "PERM_CAMPAIGN_REQUEST_APPROVE_UPDATE";
                public const string CampaignRequestApproveDetails = "PERM_CAMPAIGN_REQUEST_APPROVE_DETAILS";
            }
            public class ClaimRecallPeriod
            {
                public const string ClaimRecallPeriodIndex = "PERM_CLAIM_RECALL_PERIOD_INDEX";
                public const string ClaimRecallPeriodUpdate = "PERM_CLAIM_RECALL_PERIOD_UPDATE";
                public const string ClaimRecallPeriodDetails = "PERM_CLAIM_RECALL_PERIOD_DETAILS";
                public const string ClaimRecallPeriodCreate = "PERM_CLAIM_RECALL_PERIOD_CREATE";
            }
            public class ClaimRecallPeriodPart
            {
                public const string ClaimRecallPeriodPartIndex = "PERM_CLAIM_RECALL_PERIOD_PART_INDEX";
                public const string ClaimRecallPeriodPartCreate = "PERM_CLAIM_RECALL_PERIOD_PART_CREATE";
                public const string ClaimRecallPeriodPartUpdate = "PERM_CLAIM_RECALL_PERIOD_PART_UPDATE";
                public const string ClaimRecallPeriodPartDelete = "PERM_CLAIM_RECALL_PERIOD_PART_DELETE";
            }
            public class ClaimSupplierPart
            {
                public const string ClaimSupplierPartIndex = "PERM_CLAIM_SUPPLIER_PART_INDEX";
                public const string ClaimSupplierPartCreate = "PERM_CLAIM_SUPPLIER_PART_CREATE";
                public const string ClaimSupplierPartDelete = "PERM_CLAIM_SUPPLIER_PART_DELETE";
                public const string ClaimSupplierPartUpdate = "PERM_CLAIM_SUPPLIER_PART_UPDATE";
            }

            public class SupplierDispatchPart
            {
                public const string SupplierDispatchPartIndex = "PERM_SUPPLIER_DISPATCH_PART_INDEX";
                public const string SupplierDispatchPartList = "PERM_SUPPLIER_DISPATCH_PART_SELECT";
                public const string SupplierDispatchPartCreate = "PERM_SUPPLIER_DISPATCH_PART_CREATE";
                public const string SupplierDispatchPartUpdate = "PERM_SUPPLIER_DISPATCH_PART_UPDATE";
                public const string SupplierDispatchPartDelete = "PERM_SUPPLIER_DISPATCH_PART_DELETE";
            }

            public class PurchaseOrderDetail
            {
                public const string PurchaseOrderDetailCreate = "PERM_PURCHASE_ORDER_DETAIL_CREATE";
                public const string PurchaseOrderDetailIndex = "PERM_PURCHASE_ORDER_DETAIL_INDEX";
                public const string PurchaseOrderDetailDelete = "PERM_PURCHASE_ORDER_DETAIL_DELETE";
                public const string PurchaseOrderDetailUpdate = "PERM_PURCHASE_ORDER_DETAIL_UPDATE";
                public const string PurchaseOrderDetailDetails = "PERM_PURCHASE_ORDER_DETAIL_DETAILS";
                public const string PurchaseOrderInquiryIndex = "PERM_PURCHASE_ORDER_INQUIRY_INDEX";
                public const string PurchaseOrderDetailCancel = "PERM_PURCHASE_ORDER_DETAIL_CANCEL";
            }
            public class Announcement
            {
                public const string AnnouncementCreate = "PERM_ANNOUNCEMENT_CREATE";
                public const string AnnouncementIndex = "PERM_ANNOUNCEMENT_INDEX";
                public const string AnnouncementDelete = "PERM_ANNOUNCEMENT_DELETE";
                public const string AnnouncementUpdate = "PERM_ANNOUNCEMENT_UPDATE";
                public const string AnnouncementDetails = "PERM_ANNOUNCEMENT_DETAILS";
            }
            public class DealerPurchaseOrder
            {
                public const string DealerPurchaseOrderIndex = "PERM_DEALER_PURCHASE_ORDER_INDEX";
                public const string DealerPurchaseOrderUpdate = "PERM_DEALER_PURCHASE_ORDER_UPDATE";
                public const string DealerPurchaseOrderCreate = "PERM_DEALER_PURCHASE_ORDER_CREATE";
                public const string DealerPurchaseOrderPartCreate = "PERM_DEALER_PURCHASE_ORDER_PART_CREATE";
                public const string DealerPurchaseOrderPartDelete = "PERM_DEALER_PURCHASE_ORDER_PART_DELETE";
                public const string DealerPurchaseOrderComplete = "PERM_DEALER_PURCHASE_ORDER_COMPLETE";
            }
            public class BodyworkDetail
            {
                public const string BodyworkDetailIndex = "PERM_BODYWORK_DETAIL_INDEX";
                public const string BodyworkDetailUpdate = "PERM_BODYWORK_DETAIL_UPDATE";
                public const string BodyworkDetailCreate = "PERM_BODYWORK_DETAIL_CREATE";
                public const string BodyworkDetailDelete = "PERM_BODYWORK_DETAIL_DELETE";
            }
            public class WorkshopType
            {
                public const string WorkshopTypeIndex = "PERM_WORKSHOP_TYPE_INDEX";
                public const string WorkshopTypeUpdate = "PERM_WORKSHOP_TYPE_UPDATE";
                public const string WorkshopTypeCreate = "PERM_WORKSHOP_TYPE_CREATE";
                public const string WorkshopTypeDelete = "PERM_WORKSHOP_TYPE_DELETE";
            }
            public class LabourTechnician
            {
                public const string LabourTechnicianIndex = "PERM_LABOUR_TECHNICIAN_INDEX";
                public const string LabourTechnicianUpdate = "PERM_LABOUR_TECHNICIAN_UPDATE";
                public const string LabourTechnicianDetails = "PERM_LABOUR_TECHNICIAN_DETAILS";
            }
            public class TechnicianOperation
            {
                public const string TechnicianOperationIndex = "PERM_TECHNICIAN_OPERATION_INDEX";
                public const string TechnicianOperationLogin = "PERM_TECHNICIAN_OPERATION_LOGIN";
            }
            public class CampaignDismantlePart
            {
                public const string CampaignDismantlePartIndex = "PERM_CAMPAIGN_DISMANTLE_PART_INDEX";
                public const string CampaignDismantlePartCreate = "PERM_CAMPAIGN_DISMANTLE_PART_CREATE";
                public const string CampaignDismantlePartUpdate = "PERM_CAMPAIGN_DISMANTLE_PART_UPDATE";
                public const string CampaignDismantlePartDelete = "PERM_CAMPAIGN_DISMANTLE_PART_DELETE";
                public const string CampaignDismantlePartDetails = "PERM_CAMPAIGN_DISMANTLE_PART_DETAILS";
            }

            /// <summary>
            /// Garanti işlemlerinde Muadil parça kullanılması durumunda uygulanacak marj oranlarının
            /// tanımlandığı ekran ile ilgili yetki kodları..
            /// </summary>
            public class GuaranteeCompPartMargin
            {
                public const string GuaranteeCompPartMarginIndex = "PERM_GUARANTEE_COMP_PART_MARGIN_INDEX";
                public const string GuaranteeCompPartMarginCreate = "PERM_GUARANTEE_COMP_PART_MARGIN_CREATE";
                public const string GuaranteeCompPartMarginUpdate = "PERM_GUARANTEE_COMP_PART_MARGIN_UPDATE";
                public const string GuaranteeCompPartMarginDelete = "PERM_GUARANTEE_COMP_PART_MARGIN_DELETE";
            }

            /// <summary>
            /// Garanti işlemlerinde Muadil parça kullanılması durumunda uygulanacak marj oranlarının
            /// tanımlandığı ekran ile ilgili yetki kodları..
            /// </summary>
            public class GuaranteeCompLabourMargin
            {
                public const string GuaranteeCompLabourMarginIndex = "PERM_GUARANTEE_COMP_LABOUR_MARGIN_INDEX";
                public const string GuaranteeCompLabourMarginCreate = "PERM_GUARANTEE_COMP_LABOUR_MARGIN_CREATE";
                public const string GuaranteeCompLabourMarginUpdate = "PERM_GUARANTEE_COMP_LABOUR_MARGIN_UPDATE";
                public const string GuaranteeCompLabourMarginDelete = "PERM_GUARANTEE_COMP_LABOUR_MARGIN_DELETE";
            }

            public class WorkOrderPicking
            {
                public const string WorkOrderPickingIndex = "PERM_WORK_ORDER_PICKING_INDEX";
            }
            public class StockCardTransaction
            {
                public const string StockCardTransactionIndex = "PERM_STOCK_CARD_TRANSACTION_INDEX";
            }

            public class StockCardChangePart
            {
                public const string StockCardChangePartIndex = "PERM_STOCK_CARD_CHANGE_PART_INDEX";
            }
            public class StockCardPurchaseOrder
            {
                public const string StockCardPurchaseOrderIndex = "PERM_STOCK_CARD_PURCHASE_ORDER_INDEX";
            }

            public class DeliveryGoodsPlacement
            {
                public const string DeliveryGoodsPlacementIndex = "PERM_DELIVERY_GOODS_PLACEMENT_INDEX";
                public const string DeliveryGoodsPlacementPartsIndex = "PERM_DELIVERY_GOODS_PLACEMENT_PARTS_INDEX";
                public const string DeliveryGoodsPlacementPartsAction = "PERM_DELIVERY_GOODS_PLACEMENT_PARTS_ACTION";
            }
            public class PurchaseOrderSuggestionDetail
            {
                public const string PurchaseOrderSuggestionDetailIndex = "PERM_PURCHASE_ORDER_SUGGESTION_DETAIL_INDEX";
                public const string PurchaseOrderSuggestionDetailAction = "PERM_PURCHASE_ORDER_SUGGESTION_DETAIL_ACTION";
            }
            public class PriceList
            {
                public const string PriceListIndex = "PERM_PRICE_LIST_INDEX";
            }

            public class ClaimPeriodPartListApprove
            {
                public const string ClaimPeriodPartListApproveIndex = "PERM_CLAIM_PART_LIST_APPROVE_INDEX";
                public const string ClaimPeriodPartListApprovement = "PERM_CLAIM_PART_LIST_APPROVEMENT";
            }

            public class Catalog
            {
                public const string CatalogIndex = "PERM_CATALOG_ACCESS";
            }
        }

        public enum DeliveryStatus
        {
            NewRecord = -1,
            NotReceived = 0,
            StartToReceive = 1,
            ReceivedPartially = 2,
            ReceivedCompletely = 3,
            Cancelled = 9
        }

        public enum SupplierDealerConfirmType
        {

            NewProposal = 0,
            NotApprovedProposal = 1,
            ApprovedProposal = 2,
            NotApprovedOrder = 3,
            ApprovedOrder = 4
        }
        public enum CampaignRequestStatus
        {
            NewRecord = 0,
            WaitingForApproval = 1,
            Approved = 2,
            Cancelled = 3
        }

        public enum FixAssetStatus
        {
            Disposed = -1,
            FixInventory = 1,
            InPartStock = 2
        }

        public enum Status
        {
            Active = 1,
            Passive = 9
        }

        public enum YesNo
        {
            Yes = 1,
            No = 0
        }
        public enum ActionType
        {
            S,
            W
        }
        public enum StockCardPriceType
        {
            C,
            D,
            L
        }
        public enum FleetRequestStatus
        {
            FleetRequestRejected = -1,
            NewRecord = 0,
            FleetRequestApproved = 1,
            FleetRequestWaitingForApproval = 2
        }
        public enum CycleCountStatus
        {
            Planning = 0,
            Started = 1,
            Finished = 2,
            Approved = 3,
            Cancelled = 4
        }

        public enum SparePartSaleStatus
        {
            NewRecord = 0,
            CollectOrderCreated = 1,
            OrderCollected = 2,
            OrderWaybilled = 3,
            OrderInvoiced = 4,
            OrderCancelled = 5
        }

        public enum SparePartSaleDetailStatus
        {
            NoCollectOrder = 1,
            CollectOrderCreated = 2,
            Collected = 3
        }

        public enum PurchaseOrderStatus
        {
            NewRecord = 0,
            OpenPurchaseOrder = 1,
            ClosePurchaseOrder = 2,
            CanceledPurhcaseOrder = 9
        }
        public enum PurchaseOrderType
        {
            Urgent = 7,
            Normal = 6,
            Campaign = 8,
            RedirectedUrgent = 121,
            Contract = 145
        }
        public enum SparePartSaleOrderStatus
        {
            NotApprovedProposal = 0,
            ApprovedProposal = 1,
            NewRecord = 2,
            NotApprovedOrder = 3,
            ApprovedOrder = 4,
            ClosedOrder = 5,
            CancelledOrder = 9
        }
        public enum SparePartSaleOrderDetailStatus
        {
            OpenOrder = 0,
            ClosedOrder = 1,
            CancelledOrder = 9
        }
        public static class PurchaseOrderDMLType
        {
            public static string Complete = "C";
            public static string RollBack = "R";
        }

        public static class DMLType
        {
            public static string Insert = "I";
            public static string Update = "U";
            public static string Delete = "D";
            public static string Erase = "E";
            public static string Passive = "P";
        }

        public static class LogType
        {
            public static string Request = "REQ";
            public static string Response = "RES";
        }
        public static class StockBlockStatus
        {
            public static int PreparedToBlock = 0;
            public static int Blocked = 1;
            public static int BlockRemoved = 2;
        }

        public static class BlockType
        {
            public static string RemoveBlock = "R";
            public static string Block = "B";
        }

        public static class DefaultRackType
        {
            public static string DefaultRack = "D";
            public static string Clear = "C";
        }

        public static class GridPageSize
        {
            public static int Short = 10;
            public static int Long = 20;
        }
        public enum SaleType
        {
            NormalSale = 1,
            TenderSale = 2,
            PrivateSale = 3
        }
        public enum CustomerType
        {
            Tuzel = 1,
            Gercek = 2,
            Kamu = 3
        }
        public enum StockType
        {
            Bedelli = 1,
            Bedelsiz = 2,
            Kampanya = 3
        }
        public enum CustomerAddressType
        {
            Is = 1,
            Ev = 2,
            FaturaAdres = 3
        }

        public enum SupplyPort
        {
            Otokar = 1,
            Supplier = 2,
            DealerService = 3
        }
        public enum AppointmentStatus
        {
            Created = 1,
            Linked = 2,
            Cancelled = 3
        }
    }

}
