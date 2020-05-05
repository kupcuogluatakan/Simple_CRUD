using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using FluentValidation.Attributes;
using ODMSCommon.Resources;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.ViewModel;

namespace ODMSModel.LabourMainGroup
{
    [Validator(typeof(LabourMainGroupViewModelValidator))]
    public class LabourMainGroupViewModel : ModelBase, IExcelValidation<LabourMainGroupViewModel>
    {
        public LabourMainGroupViewModel()
        {
            MultiLanguageName = new MultiLanguageModel();
        }

        [Display(Name = "LabourMainGroup_Display_LabourMainGroupId", ResourceType = typeof(MessageResource))]
        public string MainGroupId { get; set; }

        [Display(Name = "LabourMainGroup_Display_LabourGroupName", ResourceType = typeof(MessageResource))]
        public string LabourGroupName { get; set; }

        [Display(Name = "LabourMainGroup_Display_Description", ResourceType = typeof(MessageResource))]
        public string Description { get; set; }

        public string MultiLanguageContentAsText { get; set; }

        private MultiLanguageModel _multiLangName;

        public MultiLanguageModel MultiLanguageName
        {
            get { return _multiLangName ?? new MultiLanguageModel(); }
            set { _multiLangName = value; }
        }

        public IEnumerable<LabourMainGroupListModel> List { get; set; }

        public Dictionary<int, string> Languages { get; set; }

        public LabourMainGroupViewModel ExcelValidate(LabourMainGroupViewModel model)
        {
            var result = model.List.Any(item => item.MainGroupId.Trim().ToLower() == model.MainGroupId.Trim().ToLower());

            if (result)
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.LabourMainGroup_Error_ExistsRecord;
            }

            if (model.Languages.All(x => x.Value.ToString(CultureInfo.InvariantCulture) != "TR"))
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.LabourMainGroup_Warning_LabourMainGroupName;
            }

            var list = model.Languages.Select(item => item.Value).ToList();

            var listOfLang = list.Except(ODMSCommon.CommonUtility.ListOfLanguage()).ToList();

            if (listOfLang.Any())
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.LabourMainGroup_Warning_MismatchedLanguageOptionsAvailable;
            }

            return model;
        }
    }
}
