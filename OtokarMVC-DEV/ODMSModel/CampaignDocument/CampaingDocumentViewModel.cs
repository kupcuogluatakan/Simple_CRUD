using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;

namespace ODMSModel.CampaignDocument
{
    [Validator(typeof (CampaignDocumentViewModelValidator))]
    public class CampaignDocumentViewModel : ModelBase
    {
        public CampaignDocumentViewModel()
        {
        }

        public bool HideFormElements { get; set; }

        //CampaignCode
        [Display(Name = "Campaign_Display_CampaignCode",
            ResourceType = typeof (ODMSCommon.Resources.MessageResource))]
        public string CampaignCode { get; set; }

        //DocId
        public int DocId { get; set; }

        [Display(Name = "CampaignDocument_Display_DocName", ResourceType = typeof (ODMSCommon.Resources.MessageResource)
            )]
        public string DocName { get; set; }

        //LanguageCode
        [Display(Name = "CampaignDocument_Display_LanguageCode",
            ResourceType = typeof (ODMSCommon.Resources.MessageResource))]
        public string LanguageCode { get; set; }

        [Display(Name = "CampaignDocument_Display_LanguageCode",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string LanguageName { get; set; }

        //DocDesc
        [Display(Name = "CampaignDocument_Display_DocumentDesc",
            ResourceType = typeof (ODMSCommon.Resources.MessageResource))]
        public string DocumentDesc { get; set; }
    }
}
