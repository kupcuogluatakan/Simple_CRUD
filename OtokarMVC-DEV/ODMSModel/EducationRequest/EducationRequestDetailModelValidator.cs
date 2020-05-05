using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.EducationRequest
{
    public class EducationRequestDetailModelValidator : AbstractValidator<EducationRequestDetailModel>
    {
        public EducationRequestDetailModelValidator()
        {
            RuleFor(x => x.EducationCode)
               .NotNull()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(x => x.SerializedWorkerIds)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
