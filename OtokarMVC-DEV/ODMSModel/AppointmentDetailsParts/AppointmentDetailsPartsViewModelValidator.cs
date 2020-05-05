using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.AppointmentDetailsParts
{
    public class AppointmentDetailsPartsViewModelValidator : AbstractValidator<AppointmentDetailsPartsViewModel>
    {
        public AppointmentDetailsPartsViewModelValidator()
        {
            //RuleFor(c => c.Quantity)
            //   .Matches(@"^\d{1,45}$")
            //   .WithLocalizedMessage(() => MessageResource.Validation_Number);
            RuleFor(c => c.PartId).NotEmpty().WithMessage("*");
            RuleFor(c => c.Quantity).NotEmpty().WithMessage("*");
            RuleFor(c => c.Quantity)
               .Must(CommonUtility.ValidateIntegerPart(3))
               .WithMessage(string.Format(MessageResource.Validation_IntegerLength, 3));
            RuleFor(c => c.Quantity)
                .Must(o => o.ToString().Length <= 6)
                .WithMessage(string.Format(MessageResource.Validation_Length, 6));
            RuleFor(c => c.txtPartId).NotEmpty().WithMessage("*");
            RuleFor(c => c.PartSearch).NotEmpty().WithMessage("*");
        }
    }
}
