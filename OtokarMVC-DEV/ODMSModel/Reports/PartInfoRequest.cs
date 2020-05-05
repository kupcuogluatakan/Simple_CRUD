using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Reports
{
    public class PartInfoRequest : ReportListModelBase
    {
        public string DealerId { get; set; }
        public string DealerRegionId { get; set; }
        public string ProcessType { get; set; }
        public string Category { get; set; }
        public long PartId { get; set; }
        public string PartCode { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? MaxPrice { get; set; }
        public string Currency { get; set; }
        public string VehicleModel { get; set; }

        public PartInfoRequest([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {

            SetMapper(null);
        }

        public PartInfoRequest()
        {

        }

    }
}
