using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;
using ODMSModel.MaintenanceLabour;
using ODMSModel.MaintenancePart;

namespace ODMSModel.Maintenance
{
    public class MaintenanceListModel : BaseListWithPagingModel
    {
        public MaintenanceListModel([DataSourceRequest] DataSourceRequest request) : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"MaintId", "ID_MAINT"},
                     {"MaintName", "MAINT_NAME"},
                     {"VehicleTypeName", "TYPE_NAME"},
                     {"EngineType", "ENGINE_TYPE"},
                     {"MaintTypeName", "MAINT_TYPE_NAME"},
                     {"MaintMonth", "MAINT_MONTH"},
                     {"MaintKM", "MAINT_KM"},
                     {"IsActiveS", "ACTIVITY_STATUS"},
                     {"MainCategoryName","MAIN_CATEGORY_NAME"},
                     {"CategoryName","AICL.DESCRIPTION"},
                     {"SubCategoryName","AISCL.DESCRIPTION"},
                     {"FailureCode","AIFC.CODE"}
                 };
            SetMapper(dMapper);

        }

        public MaintenanceListModel()
        {
        }
        [Display(Name = "Maintenance_Display_ID", ResourceType = typeof(MessageResource))]
        public int MaintId { get; set; }

        public string MaintTypeId { get; set; }
        [Display(Name = "Maintenance_Display_Name", ResourceType = typeof(MessageResource))]
        public string MaintName { get; set; }

        public int VehicleTypeId { get; set; }
        [Display(Name = "VehicleType_Display_Name", ResourceType = typeof(MessageResource))]
        public string VehicleTypeName { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_ModelKod", ResourceType = typeof(MessageResource))]
        public string VehicleModel { get; set; }

        [Display(Name = "Maintenance_Display_Month", ResourceType = typeof(MessageResource))]
        public string MaintMonth { get; set; }

        [Display(Name = "Maintenance_Display_Type", ResourceType = typeof(MessageResource))]
        public string MaintTypeName { get; set; }

        [Display(Name = "Vehicle_Display_EngineType", ResourceType = typeof(MessageResource))]
        public string EngineType { get; set; }

        [Display(Name = "Maintenance_Display_Km", ResourceType = typeof(MessageResource))]
        public string MaintKM { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public bool IsActive { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public string IsActiveS { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public int? IsActiveSearch { get; set; }

        [Display(Name = "Global_Display_MainCategory", ResourceType = typeof(MessageResource))]
        public int MainCategoryId { get; set; }
        [Display(Name = "Global_Display_Category", ResourceType = typeof(MessageResource))]
        public int CategoryId { get; set; }
        [Display(Name = "Global_Display_SubCategory", ResourceType = typeof(MessageResource))]
        public int SubCategoryId { get; set; }
        [Display(Name = "Global_Display_TotalPrice", ResourceType = typeof(MessageResource))]
        public decimal TotalPrice { get; set; }
        [Display(Name = "Global_Display_MainCategory", ResourceType = typeof(MessageResource))]
        public string MainCategoryName { get; set; }
        [Display(Name = "Global_Display_Category", ResourceType = typeof(MessageResource))]
        public string CategoryName { get; set; }
        [Display(Name = "Global_Display_SubCategory", ResourceType = typeof(MessageResource))]
        public string SubCategoryName { get; set; }
        [Display(Name = "AppointmentIndicatorFailureCode_Display_Code", ResourceType = typeof(MessageResource))]
        public string FailureCode { get; set; }
        [Display(Name = "AppointmentIndicatorFailureCode_Display_Code", ResourceType = typeof(MessageResource))]
        public int? FailureCodeId { get; set; }

        #region ExcelExportAllMethodField
        [Display(Name = "AppointmentIndicatorFailureCode_Display_Part_LabourID", ResourceType = typeof(MessageResource))]
        public string Part_LabourID { get; set; }
        [Display(Name = "AppointmentIndicatorFailureCode_Display_Part_LabourCode", ResourceType = typeof(MessageResource))]
        public string Part_LabourCode { get; set; }
        [Display(Name = "AppointmentIndicatorFailureCode_Display_IsMustString", ResourceType = typeof(MessageResource))]
        public string IsMustString { get; set; }
        [Display(Name = "AppointmentIndicatorFailureCode_Display_Quantity", ResourceType = typeof(MessageResource))]
        public decimal Quantity { get; set; }
        [Display(Name = "AppointmentIndicatorFailureCode_Display_LabourTypeDesc", ResourceType = typeof(MessageResource))]
        public string LabourTypeDesc { get; set; }
        [Display(Name = "AppointmentIndicatorFailureCode_Display_Part_LabourName", ResourceType = typeof(MessageResource))]
        public string Part_LabourName { get; set; }
        [Display(Name = "AppointmentIndicatorFailureCode_Display_Unit", ResourceType = typeof(MessageResource))]
        public string Unit { get; set; }
        [Display(Name = "AppointmentIndicatorFailureCode_Display_AlternateAllowString", ResourceType = typeof(MessageResource))]
        public string AlternateAllowString { get; set; }
        [Display(Name = "AppointmentIndicatorFailureCode_Display_DifBrandAllowString", ResourceType = typeof(MessageResource))]
        public string DifBrandAllowString { get; set; }
        [Display(Name = "AppointmentIndicatorFailureCode_Display_Type", ResourceType = typeof(MessageResource))]
        public string Type { get; set; } 
        #endregion

    }
}
