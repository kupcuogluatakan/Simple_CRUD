using System;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;

namespace ODMSModel.DealerGuaranteeControl
{
    public class DealerGuaranteeControlListItem
    {
        public long GuaranteeId { get; set; }
        public long GuaranteeSeq { get; set; }
        [Display(Name = "Vehicle_Display_WarrantyStatus", ResourceType = typeof(MessageResource))]
        public string WarrantyStatus { get; set; }
        [Display(Name = "FleetRequestApprove_Display_RequestDescription", ResourceType = typeof(MessageResource))]
        public string RequestDescription { get; set; }
        [Display(Name = "Global_Display_Category", ResourceType = typeof(MessageResource))]
        public string Category { get; set; }
        [Display(Name = "Global_Display_ProcessType", ResourceType = typeof(MessageResource))]
        public string ProcessType { get; set; }
        [Display(Name = "Global_Display_IndicatorType", ResourceType = typeof(MessageResource))]
        public string IndicatorType { get; set; }
        [Display(Name = "WorkOrderPartHistoryReport_Display_WorkOrderId", ResourceType = typeof(MessageResource))]
        public long WorkOrderId { get; set; }
        [Display(Name = "WorkOrderBatchInvoice_Display_WorkOrderDetailId", ResourceType = typeof(MessageResource))]
        public long WorkOrderDetailId { get; set; }
        [Display(Name = "Vehicle_Display_VinNo", ResourceType = typeof(MessageResource))]
        public string VinNo { get; set; }
        [Display(Name = "GuaranteeRequestApprove_Display_Permission", ResourceType = typeof(MessageResource))]
        public bool IsPerm { get; set; }
        [Display(Name = "GuaranteeRequestApprove_Display_Permission", ResourceType = typeof(MessageResource))]
        public string IsPermString { get; set; }
        [Display(Name = "AppointmentIndicatorFailureCode_Display_Code", ResourceType = typeof(MessageResource))]
        public string FailCode { get; set; }
        public string FailCodeDesc { get; set; }
        [Display(Name = "Global_Display_RequestDate", ResourceType = typeof(MessageResource))]
        public DateTime RequestDate { get; set; }
        [Display(Name = "Global_Display_ApproveDate", ResourceType = typeof(MessageResource))]
        public DateTime? ApproveDate { get; set; }
        [Display(Name = "GRADGif_Display_GifNo", ResourceType = typeof(MessageResource))]
        public long? GifNo { get; set; }
        public int VehicleId { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_DealerName", ResourceType = typeof(MessageResource))]
        public string Dealer { get; set; }
        public string ConfirmDesc { get; set; }
    }
}
