using System;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;

namespace ODMSModel.Delivery
{
    public class DeliveryViewModel :ModelBase
    {
        [Display(Name = "Delivery_Display_DeliveryId", ResourceType = typeof(MessageResource))]
        public long DeliveryId { get; set; }
        [Display(Name = "CycleCount_Display_DealerName", ResourceType = typeof(MessageResource))]
        public int DealerId { get; set; }
        [Display(Name = "CycleCount_Display_DealerName", ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }
        [Display(Name = "Supplier_Display_SupplierName", ResourceType = typeof(MessageResource))]
        public int SupplierId { get; set; }
        [Display(Name = "Supplier_Display_SupplierName", ResourceType = typeof(MessageResource))]
        public string SupplierName { get; set; }
        [Display(Name = "Delivery_Display_SenderDealerName", ResourceType = typeof(MessageResource))]
        public int SenderDealerId { get; set; }
        [Display(Name = "Delivery_Display_SenderDealerName", ResourceType = typeof(MessageResource))]
        public string SenderDealerName { get; set; }
        [Display(Name = "DeliveryList_Display_DeliveryStatusName", ResourceType = typeof(MessageResource))]
        public string Statu { get; set; }
        [Display(Name = "DeliveryList_Display_DeliveryStatusName", ResourceType = typeof(MessageResource))]
        public int? StatuId { get; set; }
        [Display(Name = "DeliveryList_Display_SapDeliveryNo", ResourceType = typeof(MessageResource))]
        public string SapDeliveryNo { get; set; }
        [Display(Name = "DeliveryList_Display_WaybillNo", ResourceType = typeof(MessageResource))]
        public string WaybillNo { get; set; }
        [Display(Name = "DeliveryList_Display_WaybillDate", ResourceType = typeof(MessageResource))]
        public DateTime? WaybillDate { get; set; }
        [Display(Name = "Delivery_Display_IsPlaced", ResourceType = typeof(MessageResource))]
        public bool IsPlaced { get; set; }
        [Display(Name = "Delivery_Display_AcceptedUser", ResourceType = typeof(MessageResource))]
        public int AcceptedUserId { get; set; }
        [Display(Name = "Delivery_Display_AcceptedUser", ResourceType = typeof(MessageResource))]
        public string AcceptedUser { get; set; }
        [Display(Name = "WorkorderListInvoices_Display_InvSerialNo", ResourceType = typeof(MessageResource))]
        public string InvoiceSerialNo { get; set; }
        [Display(Name = "WorkorderListInvoices_Display_InvNo", ResourceType = typeof(MessageResource))]
        public string InvoiceNo { get; set; }
        [Display(Name = "WorkorderListInvoices_Display_InvDate", ResourceType = typeof(MessageResource))]
        public DateTime? InvoiceDate { get; set; }








        //[Display(Name = "Delivery_Display_DeliverySequenceNo", ResourceType = typeof(MessageResource))]
        //public long DeliverySequenceNo  { get; set; }
        //[Display(Name = "Delivery_Display_DeliveryId", ResourceType = typeof(MessageResource))]
        //public long DeliveryId { get; set; }
        //[Display(Name = "PartDefinition_Display_PartName", ResourceType = typeof(MessageResource))]
        //public long PartId { get; set; }
        //[Display(Name = "PartDefinition_Display_PartName", ResourceType = typeof(MessageResource))]
        //public string PartName { get; set; }
        //[Display(Name = "StockTypeDetail_Display_IdStockType", ResourceType = typeof(MessageResource))]
        //public int StockTypeId { get; set; }
        //[Display(Name = "StockTypeDetail_Display_IdStockType", ResourceType = typeof(MessageResource))]
        //public string StockType { get; set; }
        //[Display(Name = "PurchaseOrderDetail_Display_SequenceNo", ResourceType = typeof(MessageResource))]
        //public long PurchaseOrderSequenceNo { get; set; }
        //[Display(Name = "Delivery_Display_SapOfferNo", ResourceType = typeof(MessageResource))]
        //public string SapOfferNo { get; set; }
        //[Display(Name = "Delivery_Display_SapRowNo", ResourceType = typeof(MessageResource))]
        //public int SapRowNo { get; set; }
        //[Display(Name = "Delivery_Display_ShipQuantity", ResourceType = typeof(MessageResource))]
        //public decimal ShipQunatity { get; set; }
        //[Display(Name = "Delivery_Display_RecievedQuantity", ResourceType = typeof(MessageResource))]
        //public decimal RecievedQuantity{ get; set; }
        //[Display(Name = "Delivery_Display_InvoicePrice", ResourceType = typeof(MessageResource))]
        //public decimal InvoicePrice { get; set; }
    }
}
