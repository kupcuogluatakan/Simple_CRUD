using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.PurchaseOrderDetail
{
    public class PurchaseOrderDetailListModel : BaseListWithPagingModel
    {
        public PurchaseOrderDetailListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"PurchaseOrderDetailSeqNo", "PO_DET_SEQ_NO"},
                    {"PurchaseOrderNumber", "PO_NUMBER"},
                    {"PartName", "PART_NAME"},
                    {"PartCode", "PART_CODE"},
                    {"PackageQuantityString", "PACKAGE_QUANT, UNIT_LOOKVAL"},
                    {"DesireQuantity", "DESIRE_QUANT"},
                    {"DesireDeliveryDate","DESIRE_DELIVERY_DATE"},
                    {"OrderQuantity", "ORDER_QUANT"},
                    {"OrderPriceS", "ORDER_PRICE"},
                    {"StatusName", "POD.STATUS_LOOKVAL"},
                    {"ShipmentQuantity","POD.SHIP_QUANT"},
                    {"CurrencyCode","CURRENCY_CODE"}
                };
            SetMapper(dMapper);
        }

        public PurchaseOrderDetailListModel()
        {
        }
        public Int64? SupplierIdDealer { get; set; }

        [Display(Name = "PurchaseOrderDetail_Display_SequenceNo",
            ResourceType = typeof(MessageResource))]
        public int PurchaseOrderDetailSeqNo { get; set; }

        [Display(Name = "PurchaseOrderDetail_Display_PoNumber",
            ResourceType = typeof(MessageResource))]
        public int PurchaseOrderNumber { get; set; }

        [Display(Name = "SparePart_Display_Unit", ResourceType = typeof(MessageResource))]
        public string UnitName { get; set; }

        public long? PartId { get; set; }
        [Display(Name = "SparePart_Display_PartCode",
            ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "SparePart_Display_PartName",
            ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }
        public bool IsProposal { get; set; }
        public int SupplierDealerConfirm { get; set; }
        [Display(Name = "PurchaseOrderDetail_Display_PackageQuantity", ResourceType = typeof(MessageResource))]
        public decimal? PackageQuantity { get; set; }
        [Display(Name = "PurchaseOrderDetail_Display_PackageQuantity", ResourceType = typeof(MessageResource))]
        public string PackageQuantityString { get; set; }

        [Display(Name = "PurchaseOrderDetail_Display_DesireQuantity", ResourceType = typeof(MessageResource))]
        public decimal? DesireQuantity { get; set; }

        [Display(Name = "PurchaseOrderDetail_Display_OrderQuantity", ResourceType = typeof(MessageResource))]
        public decimal? OrderQuantity { get; set; }

        [Display(Name = "PurchaseOrderDetail_Display_ShipmentQuantity", ResourceType = typeof(MessageResource))]
        public decimal? ShipmentQuantity { get; set; }

        [Display(Name = "PurchaseOrderDetail_Display_ReceivedQuantity", ResourceType = typeof(MessageResource))]
        public decimal? ReceivedQuantity { get; set; }
        [Display(Name = "PurchaseOrderDetail_Display_StockQuantity", ResourceType = typeof(MessageResource))]
        public decimal? StockQuantity { get; set; }

        [Display(Name = "PurchaseOrderDetail_Display_OrderPrice", ResourceType = typeof(MessageResource))]
        public decimal? OrderPrice { get; set; }

        [Display(Name = "PurchaseOrderDetail_Display_OrderPrice", ResourceType = typeof(MessageResource))]
        public string OrderPriceS { get; set; }
        public decimal? AppliedDiscountRatio { get; set; }
        public decimal? ListDiscountRatio { get; set; }
        [Display(Name = "PurchaseOrderDetail_Display_ConfirmPrice", ResourceType = typeof(MessageResource))]
        public decimal? ConfirmPrice { get; set; }
        public bool AcceptOrderProposal { get; set; }
        public int? StatusId { get; set; }
        [Display(Name = "PurchaseOrderDetail_Display_StatusName", ResourceType = typeof(MessageResource))]
        public string StatusName { get; set; }

        public int? StatusMst { get; set; }

        public long? SupplierId { get; set; }

        [Display(Name = "PurchaseOrderDetail_Display_DesireDeliveryDate", ResourceType = typeof(MessageResource))]
        public DateTime? DesireDeliveryDate { get; set; }

        public bool? ManuelPriceAllow { get; set; }

        public int? SupplyTypeMst { get; set; }

        public string SAPOfferNo { get; set; }

        public decimal DealerPrice { get; set; }

        [Display(Name = "Currency_Display_CurrencyCode", ResourceType = typeof(MessageResource))]
        public string CurrencyCode { get; set; }
        [Display(Name = "PurchaseOrderDetail_Display_ConfirmPrice", ResourceType = typeof(MessageResource))]
        public string ConfirmPriceS { get; set; }
        public bool IsCampaignPO { get; set; }

        [Display(Name = "WorkOrderCard_Display_DenyReason", ResourceType = typeof(MessageResource))]
        public string DenyReason { get; set; }
        public string SpecialExplanation { get; set; }
        public long? ChangedPartId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}

