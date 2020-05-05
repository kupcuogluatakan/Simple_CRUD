using FluentValidation.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.ClaimSupplier
{
    [Validator(typeof(ClaimSupplierViewModelValidator))]
    public class ClaimSupplierViewModel : ModelBase
    {
        public ClaimSupplierViewModel()
        {
        }

        [Display(Name = "ClaimSupplier_Display_SupplierCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SupplierCode { get; set; }

        [Display(Name = "ClaimSupplier_Display_SupplierName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SupplierName { get; set; }

        [Display(Name = "ClaimSupplier_Display_ClaimRackCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ClaimRackCode { get; set; }
    }
}
