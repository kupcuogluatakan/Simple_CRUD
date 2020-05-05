using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.WorkshopType
{
    public class WorkshopTypeModelValidator : AbstractValidator<WorkshopTypeViewModel>
    {
        public WorkshopTypeModelValidator()
        {
            //Desc
            RuleFor(m => m.Descripion)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Descripion)
                .Length(0, 300)
                .WithMessage(string.Format(MessageResource.Validation_Length, 300));
            //ML
            RuleFor(m => m.WorkshopTypelName)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(m => m.MultiLanguageContentAsText)
                .Must(CommonUtility.IsTurkishContentFilled)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
