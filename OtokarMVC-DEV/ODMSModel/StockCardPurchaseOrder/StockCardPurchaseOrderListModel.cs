using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.StockCardPurchaseOrder
{
    public class StockCardPurchaseOrderListModel : BaseListWithPagingModel 
    {
        public StockCardPurchaseOrderListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"PoNumber","PO_NUMBER"},
                    {"Status","STATUS"},
                    {"OrderQuantity", "ORDER_QUANT"},
                    {"ShipQuantity","SHIP_QUANT"},
                    {"Supplier","SUPPLIER"},
                    {"OrderType","PURCHASE_ORDER_TYPE"},
                    {"CreateDateS","CREATE_DATE"},
                    {"StockType","STOCK_TYPE_DESC"}
                };
            SetMapper(dMapper);
        }

        public StockCardPurchaseOrderListModel()
        {
        }

        public int DealerId { get; set; }
        public int PartId { get; set; }

        [Display(Name = "PurchaseOrder_Display_PoNumber", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int64? PoNumber { get; set; }

        [Display(Name = "PurchaseOrderDetail_Display_StatusName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Status { get; set; }

        [Display(Name = "PurchaseOrderDetail_Display_OrderQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string OrderQuantity { get; set; }

        [Display(Name = "PurchaseOrderDetail_Display_ShipmentQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ShipQuantity { get; set; }

        [Display(Name = "PurchaseOrder_Display_IdSupplier", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Supplier { get; set; }

        [Display(Name = "PurchaseOrder_Display_PoType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string OrderType { get; set; }

        [Display(Name = "Global_Display_CreateDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime CreateDate { get; set; }
        [Display(Name = "Global_Display_CreateDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public String CreateDateS { get; set; }
        [Display(Name = "StockCard_Display_StockTypeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StockType { get; set; }

        /*Search*/
        public int? StatusId { get; set; }
        [Display(Name = "Global_Display_StartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? StartDate { get; set; }
        [Display(Name = "Global_Display_EndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EndDate { get; set; }

    }
}
