using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;
using ODMSModel.ViewModel;

namespace ODMSModel.EducationType
{
    [Validator(typeof(EducationTypeDetailModelValidator))]
    public class EducationTypeDetailModel : ModelBase
    {
        public int Id { get; set; }

        [Display(Name = "Global_Display_Description", ResourceType = typeof(MessageResource))]
        public string Description { get; set; }

        public string MultiLanguageContentAsText { get; set; }

        private MultiLanguageModel multiLangName;
        [Display(Name = "EducationType_Display_Name", ResourceType = typeof(MessageResource))]
        public MultiLanguageModel MultiLanguageName
        {
            get { return multiLangName ?? new MultiLanguageModel(); }
            set { multiLangName = value; }
        }

        [Display(Name = "EducationType_Display_Name", ResourceType = typeof(MessageResource))]
        public string Name { get; set; }

        public EducationTypeDetailModel()
        {
            MultiLanguageName = new MultiLanguageModel();
        }
    }
}
