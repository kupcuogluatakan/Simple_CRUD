namespace ODMSModel.StockCardPriceListModel
{
    public class SparePartPriceXMLModel : ModelBase
    {
        public string SSID { get; set; }
        public string PartCode { get; set; }
        public string Price { get; set; }
        public string DateTimeEnd { get; set; }
        public string DateTimeStart { get; set; }
        public string CurrencyCode { get; set; }
        public string SSIDPriceList { get; set; }
        public new string IsActive { get; set; }
    }
}
