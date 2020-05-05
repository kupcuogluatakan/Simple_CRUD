using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.Maintenance
{
    public class MaintenanceViewModelValidator : AbstractValidator<MaintenanceViewModel>
    {
        public MaintenanceViewModelValidator()
        {
            RuleFor(m => m.MaintTypeId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(m => m.MaintKM).NotNull().When(c => c.MaintTypeId == "IT_K" || c.MaintTypeId == "IT_P").WithLocalizedMessage(() => MessageResource.Maintenance_Error_KMMust);
            RuleFor(m => m.MaintKM).GreaterThan(0).When(c => c.MaintTypeId == "IT_K" || c.MaintTypeId == "IT_P").WithLocalizedMessage(() => MessageResource.Maintenance_Error_KMMust);

            RuleFor(m => m.VehicleTypeId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(c => c.MainCategoryId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(c => c.CategoryId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(c => c.SubCategoryId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(q => q.FailureCodeId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(q => q.AdminDesc).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(q => q.AdminDesc).Length(0, 50).WithMessage(string.Format(MessageResource.Validation_Length, 50));

            RuleFor(q => q.MaintName).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(q => q.EngineType).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(c => c.MultiLanguageContentAsText).Must(CommonUtility.IsTurkishContentFilled).WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
