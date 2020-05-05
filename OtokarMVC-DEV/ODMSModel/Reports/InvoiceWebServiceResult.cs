using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ODMSModel.Reports
{
    //[XmlRoot("Result")]
    public class InvoiceWebServiceResult
    {
        public int ErrorNo { get; set; }
        public string ErrorDesc { get; set; }
        [XmlArray("Invoices")]
        [XmlArrayItem("Invoice")]
        public List<InvoiceInfoReport> Items { get; set; }

        public InvoiceWebServiceResult()
        {
            Items = new List<InvoiceInfoReport>();
        }
    }
}
