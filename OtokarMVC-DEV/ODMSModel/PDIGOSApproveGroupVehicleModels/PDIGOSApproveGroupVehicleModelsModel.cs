using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;

namespace ODMSModel.PDIGOSApproveGroupVehicleModels
{
    public class PDIGOSApproveGroupVehicleModelsModel : ModelBase
    {
        [Display(Name = "PDIGOSApproveGroupVehicleModels_Display_GroupId", ResourceType = typeof(MessageResource))]
        public int GroupId { get; set; }
        public List<string> ModelList { get; set; }
    }
}
