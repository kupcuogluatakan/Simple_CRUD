using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.CustomerSparePartDiscount
{
    public class CustomerSparePartDiscountListModel : BaseListWithPagingModel
    {

        public CustomerSparePartDiscountListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"CustomerSparePartDiscountId" , "ID_CRM_CUSTOMER_SPAREPART_DISCOUNT"},
                    {"DealerName","DEALER_SHRT_NAME"},
                    {"CustomerName","CUSTOMER_NAME"},
                    {"PartId","ID_PART"},
                    {"DiscountRatio","DISCOUNT_RATIO"},
                    {"PartName","PART_NAME"},
                    {"PartCode","PART_CODE"}
                };
            SetMapper(dMapper);
        }
        public CustomerSparePartDiscountListModel()
        {

        }
        public long CustomerSparePartDiscountId { get; set; }
        [Display(Name = "CriticalStockQuantity_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int DealerId { get; set; }
        [Display(Name = "CustomerAddress_Display_CustomerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public long? CustomerId { get; set; }
        [Display(Name = "StockCard_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public long PartId { get; set; }
        [Display(Name = "Dealer_Sale_Sparepart_Display_DiscountRatio", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal DiscountRatio { get; set; }
        [Display(Name = "CriticalStockQuantity_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }
        [Display(Name = "CustomerAddress_Display_CustomerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerName { get; set; }
        [Display(Name = "StockCard_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }
        [Display(Name = "StockCard_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }

        [Display(Name = "SparePart_Display_ClassCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SparePartClassCode { get; set; }
        [Display(Name = "CustomerSparePartDiscount_Display_OrgTypeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string OrgTypeName { get; set; }
        [Display(Name = "CustomerSparePartDiscount_Display_IsApplicableToWorkOrder", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsApplicableToWorkOrderName { get; set; }
        [Display(Name = "Appointment_Display_Customer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerIdList { get; set; }
        [Display(Name = "PartStockReport_Part_Class", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartClassList { get; set; }

    }
}

