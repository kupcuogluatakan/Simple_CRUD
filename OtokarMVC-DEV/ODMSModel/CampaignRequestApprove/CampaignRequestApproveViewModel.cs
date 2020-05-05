using FluentValidation.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.CampaignRequestApprove
{
    [Validator(typeof(CampaignRequestApproveViewModelValidator))]
    public class CampaignRequestApproveViewModel : ModelBase
    {
        [Display(Name = "CampaignRequestApprove_Display_CampaignRequestId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int CampaignRequestId { get; set; }

        public int? RequestDealerId { get; set; }
        [Display(Name = "CampaignRequestApprove_Display_RequestDealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RequestDealerName { get; set; }

        public int? SupplierDealerId { get; set; }
        [Display(Name = "CampaignRequestApprove_Display_SupplierDealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SupplierDealerName { get; set; }

        [Display(Name = "CampaignRequestApprove_Display_ModelKod", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ModelKod { get; set; }

        [Display(Name = "CampaignRequestApprove_Display_CampaignCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CampaignCode { get; set; }
        [Display(Name = "CampaignRequestApprove_Display_CampaignName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CampaignName { get; set; }

        [Display(Name = "CampaignRequestApprove_Display_Quantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? Quantity { get; set; }

        public int RequestStatusId { get; set; }
        [Display(Name = "CampaignRequestApprove_Display_RequestStatusName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RequestStatusName { get; set; }

        public int? SupplierTypeId { get; set; }
        [Display(Name = "CampaignRequestApprove_Display_SupplierTypeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SupplierTypeName { get; set; }

        [Display(Name = "CampaignRequestApprove_Display_RequestNote", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RequestNote { get; set; }

        public string VinCodes { get; set; }


        public string RejectionNote { get; set; }

        public bool IsWorkOrderRelated { get; set; }
        public int WodCampaignRequestId { get; set; }

    }
}
