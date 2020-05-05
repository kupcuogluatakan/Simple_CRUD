using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.VehicleBodywork
{
    public class VehicleBodyworkListModel : BaseListWithPagingModel
    {
        public VehicleBodyworkListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"BodyworkCode", "BODYWORK_CODE"},
                     {"BodyworkName", "BODYWORK_NAME"},
                     {"VehiclePlate", "VEHICLE_PLATE"},
                     {"CountryName", "COUNTRY_NAME"},
                     {"CityName", "CITY_NAME"},
                     {"WorkOrderName", "WORK_ORDER_NAME"},
                     {"DealerName", "DEALER_NAME"},
                     {"Manufacturer", "MANUFACTURER"}
                 };
            SetMapper(dMapper);

        }

        public VehicleBodyworkListModel()
        {
        }


        public int VehicleBodyworkId { get; set; }

        [Display(Name = "VehicleBodywork_Display_BodyworkCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string BodyworkCode { get; set; }
        [Display(Name = "VehicleBodywork_Display_BodyworkName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string BodyworkName { get; set; }

        [Display(Name = "VehicleBodywork_Display_Vehicle", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? VehicleId { get; set; }
        [Display(Name = "VehicleBodywork_Display_VehiclePlate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehiclePlate { get; set; }

        public int? CountryId { get; set; }
        [Display(Name = "VehicleBodywork_Display_CountryName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CountryName { get; set; }

        public int? CityId { get; set; }
        [Display(Name = "VehicleBodywork_Display_CityName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CityName { get; set; }

        public int? WorkOrderId { get; set; }
        [Display(Name = "VehicleBodywork_Display_WorkOrderName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WorkOrderName { get; set; }

        public int? DealerId { get; set; }
        [Display(Name = "VehicleBodywork_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "VehicleBodywork_Display_Manufacturer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Manufacturer { get; set; }

        [Display(Name = "Vehicle_Display_VinNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VinNo { get; set; }
    }
}
