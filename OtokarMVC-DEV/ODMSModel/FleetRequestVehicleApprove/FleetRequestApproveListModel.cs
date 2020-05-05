using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;

namespace ODMSModel.FleetRequestVehicleApprove
{
    public class FleetRequestApproveListModel :ModelBase
    {
        public int FleetRequestId { get; set; }
        public int FleetRequestVehicleId { get; set; }
        [Display(Name = "FleetVehicle_Display_Fleet", ResourceType = typeof(MessageResource))]
        public int FleetId { get; set; }
         [Display(Name = "FleetVehicle_Display_Fleet", ResourceType = typeof(MessageResource))]
        public string FleetName { get; set; }
         [Display(Name = "FleetVehicle_Display_Vehicle", ResourceType = typeof(MessageResource))]
        public int VehicleId { get; set; }
         [Display(Name = "FleetVehicle_Display_Vehicle", ResourceType = typeof(MessageResource))]
        public string VehicleName { get; set; }
         [Display(Name = "FleetVehicle_Display_Customer", ResourceType = typeof(MessageResource))]
        public int CustomerId { get; set; }
         [Display(Name = "FleetVehicle_Display_Customer", ResourceType = typeof(MessageResource))]
        public string CutomerName { get; set; }
        [Display(Name = "FleetRequestVehicle_Display_Document", ResourceType = typeof(MessageResource))]
        public string DocumentName { get; set; }

        [Display(Name = "FleetRequestVehicle_Display_Document", ResourceType = typeof(MessageResource))]
        public int DocumentId { get; set; }

        public string OldFleetNames { get; set; }


    }
}
