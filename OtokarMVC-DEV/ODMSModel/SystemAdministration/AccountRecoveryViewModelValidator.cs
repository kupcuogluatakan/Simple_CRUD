using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.SystemAdministration
{
    public class AccountRecoveryViewModelValidator : AbstractValidator<AccountRecoveryViewModel>
    {
        public AccountRecoveryViewModelValidator()
        {
            RuleFor(c => c.UserName).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.IdentityNo).NotEmpty().WithLocalizedMessage(() => MessageResource.User_Validation_Required);
            RuleFor(c => c.Email).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
          
            RuleFor(c => c.Email)
                .EmailAddress()
                .WithLocalizedMessage(() => MessageResource.Validation_EMail_Format);
        }
    }
}
