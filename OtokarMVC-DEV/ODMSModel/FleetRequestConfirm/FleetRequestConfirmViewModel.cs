using FluentValidation.Attributes;
using ODMSCommon.Resources;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.FleetRequestConfirm
{
    [Validator(typeof(FleetRequestConfirmViewModelValidator))]
    public class FleetRequestConfirmViewModel : ModelBase
    {
        public int FleetRequestId { get; set; }

        [Display(Name = "Global_Display_Description", ResourceType = typeof(MessageResource))]
        public string Description { get; set; }

        public new int? StatusId { get; set; }
        [Display(Name = "Global_Display_Status", ResourceType = typeof(MessageResource))]
        public string StatusName { get; set; }

        [Display(Name = "FleetRequest_Display_Cancel_Description", ResourceType = typeof(MessageResource))]
        public string RejectDescription { get; set; }

        public FleetRequestConfirmViewModel()
        {
        }
    }
}
