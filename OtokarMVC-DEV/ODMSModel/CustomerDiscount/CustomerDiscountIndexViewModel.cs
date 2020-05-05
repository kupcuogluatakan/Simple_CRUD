using FluentValidation.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.CustomerDiscount
{
    [Validator(typeof(CustomerDiscountIndexViewModelValidator))]
    public class CustomerDiscountIndexViewModel : ModelBase
    {
        public CustomerDiscountIndexViewModel()
        { 
        }
        public bool HideFormElements { get; set; }
        public string MultiLanguageContentAsText { get; set; }

        //IdCustomer
        [Display(Name = "CustomerDiscount_Display_IdCustomer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int64? IdCustomer { get; set; }

        //IdDealer
        [Display(Name = "CustomerDiscount_Display_IdDealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IdDealer { get; set; }

        //CustomerName
        [Display(Name = "CustomerDiscount_Display_CustomerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerName { get; set; }

        //DealerName
        [Display(Name = "CustomerDiscount_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }

        //PartDiscountRation
        [Display(Name = "CustomerDiscount_Display_PartDiscountRation", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? PartDiscountRatio { get; set; }

        //LabourDiscountRation
        [Display(Name = "CustomerDiscount_Display_LabourDiscountRation", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? LabourDiscountRatio { get; set; }

    }
}
