using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.PurchaseOrderSuggestionDetail
{
    public class POSuggestionDetailListModel : BaseListWithPagingModel
    {
        public POSuggestionDetailListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"MrpId", "ID_MRP"},
                    {"PartId","ID_PART"},
                    {"Part", "PART"},
                    {"PartCode","PART_CODE"},
                    {"CurrentQuantity", "CURR_QTY"},
                    {"OpenPoQuantity","OPEN_PO_QTY"},
                    {"ReserveQuantity", "RESERVE_QTY"},
                    {"PackageQuantity","PACKAGE_QTY"},
                    {"PurchasePrice", "PURCHASE_PRICE"},
                    {"MinStockQuantity","MIN_STOCK_QTY"},
                    {"OrderQuantity", "ORDER_QTY"},
                    {"LeadTime","LEAD_TIME"},
                    {"PropPoQuantity","PROP_PO_QTY"},
                    {"Unit", "UNIT"}
                };
            SetMapper(dMapper);
        }

        public POSuggestionDetailListModel()
        {
        }

        public Int64 MrpId { get; set; }
        public int PartId { get; set; }

        [Display(Name = "SparePart_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Part { get; set; }

        [Display(Name = "CycleCountResultAudit_Display_CurrentQty", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CurrentQuantity { get; set; }

        [Display(Name = "PurchaseOrderSuggestionDet_Display_OpenQty", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string OpenPoQuantity { get; set; }

        [Display(Name = "PurchaseOrderSuggestionDet_Display_PropQty", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PropPoQuantity { get; set; }

        [Display(Name = "StockTypeDetail_Display_ReserveQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ReserveQuantity { get; set; }
        
        [Display(Name = "PurchaseOrderDetail_Display_PackageQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PackageQuantity { get; set; }
        [Display(Name = "PurchaseOrderSuggestionDet_Display_OpenSoQty", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string OpenSoQuantity { get; set; }

        [Display(Name = "Global_Display_Price", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PurchasePrice { get; set; }

        [Display(Name = "PurchaseOrderSuggestionDet_Display_ResQty", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal ResultQuantity { get; set; }

        [Display(Name = "ClaimRecallPeriodPart_Display_ClaimRecallPeriodId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ResultQuantityS { get; set; }

        [Display(Name = "StockCard_Display_MinStockQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string MinStockQuantity { get; set; }

        [Display(Name = "PurchaseOrderDetail_Display_OrderQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal OrderQuantity { get; set; }

        [Display(Name = "SparePart_Display_LeadTime", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string LeadTime { get; set; }

        [Display(Name = "SparePart_Display_Unit", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Unit { get; set; }

        public bool IsChecked { get; set; }
        public int IsDivided { get; set; }
        public int IsChanged { get; set; }
        public string FromParts { get; set; }

        [Display(Name = "SparePart_Display_ChangeDivideName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ChangeDivideName { get; set; }
    }
}
