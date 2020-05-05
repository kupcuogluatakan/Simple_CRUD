using ODMSModel.ViewModel;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.PDIControlDefinition
{
    [Validator(typeof (PDIControlDefinitionViewModelValidator))]
    public class PDIControlDefinitionViewModel : ModelBase
    {

        public PDIControlDefinitionViewModel()
        {
            ControlNameML = new MultiLanguageModel();
        }

        public bool HideFormElements { get; set; }

        public string MultiLanguageContentAsText { get; set; }

        [Display(Name = "PDIControlDefinition_Display_PDIControlCode",
            ResourceType = typeof (MessageResource))]
        public int IdPDIControlDefinition { get; set; }

        [Display(Name = "PDIControlDefinition_Display_PDIControlCode",
            ResourceType = typeof (MessageResource))]
        public string PDIControlCode { get; set; }

        [Display(Name = "PDIControlDefinition_Display_ModelKod",
            ResourceType = typeof (MessageResource))]
        public string ModelKod { get; set; }

        [Display(Name = "PDIControlDefinition_Display_RowNo",
            ResourceType = typeof (MessageResource))]
        public int? RowNo { get; set; }

        [Display(Name = "Global_Display_IsGroupCode", ResourceType = typeof (MessageResource))]
        public bool? IsGroupCode { get; set; }

        [Display(Name = "Global_Display_IsGroupCode", ResourceType = typeof(MessageResource))]
        public string IsGroupCodeName { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }

        private MultiLanguageModel _appointmentIndicatorFailureCodeDescName;

        [Display(Name = "PDIControlDefinition_Display_ControlNameML",
            ResourceType = typeof (MessageResource))]
        public MultiLanguageModel ControlNameML
        {
            get { return _appointmentIndicatorFailureCodeDescName ?? new MultiLanguageModel(); }
            set { _appointmentIndicatorFailureCodeDescName = value; }
        }

        public bool HasActiveControlPartMatch { get; set; }
    }
}
