using System;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.TechnicianOperationControl
{
    public class TechnicianOperationViewModel : ModelBase
    {
        public int UserId { get; set; }

        public int DealerId { get; set; }

        [Display(Name = "Global_Display_CheckIn", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? CheckInDate { get; set; }

        [Display(Name = "Global_Display_CheckOut", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? CheckOutDate { get; set; }

        public ProcessType ProcessType { get; set; }

        [Display(Name = "TechnicianOperationViewModel_Display_ProcessDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime CreateDate { get; set; }       
    }

    public enum ProcessType
    {
        CheckIn,
        CheckOut
    }
}
