using System.ComponentModel.DataAnnotations;

namespace ODMSModel.AppointmentDetailsMaintenance
{
    public class ChangePartViewModel : ModelBase
    {
        public ChangePartViewModel()
        {

        }

        public int AppIndPartId { get; set; }

        [Display(Name = "ChangePart_Display_New", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int PartId { get; set; }

        [Display(Name = "ChangePart_Display_Old", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        [Display(Name = "Global_Display_Quantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public double Quantity { get; set; }


    }
}
