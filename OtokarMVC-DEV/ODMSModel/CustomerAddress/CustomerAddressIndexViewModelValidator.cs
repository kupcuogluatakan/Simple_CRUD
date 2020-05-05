using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.CustomerAddress
{
    public class CustomerAddressIndexViewModelValidator : AbstractValidator<CustomerAddressIndexViewModel>
    {
        public CustomerAddressIndexViewModelValidator()
        {
            RuleFor(c => c.CustomerId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.AddressTypeId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.CountryId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Address1).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(c => c.ZipCode).Length(0, 5).WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.Address1).Length(0, 50).WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.Address2).Length(0, 50).WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.Address3).Length(0, 50).WithLocalizedMessage(() => MessageResource.Validation_Length);
        }
    }
}
