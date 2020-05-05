namespace ODMSBusiness.Terminal.StockCardQuery.Dtos
{
    public class StockCardMainListItem
    {
        public string WarehouseName { get; set; }
        public decimal Quantity { get; set; }
        public string StockType { get; set; }
        public decimal BlockedQuantity { get; set; }
    }
}
