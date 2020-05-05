using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.DealerSaleChannelDiscountRatio
{
    public class DealerSaleChannelDiscountRatioViewModelValidator : AbstractValidator<DealerSaleChannelDiscountRatioViewModel>
    {
        public DealerSaleChannelDiscountRatioViewModelValidator()
        {
            RuleFor(v => v.DealerClassCode).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(v => v.ChannelCode).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(v => v.SparePartClassCode).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(v => v.TseInvalidDiscountRatio).NotEqual(0).WithMessage(MessageResource.Validation_Required);
            RuleFor(v => v.TseValidDiscountRatio).NotEqual(0).WithMessage(MessageResource.Validation_Required);
            RuleFor(v => v.DealerClassCode).Length(0, 2).WithMessage(string.Format(MessageResource.Validation_Length, 2));
            RuleFor(v => v.ChannelCode).Length(0, 10).WithMessage(string.Format(MessageResource.Validation_Length, 10));
            RuleFor(v => v.SparePartClassCode).Length(0, 3).WithMessage(string.Format(MessageResource.Validation_Length, 3));
            RuleFor(c => c.TseInvalidDiscountRatio).ExclusiveBetween(0, 101).WithLocalizedMessage(() => MessageResource.Validation_Invalid_DiscountRatio);
            RuleFor(c => c.TseInvalidDiscountRatio).Must(CommonUtility.ValidateIntegerPartNullable(3)).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 3));
            RuleFor(c => c.TseInvalidDiscountRatio).Must(o => o.ToString().Length <= 6).WithMessage(string.Format(MessageResource.Validation_Length, 6));
            RuleFor(c => c.TseValidDiscountRatio).ExclusiveBetween(0, 101).WithLocalizedMessage(() => MessageResource.Validation_Invalid_DiscountRatio);
            RuleFor(c => c.TseValidDiscountRatio).Must(CommonUtility.ValidateIntegerPartNullable(3)).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 3));
            RuleFor(c => c.TseValidDiscountRatio).Must(o => o.ToString().Length <= 6).WithMessage(string.Format(MessageResource.Validation_Length, 6));
        
        }
    }
}
