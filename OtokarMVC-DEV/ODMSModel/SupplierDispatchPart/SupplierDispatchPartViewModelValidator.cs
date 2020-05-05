using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.SupplierDispatchPart
{
    public class SupplierDispatchPartViewModelValidator : AbstractValidator<SupplierDispatchPartViewModel>
    {
        public SupplierDispatchPartViewModelValidator()
        {
            RuleFor(c => c.PartId).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);

            
            RuleFor(c => c.Qty).Must(CommonUtility.ValidateIntegerPartNullable(5)).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 5));
            RuleFor(c => c.Qty).Must(o => o.ToString().Length <= 8).WithMessage(string.Format(MessageResource.Validation_Length, 8));
            
            RuleFor(c => c.InvoicePrice).Must(CommonUtility.ValidateIntegerPartNullable(7)).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 7));
            RuleFor(c => c.InvoicePrice).Must(o => o.ToString().Length <= 10).WithMessage(string.Format(MessageResource.Validation_Length, 10));         
        }
    }
}
