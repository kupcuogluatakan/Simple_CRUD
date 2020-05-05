using FluentValidation;
using ODMSCommon.Resources;
namespace ODMSModel.VehicleNoteApprove
{
    public class VehicleNoteApproveModelValidator : AbstractValidator<VehicleNoteApproveModel>
    {
        public VehicleNoteApproveModelValidator()
        {
            RuleFor(m => m.Note).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(m => m.Note).Length(0, 500).WithLocalizedMessage(() => MessageResource.Validation_Length);            
            RuleFor(c => c.VehicleKm).LessThanOrEqualTo(100000000).WithLocalizedMessage(() => MessageResource.Validation_Length, 100000000);

        }
    }
}
