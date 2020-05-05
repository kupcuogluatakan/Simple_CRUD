using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.GuaranteeRequestApproveDetail
{
    public class GuaranteeDescriptionHistoryListModel : ModelBase
    {
        public Int64 GuaranteeId { get; set; }
        [Display(Name = "CampaignLabour_Display_Description", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Description { get; set; }
        [Display(Name = "CampaignLabour_Display_RequestDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime RequestDate { get; set; }
        [Display(Name = "CampaignLabour_Display_RequestUser", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RequestUser { get; set; }


        [Display(Name = "CampaignLabour_Display_ApproveDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime ApproveDate { get; set; }
        [Display(Name = "CampaignLabour_Display_ApproveUser", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ApproveUser { get; set; }

        [Display(Name = "CampaignLabour_Display_RequestWarrantyStatus", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public short RequestWarrantyStatus { get; set; }

        [Display(Name = "CampaignLabour_Display_ApproveWarrantyStatus", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public short ApproveWarrantyStatus { get; set; }


        [Display(Name = "CampaignLabour_Display_SeqNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public short SeqNo { get; set; }
        [Display(Name = "CampaignLabour_Display_Type", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Type { get; set; }
    }
}