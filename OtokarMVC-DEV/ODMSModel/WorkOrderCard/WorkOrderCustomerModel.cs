using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;

namespace ODMSModel.WorkOrderCard
{
    public class WorkOrderCustomerModel:Customer.CustomerIndexViewModel
    {
        [Display(Name = "Appointment_Display_ContactAddress", ResourceType = typeof(MessageResource))]
        public string CustomerAddress { get; set; }
        public string Staff { get; set; }//teslim eden 
    }
}
