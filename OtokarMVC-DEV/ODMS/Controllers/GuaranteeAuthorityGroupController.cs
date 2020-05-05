using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.GuaranteeAuthorityGroup;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class GuaranteeAuthorityGroupController : ControllerBase
    {
        #region GuaranteeAuthorityGroup Index

        [AuthorizationFilter(CommonValues.PermissionCodes.GuaranteeAuthorityGroup.GuaranteeAuthorityGroupIndex)]
        [HttpGet]
        public ActionResult GuaranteeAuthorityGroupIndex()
        {
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.GuaranteeAuthorityGroup.GuaranteeAuthorityGroupIndex, CommonValues.PermissionCodes.GuaranteeAuthorityGroup.GuaranteeAuthorityGroupDetails)]
        public ActionResult ListGuaranteeAuthorityGroup([DataSourceRequest] DataSourceRequest request, GuaranteeAuthorityGroupListModel model)
        {
            var bo = new GuaranteeAuthorityGroupBL();
            var v = new GuaranteeAuthorityGroupListModel(request)
            {
                GroupName = model.GroupName,
                MailList = model.MailList,
                SearchIsActive = model.SearchIsActive
            };

            var totalCnt = 0;
            var returnValue = bo.ListGuaranteeAuthorityGroups(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region GuaranteeAuthorityGroup Create

        [AuthorizationFilter(CommonValues.PermissionCodes.GuaranteeAuthorityGroup.GuaranteeAuthorityGroupIndex, CommonValues.PermissionCodes.GuaranteeAuthorityGroup.GuaranteeAuthorityGroupCreate)]
        public ActionResult GuaranteeAuthorityGroupCreate()
        {
            GuaranteeAuthorityGroupViewModel model = new GuaranteeAuthorityGroupViewModel();
            model.IsActive = true;
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.GuaranteeAuthorityGroup.GuaranteeAuthorityGroupIndex, CommonValues.PermissionCodes.GuaranteeAuthorityGroup.GuaranteeAuthorityGroupCreate)]
        [HttpPost]
        public ActionResult GuaranteeAuthorityGroupCreate(GuaranteeAuthorityGroupViewModel viewModel/*, ICollection<bool> WeekDays, ICollection<TeaBreakModel> TeaBreaks*/)
        {
            var bo = new GuaranteeAuthorityGroupBL();

            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Insert;

                bo.DMLGuaranteeAuthorityGroup(UserManager.UserInfo, viewModel);

                CheckErrorForMessage(viewModel, true);

                ModelState.Clear();

                return View();

            }
            return View(viewModel);
        }

        #endregion

        #region GuaranteeAuthorityGroup Update

        [AuthorizationFilter(CommonValues.PermissionCodes.GuaranteeAuthorityGroup.GuaranteeAuthorityGroupIndex, CommonValues.PermissionCodes.GuaranteeAuthorityGroup.GuaranteeAuthorityGroupUpdate)]
        [HttpGet]
        public ActionResult GuaranteeAuthorityGroupUpdate(int id = 0)
        {
            return View(new GuaranteeAuthorityGroupBL().GetGuaranteeAuthorityGroup(UserManager.UserInfo, id).Model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.GuaranteeAuthorityGroup.GuaranteeAuthorityGroupIndex, CommonValues.PermissionCodes.GuaranteeAuthorityGroup.GuaranteeAuthorityGroupUpdate)]
        [HttpPost]
        public ActionResult GuaranteeAuthorityGroupUpdate(GuaranteeAuthorityGroupViewModel viewModel)
        {
            var bo = new GuaranteeAuthorityGroupBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Update;
                bo.DMLGuaranteeAuthorityGroup(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }
            return View(viewModel);
        }

        #endregion

        #region GuaranteeAuthorityGroup Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.GuaranteeAuthorityGroup.GuaranteeAuthorityGroupIndex, CommonValues.PermissionCodes.GuaranteeAuthorityGroup.GuaranteeAuthorityGroupDelete)]
        public ActionResult DeleteGuaranteeAuthorityGroup(int id)
        {
            var viewModel = new GuaranteeAuthorityGroupViewModel() { GroupId = id };
            var bo = new GuaranteeAuthorityGroupBL();
            viewModel.CommandType = CommonValues.DMLType.Delete;
            bo.DMLGuaranteeAuthorityGroup(UserManager.UserInfo, viewModel);
            //var deleteResult = CheckErrorForMessage(viewModel, true);
            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
        }
        #endregion

        #region GuaranteeAuthorityGroup Details

        [AuthorizationFilter(CommonValues.PermissionCodes.GuaranteeAuthorityGroup.GuaranteeAuthorityGroupIndex, CommonValues.PermissionCodes.GuaranteeAuthorityGroup.GuaranteeAuthorityGroupDetails)]
        [HttpGet]
        public ActionResult GuaranteeAuthorityGroupDetails(int id = 0)
        {
            return View(new GuaranteeAuthorityGroupBL().GetGuaranteeAuthorityGroup(UserManager.UserInfo, id).Model);
        }

        #endregion
    }
}
