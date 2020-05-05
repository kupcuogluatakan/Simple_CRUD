
using System;

namespace ODMSModel.SparePart
{
    public class SparePartSupplyDiscountRatioXMLModel : ModelBase
    {
        public string SSID { get; set; }
        public int PartId { get; set; }
        public string PartCode { get; set; }
        public decimal DiscountRatio { get; set; }
        public DateTime? ValidStartDate { get; set; }
        public DateTime? ValidEndDate { get; set; }
        public string ChannelCode { get; set; }
        public new bool IsActive { get; set; }
    }
}
