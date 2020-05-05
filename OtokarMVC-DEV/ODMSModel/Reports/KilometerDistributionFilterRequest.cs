using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using ODMSModel.ListModel;


namespace ODMSModel.Reports
{
    public class KilometerDistributionFilterRequest : ReportListModelBase
    {
        public KilometerDistributionFilterRequest([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {

            SetMapper(null);
        }

        public KilometerDistributionFilterRequest() { }

        [Display(Name = "WorkOrderDetailReport_Display_StartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? BeginDate { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_EndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_DealerIdList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerIdList { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_ModelKodList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleModelList { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_DealerRegionIdList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RegionIdList { get; set; }

        [Display(Name = "PartExchangeReport_Display_ProcessTypeList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ProcessTypeList { get; set; }

        [Display(Name = "Customer_Display_CustomerTypeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustTypeList { get; set; }

        [Display(Name = "CustomerAddress_Display_CustomerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerIdList { get; set; }

        [Display(Name = "VehicleServiceDurationReport_Message_QuestioningBy", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int GroupType { get; set; }
        [Display(Name = "VehicleServiceDurationReport_Message_SumOfLastKM", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int SumOfLastKM { get; set; }

    }
}
