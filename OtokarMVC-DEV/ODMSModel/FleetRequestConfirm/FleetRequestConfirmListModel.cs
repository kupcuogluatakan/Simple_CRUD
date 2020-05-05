using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.FleetRequestConfirm
{
    public class FleetRequestConfirmListModel : BaseListWithPagingModel
    {
        public FleetRequestConfirmListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"FleetRequestId", "FLEET_REQUEST_ID"},
                     {"Description", "DESCRIPTION"},
                     {"CreateDate", "CREATE_DATE"},
                     {"StatusName","FR.FLEET_REQUEST_STATUS_LOOKVAL"}
                 };
            SetMapper(dMapper);
        }

        public FleetRequestConfirmListModel() { }

        public int FleetRequestId { get; set; }

        [Display(Name = "FleetRequest_Display_RequestDealer", ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "Global_Display_Description", ResourceType = typeof(MessageResource))]
        public string Description { get; set; }

        public int? StatusId { get; set; }
        [Display(Name = "Global_Display_Status", ResourceType = typeof(MessageResource))]
        public string StatusName { get; set; }

        [Display(Name = "Global_Display_CreateDate", ResourceType = typeof(MessageResource))]
        public DateTime CreateDate { get; set; }
        [Display(Name = "FleetRequest_Display_Cancel_Description", ResourceType = typeof(MessageResource))]
        public string RejectDescription { get; set; }
    }
}
