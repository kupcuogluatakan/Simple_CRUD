using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSModel.ViewModel;

namespace ODMSModel.Currency
{
    [Validator(typeof(CurrencyIndexViewModelValidator))]
    public class CurrencyIndexViewModel : ModelBase
    {
        public CurrencyIndexViewModel()
        {
        }
        public bool HideFormElements { get; set; }
        public string MultiLanguageContentAsText { get; set; }

        //CurrencyCode
        [Display(Name = "Currency_Display_CurrencyCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CurrencyCode { get; set; }

        //AdminName
        [Display(Name = "Currency_Display_AdminName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AdminName { get; set; }

        //ListOrder
        [Display(Name = "Currency_Display_ListOrder", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? ListOrder { get; set; }

        //DecimalPartName
        [Display(Name = "Currency_Display_DecimalPartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DecimalPartName { get; set; }

        //CurrencyName
        private MultiLanguageModel _currencyName;
        [Display(Name = "Currency_Display_CurrencyName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public MultiLanguageModel CurrencyName { get { return _currencyName ?? new MultiLanguageModel(); } set { _currencyName = value; } }
        
        //IsActive
        public new bool IsActive { get; set; }
        [Display(Name = "User_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsActiveName { get; set; }
    }
}
