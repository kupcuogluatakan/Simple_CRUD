using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.PDIPartDefinition
{
    public class PDIPartDefinitionListModel : BaseListWithPagingModel
    {
        public PDIPartDefinitionListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"AdminDesc", "ADMIN_DESC"},
                    {"IsActive","IS_ACTIVE"},
                    {"PartName", "PART_NAME"},
                    {"PdiPartCode", "PDI_PART_CODE"}
                };
            SetMapper(dMapper);
        }

        public PDIPartDefinitionListModel()
        { 
        }

        [Display(Name = "PartDefinition_Display_PdiPartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PdiPartCode { get; set; }

        [Display(Name = "PartDefinition_Display_AdminDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AdminDesc { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public bool? IsActive { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsActiveName { get; set; }

        [Display(Name = "PartDefinition_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        public bool HasActiveControlPartMatch { get; set; }
    }
}
