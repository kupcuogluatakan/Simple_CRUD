using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.WebServiceLogs
{
    public class InvoiceListLogItem: BaseListWithPagingModel
    {
        public InvoiceListLogItem(DataSourceRequest request):base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"InvoiceServiceLogId", "InvoiceServiceLogId"},
                     {"CallUserCode", "CallUserCode"},
                     {"CallDate", "CallDate"},
                     {"StartDate", "StartDate"},
                     {"EndDate", "EndDate"},
                     {"IsSuccess", "IsSuccess"},
                     {"ErrorDesc","ErrorDesc"}
                 };
            SetMapper(dMapper);
        }
        public InvoiceListLogItem()
        {

        }
        public long InvoiceServiceLogId { get; set; }
        [Display(Name = "InvoiceList_Display_UserCode",ResourceType =typeof(MessageResource))]

        public string CallUserCode { get; set; }
        [Display(Name = "InvoiceList_Display_CallDate", ResourceType = typeof(MessageResource))]
        public DateTime CallDate { get; set; }
        [Display(Name = "InvoiceList_Display_StartDate", ResourceType = typeof(MessageResource))]

        public DateTime StartDate { get; set; }
        [Display(Name = "InvoiceList_Display_EndDate", ResourceType = typeof(MessageResource))]
        public DateTime EndDate { get; set; }
        [Display(Name = "InvoiceList_Display_IsSuccess", ResourceType = typeof(MessageResource))]
        public bool IsSuccess { get; set; }
        [Display(Name = "InvoiceList_Display_ErrorDesc", ResourceType = typeof(MessageResource))]
        public string ErrorDesc { get; set; }

    }
}
