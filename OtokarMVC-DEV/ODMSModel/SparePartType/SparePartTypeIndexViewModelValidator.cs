using System.Text.RegularExpressions;
using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.SparePartType
{
    public class SparePartTypeIndexViewModelValidator : AbstractValidator<SparePartTypeIndexViewModel>
    {
        public SparePartTypeIndexViewModelValidator()
        {
            RuleFor(c => c.PartTypeCode).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.PartTypeCode)
                .Length(0, 2)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            Regex antiCharRgx = new Regex("^[a-zA-Z0-9]+$");
            RuleFor(c => c.PartTypeCode).Matches(antiCharRgx).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.AdminDesc).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.AdminDesc)
                .Length(0, 30)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.MultiLanguageContentAsText)
                .Must(CommonUtility.IsTurkishContentFilled)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.IsActive).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
