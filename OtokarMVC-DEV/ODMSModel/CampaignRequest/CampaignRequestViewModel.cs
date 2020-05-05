using FluentValidation.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.CampaignRequest
{
    [Validator(typeof(CampaignRequestViewModelValidator))]
    public class CampaignRequestViewModel : ModelBase
    {
        public CampaignRequestViewModel()
        {
        }

        [Display(Name = "CampaignRequest_Display_CampaignName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CampaignCode { get; set; }

        [Display(Name = "CampaignRequest_Display_VinCodes", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CampaignVinCodes { get; set; }

        public decimal? IdCampaignRequest { get; set; }

        [Display(Name = "CampaignRequest_Display_IdDealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IdDealer { get; set; }
        [Display(Name = "CampaignRequest_Display_IdDealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "CampaignRequest_Display_VerihcleModelCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VerihcleModelCode { get; set; }

        [Display(Name = "CampaignRequest_Display_Quantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? Quantity { get; set; }

        [Display(Name = "CampaignRequest_Display_CampaignName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CampaignName { get; set; }

        [Display(Name = "CampaignRequest_Display_RequestStatus", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? RequestStatus { get; set; }

        [Display(Name = "CampaignRequest_Display_RequestNote", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RequestNote { get; set; }

        [Display(Name = "CampaignRequest_Display_RequestStatus", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RequestStatusName { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [Display(Name = "CampaignRequest_Display_PreferredOrderDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? PreferredOrderDate { get; set; }

        [Display(Name = "CampaignRequest_Display_UpdateUsername", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string UpdateUserName { get; set; }

        public string RejectionNote { get; set; }

        public long? PoNumber { get; set; }

        public int VehicleId { get; set; }
        public int WodCampaignRequestId { get; set; }

        public Int64 WorkOrderId { get; set; }
        public string token { get; set; }
    }
}
