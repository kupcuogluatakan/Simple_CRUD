using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.ClaimRecallPeriod
{
    public class ClaimRecallPeriodListModel : BaseListWithPagingModel
    {
        public ClaimRecallPeriodListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"ClaimRecallPeriodId", "ID_CLAIM_RECALL_PERIOD"},
                    {"ValidLastDay","VALID_LAST_DAY"},
                    {"ShipFirstDay", "SHIP_FIRST_DAY"},
                    {"ShipLastDay", "SHIP_LAST_DAY"},
                    {"IsActiveName","IS_ACTIVE"}
                };
            SetMapper(dMapper);
        }

        public ClaimRecallPeriodListModel()
        {
        }

        [Display(Name = "ClaimRecallPeriod_Display_ClaimRecallPeriodId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int ClaimRecallPeriodId { get; set; }

        [Display(Name = "ClaimRecallPeriod_Display_ValidLastDay", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime ValidLastDay { get; set; }

        [Display(Name = "ClaimRecallPeriod_Display_ShipFirstDay", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime ShipFirstDay { get; set; }

        [Display(Name = "ClaimRecallPeriod_Display_ShipLastDay", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime ShipLastDay { get; set; }

        public int? IsActive { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsActiveName { get; set; }
    }
}
