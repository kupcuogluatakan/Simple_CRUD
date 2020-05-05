using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.FleetRequest
{
    [Validator(typeof(FleetRequestViewModelValidator))]
    public class FleetRequestViewModel : ModelBase
    {
        public int FleetRequestId { get; set; }

        [Display(Name = "Global_Display_Description", ResourceType = typeof(MessageResource))]
        public string Description { get; set; }

        public new int? StatusId { get; set; }
        [Display(Name = "Global_Display_Status", ResourceType = typeof(MessageResource))]
        public string StatusName { get; set; }

        public FleetRequestViewModel()
        {
        }
    }
}
