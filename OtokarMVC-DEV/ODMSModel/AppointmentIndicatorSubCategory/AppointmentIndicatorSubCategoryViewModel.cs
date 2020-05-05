using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using ODMSCommon.Resources;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.ViewModel;
using FluentValidation.Attributes;

namespace ODMSModel.AppointmentIndicatorSubCategory
{
    [Validator(typeof(AppointmentIndicatorSubCategoryViewModelValidator))]
    public class AppointmentIndicatorSubCategoryViewModel : ModelBase, IExcelValidation<AppointmentIndicatorSubCategoryViewModel>
    {
        public AppointmentIndicatorSubCategoryViewModel()
        {
            AppointmentIndicatorSubCategoryName = new MultiLanguageModel();
        }

        public bool HideFormElements { get; set; }

        public string MultiLanguageContentAsText { get; set; }

        public string _AppointmentIndicatorSubCategoryName { get; set; }

        public int AppointmentIndicatorSubCategoryId { get; set; }

        public int? AppointmentIndicatorMainCategoryId { get; set; }
        [Display(Name = "AppointmentIndicatorCategory_Display_MainCategoryName", ResourceType = typeof(MessageResource))]
        public string AppointmentIndicatorMainCategoryName { get; set; }

        public int? AppointmentIndicatorCategoryId { get; set; }
        [Display(Name = "AppointmentIndicatorCategory_Display_CategoryName", ResourceType = typeof(MessageResource))]
        public string AppointmentIndicatorCategoryName { get; set; }

        [Display(Name = "AppointmentIndicatorSubCategory_Display_SubCode", ResourceType = typeof(MessageResource))]
        public string SubCode { get; set; }

        [Display(Name = "AppointmentIndicatorSubCategory_Display_AdminDesc", ResourceType = typeof(MessageResource))]
        public string AdminDesc { get; set; }

        public bool? IsAutoCreate { get; set; }
        [Display(Name = "AppointmentIndicatorSubCategory_Display_IsAutoCreateName", ResourceType = typeof(MessageResource))]
        public string IsAutoCreateName { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }

        private MultiLanguageModel _appointmentIndicatorSubCategoryName;
        [Display(Name = "AppointmentIndicatorSubCategory_Display_Name", ResourceType = typeof(MessageResource))]
        public MultiLanguageModel AppointmentIndicatorSubCategoryName { get { return _appointmentIndicatorSubCategoryName ?? new MultiLanguageModel(); } set { _appointmentIndicatorSubCategoryName = value; } }

        [Display(Name = "AppointmentIndicatorSubCategory_Title_IndicatorTypeCode", ResourceType = typeof(MessageResource))]
        public string IndicatorTypeCode { get; set; }

        public IEnumerable<AppointmentIndicatorSubCategoryListModel> List { get; set; }

        public IEnumerable<string> IndicatorTypeCodeList { get; set; }

        public Dictionary<int, string> Languages { get; set; }

        public AppointmentIndicatorSubCategoryViewModel ExcelValidate(AppointmentIndicatorSubCategoryViewModel model)
        {
            var result = model.List.Any(item => item.SubCode.ToLower().Contains(model.SubCode.ToLower()));

            if (result)
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.LabourMainGroup_Error_ExistsRecord;
            }

            if (model.Languages.All(x => x.Value.ToString(CultureInfo.InvariantCulture) != "TR"))
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.AppointmnetIndicatorSubCategory_Warning_SubCategoryName;
            }

            var list = model.Languages.Select(item => item.Value).ToList();

            var listOfLang = list.Except(ODMSCommon.CommonUtility.ListOfLanguage()).ToList();

            if (listOfLang.Any())
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.LabourMainGroup_Warning_MismatchedLanguageOptionsAvailable;
            }

            if (model.AppointmentIndicatorMainCategoryId == 0 || model.AppointmentIndicatorCategoryId == 0)
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.AppointmnetIndicatorSubCategory_Warning_MismatchedMainOrCategoryId;
            }

            if (model.SubCode.Length > 9)
            {
                model.ErrorNo = 1;
                model.ErrorMessage = string.Format(MessageResource.ValidationCode_Length, 3);
            }

            var existsIndicatorTypecCode = model.IndicatorTypeCodeList.Any(x => x.ToString(CultureInfo.InvariantCulture) == model.IndicatorTypeCode);
            if (!existsIndicatorTypecCode)
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.AppointmnetIndicatorSubCategory_Warning_NotExistsIndicatorTypeCode;
            }

            return model;
        }
    }
}
