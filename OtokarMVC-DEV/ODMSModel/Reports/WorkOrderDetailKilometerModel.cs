using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Reports
{
    public class WorkOrderDetailKilometerModel
    {
        public string DealerName { get; set; }

        public string GroupName { get; set; }

        [Display(Name = "Sorgulama Kriteri")]
        public int GroupType { get; set; }
        public string GroupCode { get; set; }
        [Display(Name = "CustomerAddress_Display_CustomerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public long? CustomerId { get; set; }

        public long WorkOrderId { get; set; }

        //public int TotalWorkOrderNumber { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_VinNo",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VinNo { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_StartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_EndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EndDate { get; set; }

        public int TotalWaitingHour { get; set; }

        public decimal VehicleKilometer { get; set; }

        public int group_type { get; set; }

        public int TotalWorkOrderNumber { get; set; }

        public int TotalDay { get; set; }

        public int AverageWaitingHour { get; set; }

        public int VehicleId { get; set; }



    }
}
