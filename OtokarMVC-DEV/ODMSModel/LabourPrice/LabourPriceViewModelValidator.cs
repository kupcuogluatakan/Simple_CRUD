using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.LabourPrice
{
    public class LabourPriceViewModelValidator : AbstractValidator<LabourPriceViewModel>
    {
        public LabourPriceViewModelValidator()
        {
            RuleFor(c => c.ModelCode).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.CurrencyCode).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.DealerClass).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.DealerRegionId).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.LabourPriceTypeId).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.ValidFromDate).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.ValidEndDate).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.ValidEndDate).GreaterThan(o => o.ValidFromDate).WithMessage(string.Format(MessageResource.Validation_EndDateGreaterThanBeginDate, MessageResource.LabourPrice_Display_ValidEndDate, MessageResource.LabourPrice_Display_ValidFromDate));
            
            RuleFor(c => c.HasTSUnitPrice).NotNull().WithLocalizedName(() => MessageResource.Validation_Required);
            RuleFor(c => c.HasTSUnitPrice).GreaterThan(0M).WithMessage(string.Format(MessageResource.Validation_GreaterThanZero, MessageResource.LabourPrice_Display_HasTSUnitPrice));
            RuleFor(c => c.HasTSUnitPrice)
                .Must(CommonUtility.ValidateIntegerPart(7))
                .WithMessage(string.Format(MessageResource.Validation_IntegerLength, 7));
            RuleFor(c => c.HasTSUnitPrice)
                .Must(o => o.ToString().Length <= 10)
                .WithMessage(string.Format(MessageResource.Validation_Length, 10));

            RuleFor(c => c.HasNoTSUnitPrice).NotNull().WithLocalizedName(() => MessageResource.Validation_Required);
            RuleFor(c => c.HasNoTSUnitPrice).GreaterThan(0M).WithMessage(string.Format(MessageResource.Validation_GreaterThanZero, MessageResource.LabourPrice_Display_HasNoTSUnitPrice));
            RuleFor(c => c.HasNoTSUnitPrice)
                .Must(CommonUtility.ValidateIntegerPart(7))
                .WithMessage(string.Format(MessageResource.Validation_IntegerLength, 7));
            RuleFor(c => c.HasNoTSUnitPrice)
                .Must(o => o.ToString().Length <= 10)
                .WithMessage(string.Format(MessageResource.Validation_Length, 10));
        }
    }
}
