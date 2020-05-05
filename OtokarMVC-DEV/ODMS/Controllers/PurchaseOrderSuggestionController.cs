using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.PurchaseOrderSuggestion;
using System.Web.Mvc;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class PurchaseOrderSuggestionController : ControllerBase
    {
        private void SetDefaults()
        {
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;

            ViewBag.ActiveList = CommonBL.ListStatusBool().Data;

            ViewBag.AutoList = CommonBL.ListYesNo().Data;
        }

        #region PurchaseOrderSuggestion Index

        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderSuggestion.PurchaseOrderSuggestionIndex)]
        [HttpGet]
        public ActionResult PurchaseOrderSuggestionIndex()
        {
            SetDefaults();
            PurchaseOrderSuggestionListModel model = new PurchaseOrderSuggestionListModel();
            if (UserManager.UserInfo.GetUserDealerId() != 0)
                model.IdDealer = UserManager.UserInfo.GetUserDealerId();

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderSuggestion.PurchaseOrderSuggestionIndex, CommonValues.PermissionCodes.PurchaseOrderSuggestion.PurchaseOrderSuggestionIndex)]
        public ActionResult ListPurchaseOrderSuggestion([DataSourceRequest] DataSourceRequest request, PurchaseOrderSuggestionListModel model)
        {
            //SetDefaults();

            var posBo = new PurchaseOrderSuggestionBL();

            var v = new PurchaseOrderSuggestionListModel(request)
            {
                IsAuto = model.IsAuto,
                IsActive = model.IsActive,
                IdDealer = model.IdDealer,
                PlanDateStart = model.PlanDateStart,
                PlanDateEnd = model.PlanDateEnd,
                IsPoCreate = model.IsPoCreate
            };

            var totalCnt = 0;
            var returnValue = posBo.ListPurchaseOrderSuggestion(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PurchaseOrderSuggestion.PurchaseOrderSuggestionIndex, CommonValues.PermissionCodes.PurchaseOrderSuggestion.PurchaseOrderSuggestionIndex)]
        public ActionResult CreateOrderSuggest()
        {
            var purchaseOrderSuggest = new PurchaseOrderSuggestionBL();
            var model = new PurchaseOrderSuggestionViewModel();


            purchaseOrderSuggest.PurchaseOrderSuggest(UserManager.UserInfo, model);

            return model.ErrorNo == 0 ?
                GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success) :
                GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        #endregion
    }
}