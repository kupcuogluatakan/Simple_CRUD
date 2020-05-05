using FluentValidation.Attributes;
using ODMSCommon.Resources;
using ODMSModel.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.PDIPartDefinition
{
    [Validator(typeof(PDIPartDefinitionViewModelValidator))]
    public class PDIPartDefinitionViewModel : ModelBase
    {
        public PDIPartDefinitionViewModel()
        {
            PartName = new MultiLanguageModel();
        }

        public bool HideFormElements { get; set; }

        public string MultiLanguageContentAsText { get; set; }

        [Display(Name = "PartDefinition_Display_PdiPartCode", ResourceType = typeof(MessageResource))]
        public string PdiPartCode { get; set; }

        [Display(Name = "PartDefinition_Display_AdminDesc", ResourceType = typeof(MessageResource))]
        public string AdminDesc { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }

        private MultiLanguageModel _partDefinitionLangName;

        [Display(Name = "PartDefinition_Display_PartName", ResourceType = typeof(MessageResource))]
        public MultiLanguageModel PartName { get { return _partDefinitionLangName ?? new MultiLanguageModel(); } set { _partDefinitionLangName = value; } }

        public string PartDesc { get; set; }

        public bool HasActiveControlPartMatch { get; set; }
    }
}
