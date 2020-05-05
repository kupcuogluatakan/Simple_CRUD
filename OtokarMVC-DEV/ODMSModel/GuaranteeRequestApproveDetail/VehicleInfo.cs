using System.ComponentModel.DataAnnotations;

namespace ODMSModel.GuaranteeRequestApproveDetail
{
    public class VehicleInfo
    {
        [Display(Name = "CampaignVehicle_Display_VinNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VinNo { get; set; }

        [Display(Name = "Vehicle_Display_EngineNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string EngineNo { get; set; }

        [Display(Name = "Vehicle_Display_WarrantyStartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WarrantyStartDate { get; set; }

        [Display(Name = "Vehicle_Display_WarrantyEndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WarrantyEndDate { get; set; }

        [Display(Name = "VehicleCode_Display_Kod", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Code { get; set; }

        [Display(Name = "Vehicle_Display_Plate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Plate { get; set; }

        [Display(Name = "VehicleNote_Display_Note", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Note { get; set; }
    }
}
