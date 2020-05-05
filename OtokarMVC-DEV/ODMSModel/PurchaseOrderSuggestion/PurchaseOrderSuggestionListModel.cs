using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.PurchaseOrderSuggestion
{
    public class PurchaseOrderSuggestionListModel : BaseListWithPagingModel
    {
        public PurchaseOrderSuggestionListModel([DataSourceRequest] DataSourceRequest request) : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"IsAuto", "IS_AUTO"},
                    {"IsAutoName", "IS_AUTO_NAME"},
                    {"PoNumber", "PO_NUMBER"},
                    {"PlanDate","PLAN_DATE"},
                    {"PoDate", "PO_DATE"},
                    {"IsActive", "IS_ACTIVE"},
                    {"IsActiveName", "IS_ACTIVE_NAME"}
                };
            SetMapper(dMapper);
        }

        public PurchaseOrderSuggestionListModel()
        {
        }

        public Int64? IdMrp { get; set; }

        [Display(Name = "PurchaseOrderSuggestion_Display_Dealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int32? IdDealer { get; set; }
        //PoNumber
        [Display(Name = "PurchaseOrder_Display_PoNumber", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int64? PoNumber { get; set; }

        [Display(Name = "PurchaseOrderSuggestion_Display_PoDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? PoDate { get; set; }

        [Display(Name = "PurchaseOrderSuggestion_Display_PlanDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? PlanDate { get; set; }

        [Display(Name = "PurchaseOrderSuggestion_Display_PlanDateStart", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? PlanDateStart { get; set; }

        [Display(Name = "PurchaseOrderSuggestion_Display_PlanDateEnd", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? PlanDateEnd { get; set; }
      
        [Display(Name = "Global_Display_IsAuto", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public bool? IsAuto { get; set; }
        [Display(Name = "Global_Display_IsAuto", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsAutoName { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public bool? IsActive { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsActiveName { get; set; }

        [Display(Name = "PurchaseOrderSuggestion_Display_PurchaseOrderRate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? PurchaseOrderRate { get; set; }

        [Display(Name = "PurchaseOrderSuggestion_Display_IsPoCreate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsPoCreate { get; set; }
    }
}
