using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;

namespace ODMSModel.ListModel
{
    public class CurrencyListModel : BaseListWithPagingModel
    {
        public CurrencyListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"CurrencyCode", "CURRENCY_CODE"},
                    {"CurrencyName","CURRENCY_NAME"},
                    {"AdminName", "ADMIN_NAME"},
                    {"ListOrder", "LIST_ORDER"},
                    {"DecimalPartName","DECIMAL_PART_NAME"},
                    {"IsActiveName","IS_ACTIVE"}
                };
            SetMapper(dMapper);
        }

        public CurrencyListModel()
        {
        }

        [Display(Name = "Currency_Display_CurrencyCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CurrencyCode { get; set; }

        [Display(Name = "Currency_Display_CurrencyName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CurrencyName { get; set; }

        [Display(Name = "Currency_Display_AdminName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AdminName { get; set; }

        [Display(Name = "Currency_Display_ListOrder", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int ListOrder { get; set; }

        [Display(Name = "Currency_Display_DecimalPartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DecimalPartName { get; set; }

        //IsActive
        public int? IsActive { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsActiveName { get; set; }
    }
}
