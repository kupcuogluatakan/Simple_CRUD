using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using System.Data;

namespace ODMSModel.CycleCountResult
{
    [Validator(typeof(CycleCountResultViewModelValidator))]
    public class CycleCountResultViewModel : ModelBase
    {
        public CycleCountResultViewModel()
        {
        }
        public bool HideFormElements { get; set; }
        [Display(Name = "CampaignPart_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }

        [UIHint("DecimalNumericTextbox")]
        [Display(Name = "CycleCountResult_Display_AfterCountQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal AfterCountQuantityNonNull
        {
            get { return AfterCountQuantity.HasValue ? AfterCountQuantity.Value : -1; }
            set { AfterCountQuantity = value; }
        }
        //CycleCountResultId
        public int CycleCountResultId { get; set; }

        //CycleCountId
        public int CycleCountId { get; set; }

        //Warehouse
        public int? WarehouseId { get; set; }
        [Display(Name = "CycleCountResult_Display_WarehouseName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WarehouseName { get; set; }

        //Rack
        public int? RackId { get; set; }
        [Display(Name = "CycleCountResult_Display_RackName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RackName { get; set; }

        //StockCard        
        public int? StockCardId { get; set; }

        [Display(Name = "CycleCountResult_Display_StockCard", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StockCardName { get; set; }

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

        [Display(Name = "CycleCountResult_Display_ApprovedCountQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal ApprovedCountQuantity { get; set; }

        [Display(Name = "CycleCountResult_Display_RejectDescription", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RejectDescription { get; set; }

        public int CountUser { get; set; }
        [Display(Name = "CycleCountResult_Display_CountUserName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CountUserName { get; set; }

        public int ApproveUser { get; set; }
        [Display(Name = "CycleCountResult_Display_ApproveUserName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ApproveUserName { get; set; }

        public DataTable ExcelList { get; set; }
    }
}
