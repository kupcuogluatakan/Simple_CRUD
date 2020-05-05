using FluentValidation.Attributes;
using ODMSCommon.Resources;
using ODMSModel.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.BreakdownDefinition
{
    [Validator(typeof(BreakdownDefinitionViewModelValidator))]
    public class BreakdownDefinitionViewModel : ModelBase
    {
        public BreakdownDefinitionViewModel()
        {
            BreakdownName = new MultiLanguageModel();
        }

        public bool HideFormElements { get; set; }

        public string MultiLanguageContentAsText { get; set; }

        [Display(Name = "BreakdownDefinition_Display_PdiBreakdownCode", ResourceType = typeof(MessageResource))]
        public string PdiBreakdownCode { get; set; }

        [Display(Name = "BreakdownDefinition_Display_AdminDesc", ResourceType = typeof(MessageResource))]
        public string AdminDesc { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }

        private MultiLanguageModel _breakdownDefinitionLangName;

        [Display(Name = "BreakdownDefinition_Display_BreakdownName", ResourceType = typeof(MessageResource))]
        public MultiLanguageModel BreakdownName { get { return _breakdownDefinitionLangName ?? new MultiLanguageModel(); } set { _breakdownDefinitionLangName = value; } }

        public string BreakdownDesc { get; set; }
    }
}
