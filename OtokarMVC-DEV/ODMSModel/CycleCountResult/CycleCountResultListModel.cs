using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.CycleCountResult
{
    public class CycleCountResultListModel : BaseListWithPagingModel
    {
        public CycleCountResultListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"CycleCountResultId", "ID_CYCLE_COUNT_RESULT"},
                    {"CycleCountId", "ID_CYCLE_COUNT"},
                    {"WarehouseId", "ID_WAREHOUSE"},
                    {"WarehouseName", "WAREHOUSE_NAME"},
                    {"RackId", "ID_RACK"},
                    {"RackName", "RACK_NAME"},
                    {"StockCardId", "ID_STOCK_CARD"},
                    {"StockCardName", "STOCK_CARD_NAME"},
                       {"BeforeFreeOfChargeCountQuantity", "BEFORE_FREEOFCHARGE_COUNT_QUANTITY"},
                          {"BeforePaidCountQuantity", "BEFORE_PAID_COUNT_QUANTITY"},
                             {"BeforeCampaignCountQuantity", "BEFORE_CAMPAIGN_COUNT_QUANTITY"},
                    {"BeforeCountQuantity", "BEFORE_COUNT_QUANTITY"},
                    {"AfterCountQuantity", "AFTER_COUNT_QUANTITY"},
                    {"AfterCountQuantityNonNull", "AFTER_COUNT_QUANTITY"},
                    {"ApprovedCountQuantity", "APPROVED_COUNT_QUANTITY"},
                    {"RejectDescription", "REJECT_DESC"},
                    {"CountUser", "COUNT_USER"},
                    {"ApproveUser", "APPROVE_USER"},
                    {"PartCode","PART_CODE"}
                };
            SetMapper(dMapper);
        }

        public CycleCountResultListModel()
        {
        }

        public int CycleCountResultId { get; set; }

        public int CycleCountId { get; set; }

        public int WarehouseId { get; set; }
        [Display(Name = "CycleCountResult_Display_WarehouseName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WarehouseName { get; set; }

        public int? RackId { get; set; }
        [Display(Name = "CycleCountResult_Display_RackName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RackName { get; set; }

        public int IS_ONRACK { get; set; }

        public int? StockCardId { get; set; }//Real value is PartId
        [Display(Name = "CycleCountResult_Display_StockCard", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StockCardName { get; set; }
        
        [Display(Name = "CampaignPart_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }

        [Display(Name = "CycleCountResult_Display_BeforeFreeOfChargeCountQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal BeforeFreeOfChargeCountQuantity { get; set; }

        [Display(Name = "CycleCountResult_Display_BeforePaidCountQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal BeforePaidCountQuantity { get; set; }

        [Display(Name = "CycleCountResult_Display_BeforeCampaignCountQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal BeforeCampaignCountQuantity { get; set; }

        [Display(Name = "CycleCountResult_Display_BeforeCountQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal BeforeCountQuantity { get; set; }

        [Display(Name = "CycleCountResult_Display_AfterCountQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? AfterCountQuantity { get; set; }

        [UIHint("DecimalNumericTextbox")]
        [Display(Name = "CycleCountResult_Display_AfterCountQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal AfterCountQuantityNonNull
        {
            get { return AfterCountQuantity.HasValue ? AfterCountQuantity.Value : -1; }
            set { AfterCountQuantity = value; }
        }

        [UIHint("DecimalNumericTextbox")]
        [Display(Name = "CycleCountResult_Display_ApprovedCountQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal ApprovedCountQuantity { get; set; }

        public int CountUser { get; set; }
        [Display(Name = "CycleCountResult_Display_CountUserName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CountUserName { get; set; }

        [Display(Name = "CycleCountResult_Display_RejectDescription", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RejectDescription { get; set; }

        public StockState QtyState { get; set; }

        public int RealStockCardId { get; set; }

        public decimal? TotalQty { get; set; }

        public decimal TotalApprovedQty { get; set; }

        public decimal DetailTotalQty { get; set; }

        public int? CycleCountStatus { get; set; }

        public bool PageFlag { get; set; }

        [Display(Name = "CycleCountResult_Display_StockDiffQty", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StockDiffQty { get; set; }

        public decimal Price { get; set; }

        public string CurrencyCode { get; set; }

        [Display(Name = "CycleCountResult_Display_Unit", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Unit { get; set; }

        [Display(Name = "CycleCountResult_Display_Price", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PriceString { get; set; }
        
        public decimal BeforeCycleCountValue { get; set; }
    }

    public enum StockState
    {
        Green = 0,

        Yellow = 1,

        Red = 2
    }
}
