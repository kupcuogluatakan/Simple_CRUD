using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;

namespace ODMSModel.Reports
{
    public class AlternatePartReportFilterRequest : ReportListModelBase
    {

        public AlternatePartReportFilterRequest(DataSourceRequest request) : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"PartCode", "SP.PART_CODE"},
                     {"PartName", "SPL.PART_NAME"},
                     {"AlternatePartCode", "ASP.PART_CODE"},
                     {"AlternatePartName", "ASPL.PART_NAME"}
                 };
            SetMapper(dMapper);
        }

        public AlternatePartReportFilterRequest()
        {

        }

        [Display(Name = "AlternatePartReport_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }

        [Display(Name = "AlternatePartReport_Display_PartName", ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }

        [Display(Name = "AlternatePartReport_Display_AlternatePartCode", ResourceType = typeof(MessageResource))]
        public string AlternatePartCode { get; set; }

        [Display(Name = "AlternatePartReport_Display_AlternatePartName", ResourceType = typeof(MessageResource))]
        public string AlternatePartName { get; set; }
    }
}
