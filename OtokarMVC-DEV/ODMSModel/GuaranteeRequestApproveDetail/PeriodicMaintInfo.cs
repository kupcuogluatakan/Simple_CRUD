using System.ComponentModel.DataAnnotations;

namespace ODMSModel.GuaranteeRequestApproveDetail
{
    public class PeriodicMaintInfo
    {
        [Display(Name = "Maintenance_Display_Code", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string MaintCode { get; set; }

        [Display(Name = "Maintenance_Display_Km", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Km { get; set; }

        [Display(Name = "Global_Display_Date", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Date { get; set; }

        [Display(Name = "Global_Display_Service", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ServiceName { get; set; }

        [Display(Name = "Maintenance_Display_PrivateService", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public bool IsPrivateService { get; set; }
        [Display(Name = "Maintenance_Display_PeriodicMaintDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PeriodicMaintDesc { get; set; }
    }
}
