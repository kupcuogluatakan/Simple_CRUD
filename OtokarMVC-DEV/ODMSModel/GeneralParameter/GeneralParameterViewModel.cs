using System.ComponentModel.DataAnnotations;

namespace ODMSModel.GeneralParameter
{
    public class GeneralParameterViewModel:ModelBase
    {
        public string ParameterId { get; set; }
        [Display(Name = "Global_Display_Description", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Description { get; set; }

        public string Type { get; set; }

        [Display(Name = "GeneralParameter_Display_Value", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Value { get; set; }
    }
}
