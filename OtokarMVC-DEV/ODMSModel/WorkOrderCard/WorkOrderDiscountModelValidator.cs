using FluentValidation;

namespace ODMSModel.WorkOrderCard
{
    public class WorkOrderDiscountModelValidator:AbstractValidator<WorkOrderDiscountModel>
    {
        public WorkOrderDiscountModelValidator()
        {
            RuleFor(c => c.WorkOrderId).NotEmpty();
            RuleFor(c => c.Type).NotEmpty();
            RuleFor(c => c.ItemId).NotEmpty();
            RuleFor(c => c.WorkOrderDetailId).NotEmpty();
            RuleFor(c => c.DiscountPrice).GreaterThanOrEqualTo(0).When(c => c.DiscountType == DiscountType.Money);
            RuleFor(c => c.DiscountRatio).GreaterThanOrEqualTo(0).When(c => c.DiscountType == DiscountType.Percentage);
            RuleFor(c => c.DiscountRatio).LessThanOrEqualTo(100).When(c => c.DiscountType == DiscountType.Percentage);
            RuleFor(c => c.DiscountPrice).Must(c => c.ToString().Length < 11);
            RuleFor(c => c.DiscountRatio).Must(c => c.ToString().Length < 6);
            RuleFor(c => c.Type).Must(c => c=="PART"||c=="LABOUR");
        }
    }
}
