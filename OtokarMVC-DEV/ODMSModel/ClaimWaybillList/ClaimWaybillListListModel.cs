using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.ClaimWaybillList
{
    public class ClaimWaybillListListModel : BaseListWithPagingModel
    {
        public ClaimWaybillListListModel([DataSourceRequest] DataSourceRequest request) : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"DealerName", "DEALER_NAME"},
                    {"WaybillText","WAYBILL_TEXT"},
                    {"WaybillNo","WAYBILL_NO"},
                    {"WaybillSerialNo", "WAYBILL_SERIAL_NO"},
                    {"WaybillDate", "WAYBILL_DATE"},
                    {"AcceptUser", "ACCEPT_USER"},
                    {"AcceptDate", "ACCEPT_DATE"}
                };
            SetMapper(dMapper);
        }

        public ClaimWaybillListListModel()
        {
        }

        public int? IdClaimWaybill { get; set; }

        [Display(Name = "ClaimWaybillList_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "ClaimWaybillList_Display_WaybillText", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WaybillText { get; set; }

        [Display(Name = "ClaimWaybillList_Display_WaybillSerialNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WaybillSerialNo { get; set; }

        [Display(Name = "ClaimWaybillList_Display_WaybillDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? WaybillDate { get; set; }

        [Display(Name = "ClaimWaybillList_Display_AcceptUser", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AcceptUser { get; set; }

        [Display(Name = "ClaimWaybillList_Display_AcceptDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? AcceptDate { get; set; }

        [Display(Name = "ClaimWaybillList_Display_IsAccepted", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IsAccepted { get; set; }

        [Display(Name = "ClaimWaybillList_Display_WaybillNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WaybillNo { get; set; }
    }
}
