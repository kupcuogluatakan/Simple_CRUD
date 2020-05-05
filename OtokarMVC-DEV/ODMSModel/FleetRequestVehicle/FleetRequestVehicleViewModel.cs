using System.ComponentModel.DataAnnotations;
using System.Web;
using ODMSCommon.Resources;
using FluentValidation.Attributes;
namespace ODMSModel.FleetRequestVehicle
{
    [Validator(typeof(FleetRequestVehicleViewModelValidator))]
    public class FleetRequestVehicleViewModel:ModelBase
    {
        public int FleetRequestVehicleId { get; set; }
        [Display(Name = "FleetVehicle_Display_Fleet", ResourceType = typeof(MessageResource))]
        public int FleetRequestId { get; set; }
        [Display(Name = "FleetVehicle_Display_Customer", ResourceType = typeof(MessageResource))]
        public int CustomerId { get; set; }
        [Display(Name = "FleetVehicle_Display_Vehicle", ResourceType = typeof(MessageResource))]
        public int VehicleId { get; set; }
        [Display(Name = "FleetVehicle_Display_Customer", ResourceType = typeof(MessageResource))]
        public string CustomerName { get; set; }
        [Display(Name = "FleetVehicle_Display_Vehicle", ResourceType = typeof(MessageResource))]
        public string VehicleName { get; set; }

        [Display(Name = "FleetRequestVehicle_Display_Document", ResourceType = typeof(MessageResource))]
        public string DocumentName { get; set; }

        [Display(Name = "FleetRequestVehicle_Display_Document", ResourceType = typeof(MessageResource))]
        public int DocumentId { get; set; }

        public HttpPostedFileBase Document { get; set; }

        public bool HideElements { get; set; }

        public int? FleetRequestStatus { get; set; }
    }
}
