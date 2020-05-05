using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;

namespace ODMSModel.ObjectSearch
{
    public class VehicleSearchListModel : ODMSModel.ListModel.BaseListWithPagingModel
    {
        public VehicleSearchListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"CustomerFullName", "CUSTOMER_FULL_NAME"},
                    {"VehicleCodeDesc", "VEHICLE_CODE_DESC"},
                    {"VinNo", "VIN_NO"},
                    {"EngineNo", "ENGINE_NO"},
                    {"ModelYear", "MODEL_YEAR"},
                    {"FactoryProductionDate", "FACT_PROD_DATE"},
                    {"Plate","PLATE"},
                    {"Mobile","MOBILE"},
                    {"Color","COLOR"}
                };
            SetMapper(dMapper);
        }

        public VehicleSearchListModel()
        {
        }

        public int VehicleId { get; set; }

        public string VehicleCode { get; set; }
        [Display(Name = "Vehicle_Display_VehicleCode", ResourceType = typeof(MessageResource))]
        public string VehicleCodeDesc { get; set; }

        [Display(Name = "Vehicle_Display_CustomerName", ResourceType = typeof(MessageResource))]
        public string CustomerFullName { get; set; }

        [Display(Name = "Vehicle_Display_VinNo", ResourceType = typeof(MessageResource))]
        public string VinNo { get; set; }

        [Display(Name = "Vehicle_Display_EngineNo", ResourceType = typeof(MessageResource))]
        public string EngineNo { get; set; }

        [Display(Name = "Vehicle_Display_Plate", ResourceType = typeof(MessageResource))]
        public string Plate { get; set; }

        [Display(Name = "Vehicle_Display_Color", ResourceType = typeof(MessageResource))]
        public string Color { get; set; }

        [Display(Name = "Vehicle_Display_ModelYear", ResourceType = typeof(MessageResource))]
        public int? ModelYear { get; set; }

        [Display(Name = "Vehicle_Display_FactoryProductionDate", ResourceType = typeof(MessageResource))]
        public DateTime? FactoryProductionDate { get; set; }

        [Display(Name = "VehicleNote_Display_WarrantlyStartDate", ResourceType = typeof(MessageResource))]
        public DateTime? WarrantyStartDate { get; set; }

        [Display(Name = "Customer_Display_MobileNo", ResourceType = typeof(MessageResource))]
        public string Mobile { get; set; }

        [Display(Name = "VehicleModel_Display_Name", ResourceType = typeof(MessageResource))]
        public string VehicleModel { get; set; }

        [Display(Name = "VehicleType_Display_Name", ResourceType = typeof(MessageResource))]
        public string VehicleType { get; set; }

        public bool? BodyworkDetailRequired { get; set; }
    }
}
