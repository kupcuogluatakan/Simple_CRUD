using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.FleetRequest
{
    public class FleetRequestListModel : BaseListWithPagingModel
    {
        public FleetRequestListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"FleetRequestId", "FLEET_REQUEST_ID"},
                     {"Description", "DESCRIPTION"},
                     {"StatusName","FR.FLEET_REQUEST_STATUS_LOOKVAL"},
                     {"DealerName","DEALER_NAME"},
                 };
            SetMapper(dMapper);
        }

        public FleetRequestListModel() { }

        public int FleetRequestId { get; set; }

        public int DealerId { get; set; }
        [Display(Name = "FleetRequest_Display_RequestDealer", ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }
        
        [Display(Name = "Global_Display_Description", ResourceType = typeof(MessageResource))]
        public string Description { get; set; }

        public int? StatusId { get; set; }
        [Display(Name = "Global_Display_Status", ResourceType = typeof(MessageResource))]
        public string StatusName { get; set; }

        [Display(Name = "FleetRequest_Display_Cancel_Description", ResourceType = typeof(MessageResource))]
        public string RejectDescription { get; set; }

    }
}
