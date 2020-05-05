using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;
using ODMSModel.ViewModel;

namespace ODMSModel.PurchaseOrderType
{
    [Validator(typeof(PurchaseOrderTypeViewModelValidator))]
    public class PurchaseOrderTypeViewModel : ModelBase
    {
        public PurchaseOrderTypeViewModel()
        {
            MultiLanguageName = new MultiLanguageModel();
        }

        public int PurchaseOrderTypeId { get; set; }

        [Display(Name = "PurchaseOrderType_Display_PurchaseOrderTypeName", ResourceType = typeof(MessageResource))]
        public string PurchaseOrderTypeName { get; set; }

        [Display(Name = "PurchaseOrderType_Display_Description", ResourceType = typeof(MessageResource))]
        public string Description { get; set; }

        [Display(Name = "PurchaseOrderType_Display_ProposalType", ResourceType = typeof(MessageResource))]
        public string ProposalType { get; set; }

        [Display(Name = "PurchaseOrderType_Display_DeliveryPriority", ResourceType = typeof(MessageResource))]
        public int DeliveryPriority { get; set; }

        [Display(Name = "PurchaseOrderType_Display_SalesOrganization", ResourceType = typeof(MessageResource))]
        public string SalesOrganization { get; set; }

        [Display(Name = "PurchaseOrderType_Display_OrderReason", ResourceType = typeof(MessageResource))]
        public string OrderReason { get; set; }

        [Display(Name = "PurchaseOrderType_Display_ItemCategory", ResourceType = typeof(MessageResource))]
        public string ItemCategory { get; set; }

        [Display(Name = "PurchaseOrderType_Display_DistrChan", ResourceType = typeof(MessageResource))]
        public string DistrChan { get; set; }

        [Display(Name = "PurchaseOrderType_Display_Division", ResourceType = typeof(MessageResource))]
        public string Division { get; set; }

        public string MultiLanguageContentAsText { get; set; }

        private MultiLanguageModel _multiLangName;

        [Display(Name = "EducationType_Display_Name", ResourceType = typeof(MessageResource))]
        public MultiLanguageModel MultiLanguageName
        {
            get { return _multiLangName ?? new MultiLanguageModel(); }
            set { _multiLangName = value; }
        }

        [Display(Name = "PurchaseOrderType_Display_Is_Vehicle_Selection_Must", ResourceType = typeof(MessageResource))]
        public bool IsVehicleSelectionMust { get; set; }

        [Display(Name = "PurchaseOrderType_Display_ManuelPriceAllow", ResourceType = typeof(MessageResource))]
        public bool ManuelPriceAllow { get; set; }

        public string DealerBranchSSID { get; set; }
        
        [Display(Name = "PurchaseOrderType_Display_IsCurrencySelectAllow", ResourceType = typeof(MessageResource))]
        public bool IsCurrencySelectAllow { get; set; }
        [Display(Name = "PurchaseOrderType_Display_IsFirmOrderAllow", ResourceType = typeof(MessageResource))]
        public bool IsFirmOrderAllow { get; set; }
        [Display(Name = "PurchaseOrderType_Display_IsDealerOrderAllow", ResourceType = typeof(MessageResource))]
        public bool IsDealerOrderAllow { get; set; }
        [Display(Name = "PurchaseOrderType_Display_IsSupplierOrderAllow", ResourceType = typeof(MessageResource))]
        public bool IsSupplierOrderAllow { get; set; }
        [Display(Name = "PurchaseOrderType_Display_StockType", ResourceType = typeof(MessageResource))]
        public int? StockTypeId { get; set; }
        [Display(Name = "PurchaseOrderType_Display_StockType", ResourceType = typeof(MessageResource))]
        public string StockTypeName { get; set; }
        [Display(Name = "PurchaseOrderType_Display_IsSaleOrderAllow", ResourceType = typeof(MessageResource))]
        public bool IsSaleOrderAllow { get; set; }
    }
}
