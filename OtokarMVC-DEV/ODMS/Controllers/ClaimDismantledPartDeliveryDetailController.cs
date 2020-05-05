using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSModel.ClaimDismantledPartDeliveryDetails;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class ClaimDismantledPartDeliveryDetailController : ControllerBase
    {

        private readonly ClaimDismantledPartDeliveryDetailBL _service = new ClaimDismantledPartDeliveryDetailBL();

        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimDismantledPartDelivery.ClaimDismantledPartDeliveryIndex)]
        public ActionResult ClaimDismantledPartDeliveryDetailIndex(int id)
        {
            var model = new ClaimDismantledPartDeliveryDetailListModel() { ClaimWayBillId = id };

            model.IsExists = _service.Exists(UserManager.UserInfo, model).Model.IsSelected;

            return PartialView(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.ClaimDismantledPartDelivery.ClaimDismantledPartDeliveryIndex)]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request, ClaimDismantledPartDeliveryDetailListModel model)
        {
            int totalCnt;

            var referenceModel = new ClaimDismantledPartDeliveryDetailListModel(request) { ClaimWayBillId = model.ClaimWayBillId };

            return Json(_service.List(UserManager.UserInfo, referenceModel, out totalCnt).Data.ToDataSourceResult(request));
        }

        [HttpPost]
        public JsonResult SetClaimWayBill(int claimDismantledPartId, int claimWayBillId)
        {
            _service.SetClaimWayBill(UserManager.UserInfo, claimDismantledPartId, claimWayBillId);

            return Json(null);
        }
    }
}
