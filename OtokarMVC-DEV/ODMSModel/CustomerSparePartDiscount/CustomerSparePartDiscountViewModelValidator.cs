using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.CustomerSparePartDiscount
{
    public class CustomerSparePartDiscountViewModelValidator : AbstractValidator<CustomerSparePartDiscountViewModel>
    {
        public CustomerSparePartDiscountViewModelValidator()
        {
            //RuleFor(c => c.DealerId).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.CustomerId).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            //RuleFor(c => c.PartId).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.DiscountRatio).GreaterThan(0).WithLocalizedMessage(() => MessageResource.Validation_Invalid_DiscountRatio);
            RuleFor(c => c.DiscountRatio).LessThan(100).WithLocalizedMessage(() => MessageResource.Validation_Invalid_DiscountRatio);
        }
    }
}
