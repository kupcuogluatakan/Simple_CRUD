using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.GuaranteeAuthorityGroupIndicatorTransTypes;
using Permission = ODMSCommon.CommonValues.PermissionCodes.GuaranteeAuthorityGroupIndicatorTransTypes;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class GuaranteeAuthorityGroupIndicatorTransTypesController : ControllerBase
    {

        [HttpGet]
        [AuthorizationFilter(Permission.GuaranteeAuthorityGroupIndicatorTransTypesIndex)]
        public ActionResult GuaranteeAuthorityGroupIndicatorTransTypesIndex(int id = 0)
        {
            if (id == 0)
            {
                SetMessage(MessageResource.Error_DB_NoRecordFound, CommonValues.MessageSeverity.Fail);
            }
            
            ViewBag.GroupId = id;
            return PartialView();
        }

        [HttpPost]
        [AuthorizationFilter(Permission.GuaranteeAuthorityGroupIndicatorTransTypesIndex)]
        public ActionResult ListTypesIncluded(int groupId = 0)
        {
            return groupId == 0
                ? Json(new { Data = new List<SelectListItem>() })
                : Json(new { Data = new GuaranteeAuthorityGroupIndicatorTransTypeBL().ListGuaranteeAuthorityGroupIndicatorTransTypesIncluded(UserManager.UserInfo, groupId).Data });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.GuaranteeAuthorityGroupIndicatorTransTypesIndex)]
        public ActionResult ListTypesExcluded(int groupId = 0)
        {
            return groupId == 0
                ? Json(new { Data = new List<SelectListItem>() })
                : Json(
                    new
                    {
                        Data =
                            new GuaranteeAuthorityGroupIndicatorTransTypeBL().ListGuaranteeAuthorityGroupIndicatorTransTypesExcluded(UserManager.UserInfo, groupId).Data
                    });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(Permission.GuaranteeAuthorityGroupIndicatorTransTypesIndex, Permission.GuaranteeAuthorityGroupIndicatorTransTypesSave)]
        public ActionResult Save(GuaranteeAuthorityGroupIndicatorTransTypesModel model)
        {
            if (model.GroupId > 0)
            {
                model.TypeCodeList = model.TypeCodeList != null
                    ? model.TypeCodeList.Distinct().ToList()
                    : null;
                new GuaranteeAuthorityGroupIndicatorTransTypeBL().Save(UserManager.UserInfo, model);
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
