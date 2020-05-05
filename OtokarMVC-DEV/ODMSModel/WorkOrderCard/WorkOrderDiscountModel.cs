using FluentValidation.Attributes;

namespace ODMSModel.WorkOrderCard
{ 
    [Validator(typeof(WorkOrderDiscountModelValidator))]
    public class WorkOrderDiscountModel:ModelBase
    {
        public long WorkOrderId { get; set; }
        public string Type { get; set; }
        public long WorkOrderDetailId { get; set; }
        public long ItemId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Duration { get; set; }
        public decimal ListPrice { get; set; }
        public decimal DealerPrice { get; set; }
        public decimal DiscountRatio { get; set; }
        public decimal DiscountPrice { get; set; }
        public decimal WarrantyRatio { get; set; }
        public decimal WarrantyPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal VatRatio { get; set; }
        public DiscountType DiscountType { get; set; }
        public bool DisableDiscount { get; set; }
        public decimal TotalFleetDiscountRate { get; set; }
    }
    public enum DiscountType { Percentage, Money }
}
