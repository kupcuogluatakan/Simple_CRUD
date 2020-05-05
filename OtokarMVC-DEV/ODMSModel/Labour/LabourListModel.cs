using Kendo.Mvc.UI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.Labour
{
    public class LabourListModel : BaseListWithPagingModel
    {
        public LabourListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"LabourId", "ID_LABOUR"},
                    {"LabourMainGrp", "LABOUR_GROUP_DESC"},
                    {"LabourSubGrp", "LABOUR_SUBGROUP_DESC"},
                    {"LabourCode","LABOUR_CODE"},
                    {"LabourSSID", "LABOUR_SSID"},
                    {"LabourName","LABOUR_NAME"},
                    {"RepairCode","REPAIR_CODE"},
                    {"IsActiveS", "IS_ACTIVE_STRING"},
                    {"LabourType", "LL.LABOUR_TYPE_DESC"}
                };
            SetMapper(dMapper);

        }
        public LabourListModel()
        {
        }

        public int LabourId { get; set; }

        [Display(Name = "Labour_Display_MainGrp", ResourceType = typeof(MessageResource))]
        public string LabourMainGrp { get; set; }

        [Display(Name = "Labour_Display_SubGrp", ResourceType = typeof(MessageResource))]
        public string LabourSubGrp { get; set; }

        [Display(Name = "Labour_Display_RepairCode", ResourceType = typeof(MessageResource))]
        public string RepairCode { get; set; }

        [Display(Name = "Labour_Display_Code", ResourceType = typeof(MessageResource))]
        public string LabourCode { get; set; }

        [Display(Name = "Labour_Display_SSID", ResourceType = typeof(MessageResource))]
        public string LabourSSID { get; set; }

        [Display(Name = "Labour_Display_Name", ResourceType = typeof(MessageResource))]
        public string LabourName { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public int? IsActiveSearch { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public bool IsActive { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public string IsActiveS { get; set; }

        [Display(Name = "Labour_Display_DealerDuration", ResourceType = typeof(MessageResource))]
        public bool IsDealerDuration { get; set; }

        [Display(Name = "Labour_Display_Description", ResourceType = typeof(MessageResource))]
        public string Description { get; set; }

        [Display(Name = "Labour_Display_MainGrp", ResourceType = typeof(MessageResource))]
        public int? LabourMainGroupId { get; set; }

        [Display(Name = "Labour_Display_SubGrp", ResourceType = typeof(MessageResource))]
        public int? LabourSubGroupId { get; set; }

        [Display(Name = "LabourType_Display_Name", ResourceType = typeof(MessageResource))]
        public int? LabourTypeId { get; set; }

        [Display(Name = "LabourType_Display_Name", ResourceType = typeof(MessageResource))]
        public string LabourType { get; set; }
    }
}
