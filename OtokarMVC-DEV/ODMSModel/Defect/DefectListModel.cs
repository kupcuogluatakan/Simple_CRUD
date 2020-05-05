using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Defect
{
    public class DefectListModel : BaseListWithPagingModel
    {
        public DefectListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"IdDefect","ID_DEFECT" },
                    {"DefectNo", "DEFECT_NO"},
                    {"DocId","ID_DOC"},
                    {"DocName","DOC_NAME"},
                    {"IdDealer", "ID_DEALER"},
                    {"IdVehicle", "ID_VEHICLE"},
                    {"DeclarationDate", "DECLARATION_DATE"},
                    {"DealerDeclarationDate", "DEALER_DECLARATION_DATE"},
                    {"IsActive", "IS_ACTIVE"},
                    {"IsActiveName", "IS_ACTIVE_NAME"},
                    {"IdContract", "ID_CONTRACT"},
                    {"DealerName", "DEALER_NAME"},
                    {"ContractName", "CONTRACT_NAME"}
                };
            SetMapper(dMapper);
        }

        public DefectListModel()
        {
        }

        public int? IdDefect { get; set; }

        [Display(Name = "Defect_Display_DefectNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DefectNo { get; set; }

        [Display(Name = "Defect_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IdDealer { get; set; }
        [Display(Name = "Defect_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }
        [Display(Name = "Vehicle_Display_IdVehicle", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IdVehicle { get; set; }
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

        public int? IsActive { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsActiveName { get; set; }

        [Display(Name = "Contract_Display_ContractName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ContractName { get; set; }
        [Display(Name = "Defect_Display_VehicleVinNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleVinNo { get; set; }
    }
}
