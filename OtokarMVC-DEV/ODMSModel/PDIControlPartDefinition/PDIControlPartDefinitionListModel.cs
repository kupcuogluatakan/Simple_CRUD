using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.PDIControlPartDefinition
{
    public class PDIControlPartDefinitionListModel : BaseListWithPagingModel
    {
                public PDIControlPartDefinitionListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"PDIControlCode", "PDI_CONTROL_CODE"},
                    {"PDIModelCode", "MODEL_KOD"},
                    {"PDIPartCode", "PDI_PART_CODE"},
                    {"IsActiveName", "IS_ACTIVE"}
                };
            SetMapper(dMapper);

        }
        public PDIControlPartDefinitionListModel()
        {
        }

        public string MultiLanguageContentAsText { get; set; }

        public int? IdPDIControlDefinition { get; set; }

        [Display(Name = "PDIControlPartDefinition_Display_PDIControlModelCode",
            ResourceType = typeof(MessageResource))]
        public string PDIControlModelCode { get; set; }
        
        [Display(Name = "PDIControlPartDefinition_Display_PDIControlCode",
            ResourceType = typeof(MessageResource))]
        public string PDIControlCode { get; set; }

        [Display(Name = "PDIControlPartDefinition_Display_PDIModelCode",
            ResourceType = typeof(MessageResource))]
        public string PDIModelCode { get; set; }

        [Display(Name = "PDIControlPartDefinition_Display_PDIPartCode",
            ResourceType = typeof(MessageResource))]
        public string PDIPartCode { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public bool? IsActive { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }
    }
    
}
