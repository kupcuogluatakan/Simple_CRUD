using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.DealerFleetVehicle
{
    public class DealerFleetVehicleListModel : BaseListWithPagingModel
    {
        public DealerFleetVehicleListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"FleetRequestId", "ID_FLEET_REQUEST"},
                    {"VinNo","VIN_NO"},
                    {"CustomerName", "CUSTOMER_NAME"},
                    {"WorkOrderId", "ID_WORK_ORDER"},
                    {"DealerName","DEALER_NAME"}
                };
            SetMapper(dMapper);
        }

        public DealerFleetVehicleListModel()
        {
        }
        [Display(Name = "DealerFleetVehicle_Display_FleetReqNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int FleetRequestId { get; set; }

        public int FleetId { get; set; }

        [Display(Name = "Vehicle_Display_VinNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VinNo { get; set; }
        public int VehicleId{ get; set; }

        [Display(Name = "Vehicle_Display_CustomerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }

        [Display(Name = "GRADGif_Display_WorkOrderId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int64 WorkOrderId { get; set; }

        [Display(Name = "Dealer_Display_Name", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }
        public int DealerId { get; set; }
    }
}
