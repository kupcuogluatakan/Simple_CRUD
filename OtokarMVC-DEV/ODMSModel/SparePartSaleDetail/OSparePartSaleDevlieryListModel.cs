using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.SparePartSaleDetail
{
    public class OSparePartSaleDevlieryListModel : BaseListWithPagingModel
    {
        public OSparePartSaleDevlieryListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"PartsNameCode", "PART_NAME_CODE"},
                    {"DeliverBillyNo","VAYBILL_NO"},
                    {"PlanQuantity", "PLAN_QUANTITY"},
                    {"DiscountPrice", "DISCOUNT_PRICE"},
                    {"ReasonText", "RETURN_REASON_TEXT"}
                };
            SetMapper(dMapper);
        }

        public OSparePartSaleDevlieryListModel()
        {
        }

        public Int64 DeliveryId { get; set; }

        public Int64 DeliverySeqNo { get; set; }

        [Display(Name = "OtokarPartSaleDetail_Display_PartNameCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int PartId { get; set; }

        [Display(Name = "OtokarPartSaleDetail_Display_DeliveryBillNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WayBillNo { get; set; }

        [Display(Name = "OtokarPartSaleDetail_Display_DeliveryBillDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime WayBillDate { get; set; }

        [Display(Name = "OtokarSparePartSaleDetail_Display_Quantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Quantity { get; set; }

        [Display(Name = "OtokarPartSaleDetail_Display_DiscountPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Price { get; set; }


    }
}
