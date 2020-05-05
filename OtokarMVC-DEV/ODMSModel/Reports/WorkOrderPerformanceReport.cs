using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Reports
{
    public class WorkOrderPerformanceReport
    {
        [Display(Name = "WorkOrderPerformanceReport_DealerSSID", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerSSID { get; set; }
        [Display(Name = "WorkOrderPerformanceReport_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }
        [Display(Name = "WorkOrderPerformanceReport_RegionName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RegionName { get; set; }

        [Display(Name = "WorkOrderPerformanceReport_VinNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VinNo { get; set; }
        public string VehicleId { get; set; }
        [Display(Name = "WorkOrderPerformanceReport_WorkOderID", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WorkOderID { get; set; }
        [Display(Name = "WorkOrderPerformanceReport_WorkOrderDetailID", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WorkOrderDetailID { get; set; }
        [Display(Name = "WorkOrderPerformanceReport_WorkOrderOpenedDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime WorkOrderOpenedDate { get; set; }
        [Display(Name = "WorkOrderPerformanceReport_GuaranteeSeq", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string GuaranteeSeq { get; set; }
        [Display(Name = "WorkOrderPerformanceReport_RequestDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime RequestDate { get; set; }
        [Display(Name = "WorkOrderPerformanceReport_ApproveDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime ApproveDate { get; set; }
        [Display(Name = "WorkOrderPerformanceReport_RequestApproveBetweenMinute", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RequestApproveBetweenMinute { get; set; }
        [Display(Name = "WorkOrderPerformanceReport_IndicatorType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IndicatorType { get; set; }
        [Display(Name = "WorkOrderPerformanceReport_ProcessType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ProcessType { get; set; }
        [Display(Name = "WorkOrderPerformanceReport_RequestWarrantyStatu", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RequestWarrantyStatu { get; set; }
        [Display(Name = "WorkOrderPerformanceReport_ApprovaWarrantyStatu", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ApprovaWarrantyStatu { get; set; }
        [Display(Name = "WorkOrderPerformanceReport_GifNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string GifNo { get; set; }
        [Display(Name = "WorkOrderPerformanceReport_ProcessOfUser", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ProcessOfUser { get; set; }


    }
}
