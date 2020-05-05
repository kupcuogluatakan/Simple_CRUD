using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Reports
{
    public class SshReport
    {
        [Display(Name = "SSHReport_Display_DefectNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DefectNo { get; set; }
        [Display(Name = "SSHReport_Display_ContractName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ContractName { get; set; }
        [Display(Name = "SSHReport_Display_PoNumber", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PoNumber { get; set; }
        [Display(Name = "SSHReport_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "SSHReport_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }
        [Display(Name = "SSHReport_Display_Duration", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Duration { get; set; }
        [Display(Name = "SSHReport_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }
        [Display(Name = "SSHReport_Display_VinNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VinNo { get; set; }
        [Display(Name = "SSHReport_Display_DeclarationDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? DeclarationDate { get; set; }
        [Display(Name = "SSHReport_Display_DealerDeclarationDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? DealerDeclarationDate { get; set; }
        [Display(Name = "SSHReport_Display_OrderDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? OrderDate { get; set; }
        [Display(Name = "SSHReport_Display_DurationDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? DurationDate { get; set; }
        [Display(Name = "SSHReport_Display_PoStatus", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PoStatus { get; set; }
        [Display(Name = "SSHReport_Display_CreateDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? CreateDate { get; set; }
        [Display(Name = "SSHReport_Display_OrderPeriod", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? OrderPeriod { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsActive { get; set; }
    }
}
