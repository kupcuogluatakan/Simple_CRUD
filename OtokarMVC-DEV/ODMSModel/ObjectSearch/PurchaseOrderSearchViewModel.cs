using System;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.ObjectSearch
{
    public class PurchaseOrderSearchViewModel:ObjectSearchModel
    {
        public int PurchaseOrderDetailSeqNo { get; set; }
        [Display(Name = "PurchaseOrder_Display_PoNumber", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int64? PoNumber { get; set; }

        //PoType
        [Display(Name = "PurchaseOrder_Display_PoType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int32? PoType { get; set; }

        //PoTypeName
        [Display(Name = "PurchaseOrder_Display_PoType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PoTypeName { get; set; }

        //DesiredShipDate
        [Display(Name = "PurchaseOrder_Display_DesiredShipDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? DesiredShipDate { get; set; }

        //StatusName
        [Display(Name = "PurchaseOrder_Display_StatusName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StatusName { get; set; }

        //Status
        [Display(Name = "PurchaseOrder_Display_StatusName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int32? Status { get; set; }

        //IdStockType
        [Display(Name = "StockCardTransaction_Display_StockType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int32? IdStockType { get; set; }

        [Display(Name = "WorkOrderCard_Display_Part", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public long PartId { get; set; }

    } 
}
