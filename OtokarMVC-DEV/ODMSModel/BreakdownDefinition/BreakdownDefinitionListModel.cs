using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.BreakdownDefinition
{
    public class BreakdownDefinitionListModel : BaseListWithPagingModel
    {
        public BreakdownDefinitionListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"AdminDesc", "ADMIN_DESC"},
                    {"IsActive","IS_ACTIVE"},
                    {"BreakdownName", "BREKDOWN_NAME"},
                    {"PdiBreakdownCode", "PDI_BREAKDOWN_CODE"}
                };
            SetMapper(dMapper);
        }

        public BreakdownDefinitionListModel()
        { 
        }

        [Display(Name = "BreakdownDefinition_Display_PdiBreakdownCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PdiBreakdownCode { get; set; }

        [Display(Name = "BreakdownDefinition_Display_AdminDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AdminDesc { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IsActive { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsActiveName { get; set; }

        [Display(Name = "BreakdownDefinition_Display_BreakdownName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string BreakdownName { get; set; }
    }
}
