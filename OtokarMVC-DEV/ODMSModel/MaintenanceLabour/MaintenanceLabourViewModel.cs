using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using  FluentValidation.Attributes;
namespace ODMSModel.MaintenanceLabour
{
    [Validator(typeof(MaintenanceLabourViewModelValidator))]
    public class MaintenanceLabourViewModel:ModelBase
    {
        [Display(Name = "Maintenance_Display_Name", ResourceType = typeof(MessageResource))]
        public int MaintenanceId { get; set; }
        [Display(Name = "Maintenance_Display_Name", ResourceType = typeof(MessageResource))]
        public string MaintenanceName { get; set; }
        [Display(Name = "Labour_Display_Name", ResourceType = typeof(MessageResource))]
        public int LabourId { get; set; }
        public string _LabourId { get; set; }
        [Display(Name = "Labour_Display_Name", ResourceType = typeof(MessageResource))]
        public string LabourName { get; set; }
        [Display(Name = "Global_Display_Quantity", ResourceType = typeof(MessageResource))]
        public decimal? Quantity { get; set; }
        [Display(Name = "MaintenanceLabour_Display_IsMust", ResourceType = typeof(MessageResource))]
        public bool? IsMust { get; set; }
        [Display(Name = "MaintenanceLabour_Display_IsMust", ResourceType = typeof(MessageResource))]
        public string IsMustString { get; set; }

        public bool IsCreate { get; set; }

        public bool HideElements { get; set; }

        public string LabourCode { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsActiveString { get; set; }
    }
}
