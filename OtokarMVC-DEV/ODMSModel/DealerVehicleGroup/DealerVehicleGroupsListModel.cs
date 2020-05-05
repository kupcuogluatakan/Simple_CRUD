using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.DealerVehicleGroup
{
    public class DealerVehicleGroupsListModel : BaseListWithPagingModel
    {
        public int DealerId { get; set; }
        [Display(Name = "Dealer_Display_Name", ResourceType = typeof(MessageResource))]
        public String DealerName { get; set; }
        public int VehicleGroupId { get; set; }

        public string VehicleModelCode { get; set; }

        [Display(Name = "VehicleGroup_Display_Name", ResourceType = typeof(MessageResource))]
        public string VehicleGroupName { get; set; }

        [Display(Name = "VehicleModel_Display_Name", ResourceType = typeof(MessageResource))]
        public string VehicleModelName { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public bool IsActive { get; set; }
        public DealerVehicleGroupsListModel()
        {
        }

        public DealerVehicleGroupsListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
             var dMapper = new Dictionary<string, string>
                 {                  
                     {"DealerName", "DEALER_NAME"},                                     
                     {"IsActiveString","IS_ACTIVE"},
                     {"VehicleModelName","MODEL_NAME" }
                 };
            SetMapper(dMapper);

        }
    }
}
