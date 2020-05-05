using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.CampaignLabour
{
    public class CampaignLabourListModel : BaseListWithPagingModel
    {
        public CampaignLabourListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"CampaignCode", "CAMPAIGN_CODE"},
                    {"LabourId","ID_LABOUR"},
                    {"LabourCode", "LABOUR_CODE"},
                    {"LabourTypeDesc", "LABOUR_TYPE_DESC"},
                    {"Quantity","QUANTITY"}
                };
            SetMapper(dMapper);
        }

        public CampaignLabourListModel()
        {
        }

        public string CampaignCode { get; set; }

        public int LabourId { get; set; }
        [Display(Name = "CampaignLabour_Display_LabourCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string LabourCode { get; set; }

        [Display(Name = "CampaignLabour_Display_LabourTypeDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string LabourTypeDesc { get; set; }

        [Display(Name = "CampaignLabour_Display_Quantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal Quantity { get; set; }

        public int Duration { get; set; }
        public string LabourDuration { get; set; }
    }
}
