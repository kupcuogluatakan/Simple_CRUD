using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;
using ODMSModel.ViewModel;

namespace ODMSModel.Equipment
{
    [Validator(typeof(EquipmentViewModelValidator))]
    public class EquipmentViewModel : ModelBase
    {
        public int EquipmentId { get; set; }

        [Display(Name = "EquipmentViewModel_Display_TypeName", ResourceType = typeof(MessageResource))]
        public string EquipmentTypeName { get; set; }

        [Display(Name = "EquipmentViewModel_Display_TypeDesc", ResourceType = typeof(MessageResource))]
        [MaxLength(100, ErrorMessageResourceName = "Equipment_Invalid_Message", ErrorMessageResourceType = typeof(MessageResource))]
        public string EquipmentTypeDesc { get; set; }

        public string EquipmentTypeLangCode { get; set; }

        public string MultiLanguageContentAsText { get; set; }

        private MultiLanguageModel multiLangName;

        [Display(Name = "EducationType_Display_Name", ResourceType = typeof(MessageResource))]
        public MultiLanguageModel MultiLanguageName
        {
            get { return multiLangName ?? new MultiLanguageModel(); }
            set { multiLangName = value; }
        }

        public EquipmentViewModel()
        {
            MultiLanguageName = new MultiLanguageModel();
        }

    }
}
