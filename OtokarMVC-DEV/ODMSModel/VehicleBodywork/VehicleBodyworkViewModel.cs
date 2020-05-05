using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;

namespace ODMSModel.VehicleBodywork
{
    [Validator(typeof(VehicleBodyworkViewModelValidator))]
    public class VehicleBodyworkViewModel : ModelBase
    {
        public VehicleBodyworkViewModel()
        {
        }
        public bool HideFormElements { get; set; }

        public int VehicleBodyworkId { get; set; }

        [Display(Name = "VehicleBodywork_Display_BodyworkCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string BodyworkCode { get; set; }
        [Display(Name = "VehicleBodywork_Display_BodyworkName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string BodyworkName { get; set; }

        [Display(Name = "VehicleBodywork_Display_Vehicle", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? VehicleId { get; set; }
        [Display(Name = "VehicleBodywork_Display_VehiclePlate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehiclePlate { get; set; }

        public int? CountryId { get; set; }
        [Display(Name = "VehicleBodywork_Display_CountryName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CountryName { get; set; }

        public int? CityId { get; set; }
        [Display(Name = "VehicleBodywork_Display_CityName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CityName { get; set; }

        public Int64? WorkOrderId { get; set; }
        [Display(Name = "VehicleBodywork_Display_WorkOrderName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WorkOrderName { get; set; }

        public int? DealerId { get; set; }
        [Display(Name = "VehicleBodywork_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "VehicleBodywork_Display_Manufacturer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Manufacturer { get; set; }

        public string VehicleVinNo { get; set; }
    }
}
