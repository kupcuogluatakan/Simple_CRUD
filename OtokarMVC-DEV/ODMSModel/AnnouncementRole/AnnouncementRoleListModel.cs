using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.AnnouncementRole
{
    public class AnnouncementRoleListModel : BaseListWithPagingModel
    {
                public AnnouncementRoleListModel([DataSourceRequest] DataSourceRequest request) : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"IdAnnouncement", "ID_ANNOUNCEMENT"},
                    {"IdRoleType","ROLE_TYPE_ID"}
                };
            SetMapper(dMapper);
        }

        public AnnouncementRoleListModel()
        {
        }

        //IdAnnouncement
        [Display(Name = "AnnouncementRole_Display_IdAnnouncement", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int64? IdAnnouncement { get; set; }

        [Display(Name = "AnnouncementRole_Display_IdRoleType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int32? IdRoleType { get; set; }

        public string RoleTypeName { get; set; }
    }
}
