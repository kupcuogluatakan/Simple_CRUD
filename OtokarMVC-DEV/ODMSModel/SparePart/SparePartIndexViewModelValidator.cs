using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSModel.SparePart
{
    public class SparePartIndexViewModelValidator : AbstractValidator<SparePartIndexViewModel>
    {
        public SparePartIndexViewModelValidator()
        {
            RuleFor(c => c.OriginalPartId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.PartCode).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.PartCode)
                .Length(0, 30)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.AdminDesc)
                .Length(0, 50)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.MultiLanguageContentAsText)
                .Must(CommonUtility.IsTurkishContentFilled)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.IsActive).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            // TFS NO : 29729 OYA
            //RuleFor(c => c.PartTypeCode).NotNull().When(c => !UserManager.UserInfo.IsDealer).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Unit).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required); 
            RuleFor(c => c.Brand)
                 .Length(0, 30)
                 .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.Brand).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.ShipQuantity).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.PartSection)
                .Length(0, 50)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.Weight).Must(CommonUtility.ValidateIntegerPart(4)).When(c => !UserManager.UserInfo.IsDealer).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 4));
            RuleFor(c => c.Weight).Must(o => o.ToString().Length <= 7).When(c => !UserManager.UserInfo.IsDealer).WithMessage(string.Format(MessageResource.Validation_Length, 7));
            RuleFor(c => c.Volume).Must(CommonUtility.ValidateIntegerPart(6)).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 6));
            RuleFor(c => c.Volume).Must(o => o.ToString().Length <= 6).WithMessage(string.Format(MessageResource.Validation_Length, 6));
            RuleFor(c => c.ShipQuantity).Must(CommonUtility.ValidateIntegerPartNullable(3)).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 3));
            RuleFor(c => c.ShipQuantity).Must(o => o.ToString().Length <= 6).WithMessage(string.Format(MessageResource.Validation_Length, 6));

            RuleFor(c => c.GuaranteeAuthorityNeed).NotNull().When(c => UserManager.UserInfo.IsDealer == false && c.IsOriginal == 1).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.LeadTime).NotEmpty().When(c => UserManager.UserInfo.IsDealer == false).WithLocalizedMessage(() => MessageResource.Validation_Required);

        }
    }
}
