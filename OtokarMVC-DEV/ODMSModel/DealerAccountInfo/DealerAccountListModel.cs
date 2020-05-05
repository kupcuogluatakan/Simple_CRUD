using FluentValidation.Attributes;
using ODMSCommon.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.DealerAccountInfo
{
    [Validator(typeof(DealerAccountInfoListModelValidator))]
    public class DealerAccountListModel : ModelBase
    {
        public int Id { get; set; }      
        public int BankId { get; set; }
        [Display(Name = "DealerAccountInfo_Banka", ResourceType = typeof(MessageResource))]
     
        public string BankName { get; set; }
        [Display(Name = "DealerAccountInfo_Branch", ResourceType = typeof(MessageResource))]
        public string Branch { get; set; }
        public int DealerId { get; set; }
        [Display(Name = "DealerAccountInfo_Iban", ResourceType = typeof(MessageResource))]
        public string Iban { get; set; }
        public string CommandType { get; set; }
        public bool IsActive { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public new string IsActiveString
        {
            get
            {
                if (IsActive)
                    return MessageResource.Global_Display_Active;
                return MessageResource.Global_Display_Passive;
            }
        }
    }
}
