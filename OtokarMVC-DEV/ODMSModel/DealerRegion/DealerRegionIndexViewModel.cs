using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;

namespace ODMSModel.DealerRegion
{

    [Validator(typeof(DealerRegionIndexViewModelValidator))]
    public class DealerRegionIndexViewModel : ModelBase
    {
        public DealerRegionIndexViewModel()
        {
        }
        public bool HideFormElements { get; set; }

        //DealerRegionId
        public int DealerRegionId { get; set; }

        //DealerRegionName
        [Display(Name = "DealerRegion_Display_Name", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerRegionName { get; set; }
    }
}
