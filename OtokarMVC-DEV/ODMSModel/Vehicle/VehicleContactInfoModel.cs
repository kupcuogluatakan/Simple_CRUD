using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.Vehicle
{
    [Validator(typeof(VehicleContactInfoModelValidator))]
    public class VehicleContactInfoModel
    {
        [Display(Name="WorkOrderCard_Display_VehicleLocation",ResourceType = typeof(MessageResource))]
        public string Location { get; set; }
        [Display(Name = "WorkOrderCard_Display_VehicleResponsible", ResourceType = typeof(MessageResource))]
        public string  ResponsiblePerson { get; set; }
        [Display(Name = "WorkOrderCard_Display_VehicleResponsiblePhone", ResourceType = typeof(MessageResource))]
        public string ResponsiblePersonPhone { get; set; }

        public int ErrorNo{ get; set; }
        public string ErrorDesc { get; set; }
        public int VehicleId { get; set; }
    }
}
