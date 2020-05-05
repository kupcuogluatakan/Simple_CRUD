using System.ComponentModel.DataAnnotations;

namespace ODMSModel.GuaranteeRequestApproveDetail
{
    public class FailureInfo
    {
        [Display(Name = "Failure_Dislay_FailureDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string FailureDate { get; set; }

        [Display(Name = "Failure_Dislay_VehicleLeaveDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleLeaveDate { get; set; }

        [Display(Name = "Maintenance_Display_Km", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Km { get; set; }

        [Display(Name = "Campaign_Display_CampaignCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CampaignCode { get; set; }

        [Display(Name = "AppointmentIndicatorFailureCode_Display_Code", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string FailureCode { get; set; }
    }
}
