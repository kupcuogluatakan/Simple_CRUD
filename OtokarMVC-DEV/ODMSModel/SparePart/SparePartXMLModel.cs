
namespace ODMSModel.SparePart
{
    public class SparePartXMLModel : ModelBase
    {
        public string PartCode { get; set; }
        public string PartClassCode { get; set; }
        public string UnitLookval { get; set; }
        public string Brand { get; set; }
        public string Weight { get; set; }
        public string Volume { get; set; }
        public string ShipQuantity { get; set; }
        public string NSN { get; set; }
        public string AdminDesc { get; set; }
        public string OrderAllow { get; set; }
        public string TerminDay { get; set; }
        public string AlternatePart { get; set; }
    }
}
