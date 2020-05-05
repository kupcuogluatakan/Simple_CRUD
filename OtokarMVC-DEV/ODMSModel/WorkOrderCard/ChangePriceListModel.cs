using System;
using FluentValidation.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.WorkOrderCard
{
    [Validator(typeof(ChangePriceListModelValidator))]
    public class ChangePriceListModel:ModelBase
    {
        public long WorkOrderId { get; set; }
        public long WorkOrderDetailId { get; set; }
        public string Type { get; set; }
        public long ItemId { get; set; }
        public DateTime WorkOrderDate { get; set; }
        public DateTime PriceListDate { get; set; }
        public decimal UnitPrice { get; set; }
    }
    [Validator(typeof(AddLabourPriceModellValidator))]
    public class AddLabourPrice : ModelBase
    {
        public long WorkOrderId { get; set; }
        public long WorkOrderDetailId { get; set; }
        public string Type { get; set; }
        public long ItemId { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
