using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.ClaimDismantledPartDelivery
{
    public class ClaimDismantledPartDeliveryListModel : BaseListWithPagingModel
    {
        public ClaimDismantledPartDeliveryListModel([DataSourceRequest] DataSourceRequest request) : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"ClaimWayBillNo", "WAYBILL_NO"},
                    {"ClaimWayBillSerialNo", "WAYBILL_SERIAL_NO"},
                    {"ClaimWayBillDate", "WAYBILL_DATE"}
                };
            SetMapper(dMapper);
        }

        public ClaimDismantledPartDeliveryListModel()
        {
            
        }

        public int ClaimWayBillId { get; set; }

        [Display(Name = "ClaimDismantledPartDelivery_Display_ClaimWayBillNo", ResourceType = typeof(MessageResource))]
        public string ClaimWayBillNo { get; set; }

        [Display(Name = "ClaimDismantledPartDelivery_Display_ClaimWayBillSerialNo", ResourceType = typeof(MessageResource))]
        public string ClaimWayBillSerialNo { get; set; }

        [Display(Name = "ClaimDismantledPartDelivery_Display_ClaimWayBillDate", ResourceType = typeof(MessageResource))]
        public DateTime ClaimWayBillDate { get; set; }
    }
}
