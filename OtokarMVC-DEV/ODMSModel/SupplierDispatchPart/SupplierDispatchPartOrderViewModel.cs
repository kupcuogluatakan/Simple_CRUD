using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.SupplierDispatchPart
{
    [Validator(typeof(SupplierDispatchPartOrderViewModelValidator))]
    public class SupplierDispatchPartOrderViewModel :ModelBase
    {
        public int SupplierId { get; set; }

        public int DeliveryId { get; set; }

        [Display(Name = "Delivery_Display_WayBillNo", ResourceType = typeof(MessageResource))]
        public string WayBillNo { get; set; }

        [Display(Name = "Delievery_Display_DeliveryDate", ResourceType = typeof(MessageResource))]
        public string WayBillDate { get; set; }

        [Display(Name = "PurchaseOrderDetail_Display_PoNumber", ResourceType = typeof(MessageResource))]
        public string PurchaseNo { get; set; }
    }
}
