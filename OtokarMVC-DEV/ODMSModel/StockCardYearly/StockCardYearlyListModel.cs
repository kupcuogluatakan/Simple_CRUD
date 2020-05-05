using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.StockCardYearly
{
    public class StockCardYearlyListModel : BaseListWithPagingModel
    {
        public StockCardYearlyListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"Month","DATE_MONTH"},
                    {"Year","DATE_YEAR"},
                    {"Quantity", "QUANTITY"}
                };
            SetMapper(dMapper);
        }

        public StockCardYearlyListModel()
        {
        }

        public int? Month { get; set; }

        //[Display(Name = "StockCard_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? Year { get; set; }
        
        public decimal? Quantity { get; set; }

        [Display(Name = "PurchaseOrder_Display_IdStockType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IdStockType { get; set; }

        public DateTime? Date { get; set; }

        public int? IdDealer { get; set; }

        public Int64? IdPart { get; set; }
    }
}
