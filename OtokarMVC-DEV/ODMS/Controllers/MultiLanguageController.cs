using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon.Security;
using ODMSModel.ViewModel;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class MultiLanguageController : ControllerBase
    {

        public ActionResult MultiLanguageTextbox(string DefaultText,bool isEnabled)
        {
            List<SelectListItem> languageList = LanguageBL.ListLanguageAsSelectListItem(UserManager.UserInfo).Data;
            List<MultiLanguageContentViewModel> defaultLang = languageList.Select(selectListItem => new MultiLanguageContentViewModel
                {
                    Content = "", LanguageCode = selectListItem.Value, OperationType = ""
                }).ToList();

            string isEnabledStr = string.Empty;
            isEnabledStr = isEnabled ? "1" : "0";
            ViewBag.IsEnabled = isEnabledStr;
            ViewBag.DefaultLanguages = defaultLang;
            ViewBag.DefaultText = DefaultText;
            return View();
        }

        public bool SetIsValid(List<MultiLanguageContentViewModel> finalList, List<MultiLanguageContentViewModel> initialList)
        {
            var finalTurkishContent = (finalList == null || finalList.Count == 0) ? null : (from r in finalList
                                                                     where r.LanguageCode.Trim() == "TR"
                                                                     select r).First();
            if (finalTurkishContent == null) // son listede türkçe içerik yoksa, initialda olmak zorunda.
            {
                var initialTurkishContent = (initialList == null || initialList.Count == 0) ? null : (from r in initialList
                                             where r.LanguageCode.Trim() == "TR"
                                             select r).First();
                if (initialTurkishContent == null || string.IsNullOrEmpty(initialTurkishContent.Content))
                    return false;

            }
            else if (string.IsNullOrEmpty(finalTurkishContent.Content) || finalTurkishContent.OperationType == "D") //finalda türkçe var ama içi boş veya delete ise hata
                return false;
            return true;
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.PermissionCodeTest)]
        public ActionResult SetInitialContent(MultiLanguageModel viewModel)
        {
            const string nonSelectedClass = "btn-default";
            const string selectedClass = "btn-primary";
            string defaultLangText = string.Empty;
            string completeLangHeaderCode = string.Empty;
            string tempCode = string.Empty;
            foreach (var singleLang in viewModel.listOfInitialContent)
            {
                if (singleLang.LanguageCode.Trim() == "TR")
                {
                    defaultLangText = singleLang.Content;
                    tempCode = "<a href='#' class='btn " + selectedClass + " languageBtn language " + singleLang.LanguageCode.ToUpper() + "'>" + singleLang.LanguageCode.ToUpper() + "</a>";
                }
                else
                {
                    tempCode = "<a href='#' class='btn " + nonSelectedClass + " languageBtn language " + singleLang.LanguageCode.ToUpper() + "'>" + singleLang.LanguageCode.ToUpper() + "</a>";
                }
                completeLangHeaderCode += tempCode;
            }
            if (string.IsNullOrEmpty(completeLangHeaderCode))
                completeLangHeaderCode = "<a href='#' class='btn btn-primary languageBtn language TR'>TR</a>";

            SetAllContent(viewModel);
            viewModel.isValid = SetIsValid(null , viewModel.listOfInitialContent);
            TempData["MLModel"] = viewModel;
            return Json(new { retModel = viewModel, headerHTML = completeLangHeaderCode, defaultText = defaultLangText });
        }

        /// <summary>
        /// Allcontent içerisinden addedcontentde geçenleri çıkaran linq expression lazım.
        /// </summary>
        /// <param name="mlContent"></param>
        /// <returns></returns>
        private void SetAllContent(MultiLanguageModel mlContent)
        {
            var all = mlContent.listOfAllContent;
            var initial = mlContent.listOfInitialContent;
            var finalAll = (from mlLast in all
                            let checkItem = initial.FirstOrDefault(v => v.LanguageCode == mlLast.LanguageCode)
                            where checkItem == null
                            select new MultiLanguageContentViewModel {LanguageCode = mlLast.LanguageCode}).ToList();

            //burdan sonrası artık initial doluysa yani ekran update için açıldıysa kısmını kapsıyor.
            //listenin son halindeki her item için dolaşıp, text değişmişmi diye bakmak gerek.

            mlContent.listOfAllContent = finalAll;

        }


        public MultiLanguageModel GetLanguageContentAsString(MultiLanguageModel mlContent)
        {
            if (mlContent.listOfInitialContent == null)
                mlContent.listOfInitialContent = new List<MultiLanguageContentViewModel>();
            List<MultiLanguageContentViewModel> finalList = CheckDifferences(mlContent);

            string returnValue = finalList.Where(singleContent => !String.IsNullOrEmpty(singleContent.Content) || singleContent.OperationType == "D").Aggregate(string.Empty, (current, singleContent) => current + (singleContent.OperationType + "|" + singleContent.LanguageCode + "|" + singleContent.Content + "|"));
            if (returnValue.Length > 0)
                returnValue = returnValue.Substring(0, returnValue.Length - 1);
            mlContent.txtLanguageContentAsString = returnValue;
            mlContent.isValid = SetIsValid(finalList, mlContent.listOfInitialContent);
            return mlContent;
        }

        private List<MultiLanguageContentViewModel> CheckDifferences(MultiLanguageModel mlContent)
        {
            List<MultiLanguageContentViewModel> initial = mlContent.listOfInitialContent;
            List<MultiLanguageContentViewModel> last = mlContent.listOfAddedContent;
            List<MultiLanguageContentViewModel> finalList = new List<MultiLanguageContentViewModel>();

            bool isFound = false;
            bool isSame = false;
            if (initial.Count == 0)
            {
                // ekran açıldığında hiç dil kodu yokmuş, last da ne varsa onu return etmek gerekiyor
                return last;
            }
            //burdan sonrası artık initial doluysa yani ekran update için açıldıysa kısmını kapsıyor.
            //listenin son halindeki her item için dolaşıp, text değişmişmi diye bakmak gerek.
            foreach (MultiLanguageContentViewModel mlLast in last)
            {
                foreach (MultiLanguageContentViewModel mlInitial in initial)
                {
                    if (mlInitial.LanguageCode == mlLast.LanguageCode)
                    {
                        if (mlInitial.Content != mlLast.Content)
                        {
                            if (string.IsNullOrEmpty(mlLast.Content))
                            {
                                finalList.Add(new MultiLanguageContentViewModel(mlLast.LanguageCode, mlLast.Content, "D"));
                                isFound = true;
                            }
                            else
                            {
                                // kayıt var, update yapılması gerekli.
                                finalList.Add(new MultiLanguageContentViewModel(mlLast.LanguageCode, mlLast.Content, "U"));

                                isFound = true;
                            }
                            break;
                        }
                        else
                        {
                            isSame = true;
                        }
                    }
                }
                //item eski listede bulunamadıysa yeni eklenmiş demektir, güncelleme değil insert yapılacaktır.
                if (!isFound && !isSame)
                {
                    if (!string.IsNullOrEmpty(mlLast.Content))
                    {
                        finalList.Add(new MultiLanguageContentViewModel(mlLast.LanguageCode, mlLast.Content, "I"));

                    }
                }
                isFound = false;
                isSame = false;
            }

            return finalList;
        }


        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.PermissionCodeTest)]
        public ActionResult SaveLanguageContent(MultiLanguageModel viewModel, string languageCode,
                                                string languageContent)
        {
            if (viewModel.listOfAddedContent == null)
                viewModel.listOfAddedContent = new List<MultiLanguageContentViewModel>();

            var foundItem = viewModel.listOfAddedContent.FirstOrDefault(v => v.LanguageCode == languageCode);
            if (foundItem != null)
            {
                foundItem.Content = languageContent;
            }
            else
            {
                MultiLanguageContentViewModel newContent = new MultiLanguageContentViewModel
                    {
                        LanguageCode = languageCode,
                        Content = languageContent,
                        OperationType = "I"
                    };
                viewModel.listOfAddedContent.Add(newContent);
            }
            viewModel = GetLanguageContentAsString(viewModel);
            TempData["MLModel"] = viewModel;
            return Json(new { retModel = viewModel, contentString = viewModel.txtLanguageContentAsString });
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.PermissionCodeTest)]
        public ActionResult SavePastContentGetCurrentContent(MultiLanguageModel viewModel, string currentLanguageCode,
                                                             string pastLanguageCode, string pastContent)
        {
            if (pastContent == null)
                pastContent = string.Empty;

            #region savePastContent

            var pastLanguageObject = viewModel.listOfAddedContent.FirstOrDefault(v => v.LanguageCode == pastLanguageCode);
            if (pastLanguageObject != null)
            {
                pastLanguageObject.Content = pastContent;
            }
            else
            {
                var initialLanguageObject = viewModel.listOfInitialContent.FirstOrDefault(v => v.LanguageCode == pastLanguageCode);
                if (initialLanguageObject != null)
                {
                    if (initialLanguageObject.Content != pastContent)
                    {
                        pastLanguageObject = new MultiLanguageContentViewModel
                            {
                                OperationType = !string.IsNullOrEmpty(pastContent) ? "U" : "D",
                                LanguageCode = pastLanguageCode,
                                Content = pastContent
                            };
                    }
                }
                else
                {
                    pastLanguageObject = new MultiLanguageContentViewModel
                        {
                            LanguageCode = pastLanguageCode,
                            Content = pastContent,
                            OperationType = "I"
                        };
                    viewModel.listOfAddedContent.Add(pastLanguageObject);
                }

            }

            #endregion

            #region getCurrentContent

            string selectedLanguageContent = string.Empty;
            if (viewModel.listOfAddedContent == null)
                viewModel.listOfAddedContent = new List<MultiLanguageContentViewModel>();

            var foundItem = viewModel.listOfAddedContent.FirstOrDefault(v => v.LanguageCode == currentLanguageCode);
            if (foundItem != null)
            {
                selectedLanguageContent = foundItem.Content;
            }
            else
            {
                var foundItemInInitial = viewModel.listOfInitialContent.FirstOrDefault(v => v.LanguageCode == currentLanguageCode);
                if (foundItemInInitial != null)
                    selectedLanguageContent = foundItemInInitial.Content;
            }
            viewModel = GetLanguageContentAsString(viewModel);
            TempData["MLModel"] = viewModel;
            return
                Json(
                    new
                        {
                            retModel = viewModel,
                            content = selectedLanguageContent,
                            contentString = viewModel.txtLanguageContentAsString
                        });

            #endregion
        }
    }
}