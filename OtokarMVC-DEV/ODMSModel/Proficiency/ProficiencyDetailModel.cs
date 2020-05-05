using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;
using ODMSModel.ViewModel;

namespace ODMSModel.Proficiency
{
    /// <summary>
    /// DealerId, DealerProficiencyController ve ilgili viewler tarafindan kullanilmaktadir. ProficiencyController ve ilgili viewleri bu modelin
    /// DealerId property'si ile ilgilenmezler
    /// </summary>
    [Validator(typeof(ProficiencyDetailModelValidator))]
    public class ProficiencyDetailModel : ModelBase
    {
        public int DealerId { get; set; }

        public string MultiLanguageContentAsText { get; set; }

        private MultiLanguageModel multiLangName;

        [Display(Name = "Proficiency_Display_Name", ResourceType = typeof(MessageResource))]
        public MultiLanguageModel MultiLanguageName
        {
            get { return multiLangName ?? new MultiLanguageModel(); }
            set { multiLangName = value; }
        }

        [Display(Name = "Proficiency_Display_Code", ResourceType = typeof(MessageResource))]
        public string ProficiencyCode { get; set; }

        [Display(Name = "Proficiency_Display_Name", ResourceType = typeof(MessageResource))]
        public string ProficiencyName { get; set; }

        [Display(Name = "Global_Display_Description", ResourceType = typeof(MessageResource))]
        public string Description { get; set; }

        public ProficiencyDetailModel()
        {
            MultiLanguageName = new MultiLanguageModel();
        }
    }
}
