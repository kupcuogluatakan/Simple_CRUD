using System.ComponentModel.DataAnnotations;
using System.Globalization;
using ODMSCommon;
using ODMSCommon.Resources;
using FluentValidation.Attributes;

namespace ODMSModel.DealerRegionResponsible
{
    [Validator(typeof(DealerRegionResponsibleDetailModelValidator))]
    public class DealerRegionResponsibleDetailModel : ModelBase
    {
        [Display(Name = "DealerRegion_Display_Name", ResourceType = typeof(MessageResource))]
        public int? DealerRegionId { get; set; }

        [Display(Name = "DealerRegionResponsible_Display_Responsible", ResourceType = typeof(MessageResource))]
        public int? UserId { get; set; }

        [Display(Name = "DealerRegion_Display_Name", ResourceType = typeof(MessageResource))]
        public string DealerRegionName { get; set; }

        [Display(Name = "DealerRegionResponsible_Display_Name", ResourceType = typeof(MessageResource))]
        public string Name { get; set; }

        [Display(Name = "DealerRegionResponsible_Display_Surname", ResourceType = typeof(MessageResource))]
        public string Surname { get; set; }

        [Display(Name = "User_Display_Phone", ResourceType = typeof(MessageResource))]
        public string Phone { get; set; }

        [Display(Name = "User_Display_EMail", ResourceType = typeof(MessageResource))]
        public string Email { get; set; }

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

        public string DealerRegionIdString
        {
            get { return DealerRegionId == null ? string.Empty : DealerRegionId.GetValue<string>(); }
            set
            {
                int val;
                var result = int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out val);
                DealerRegionId = result ? (int?)val : null;
            }
        }

        public string UserIdString
        {
            get { return UserId == null ? string.Empty : UserId.GetValue<string>(); }
            set
            {
                int val;
                var result = int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out val);
                UserId = result ? (int?)val : null;
            }
        }

    }
}
