using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Reports
{
    public class WorkOrderCardNumericalOverviewReportFilterRequest:ReportListModelBase
    {
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string DealerIdList { get; set; }
        public string DealerRegionIdList { get; set; }
        public string VehicleModelList { get; set; }
        public string VehicleTypeList { get; set; }
        public string WorkOrderStatIdList { get; set; }
        public string CurrencyCode { get; set; }
        public string WorkOrderQueryType { get; set; }
        public int? IsInWarranty { get; set; }

    }
}
