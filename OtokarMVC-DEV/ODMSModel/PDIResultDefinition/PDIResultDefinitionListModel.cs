using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.PDIResultDefinition
{
    public class PDIResultDefinitionListModel : BaseListWithPagingModel
    {
                public PDIResultDefinitionListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"PDIResultCode", "PDI_Result_CODE"},
                    {"AdminDesc", "ADMIN_DESC"},
                    {"IsActiveName", "IS_ACTIVE"}
                };
            SetMapper(dMapper);

        }
        public PDIResultDefinitionListModel()
        {
        }
        
        [Display(Name = "PDIResultDefinition_Display_PDIResultCode",
            ResourceType = typeof(MessageResource))]
        public string PDIResultCode { get; set; }

        [Display(Name = "PDIResultDefinition_Display_AdminDesc",
            ResourceType = typeof(MessageResource))]
        public string AdminDesc { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public bool? IsActive { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }
    }
    
}
