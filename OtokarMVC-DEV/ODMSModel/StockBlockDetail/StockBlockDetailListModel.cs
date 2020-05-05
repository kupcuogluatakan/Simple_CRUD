using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.StockBlockDetail
{
    public class StockBlockDetailListModel : BaseListWithPagingModel
    {
        public StockBlockDetailListModel([DataSourceRequest] DataSourceRequest request) : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"StockTypeName","ID_STOCK_TYPE"},
                    {"BlockQty", "BLCK_QTY"},
                    {"PartName", "PART_NAME"},
                    {"BlockedName", "BLOCKED_NAME"}
                };
            SetMapper(dMapper);
        }

        public StockBlockDetailListModel()
        {
        }

        public Int64? IdStockBlock { get; set; }

        [Display(Name = "StockBlock_Display_Dealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IdDealer { get; set; }
        [Display(Name = "StockBlock_Display_Dealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "Global_Display_StartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Global_Display_EndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EndDate { get; set; }
        
        [Display(Name = "StockBlock_Display_IdPart", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int64? IdPart { get; set; }

        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        [Display(Name = "SparePart_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }

        [Display(Name = "StockBlock_Display_IdStockType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IdStockType { get; set; }

        [Display(Name = "StockBlock_Display_IdStockType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StockTypeName { get; set; }

        [Display(Name = "StockBlock_Display_IsBalance", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public bool IsBalance { get; set; }

        public Int64? IdStockBlockDet { get; set; }

        [Display(Name = "StockBlock_Display_RemovedBlockQty", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? BlockQty { get; set; }

        [Display(Name = "StockBlock_Display_UnBlockQty", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? UnBlockQty { get; set; }

        [Display(Name = "StockBlock_Display_RemovedBlockQty", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? RemovableQty { get; set; }

        [Display(Name = "StockBlock_Display_RemoveBlockQty", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? RemovedQty { get; set; }

        [Display(Name = "Global_Display_Status", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int StockBlockStatusId { get; set; }
        [Display(Name = "Global_Display_Status", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StockBlockStatusName { get; set; }

        [Display(Name = "StockBlock_Display_BlockReasonDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string BlockReasonDesc { get; set; }
    }
}
