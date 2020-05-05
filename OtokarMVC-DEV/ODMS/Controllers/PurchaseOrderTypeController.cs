using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.PurchaseOrderType;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class PurchaseOrderTypeController : ControllerBase
    {
        private void SetDefaults()
        {
            List<SelectListItem> stockTypeList = CommonBL.ListStockTypes(UserManager.UserInfo).Data;
            ViewBag.StockTypeList = stockTypeList;
        }

        private readonly PurchaseOrderTypeBL _service = new PurchaseOrderTypeBL();

        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderType.PurchaseOrderTypeIndex)]
        public ActionResult PurchaseOrderTypeIndex()
        {
            SetDefaults();
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderType.PurchaseOrderTypeSelect)]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request, PurchaseOrderTypeListModel model)
        {
            int totalCnt;

            var referenceModel = new PurchaseOrderTypeListModel(request) { IsActive = model.IsActive, StockTypeId = model.StockTypeId };

            var list = _service.List(UserManager.UserInfo, referenceModel, out totalCnt).Data;

            return Json(new
            {
                Data = list,
                Total = totalCnt
            });
        }

        #region Create

        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderType.PurchaseOrderTypeCreate)]
        public ActionResult PurchaseOrderTypeCreate()
        {
            SetDefaults();
            var model = new PurchaseOrderTypeViewModel { IsActive = true };
            return PartialView(model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderType.PurchaseOrderTypeCreate)]
        public ActionResult PurchaseOrderTypeCreate(PurchaseOrderTypeViewModel model)
        {
            SetDefaults();
            if (ModelState.IsValid)
            {
                model.CommandType = CommonValues.DMLType.Insert;
                _service.Insert(UserManager.UserInfo, model);

                if (model.ErrorNo == 0)
                {
                    foreach (var item in ModelState.Keys.Where(item => item != "PurchaseOrderTypeId"))
                        ModelState[item].Value = new ValueProviderResult(string.Empty, string.Empty, CultureInfo.CurrentCulture);

                    model.MultiLanguageContentAsText = string.Empty;
                }

                CheckErrorForMessage(model, true);
            }

            return PartialView(model);
        }

        #endregion

        #region Update

        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderType.PurchaseOrderTypeUpdate)]
        public ActionResult PurchaseOrderTypeUpdate(int id)
        {
            SetDefaults();
            return PartialView(_service.Get(UserManager.UserInfo, new PurchaseOrderTypeViewModel() { PurchaseOrderTypeId = id }).Model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderType.PurchaseOrderTypeUpdate)]
        public ActionResult PurchaseOrderTypeUpdate(PurchaseOrderTypeViewModel model)
        {
            SetDefaults();
            if (ModelState.IsValid)
            {
                model.CommandType = CommonValues.DMLType.Update;
                _service.Update(UserManager.UserInfo, model);

                CheckErrorForMessage(model, true);
            }

            return PartialView(model);
        }

        #endregion

        #region Detail

        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderType.PurchaseOrderTypeDetail)]
        public ActionResult PurchaseOrderTypeDetail(int id)
        {
            return PartialView(_service.Get(UserManager.UserInfo, new PurchaseOrderTypeViewModel() { PurchaseOrderTypeId = id }).Model);
        }

        #endregion

        #region Delete

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderType.PurchaseOrderTypeDelete)]
        public ActionResult PurchaseOrderTypeDelete(int id)
        {
            var model = new PurchaseOrderTypeViewModel
            {
                PurchaseOrderTypeId = id,
                CommandType = CommonValues.DMLType.Delete
            };

            _service.Delete(UserManager.UserInfo, model);

            ModelState.Clear();

            return model.ErrorNo == 0 ?
                GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success) :
                GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        #endregion
    }
}
