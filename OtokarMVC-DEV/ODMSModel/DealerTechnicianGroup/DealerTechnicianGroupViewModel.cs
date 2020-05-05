using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;
using System.Collections.Generic;

namespace ODMSModel.DealerTechnicianGroup
{
    [Validator(typeof(DealerTechnicianGroupViewModelValidator))]
    public class DealerTechnicianGroupViewModel:ModelBase
    {
        public int DealerTechnicianGroupId { get; set; }
        [Display(Name = "DealerTechnicianGroup_Display_TechnicianGroupName", ResourceType = typeof(MessageResource))]
        public string TechnicianGroupName { get; set; }

        public int? WorkshopTypeId { get; set; }
        [Display(Name = "DealerTechnicianGroup_Display_WorkshopTypeName", ResourceType = typeof(MessageResource))]
        public string WorkshopTypeName { get; set; }

        [Display(Name = "DealerTechnicianGroup_Display_Description", ResourceType = typeof(MessageResource))]
        public string Description { get; set; }

        public int? DealerId { get; set; }
        [Display(Name = "DealerTechnicianGroup_Display_DealerName", ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }

        public List<string> VehicleModelKodList { get; set; }
        [Display(Name = "DealerTechnicianGroup_Display_VehicleModelKod", ResourceType = typeof(MessageResource))]
        public string VehicleModelKod { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }

        public string VehicleModelKodOld { get; set; }
    }
}
