using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.MaintenanceLabour
{
    public class MaintenanceLabourListModel : BaseListWithPagingModel
    {

        public MaintenanceLabourListModel()
        {

        }
        public MaintenanceLabourListModel(Kendo.Mvc.UI.DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
            {
                {"MaintenanceId", "ID_MAINT"},
                {"LabourId", "LABOUR_ID"},
                {"LabourName", "LL.LABOUR_TYPE_DESC"},
                {"LabourCode", "L.LABOUR_CODE"},
                {"Quantity", "ML.QUANTITY"},
                {"IsMustString", "ML.IS_MUST"}
            };
            SetMapper(dMapper);
        }

        [Display(Name = "Maintenance_Display_Name", ResourceType = typeof(MessageResource))]
        public int MaintenanceId { get; set; }
        [Display(Name = "Maintenance_Display_Name", ResourceType = typeof(MessageResource))]
        public string MaintenanceName { get; set; }
        [Display(Name = "Labour_Display_Name", ResourceType = typeof(MessageResource))]
        public int LabourId { get; set; }
        [Display(Name = "Labour_Display_Code", ResourceType = typeof(MessageResource))]
        public string LabourCode { get; set; }
        [Display(Name = "Labour_Display_Name", ResourceType = typeof(MessageResource))]
        public string LabourName { get; set; }
        [Display(Name = "Global_Display_Quantity", ResourceType = typeof(MessageResource))]
        public decimal Quantity { get; set; }
        [Display(Name = "MaintenanceLabour_Display_IsMust", ResourceType = typeof(MessageResource))]
        public bool? IsMust { get; set; }
        [Display(Name = "MaintenanceLabour_Display_IsMust", ResourceType = typeof(MessageResource))]
        public string IsMustString { get; set; }



        //
        [Display(Name = "VehicleModel_Display_Code", ResourceType = typeof(MessageResource))]
        public string VehicleModel { get; set; }
        [Display(Name = "VehicleType_Display_Name", ResourceType = typeof(MessageResource))]
        public string VehicleTypeName { get; set; }
        [Display(Name = "Vehicle_Display_EngineType", ResourceType = typeof(MessageResource))]
        public string EngineType { get; set; }
        [Display(Name = "Maintenance_Display_Type", ResourceType = typeof(MessageResource))]
        public string MaintTypeName { get; set; }
        [Display(Name = "Maintenance_Display_Km", ResourceType = typeof(MessageResource))]
        public string MaintKM { get; set; }
        [Display(Name = "Maintenance_Display_Month", ResourceType = typeof(MessageResource))]
        public string MaintMonth { get; set; }
        [Display(Name = "Global_Display_MainCategory", ResourceType = typeof(MessageResource))]
        public string MainCategoryName { get; set; }
        [Display(Name = "Global_Display_Category", ResourceType = typeof(MessageResource))]
        public string CategoryName { get; set; }
        [Display(Name = "Global_Display_SubCategory", ResourceType = typeof(MessageResource))]
        public string SubCategoryName { get; set; }
        [Display(Name = "AppointmentIndicatorFailureCode_Display_Code", ResourceType = typeof(MessageResource))]
        public string FailureCode { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public bool? IsActive { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public string IsActiveS { get; set; }
    }
}
