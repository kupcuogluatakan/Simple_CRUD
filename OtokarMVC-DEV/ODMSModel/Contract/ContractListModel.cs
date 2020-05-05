using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Contract
{
    public class ContractListModel : BaseListWithPagingModel
    {
        public ContractListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"IdContract","ID_CONTRACT" },
                    {"ContractName", "CONTRACT_NAME"},
                    {"DocId","ID_DOC"},
                    {"DocName","DOC_NAME"},
                    {"StartDate", "START_DATE"},
                    {"EndDate", "END_DATE"},
                    {"Duration", "DURATION"},
                    {"DocumentDesc", "DOCUMENT_DESC"},
                    {"IsActive", "IS_ACTIVE"},
                    {"IsActiveName", "IS_ACTIVE_NAME"}
                };
            SetMapper(dMapper);
        }

        public ContractListModel()
        {
        }

        [Display(Name = "Contract_Display_IdContract", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IdContract { get; set; }

        [Display(Name = "Contract_Display_ContractName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ContractName { get; set; }

        //DocId
        public int DocId { get; set; }

        [Display(Name = "CampaignDocument_Display_DocName", ResourceType = typeof(ODMSCommon.Resources.MessageResource)
            )]
        public string DocName { get; set; }

        //LanguageCode
        [Display(Name = "CampaignDocument_Display_LanguageCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string LanguageCode { get; set; }

        [Display(Name = "Global_Display_StartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Global_Display_EndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Contract_Display_Duration", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Duration { get; set; }

        //DocDesc
        [Display(Name = "CampaignDocument_Display_DocumentDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DocumentDesc { get; set; }

        public int? IsActive { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsActiveName { get; set; }
    }
}
