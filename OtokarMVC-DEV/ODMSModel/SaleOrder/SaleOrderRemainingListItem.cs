using ODMSCommon.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.SaleOrder
{
    public class SaleOrderRemainingListItem
    {
        public bool IsSelected { get; set; }

        [Display(Name = "SaleOrderRemaining_Display_SaleOrderNumber", ResourceType = typeof(MessageResource))]
        public long SaleOrderNumber { get; set; }
        [Display(Name = "SaleOrderRemaining_Display_SaleOrderCreateDate", ResourceType = typeof(MessageResource))]
        public DateTime SaleOrderCreateDate { get; set; }
        [Display(Name = "SaleOrderRemaining_Display_SaleOrderType", ResourceType = typeof(MessageResource))]
        public string SaleOrderType { get; set; }
        [Display(Name = "SaleOrderRemaining_Display_CustomerName", ResourceType = typeof(MessageResource))]
        public string CustomerName { get; set; }
        [Display(Name = "SaleOrderRemaining_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "SaleOrderRemaining_Display_PartName", ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }
        [Display(Name = "SaleOrderRemaining_Display_PlannedQuantity", ResourceType = typeof(MessageResource))]
        public decimal PlannedQuantity { get; set; }
        [Display(Name = "SaleOrderRemaining_Display_OnOrderQuantity", ResourceType = typeof(MessageResource))]
        public decimal OnOrderQuantity { get; set; }
        [Display(Name = "SaleOrderRemaining_Display_TotalOnOrderQuantity", ResourceType = typeof(MessageResource))]
        public decimal TotalOnOrderQuantity { get; set; }
        [Display(Name = "SaleOrderRemaining_Display_ExistingStockQuantity", ResourceType = typeof(MessageResource))]
        public decimal ExistingStockQuantity { get; set; }
        [Display(Name = "SaleOrderRemaining_Display_StockType", ResourceType = typeof(MessageResource))]
        public string StockType { get; set; }
        [Display(Name = "SaleOrderRemaining_Display_IsOriginal", ResourceType = typeof(MessageResource))]
        public string IsOriginal { get; set; }
        public long SoDetSeqNo { get; set; }
        public long PartId { get; set; }
        public long? ChangedPartId { get; set; }
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
    }
}
