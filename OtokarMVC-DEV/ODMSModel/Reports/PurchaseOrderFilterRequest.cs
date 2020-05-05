using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.Reports
{
    public class PurchaseOrderFilterRequest : ReportListModelBase
    {
        public PurchaseOrderFilterRequest([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {

            SetMapper(null);
        }

        public PurchaseOrderFilterRequest() { }
        [Display(Name = "WorkOrderDetailReport_Display_DealerIdList",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerIdList { get; set; }
        [Display(Name = "WorkOrderDetailReport_Display_DealerRegionIdList",
           ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerRegionIdList { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_SupplierIdList",
           ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SupplierIdList { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_PurchaseOrderStartDate",
           ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? PurchaseOrderStartDate { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_PurchaseOrderEndDate",
          ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? PurchaseOrderEndDate { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_DeliveryStartDate",
           ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? DeliveryStartDate { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_DeliveryEndDate",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? DeliveryEndDate { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_InvoiceStartDate",
           ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? InvoiceStartDate { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_InvoiceEndDate",
           ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? InvoiceEndDate { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_PartCode",
         ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_IsOriginal",
        ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IsOriginal { get; set; }
        [Display(Name = "PartStockReport_StockType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StockTypeId { get; set; }
        [Display(Name = "PartStockReport_Display_PoLocation", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? PoLocation { get; set; }

    }
}
