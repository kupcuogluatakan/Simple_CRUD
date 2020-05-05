using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.ContractPart
{
    public class ContractPartListModel : BaseListWithPagingModel
    {
        public ContractPartListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"IdContractPart","ID_CONTRACT_PART" },
                    {"IdContract","ID_CONTRACT" },
                    {"IdPart", "ID_PART"},
                    {"PartCode","PART_CODE"},
                    {"ContractName","CONTRACT_NAME"},
                    {"AdminDesc","ADMIN_DESC"}
                };
            SetMapper(dMapper);
        }

        public ContractPartListModel()
        {
        }

        public int IdContractPart { get; set; }
        [Display(Name = "Contract_Display_ContractName",ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IdContract { get; set; }
        [Display(Name = "SaleReport_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int IdPart { get; set; }
        [Display(Name = "SaleReport_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }
        
        [Display(Name = "Campaign_Display_AdminDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AdminDesc { get; set; }
        [Display(Name = "Contract_Display_ContractName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ContractName { get; set; }
    }
}
