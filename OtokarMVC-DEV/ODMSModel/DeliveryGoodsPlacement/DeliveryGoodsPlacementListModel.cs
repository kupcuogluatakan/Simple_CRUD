using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.DeliveryGoodsPlacement
{
    public class DeliveryGoodsPlacementListModel : BaseListWithPagingModel
    {

        public DeliveryGoodsPlacementListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"DeliveryId", "ID_DELIVERY"},
                    {"Status","STATUS"},
                    {"SapDeliveryNo","SAP_DELIVERY_NO"},
                    {"WayBillNo", "VAYBILL_NO"},
                    {"WayBillDate", "WAYBILL_DATE"},
                    {"AcceptedByUser", "ACCEPTED_BY_USER"}
                };
            SetMapper(dMapper);
        }

        public DeliveryGoodsPlacementListModel()
        {
            
        }

        public Int64 DeliveryId { get; set; }
        [Display(Name = "Global_Display_Status", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? StatusId { get; set; }
        [Display(Name = "Global_Display_Status", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Status { get; set; }

        [Display(Name = "DeliveryPlacement_Display_SapDelivery", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SapDeliveryNo { get; set; }

        [Display(Name = "DeliveryPlacement_Display_WayBillNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WayBillNo { get; set; }

        [Display(Name = "DeliveryPlacement_Display_WayBillDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? WayBillDate { get; set; }

        [Display(Name = "DeliveryPlacement_Display_IsPlaced", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public bool IsPlaced { get; set; }
        [Display(Name = "DeliveryPlacement_Display_IsPlaced", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsPlacedS { get; set; }

        [Display(Name = "DeliveryPlacement_Display_AcceptedUser", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AcceptedByUser { get; set; }

        public int? IdDealer { get; set; }
    }
}
