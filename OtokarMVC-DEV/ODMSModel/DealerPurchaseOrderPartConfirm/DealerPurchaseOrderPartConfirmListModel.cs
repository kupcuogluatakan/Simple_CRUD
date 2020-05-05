using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.DealerPurchaseOrderPartConfirm
{
    public class DealerPurchaseOrderPartConfirmListModel : BaseListWithPagingModel
    {
        public DealerPurchaseOrderPartConfirmListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
             var dMapper = new Dictionary<string, string>
                 {
                     {"PurchaseOrderDetailSeqNo", "POD.PO_DET_SEQ_NO"},
                     {"PartCodeName", "4"},
                     {"OrderQuantity", "POD.ORDER_QUANT"},
                     {"StockQuantity", "12"},
                     {"ShipmentQuantity", "POD.SHIP_QUANT"},
                     {"StatusName", "POD.STATUS_LOOKVAL"}
                 };
            SetMapper(dMapper);

        }

        public DealerPurchaseOrderPartConfirmListModel() { }

        [Display(Name = "PurchaseOrder_Display_PoNumber", ResourceType = typeof(MessageResource))]
        public long PoNumber { get; set; }

        [Display(Name = "PurchaseOrderDetail_Display_SequenceNo", ResourceType = typeof(MessageResource))]
        public long PurchaseOrderDetailSeqNo { get; set; }
        
        public int? PartId { get; set; }
        [Display(Name = "PurchaseOrderDetail_Display_PartName", ResourceType = typeof(MessageResource))]
        public string PartCodeName { get; set; }
        
        [Display(Name = "PurchaseOrderDetail_Display_OrderQuantity", ResourceType = typeof(MessageResource))]
        public decimal OrderQuantity { get; set; }
        
        [Display(Name = "PurchaseOrderDetailConfirm_Display_UsableStockQuantity", ResourceType = typeof(MessageResource))]
        public decimal StockQuantity { get; set; }

        [UIHint("DecimalNumericTextbox")]
        [Display(Name = "PurchaseOrderDetail_Display_ShipmentQuantity", ResourceType = typeof(MessageResource))]
        public decimal ShipmentQuantity { get; set; }

        public int? StatusId { get; set; }
        [Display(Name = "PurchaseOrderDetail_Display_StatusName", ResourceType = typeof(MessageResource))]
        public string StatusName { get; set; }
        
        public int? OrderStatusId { get; set; }
        public int SupplierDealerId { get; set; }
        public int DealerId { get; set; }

        public int SupplierDealerConfirm { get; set; }
    }
}
