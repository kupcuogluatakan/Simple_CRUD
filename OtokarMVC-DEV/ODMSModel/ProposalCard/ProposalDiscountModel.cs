using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.ProposalCard
{
    [Validator(typeof(ProposalDiscountModelValidator))]
    public class ProposalDiscountModel : ModelBase
    {
        public long ProposalId { get; set; }
        public string Type { get; set; }
        public long ProposalDetailId { get; set; }
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
