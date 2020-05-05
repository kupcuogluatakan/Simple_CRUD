using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;

namespace ODMSModel.PDIGOSApproveGroupDealers
{
    public class PDIGOSApproveGroupDealersListModel : BaseListWithPagingModel
    {
        public PDIGOSApproveGroupDealersListModel([DataSourceRequest] DataSourceRequest request) : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"IdGrpoup", "ID_GROUP"},
                    {"IdDealer","ID_DEALER"}
                };
            SetMapper(dMapper);
        }

        public PDIGOSApproveGroupDealersListModel()
        {
        }

        //[Display(Name = "AnnouncementRole_Display_IdAnnouncement", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IdGroup { get; set; }

        //[Display(Name = "AnnouncementRole_Display_IdRoleType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int32? IdDealer { get; set; }

        public string DealerName { get; set; }
    }
}
