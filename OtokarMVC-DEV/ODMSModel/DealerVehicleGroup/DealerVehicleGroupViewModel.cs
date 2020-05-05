using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.DealerVehicleGroup
{
    [Validator(typeof(DealerVehicleGroupViewModelValidator))]
    public class DealerVehicleGroupViewModel:ModelBase
    {
        public int DealerId { get; set; }
        [Display(Name = "Dealer_Display_Name", ResourceType = typeof(MessageResource))]
        public String DealerName { get; set; }
        public int? VehicleGroupId { get; set; }

        public string VehicleModelCode { get; set; }

        [Display(Name = "VehicleGroup_Display_Name", ResourceType = typeof(MessageResource))]
        public string VehicleGroupName { get; set; }

        [Display(Name = "VehicleModel_Display_Name", ResourceType = typeof(MessageResource))]
        public string VehicleModelName { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public string IsActiveString { get; set; }
    }
}
