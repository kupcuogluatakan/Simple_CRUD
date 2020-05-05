using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;

namespace ODMSModel.FleetVehicle
{
    public class FleetVehicleViewModel:ModelBase
    {
        public int FleetVehicleId { get; set; }
        [Display(Name = "FleetVehicle_Display_Fleet", ResourceType = typeof(MessageResource))]
        public int FleetId { get; set; }

        [Display(Name = "FleetVehicle_Display_Customer", ResourceType = typeof(MessageResource))]
        public int CustomerId { get; set; }
        [Display(Name = "FleetVehicle_Display_Vehicle", ResourceType = typeof(MessageResource))]
        public int VehicleId { get; set; }
        [Display(Name = "FleetVehicle_Display_Customer", ResourceType = typeof(MessageResource))]
        public string CustomerName { get; set; }
        [Display(Name = "FleetVehicle_Display_Vehicle", ResourceType = typeof(MessageResource))]
        public string VehicleName { get; set; }

        public string VehicleVinNo { get; set; }

        public bool HideElements { get; set; }
    }
}
