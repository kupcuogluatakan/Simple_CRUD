using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.PurchaseOrderType
{
    public class PurchaseOrderTypeViewModelValidator : AbstractValidator<PurchaseOrderTypeViewModel>
    {
        public PurchaseOrderTypeViewModelValidator()
        {
            RuleFor(c => c.Description).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Description).Length(0, 100).WithLocalizedMessage(() => MessageResource.Validation_Length, 100);
            RuleFor(x => x.OrderReason).Length(0, 10).When(x => x.OrderReason != null).WithLocalizedMessage(() => MessageResource.Validation_Length, 10);
            RuleFor(x => x.ItemCategory).Length(0, 5).When(x => x.ItemCategory != null).WithLocalizedMessage(() => MessageResource.Validation_Length, 5);
            RuleFor(v => v.ProposalType).Length(0, 5).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(v => v.DeliveryPriority).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.MultiLanguageContentAsText).Must(CommonUtility.IsTurkishContentFilled).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.MultiLanguageContentAsText).Length(0, 100).WithLocalizedMessage(() => MessageResource.Validation_Length, 100);
            RuleFor(c => c.StockTypeId).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.IsDealerOrderAllow).NotEqual(false).When(c => !c.IsFirmOrderAllow && !c.IsSupplierOrderAllow).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.IsFirmOrderAllow).NotEqual(false).When(c => !c.IsDealerOrderAllow && !c.IsSupplierOrderAllow).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.IsSupplierOrderAllow).NotEqual(false).When(c => !c.IsFirmOrderAllow && !c.IsDealerOrderAllow).WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
