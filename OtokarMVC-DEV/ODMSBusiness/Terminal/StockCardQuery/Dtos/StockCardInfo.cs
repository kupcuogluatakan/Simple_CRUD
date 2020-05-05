using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.StockCardQuery.Dtos
{
    public class StockCardInfo:IResponse
    {
        public string PartCode { get; set; }
        public long PartId { get; set; }
        public string PartName { get; set; }
        public decimal Quantity { get; set; }
        public EnumerableResponse<StockCardMainListItem> MainList { get; set; }
        public EnumerableResponse<StockCardDetailListItem> DetailList { get; set; }
    }
}
