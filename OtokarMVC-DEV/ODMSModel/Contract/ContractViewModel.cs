using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Contract
{
    [Validator(typeof(ContractViewModelValidator))]
    public class ContractViewModel : ModelBase
    {
        public ContractViewModel()
        {

        }
        public bool HideFormElements { get; set; }

        //CampaignCode
        [Display(Name = "Contract_Display_ContractName",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ContractName { get; set; }

        //DocId
        public int DocId { get; set; }

        [Display(Name = "CampaignDocument_Display_DocName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DocName { get; set; }

        //LanguageCode
        [Display(Name = "CampaignDocument_Display_LanguageCode",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string LanguageCode { get; set; }

        [Display(Name = "Global_Display_StartDate",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Global_Display_EndDate",
    ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Contract_Display_Duration",
ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? Duration { get; set; }

        //DocDesc
        [Display(Name = "CampaignDocument_Display_DocumentDesc",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DocumentDesc { get; set; }
        public int? IdContract { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsActiveName { get; set; }
    }
}
