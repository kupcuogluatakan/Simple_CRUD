using System;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.WorkOrderPicking
{
    public class WorkOrderPickingViewModel : ModelBase
    {
        public Int64 WorkOrderPickingId { get; set; }

        public long WorkOrderId { get; set; }

        [Display(Name = "WorkOrderPicking_Display_Plate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WorkOrderPlate { get; set; }

        [Display(Name = "WorkOrderPicking_Display_Status", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public new int? StatusId { get; set; }

        [Display(Name = "WorkOrderPicking_Display_Status", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StatusIds { get; set; }
        [Display(Name = "WorkOrderPicking_Display_Status", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public new string Status { get; set; }

        [Display(Name = "Global_Display_CreateDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime CreateDate { get; set; }

        //Just Search Criteria
        [Display(Name = "WorkOrderPicking_Display_Return", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IsReturn { get; set; }

        [Display(Name = "Global_Display_StartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Global_Display_EndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EndDate { get; set; }

        public int? PartSaleId { get; set; }

        [Display(Name = "WorkOrderPicking_Display_No", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int64? No { get; set; }

        [Display(Name = "Appointment_Display_Customer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? CustomerId { get; set; }

        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        [Display(Name = "SparePart_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }

        [Display(Name = "WorkOrderPicking_Display_OrderSource", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? PickSource { get; set; }

        public int DealerId { get; set; }
    }
}
