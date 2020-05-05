using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.SaleOrder
{
    public class SaleOrderRemainingFilter : ReportListModelBase
    {
        public SaleOrderRemainingFilter(DataSourceRequest request) : base(request)
        {
            SetMapper(null);
        }
        public SaleOrderRemainingFilter()
        {

        }
        [Display(Name = "SaleOrderRemaining_Display_CustomerName", ResourceType = typeof(MessageResource))]
        public int? CustomerId { get; set; }
        [Display(Name = "SaleOrderRemaining_Display_SaleOrderType", ResourceType = typeof(MessageResource))]
        public int? PurchaseOrderType { get; set; }
        [Display(Name = "SaleOrderRemaining_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "SaleOrderRemaining_Display_PartName", ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }
        [Display(Name = "SaleOrderRemaining_Display_PartType", ResourceType = typeof(MessageResource))]
        public int? PartType { get; set; }
        [Display(Name = "SaleOrderRemaining_Display_BeginDate", ResourceType = typeof(MessageResource))]
        public DateTime? BeginDate { get; set; }
        [Display(Name = "SaleOrderRemaining_Display_EndDate", ResourceType = typeof(MessageResource))]
        public DateTime? EndDate { get; set; }
        [Display(Name = "SaleOrderRemaining_Display_MaxRecordCount", ResourceType = typeof(MessageResource))]
        public int? MaxRecordCount { get; set; }
        [Display(Name = "SaleOrderRemaining_Display_StockType", ResourceType = typeof(MessageResource))]
        public int? StockTypeId { get; set; }

    }
}
