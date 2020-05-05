using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.ProposalCard
{
    [Validator(typeof(ProposalCampaignRequestViewModelValidator))]
    public class ProposalCampaignRequestViewModel : ModelBase
    {
        public ProposalCampaignRequestViewModel()
        {
        }

        [Display(Name = "CampaignRequest_Display_CampaignName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CampaignCode { get; set; }

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

        public long? PoNumber { get; set; }

        public Int64 ProposalId { get; set; }
    }
}
