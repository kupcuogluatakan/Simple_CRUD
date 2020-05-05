using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.WorkOrderDocuments
{
    public class WorkOrderDocumentsViewModelValidator: AbstractValidator<WorkOrderDocumentsViewModel>
    {
        public WorkOrderDocumentsViewModelValidator()
        {
            //Description
            RuleFor(q => q.Description)
                .Length(0, 250)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(q => q.Description).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(q => q.file).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
