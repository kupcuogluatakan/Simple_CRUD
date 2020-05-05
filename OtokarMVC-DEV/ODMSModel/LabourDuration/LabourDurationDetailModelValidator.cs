using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.LabourDuration
{
    public class LabourDurationDetailModelValidator : AbstractValidator<LabourDurationDetailModel>
    {
        public LabourDurationDetailModelValidator()
        {
            RuleFor(x => x.LabourId)
                .NotEmpty()
                .WithLocalizedMessage(() =>MessageResource.Validation_Required);
            RuleFor(x => x.VehicleEngineTypeIdList)
                .NotEmpty()
                .WithLocalizedMessage(() =>MessageResource.Validation_Required);

            RuleFor(x => x.VehicleModelId)
                .NotEmpty()
                .WithLocalizedMessage(() =>MessageResource.Validation_Required);

            RuleFor(x => x.Duration)
                .NotEmpty()
                .WithLocalizedMessage(() =>MessageResource.Validation_Required);
            RuleFor(x => x.Duration)
                .Must(c=>c.ToString().Length<=4)
                .WithMessage(string.Format(MessageResource.Validation_Length,4));
            RuleFor(c => c.Duration).GreaterThan(0).WithLocalizedMessage(() => MessageResource.Validation_GreaterThanZero,MessageResource.LabourDuration_Display_Duration);
        }
    }
}
