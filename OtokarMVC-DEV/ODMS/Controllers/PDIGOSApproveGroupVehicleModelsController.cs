using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.PDIGOSApproveGroupVehicleModels;
using Permission = ODMSCommon.CommonValues.PermissionCodes.PDIGOSApproveGroupVehicleModels;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class PDIGOSApproveGroupVehicleModelsController : ControllerBase
    {

        [HttpGet]
        [AuthorizationFilter(Permission.PDIGOSApproveGroupVehicleModelsIndex)]
        public ActionResult PDIGOSApproveGroupVehicleModelsIndex(int id = 0)
        {
            if (id == 0)
            {
                SetMessage(MessageResource.Error_DB_NoRecordFound, CommonValues.MessageSeverity.Fail);
            }

            ViewBag.GroupId = id;
            return PartialView();
        }

        [HttpPost]
        [AuthorizationFilter(Permission.PDIGOSApproveGroupVehicleModelsIndex)]
        public ActionResult ListModelsIncluded(int groupId = 0)
        {
            return groupId == 0
                ? Json(new { Data = new List<SelectListItem>() })
                : Json(new { Data = new PDIGOSApproveGroupVehicleModelsBL().ListPDIGOSApproveGroupVehicleModelsIncluded(groupId).Data });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.PDIGOSApproveGroupVehicleModelsIndex)]
        public ActionResult ListModelsExcluded(int groupId = 0)
        {
            return groupId == 0
                ? Json(new { Data = new List<SelectListItem>() })
                : Json(
                    new
                    {
                        Data =
                            new PDIGOSApproveGroupVehicleModelsBL().ListPDIGOSApproveGroupVehicleModelsExcluded(groupId).Data
                    });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(Permission.PDIGOSApproveGroupVehicleModelsIndex, Permission.PDIGOSApproveGroupVehicleModelsSave)]
        public ActionResult Save(PDIGOSApproveGroupVehicleModelsModel model)
        {
            if (model.GroupId > 0)
            {
                model.ModelList = model.ModelList != null
                    ? model.ModelList.Distinct().ToList()
                    : null;
                new PDIGOSApproveGroupVehicleModelsBL().Save(UserManager.UserInfo, model);
                if (model.ErrorNo > 0)
                {
                    return Json(new { Result = false, Message = model.ErrorMessage });
                }
                return Json(new { Result = true, Message = MessageResource.Global_Display_Success });
            }
            return Json(new { Result = false, Message = MessageResource.Error_DB_NoRecordFound });
        }

    }
}
