using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace ODMSModel.DealerSaleSparepart
{
    public class DealerSaleSparepartListModel : BaseListWithPagingModel
    {
        public DealerSaleSparepartListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"Dealer", "ID_DEALER"},
                    {"PartName","ID_PART"},
                    {"DiscountRatio", "DISCOUNT_RATIO"},
                    {"DiscountPrice","DISCOUNT_PRICE"},
                    {"ListPrice", "LIST_PRICE"},
                    {"StockQuantity","STOCK_QUANTITY"},
                    {"PartCode","PART_CODE"},
                    {"Quantity","DESIRE_QUANT"},
                    {"TotalPrice","TOTAL_PRICE"},
                    {"CreateDate","CREATE_DATE"},
                    {"ShipQty","SHIP_QUANT"},
                    {"Unit","UNIT"}
                };
            SetMapper(dMapper);
        }

        public DealerSaleSparepartListModel()
        {
            // TODO: Complete member initialization
        }
        //IdDealer
        [Display(Name = "DealerSaleSparepart_Display_IdDealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IdDealer { get; set; }

        //IdPart
        [Display(Name = "DealerSaleSparepart_Display_IdPart", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public long? IdPart { get; set; }

        //DiscountRatio
        [Display(Name = "DealerSaleSparepart_Display_DiscountRatio", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? DiscountRatio { get; set; }

        //DiscountPrice
        [Display(Name = "DealerSaleSparepart_Display_DiscountPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? DiscountPrice { get; set; }

        //ListPrice
        [Display(Name = "DealerSaleSparepart_Display_ListPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? ListPrice { get; set; }

        //PartName
        [Display(Name = "DealerSaleSparepart_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        //PartCode
        [Display(Name = "DealerSaleSparepart_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }

        //CreateDate
        [Display(Name = "DealerSaleSparepart_Display_CreateDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? CreateDate { get; set; }

        //StockQuantity
        [Display(Name = "DealerSaleSparepart_Display_StockQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? StockQuantity { get; set; }

        [Display(Name = "DealerSaleSparepart_Display_SalePrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? SalePrice { get; set; }
        //DealerPurchaseOrder sayfası için kullanılacak.
        public int? PODetSeqNo { get; set; }

        public int? PurchaseOrderId { get; set; }

        [Display(Name = "DealerPurchaseOrder_Display_TotalPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? TotalPrice { get; set; }

        [Display(Name = "DealerPurchaseOrder_Display_Quantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? Quantity { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public bool? IsActive { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsActiveName { get; set; }

        [Display(Name = "DealerSaleSparepart_Display_ShipQty", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal ShipQty { get; set; }

        [Display(Name = "DealerSaleSparepart_Display_Unit", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Unit { get; set; }

        public bool DoNotReturnStockQuantityZero { get; set; }

        public int ErrorNo { get; set; }
        public string ErrorMessage { get; set; }
        public string MultiLanguageContentAsText { get; set; }
    }
}
