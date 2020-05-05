using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.UserRole;
using ODMSModel.RolePermission;

namespace ODMSUnitTest
{

    [TestClass]
    public class UserRoleBLTest
    {

        UserRoleBL _UserRoleBL = new UserRoleBL();

        [TestMethod]
        public void UserRoleBL_DMLUserRole_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new UserRoleViewModel();
            model.UserId = UserManager.UserInfo.UserId;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _UserRoleBL.DMLUserRole(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void UserRoleBL_Save_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new SaveModel();
            model.RoleId = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _UserRoleBL.Save(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void UserRoleBL_GetUserRolesIncluded_GetModel()
        {
            var resultGet = _UserRoleBL.GetUserRolesIncluded(UserManager.UserInfo, UserManager.UserInfo.UserId.ToString());

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Text != String.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void UserRoleBL_GetUserRolesExcluded_GetModel()
        {
            var resultGet = _UserRoleBL.GetUserRolesExcluded(UserManager.UserInfo, UserManager.UserInfo.UserId.ToString());

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Text != String.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void UserRoleBL_GetUsersList_GetAll()
        {
            var resultGet = _UserRoleBL.GetUsersList(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

