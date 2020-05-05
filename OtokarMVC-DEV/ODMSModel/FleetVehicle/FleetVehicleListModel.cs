using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.FleetVehicle
{
    public class FleetVehicleListModel:BaseListWithPagingModel
    {

        public FleetVehicleListModel(Kendo.Mvc.UI.DataSourceRequest request):base(request)
        {
            var mapper=new Dictionary<string, string>
            {
                {"VehicleName","VIN_NO"},
                {"CustomerName","CUSTOMER_NAME"}
            };
            SetMapper(mapper);
        }

        public FleetVehicleListModel(){}

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
    }
}
