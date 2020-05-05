using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.CampaignDocument
{
    public class CampaignDocumentListModel : BaseListWithPagingModel
    {
        public CampaignDocumentListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"CampaignCode", "CAMPAIGN_CODE"},
                    {"DocId","ID_DOC"},
                    {"DocName","DOC_NAME"},
                    {"LanguageCode", "LANGUAGE_CODE"},
                    {"DocumentDesc", "DOCUMENT_DESC"}
                };
            SetMapper(dMapper);
        }

        public CampaignDocumentListModel()
        {
        }

        public string CampaignCode { get; set; }
        
        public int DocId { get; set; }
        [Display(Name = "CampaignDocument_Display_DocName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DocName { get; set; }

        [Display(Name = "CampaignDocument_Display_LanguageCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string LanguageCode { get; set; }
        [Display(Name = "CampaignDocument_Display_LanguageCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string LanguageName { get; set; }

        [Display(Name = "CampaignDocument_Display_DocumentDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DocumentDesc { get; set; }
    }
}
