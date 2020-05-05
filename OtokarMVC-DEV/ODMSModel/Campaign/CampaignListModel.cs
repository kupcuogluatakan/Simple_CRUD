using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.Campaign
{
    public class CampaignListModel : BaseListWithPagingModel
    {
        public CampaignListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"CampaignCode", "CAMPAIGN_CODE"},
                    {"CampaignName","CAMPAIGN_NAME"},
                    {"AdminDesc", "ADMIN_DESC"},
                    {"ModelKod", "MODEL_KOD"},
                    {"IsActive","IS_ACTIVE"},
                    {"IsActiveName","IS_ACTIVE_NAME"},
                    {"StartDate","START_DATE"},
                    {"EndDate","END_DATE"},
                    {"MainFailureCode","MAIN_FAILURE_CODE"},
                    {"IsValidAbroad","IS_VALID_ABROAD"},
                    {"IsValidAbroadName","IS_VALID_ABROAD_NAME"},
                    {"IsMust","IS_MUST"},
                    {"IsMustName","IS_MUST_NAME "}
                };
            SetMapper(dMapper);
        }

        public CampaignListModel()
        {
        }
        [Display(Name = "Campaign_Display_CampaignCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CampaignCode { get; set; }

        [Display(Name = "Campaign_Display_CampaignName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CampaignName { get; set; }

        [Display(Name = "Campaign_Display_AdminDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AdminDesc { get; set; }

        [Display(Name = "Campaign_Display_StartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Campaign_Display_EndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Campaign_Display_ModelKod", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ModelKod { get; set; }

        [Display(Name = "Campaign_Display_MainFailureCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string MainFailureCode { get; set; }

        public int? IsActive { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsActiveName { get; set; }

        public int IsValidAbroad { get; set; }
        [Display(Name = "Campaign_Display_IsValidAbroad", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsValidAbroadName { get; set; }

        [Display(Name = "Global_Display_Category", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? SubCategoryId { get; set; }

        [Display(Name = "Global_Display_Category", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SubCategoryName { get; set; }
        
        public bool? IsMust { get; set; }
        [Display(Name = "Campaign_Display_Is_Must", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsMustName { get; set; }

        [Display(Name = "Campaign_Display_IndicatorCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IndicatorCode { get; set; }

    }
}
