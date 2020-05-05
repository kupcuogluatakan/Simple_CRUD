using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.Currency;
using ODMSModel.ListModel;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class CurrencyController : ControllerBase
    {
        private void SetDefaults()
        {
            //Status List
            List<SelectListItem> statusList = CommonBL.ListStatus().Data;
            ViewBag.StatusList = statusList;
        }

        #region Currency Index

        [AuthorizationFilter(CommonValues.PermissionCodes.Currency.CurrencyIndex)]
        [HttpGet]
        public ActionResult CurrencyIndex()
        {
            SetDefaults();
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Currency.CurrencyIndex, CommonValues.PermissionCodes.Currency.CurrencyDetails)]
        public ActionResult ListCurrency([DataSourceRequest] DataSourceRequest request, CurrencyListModel model)
        {
            var currencyBo = new CurrencyBL();
            var v = new CurrencyListModel(request);
            var totalCnt = 0;
            v.CurrencyName = model.CurrencyName;
            v.IsActive = model.IsActive;
            var returnValue = currencyBo.ListCurrencys(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region Currency Create
        [AuthorizationFilter(CommonValues.PermissionCodes.Currency.CurrencyIndex, CommonValues.PermissionCodes.Currency.CurrencyCreate)]
        public ActionResult CurrencyCreate()
        {
            SetDefaults();
            var model = new CurrencyIndexViewModel();
            model.IsActive = true;
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Currency.CurrencyIndex,
            CommonValues.PermissionCodes.Currency.CurrencyCreate)]
        [HttpPost]
        public ActionResult CurrencyCreate(CurrencyIndexViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var currencyBo = new CurrencyBL();

            if (ModelState.IsValid)
            {
                int totalCount = 0;
                CurrencyListModel listModel = new CurrencyListModel();
                List<CurrencyListModel> list = currencyBo.ListCurrencys(UserManager.UserInfo, listModel, out totalCount).Data;
                var control = (from r in list.AsEnumerable()
                               where r.ListOrder == viewModel.ListOrder
                                     && r.CurrencyCode != viewModel.CurrencyCode
                               select r);
                if (control.Any())
                {
                    SetMessage(MessageResource.Currency_Warning_SameListOrder, CommonValues.MessageSeverity.Fail);
                }
                else
                {
                    viewModel.CommandType = !String.IsNullOrEmpty(viewModel.CurrencyCode)
                                                ? CommonValues.DMLType.Update
                                                : CommonValues.DMLType.Insert;
                    currencyBo.DMLCurrency(UserManager.UserInfo, viewModel);
                    CheckErrorForMessage(viewModel, true);
                    ModelState.Clear();
                    return View(new CurrencyIndexViewModel());
                }
            }
            return View(viewModel);
        }

        #endregion

        #region Currency Update
        [AuthorizationFilter(CommonValues.PermissionCodes.Currency.CurrencyIndex, CommonValues.PermissionCodes.Currency.CurrencyUpdate)]
        [HttpGet]
        public ActionResult CurrencyUpdate(string id)
        {
            SetDefaults();
            var v = new CurrencyIndexViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                var currencyBo = new CurrencyBL();
                v.CurrencyCode = id;
                currencyBo.GetCurrency(UserManager.UserInfo, v);
            }
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Currency.CurrencyIndex, CommonValues.PermissionCodes.Currency.CurrencyUpdate)]
        [HttpPost]
        public ActionResult CurrencyUpdate(CurrencyIndexViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var currencyBo = new CurrencyBL();
            if (ModelState.IsValid)
            {
                int totalCount = 0;
                CurrencyListModel listModel = new CurrencyListModel();
                List<CurrencyListModel> list = currencyBo.ListCurrencys(UserManager.UserInfo, listModel, out totalCount).Data;
                var control = (from r in list.AsEnumerable()
                               where r.ListOrder == viewModel.ListOrder
                                     && r.CurrencyCode != viewModel.CurrencyCode
                               select r);

                if (control.Any())
                {
                    SetMessage(MessageResource.Currency_Warning_SameListOrder, CommonValues.MessageSeverity.Fail);
                }
                else
                {
                    viewModel.CommandType = !String.IsNullOrEmpty(viewModel.CurrencyCode)
                                                ? CommonValues.DMLType.Update
                                                : CommonValues.DMLType.Insert;
                    currencyBo.DMLCurrency(UserManager.UserInfo, viewModel);
                    CheckErrorForMessage(viewModel, true);
                }
            }
            return View(viewModel);
        }

        #endregion

        #region Currency Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.Currency.CurrencyIndex, CommonValues.PermissionCodes.Currency.CurrencyDelete)]
        public ActionResult DeleteCurrency(string currencyCode)
        {
            CurrencyIndexViewModel viewModel = new CurrencyIndexViewModel() { CurrencyCode = currencyCode };
            var currencyBo = new CurrencyBL();
            viewModel.CommandType = !string.IsNullOrEmpty(viewModel.CurrencyCode) ? CommonValues.DMLType.Delete : string.Empty;
            currencyBo.DMLCurrency(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
        }
        #endregion

        #region Currency Details
        [AuthorizationFilter(CommonValues.PermissionCodes.Currency.CurrencyIndex, CommonValues.PermissionCodes.Currency.CurrencyDetails)]
        [HttpGet]
        public ActionResult CurrencyDetails(string id)
        {
            var v = new CurrencyIndexViewModel();
            var currencyBo = new CurrencyBL();

            v.CurrencyCode = id;
            currencyBo.GetCurrency(UserManager.UserInfo, v);

            return View(v);
        }

        #endregion
    }
}