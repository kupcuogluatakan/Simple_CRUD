using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.StockCardTransaction
{
    public class StockCardTransactionListModel : BaseListWithPagingModel
    {
        public StockCardTransactionListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"TransactionType","TRANSACTION_TYPE"},
                    {"TransactionDesc","TRANSACTION_DESC"},
                    {"FromWareRack", "FROM_WARE_RACK"},
                    {"ToWareRack","TO_WARE_RACK"},
                    {"StockType","STOCK_TYPE"},
                    {"Quantity","QNTY"},
                    {"CreateDate","CREATE_DATE"}
                };
            SetMapper(dMapper);
        }

        public StockCardTransactionListModel()
        {
        }

        public int PartId { get; set; }
        public int DealerId { get; set; }

        [Display(Name = "StockCardTransaction_Display_Type", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string TransactionType { get; set; }

        [Display(Name = "StockCardTransaction_Display_Desc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string TransactionDesc { get; set; }

        [Display(Name = "StockCardTransaction_Display_FromWareRack", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string FromWareRack { get; set; }

        [Display(Name = "StockCardTransaction_Display_ToWareRack", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ToWareRack { get; set; }

        [Display(Name = "StockCardTransaction_Display_StockType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StockTypeId { get; set; }

        [Display(Name = "StockCardTransaction_Display_StockType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StockType { get; set; }

        [Display(Name = "Global_Display_Quantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Quantity { get; set; }

        [Display(Name = "Global_Display_CreateDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Global_Display_CreateDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DateTimeS { get; set; }

        [Display(Name = "Announcement_Display_StartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? StartDate { get; set; }
        [Display(Name = "Announcement_Display_EndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EndDate { get; set; }
    }
}
