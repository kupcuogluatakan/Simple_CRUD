using FluentValidation;

namespace ODMSModel.UserRole
{
    public class UserRoleViewModelValidator : AbstractValidator<UserRoleViewModel>
    {
        public UserRoleViewModelValidator()
        {
            RuleFor(c => c.UserId)
                .NotEqual(0)
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);
        }
    }
}
