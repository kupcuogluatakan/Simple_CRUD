using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.Menu;
using System;
using ODMSModel.ListModel;


namespace ODMSUnitTest
{

    [TestClass]
    public class MenuBLTest
    {

        MenuBL _MenuBL = new MenuBL();

        [TestMethod]
        public void MenuBL_DMLMenu_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new MenuIndexViewModel();
            model.MenuId = 1;
            model.MultiLanguageContentAsText = "TR || TEST";
            model.LinkName = guid;
            model.OrderNo = 1;
            model.Controller = guid;
            model.Action = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _MenuBL.DMLMenu(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void MenuBL_DMLMenuPlacement_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new MenuPlacementViewModel();
            model.MenuId = 1;
            model.MenuName = guid;
            model.SubMenuString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _MenuBL.DMLMenuPlacement(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void MenuBL_GetMenu_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new MenuPlacementViewModel();
            model.MenuId = 1;
            model.MenuName = guid;
            model.SubMenuString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _MenuBL.DMLMenuPlacement(UserManager.UserInfo, model);

            var filter = new MenuIndexViewModel();
            filter.MultiLanguageContentAsText = "TR || TEST";
            var resultGet = _MenuBL.GetMenu(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.OrderNo > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void MenuBL_GetUserMenu_GetModel()
        {
            var resultGet = _MenuBL.GetUserMenu(UserManager.UserInfo, UserManager.UserInfo.UserId);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.MenuItemId > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void MenuBL_ListMenu_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new MenuPlacementViewModel();
            model.MenuId = 1;
            model.MenuName = guid;
            model.SubMenuString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _MenuBL.DMLMenuPlacement(UserManager.UserInfo, model);

            int count = 0;
            var filter = new MenuListModel();

            var resultGet = _MenuBL.ListMenu(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void MenuBL_DMLMenuPlacement_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new MenuPlacementViewModel();
            model.MenuId = 1;
            model.MenuName = guid;
            model.SubMenuString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _MenuBL.DMLMenuPlacement(UserManager.UserInfo, model);

            var filter = new MenuListModel();

            int count = 0;
            var resultGet = _MenuBL.ListMenu(UserManager.UserInfo, filter, out count);

            var modelUpdate = new MenuPlacementViewModel();
            modelUpdate.MenuName = guid;
            modelUpdate.SubMenuString = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _MenuBL.DMLMenuPlacement(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void MenuBL_DMLMenuPlacement_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new MenuPlacementViewModel();
            model.MenuId = 1;
            model.MenuName = guid;
            model.SubMenuString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _MenuBL.DMLMenuPlacement(UserManager.UserInfo, model);

            var filter = new MenuListModel();

            int count = 0;
            var resultGet = _MenuBL.ListMenu(UserManager.UserInfo, filter, out count);

            var modelDelete = new MenuPlacementViewModel();
            modelDelete.MenuName = guid;
            modelDelete.SubMenuString = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _MenuBL.DMLMenuPlacement(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }

        [TestMethod]
        public void MenuBL_GetDefinedSubMenuList_GetAll()
        {
            var resultGet = _MenuBL.GetDefinedSubMenuList(UserManager.UserInfo, 1);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void MenuBL_GetSubMenuList_GetAll()
        {
            var resultGet = _MenuBL.GetSubMenuList(UserManager.UserInfo, 1);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

