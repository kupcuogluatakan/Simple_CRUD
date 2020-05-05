using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using FluentValidation.Attributes;
using ODMSCommon.Resources;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.ViewModel;

namespace ODMSModel.LabourSubGroup
{
    [Validator(typeof(LabourSubGroupViewModelValidator))]
    public class LabourSubGroupViewModel : ModelBase, IExcelValidation<LabourSubGroupViewModel>
    {

        public LabourSubGroupViewModel()
        {
            MultiLanguageName = new MultiLanguageModel();
        }

        [Display(Name = "LabourSubGroup_Display_SubGroupId", ResourceType = typeof(MessageResource))]
        public string SubGroupId { get; set; }

        [Display(Name = "LabourMainGroup_Display_LabourMainGroupId", ResourceType = typeof(MessageResource))]
        public string MainGroupId { get; set; }

        [Display(Name = "LabourSubGroup_Display_Description", ResourceType = typeof(MessageResource))]
        public string Description { get; set; }

        [Display(Name = "LabourSubGroup_Display_LabourSubGroupName", ResourceType = typeof(MessageResource))]
        public string LabourSubGroupName { get; set; }

        public string MultiLanguageContentAsText { get; set; }

        private MultiLanguageModel _multiLangName;

        public MultiLanguageModel MultiLanguageName
        {
            get { return _multiLangName ?? new MultiLanguageModel(); }
            set { _multiLangName = value; }
        }

        public IEnumerable<LabourSubGroupListModel> List { get; set; }

        public Dictionary<int, string> Languages { get; set; }

        public LabourSubGroupViewModel ExcelValidate(LabourSubGroupViewModel model)
        {
            var result =
                model.List.Any(item => String.Equals(item.MainGroupId.Trim(), model.MainGroupId.Trim(), StringComparison.CurrentCultureIgnoreCase) &&
                                       String.Equals(item.SubGroupId.Trim(), model.SubGroupId.Trim(), StringComparison.CurrentCultureIgnoreCase));
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
