using FluentValidation;

namespace ODMSModel.Workshop
{
    public class WorkshopDetailModelValidator : AbstractValidator<WorkshopDetailModel>
    {
        public WorkshopDetailModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);

            RuleFor(x => x.DealerId)
                .GreaterThan(0)
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);
        }
    }
}
