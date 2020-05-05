using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.Supplier
{
    public class SupplierViewModelValidator : AbstractValidator<SupplierViewModel>
    {
        public SupplierViewModelValidator()
        {
            //required
            
            RuleFor(c => c.DealerId).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.DealerId).GreaterThan(0).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.SupplierName).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.SupplierName).Length(0, 100).WithMessage(string.Format(MessageResource.Validation_Length, 100));

            RuleFor(c => c.TaxOffice).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required).When(x => x.DealerPoGroup != 13);
            //RuleFor(c => c.TaxOffice).Length(0, 25).WithMessage(string.Format(MessageResource.Validation_Length, 25));
            RuleFor(c => c.TaxNo).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required).When(x => x.DealerPoGroup != 13);
            RuleFor(c => c.TaxNo).Length(0, 20).WithMessage(string.Format(MessageResource.Validation_Length, 20));

            RuleFor(c => c.Url).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required).When(x => x.DealerPoGroup == 13);
            //RuleFor(c => c.Url).Length(0, 50).WithMessage(string.Format(MessageResource.Validation_Length, 50));
            RuleFor(c => c.Url).Matches(@"([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?").WithLocalizedMessage(() => MessageResource.Validation_InValidUrl);

            RuleFor(c => c.ContactPerson).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.ContactPerson).Length(0, 30).WithMessage(string.Format(MessageResource.Validation_Length, 30));

            RuleFor(c => c.Email).Length(0, 50).WithMessage(string.Format(MessageResource.Validation_Length, 30));
            RuleFor(c => c.Email).EmailAddress().WithLocalizedMessage(() => MessageResource.Validation_EMail_Format);

            RuleFor(c => c.Phone).Length(0, 20).WithMessage(string.Format(MessageResource.Validation_Length, 20));
            RuleFor(c => c.Phone).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Fax).Length(0, 20).WithMessage(string.Format(MessageResource.Validation_Length, 20)).When(x => x.DealerPoGroup != 13);
            RuleFor(c => c.MobilePhone).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(c => c.ChamberOfCommerce).Length(0, 50).WithMessage(string.Format(MessageResource.Validation_Length, 50)).When(x => x.DealerPoGroup != 13);
            RuleFor(c => c.TradeRegistryNo).Length(0, 15).WithMessage(string.Format(MessageResource.Validation_Length, 15)).When(x => x.DealerPoGroup != 13);

            RuleFor(c => c.CountryId).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.CountryId).GreaterThan(0).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.CityId).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.CityId).GreaterThan(0).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.TownId).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required).When(x => x.DealerPoGroup != 13);
            RuleFor(c => c.TownId).GreaterThan(0).WithLocalizedMessage(() => MessageResource.Validation_Required).When(x => x.DealerPoGroup != 13);

            RuleFor(c => c.ZipCode).Length(0, 5).WithMessage(string.Format(MessageResource.Validation_Length, 5)).When(x => x.DealerPoGroup != 13);
            RuleFor(c => c.Address).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Address).Length(0, 100).WithMessage(string.Format(MessageResource.Validation_Length, 100));
            

            //model specific
            
            
            //RuleFor(c=>c.ContactPerson).Matches(@"^[a-zA-Z ]+$").WithLocalizedMessage(() => MessageResource.Validation_AlphaNumericOnly);
        }
    }
}
