using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.Vehicle
{


    [Validator(typeof(VehicleIndexViewModelValidator))]
    public class VehicleIndexViewModel : ModelBase
    {
        public VehicleIndexViewModel()
        {
        }
        public bool HideFormElements { get; set; }

        //VehicleId
        public int VehicleId { get; set; }

        //UserCode
        [Display(Name = "Vehicle_Display_VehicleCodeDef", ResourceType = typeof(MessageResource))]
        public string VehicleCode { get; set; }
        [Display(Name = "VehicleModel_Display_Name", ResourceType = typeof(MessageResource))]
        public string VehicleModel { get; set; }
        [Display(Name = "VehicleType_Display_Name", ResourceType = typeof(MessageResource))]
        public string VehicleType { get; set; }
        //Customer
        public int? CustomerId { get; set; }
        [Display(Name = "Vehicle_Display_CustomerName", ResourceType = typeof(MessageResource))]
        public string CustomerName { get; set; }

        //Vın No
        [Display(Name = "Vehicle_Display_VinNo", ResourceType = typeof(MessageResource))]
        public string VinNo { get; set; }

        //Engine No
        [Display(Name = "Vehicle_Display_EngineNo", ResourceType = typeof(MessageResource))]
        public string EngineNo { get; set; }

        //ModelYear
        [Display(Name = "Vehicle_Display_ModelYear", ResourceType = typeof(MessageResource))]
        public int? ModelYear { get; set; }

        //FactoryProductionDate
        [Display(Name = "Vehicle_Display_FactoryProductionDate", ResourceType = typeof(MessageResource))]
        public DateTime? FactoryProductionDate { get; set; }

        //FactoryQualityControlDate
        [Display(Name = "Vehicle_Display_FactoryQualityControlDate", ResourceType = typeof(MessageResource))]
        public DateTime? FactoryQualityControlDate { get; set; }

        //FactoryShipmentDate
        [Display(Name = "Vehicle_Display_FactoryShipmentDate", ResourceType = typeof(MessageResource))]
        public DateTime? FactoryShipmentDate { get; set; }

        //VatExclude
        [Display(Name = "Vehicle_Display_VatExclude", ResourceType = typeof(MessageResource))]
        public int? VatExcludeType { get; set; }

        [Display(Name = "Vehicle_Display_VatExclude", ResourceType = typeof(MessageResource))]
        public string VatExcludeTypeName { get; set; }

        //ContractNo
        [Display(Name = "Vehicle_Display_ContractNo", ResourceType = typeof(MessageResource))]
        public string ContractNo { get; set; }
        
        //Plate
        [Display(Name = "Vehicle_Display_Plate", ResourceType = typeof(MessageResource))]
        public string Plate { get; set; }

        //Color
        [Display(Name = "Vehicle_Display_Color", ResourceType = typeof(MessageResource))]
        public string Color { get; set; }

        //WarrantyStartDate
        [Display(Name = "Vehicle_Display_WarrantyStartDate", ResourceType = typeof(MessageResource))]
        public DateTime? WarrantyStartDate { get; set; }

        //WarrantyEndDate
        [Display(Name = "Vehicle_Display_WarrantyEndDate", ResourceType = typeof(MessageResource))]
        public DateTime? WarrantyEndDate { get; set; }

        //PaintWarrantyEndDate
        [Display(Name = "Vehicle_Display_PaintWarrantyEndDate", ResourceType = typeof(MessageResource))]
        public DateTime? PaintWarrantyEndDate { get; set; }

        //WarrantyEndKilometer
        [Display(Name = "Vehicle_Display_WarrantyEndKilometer", ResourceType = typeof(MessageResource))]
        public long? WarrantyEndKilometer { get; set; }

        //CorrosionWarrantyEndDate
        [Display(Name = "Vehicle_Display_CorrosionWarrantyEndDate", ResourceType = typeof(MessageResource))]
        public DateTime? CorrosionWarrantyEndDate { get; set; }

        //VehicleKilometer
        [Display(Name = "Vehicle_Display_VehicleKilometer", ResourceType = typeof(MessageResource))]
        public long? VehicleKilometer { get; set; }

        //SpecialConditions
        [Display(Name = "Vehicle_Display_SpecialConditions", ResourceType = typeof(MessageResource))]
        public string SpecialConditions { get; set; }

        //Notes
        [Display(Name = "Vehicle_Display_Notes", ResourceType = typeof(MessageResource))]
        public string Notes { get; set; }

        //WarrantyStatus
        [Display(Name = "Vehicle_Display_WarrantyStatus", ResourceType = typeof(MessageResource))]
        public int? WarrantyStatus { get; set; }
        [Display(Name = "Vehicle_Display_WarrantyStatus", ResourceType = typeof(MessageResource))]
        public string WarrantyStatusName { get; set; }

        //OutOfWarrantyDescription
        [Display(Name = "Vehicle_Display_OutOfWarrantyDescription", ResourceType = typeof(MessageResource))]
        public string OutOfWarrantyDescription { get; set; }

        //IsActive
        [Display(Name = "User_Display_IsActive", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }
        [Display(Name = "User_Display_IsActive", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }

        [Display(Name = "Vehicle_Display_Description", ResourceType = typeof(MessageResource))]
        public string Description { get; set; }

        [Display(Name = "Vehicle_Display_IdPriceList", ResourceType = typeof(MessageResource))]
        public int? IdPriceList { get; set; }

        [Display(Name = "Vehicle_Display_IdPriceList", ResourceType = typeof(MessageResource))]
        public string SSIDPriceList { get; set; }

        public bool IsHourMaint { get; set; }
        [Display(Name = "Vehicle_Display_IsHourMaint", ResourceType = typeof(MessageResource))]
        public string IsHourMaintName { get; set; }
        [Display(Name = "Vehicle_Display_Hour", ResourceType = typeof(MessageResource))]
        public int? Hour { get; set; }

        [Display(Name = "Vehicle_Display_Location", ResourceType = typeof(MessageResource))]
        public string Location { get; set; }
        [Display(Name = "Vehicle_Display_ResponsiblePerson", ResourceType = typeof(MessageResource))]
        public string ResponsiblePerson { get; set; }
        [Display(Name = "Vehicle_Display_ResponsiblePersonPhone", ResourceType = typeof(MessageResource))]
        public string ResponsiblePersonPhone { get; set; }

        public bool PlateWillBeUpdated { get; set; }
        [Display(Name = "Vehicle_Display_VinNo", ResourceType = typeof(MessageResource))]
        public string VinCodeList { get; set; }
    }
}
