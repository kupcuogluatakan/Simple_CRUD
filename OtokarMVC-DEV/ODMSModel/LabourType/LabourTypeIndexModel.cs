using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;

namespace ODMSModel.LabourType
{
    public class LabourTypeIndexModel : IndexModelBase
    {
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public bool? IsActive { get; set; }

        [Display(Name = "LabourType_Display_Name", ResourceType = typeof(MessageResource))]
        public string Name { get; set; }

    }
}
