using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.CampaignDismantlePart
{
    public class CampaignDismantlePartListModel : BaseListWithPagingModel
    {
        public CampaignDismantlePartListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"CampaignCode", "CAMPAIGN_CODE"},
                    {"PartId","ID_PART"},
                    {"PartCode","PART_CODE"},
                    {"PartName","PART_NAME"},
                    {"CreateDateFormatted","CREATE_DATE"},
                    {"Note","NOTE"}
                };
            SetMapper(dMapper);
        }

        public CampaignDismantlePartListModel()
        {
        }

        [Display(Name = "SparePart_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }

        [Display(Name = "Campaign_Display_CampaignCode", ResourceType = typeof(MessageResource))]
        public string CampaignCode { get; set; }

        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }
        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(MessageResource))]
        public int PartId { get; set; }

        [Display(Name = "Campaign_Display_Note", ResourceType = typeof(MessageResource))]
        public string Note { get; set; }

        [Display(Name = "Global_Display_CreateDate", ResourceType = typeof(MessageResource))]
        public string CreateDateFormatted { get { return CreateDate == null || CreateDate == DateTime.MinValue ? string.Empty : CreateDate.ToString("dd.MM.yyyy"); } }

        [Display(Name = "Global_Display_CreateDate", ResourceType = typeof(MessageResource))]
        public DateTime CreateDate { get; set; }
    }
}
