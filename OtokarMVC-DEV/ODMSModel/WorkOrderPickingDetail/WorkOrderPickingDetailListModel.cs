using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;
namespace ODMSModel.WorkOrderPickingDetail
{
    public class WorkOrderPickingDetailListModel : BaseListWithPagingModel
    {
        public WorkOrderPickingDetailListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"WorkOrderPickDetId", "ID_WORK_ORDER_PICKING_DET"},
                     {"RequestQuantity", "REQ_QTY"},
                     {"PickQuantity", "PICK_QTY"},
                     {"PartName", "PART_NAME"},
                     {"StockType", "MAINT_NAME"}
                 };
            SetMapper(dMapper);
        }

        public WorkOrderPickingDetailListModel()
        {

        }

        public Int64 WOPMstId { get; set; }

        public Int64 WOPDetId { get; set; }

        [Display(Name = "WorkOrderPickingDet_Display_AppInd", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AppIndicator { get; set; }

        [Display(Name = "WorkOrderPickingDet_Display_ReqQty", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RequestQuantity { get; set; }

        [Display(Name = "WorkOrderPickingDet_Display_PickQty", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PickQuantity { get; set; }

        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        [Display(Name = "WorkOrderPickingDet_Display_StockType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StockType { get; set; }

        public bool IsReturn { get; set; }

        //ForBarcode

        [Display(Name = "SparePart_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int PartId { get; set; }
        [Display(Name = "SparePart_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCodeName { get; set; }

        [Display(Name = "WorkOrderPicking_Display_Status", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int ParentStatusId { get; set; }

        public string Unit { get; set; }
        [Display(Name = "SparePart_Display_Unit", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string UnitName { get; set; }

        public int StockTypeId { get; set; }
    }
}
