using FluentValidation.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.Fleet
{
    [Validator(typeof(FleetViewModelValidator))]
    public class FleetViewModel : ModelBase
    {
        public FleetViewModel()
        { 
        }

        public int? IdFleet { get; set; }

        [Display(Name = "Fleet_Display_FleetName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string FleetName { get; set; }

        [Display(Name = "Fleet_Display_FleetCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string FleetCode { get; set; }

        [Display(Name = "Fleet_Display_OtokarPartDiscount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? OtokarPartDiscount { get; set; }

        [Display(Name = "Fleet_Display_OtokarLabourDiscount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? OtokarLabourDiscount { get; set; }

        [Display(Name = "Fleet_Display_DealerPartDiscount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? DealerPartDiscount { get; set; }

        [Display(Name = "Fleet_Display_DealerLabourDiscount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? DealerLabourDiscount { get; set; }

        [Display(Name = "Fleet_Display_IsConstrictedName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsConstrictedName { get; set; }

        [Display(Name = "Fleet_Display_IsVinControl", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public bool IsVinControl { get; set; }

        [Display(Name = "Fleet_Display_IsConstrictedName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        /*olm bu neyin kafası la?*/
        public int? IsConstricted { get; set; }

        [Display(Name = "Fleet_Display_StartDateValid", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? StartDateValid { get; set; }

        [Display(Name = "Fleet_Display_EndDateValid", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EndDateValid { get; set; }

        public int HasContent { get; set; }
    }
}
