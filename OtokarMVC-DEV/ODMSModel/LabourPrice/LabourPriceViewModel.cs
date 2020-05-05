using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.LabourPrice
{
    [Validator(typeof(LabourPriceViewModelValidator))]
    public class LabourPriceViewModel:ModelBase
    {
        [Display(Name = "VehicleModel_Display_LabourPriceId", ResourceType = typeof(MessageResource))]
        public int LabourPriceId { get; set; }
        public string _LabourPriceId { get; set; }
        public int HasTSLabourPriceId { get; set; }
        public int HasNoTSLabourPriceId { get; set; }
        [Display(Name = "VehicleModel_Display_Code", ResourceType = typeof(MessageResource))]
        public string ModelCode { get; set; }
        [Display(Name = "VehicleGroup_Display_Name", ResourceType = typeof(MessageResource))]
        public int VehicleGroupId { get; set; }
        [Display(Name = "LabourPrice_Display_ValidFromDate", ResourceType = typeof(MessageResource))]
        public DateTime ValidFromDate { get; set; }
        [Display(Name = "LabourPrice_Display_ValidFromDate", ResourceType = typeof(MessageResource))]
        public string _ValidFromDate { get; set; }
        [Display(Name = "LabourPrice_Display_ValidEndDate", ResourceType = typeof(MessageResource))]
        public DateTime ValidEndDate { get; set; }
        [Display(Name = "LabourPrice_Display_ValidDate", ResourceType = typeof(MessageResource))]
        public DateTime ValidDate { get; set; }
        [Display(Name = "LabourPrice_Display_ValidEndDate", ResourceType = typeof(MessageResource))]
        public string _ValidEndDate { get; set; }

        [Display(Name = "DealerRegion_Display_Name", ResourceType = typeof(MessageResource))]
        public int DealerRegionId { get; set; }
        [Display(Name = "LabourPrice_Display_HasTsPage", ResourceType = typeof(MessageResource))]
        public bool HasTsPaper { get; set; }


        [Display(Name = "Dealer_Display_DealerClass", ResourceType = typeof(MessageResource))]
        public string DealerClassName { get; set; }
        [Display(Name = "Dealer_Display_CurrencyCode", ResourceType = typeof(MessageResource))]
        public string CurrencyCode { get; set; }
        [Display(Name = "LabourPrice_Display_LabourPriceType", ResourceType = typeof(MessageResource))]
        public int LabourPriceTypeId { get; set; }
        [Display(Name = "LabourPrice_Display_UnitPrice", ResourceType = typeof(MessageResource))]
        public decimal UnitPrice { get; set; }
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public string IsActiveString { get; set; }

        public string CurrencyName { get; set; }

        public string DealerClass { get; set; }

        public string HasTsPaperString { get; set; }

        public string LabourPriceType { get; set; }

        public string ModelName { get; set; }

        public string VehicleGroup { get; set; }

        public string DealerRegionName { get; set; }

        public bool? SearchHasTsPaper { get; set; }

        [Display(Name = "LabourPrice_Display_HasTSUnitPrice", ResourceType = typeof(MessageResource))]
        public decimal HasTSUnitPrice { get; set; }
        public string _HasTSUnitPrice { get; set; }

        [Display(Name = "LabourPrice_Display_HasNoTSUnitPrice", ResourceType = typeof(MessageResource))]
        public decimal HasNoTSUnitPrice { get; set; }
        public string _HasNoTSUnitPrice { get; set; }
    }
}
