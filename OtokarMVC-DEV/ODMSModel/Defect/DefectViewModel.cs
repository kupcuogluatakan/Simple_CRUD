using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Defect
{
    [Validator(typeof(DefectViewModelValidator))]
    public class DefectViewModel : ModelBase
    {
        public DefectViewModel()
        { }

        public bool HideFormElements { get; set; }

        public int? IdDefect { get; set; }

        [Display(Name = "Defect_Display_DefectNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DefectNo { get; set; }
        [Display(Name = "Defect_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IdDealer { get; set; }
        [Display(Name = "KilometerReportInfo_Display_VehicleId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? VehicleId { get; set; }
        //DocId
        [Display(Name = "CampaignDocument_Display_DocName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int DocId { get; set; }

        [Display(Name = "CampaignDocument_Display_DocName", ResourceType = typeof(ODMSCommon.Resources.MessageResource)
            )]
        public string DocName { get; set; }

        //LanguageCode
        [Display(Name = "CampaignDocument_Display_LanguageCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string LanguageCode { get; set; }

        [Display(Name = "Defect_Display_DeclarationDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? DeclarationDate { get; set; }

        [Display(Name = "Defect_Display_DealerDeclarationDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? DealerDeclarationDate { get; set; }
        [Display(Name = "Contract_Display_ContractName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IdContract { get; set; }

        //public int? IsActive { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsActiveName { get; set; }
        [Display(Name = "Defect_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }
        [Display(Name = "SSHReport_Display_VinNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleVinNo { get; set; }
        [Display(Name = "Contract_Display_ContractName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ContractName { get; set; }
    }
}
