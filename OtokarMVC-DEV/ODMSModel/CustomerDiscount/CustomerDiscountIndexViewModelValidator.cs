using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.CustomerDiscount
{
    public class CustomerDiscountIndexViewModelValidator : AbstractValidator<CustomerDiscountIndexViewModel>
    {
        public CustomerDiscountIndexViewModelValidator()
        {
            RuleFor(c => c.IdCustomer).NotNull().WithLocalizedMessage(() => MessageResource.Validation_NotNull);
            RuleFor(c => c.PartDiscountRatio).NotNull().WithLocalizedMessage(() => MessageResource.Validation_NotNull);
            RuleFor(c => c.LabourDiscountRatio).NotNull().WithLocalizedMessage(() => MessageResource.Validation_NotNull);

            RuleFor(c => c.PartDiscountRatio.GetValueOrDefault()).LessThanOrEqualTo(100).WithName("PartDiscountRatio").WithLocalizedMessage(() => MessageResource.Validation_Invalid_DiscountRatio);
            RuleFor(c => c.PartDiscountRatio.GetValueOrDefault()).GreaterThanOrEqualTo(0).WithName("PartDiscountRatio").WithLocalizedMessage(() => MessageResource.Validation_Invalid_DiscountRatio);
            RuleFor(c => c.LabourDiscountRatio.GetValueOrDefault()).LessThanOrEqualTo(100).WithName("LabourDiscountRatio").WithLocalizedMessage(() => MessageResource.Validation_Invalid_DiscountRatio);
            RuleFor(c => c.LabourDiscountRatio.GetValueOrDefault()).GreaterThanOrEqualTo(0).WithName("LabourDiscountRatio").WithLocalizedMessage(() => MessageResource.Validation_Invalid_DiscountRatio);
            
        }
    }
}
