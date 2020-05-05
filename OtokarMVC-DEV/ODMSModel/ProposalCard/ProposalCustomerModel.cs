using ODMSCommon.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.ProposalCard
{
    public class ProposalCustomerModel : Customer.CustomerIndexViewModel
    {
        [Display(Name = "Appointment_Display_ContactAddress", ResourceType = typeof(MessageResource))]
        public string CustomerAddress { get; set; }
        public string Staff { get; set; }//teslim eden 
    }
}
