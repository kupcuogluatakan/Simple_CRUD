using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.SparePartSaleDetail
{
    public class OSparePartSaleDetailListModel : BaseListWithPagingModel
    {
        public OSparePartSaleDetailListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"PartsNameCode", "PART_NAME_CODE"},
                    {"DeliverBillyNo","VAYBILL_NO"},
                    {"PlanQuantity", "PLAN_QUANTITY"},
                    {"DiscountPrice", "DISCOUNT_PRICE"},
                    {"ReasonText", "RETURN_REASON_TEXT"},
                    {"Status", "STATUS"}
                };
            SetMapper(dMapper);
        }

        public OSparePartSaleDetailListModel()
        {
        }

        public Int64 SparePartSaleDetailId { get; set; }

        public Int64 SparePartSaleId { get; set; }

        public int PartId { get; set; }

        [Display(Name = "OtokarPartSaleDetail_Display_PartNameCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartsNameCode { get; set; }

        [Display(Name = "OtokarPartSaleDetail_Display_DiscountPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal DiscountPrice { get; set; }

        [Display(Name = "OtokarPartSaleDetail_Display_DeliveryBillNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DeliverBillyNo { get; set; }

        [Display(Name = "OtokarSparePartSaleDetail_Display_PlanQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PlanQuantity { get; set; }

        [Display(Name = "OtokarPartSaleDetail_Display_ReasonText", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ReasonText { get; set; }

        [Display(Name = "Global_Display_Status", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Status { get; set; }
        [Display(Name = "Global_Display_Status", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int StatusId { get; set; }
    }
}
