using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.PdiGifApproveGroupMembers;
using Permission = ODMSCommon.CommonValues.PermissionCodes.PdiGifApproveGroupMembers;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    public class PdiGifApproveGroupMembersController : ControllerBase
    {

        [HttpGet]
        [AuthorizationFilter(Permission.PdiGifApproveGroupMembersIndex)]
        public ActionResult PdiGifApproveGroupMembersIndex(int id = 0)
        {
            if (id == 0)
            {
                SetMessage(MessageResource.Error_DB_NoRecordFound, CommonValues.MessageSeverity.Fail);
            }

            ViewBag.GroupId = id;
            return PartialView();
        }

        [HttpPost]
        [AuthorizationFilter(Permission.PdiGifApproveGroupMembersIndex)]
        public ActionResult ListUsersIncluded(int groupId = 0)
        {
            return groupId == 0
                ? Json(new { Data = new List<SelectListItem>() })
                : Json(new { Data = new PdiGifApproveGroupMembersBL().ListPdiGifApproveGroupMembersIncluded(groupId).Data });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.PdiGifApproveGroupMembersIndex)]
        public ActionResult ListUsersExcluded(int groupId = 0)
        {
            return groupId == 0
                ? Json(new { Data = new List<SelectListItem>() })
                : Json(
                    new
                    {
                        Data =
                            new PdiGifApproveGroupMembersBL().ListPdiGifApproveGroupMembersExcluded(groupId).Data
                    });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(Permission.PdiGifApproveGroupMembersIndex, Permission.PdiGifApproveGroupMembersSave)]
        public ActionResult Save(PdiGifApproveGroupMembersModel model)
        {
            if (model.GroupId > 0)
            {
                model.UserList = model.UserList != null
                    ? model.UserList.Distinct().ToList()
                    : null;
                new PdiGifApproveGroupMembersBL().Save(UserManager.UserInfo, model);
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
