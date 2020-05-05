using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.UI;

namespace ODMSModel.Reports
{
    public class ChargeWorkOrderDetailFilterRequest : ReportListModelBase
    {
        public ChargeWorkOrderDetailFilterRequest([DataSourceRequest] DataSourceRequest request)
           : base(request)
        {

            SetMapper(null);
        }

        public ChargeWorkOrderDetailFilterRequest()
        {

        }
        public int DealerId { get; set; }
        public int DealerRegionId { get; set; }
        public int CustomerId { get; set; }
        public int VehicleTypeId { get; set; }
        public string ModelCode { get; set; }


        public string ProcessTypeIdList { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Currency { get; set; }
        public string VinNo { get; set; }
        public int InGuarantee { get; set; }

        public DateTime? GuaranteeConfirmDate { get; set; }
        public string GuaranteeCategories { get; set; }

    }
}
