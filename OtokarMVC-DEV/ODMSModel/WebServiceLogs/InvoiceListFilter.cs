using ODMSCommon.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.WebServiceLogs
{
    public class InvoiceListFilter
    {
        [Display(Name = "User_Display_UserCode", ResourceType =typeof(MessageResource))]
        public string UserCode { get; set; }
        [Display(Name = "WorkOrderPartHistoryReport_Display_BeginDate", ResourceType = typeof(MessageResource))]
        public DateTime? StartDate { get; set; }
        [Display(Name = "WorkshopWorker_Display_EndDate", ResourceType = typeof(MessageResource))]
        public DateTime? EndDate { get; set; }
       

    }
}
