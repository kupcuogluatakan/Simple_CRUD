using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.CountryVatRatio;
using Permission = ODMSCommon.CommonValues.PermissionCodes.CountryVatRatio;
namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class CountryVatRatioController : ControllerBase
    {
        #region CountryVatRatio Index
        [HttpGet]
        [AuthorizationFilter(Permission.CountryVatRatioIndex)]
        public ActionResult CountryVatRatioIndex()
        {
            FillComboBoxes();
            return View();
        }
        [HttpPost]
        [AuthorizationFilter(Permission.CountryVatRatioIndex)]
        public ActionResult ListCountryVatRatios([DataSourceRequest]DataSourceRequest request, CountryVatRatioListModel viewModel)
        {
            var bus = new CountryVatRatioBL();
            var model = new CountryVatRatioListModel(request)
            {
                CountryId = viewModel.CountryId,
                LabourVatRatio = viewModel.LabourVatRatio,
                PartVatRatio = viewModel.PartVatRatio
            };
            var totalCnt = 0;
            var returnValue = bus.ListCountryVatRatios(UserManager.UserInfo, model, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
        #endregion

        #region CountryVatRatio Create
        [HttpGet]
        [AuthorizationFilter(Permission.CountryVatRatioIndex, Permission.CountryVatRatioCreate)]
        public ActionResult CountryVatRatioCreate()
        {
            FillComboBoxes();
            return View();
        }
        [HttpPost]
        [AuthorizationFilter(Permission.CountryVatRatioIndex, Permission.CountryVatRatioCreate)]
        [ValidateAntiForgeryToken]
        public ActionResult CountryVatRatioCreate(CountryVatRatioViewModel model)
        {
            var bus = new CountryVatRatioBL();

            FillComboBoxes();
            if (ModelState.IsValid)
            {
                model.CommandType = CommonValues.DMLType.Insert;
                bus.DMLCountryVatRatio(UserManager.UserInfo, model);

                if (model.ErrorNo != 0)
                    SetMessage(model.ErrorMessage, CommonValues.MessageSeverity.Fail);
                else
                {
                    SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
                    CountryVatRatioViewModel newModel = new CountryVatRatioViewModel();
                    return View(newModel);
                }
            }
            return View(model);
        }
        #endregion

        #region CountryVatRatio Update
        [HttpGet]
        [AuthorizationFilter(Permission.CountryVatRatioIndex, Permission.CountryVatRatioUpdate)]
        public ActionResult CountryVatRatioUpdate(int? countryId)
        {
            var bo = new CountryVatRatioBL();
            FillComboBoxes();

            CountryVatRatioViewModel model = bo.GetCountryVatRatio(countryId.GetValue<int>()).Model;

            return View(model);
        }
        [HttpPost]
        [AuthorizationFilter(Permission.CountryVatRatioIndex, Permission.CountryVatRatioUpdate)]
        [ValidateAntiForgeryToken]
        public ActionResult CountryVatRatioUpdate(CountryVatRatioViewModel model)
        {
            var bo = new CountryVatRatioBL();
            FillComboBoxes();
            if (ModelState.IsValid)
            {
                model.CommandType = CommonValues.DMLType.Update;
                bo.DMLCountryVatRatio(UserManager.UserInfo, model);
                if (model.ErrorNo != 0)
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
                else
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            }
            return View(model);
        }
        #endregion

        #region CountryVatRatio Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(Permission.CountryVatRatioIndex, Permission.CountryVatRatioDelete)]
        public ActionResult CountryVatRatioDelete(CountryVatRatioViewModel model)
        {
            if (model.CountryId == 0 || model.LabourVatRatio == 0 || model.PartVatRatio == 0)
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Error_DB_NoRecordFound);
            }
            var bus = new CountryVatRatioBL();
            model.CommandType = CommonValues.DMLType.Delete;
            bus.DMLCountryVatRatio(UserManager.UserInfo, model);

            ModelState.Clear();
            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }
        #endregion

        #region Private Methods

        private void FillComboBoxes()
        {
            ViewBag.CountryList = CommonBL.ListCountries(UserManager.UserInfo).Data;
            ViewBag.VatRatioList = CommonBL.ListVatRatio().Data;

        }

        #endregion

        public ActionResult CountryVatRatioList(int id, int dealerId = 0, int countryId = 0)
        {
            ViewBag.CountryId = countryId == 0 ? DealerBL.GetDealerCountryId(UserManager.UserInfo.GetUserDealerId()).Model : countryId;
            ViewBag.PartId = id;
            ViewBag.DealerId = dealerId == 0 ? UserManager.UserInfo.GetUserDealerId() : dealerId;
            var countryList = new CountryVatRatioBL().GetVatRatioCountries(UserManager.UserInfo).Data;
            ViewBag.CountryList = countryList;

            return PartialView();
        }

        public JsonResult GetVatRatio(int partId, int countryId)
        {
            if (countryId == 0)
            {
                return Json(new { VatRatio = MessageResource.Error_SparePart_VatRatioUndefined });
            }
            var vatRatio = new CountryVatRatioBL().GetVatRatioByPartAndCountry(partId, countryId).Model;
            return vatRatio == 0 ? Json(new { VatRatio = MessageResource.Error_SparePart_VatRatioUndefined }) : Json(new { VatRatio = vatRatio });
        }
    }
}
