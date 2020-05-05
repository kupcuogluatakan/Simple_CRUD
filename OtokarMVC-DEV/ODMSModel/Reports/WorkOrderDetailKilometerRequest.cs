using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.UI;

namespace ODMSModel.Reports
{
    public class WorkOrderDetailKilometerRequest : ReportListModelBase
    {
        public WorkOrderDetailKilometerRequest([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {

            SetMapper(null);
        }

        public WorkOrderDetailKilometerRequest() { }

        public int GroupType { get; set; }
        public string GroupCode { get; set; }
        public int CustomerId { get; set; }
        [Display(Name = "PartExchangeReport_Display_ProcessTypeList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ProcessTypeList { get; set; }

        [Display(Name = "Customer_Display_CustomerTypeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustTypeList { get; set; }

        [Display(Name = "CustomerAddress_Display_CustomerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerIdList { get; set; }
        [Display(Name = "WorkOrderDetailReport_Display_ModelKodList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleModelList { get; set; }
    }
}
