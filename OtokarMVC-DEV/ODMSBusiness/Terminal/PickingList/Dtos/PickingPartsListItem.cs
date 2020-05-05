namespace ODMSBusiness.Terminal.PickingList.Dtos
{
    using System;
    public class PickingPartsListItem
    {
        public long PickicngDetailId { get; set; }
        public long PartId { get; set; }
        public string  PartCode { get; set; }
        public decimal AcceptedQuantity { get; set; }
        public string StockType { get; set; }
        public int StockTypeId { get; set; }
        public decimal LeftQuantity { get; set; }
    }
}
