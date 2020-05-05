using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.VehicleCode
{
    public class VehicleCodeIndexViewModelValidator : AbstractValidator<VehicleCodeIndexViewModel>
    {
        public VehicleCodeIndexViewModelValidator()
        {
            //AdminDesc
            RuleFor(c => c.AdminDesc)
                .NotEmpty()
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);
            RuleFor(c => c.AdminDesc).Length(0, 100).WithMessage(string.Format(MessageResource.Validation_Length, 100));
            //MultiLanguageContentAsText
            RuleFor(m => m.MultiLanguageContentAsText)
                .Must(CommonUtility.IsTurkishContentFilled)
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);
        }


    }
}
