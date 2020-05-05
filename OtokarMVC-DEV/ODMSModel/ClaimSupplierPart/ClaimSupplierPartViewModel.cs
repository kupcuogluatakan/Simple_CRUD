using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.ClaimSupplierPart
{
    [Validator(typeof(ClaimSupplierPartViewModelValidator))]
    public class ClaimSupplierPartViewModel : ModelBase
    {
        public ClaimSupplierPartViewModel()
        {
        }

        [Display(Name = "ClaimSupplierdPart_Display_ClaimRecallPeriodId", ResourceType = typeof(MessageResource))]
        public int ClaimRecallPeriodId { get; set; }

        [Display(Name = "ClaimSupplierPart_Display_SupplierCode", ResourceType = typeof(MessageResource))]
        public string SupplierCode { get; set; }

        [Display(Name = "ClaimSupplierPart_Display_SupplierName", ResourceType = typeof(MessageResource))]
        public string SupplierName { get; set; }

        [Display(Name = "WorkOrderCard_Display_Part", ResourceType = typeof(MessageResource))]
        public long PartId { get; set; }
        [Display(Name = "ClaimSupplierPart_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "ClaimSupplierPart_Display_PartName", ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }


        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }
    }
}
