using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.FleetRequestVehicleApprove;
using Perm = ODMSCommon.CommonValues.PermissionCodes.FleetRequestVehicleApprove;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class FleetRequestVehicleApproveController : ControllerBase
    {
        //
        // GET: /FleetRequestVehicleApprove/
        [HttpGet]
        [AuthorizationFilter(Perm.FleetRequestVehicleApproveIndex)]
        public ActionResult FleetRequestVehicleApproveIndex(int id = 0)
        {
            var model = new FleetRequestApproveViewModel();
            if (id == 0)
            {
                model.HideElements = true;
            }
            else
            {
                model = new FleetRequestVehicleApproveBL().GetFleetRequestData(UserManager.UserInfo, id).Model;
                if (string.IsNullOrEmpty(model.Description))
                    model.HideElements = true;
                else
                {
                    ViewBag.RequestStatus = new FleetRequestVehicleBL().GetFleetRequestStatus(model.FleetRequestId).Model;
                }
            }
            return PartialView(model);
        }

        [HttpPost]
        [AuthorizationFilter(Perm.FleetRequestVehicleApproveIndex, Perm.FleetRequestVehicleApproveUpdate)]
        public ActionResult SaveRequests(FleetRequestApproveViewModel model)
        {
            var list = new List<FleetRequestApproveListModel>();

            list.AddRange(model.Requests);

            string errorMessage = string.Empty;
            int errorNo = 0;
            if (list.Any())
                new FleetRequestVehicleApproveBL().SaveRequests(UserManager.UserInfo, list, out errorNo, out errorMessage);
            if (errorNo > 0)
            {
                SetMessage(errorMessage, CommonValues.MessageSeverity.Fail);
            }
            else
            {
                SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
            }

            return Redirect(Request.UrlReferrer.AbsolutePath);
        }
    }
}
