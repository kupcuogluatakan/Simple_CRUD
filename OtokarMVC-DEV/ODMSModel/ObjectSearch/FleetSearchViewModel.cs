using System.ComponentModel.DataAnnotations;

namespace ODMSModel.ObjectSearch
{
    public class FleetSearchViewModel:ObjectSearchModel
    {
        [Display(Name = "Fleet_Display_FleetName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string FleetName { get; set; }
        [Display(Name = "Fleet_Display_FleetCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string FleetCode { get; set; }
        [Display(Name = "Fleet_Display_IsConstrictedName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public bool? IsPartConstricted { get; set; }
    }
}
