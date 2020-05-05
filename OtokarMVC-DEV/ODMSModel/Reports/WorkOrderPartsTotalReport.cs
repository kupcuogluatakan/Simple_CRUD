using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Reports
{
    public class WorkOrderPartsTotalReport
    {
        [Display(Name = "WorkOrderPartsTotalReport_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "WorkOrderPartsTotalReport_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }
        [Display(Name = "WorkOrderPartsTotalReport_PaidCount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal PaidCount { get; set; }
        [Display(Name = "WorkOrderPartsTotalReport_PaidAmount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal PaidAmount { get; set; }
        [Display(Name = "WorkOrderPartsTotalReport_FreeCount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal FreeCount { get; set; }
        [Display(Name = "WorkOrderPartsTotalReport_FreeAmount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal FreeAmount { get; set; }
        [Display(Name = "WorkOrderPartsTotalReport_Currency", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Currency { get; set; }
        [Display(Name = "WorkOrderPartsTotalReport_AvgKm", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AvgKm { get; set; }
        [Display(Name = "WorkOrderPartsTotalReport_MinKm", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string MinKm { get; set; }
        [Display(Name = "WorkOrderPartsTotalReport_MaxKm", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string MaxKm { get; set; }
        [Display(Name = "WorkOrderPartsTotalReport_AmountPercent", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal AmountPercent { get; set; }
        [Display(Name = "WorkOrderPartsTotalReport_CountPercent", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal CountPercent { get; set; }
        [Display(Name = "WorkOrderPartsTotalReport_CustomerPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal CustomerAmount { get; set; }
    }
}
