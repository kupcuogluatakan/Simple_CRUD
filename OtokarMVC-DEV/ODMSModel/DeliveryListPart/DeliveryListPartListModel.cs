using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.DeliveryListPart
{
    public class DeliveryListPartListModel : BaseListWithPagingModel
    {
        public DeliveryListPartListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"VehicleNotesId", "ID_VEHICLE_NOTE"},
                    {"DealerName", "DEALER_NAME"},
                    {"Note", "NOTE"},
                    {"IsActiveName", "IS_ACTIVE_STRING"}
                };
            SetMapper(dMapper);
        }

        public DeliveryListPartListModel()
        {
        }

        public Int64 DeliverySeqNo { get; set; }

        public Int64 DeliveryId { get; set; }

        [Display(Name = "PurchaseOrder_Display_PoNumber", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PoNumber { get; set; }

        [Display(Name = "DeliveryListPart_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }

        [Display(Name = "StockTypeDetail_Display_IdStockType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StockType { get; set; }

        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int PartId { get; set; }

        [Display(Name = "SparePartDeliverList_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        [Display(Name = "Delivery_Display_InvoicePrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? InvoicePrice { get; set; }

        [Display(Name = "Delivery_Display_ShipQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal ShipQnty { get; set; }

        [Display(Name = "Delivery_Display_RecievedQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal ReceiveQnty { get; set; }

        [Display(Name = "Delivery_Display_RecievedQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int Id { get; set; }

        [Display(Name = "SparePart_Display_Barcode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Barcode { get; set; }
        public long PoDetSeqNo { get; set; }

        public string SAPOfferNo { get; set; }

        [Display(Name = "Global_Display_Status", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int Status { get; set; }

        public decimal PackageQnty { get; set; }

        public int ChildCount { get; set; }

        [Display(Name = "Delivery_Display_RemainingQty", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal RemainingQnty { get; set; }
    }
}
