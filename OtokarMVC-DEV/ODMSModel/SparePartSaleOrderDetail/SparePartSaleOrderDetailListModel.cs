using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.SparePartSaleOrderDetail
{
    public class SparePartSaleOrderDetailListModel : BaseListWithPagingModel
    {
        public bool IsSelected { get; set; }

        [Display(Name = "Global_Display_Select", ResourceType = typeof(MessageResource))]
        public string SoNumber { get; set; }
        public long SparePartSaleOrderDetailId { get; set; }
        public long? ChangedPartId { get; set; }
        public long SparePartId { get; set; }
        [Display(Name = "SparePart_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string SparePartCode { get; set; }
        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(MessageResource))]
        public string SparePartName { get; set; }

        [Display(Name = "SparePartSaleOrderDetail_Display_IsOriginalPart", ResourceType = typeof(MessageResource))]
        public bool? IsOriginalPart { get; set; }

        [Display(Name = "SparePartSaleOrderDetail_Display_OrderQuantity", ResourceType = typeof(MessageResource))]
        public decimal? OrderQuantity { get; set; }

        [Display(Name = "SparePartSaleOrderDetail_Display_PlannedQuantity", ResourceType = typeof(MessageResource))]
        public decimal? PlannedQuantity { get; set; }

        [Display(Name = "SparePartSaleOrderDetail_Display_ShippedQuantity", ResourceType = typeof(MessageResource))]
        public decimal? ShippedQuantity { get; set; }

        [Display(Name = "SparePartSaleOrderDetail_Display_ListPrice", ResourceType = typeof(MessageResource))]
        public decimal? ListPrice { get; set; }

        [Display(Name = "SparePartSaleOrderDetail_Display_OrderPrice", ResourceType = typeof(MessageResource))]
        public decimal? OrderPrice { get; set; }

        [Display(Name = "SparePartSaleOrderDetail_Display_ConfirmPrice", ResourceType = typeof(MessageResource))]
        public decimal? ConfirmPrice { get; set; }

        [Display(Name = "SparePartSaleOrderDetail_Display_ListDiscountRatio", ResourceType = typeof(MessageResource))]
        public decimal? ListDiscountRatio { get; set; }

        [Display(Name = "SparePartSaleOrderDetail_Display_AppliedDiscountRatio", ResourceType = typeof(MessageResource))]
        public decimal? AppliedDiscountRatio { get; set; }

        public int? StatusId { get; set; }
        [Display(Name = "SparePartSaleOrderDetail_Display_Status", ResourceType = typeof(MessageResource))]
        public string StatusName { get; set; }
        public int? MasterStatusId { get; set; }

        public string PurchaseOrderDetailSeqNo { get; set; }

        [Display(Name = "SparePartSaleOrderDetail_Display_PartSaleId", ResourceType = typeof(MessageResource))]
        public int? PartSaleId { get; set; }
        public int? SparePartSaleWaybillId { get; set; }
        [Display(Name = "SparePartSaleOrderDetail_Display_WaybillDate", ResourceType = typeof(MessageResource))]
        public DateTime? WaybillDate { get; set; }
        public int? SparePartSaleInvoiceId { get; set; }
        [Display(Name = "SparePartSaleOrderDetail_Display_InvoiceDate", ResourceType = typeof(MessageResource))]
        public DateTime? InvoiceDate { get; set; }
        public string InvoiceNo { get; set; }

        [Display(Name = "PurchaseOrder_Display_PoNumber", ResourceType = typeof(MessageResource))]
        public string PoNumber { get; set; }

        [Display(Name = "Dealer_Display_CurrencyCode", ResourceType = typeof(MessageResource))]
        public string CurrencyCode { get; set; }
        public string SpecialExplanation { get; set; }
        public int PODetailCount { get; set; }
        public int SparePartSaleId { get; set; }
        [Display(Name = "StockTypeDetail_Display_OpenQuantity", ResourceType = typeof(MessageResource))]
        public decimal? OpenQuantity { get; set; }
        [Display(Name = "CriticalStockQuantity_Display_StockQuantity", ResourceType = typeof(MessageResource))]
        public decimal? StockQuantity { get; set; }
        [Display(Name = "SparePartSaleOrderDetail_Display_OrderedQuantity", ResourceType = typeof(MessageResource))]
        public decimal? OrderedQuantity { get; set; }

        public SparePartSaleOrderDetailListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"PartCode", "PART_CODE"},
                     {"PartName", "PART_NAME"},
                     {"StockQuantity", "STOCK_QUANT"},
                     {"StatusName","STATUS_NAME"}
                 };
            SetMapper(dMapper);
        }

        public SparePartSaleOrderDetailListModel() { }
    }
}
