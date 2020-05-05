using System;

namespace ODMSModel.DeliveryListPart
{
    public class DeliveryListPartSubViewModel : ModelBase
    {
        public Int64 DeliverySeqNo { get; set; }
        public Int64 DeliveryId { get; set; }
        public int Quantity { get; set; }
        public decimal ShipQnty { get; set; }
        public decimal ReceiveQnty { get; set; }
        public long PoDetSeqNo { get; set; }
        public string SapOfferNo { get; set; }
        public string SapRowNo { get; set; }
        public string SapOriginalRowNo { get; set; }
        public decimal InvoicePrice { get; set; }
        public string DefaultType { get; set; }

        public int WarehouseId { get; set; }
        public int RackId { get; set; }
        public int StockTypeId { get; set; }
        public decimal Qty { get; set; }
        public long PartId { get; set; }
        public int DealerId { get; set; }
        public decimal DealerPrice { get; set; }
        public string TransactionDescription { get; set; }
    }
}
