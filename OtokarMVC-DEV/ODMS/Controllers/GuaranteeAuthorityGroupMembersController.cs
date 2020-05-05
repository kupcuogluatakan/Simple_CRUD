using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.GuaranteeAuthorityGroupMembers;
using Permission = ODMSCommon.CommonValues.PermissionCodes.GuaranteeAuthorityGroupMembers;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class GuaranteeAuthorityGroupMembersController : ControllerBase
    {

        [HttpGet]
        [AuthorizationFilter(Permission.GuaranteeAuthorityGroupMembersIndex)]
        public ActionResult GuaranteeAuthorityGroupMembersIndex(int id = 0)
        {
            if (id == 0)
            {
                SetMessage(MessageResource.Error_DB_NoRecordFound, CommonValues.MessageSeverity.Fail);
            }
            
            ViewBag.GroupId = id;
            return PartialView();
        }

        [HttpPost]
        [AuthorizationFilter(Permission.GuaranteeAuthorityGroupMembersIndex)]
        public ActionResult ListUsersIncluded(int groupId = 0)
        {
            return groupId == 0
                ? Json(new { Data = new List<SelectListItem>() })
                : Json(new { Data = new GuaranteeAuthorityGroupMemberBL().ListGuaranteeAuthorityGroupMembersIncluded(groupId).Data });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.GuaranteeAuthorityGroupMembersIndex)]
        public ActionResult ListUsersExcluded(int groupId = 0)
        {
            return groupId == 0
                ? Json(new { Data = new List<SelectListItem>() })
                : Json(
                    new
                    {
                        Data =
                            new GuaranteeAuthorityGroupMemberBL().ListGuaranteeAuthorityGroupMembersExcluded(groupId).Data
                    });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(Permission.GuaranteeAuthorityGroupMembersIndex, Permission.GuaranteeAuthorityGroupMembersSave)]
        public ActionResult Save(GuaranteeAuthorityGroupMembersModel model)
        {
            if (model.GroupId > 0)
            {
                model.UserList = model.UserList != null
                    ? model.UserList.Distinct().ToList()
                    : null;
                new GuaranteeAuthorityGroupMemberBL().Save(UserManager.UserInfo, model);
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
