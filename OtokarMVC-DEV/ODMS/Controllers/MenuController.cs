using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSModel.ListModel;
using ODMSModel.Menu;
using ODMSModel.ViewModel;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class MenuController : ControllerBase
    {
        #region Menu Index

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Menu.MenuIndex)]
        public ActionResult MenuIndex()
        {
            return View();
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Menu.MenuIndex, ODMSCommon.CommonValues.PermissionCodes.Menu.MenuDetails)]
        public ActionResult ListMenu([DataSourceRequest] DataSourceRequest request, MenuListModel model)
        {
            var menuBo = new MenuBL();
            var v = new MenuListModel(request);
            var totalCnt = 0;
            v.MenuText = model.MenuText;
            var returnValue = menuBo.ListMenu(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region Menu Details

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Menu.MenuIndex, ODMSCommon.CommonValues.PermissionCodes.Menu.MenuDetails)]
        [HttpGet]
        public ActionResult MenuDetails(int id = 0)
        {
            var v = new MenuIndexViewModel();
            var roleBo = new MenuBL();

            v.MenuId = id;
            roleBo.GetMenu(UserManager.UserInfo, v);

            return View(v);
        }

        #endregion

        #region Menu Update

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Menu.MenuIndex, ODMSCommon.CommonValues.PermissionCodes.Menu.MenuUpdate)]
        [HttpGet]
        public ActionResult MenuUpdate(int id = 0)
        {
            var v = new MenuIndexViewModel();
            if (id > 0)
            {
                var menuBo = new MenuBL();

                v.MenuId = id;
                menuBo.GetMenu(UserManager.UserInfo, v);
            }
            return View(v);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Menu.MenuIndex, ODMSCommon.CommonValues.PermissionCodes.Menu.MenuUpdate)]
        [HttpPost]
        public ActionResult MenuUpdate(MenuIndexViewModel viewModel)
        {
            var menuBo = new MenuBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = viewModel.MenuId > 0 ? ODMSCommon.CommonValues.DMLType.Update : ODMSCommon.CommonValues.DMLType.Insert;
                menuBo.DMLMenu(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
                viewModel.MenuText = (MultiLanguageModel)ODMSCommon.CommonUtility.DeepClone(viewModel.MenuText);
                viewModel.MenuText.MultiLanguageContentAsText = viewModel.MultiLanguageContentAsText;
            }
            return View(viewModel);
        }

        #endregion

        [HttpGet]
        public ActionResult MenuPlacementUpdate(int id, string menuName)
        {
            var bl = new MenuBL();
            var model = new MenuPlacementViewModel()
            {
                MenuId = id,
                MenuName = menuName,
                DefinedSubMenuList = bl.GetDefinedSubMenuList(UserManager.UserInfo, id).Data
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult MenuPlacementUpdate(MenuPlacementViewModel model)
        {
            var bl = new MenuBL();


            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.SubMenuString))
                {
                    model.SubMenus = model.SubMenuString.Split(',').AsEnumerable();
                }

                bl.DMLMenuPlacement(UserManager.UserInfo, model);
            }
            CheckErrorForMessage(model, true);
            return View(model);
        }

        public JsonResult ListSubMenus(int id)
        {
            return Json(new MenuBL().GetSubMenuList(UserManager.UserInfo, id).Data, JsonRequestBehavior.AllowGet);
        }

    }
}
