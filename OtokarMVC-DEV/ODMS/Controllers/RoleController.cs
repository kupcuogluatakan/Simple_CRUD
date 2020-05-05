using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.Role;
using ODMSModel.Shared;
using ODMSModel.ViewModel;
using ODMSBusiness;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class RoleController : ControllerBase
    {
        private void SetDefaults()
        {
            ViewBag.YesNoList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.YesNo).Data;
            ViewBag.StatusList = CommonBL.ListStatus().Data;
        }

        #region Role Index

        [AuthorizationFilter(CommonValues.PermissionCodes.Role.RoleTypeIndex)]
        [HttpGet]
        public ActionResult RoleIndex()
        {
            SetDefaults();
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Role.RoleTypeIndex, CommonValues.PermissionCodes.Role.RoleTypeDetails)]
        public ActionResult ListRole([DataSourceRequest]DataSourceRequest request, RoleListModel model)
        {
            var roleBo = new RoleBL();
            var v = new RoleListModel(request);
            var totalCnt = 0;

            v.AdminDesc = model.AdminDesc;
            v.RoleTypeName = model.RoleTypeName;
            v.IsSystemRole = 1;
            v.IsActive = model.IsActive;

            var returnValue = roleBo.ListRoles(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region Role Create

        [AuthorizationFilter(CommonValues.PermissionCodes.Role.RoleTypeIndex, CommonValues.PermissionCodes.Role.RoleTypeCreate)]
        [HttpGet]
        public ActionResult RoleCreate()
        {
            SetDefaults();
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Role.RoleTypeIndex, CommonValues.PermissionCodes.Role.RoleTypeCreate)]
        [HttpPost]
        public ActionResult RoleCreate(RoleIndexViewModel viewModel)
        {
            SetDefaults();
            if (ModelState.IsValid)
            {
                var roleBo = new RoleBL();
                viewModel.CommandType = viewModel.RoleId > 0 ? CommonValues.DMLType.Update : CommonValues.DMLType.Insert;
                roleBo.DMLRole(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
                ModelState.Clear();
                return View();

            }

            return View(viewModel);
        }
        #endregion

        #region Role Update
        [AuthorizationFilter(CommonValues.PermissionCodes.Role.RoleTypeIndex, CommonValues.PermissionCodes.Role.RoleTypeUpdate)]
        [HttpGet]
        public ActionResult RoleUpdate(int id = 0)
        {
            SetDefaults();
            var v = new RoleIndexViewModel();
            if (id > 0)
            {
                var roleBo = new RoleBL();

                v.RoleId = id;
                roleBo.GetRole(UserManager.UserInfo, v);
            }
            return View(v);
        }
        [AuthorizationFilter(CommonValues.PermissionCodes.Role.RoleTypeIndex, CommonValues.PermissionCodes.Role.RoleTypeUpdate)]
        [HttpPost]
        public ActionResult RoleUpdate(RoleIndexViewModel viewModel)
        {
            SetDefaults();
            var roleBo = new RoleBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = viewModel.RoleId > 0 ? CommonValues.DMLType.Update : CommonValues.DMLType.Insert;
                roleBo.DMLRole(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
                viewModel.RoleTypeName = (MultiLanguageModel)CommonUtility.DeepClone(viewModel.RoleTypeName);
                viewModel.RoleTypeName.MultiLanguageContentAsText = viewModel.MultiLanguageContentAsText;
            }
            return View(viewModel);
        }
        #endregion

        #region Role Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.Role.RoleTypeIndex, CommonValues.PermissionCodes.Role.RoleTypeDelete)]
        public ActionResult DeleteRole(int roleId)
        {
            RolePermissionBL rpBo = new RolePermissionBL();
            List<PermissionListModel> rolePermissionList = rpBo.ListPermissionsIncludedInRole(UserManager.UserInfo, roleId).Data;
            if (rolePermissionList.Count == 0)
            {
                RoleIndexViewModel viewModel = new RoleIndexViewModel() { RoleId = roleId };
                var roleBo = new RoleBL();
                viewModel.CommandType = viewModel.RoleId > 0 ? CommonValues.DMLType.Delete : string.Empty;
                roleBo.DMLRole(UserManager.UserInfo, viewModel);

                ModelState.Clear();
                if (viewModel.ErrorNo == 0)
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
            }
            else
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Role_Display_Warning_PermissionExists);
            }
        }

        #endregion

        #region Role Details

        [AuthorizationFilter(CommonValues.PermissionCodes.Role.RoleTypeIndex, CommonValues.PermissionCodes.Role.RoleTypeDetails)]
        [HttpGet]
        public ActionResult RoleDetails(int id = 0)
        {
            var v = new RoleIndexViewModel();
            var roleBo = new RoleBL();

            v.RoleId = id;
            roleBo.GetRole(UserManager.UserInfo, v);

            return View(v);
        }

        #endregion
    }
}
