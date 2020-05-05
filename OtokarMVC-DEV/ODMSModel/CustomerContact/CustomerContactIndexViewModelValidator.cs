using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.CustomerContact
{
    public class CustomerContactIndexViewModelValidator : AbstractValidator<CustomerContactIndexViewModel>
    {
        public CustomerContactIndexViewModelValidator()
        {
            RuleFor(c => c.CustomerId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.ContactTypeId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Name).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Surname).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.ContactTypeValue).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Name)
                .Length(0, 50)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.Surname)
                .Length(0, 50)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.ContactTypeValue)
                .Length(0, 50)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.ContactTypeValue)
                .EmailAddress().When(c => c.ContactTypeId == 1)
                .WithLocalizedMessage(() => MessageResource.Validation_EMail_Format);
        }
    }
}
