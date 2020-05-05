using FluentValidation;
using ODMSModel.VehicleNoteProposal;

namespace ODMSModel.VehicleNote
{
    public class VehicleNotesProposalModelValidator : AbstractValidator<VehicleNotesProposalModel>
    {
        public VehicleNotesProposalModelValidator()
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
