using System.ComponentModel.DataAnnotations;

namespace ODMSModel.GuaranteeRequestApproveDetail
{
    public class FailureDescInfo
    {
        [Display(Name = "FailureDesc_Display_TechnicDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string TechnicDesc { get; set; }

        [Display(Name = "FailureDesc_Display_SupportDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SupportDesc { get; set; }
    }
}
