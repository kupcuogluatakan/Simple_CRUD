using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.FleetRequestVehicle
{
    public class FleetRequestVehicleListModel:BaseListWithPagingModel
    {

        public FleetRequestVehicleListModel(Kendo.Mvc.UI.DataSourceRequest request):base(request)
        {
            var mapper=new Dictionary<string, string>
            {
                {"VehicleName","VIN_NO"},
                {"CustomerName","CUSTOMER_NAME"},
                {"DocumentName","DOC_NAME"}
            };
            SetMapper(mapper);
        }

        public FleetRequestVehicleListModel(){}

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
        [Display(Name = "Fleet_Display_FleetName", ResourceType = typeof(MessageResource))]
        public string FleetName { get; set; }

        [Display(Name = "LabourDuration_Display_VehicleModelName", ResourceType = typeof(MessageResource))]
        public string ModelName { get; set; }

        [Display(Name = "FleetRequestVehicle_Display_Document", ResourceType = typeof(MessageResource))]
        public int DocumentId { get; set; }

        [Display(Name = "FleetRequestVehicle_Display_IsWarranty", ResourceType = typeof(MessageResource))]
        public string IsWarranty { get; set; }
    }
}
