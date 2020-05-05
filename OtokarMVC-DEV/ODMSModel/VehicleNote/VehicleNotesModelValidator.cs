using FluentValidation;
namespace ODMSModel.VehicleNote
{
    public class VehicleNotesModelValidator : AbstractValidator<VehicleNotesModel>
    {
        public VehicleNotesModelValidator()
        {
           
            RuleFor(m => m.VehicleId)
                .NotEmpty()
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);
            RuleFor(m => m.Note)
                .NotEmpty()
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);
            RuleFor(m => m.Note)
                .Length(0, 500)
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Length);

        }
    }
}
