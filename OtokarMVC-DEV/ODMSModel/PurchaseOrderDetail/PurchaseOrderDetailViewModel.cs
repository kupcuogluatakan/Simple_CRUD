using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.PurchaseOrderDetail
{
    [Validator(typeof(PurchaseOrderDetailViewModelValidator))]
    public class PurchaseOrderDetailViewModel : ModelBase
    {
        public PurchaseOrderDetailViewModel()
        {
        }

        public bool HideFormElements { get; set; }

        [Display(Name = "PurchaseOrderDetail_Display_SequenceNo",
            ResourceType = typeof(MessageResource))]
        public long PurchaseOrderDetailSeqNo { get; set; }

        [Display(Name = "PurchaseOrderDetail_Display_PoNumber",
            ResourceType = typeof(MessageResource))]
        public int PurchaseOrderNumber { get; set; }

        [Display(Name = "SparePart_Display_Unit", ResourceType = typeof(MessageResource))]
        public string UnitName { get; set; }

        public long? PartId { get; set; }
        [Display(Name = "PurchaseOrderDetail_Display_PartName",
            ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }
        [Display(Name = "PurchaseOrderDetail_Display_ListPrice", ResourceType = typeof(MessageResource))]
        public decimal? ListPrice { get; set; }
        [Display(Name = "PurchaseOrderDetail_Display_ListDiscountRatio", ResourceType = typeof(MessageResource))]
        public decimal? ListDiscountRatio { get; set; }

        [Display(Name = "PurchaseOrderDetail_Display_ConfirmPrice", ResourceType = typeof(MessageResource))]
        public decimal? ConfirmPrice { get; set; }

        [Display(Name = "PurchaseOrderDetail_Display_AppliedDiscountRatio", ResourceType = typeof(MessageResource))]
        public decimal? AppliedDiscountRatio { get; set; }
        [Display(Name = "PurchaseOrderDetail_Display_PackageQuantity", ResourceType = typeof(MessageResource))]
        public decimal? PackageQuantity { get; set; }

        [Display(Name = "PurchaseOrderDetail_Display_DesireQuantity", ResourceType = typeof(MessageResource))]
        public decimal? DesireQuantity { get; set; }

        [Display(Name = "PurchaseOrderDetail_Display_OrderQuantity", ResourceType = typeof(MessageResource))]
        public decimal? OrderQuantity { get; set; }

        [Display(Name = "PurchaseOrderDetail_Display_ShipmentQuantity", ResourceType = typeof(MessageResource))]
        public decimal? ShipmentQuantity { get; set; }

        [Display(Name = "PurchaseOrderDetail_Display_OrderPrice", ResourceType = typeof(MessageResource))]
        public decimal? OrderPrice { get; set; }

        public new int StatusId { get; set; }
        [Display(Name = "PurchaseOrderDetail_Display_StatusName", ResourceType = typeof(MessageResource))]
        public string StatusName { get; set; }

        [Display(Name = "PurchaseOrderDetail_Display_DesireDeliveryDate", ResourceType = typeof(MessageResource))]
        public DateTime? DesireDeliveryDate { get; set; }

        public bool? ManuelPriceAllow { get; set; }

        public int? SupplyTypeMst { get; set; }

        public string SAPOfferNo { get; set; }
        public string SAPRowNo { get; set; }
        public string DenyReason { get; set; }

        public string RowNumber { get; set; }
        public string PartCode { get; set; }
        public string OrderNumber { get; set; }

        [Display(Name = "PurchaseOrder_Display_IdSupplier", ResourceType = typeof(MessageResource))]
        public string SupplierName { get; set; }
        public int? PoTypeId { get; set; }
        [Display(Name = "PurchaseOrder_Display_PoTypeName", ResourceType = typeof(MessageResource))]
        public string PoTypeName { get; set; }
        [Display(Name = "SparePartSale_Display_Vehicle", ResourceType = typeof(MessageResource))]
        public string StockTypeName { get; set; }
        [Display(Name = "SparePartSale_Display_Vehicle", ResourceType = typeof(MessageResource))]
        public string VehicleName { get; set; }

        public int? StatusMst { get; set; }

        [Display(Name = "Currency_Display_CurrencyCode", ResourceType = typeof(MessageResource))]
        public string CurrencyCode { get; set; }
        public bool IsCampaignPO { get; set; }
        public long SAPShipIdPart { get; set; }
        [Display(Name = "StockCard_Display_Otokar_Stock", ResourceType = typeof(MessageResource))]
        public string StockServiceValue { get; set; }

        public string SpecialExplanation { get; set; }
        public int? MstDealerCustomerId { get; set; }
        public int MstSupplierIdDealer { get; set; }

        [Display(Name = "PurchaseOrderDetail_Display_AlternatePart", ResourceType = typeof(MessageResource))]
        public string AlternatePart { get; set; }

        [Display(Name = "PurchaseOrderDetail_Display_AltenativeCostPrice", ResourceType = typeof(MessageResource))]
        public decimal? CostPrice { get; set; }
    }
}

