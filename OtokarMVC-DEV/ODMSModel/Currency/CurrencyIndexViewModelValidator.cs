using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.Currency
{
    public class CurrencyIndexViewModelValidator : AbstractValidator<CurrencyIndexViewModel>
    {
        public CurrencyIndexViewModelValidator()
        {
            RuleFor(c => c.CurrencyCode).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.CurrencyCode)
                .Length(0, 3)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.AdminName).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.AdminName)
                .Length(0, 30)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.MultiLanguageContentAsText)
                .Must(CommonUtility.IsTurkishContentFilled)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.DecimalPartName)
                .Length(0, 15)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.ListOrder).GreaterThan(0).WithLocalizedMessage(() => MessageResource.Validation_GreaterThanZero, MessageResource.Currency_Display_ListOrder);
        }
    }
}
