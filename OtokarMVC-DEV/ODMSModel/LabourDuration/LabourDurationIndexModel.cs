using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ODMSCommon.Resources;

namespace ODMSModel.LabourDuration
{
    public class LabourDurationIndexModel : IndexModelBase
    {
        public int? LabourId { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public bool? IsActive { get; set; }

        [Display(Name = "LabourDuration_Display_LabourList", ResourceType = typeof(MessageResource))]
        public List<SelectListItem> LabourList { get; set; }

        [Display(Name = "LabourDuration_Display_VehicleModelList", ResourceType = typeof(MessageResource))]
        public List<SelectListItem> VehicleModelList { get; set; }

        [Display(Name = "LabourDuration_Display_VehicleTypeList", ResourceType = typeof(MessageResource))]
        public List<SelectListItem> VehicleTypeList { get; set; }

        [Display(Name = "LabourDuration_Display_Duration", ResourceType = typeof(MessageResource))]
        public double? Duration { get; set; }
        [Display(Name = "LabourDuration_Display_VehicleTypeName",
ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleType { get; set; }

        [Display(Name = "LabourDuration_Display_EngineType",
ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string EngineType { get; set; }


        [Display(Name = "WorkOrderDetailReport_Display_ModelKodList",
         ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ModelKodList { get; set; }
        public LabourDurationIndexModel()
        {
            VehicleModelList = new List<SelectListItem>();
            VehicleTypeList = new List<SelectListItem>();
        }
    }
}
