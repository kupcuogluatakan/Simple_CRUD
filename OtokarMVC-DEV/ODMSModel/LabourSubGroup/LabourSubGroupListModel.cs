using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.LabourSubGroup
{
    public class LabourSubGroupListModel : BaseListWithPagingModel
    {
        public LabourSubGroupListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"SubGroupId", "LSG.ID_SUB_GROUP"},
                    {"Description", "ADMIN_DESC"},
                    {"StateName", "IS_ACTIVE"},
                    {"MainGroupName", "LMGL.LABOUR_GROUP_DESC"}   ,
                    {"LabourSubGroupName","LSGL.LABOUR_SUBGROUP_DESC"}
                };
            SetMapper(dMapper);
        }

        public LabourSubGroupListModel()
        {

        }

        [Display(Name = "LabourSubGroup_Display_SubGroupId", ResourceType = typeof(MessageResource))]
        public string SubGroupId { get; set; }

        [Display(Name = "LabourMainGroup_Display_LabourMainGroupId", ResourceType = typeof(MessageResource))]
        public string MainGroupId { get; set; }

        [Display(Name = "LabourMainGroup_Display_LabourMainGroupName", ResourceType = typeof(MessageResource))]
        public string MainGroupName { get; set; }

        [Display(Name = "LabourSubGroup_Display_Description", ResourceType = typeof(MessageResource))]
        public string Description { get; set; }

        [Display(Name = "LabourSubGroup_Display_LabourSubGroupName", ResourceType = typeof(MessageResource))]
        public string LabourSubGroupName { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IsActive { get; set; }

        [Display(Name = "LabourSubGroup_Display_StateName", ResourceType = typeof(MessageResource))]
        public string StateName { get; set; }
    }
}
