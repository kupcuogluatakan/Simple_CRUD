using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.LabourCountryVatRatio
{
    public class LabourCountryVatRatioViewModelValidator : AbstractValidator<LabourCountryVatRatioViewModel>
    {
        public LabourCountryVatRatioViewModelValidator()
        {
            RuleFor(c => c.LabourId).NotEmpty().WithMessage(MessageResource.Validation_Required);                        
            RuleFor(c => c.CountryId).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.VatRatio).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.VatRatio.Value).Must(CommonUtility.ValidateIntegerPart(3)).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 3));
            RuleFor(c => c.VatRatio.Value).Must(o => o.ToString().Length <= 6).WithMessage(string.Format(MessageResource.Validation_Length, 6));
        }
    }
}
