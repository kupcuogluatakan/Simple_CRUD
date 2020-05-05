using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;

namespace ODMSModel.FleetRequestVehicleApprove
{
    public class FleetRequestApproveViewModel
    {
        public int  FleetRequestId { get; set; }
        [Display(Name = "FleetRequestApprove_Display_RequestDescription",ResourceType = typeof(MessageResource))]
        public string Description { get; set; }
        [Display(Name = "FleetRequestApprove_Display_RequestStatus", ResourceType = typeof(MessageResource))]
        public string FleetRequestStatus { get; set; }
        public bool HideElements { get; set; }

        public List<FleetRequestApproveListModel> Requests { get; set; }

        public FleetRequestApproveViewModel()
        {
            Requests= new List<FleetRequestApproveListModel>();
        }
    }
}
