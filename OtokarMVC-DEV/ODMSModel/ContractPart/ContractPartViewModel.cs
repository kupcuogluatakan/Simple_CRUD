using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.ContractPart
{
    [Validator(typeof(ContractPartViewModelValidator))]
    public class ContractPartViewModel :  ModelBase
    {
        public ContractPartViewModel()
        {

        }

        public int? IdContractPart { get; set; }

        public int? IdContract { get; set; }
        [Display(Name = "SaleReport_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int IdPart { get; set; }
        
        [Display(Name = "SaleReport_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }

        public string AdminDesc { get; set; }
        [Display(Name = "Contract_Display_ContractName",
ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ContractName { get; set; }
    }
}
