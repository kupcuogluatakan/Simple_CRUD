using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.LabourTechnician
{
    public class LabourTechnicianViewModelValidator : AbstractValidator<LabourTechnicianViewModel>
    {
        public LabourTechnicianViewModelValidator()
        {
            RuleFor(c => c.WorkOrderDetailId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.LabourId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.StatusId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            //RuleFor(c => c.TecnicianUsers).NotNull().When(c=>c.NewTecnicianUsers == null).WithLocalizedMessage(() => MessageResource.Validation_Required);
            //RuleFor(c => c.NewTecnicianUsers).NotNull().When(c => c.TecnicianUsers == null).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.WorkTimeEstimate)
                .NotEmpty()
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.WorkTimeEstimate)
                .NotEqual(0)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.WorkTimeEstimate)
                .Must(o => o.GetValue<string>().Length <= 5)
                .WithMessage(string.Format(MessageResource.Validation_Length, 5));
            //RuleFor(c => c.WorkTimeReal)
            //     .NotEmpty().When(c => c.WorkshopPlanTypeId == 0)
            //    .WithLocalizedMessage(() => MessageResource.Validation_Required);
            //RuleFor(c => c.WorkTimeReal)
            //    .Must(o => o.GetValue<string>().Length <= 5).When(c => c.WorkshopPlanTypeId == 0)
            //    .WithMessage(string.Format(MessageResource.Validation_Length, 5));
            //RuleFor(c => c.WorkTimeReal)
            //    .NotEqual(0).When(c=>c.WorkshopPlanTypeId == 0)
            //    .WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
