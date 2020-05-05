using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace ODMSModel.Reports
{
    public class VehicleServiceDurationReport
    {

        [Display(Name = "WorkOrderDetailReport_Display_DealerIdList",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerIdList { get; set; }

        //İş Emri Kart No?

        [Display(Name = "WorkOrderDetailReport_Display_VinNo",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VinNo { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_StartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_EndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EndDate { get; set; }
        [Display(Name = "CarServiceDurationReport_Display_GroupName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]

        public string GroupName { get; set; }
        [Display(Name = "CarServiceDurationReport_Display_TotalWorkOrderNumber", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int TotalWorkOrderNumber { get; set; }
        [Display(Name = "CarServiceDurationReport_Display_TotalWaitingHour", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]

        public decimal TotalWaitingHour { get; set; }
        [Display(Name = "CarServiceDurationReport_Display_TotalDay", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]

        public decimal TotalDay { get; set; }
        [Display(Name = "CarServiceDurationReport_Display_AverageWaitingHour", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]

        public decimal AverageWaitingHour { get; set; }

        public decimal WoDate { get; set; }
        [Display(Name = "CarServiceDurationReport_Display_TotalDay", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]

        public decimal WoTotalDay { get; set; }
        [Display(Name = "CarServiceDurationReport_Display_AverageWaitingHour", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]

        public decimal WoAverageWaitingHour { get; set; }

        public int group_type { get; set; }

        public int KM { get; set; }

        public int VehicleId { get; set; }
        public decimal WoDateTotal { get; set; }

        [Display(Name = "CarServiceDurationReport_Display_AverageDay", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]

        public decimal AverageDay { get; set; }

        [Display(Name = "CarServiceDurationReport_Display_TotalServiceHour", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]

        public decimal TotalServiceHour { get; set; }

        [Display(Name = "CarServiceDurationReport_Display_TotalServiceDay", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]

        public decimal TotalServiceDay { get; set; }
        [Display(Name = "CarServiceDurationReport_Display_AverageServiceHour", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]

        public decimal AverageServiceHour { get; set; }
        [Display(Name = "CarServiceDurationReport_Display_AverageServiceDay", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]

        public decimal AverageServiceDay { get; set; }
    }
}
