using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSBusiness.Reports.Web
{
    public class WebReportBase<T> where T:class
    {
        public long Total { get; set; }
        public long FilteredTotal { get; set; }
        public List<T> Items { get; set; }

        public WebReportBase()
        {
            Items = new List<T>();
        }
    }
}
