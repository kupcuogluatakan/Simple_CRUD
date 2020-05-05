using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.Workshop
{
    [Validator(typeof(WorkshopDetailModelValidator))]
    public class WorkshopDetailModel : ModelBase
    {
        public int Id { get; set; }

        [Display(Name = "Workshop_Display_Name", ResourceType = typeof(MessageResource))]
        public string Name { get; set; }

        public int DealerId { get; set; }

        [Display(Name = "Dealer_Display_Name", ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public string IsActiveString
        {
            get
            {
                if (IsActive)
                    return MessageResource.Global_Display_Active;
                return MessageResource.Global_Display_Passive;
            }
        }
    }

}
