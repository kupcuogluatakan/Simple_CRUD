using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.PdiGifApproveGroup;
using Perm = ODMSCommon.CommonValues.PermissionCodes.PdiGifApproveGroup;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class PdiGifApproveGroupController : ControllerBase
    {

        #region PdiGifApproveGroup Index

        [AuthorizationFilter(Perm.PdiGifApproveGroupIndex)]
        [HttpGet]
        public ActionResult PdiGifApproveGroupIndex()
        {
            return View();
        }

        [AuthorizationFilter()]
        public ActionResult ListPdiGifApproveGroup([DataSourceRequest] DataSourceRequest request, PdiGifApproveGroupListModel model)
        {
            var bo = new PdiGifApproveGroupBL();
            var v = new PdiGifApproveGroupListModel(request)
            {
                GroupName = model.GroupName,
                MailList = model.MailList,
                SearchIsActive = model.SearchIsActive
            };
            var totalCnt = 0;
            var returnValue = bo.ListPdiGifApproveGroups(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region PdiGifApproveGroup Create

        [AuthorizationFilter(Perm.PdiGifApproveGroupIndex, Perm.PdiGifApproveGroupCreate)]
        public ActionResult PdiGifApproveGroupCreate()
        {
            var model = new PdiGifApproveGroupViewModel { IsActive = true };
            return View(model);
        }

        [AuthorizationFilter(Perm.PdiGifApproveGroupIndex, Perm.PdiGifApproveGroupCreate)]
        [HttpPost]
        public ActionResult PdiGifApproveGroupCreate(PdiGifApproveGroupViewModel viewModel/*, ICollection<bool> WeekDays, ICollection<TeaBreakModel> TeaBreaks*/)
        {
            var bo = new PdiGifApproveGroupBL();

            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Insert;

                bo.DMLPdiGifApproveGroup(UserManager.UserInfo, viewModel);

                CheckErrorForMessage(viewModel, true);

                ModelState.Clear();

                return View();

            }
            return View(viewModel);
        }

        #endregion

        #region PdiGifApproveGroup Update

        [AuthorizationFilter(Perm.PdiGifApproveGroupIndex, Perm.PdiGifApproveGroupUpdate)]
        [HttpGet]
        public ActionResult PdiGifApproveGroupUpdate(int id = 0)
        {
            return View(new PdiGifApproveGroupBL().GetPdiGifApproveGroup(UserManager.UserInfo, id).Model);
        }

        [AuthorizationFilter(Perm.PdiGifApproveGroupIndex, Perm.PdiGifApproveGroupUpdate)]
        [HttpPost]
        public ActionResult PdiGifApproveGroupUpdate(PdiGifApproveGroupViewModel viewModel)
        {
            var bo = new PdiGifApproveGroupBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Update;
                bo.DMLPdiGifApproveGroup(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }
            return View(viewModel);
        }

        #endregion

        #region PdiGifApproveGroup Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(Perm.PdiGifApproveGroupIndex, Perm.PdiGifApproveGroupDelete)]
        public ActionResult DeletePdiGifApproveGroup(int id)
        {
            var viewModel = new PdiGifApproveGroupViewModel() { GroupId = id };
            var bo = new PdiGifApproveGroupBL();
            viewModel.CommandType = CommonValues.DMLType.Delete;
            bo.DMLPdiGifApproveGroup(UserManager.UserInfo, viewModel);
            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
        }

        #endregion

        #region PdiGifApproveGroup Details

        [AuthorizationFilter(Perm.PdiGifApproveGroupIndex, Perm.PdiGifApproveGroupDetails)]
        [HttpGet]
        public ActionResult PdiGifApproveGroupDetails(int id = 0)
        {
            return View(new PdiGifApproveGroupBL().GetPdiGifApproveGroup(UserManager.UserInfo, id).Model);
        }

        #endregion
    }
}
