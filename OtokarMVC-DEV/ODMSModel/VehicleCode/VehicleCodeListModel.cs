using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.VehicleCode
{
    public class VehicleCodeListModel : BaseListWithPagingModel
    {
        public VehicleCodeListModel([DataSourceRequest] DataSourceRequest request) : base(request)
        {
            var dMapper = new Dictionary<string, string>
            {
                {"VehicleCodeKod", "V_CODE_KOD"},
                {"VehicleTypeName", "TYPE_NAME"},
                {"VehicleCodeSSID", "CODE_SSID"},
                {"VehicleCodeName", "CODE_NAME"},
                {"IsActiveS", "IS_ACTIVE"},
                {"VehicleGroupId","VHCL_GRP_NAME"},
                {"ModelName","MODEL_NAME"},
                {"EngineType","ENGINE_TYPE"}
            };
            SetMapper(dMapper);
        }

        public VehicleCodeListModel()
        {
        }

        [Display(Name = "VehicleCode_Display_EngineType", ResourceType = typeof(MessageResource))]
        public string EngineType { get; set; }

        [Display(Name = "VehicleCode_Display_Kod", ResourceType = typeof(MessageResource))]
        public string VehicleCodeKod { get; set; }

        public int VehicleTypeId { get; set;}
        [Display(Name = "VehicleType_Display_Name", ResourceType = typeof(MessageResource))]
        public string VehicleTypeName { get; set; }

        [Display(Name = "VehicleCode_Display_SSID", ResourceType = typeof(MessageResource))]
        public string VehicleCodeSSID { get; set; }

        [Display(Name = "VehicleCode_Display_Name", ResourceType = typeof(MessageResource))]
        public string VehicleCodeName { get; set; }     

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public bool IsActive { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public string IsActiveS { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public int? IsActiveSearch { get; set; }

        [Display(Name = "DealerTechnicianGroup_Display_VehicleGroupName", ResourceType = typeof(MessageResource))]
        public string VehicleGroupId { get; set; }

        [Display(Name = "Appointment_Display_VehicleModel", ResourceType = typeof(MessageResource))]
        public string ModelName { get; set; }
        [Display(Name = "Appointment_Display_VehicleCode", ResourceType = typeof(MessageResource))]
        public string VehicleCode { get; set; }

    }
}
