using System.ComponentModel.DataAnnotations;
using ODMSModel.ViewModel;
using FluentValidation.Attributes;

namespace ODMSModel.BodyworkDetail
{
    [Validator(typeof(BodyworkDetailModelValidator))]
    public class BodyworkDetailViewModel : ModelBase
    {
        public BodyworkDetailViewModel()
        {
            BodyworkDetailName = new MultiLanguageModel();
        }

        public string MultiLanguageContentAsText { get; set; }

        [Display(Name = "BodyworkDetail_Display_Code", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string BodyworkCode { get; set; }

        [Display(Name = "Global_Display_Description", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Descripion { get; set; }

        private MultiLanguageModel _bodyworkDetailName;
        [Display(Name = "BodyworkDetail_Display_Name", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public MultiLanguageModel BodyworkDetailName { get { return _bodyworkDetailName ?? new MultiLanguageModel(); } set { _bodyworkDetailName = value; } }
    }
}
