using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using ODMSCommon.Resources;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.ViewModel;
using FluentValidation.Attributes;

namespace ODMSModel.AppointmentIndicatorCategory
{
    [Validator(typeof(AppointmentIndicatorCategoryViewModelValidator))]
    public class AppointmentIndicatorCategoryViewModel : ModelBase, IExcelValidation<AppointmentIndicatorCategoryViewModel>
    {
        public AppointmentIndicatorCategoryViewModel()
        {
            AppointmentIndicatorCategoryName = new MultiLanguageModel();
        }

        public bool HideFormElements { get; set; }

        public string MultiLanguageContentAsText { get; set; }

        public int AppointmentIndicatorCategoryId { get; set; }

        public string _AppointmentIndicatorCategoryName { get; set; }

        public int? AppointmentIndicatorMainCategoryId { get; set; }
        [Display(Name = "AppointmentIndicatorCategory_Display_MainCategoryName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AppointmentIndicatorMainCategoryName { get; set; }

        [Display(Name = "AppointmentIndicatorCategory_Display_Code", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Code { get; set; }

        [Display(Name = "AppointmentIndicatorCategory_Display_AdminDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AdminDesc { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public new bool IsActive { get; set; }
        [Display(Name = "Global_Display_Active", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsActiveName { get; set; }

        private MultiLanguageModel _appointmentIndicatorCategoryName;
        [Display(Name = "AppointmentIndicatorCategory_Display_Name", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public MultiLanguageModel AppointmentIndicatorCategoryName { get { return _appointmentIndicatorCategoryName ?? new MultiLanguageModel(); } set { _appointmentIndicatorCategoryName = value; } }

        public IEnumerable<AppointmentIndicatorCategoryListModel> List { get; set; }

        public Dictionary<int, string> Languages { get; set; }

        public AppointmentIndicatorCategoryViewModel ExcelValidate(AppointmentIndicatorCategoryViewModel model)
        {
            var result = model.List.Any(item => item.Code.ToLower().Contains(model.Code.ToLower()));

            if (result)
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.LabourMainGroup_Error_ExistsRecord;
            }           

            if (model.Languages.All(x => x.Value.ToString(CultureInfo.InvariantCulture) != "TR"))
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.AppointmnetIndicatorCategory_Warning_CategoryName;
            }

            var list = model.Languages.Select(item => item.Value).ToList();

            var listOfLang = list.Except(ODMSCommon.CommonUtility.ListOfLanguage()).ToList();

            if (listOfLang.Any())
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.LabourMainGroup_Warning_MismatchedLanguageOptionsAvailable;
            }

            if (model.AppointmentIndicatorMainCategoryId == 0)
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.AppointmnetIndicatorSubCategory_Warning_MismatchedMainOrCategoryId;
            }

            if (model.Code.Length > 3)
            {
                model.ErrorNo = 1;
                model.ErrorMessage = string.Format(MessageResource.ValidationCode_Length, 3);
            }

            return model;
        }
    }
}
