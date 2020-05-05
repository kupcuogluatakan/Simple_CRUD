using System;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;

namespace ODMSModel.ObjectSearch
{
    public class VehicleSearchViewModel : ObjectSearchModel
    {
        [Display(Name = "Vehicle_Display_CustomerName", ResourceType = typeof(MessageResource))]
        public string CustomerFullName { get; set; }

        [Display(Name = "Vehicle_Display_VinNo", ResourceType = typeof(MessageResource))]
        public string VinNo { get; set; }

        [Display(Name = "Vehicle_Display_EngineNo", ResourceType = typeof(MessageResource))]
        public string EngineNo { get; set; }

        [Display(Name = "Vehicle_Display_Plate", ResourceType = typeof(MessageResource))]
        public string Plate { get; set; }

        [Display(Name = "Vehicle_Display_ModelYear", ResourceType = typeof(MessageResource))]
        public int? ModelYear { get; set; }

        [Display(Name = "VehicleNote_Display_WarrantlyStartDate", ResourceType = typeof(MessageResource))]
        public DateTime? WarrantyDate { get; set; }

        [Display(Name = "VehicleModel_Display_Name", ResourceType = typeof(MessageResource))]
        public string VehicleModel { get; set; }

        [Display(Name = "VehicleType_Display_Name", ResourceType = typeof(MessageResource))]
        public string VehicleType { get; set; }
    }
}
