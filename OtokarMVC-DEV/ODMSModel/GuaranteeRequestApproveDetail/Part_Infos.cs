using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.GuaranteeRequestApproveDetail
{
    public class Part_Infos
    {
        [Display(Name = "ClaimDismantledPartDelivery_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }
        [Display(Name = "ClaimDismantledPartDelivery_Display_Qty", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal Quantity { get; set; }
        [Display(Name = "GuaranteeReport_VinNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VinNo { get; set; }
        [Display(Name = "Maintenance_Display_Km", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Km { get; set; }
        [Display(Name = "SaleOrderRemaining_Display_CustomerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerName { get; set; }
        [Display(Name = "SaleReport_Display_WorkOrderId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WorkOrderId { get; set; }
        [Display(Name = "WorkOrderBatchInvoice_Display_WorkOrderDetailId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WorkOrderDetId { get; set; }
        [Display(Name = "WorkOrderPerformanceReport_GifNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string GuaranteeId { get; set; }
        [Display(Name = "AppointmentIndicatorFailureCode_Display_Description", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string GuaranteeDesc { get; set; }
    }
}
