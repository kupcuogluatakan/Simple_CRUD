using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ODMSCommon.Resources;

namespace ODMSModel.Rack
{
    public class RackIndexModel : IndexModelBase
    {
        public int DealerId { get; set; }

        [Display(Name = "Rack_Display_DealerList", ResourceType = typeof(MessageResource))]
        public List<SelectListItem> DealerList { get; set; }

        [Display(Name = "Rack_Display_WarehouseList", ResourceType = typeof(MessageResource))]
        public List<SelectListItem> WarehouseList { get; set; }
        
        [Display(Name = "Rack_Display_Code", ResourceType = typeof(MessageResource))]
        public string Code { get; set; }

        [Display(Name = "Rack_Display_Name", ResourceType = typeof(MessageResource))]
        public string Name { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public bool? IsActive { get; set; }
        
        public bool IsDealerUser
        {
            get { return DealerId != -1; }
        }

        public RackIndexModel()
        {
            DealerId = -1;
            DealerList = new List<SelectListItem>();
            WarehouseList = new List<SelectListItem>();
        }
    }
}
