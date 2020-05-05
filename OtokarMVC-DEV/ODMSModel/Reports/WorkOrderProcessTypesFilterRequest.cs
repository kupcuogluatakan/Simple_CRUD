using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.Reports
{
    public class WorkOrderProcessTypesFilterRequest : ReportListModelBase
    {
        public WorkOrderProcessTypesFilterRequest([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {

            SetMapper(null);
        }

        public WorkOrderProcessTypesFilterRequest() { }
        [Display(Name = "WorkOrderProcessTypes_Dealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerIdList { get; set; }
        [Display(Name = "WorkOrderProcessTypes_Region", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RegionIdList { get; set; }
        [Display(Name = "WorkOrderProcessTypes_VehicleModel", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleModel { get; set; }
        [Display(Name = "WorkOrderProcessTypes_VehicleType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleType { get; set; }
        [Display(Name = "WorkOrderProcessTypes_StartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? StartDate { get; set; }
        [Display(Name = "WorkOrderProcessTypes_EndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EndDate { get; set; }
        [Display(Name = "WorkOrderProcessTypes_WorkOrderStatus", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WorkOrderStatus { get; set; }
        [Display(Name = "WorkOrderProcessTypes_Currency", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CurrencyCode { get; set; }
        [Display(Name = "WorkOrderProcessTypes_Group", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public short? GroupId { get; set; }
        [Display(Name = "WorkOrderProcessTypes_InGuarantee", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public short InGuarantee { get; set; }
        
    }
}
