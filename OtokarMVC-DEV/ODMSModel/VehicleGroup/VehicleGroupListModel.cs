using Kendo.Mvc.UI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.VehicleGroup  
{
    public class VehicleGroupListModel : BaseListWithPagingModel
    {
        public VehicleGroupListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"VehicleGroupId", "ID_VEHICLE_GROUP"},
                    {"VehicleGroupName", "VHCL_GRP_NAME"},
                    {"AdminDesc","ADMIN_DESC"},
                    {"IsActive", "IS_ACTIVE"},
                    {"IsActiveS","IS_ACTIVE_STRING"}
                };
            SetMapper(dMapper);

        }

        public VehicleGroupListModel(){}

        public int VehicleGroupId { get; set; }

        public string VehicleGroupSSID { get; set; }

        [Display(Name = "Global_Display_AdminDesc", ResourceType = typeof(MessageResource))]
        public string AdminDesc { get; set; }

        [Display(Name = "VehicleGroup_Display_Name", ResourceType = typeof(MessageResource))]
        public string VehicleGroupName { get; set; }

        [Display(Name = "VehicleGroup_Display_Name", ResourceType = typeof(MessageResource))]
        public string GroupName { get; set; }


        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public string IsActiveS { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public bool IsActive { get ;  set ;  }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public int? IsActiveSearch { get; set; }
    
    }
}
