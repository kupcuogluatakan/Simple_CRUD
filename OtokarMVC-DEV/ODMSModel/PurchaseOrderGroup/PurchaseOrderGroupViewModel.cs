using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.PurchaseOrderGroup
{
    [Validator(typeof(PurchaseOrderGroupViewModelValidator))]
    public class PurchaseOrderGroupViewModel : ModelBase
    {        
        public int PurchaseOrderGroupId { get; set; }
        
        [Display(Name = "PurchaseOrderGroup_Display_GroupName", ResourceType = typeof(MessageResource))]
        public string GroupName { get; set; }       
    }
}
