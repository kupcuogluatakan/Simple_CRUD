using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.LabourMainGroup
{
    public class LabourMainGroupListModel : BaseListWithPagingModel
    {
        public LabourMainGroupListModel([DataSourceRequest] DataSourceRequest request) : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"MainGroupId", "ID_MAIN_GROUP"},
                    {"LabourGroupName", "LABOUR_GROUP_DESC"},
                    {"Description", "ADMIN_DESC"},
                    {"IsActiveString", "IS_ACTIVE"}
                };
            SetMapper(dMapper);
        }

        public LabourMainGroupListModel()
        {
        }

        [Display(Name = "LabourMainGroup_Display_LabourMainGroupId", ResourceType = typeof(MessageResource))]
        public string MainGroupId { get; set; }

        [Display(Name = "LabourMainGroup_Display_LabourGroupName", ResourceType = typeof(MessageResource))]
        public string LabourGroupName { get; set; }

        [Display(Name = "LabourMainGroup_Display_Description", ResourceType = typeof(MessageResource))]
        public string Description { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IsActive { get; set; }

        [Display(Name = "LabourMainGroup_Display_StateName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StateName { get; set; }
    }
}