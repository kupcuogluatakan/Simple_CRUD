using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ODMSCommon.Resources;

namespace ODMSModel.Workshop
{
    public class WorkshopIndexModel : IndexModelBase
    {
        [Display(Name = "Workshop_Display_Name", ResourceType = typeof(MessageResource))]
        public IEnumerable<SelectListItem> DealerList { get; set; }

        [Display(Name = "Workshop_Display_Name", ResourceType = typeof(MessageResource))]
        public string WorkshopName { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public bool? IsActive { get; set; }
        
        public WorkshopIndexModel()
        {
            DealerList = new List<SelectListItem>();
        }
    }
}
