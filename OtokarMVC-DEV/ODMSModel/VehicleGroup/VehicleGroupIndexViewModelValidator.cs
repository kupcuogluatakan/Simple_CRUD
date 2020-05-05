using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.VehicleGroup
{
    public class VehicleGroupIndexViewModelValidator : AbstractValidator<VehicleGroupIndexViewModel>
    {
        public VehicleGroupIndexViewModelValidator()
        {
            //AdminDesc
            RuleFor(c => c.AdminDesc)
                .NotEmpty()
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);
            RuleFor(c => c.AdminDesc).Length(0, 50).WithMessage(string.Format(MessageResource.Validation_Length, 50));
            //VatRatio
            /*RuleFor(c => c.VatRatio)
                .Matches(@"^\d{1,3}\,\d{1,2}$")
                .WithLocalizedMessage(() => MessageResource.Validation_Number);
            //MultiLanguageContentAsText*/
            RuleFor(m => m.MultiLanguageContentAsText)
                .Must(CommonUtility.IsTurkishContentFilled)
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);

        }
    }
}
