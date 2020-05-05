using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.StockBlock
{
    public class StockBlockListModel : BaseListWithPagingModel
    {
        public StockBlockListModel([DataSourceRequest] DataSourceRequest request) : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"BlockDate", "BLOCK_DATE"},
                    {"BlockReasonDesc", "BLOCK_REASON_DESC"},
                    {"IsBlocked", "IS_BLOCKED"}
                };
            SetMapper(dMapper);
        }

        public StockBlockListModel()
        {
        }

        public Int64? IdStockBlock { get; set; }

        [Display(Name = "StockBlock_Display_Dealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IdDealer { get; set; }
        [Display(Name = "StockBlock_Display_Dealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "StockBlock_Display_BlockDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? BlockDate { get; set; }

        [Display(Name = "StockBlock_Display_BlockReasonDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string BlockReasonDesc { get; set; }
        
        [Display(Name = "Global_Display_Status", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? BlockStatusId { get; set; }

        [Display(Name = "Global_Display_Status", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string BlockStatusName { get; set; }

        [Display(Name = "Global_Display_StartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Global_Display_EndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "StockBlock_Display_IsBalance", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IsBalance { get; set; }
    }
}
