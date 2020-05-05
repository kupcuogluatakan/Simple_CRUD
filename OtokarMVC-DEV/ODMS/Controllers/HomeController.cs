using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using ODMS.Filters;
using ODMS.ReportingService;
using ODMS.Security;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.PurchaseOrder;
using ODMSModel.ServiceCallSchedule;
using ODMSModel.SparePart;
using ODMSModel.StockCardPriceListModel;
using ODMSModel.Vehicle;
using ODMSModel.VehicleCode;
using ODMSModel.VehicleGroup;
using ODMSModel.VehicleModel;
using ODMSModel.VehicleType;
using ODMSModel.ViewModel;
using ODMS.OtokarService;
using ODMSModel.Announcement;
using ODMSModel.Appointment;
using ODMSModel.CycleCount;
using ODMSModel.PurchaseOrderDetail;
using ODMSModel.StockCard;
using ODMSCommon.Security;
using ODMSModel.Bank;
using ODMSCommon.Logging;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class HomeController : ControllerBase
    {

        public ActionResult Test1()
        {

            //AppErrorsBL.Add("SELAM HATA", "Home", "Test1");
            //var a = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss");
            //var filter = new PurchaseOrderDetailListModel();
            //filter.PurchaseOrderNumber = 1;
            //var total = 0;
            //filter.PageSize = 10;
            //var business = new PurchaseOrderDetailBL();
            //var result = business.ListPurchaseOrderDetails(filter, out total);

            //var filter = new StockCardSearchListModel();
            //filter.CurrentDealerId = ((!UserManager.UserInfo.IsDealer) ? null : (int?)UserManager.UserInfo.GetUserDealerId());
            //filter.StockLocationId = 1;
            //filter.IsHq = !UserManager.UserInfo.IsDealer;
            //filter.PartCodeList = "LR004449,LR048186,LR004420,LR004419,LR029906,LR004461,LR049203,LR004444,LR056258,LR004390,LR058096,LR068979,LR034071,LR009837,LR004405,LR081697,LR006803,LR022164,LR009587";
            //var business = new StockCardBL();
            //business.ListStockCardSearch(filter, out total);

            var filter = new BankFilter();
            filter.Description.Value = "Finans";

            var data = new BankBL();
            var result = data.ListBanks(filter);

            return View();
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.PermissionCodeTest)]
        public ActionResult Index()
        {
            var m = new HomeViewModel();
            return View(m);
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
                            select new MultiLanguageContentViewModel { LanguageCode = mlLast.LanguageCode }).ToList();

            //burdan sonrası artık initial doluysa yani ekran update için açıldıysa kısmını kapsıyor.
            //listenin son halindeki her item için dolaşıp, text değişmişmi diye bakmak gerek.

            mlContent.listOfAllContent = finalAll;

        }
        [AuthorizationFilter]
        public ActionResult About()
        {
            return View();
        }



        /// <summary>
        /// menu session da kayıtlı kalıyor. 
        /// </summary>
        /// <returns></returns>
        [AuthorizationFilter]
        [HttpPost]
        public ActionResult ChangeLanguage(string lang)
        {
            var userbl = new UserBL();
            CultureInfo culInfo = new CultureInfo(lang);
            lang = culInfo.TwoLetterISOLanguageName.ToUpper();
            userbl.UpdateUserLanguage(UserManager.UserInfo.UserId, lang);
            var user = UserManager.UserInfo;
            user.LanguageCode = lang;

            System.Web.HttpContext.Current.Items[CommonValues.UserInfoSessionKey] = user;
            CommonUtility.CreateAuthenticationCookie(user);

            new UserPermissionManager().InitializeUserMenuSessionState();
            return Json("1");
        }

        [AuthorizationFilter]
        public ActionResult Test(HomeViewModel model)
        {
            return RedirectToAction("Index");
        }

        public MultiLanguageModel GetLanguageContentAsString(MultiLanguageModel mlContent)
        {
            if (mlContent.listOfInitialContent == null)
                mlContent.listOfInitialContent = new List<MultiLanguageContentViewModel>();
            List<MultiLanguageContentViewModel> finalList = CheckDifferences(mlContent);

            string returnValue =
                finalList.Where(
                    singleContent => !String.IsNullOrEmpty(singleContent.Content) || singleContent.OperationType == "D")
                         .Aggregate(string.Empty,
                                    (current, singleContent) =>
                                    current +
                                    (singleContent.OperationType + "|" + singleContent.LanguageCode + "|" +
                                     singleContent.Content + "|"));
            if (returnValue.Length > 0)
                returnValue = returnValue.Substring(0, returnValue.Length - 1);
            mlContent.txtLanguageContentAsString = returnValue;
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
        public ActionResult SaveLanguageContent(MultiLanguageModel viewModel, string languageCode, string languageContent)
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
            return Json(new { retModel = viewModel, contentString = viewModel.txtLanguageContentAsString });
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.PermissionCodeTest)]
        public ActionResult SavePastContentGetCurrentContent(MultiLanguageModel viewModel, string currentLanguageCode, string pastLanguageCode, string pastContent)
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
                pastLanguageObject = new MultiLanguageContentViewModel
                {
                    LanguageCode = pastLanguageCode,
                    Content = pastContent,
                    OperationType = "I"
                };
                viewModel.listOfAddedContent.Add(pastLanguageObject);
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
            viewModel = GetLanguageContentAsString(viewModel);
            return Json(new { retModel = viewModel, content = selectedLanguageContent, contentString = viewModel.txtLanguageContentAsString });
            #endregion
        }

    }
}
