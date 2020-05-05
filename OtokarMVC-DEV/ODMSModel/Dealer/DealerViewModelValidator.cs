using System;
using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.Dealer
{
    public class DealerViewModelValidator : AbstractValidator<DealerViewModel>
    {
        public DealerViewModelValidator()
        {

            RuleFor(c => c.SSID).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            //RuleFor(c => c.SSID).Length(10).WithMessage(string.Format(MessageResource.Validation_Length_Must, 10));
            RuleFor(c => c.BranchSSID).Length(1, 30).WithMessage(string.Format(MessageResource.Validation_Length, 30));
            RuleFor(c => c.ShortName).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.ShortName).Length(1,30).WithMessage(string.Format(MessageResource.Validation_Length,30));
            RuleFor(c => c.Name).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Name).Length(1, 100).WithMessage(string.Format(MessageResource.Validation_Length, 100));
            RuleFor(c => c.Ts12047CertificateDate).Must((m, o) =>
            {
                if (m.HasTs12047Certificate == false)
                    return true;
                if(m.HasTs12047Certificate)
                {
                    return (o.HasValue && o.Value > DateTime.Now.AddDays(1));
                }
                return true;
            }).WithLocalizedMessage(() => MessageResource.Validation_Dealer_Ts12047CertificateDate);

            RuleFor(c => c.Ts12047CertificateDate).NotNull().When(c => c.HasTs12047Certificate).WithLocalizedMessage(() => MessageResource.Validation_Dealer_Ts12047CertificateDate);


            RuleFor(c => c.DealerRegionId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);

            //Address 1
            RuleFor(c => c.Address1).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Address1).Length(1, 250).WithMessage(string.Format(MessageResource.Validation_Length, 250));
            RuleFor(c => c.Address2).Length(0, 250).WithMessage(string.Format(MessageResource.Validation_Length, 250));
            RuleFor(c => c.Country).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);

            //City
            RuleFor(c => c.City).NotEmpty().When(c => c.Country == 1).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.ForeignCity).NotEmpty().When(c => c.Country != 1).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.ForeignCity).NotEmpty().When(c => !c.City.HasValue || c.City == 0).WithLocalizedMessage(() => MessageResource.Validation_Required);
            
            // TaxNo
            RuleFor(c => c.TaxNo)
                .NotEmpty()
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.TaxNo)
                .Length(1, 20).When(c => c.Country == 1)
                .WithMessage( string.Format(MessageResource.Validation_Length, 20));
            RuleFor(c => c.TaxNo)
                .Length(1, 50).When(c => c.Country != 1)
                .WithMessage(string.Format(MessageResource.Validation_Length, 50));
            RuleFor(c => c.TaxNo).Matches(@"^[-,0-9]+$").WithLocalizedMessage(() => MessageResource.Validation_Number);

            //TaxOffice
            RuleFor(c => c.TaxOffice).Length(0, 25).WithMessage(string.Format(MessageResource.Validation_Length, 25));

            //Contract Email
            RuleFor(c => c.ContactEmail)
                .NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c=>c.ContactEmail)
                .EmailAddress().WithLocalizedMessage(() => MessageResource.Validation_EMail_Format);
            RuleFor(c => c.ContactEmail)
                .Length(1, 30)
                .WithMessage(string.Format(MessageResource.Validation_Length, 30));

            //Contract Name Surname
            RuleFor(c=>c.ContactNameSurname)
                .NotEmpty().WithLocalizedMessage(()=>MessageResource.Validation_Required);
            RuleFor(c => c.ContactNameSurname)
                .Length(0, 50)
                .WithMessage(string.Format(MessageResource.Validation_Length, 50));

            //Dealer Class code
            RuleFor(c => c.DealerClassCode)
                .NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.DealerClassCode)
               .Length(0,2).WithMessage(string.Format(MessageResource.Validation_Length,2));


            RuleFor(c => c.CustomerGroupLookVal)
                .NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c=>c.Fax)
               .Length(0, 20).WithMessage(string.Format(MessageResource.Validation_Length, 20));
            //RuleFor(c => c.Fax).Matches(@"^(0 (\d{3}) (\d{3}) (\d{2}) (\d{2}))$")
            //    .WithLocalizedMessage(() => MessageResource.Validation_Phone_Format);
            RuleFor(c => c.Phone)
               .NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Phone).Length(0, 20).WithMessage(string.Format(MessageResource.Validation_Length, 20));
            //RuleFor(c => c.Phone)
            //    .Matches(@"^(0 (\d{3}) (\d{3}) (\d{2}) (\d{2}))$")
            //    .WithLocalizedMessage(() => MessageResource.Validation_Phone_Format);
            RuleFor(c => c.WorkshopPlanType)
               .NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(c => c.SaleChannelCode).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.SaleChannelCode).Length(0,10).WithMessage(string.Format(MessageResource.Validation_Length,10));
            RuleFor(c => c.ClaimRatio).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.ClaimRatio)
                .Must(c => c <= 1)
                .WithLocalizedMessage(() => MessageResource.Validation_MaxValue,
                    ODMSCommon.CommonUtility.GetResourceValue("Dealer_Display_ClaimRatio"), 1);
            RuleFor(c => c.ClaimRatio).Must(o=>o.ToString().Length<=9).WithMessage(string.Format(MessageResource.Validation_Length,9));
            RuleFor(c => c.PurchaseOrderGroupId).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
        } 
    }
}
