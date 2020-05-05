
namespace ODMSModel.SparePart
{
    public class SparePartSplitterXMLModel:ModelBase
    {
        public string GroupId { get; set; }
        public string RankNo { get; set; }

        public string OldPartId { get; set; }
        public string OldPartCode { get; set; }

        public string NewPartId { get; set; }
        public string NewPartCode { get; set; }

        public string CounterNo { get; set; }
        public string Quantity { get; set; }
        public string Usable { get; set; }

        public string CreateUser { get; set; }
        public string CreateDate { get; set; }

    }
}
