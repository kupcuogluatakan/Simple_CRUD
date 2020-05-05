using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;

namespace ODMSModel.FleetPartPartial
{
    [Validator(typeof(FleetPartPartialViewModelValidator))]
    public class FleetPartViewModel : ModelBase
    {
        public int FleetId { get; set; }

        public int? PartId { get; set; }

        [Display(Name = "FleetPartViewModel_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        [Display(Name = "FleetPartViewModel_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }
    }
}
