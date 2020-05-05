using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;
using ODMSModel.ViewModel;

namespace ODMSModel.SparePartType
{
    [Validator(typeof(SparePartTypeIndexViewModelValidator))]
    public class SparePartTypeIndexViewModel : ModelBase
    {
        public SparePartTypeIndexViewModel()
        {
        }
        public bool HideFormElements { get; set; }
        public string MultiLanguageContentAsText { get; set; }

        //PartTypeCode
        [Display(Name = "SparePartType_Display_PartTypeCode", ResourceType = typeof(MessageResource))]
        public string PartTypeCode { get; set; }
        
        //AdminDesc
        [Display(Name = "SparePartType_Display_AdminDesc", ResourceType = typeof(MessageResource))]
        public string AdminDesc { get; set; }

        //IsActive
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }

        //PartTypeName
        private MultiLanguageModel _partTypeName;
        [Display(Name = "SparePartType_Display_PartTypeName", ResourceType = typeof(MessageResource))]
        public MultiLanguageModel PartTypeName { get { return _partTypeName ?? new MultiLanguageModel(); } set { _partTypeName = value; } }
    }
}
