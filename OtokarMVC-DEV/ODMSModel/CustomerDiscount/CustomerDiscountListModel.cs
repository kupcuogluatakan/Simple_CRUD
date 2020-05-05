using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.CustomerDiscount
{
    public class CustomerDiscountListModel : BaseListWithPagingModel
    {
        public CustomerDiscountListModel([DataSourceRequest] DataSourceRequest request) : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"CustomerName", "CUSTOMER_NAME"},
                    {"DealerName","DEALER_NAME"},
                    {"PartDiscountRatio", "PART_DISCOUNT_RATIO"},
                    {"LabourDiscountRatio", "LABOUR_DISCOUNT_RATIO"}
                };
            SetMapper(dMapper);
        }

        public CustomerDiscountListModel()
        {
        }

        //IdCustomer
        [Display(Name = "CustomerDiscount_Display_IdCustomer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int64? IdCustomer { get; set; }

        //CustomerName
        [Display(Name = "CustomerDiscount_Display_CustomerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerName { get; set; }

        //DealerName
        [Display(Name = "CustomerDiscount_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }

        //IdDealer
        [Display(Name = "CustomerDiscount_Display_IdDealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IdDealer { get; set; }

        //PartDiscountRation
        [Display(Name = "CustomerDiscount_Display_PartDiscountRation", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? PartDiscountRatio { get; set; }

        //LabourDiscountRation
        [Display(Name = "CustomerDiscount_Display_LabourDiscountRation", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? LabourDiscountRatio { get; set; }
    }
}
