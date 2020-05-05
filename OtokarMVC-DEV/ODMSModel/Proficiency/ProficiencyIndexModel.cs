using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;

namespace ODMSModel.Proficiency
{
    public class ProficiencyIndexModel
    {
        [Display(Name = "Proficiency_Display_Code", ResourceType = typeof(MessageResource))]
        public string ProficiencyCode { get; set; }

        [Display(Name = "Proficiency_Display_Name", ResourceType = typeof(MessageResource))]
        public string ProficiencyName { get; set; }

        [Display(Name = "Global_Display_Description", ResourceType = typeof(MessageResource))]
        public string Description { get; set; }
    }
}
