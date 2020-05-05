using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ODMSCommon.Resources;

namespace ODMSModel.DealerRegionResponsible
{
    public class DealerRegionResponsibleIndexModel : IndexModelBase
    {
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public bool? IsActive { get; set; }

        [Display(Name = "Global_Display_DealerRegionList", ResourceType = typeof(MessageResource))]
        public List<SelectListItem> DealerRegionList { get; set; }

        [Display(Name = "DealerRegionResponsible_Display_Name", ResourceType = typeof(MessageResource))]
        public string Name { get; set; }

        [Display(Name = "DealerRegionResponsible_Display_Surname", ResourceType = typeof(MessageResource))]
        public string Surname { get; set; }

        [Display(Name = "User_Display_Phone", ResourceType = typeof(MessageResource))]
        public string Phone { get; set; }

        [Display(Name = "User_Display_EMail", ResourceType = typeof(MessageResource))]
        public string Email { get; set; }

        public DealerRegionResponsibleIndexModel()
        {
            DealerRegionList = new List<SelectListItem>();
        }
    }
}
