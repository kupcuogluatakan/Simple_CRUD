using FluentValidation;

namespace ODMSModel.WorkOrderCard
{
    public class ChangePriceListModelValidator : AbstractValidator<ChangePriceListModel>
    {
        public ChangePriceListModelValidator()
        {
            RuleFor(c => c.WorkOrderId).NotEmpty();
            RuleFor(c => c.Type).NotEmpty();
            RuleFor(c => c.ItemId).NotEmpty();
            RuleFor(c => c.WorkOrderDetailId).NotEmpty();
            RuleFor(c => c.PriceListDate).NotEmpty();
            RuleFor(c => c.Type).Must(c => c == "PART" || c == "LABOUR");    
        }
    }
    public class AddLabourPriceModellValidator : AbstractValidator<AddLabourPrice>
    {
        public AddLabourPriceModellValidator()
        {
            RuleFor(c => c.WorkOrderId).NotEmpty();
            RuleFor(c => c.Type).NotEmpty();
            RuleFor(c => c.ItemId).NotEmpty();
            RuleFor(c => c.WorkOrderDetailId).NotEmpty();
            RuleFor(c => c.Type).Must(c => c == "PART" || c == "LABOUR");
        }
    }
    
}
