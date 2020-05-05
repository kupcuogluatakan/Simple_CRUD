using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.GuaranteeAuthorityLimitations;
using Permission = ODMSCommon.CommonValues.PermissionCodes.GuaranteeAuthorityLimitations;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class GuaranteeAuthorityLimitationsController : ControllerBase
    {
        #region GuaranteeAuthorityLimitations Index

        [HttpGet]
        [AuthorizationFilter(Permission.GuaranteeAuthorityLimitationsIndex)]
        public ActionResult GuaranteeAuthorityLimitationsIndex()
        {
            FillComboBoxes();
            return View();
        }


        [HttpPost]
        [AuthorizationFilter(Permission.GuaranteeAuthorityLimitationsIndex)]
        public ActionResult ListGuaranteeAuthorityLimitations([DataSourceRequest]DataSourceRequest request, GuaranteeAuthorityLimitationsListModel viewModel)
        {
            var bus = new GuaranteeAuthorityLimitationsBL();
            var model = new GuaranteeAuthorityLimitationsListModel(request)
            {
                CurrencyCode = viewModel.CurrencyCode,
                ModelKod = viewModel.ModelKod
            };
            var totalCnt = 0;
            var returnValue = bus.ListGuaranteeAuthorityLimitations(UserManager.UserInfo, model, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
        #endregion

        #region GuaranteeAuthorityLimitations Create

        [HttpGet]
        [AuthorizationFilter(Permission.GuaranteeAuthorityLimitationsIndex, Permission.GuaranteeAuthorityLimitationsCreate)]
        public ActionResult GuaranteeAuthorityLimitationsCreate()
        {
            FillComboBoxes();
            return View();
        }

        [HttpPost]
        [AuthorizationFilter(Permission.GuaranteeAuthorityLimitationsIndex, Permission.GuaranteeAuthorityLimitationsCreate)]
        [ValidateAntiForgeryToken]
        public ActionResult GuaranteeAuthorityLimitationsCreate(GuaranteeAuthorityLimitationsViewModel model)
        {

            if (ModelState.IsValid == false)
            {
                FillComboBoxes();
                return View(model);
            }
            var bus = new GuaranteeAuthorityLimitationsBL();
            model.CommandType = CommonValues.DMLType.Insert;
            bus.DMLGuaranteeAuthorityLimitations(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            ModelState.Clear();
            return RedirectToAction("GuaranteeAuthorityLimitationsCreate");
        }
        #endregion

        #region GuaranteeAuthorityLimitations Update

        [HttpGet]
        [AuthorizationFilter(Permission.GuaranteeAuthorityLimitationsIndex, Permission.GuaranteeAuthorityLimitationsUpdate)]
        public ActionResult GuaranteeAuthorityLimitationsUpdate(string currencyCode, string modelKod)
        {
            return View(new GuaranteeAuthorityLimitationsBL().GetGuaranteeAuthorityLimitations(UserManager.UserInfo, currencyCode, modelKod).Model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.GuaranteeAuthorityLimitationsIndex, Permission.GuaranteeAuthorityLimitationsUpdate)]
        [ValidateAntiForgeryToken]
        public ActionResult GuaranteeAuthorityLimitationsUpdate(GuaranteeAuthorityLimitationsViewModel model)
        {
            if (ModelState.IsValid == false)
                return View(model);
            var bus = new GuaranteeAuthorityLimitationsBL();
            model.CommandType = CommonValues.DMLType.Update;
            bus.DMLGuaranteeAuthorityLimitations(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            ModelState.Clear();
            return RedirectToAction("GuaranteeAuthorityLimitationsUpdate",
                new { currencyCode = model.CurrencyCode, modelKod = model.ModelKod });
        }

        #endregion

        #region GuaranteeAuthorityLimitations Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(Permission.GuaranteeAuthorityLimitationsIndex, Permission.GuaranteeAuthorityLimitationsDelete)]
        public ActionResult GuaranteeAuthorityLimitationsDelete(string currencyCode, string modelKod)
        {
            if (string.IsNullOrEmpty(currencyCode) || string.IsNullOrEmpty(modelKod))
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Error_DB_NoRecordFound);
            }
            var bus = new GuaranteeAuthorityLimitationsBL();
            var model = new GuaranteeAuthorityLimitationsViewModel
            {
                CurrencyCode = currencyCode,
                ModelKod = modelKod,
                CommandType = CommonValues.DMLType.Delete
            };
            bus.DMLGuaranteeAuthorityLimitations(UserManager.UserInfo, model);
            ModelState.Clear();
            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }
        #endregion

        private void FillComboBoxes()
        {
            ViewBag.CurrencyList = CommonBL.ListCurrencies(UserManager.UserInfo).Data;
            ViewBag.ModelKodList = VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo).Data;
        }


    }
}
