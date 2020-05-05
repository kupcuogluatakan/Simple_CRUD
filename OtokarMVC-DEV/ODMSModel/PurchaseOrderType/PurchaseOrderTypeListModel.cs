using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.PurchaseOrderType
{
    public class PurchaseOrderTypeListModel : BaseListWithPagingModel
    {        
        public PurchaseOrderTypeListModel([DataSourceRequest] DataSourceRequest request) : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"PurchaseOrderTypeName", "PURCHASE_ORDER_TYPE_NAME"},
                    {"Description", "ADMIN_DESC"},
                    {"DeliveryPriority", "DELIVERY_PRIORITY"},
                    {"ProposalType", "PROPOSAL_TYPE"},
                    {"StateName", "IS_ACTIVE"},
                    {"CurrencyName", "CURRENCY_NAME"},
                    {"StockTypeName","MAINT_NAME"}
                };
            SetMapper(dMapper);
        }

        public PurchaseOrderTypeListModel()
        {
                       
        }

        public int PurchaseOrderTypeId { get; set; }

        [Display(Name = "PurchaseOrderType_Display_PurchaseOrderTypeName", ResourceType = typeof(MessageResource))]
        public string PurchaseOrderTypeName { get; set; }

        [Display(Name = "PurchaseOrderType_Display_Description", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Description { get; set; }

        [Display(Name = "PurchaseOrderType_Display_ProposalType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ProposalType { get; set; }

        [Display(Name = "PurchaseOrderType_Display_DeliveryPriority", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int DeliveryPriority { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public int? IsActive { get; set; }

        [Display(Name = "PurchaseOrderType_Display_StateName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StateName { get; set; }

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
    }
}
