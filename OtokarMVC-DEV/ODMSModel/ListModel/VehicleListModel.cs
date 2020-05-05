using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;

namespace ODMSModel.ListModel
{
    public class VehicleListModel : BaseListWithPagingModel
    {
        public VehicleListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"VehicleId", "ID_VEHICLE"},
                    {"VehicleCode", "V.V_CODE_KOD"},
                    {"VehicleCodeDesc","CODE_NAME"},
                    {"CustomerId", "ID_CUSTOMER"},
                    {"CustomerName","CUSTOMER_NAME"},
                    {"VinNo", "VIN_NO"},
                    {"EngineNo", "ENGINE_NO"},
                    {"ModelYear", "MODEL_YEAR"},
                    {"EngineType", "ENGINE_TYPE"},
                    {"VehicleModel", "VEHICLE_MODEL"},
                    {"VehicleType", "VEHICLE_TYPE_NAME"},
                    {"VehicleGroup", "VEHICLE_GROUP_NAME"},
                    {"FactoryProductionDate", "FACT_PROD_DATE"},
                    {"IsActiveName", "IS_ACTIVE"},
                    {"Description","DESCRIPTION"},
                    {"Location","LOCATION"},
                    {"ResponsiblePerson","RESPONSIBLE_PERSON"},
                    {"ResponsiblePersonPhone","RESPONSIBLE_PERSON_PHONE"},
                    {"Plate","PLATE"},
                    {"WarrantyStartDate","WARRANTY_START_DATE"},
                    {"WarrantyEndDate","WARRANTY_END_DATE"},
                    {"PaintWarrantyEndDate","PAINT_WARRANTY_END_DATE"},
                    {"CorrosionWarrantyEndDate","CORROSION_WARRANTY_END_DATE"},
                    {"FactoryShipmentDate","FACT_SHIP_DATE"}
                };
            SetMapper(dMapper);

        }

        public VehicleListModel()
        {
        }

        public int VehicleId { get; set; }
        
        [Display(Name = "Vehicle_Display_VehicleCode", ResourceType = typeof(MessageResource))]
        public string VehicleCode { get; set; }
        [Display(Name = "Vehicle_Display_VehicleCode", ResourceType = typeof(MessageResource))]
        public string VehicleCodeDesc { get; set; }

        [Display(Name = "VehicleModel_Display_Name", ResourceType = typeof(MessageResource))]
        public string VehicleModel { get; set; }
        [Display(Name = "VehicleType_Display_Name", ResourceType = typeof(MessageResource))]
        public string VehicleType { get; set; }
        [Display(Name = "VehicleGroup_Display_Name", ResourceType = typeof(MessageResource))]
        public string VehicleGroup { get; set; }

        [Display(Name = "Vehicle_Display_EngineType", ResourceType = typeof(MessageResource))]
        public string EngineType { get; set; }

        public int CustomerId { get; set; }
        [Display(Name = "Vehicle_Display_CustomerName", ResourceType = typeof (MessageResource))]
        public string CustomerName { get; set; }

        [Display(Name = "Vehicle_Display_VinNo", ResourceType = typeof (MessageResource))]
        public string VinNo { get; set; }

        [Display(Name = "Vehicle_Display_Plate", ResourceType = typeof(MessageResource))]
        public string Plate { get; set; }

        [Display(Name = "Vehicle_Display_EngineNo", ResourceType = typeof (MessageResource))]
        public string EngineNo { get; set; }

        [Display(Name = "Vehicle_Display_ModelYear", ResourceType = typeof (MessageResource))]
        public int ModelYear { get; set; }

        [Display(Name = "Vehicle_Display_FactoryProductionDate", ResourceType = typeof (MessageResource))]
        public DateTime? FactoryProductionDate { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public int? IsActive { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }
        
        [Display(Name = "Vehicle_Display_Description", ResourceType = typeof(MessageResource))]
        public string Description { get; set; }

        [Display(Name = "Vehicle_Display_IdPriceList", ResourceType = typeof(MessageResource))]
        public int? IdPriceList { get; set; }

        [Display(Name = "Vehicle_Display_IdPriceList", ResourceType = typeof(MessageResource))]
        public string SSIDPriceList { get; set; }

        //VatExclude
        [Display(Name = "Vehicle_Display_VatExclude", ResourceType = typeof(MessageResource))]
        public int? VatExcludeType { get; set; }

        [Display(Name = "Vehicle_Display_WarrantyEndDate", ResourceType = typeof(MessageResource))]
        public DateTime? WarrantyEndDate { get; set; }
        [Display(Name = "Vehicle_Display_WarrantyStartDate", ResourceType = typeof(MessageResource))]
        public DateTime? WarrantyStartDate { get; set; }

        public bool IsHourMaint { get; set; }
        [Display(Name = "Vehicle_Display_IsHourMaint", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsHourMaintName { get; set; }
        [Display(Name = "Vehicle_Display_Hour", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int Hour { get; set; }

        [Display(Name = "Vehicle_Display_Location", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Location { get; set; }
        [Display(Name = "Vehicle_Display_ResponsiblePerson", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ResponsiblePerson { get; set; }
        [Display(Name = "Vehicle_Display_ResponsiblePersonPhone", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ResponsiblePersonPhone { get; set; }



        public string ErrorMessage { get; set; }
        public int ErrorNo { get; set; }
        [Display(Name = "Vehicle_Display_VinNo", ResourceType = typeof(MessageResource))]
        public string VinCodeList { get; set; }

        public string ModelCode { get; set; }
        public string ModelName { get; set; }
        public string TypeCode { get; set; }
        public string TypeName { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public int VehicleLastUpdate { get; set; }
        public int VehicleModelLastUpdate { get; set; }

        [Display(Name = "Vehicle_Display_FactoryShipmentDate", ResourceType = typeof(MessageResource))]
        public DateTime? FactoryShipmentDate { get; set; }
        [Display(Name = "Vehicle_Display_PaintWarrantyEndDate", ResourceType = typeof(MessageResource))]
        public DateTime? PaintWarrantyEndDate { get; set; }
        [Display(Name = "Vehicle_Display_CorrosionWarrantyEndDate", ResourceType = typeof(MessageResource))]
        public DateTime? CorrosionWarrantyEndDate { get; set; }
        //Start
        [Display(Name = "Vehicle_Display_ContractNo", ResourceType = typeof(MessageResource))]
        public string ContractNo { get; set; }

        //public string Plate { get; set; }

        //public DateTime? WarrantyEndDate { get; set; }

        [Display(Name = "Vehicle_Display_FactoryQualityControlDate", ResourceType = typeof(MessageResource))]
        public DateTime? FactoryQualityControlDate { get; set; }

        [Display(Name = "Vehicle_Display_VehicleKilometer", ResourceType = typeof(MessageResource))]
        public long? VehicleKilometer { get; set; }
        [Display(Name = "Vehicle_Display_SpecialConditions", ResourceType = typeof(MessageResource))]
        public string SpecialConditions { get; set; }

        //KDV Muhafiyet
        [Display(Name = "Vehicle_Display_VatExclude", ResourceType = typeof(MessageResource))]
        public string VatExcludeTypeName { get; set; }
        //Garanti Dışı Kalma Açıklaması
        [Display(Name = "Vehicle_Display_OutOfWarrantyDescription", ResourceType = typeof(MessageResource))]
        public string OutOfWarrantyDescription { get; set; }

        //Fiyat Listesi
        //public string SSIDPriceList { get; set; }

        //Saatli onarım mı
        //public string IsHourMaintName { get; set; }

        [Display(Name = "Vehicle_Display_Color", ResourceType = typeof(MessageResource))]
        public string Color { get; set; }

        [Display(Name = "Vehicle_Display_WarrantyEndKilometer", ResourceType = typeof(MessageResource))]
        public long? WarrantyEndKilometer { get; set; }

        //Merkez Not
        [Display(Name = "Vehicle_Display_Notes", ResourceType = typeof(MessageResource))]
        public string Notes { get; set; }

        [Display(Name = "Vehicle_Display_WarrantyStatus", ResourceType = typeof(MessageResource))]
        public string WarrantyStatusName { get; set; }
    }
}
