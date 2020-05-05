using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon.Resources;
using ODMSModel.ListModel;
using ODMSModel.User;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class DealerUserController : ControllerBase
    {
        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.User.DealerUserIndex)]
        public ActionResult DealerUserIndex(int id)
        {
            ViewBag.DealerId = id;
            return PartialView();
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.User.DealerUserIndex,
            ODMSCommon.CommonValues.PermissionCodes.User.DealerUserDetails)]
        public ActionResult ListUsers([DataSourceRequest] DataSourceRequest request, int dealerId)
        {
            int totalCnt;

            var userBo = new UserBL();
            var model = new UserListModel(request)
            {
                DealerId = dealerId,
                IsActive = 1,
                IsTechnician = 1
            };
            var result = userBo.ListDealerUsers(UserManager.UserInfo, model, out totalCnt).Data;

            return Json(new
            {
                Data = result,
                Total = totalCnt
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.User.DealerUserIndex,
            ODMSCommon.CommonValues.PermissionCodes.User.DealerUserDelete)]
        public ActionResult SetUserInactive(int userId)
        {
            var bo = new DealerUserBL();
            var model = new UserIndexViewModel { UserId = userId };
            bo.SetUserInactive(UserManager.UserInfo, model);

            CheckErrorForMessage(model, true);
            ModelState.Clear();

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }
    }
}
