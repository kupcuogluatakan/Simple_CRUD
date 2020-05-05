using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;
using System;

namespace ODMSModel.Fleet
{
    public class FleetViewModelValidator : AbstractValidator<FleetViewModel>
    {
        public FleetViewModelValidator()
        {
            
            RuleFor(c => c.FleetName).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.FleetName)
                .Length(0, 30)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);

            RuleFor(c => c.FleetCode).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.FleetCode)
                .Length(0, 20)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);

            RuleFor(c => c.OtokarPartDiscount).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.OtokarPartDiscount).LessThanOrEqualTo(100).WithLocalizedMessage(() => MessageResource.Validation_Invalid_DiscountRatio);
            RuleFor(c => c.OtokarPartDiscount).GreaterThanOrEqualTo(0).WithLocalizedMessage(() => MessageResource.Validation_Invalid_DiscountRatio);

            RuleFor(c => c.OtokarLabourDiscount).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.OtokarLabourDiscount).LessThanOrEqualTo(100).WithLocalizedMessage(() => MessageResource.Validation_Invalid_DiscountRatio);
            RuleFor(c => c.OtokarLabourDiscount).GreaterThanOrEqualTo(0).WithLocalizedMessage(() => MessageResource.Validation_Invalid_DiscountRatio);

            RuleFor(c => c.DealerPartDiscount).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.DealerPartDiscount).LessThanOrEqualTo(100).WithLocalizedMessage(() => MessageResource.Validation_Invalid_DiscountRatio);
            RuleFor(c => c.DealerPartDiscount).GreaterThanOrEqualTo(0).WithLocalizedMessage(() => MessageResource.Validation_Invalid_DiscountRatio);

            RuleFor(c => c.DealerLabourDiscount).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.DealerLabourDiscount).LessThanOrEqualTo(100).WithLocalizedMessage(() => MessageResource.Validation_Invalid_DiscountRatio);
            RuleFor(c => c.DealerLabourDiscount).GreaterThanOrEqualTo(0).WithLocalizedMessage(() => MessageResource.Validation_Invalid_DiscountRatio);

            RuleFor(c => c.EndDateValid).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.StartDateValid).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(c => c.IsConstricted).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);

            ////StartDateValid
            RuleFor(c => c.EndDateValid.GetValue<DateTime>())
                .GreaterThan(o => o.StartDateValid.GetValue<DateTime>()).WithName("EndDateValid")
                .WithMessage(string.Format(MessageResource.Validation_EndDateGreaterThanBeginDate,
                                           MessageResource.Announcement_Display_EndDate,
                                           MessageResource.Announcement_Display_StartDate));
        }
    }
}
