using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.Customer
{
    public class CustomerIndexViewModelValidator : AbstractValidator<CustomerIndexViewModel>
    {
        public CustomerIndexViewModelValidator()
        {
            RuleFor(c => c.SAPCustomerSSID)
                .Length(0, 50)
                .WithLocalizedMessage(() => MessageResource.Validation_Length, 50);
            RuleFor(c => c.BOSCustomerSSID)
                .Length(0, 50)
                .WithLocalizedMessage(() => MessageResource.Validation_Length, 50);

            RuleFor(c => c.CustomerName).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.CustomerName)
                .Length(0, 100)
                .WithLocalizedMessage(() => MessageResource.Validation_Length,100);

            RuleFor(c => c.CustomerLastName).NotEmpty().When(c=>c.CustomerTypeId==2).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.CustomerLastName)
                .Length(0, 30).When(c=>c.CustomerTypeId == 2)
                .WithLocalizedMessage(() => MessageResource.Validation_Length, 30);

            RuleFor(c => c.CustomerTypeId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(c => c.CountryId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(c => c.TaxOffice).NotEmpty().When(c=>c.CustomerTypeId != 2).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.TaxOffice)
                .Length(0, 25).When(c => c.CustomerTypeId != 2)
                .WithLocalizedMessage(() => MessageResource.Validation_Length, 25);

            RuleFor(c => c.MobileNo).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(c => c.TaxNo).NotEmpty().When(c => c.CustomerTypeId != 2).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.TaxNo)
                .Length(0, 20).When(c => c.CustomerTypeId != 2)
                .WithLocalizedMessage(() => MessageResource.Validation_Length,20);

            RuleFor(c => c.TcIdentityNo).NotEmpty().When(c => c.CustomerTypeId == 2).When(c => c.CountryId == 1).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.TcIdentityNo)
                .Matches(@"^\d{11}$").When(c => c.CustomerTypeId == 2).When(c => c.CountryId == 1)
                .WithLocalizedMessage(() => MessageResource.Validation_TCIdentityNo_Format);
            RuleFor(c => c.TcIdentityNo)
                .Length(0, 11).When(c => c.CustomerTypeId == 2).When(c => c.CountryId == 1)
                .WithLocalizedMessage(() => MessageResource.Validation_Length,11);

            RuleFor(c => c.PassportNo).NotEmpty().When(c => c.CountryId != 1).When(c => c.CustomerTypeId == 2).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.PassportNo)
                .Length(0, 30).When(c => c.CustomerTypeId == 2)
                .WithLocalizedMessage(() => MessageResource.Validation_Length,30);
            
            RuleFor(c => c.IsActive).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(c => c.CompanyTypeId)
                .NotNull().When(c=>c.CustomerTypeId != 2)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(c => c.GovernmentTypeId)
                .NotNull().When(c=>c.CustomerTypeId == 3)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(c => c.WitholdingStatus)
                .NotNull().When(c => c.CustomerTypeId != 2)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.WitholdingId)
                .Must((m, c) =>
                {
                    int val;
                    int.TryParse(c, out val);
                    if (m.WitholdingStatus.GetValueOrDefault(0) == 1 && val == 0)
                        return false;
                    return true;
                }).When(c => c.CustomerTypeId != 2)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
