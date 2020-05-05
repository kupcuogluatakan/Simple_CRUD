using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using ODMSCommon.Resources;
using ODMSModel.ViewModel;
using FluentValidation.Attributes;

namespace ODMSModel.LabourType
{
    [Validator(typeof(LabourTypeDetailModelValidator))]
    public class LabourTypeDetailModel : ModelBase
    {
        public int Id { get; set; }

        private MultiLanguageModel multiLangName;

        [Display(Name = "LabourType_Display_Name", ResourceType = typeof(MessageResource))]
        public MultiLanguageModel MultiLanguageName
        {
            get { return multiLangName ?? new MultiLanguageModel(); }
            set { multiLangName = value; }
        }

        public string MultiLanguageContentAsText { get; set; }

        [Display(Name = "LabourType_Display_Name", ResourceType = typeof(MessageResource))]
        public string Name { get; set; }

        [Display(Name = "LabourType_Display_VatRatio", ResourceType = typeof(MessageResource))]
        public decimal VatRatio { get; set; }

        [Display(Name = "LabourType_Display_VatRatio", ResourceType = typeof(MessageResource))]
        public string VatRatioString
        {
            get { return Convert.ToString(VatRatio, CultureInfo.InvariantCulture); }
            set { VatRatio = decimal.Parse(string.IsNullOrEmpty(value) ? "0" : value, CultureInfo.InvariantCulture); }
        }


        [Display(Name = "LabourType_Display_Description", ResourceType = typeof(MessageResource))]
        public string Description { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public string IsActiveString
        {
            get
            {
                if (IsActive)
                    return MessageResource.Global_Display_Active;
                return MessageResource.Global_Display_Passive;
            }
        }

        public bool HideFormElements { get; set; }


        public LabourTypeDetailModel()
        {
            MultiLanguageName = new MultiLanguageModel();
        }
    }
}
