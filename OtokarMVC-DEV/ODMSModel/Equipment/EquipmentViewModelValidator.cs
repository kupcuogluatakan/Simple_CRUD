using FluentValidation;
using ODMSCommon;

namespace ODMSModel.Equipment
{
    public class EquipmentViewModelValidator : AbstractValidator<EquipmentViewModel>
    {
        public EquipmentViewModelValidator()
        {
            RuleFor(c => c.EquipmentTypeDesc)
                .NotEmpty()
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);

            RuleFor(c => c.MultiLanguageContentAsText)
                .Must(CommonUtility.IsTurkishContentFilled)
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);
        }
    }
}
