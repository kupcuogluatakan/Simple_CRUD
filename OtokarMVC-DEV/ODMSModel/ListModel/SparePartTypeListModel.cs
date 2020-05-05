using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;

namespace ODMSModel.ListModel
{
    public class SparePartTypeListModel: BaseListWithPagingModel
    {
        public SparePartTypeListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"PartTypeCode", "PART_TYPE_CODE"},
                    {"AdminDesc", "ADMIN_DESC"},
                    {"PartTypeName", "PART_TYPE_NAME"},
                    {"IsActiveName","IS_ACTIVE"}
                };
            SetMapper(dMapper);
        }

        public SparePartTypeListModel()
        {
        }

        [Display(Name = "SparePartType_Display_PartTypeCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartTypeCode { get; set; }

        [Display(Name = "SparePartType_Display_AdminDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AdminDesc { get; set; }

        [Display(Name = "SparePartType_Display_PartTypeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartTypeName { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public int? IsActive { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }
    }
}
