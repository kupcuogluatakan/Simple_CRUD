using FluentValidation.Attributes;
using System.ComponentModel.DataAnnotations;
namespace ODMSModel.CustomerSparePartDiscount
{
    [Validator(typeof(CustomerSparePartDiscountViewModelValidator))]
    public class CustomerSparePartDiscountViewModel : ModelBase
    {
        public long CustomerSparePartDiscountId { get; set; }
        [Display(Name = "CriticalStockQuantity_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int DealerId { get; set; }
        [Display(Name = "CustomerAddress_Display_CustomerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public long CustomerId { get; set; }
        [Display(Name = "StockCard_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public long PartId { get; set; }
        [Display(Name = "Dealer_Sale_Sparepart_Display_DiscountRatio", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal DiscountRatio { get; set; }
        [Display(Name = "CustomerSparePartDiscount_Display_OrgTypeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? OrgTypeId { get; set; }
        [Display(Name = "CriticalStockQuantity_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }
        [Display(Name = "CustomerAddress_Display_CustomerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerName { get; set; }
        [Display(Name = "StockCard_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }
        [Display(Name = "CustomerSparePartDiscount_Display_OrgTypeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string OrgTypeName { get; set; }
        [Display(Name = "SparePart_Display_ClassCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SparePartClassCode { get; set; }
        [Display(Name = "CustomerSparePartDiscount_Display_IsApplicableToWorkOrder", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsApplicableToWorkOrderName { get; set; }
        [Display(Name = "CustomerSparePartDiscount_Display_IsApplicableToWorkOrder", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public bool IsApplicableToWorkOrder { get; set; }

    }
}

