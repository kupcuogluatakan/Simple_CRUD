using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.PurchaseOrderSuggestionDetail
{
    public class POSuggestionDetailViewModel : ModelBase
    {
        public Int64 MrpId { get; set; }

        [Display(Name = "PurchaseOrderSuggestionDetail_Display_PropPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal SuggestionPrice { get; set; }

        [Display(Name = "PurchaseOrderSuggestionDetail_Display_OrderPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal OrderPrice { get; set; }

        public string OrderPriceS { get; set; }

        public int OrderNo { get; set; }

        public Int64 PoNumber { get; set; }
        public List<POSuggestionDetailListModel> ListModel { get; set; }

        [Display(Name = "PurchaseOrder_Display_CreditLimit", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CreditLimit { get; set; }
    }
}
