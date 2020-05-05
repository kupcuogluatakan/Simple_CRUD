using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using ODMSCommon.Resources;

namespace ODMSModel.MaintenancePart
{
    public class MaintenancePartListModel : BaseListWithPagingModel
    {
        public MaintenancePartListModel(DataSourceRequest request) : base(request)
        {
            var dMapper = new Dictionary<string, string>
            {
                {"MaintId", "ID_MAINT"},
                {"PartId", "ID_PART"},
                {"IsMustS", "IS_MUST_STRING"},
                {"MaintName", "MAINT_NAME"},
                {"PartCode", "PART_CODE"},
                {"PartName", "PART_NAME"},
                {"IsAlernateAllowS", "ALTERNATE_ALLOW_STRING"},
                {"IsDifBrandAllowS", "DIF_BRAND_ALLOW_STRING"},
                {"IsActiveString", "PMP.IS_ACTIVE"},
                {"Unit", "UNIT"},
                {"Quantity", "QUANTITY"}
            };
            SetMapper(dMapper);
        }

        public MaintenancePartListModel()
        {
        }

        [Display(Name = "Maintenance_Display_Name", ResourceType = typeof(MessageResource))]
        public int MaintId { get; set; }
        [Display(Name = "Maintenance_Display_Name", ResourceType = typeof(MessageResource))]
        public string MaintName { get; set; }

        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(MessageResource))]
        public int PartId { get; set; }
        [Display(Name = "SparePart_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }

        [Display(Name = "MaintenancePart_Display_IsMust", ResourceType = typeof(MessageResource))]
        public string IsMustS { get; set; }

        [Display(Name = "MaintenancePart_Display_IsAlternAllow", ResourceType = typeof(MessageResource))]
        public string IsAlernateAllowS { get; set; }

        [Display(Name = "MaintenancePart_Display_IsDifBrandAllow", ResourceType = typeof(MessageResource))]
        public string IsDifBrandAllowS { get; set; }

        [Display(Name = "MaintenancePart_Display_Quantity", ResourceType = typeof(MessageResource))]
        public string Quantity { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public int? IsActiveSearch { get; set; }

        [Display(Name = "Scrap_Display_Unit", ResourceType = typeof(MessageResource))]
        public string Unit { get; set; }


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
        public string IsActiveS { get; set; }












    }
}
