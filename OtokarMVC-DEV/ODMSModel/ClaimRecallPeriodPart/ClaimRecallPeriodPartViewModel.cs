using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;

namespace ODMSModel.ClaimRecallPeriodPart
{
    [Validator(typeof(ClaimRecallPeriodPartViewModelValidator))]
    public class ClaimRecallPeriodPartViewModel : ModelBase
    {
        public ClaimRecallPeriodPartViewModel()
        {
        }

        [Display(Name = "WorkOrderCard_Display_Part", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public long PartId { get; set; }
        [Display(Name = "ClaimRecallPeriodPart_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }

        [Display(Name = "ClaimRecallPeriodPart_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }
        [Display(Name = "ClaimRecallPeriodPart_Display_CreateUser", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CreateUserName { get; set; }
        [Display(Name = "ClaimRecallPeriodPart_Display_UpdateUser", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string UpdateUserName { get; set; }
    }
}
