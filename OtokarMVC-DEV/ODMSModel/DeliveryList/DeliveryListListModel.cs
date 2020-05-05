using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.DeliveryList
{
    public class DeliveryListListModel : BaseListWithPagingModel
    {
        public DeliveryListListModel([DataSourceRequest] DataSourceRequest request) : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"SapDeliveryNo", "SAP_DELIVERY_NO"},
                    {"WaybillNo","VAYBILL_NO"},
                    {"WaybillDate", "WAYBILL_DATE"},
                    {"DeliveryStatusName", "DELIVERY_STATUS_NAME"},
                    {"Sender", "7"},
                };
            SetMapper(dMapper);
        }

        public DeliveryListListModel()
        {
        }

        [Display(Name = "DeliveryList_Display_IdDelivery", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int64? IdDelivery { get; set; }

        [Display(Name = "DeliveryList_Display_SapDeliveryNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SapDeliveryNo { get; set; }

        [Display(Name = "DeliveryList_Display_WaybillNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WaybillNo { get; set; }

        [Display(Name = "DeliveryList_Display_WaybillDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? WaybillDate { get; set; }

        [Display(Name = "DeliveryList_Display_DeliveryStatusName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DeliveryStatusName { get; set; }

        //IdDealer
        [Display(Name = "DeliveryList_Display_DeliveryStatusName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? DeliveryStatus { get; set; }

        public int? IdDealer { get; set; }
        [Display(Name = "DeliveryList_Display_Sender", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Sender { get; set; }
    }
}
