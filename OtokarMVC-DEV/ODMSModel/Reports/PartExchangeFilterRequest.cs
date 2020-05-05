using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.Reports
{
    public class PartExchangeFilterRequest : ReportListModelBase
    {
        public PartExchangeFilterRequest([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {

            SetMapper(null);
        }

        public PartExchangeFilterRequest() { }
        [Display(Name = "WorkOrderDetailReport_Display_StartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? StartDate { get; set; }
        [Display(Name = "WorkOrderDetailReport_Display_EndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EndDate { get; set; }
        [Display(Name = "WorkOrderDetailReport_Display_DealerIdList",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerIdList { get; set; }
        [Display(Name = "WorkOrderDetailReport_Display_DealerRegionIdList",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerRegionIdList { get; set; }
        [Display(Name = "WorkOrderDetailReport_Display_ModelKodList",
         ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleModelList { get; set; }
        [Display(Name = "PartExchangeReport_Display_ProcessTypeList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ProcessTypeList { get; set; }
        [Display(Name = "PartExchangeReport_Display_CurrencyCodeList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CurrencyCodeList { get; set; }
        [Display(Name = "PartExchangeReport_Display_GifCostCenterList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string GifCostCenterList { get; set; }
        [Display(Name = "PartExchangeReport_Display_MaxPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? MaxPrice { get; set; }
        [Display(Name = "CampaignPart_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }
        public int PartId { get; set; }
        
    }
}
