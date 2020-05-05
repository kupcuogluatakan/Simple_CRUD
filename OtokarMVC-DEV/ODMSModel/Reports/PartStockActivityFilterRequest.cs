using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Reports
{
    public class PartStockActivityFilterRequest : ReportListModelBase
    {
        public PartStockActivityFilterRequest([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {

            SetMapper(null);
        }

        public PartStockActivityFilterRequest()
        {
        }

        [Display(Name = "PartStockReport_Dealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerIdList { get; set; }
        [Display(Name = "ChargePerCarReport_InGuarantee", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int InGuarantee { get; set; }
        [Display(Name = "ChargePerCarReport_Year", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int Year { get; set; }
        [Display(Name = "PartStockActivityFilterRequest_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode  { get; set; }
        [Display(Name = "PartStockActivityFilterRequest_ProcessType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ProcessType { get; set; }
    }
}
