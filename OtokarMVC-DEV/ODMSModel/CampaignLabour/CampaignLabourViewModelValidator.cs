using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.CampaignLabour
{
    public class CampaignLabourViewModelValidator : AbstractValidator<CampaignLabourViewModel>
    {
        public CampaignLabourViewModelValidator()
        {
            RuleFor(c => c.CampaignCode).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.CampaignCode)
                .Length(0, 8)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.LabourId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required); 
            RuleFor(c => c.Quantity).GreaterThan(0).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Quantity).Must(CommonUtility.ValidateIntegerPartNullable(3)).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 3));
            RuleFor(c => c.Quantity).Must(o => o.ToString().Length <= 6).WithMessage(string.Format(MessageResource.Validation_Length, 6));
        
        }
    }
}
