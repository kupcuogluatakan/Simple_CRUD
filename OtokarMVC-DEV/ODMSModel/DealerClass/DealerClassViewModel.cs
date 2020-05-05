using FluentValidation.Attributes;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;

namespace ODMSModel.DealerClass
{
    [Validator(typeof(DealerClassViewModelValidator))]
    public class DealerClassViewModel : ModelBase
    {
        public DealerClassViewModel()
        { 
        }

        public bool HideFormElements { get; set; }
        public string MultiLanguageContentAsText { get; set; }

        [Display(Name = "DealerClass_Display_DealerClassCode", ResourceType = typeof(MessageResource))]
        public string DealerClassCode { get; set; }

        //SSIdDealerClass
        [Display(Name = "DealerClass_Display_SSIdDealerClass", ResourceType = typeof(MessageResource))]
        public string SSIdDealerClass { get; set; }

        //DealerClassName
        [Display(Name = "DealerClass_Display_DealerClassName", ResourceType = typeof(MessageResource))]
        public string DealerClassName { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }
    }
}
