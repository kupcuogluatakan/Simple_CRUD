using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.CountryVatRatio
{
    public class CountryVatRatioModelValidator : AbstractValidator<CountryVatRatioViewModel>
    {
        public CountryVatRatioModelValidator()
        {
            RuleFor(c => c.CountryId).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.PartVatRatio).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.LabourVatRatio).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.PartVatRatio).Must(CommonUtility.ValidateIntegerPart(3)).WithMessage(string.Format(MessageResource.Validation_IntegerLength,3));
            RuleFor(c => c.LabourVatRatio).Must(CommonUtility.ValidateIntegerPart(3)).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 3));
        }   
    }
}
