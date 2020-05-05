using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.LabourType
{
    public class LabourTypeDetailModelValidator : AbstractValidator<LabourTypeDetailModel>
    {
        public LabourTypeDetailModelValidator()
        {
            RuleFor(c => c.MultiLanguageContentAsText)
                .Must(CommonUtility.IsTurkishContentFilled)
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);
            
            //RuleFor(f => f.VatRatio)
            //    .NotEmpty()
            //    .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);

            RuleFor(f => f.Description)
                .NotEmpty()
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);

            RuleFor(f => f.Description)
                .Length(0,30)
                .WithMessage( string.Format(MessageResource.Validation_Length,30));

            RuleFor(c => c.MultiLanguageContentAsText)
                .Must(CommonUtility.IsTurkishContentFilled)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.MultiLanguageContentAsText)
                .Length(0,30)
                .WithMessage(string.Format(MessageResource.Validation_Length, 30));
        }
    }
}
