using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.Delivery
{
    public class DeliveryListModel : BaseListWithPagingModel
    {
        public DeliveryListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"WayBillNo", "VAYBILL_NO"},
                    {"SupplierName","SUPPLIER_NAME"},
                    {"Status","STATUS_NAME"},
                    {"WayBillDate", "WAYBILL_DATE"},
                    {"TotalPrice", "TOTAL_PRICE"},
                    {"IsPlaced","IS_PLACED"}
                };
            SetMapper(dMapper);
        }

        public DeliveryListModel()
        {

        }
        [Display(Name = "Supplier_Display_SupplierName", ResourceType = typeof(MessageResource))]
        public string SupplierName { get; set; }
        [Display(Name = "Supplier_Display_SupplierName", ResourceType = typeof(MessageResource))]
        public long SupplierId { get; set; }
        [Display(Name = "Appointment_Display_Status", ResourceType = typeof(MessageResource))]
        public string Status { get; set; }
        [Display(Name = "Delivery_Display_WayBillNo", ResourceType = typeof(MessageResource))]
        public string WayBillNo { get; set; }
        [Display(Name = "Delievery_Display_DeliveryDate", ResourceType = typeof(MessageResource))]
        public DateTime? WayBillDate { get; set; }
        [Display(Name = "DealerPurchaseOrder_Display_TotalPrice", ResourceType = typeof(MessageResource))]
        public decimal TotalPrice { get; set; }
        [Display(Name = "DealerPurchaseOrder_Display_TotalPrice", ResourceType = typeof(MessageResource))]
        public string TotalPriceString { get; set; }
        [Display(Name = "Global_Display_Cancel", ResourceType = typeof(MessageResource))]
        public bool IsPlaced  { get; set; }

        public string WayBillDateString
        {
            get
            {
                return WayBillDate.HasValue ? WayBillDate.Value.ToString("dd/MM/yyyy").Replace(CommonValues.Dot, CommonValues.Slash) : string.Empty;
            }
        }

        public int StatusId { get; set; }
        [Display(Name = "DealerPurchaseOrder_Display_DeliveryId", ResourceType = typeof(MessageResource))]
        public long DeliveryId { get; set; }
    }
}
