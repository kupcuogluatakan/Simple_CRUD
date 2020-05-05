using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.PDIControlPartDefinition
{
    [Validator(typeof (PDIControlPartDefinitionViewModelValidator))]
    public class PDIControlPartDefinitionViewModel : ModelBase
    {

        public PDIControlPartDefinitionViewModel()
        {
        }

        public bool HideFormElements { get; set; }

        public string MultiLanguageContentAsText { get; set; }

        public int IdPDIControlDefinition { get; set; }

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
            ResourceType = typeof (MessageResource))]
        public string PDIPartCode { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }
    }
}
