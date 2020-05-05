using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Reports
{
    public class SshReportFilterRequest : ReportListModelBase
    {
        #region ctors
        public SshReportFilterRequest(DataSourceRequest request) : base(request)
        {
            SetMapper(null);
        }
        public SshReportFilterRequest()
        {
        }
        #endregion

        #region public properties

        [Display(Name = "WorkOrderPerformanceReport_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]

        public string DealerIdList { get; set; }

        [Display(Name = "SSHReport_Display_DefectIdList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DefectIdList { get; set; }

        [Display(Name = "PersonnelInfoReport_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IsActive { get; set; }

        [Display(Name = "SSHReport_Display_ContractIdList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ContractIdList { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        [Display(Name = "SSHReport_Display_VehicleVinNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleVinNo { get; set; }
        [Display(Name = "SSHReport_Display_PoStatusList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? PoStatus { get; set; }

        #endregion
    }
}
