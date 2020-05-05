using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.VehicleType
{
    public class VehicleTypeListModel : BaseListWithPagingModel
    {
        public VehicleTypeListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
            {
                {"TypeId", "ID_TYPE"},
                {"ModelName", "MODEL_NAME"},
                {"TypeName", "TYPE_NAME"},
                {"TypeSSID", "TYPE_SSID"},
                {"IsActiveS", "IS_ACTIVE"}

            };
            SetMapper(dMapper);
        }


        public VehicleTypeListModel(){}

        public int TypeId { get; set; }

        public string ModelKod { get; set; }

        [Display(Name = "VehicleModel_Display_Name",ResourceType = typeof(MessageResource))]
        public string ModelName { get; set; }

        [Display(Name = "VehicleType_Display_Name", ResourceType = typeof(MessageResource))]
        public string TypeName { get; set; }

        [Display(Name = "VehicleModel_Display_SSID", ResourceType = typeof(MessageResource))]
        public string TypeSSID { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public string IsActiveS { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public bool IsActive { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public int? IsActiveSearch { get; set; }
        [Display(Name = "VehicleGroup_Display_Name", ResourceType = typeof(MessageResource))]
        public string VehicleGroup { get; set; }
        [Display(Name = "VehicleGroup_Display_Name", ResourceType = typeof(MessageResource))]
        public string VehicleGroupId { get; set; }
    }
}
