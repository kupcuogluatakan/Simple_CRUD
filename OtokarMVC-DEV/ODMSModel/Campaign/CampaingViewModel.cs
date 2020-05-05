using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;
using ODMSModel.ViewModel;

namespace ODMSModel.Campaign
{
    [Validator(typeof(CampaignViewModelValidator))]
    public class CampaignViewModel : ModelBase
    {
        public CampaignViewModel()
        {
        }
        public bool HideFormElements { get; set; }
        public string MultiLanguageContentAsText { get; set; }

        //CampaignCode
        [Display(Name = "Campaign_Display_CampaignCode", ResourceType = typeof(MessageResource))]
        public string CampaignCode { get; set; }

        //AdminDesc
        [Display(Name = "Campaign_Display_AdminDesc", ResourceType = typeof(MessageResource))]
        public string AdminDesc { get; set; }

        //StartDate
        [Display(Name = "Campaign_Display_StartDate", ResourceType = typeof(MessageResource))]
        public DateTime StartDate { get; set; }

        //EndDate
        [Display(Name = "Campaign_Display_EndDate", ResourceType = typeof(MessageResource))]
        public DateTime EndDate { get; set; }

        //ModelKod
        [Display(Name = "Campaign_Display_ModelKod", ResourceType = typeof(MessageResource))]
        public string ModelKod { get; set; }

        //MainFaiureCode
        [Display(Name = "Campaign_Display_MainFailureCode", ResourceType = typeof(MessageResource))]
        public string MainFailureDesc { get; set; }
        [Display(Name = "Campaign_Display_MainFailureCode", ResourceType = typeof(MessageResource))]
        public int? MainFailureCode { get; set; }

        //IsActive
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }

        //IsValidAbroad
        public bool? IsValidAbroad { get; set; }
        [Display(Name = "Campaign_Display_IsValidAbroad", ResourceType = typeof(MessageResource))]
        public string IsValidAbroadName { get; set; }

        //CampaignName
        private MultiLanguageModel _currencyName;
        [Display(Name = "Campaign_Display_CampaignName", ResourceType = typeof(MessageResource))]
        public MultiLanguageModel CampaignName { get { return _currencyName ?? new MultiLanguageModel(); } set { _currencyName = value; } }

        [Display(Name = "Campaign_Display_Category", ResourceType = typeof(MessageResource))]
        public int? SubCategoryId { get; set; }

        [Display(Name = "Campaign_Display_Category", ResourceType = typeof(MessageResource))]
        public string SubCategoryName { get; set; }

        public bool IsMust { get; set; }
        [Display(Name = "Campaign_Display_Is_Must", ResourceType = typeof(MessageResource))]
        public string IsMustName { get; set; }
    }
}
