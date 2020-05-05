using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;
namespace ODMSModel.WorkOrderPicking
{
    public class WorkOrderPickingListModel : BaseListWithPagingModel
    {
        public WorkOrderPickingListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
            {
                {"WorkOrderPickingId", "ID_WORK_ORDER_PICKING_MST"},
                {"WorkOrderPlate", "PLATE"},
                {"Status", "STATUS"},//Fatura seri no
                {"IsReturnS","IS_RETURN"},
                {"CreateDate", "CREATE_DATE"}

            };
            SetMapper(dMapper);
        }

        public WorkOrderPickingListModel()
        {
        }

        public bool IsSelected { get; set; }
        public string No { get; set; }

        public Int64 WorkOrderPickingId { get; set; }

        [Display(Name = "WorkOrderPicking_Display_PlateCustomer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WorkOrderPlate { get; set; }

        [Display(Name = "WorkOrderPicking_Display_Status", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StatusIds { get; set; }
        [Display(Name = "WorkOrderPicking_Display_Status", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? StatusId { get; set; }
        [Display(Name = "WorkOrderPicking_Display_Status", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Status { get; set; }

        [Display(Name = "Global_Display_CreateDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime CreateDate { get; set; }

        [Display(Name = "WorkOrderPicking_Display_OrderSource", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string OrderSource { get; set; }

        //Just Search Criteria  
        [Display(Name = "WorkOrderPicking_Display_Return", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IsReturn { get; set; }

        [Display(Name = "Global_Display_StartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Global_Display_EndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "WorkOrderPicking_Display_Type", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsReturnS { get; set; }

        public int? PartSaleId { get; set; }

        public int? CustomerId { get; set; }

        public string PartName { get; set; }

        public string PartCode { get; set; }

        public int? SourceType { get; set; }
    }
}
