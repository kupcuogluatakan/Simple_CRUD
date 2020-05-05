using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Reports
{
    public class WorkOrderInfoModel
    {

        //[Display(Name = "WorkOrderDetailReport_Display_DealerIdList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        //public string DealerIdList { get; set; }
        [Display(Name = "CarServiceDurationReport_Display_GroupName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }

        public string GroupName { get; set; }

        [Display(Name = "Sorgulama Kriteri")]
        public int GroupType { get; set; }
        public string GroupCode { get; set; }
        [Display(Name = "CustomerAddress_Display_CustomerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public long? CustomerId { get; set; }
        [Display(Name = "WorkOrderBatchInvoice_Display_WorkOrderId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public long WorkOrderId { get; set; }

        //public int TotalWorkOrderNumber { get; set; }

        [Display(Name = "Vehicle_Display_VinNo",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VinNo { get; set; }

        [Display(Name = "Vehicle_Display_WarrantyEndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? WarrantyEndDate { get; set; }
        [Display(Name = "Vehicle_Display_WarrantyStartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? WarrantyStartDate { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_WorkOrderDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_ClosedDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? CloseDate { get; set; }

        [Display(Name = "WorkOrderCard_Dislay_VehicleLeaveDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EndDate { get; set; }
        [Display(Name = "CarServiceDurationReport_Display_TotalWaitingHour", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal TotalWaitingHour { get; set; }
        [Display(Name = "VehicleHistory_Display_VehicleKM", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleKilometer { get; set; }
        [Display(Name = "CarServiceDurationReport_Display_TotalStatusDay", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal WoDate { get; set; }
        public int group_type { get; set; }
        [Display(Name = "CarServiceDurationReport_Display_TotalWorkOrderNumber", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int TotalWorkOrderNumber { get; set; }
        [Display(Name = "CarServiceDurationReport_Display_TotalDay", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal TotalDay { get; set; }
        [Display(Name = "CarServiceDurationReport_Display_AverageWaitingHour", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int AverageWaitingHour { get; set; }
        [Display(Name = "Vehicle Id")]
        public int VehicleId { get; set; }


        [Display(Name = "CarServiceDurationReport_Display_WoDateTotal", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal WoDateTotal { get; set; }


    }
}
