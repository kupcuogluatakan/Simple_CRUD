
using ODMSCommon.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Reports
{
    public class WorkOrderPartHistoryReport
    {
        [Display(Name = "WorkOrderPartHistoryReport_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "WorkOrderPartHistoryReport_Display_PartName", ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }
        [Display(Name = "WorkOrderPartHistoryReport_Display_WorkOrderId", ResourceType = typeof(MessageResource))]
        public long WorkOrderId { get; set; }
        [Display(Name = "WorkOrderPartHistoryReport_Display_AddedQuantity", ResourceType = typeof(MessageResource))]
        public decimal AddedQuantity { get; set; }
        [Display(Name = "WorkOrderPartHistoryReport_Display_ReturnedQuantity", ResourceType = typeof(MessageResource))]
        public decimal ReturnedQuantity { get; set; }
        [Display(Name = "WorkOrderPartHistoryReport_Display_OriginalQuantity", ResourceType = typeof(MessageResource))]
        public decimal OriginalQuantity { get; set; }
        [Display(Name = "WorkOrderPartHistoryReport_Display_AvailableQuantity", ResourceType = typeof(MessageResource))]
        public decimal AvailableQuantity { get; set; }
        [Display(Name = "WorkOrderPartHistoryReport_Display_IsAllAdded", ResourceType = typeof(MessageResource))]
        public string IsAllAdded { get; set; }
        [Display(Name = "WorkOrderPartHistoryReport_Display_IsRemoved", ResourceType = typeof(MessageResource))]
        public string IsRemoved { get; set; }
        [Display(Name = "WorkOrderPartHistoryReport_Display_AddDate", ResourceType = typeof(MessageResource))]
        public DateTime AddDate { get; set; }
        [Display(Name = "WorkOrderPartHistoryReport_Display_StockType", ResourceType = typeof(MessageResource))]
        public string StockType { get; set; }
        [Display(Name = "WorkOrderPartHistoryReport_Display_WorkOrderStatus", ResourceType = typeof(MessageResource))]
        public string WorkOrderStatus { get; set; }
        [Display(Name = "WorkOrderPartHistoryReport_Display_IndicatorType", ResourceType = typeof(MessageResource))]
        public string IndicatorType { get; set; }
        [Display(Name = "WorkOrderPartsTotalReport_CustomerPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal CustomerAmount { get; set; }
        [Display(Name = "WorkOrderPartsTotalReport_PaidAmount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal PaidAmount { get; set; }
        [Display(Name = "WorkOrderPartHistoryReport_Display_Price", ResourceType = typeof(MessageResource))]
        public decimal Price { get; set; }
        public long PartId { get; set; }
        public int DealerId { get; set; }
        [Display(Name = "WorkOrderPartHistoryReport_Display_DealerIdList", ResourceType = typeof(MessageResource))]
        public string Dealer { get; set; }
    }
}
