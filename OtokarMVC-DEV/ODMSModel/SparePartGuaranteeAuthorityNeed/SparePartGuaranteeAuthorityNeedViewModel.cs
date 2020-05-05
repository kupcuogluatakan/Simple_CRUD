using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;

namespace ODMSModel.SparePartGuaranteeAuthorityNeed
{
    [Validator(typeof (SparePartGuaranteeAuthorityNeedViewModelValidator))]
    public class SparePartGuaranteeAuthorityNeedViewModel : ModelBase
    {
        public SparePartGuaranteeAuthorityNeedViewModel()
        {
        }

        public bool HideFormElements { get; set; }

        public int? PartId { get; set; }

        [Display(Name = "SparePart_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }

        [Display(Name = "SparePart_Display_PartName",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        [Display(Name = "SparePart_Display_GuaranteeAuthorityNeed",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public bool? GuaranteeAuthorityNeed { get; set; }
        [Display(Name = "SparePart_Display_GuaranteeAuthorityNeedName",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string GuaranteeAuthorityNeedName { get; set; }
    }
}
