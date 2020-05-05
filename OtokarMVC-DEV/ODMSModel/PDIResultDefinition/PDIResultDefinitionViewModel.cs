using ODMSModel.ViewModel;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.PDIResultDefinition
{
    [Validator(typeof (PDIResultDefinitionViewModelValidator))]
    public class PDIResultDefinitionViewModel : ModelBase
    {

        public PDIResultDefinitionViewModel()
        {
            ResultNameML = new MultiLanguageModel();
        }

        public bool HideFormElements { get; set; }

        public string MultiLanguageContentAsText { get; set; }
        
        [Display(Name = "PDIResultDefinition_Display_PDIResultCode",
            ResourceType = typeof (MessageResource))]
        public string PDIResultCode { get; set; }

        [Display(Name = "PDIResultDefinition_Display_AdminDesc",
            ResourceType = typeof (MessageResource))]
        public string AdminDesc { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }

        private MultiLanguageModel _appointmentIndicatorFailureCodeDescName;

        [Display(Name = "PDIResultDefinition_Display_ResultNameML",
            ResourceType = typeof (MessageResource))]
        public MultiLanguageModel ResultNameML
        {
            get { return _appointmentIndicatorFailureCodeDescName ?? new MultiLanguageModel(); }
            set { _appointmentIndicatorFailureCodeDescName = value; }
        }
    }
}
