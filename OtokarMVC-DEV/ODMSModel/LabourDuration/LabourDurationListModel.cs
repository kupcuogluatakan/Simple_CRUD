using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.LabourDuration
{
    public class LabourDurationListModel : BaseListWithPagingModel
    {
        public int LabourId { get; set; }

        [Display(Name = "Labour_Display_Code", ResourceType = typeof(MessageResource))]
        public string LabourCode { get; set; }

        [Display(Name = "Labour_Display_Name", ResourceType = typeof(MessageResource))]
        public string LabourName { get; set; }

        public string VehicleModelId { get; set; }
        [Display(Name = "Labour_Display_VehicleModelSSID", ResourceType = typeof(MessageResource))]
        public string VehicleModelSSID { get; set; }

        [Display(Name = "LabourDuration_Display_VehicleModelName", ResourceType = typeof(MessageResource))]
        public string VehicleModelName { get; set; }

        public int VehicleTypeId { get; set; }
        [Display(Name = "Labour_Display_VehicleTypeSSID", ResourceType = typeof(MessageResource))]
        public string VehicleTypeSSID { get; set; }

        [Display(Name = "LabourDuration_Display_VehicleTypeName", ResourceType = typeof(MessageResource))]
        public string VehicleTypeName { get; set; }

        [Display(Name = "Vehicle_Display_EngineType", ResourceType = typeof(MessageResource))]
        public string EngineType { get; set; }
        [Display(Name = "Labour_Display_EngineTypeID", ResourceType = typeof(MessageResource))]
        public string EngineTypeID{ get; set; }

        [Display(Name = "LabourDuration_Display_Duration", ResourceType = typeof(MessageResource))]
        public double Duration { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public bool IsActive { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public new string IsActiveString
        {
            get
            {
                if (IsActive)
                    return MessageResource.Global_Display_Active;
                return MessageResource.Global_Display_Passive;
            }
        }

        public LabourDurationListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"LabourId", "ID_LABOUR"},
                     {"VehicleModelId", "MODEL_KOD"},
                     {"VehicleModelName", "MODEL_NAME"},
                     {"EngineType", "ENGINE_TYPE"},
                     {"VehicleTypeId", "ID_TYPE"},
                     {"VehicleTypeName", "TYPE_NAME"},
                     {"Duration", "DURATION"},
                     {"IsActiveString", "IS_ACTIVE"}
                 };
            SetMapper(dMapper);

           
    }

        public LabourDurationListModel() { }    
    }
}
