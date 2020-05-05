using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.WorkshopWorker
{
    public class WorkshopWorkerDetailModelValidator : AbstractValidator<WorkshopWorkerDetailModel>
    {
        public WorkshopWorkerDetailModelValidator()
        {
            RuleFor(x => x.WorkshopId)
                .NotEmpty()
                //.GreaterThan(0)
                .WithMessage(MessageResource.Validation_Required);

            RuleFor(x => x.WorkerId)
                .NotEmpty()
                .WithMessage( MessageResource.Validation_Required);

            RuleFor(x => x.StartDate)
                .NotEmpty()
                .WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(x => x.EndDate)
                .NotEmpty()
                .WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(x => x.StartDate)
                .Must((m,o)=>m.EndDate>=o)
                .WithMessage(string.Format(MessageResource.Validation_EndDateGreaterThanBeginDate, MessageResource.WorkHour_Display_StartDate, MessageResource.WorkHour_Display_EndDate));

        }
    }
}
