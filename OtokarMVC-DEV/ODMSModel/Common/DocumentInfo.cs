
namespace ODMSModel.Common
{
    public class DocumentInfo : ModelBase
    {
        public int DocId { get; set; }
        public string DocMimeType { get; set; }
        public string DocName { get; set; }
        public byte[] DocBinary { get; set; }
    }
}
