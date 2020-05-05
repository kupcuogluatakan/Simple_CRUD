using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.Labour
{
    public class LabourViewModelValidator : AbstractValidator<LabourViewModel>
    {
        public LabourViewModelValidator()
        {
            RuleFor(c => c.LabourTypeId).NotEmpty().WithMessage(MessageResource.Validation_Required);
            //AdminDesc
            RuleFor(m => m.AdminDesc)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.AdminDesc)
                .Length(0, 50)
                .WithMessage(string.Format(MessageResource.Validation_Length, 50));
            //LabourCode
            RuleFor(m => m.LabourCode)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(m => m.LabourCode)
                .Length(0, 30)
                .WithMessage(string.Format(MessageResource.Validation_Length, 30));
            //LabourName
            RuleFor(m => m.LabourName)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(m => m.MultiLanguageContentAsText)
                .Must(CommonUtility.IsTurkishContentFilled)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            //RepairCode
            RuleFor(m => m.RepairCode)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(m => m.RepairCode)
                .Length(0, 6)
                .WithMessage(string.Format(MessageResource.Validation_Length, 6));
            //LabourMainGroupId
            RuleFor(m => m.LabourMainGroupId)
               .NotNull()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            //LabourSubGroupId
            RuleFor(m => m.LabourSubGroupId)
               .NotNull()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);          

        }
    }
}
