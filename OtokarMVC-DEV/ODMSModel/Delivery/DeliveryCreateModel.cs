using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.Delivery
{
    [Validator(typeof(DeliveryViewModelValidator))]
    public class DeliveryCreateModel : ModelBase
    {
        [Display(Name = "Supplier_Display_SupplierName", ResourceType = typeof(MessageResource))]
        public string SupplierName { get; set; }
        [Display(Name = "Supplier_Display_SupplierName", ResourceType = typeof(MessageResource))]
        public long SupplierId { get; set; }
        [Display(Name = "Appointment_Display_Status", ResourceType = typeof(MessageResource))]
        public new string Status { get; set; }
        [Display(Name = "Appointment_Display_Status", ResourceType = typeof(MessageResource))]
        public new int? StatusId { get; set; }
        [Display(Name = "Delivery_Display_WayBillNo", ResourceType = typeof(MessageResource))]
        public string WayBillNo { get; set; }
        [Display(Name = "Delievery_Display_DeliveryDate", ResourceType = typeof(MessageResource))]
        public DateTime? WayBillDate { get; set; }
        [Display(Name = "DealerPurchaseOrder_Display_TotalPrice", ResourceType = typeof(MessageResource))]
        public decimal TotalPrice { get; set; }
        [Display(Name = "Delivery_Display_SapDeliveryNo", ResourceType = typeof(MessageResource))]
        public string SapDeliveryNo { get; set; }
        public long DeliveryId { get; set; }
        [Display(Name = "PurchaseOrderDetail_Display_PoNumber", ResourceType = typeof(MessageResource))]
        public string PurchaseNo { get; set; }
        [Display(Name = "WorkorderListInvoices_Display_InvSerialNo", ResourceType = typeof(MessageResource))]
        public string InvoiceSerialNo { get; set; }
        [Display(Name = "WorkorderListInvoices_Display_InvNo", ResourceType = typeof(MessageResource))]
        public string InvoiceNo { get; set; }
        [Display(Name = "WorkorderListInvoices_Display_InvDate", ResourceType = typeof(MessageResource))]
        public DateTime? InvoiceDate { get; set; }
        public bool HasDeleteItem { get; set; }        

    }
}
