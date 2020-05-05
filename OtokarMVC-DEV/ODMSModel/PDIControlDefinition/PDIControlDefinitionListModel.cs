using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.PDIControlDefinition
{
    public class PDIControlDefinitionListModel : BaseListWithPagingModel
    {
                public PDIControlDefinitionListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"PDIControlCode", "PDI_CONTROL_CODE"},
                    {"PDIControlName", "CONTROL_NAME"},
                    {"ModelKod", "MODEL_KOD"},
                    {"RowNo", "ROW_NO"},
                    {"IsGroupCodeName","IS_GROUP_CODE"},
                    {"IsActiveName", "IS_ACTIVE"}
                };
            SetMapper(dMapper);

        }
        public PDIControlDefinitionListModel()
        {
        }

        [Display(Name = "PDIControlDefinition_Display_PDIControlCode",
            ResourceType = typeof(MessageResource))]
        public int IdPDIControlDefinition { get; set; }

        [Display(Name = "PDIControlDefinition_Display_PDIControlCode",
            ResourceType = typeof(MessageResource))]
        public string PDIControlCode { get; set; }
        [Display(Name = "PDIControlDefinition_Display__PDIControlCode",
           ResourceType = typeof(MessageResource))]
        public string _PDIControlCode { get; set; }

        [Display(Name = "PDIControlDefinition_Display_PDIControlName",
            ResourceType = typeof(MessageResource))]
        public string PDIControlName { get; set; }
        [Display(Name = "PDIControlDefinition_Display__PDIControlName",
          ResourceType = typeof(MessageResource))]
        public string _PDIControlName { get; set; }

        [Display(Name = "PDIControlDefinition_Display_ModelKod",
            ResourceType = typeof(MessageResource))]
        public string ModelKod { get; set; }

        [Display(Name = "PDIControlDefinition_Display_RowNo",
            ResourceType = typeof(MessageResource))]
        public int RowNo { get; set; }

        [Display(Name = "Global_Display_IsGroupCode", ResourceType = typeof(MessageResource))]
        public bool? IsGroupCode { get; set; }

        [Display(Name = "Global_Display_IsGroupCode", ResourceType = typeof(MessageResource))]
        public string IsGroupCodeName { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public bool? IsActive { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }

        public bool HasActiveControlPartMatch { get; set; }
    }
    
}
