using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.PDIVehicleResult
{
    public class PDIVehicleResultViewModelValidator : AbstractValidator<PDIVehicleResultViewModel>
    {
        public PDIVehicleResultViewModelValidator()
        {
            RuleFor(m => m.ApprovalNote).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
