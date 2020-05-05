using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.StockMovement.Dtos
{
    public class PartSearchInfo:IResponse
    {
        public long PartId { get; set; }
        public string PartCode { get; set; }
        public decimal Quantity { get; set; }
    }
}
