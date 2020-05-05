using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.LabourCountryVatRatio;
using Permission = ODMSCommon.CommonValues.PermissionCodes.LabourCountryVatRatio;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class LabourCountryVatRatioController : ControllerBase
    {
        #region LabourCountryVatRatio Index

        [HttpGet]
        [AuthorizationFilter(Permission.LabourCountryVatRatioIndex)]
        public ActionResult LabourCountryVatRatioIndex()
        {
            FillComboBoxes();
            return View();
        }

        [HttpPost]
        [AuthorizationFilter(Permission.LabourCountryVatRatioIndex)]
        public ActionResult ListLabourSubCategories(int mainCategoryId)
        {
            if (mainCategoryId == 0) return Json(new { Data = "" });
            return Json(LabourBL.ListSubGrpAsSelectList(UserManager.UserInfo,mainCategoryId).Data);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.LabourCountryVatRatioIndex)]
        public ActionResult ListLabours(int subCategoryId)
        {
            if (subCategoryId == 0) return Json(new { Data = "" });
            return Json(new LabourCountryVatRatioBL().ListLaboursBySubGroup(UserManager.UserInfo, subCategoryId).Data);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.LabourCountryVatRatioIndex)]
        public ActionResult ListLabourCountryVatRatios([DataSourceRequest]DataSourceRequest request, LabourCountryVatRatioListModel viewModel)
        {
            var bus = new LabourCountryVatRatioBL();

            var model = new LabourCountryVatRatioListModel(request)
            {
                CountryId = viewModel.CountryId,
                LabourId = viewModel.LabourId,
                VatRatio = viewModel.VatRatio,
                SearchIsActive = viewModel.SearchIsActive
            };
            var totalCnt = 0;
            var returnValue = bus.ListLabourCountryVatRatios(UserManager.UserInfo, model, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
        #endregion

        #region LabourCountryVatRatio Create
        [HttpGet]
        [AuthorizationFilter(Permission.LabourCountryVatRatioIndex, Permission.LabourCountryVatRatioCreate)]
        public ActionResult LabourCountryVatRatioCreate()
        {
            ViewBag.LabourMainCategoryList = LabourBL.ListMainGrpAsSelectList(UserManager.UserInfo);
            ViewBag.CountryList = new CountryVatRatioBL().GetVatRatioCountries(UserManager.UserInfo).Data;
            ViewBag.VatRatioList = CommonBL.ListVatRatio().Data;
            LabourCountryVatRatioViewModel model = new LabourCountryVatRatioViewModel();
            model.IsActive = true;
            return View(model);
        }
        [HttpPost]
        [AuthorizationFilter(Permission.LabourCountryVatRatioIndex, Permission.LabourCountryVatRatioCreate)]
        [ValidateAntiForgeryToken]
        public ActionResult LabourCountryVatRatioCreate(LabourCountryVatRatioViewModel model)
        {
            ViewBag.CountryList = new CountryVatRatioBL().GetVatRatioCountries(UserManager.UserInfo).Data;
            ViewBag.VatRatioList = CommonBL.ListVatRatio().Data;

            if (ModelState.IsValid == false)
                return View(model);

            var bus = new LabourCountryVatRatioBL();
            model.CommandType = CommonValues.DMLType.Insert;
            bus.DMLLabourCountryVatRatio(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            ModelState.Clear();

            LabourCountryVatRatioViewModel newModel = new LabourCountryVatRatioViewModel();
            newModel.IsActive = true;
            return View(newModel);
        }
        #endregion

        #region Labour Country VatRatio Update

        [HttpGet]
        [AuthorizationFilter(Permission.LabourCountryVatRatioIndex, Permission.LabourCountryVatRatioUpdate)]
        public ActionResult LabourCountryVatRatioUpdate(int LabourId, int CountryId)
        {
            ViewBag.VatRatioList = CommonBL.ListVatRatio().Data;
            var model = new LabourCountryVatRatioBL().GetLabourCountryVatRatio(UserManager.UserInfo, LabourId, CountryId).Model;
            return View(model);
        }
        [HttpPost]
        [AuthorizationFilter(Permission.LabourCountryVatRatioIndex, Permission.LabourCountryVatRatioUpdate)]
        [ValidateAntiForgeryToken]
        public ActionResult LabourCountryVatRatioUpdate(LabourCountryVatRatioViewModel model)
        {
            ViewBag.CountryList = new CountryVatRatioBL().GetVatRatioCountries(UserManager.UserInfo).Data;
            ViewBag.VatRatioList = CommonBL.ListVatRatio().Data;
            if (ModelState.IsValid == false)
                return View(model);
            var bus = new LabourCountryVatRatioBL();
            model.CommandType = CommonValues.DMLType.Update;
            bus.DMLLabourCountryVatRatio(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            ModelState.Clear();
            return View(model);
        }
        #endregion

        #region Details
        [HttpGet]
        [AuthorizationFilter(Permission.LabourCountryVatRatioIndex, Permission.LabourCountryVatRatioDetails)]
        public ActionResult LabourCountryVatRatioDetails(int LabourId, int CountryId)
        {
            var model = new LabourCountryVatRatioBL().GetLabourCountryVatRatio(UserManager.UserInfo, LabourId, CountryId).Model;
            return View(model);
        }
        #endregion


        #region LabourCountryVatRatio Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(Permission.LabourCountryVatRatioIndex, Permission.LabourCountryVatRatioDelete)]
        public ActionResult LabourCountryVatRatioDelete(int labourId, int countryId)
        {
            var bus = new LabourCountryVatRatioBL();
            LabourCountryVatRatioViewModel model = new LabourCountryVatRatioViewModel()
            {
                CommandType = CommonValues.DMLType.Delete,
                LabourId = labourId,
                CountryId = countryId
            };

            bus.DMLLabourCountryVatRatio(UserManager.UserInfo, model);

            ModelState.Clear();
            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            else
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }
        #endregion

        #region Private Methods

        private void FillComboBoxes()
        {
            ViewBag.VatRatioList = CommonBL.ListVatRatio().Data;
            ViewBag.CountryList = new CountryVatRatioBL().GetVatRatioCountries(UserManager.UserInfo).Data;
            ViewBag.LabourList = new LabourDurationBL().GetLabourList(UserManager.UserInfo).Data;
        }
        #endregion
    }
}
