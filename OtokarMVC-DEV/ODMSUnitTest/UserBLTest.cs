using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.User;
using System;
using ODMSModel.ListModel;


namespace ODMSUnitTest
{

    [TestClass]
    public class UserBLTest
    {

        UserBL _UserBL = new UserBL();

        [TestMethod]
        public void UserBL_DMLUser_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new UserIndexViewModel();
            model.DealerId = UserManager.UserInfo.GetUserDealerId().ToString();
            model.UserCode = guid;
            model.Password = guid;
            model.DealerId = UserManager.UserInfo.DealerID.ToString();
            model.DealerName = guid;
            model.LanguageCode = guid;
            model.UserFirstName = guid;
            model.UserMidName = guid;
            model.UserLastName = guid;
            model.PassportNo = guid;
            model.IdentityNo = guid;
            model.Sex = guid;
            model.MaritalStatus = guid;
            model.PhotoDocId = 1;
            model.Phone = guid;
            model.Mobile = guid;
            model.Extension = guid;
            model.EMail = guid;
            model.Address = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.RoleTypeName = guid;
            model.IsPartial = true;
            model.IsPasswordSet = true;
            model.IsTechnician = true;
            model.Index = 1;
            model.IsAutoPassword = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _UserBL.DMLUser(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void UserBL_DMLUserPassword_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new UserIndexViewModel();
            model.DealerId = UserManager.UserInfo.GetUserDealerId().ToString();
            model.UserCode = guid;
            model.Password = guid;
            model.DealerId = UserManager.UserInfo.DealerID.ToString();
            model.DealerName = guid;
            model.LanguageCode = guid;
            model.UserFirstName = guid;
            model.UserMidName = guid;
            model.UserLastName = guid;
            model.PassportNo = guid;
            model.IdentityNo = guid;
            model.Sex = guid;
            model.MaritalStatus = guid;
            model.PhotoDocId = 1;
            model.Phone = guid;
            model.Mobile = guid;
            model.Extension = guid;
            model.EMail = guid;
            model.Address = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.RoleTypeName = guid;
            model.IsPartial = true;
            model.IsPasswordSet = true;
            model.IsTechnician = true;
            model.Index = 1;
            model.IsAutoPassword = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _UserBL.DMLUserPassword(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void UserBL_GetUser_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new UserIndexViewModel();
            model.DealerId = UserManager.UserInfo.GetUserDealerId().ToString();
            model.UserCode = guid;
            model.Password = guid;
            model.DealerId = UserManager.UserInfo.DealerID.ToString();
            model.DealerName = guid;
            model.LanguageCode = guid;
            model.UserFirstName = guid;
            model.UserMidName = guid;
            model.UserLastName = guid;
            model.PassportNo = guid;
            model.IdentityNo = guid;
            model.Sex = guid;
            model.MaritalStatus = guid;
            model.PhotoDocId = 1;
            model.Phone = guid;
            model.Mobile = guid;
            model.Extension = guid;
            model.EMail = guid;
            model.Address = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.RoleTypeName = guid;
            model.IsPartial = true;
            model.IsPasswordSet = true;
            model.IsTechnician = true;
            model.Index = 1;
            model.IsAutoPassword = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _UserBL.DMLUserPassword(UserManager.UserInfo, model);

            var filter = new UserIndexViewModel();
            filter.UserId = result.Model.UserId;
            filter.DealerId = UserManager.UserInfo.GetUserDealerId().ToString();
            filter.UserCode = guid;
            filter.DealerId = UserManager.UserInfo.DealerID.ToString();
            filter.LanguageCode = guid;

            var resultGet = _UserBL.GetUser(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.UserFirstName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void UserBL_GetUserView_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new UserIndexViewModel();
            model.DealerId = UserManager.UserInfo.GetUserDealerId().ToString();
            model.UserCode = guid;
            model.Password = guid;
            model.DealerId = UserManager.UserInfo.DealerID.ToString();
            model.DealerName = guid;
            model.LanguageCode = guid;
            model.UserFirstName = guid;
            model.UserMidName = guid;
            model.UserLastName = guid;
            model.PassportNo = guid;
            model.IdentityNo = guid;
            model.Sex = guid;
            model.MaritalStatus = guid;
            model.PhotoDocId = 1;
            model.Phone = guid;
            model.Mobile = guid;
            model.Extension = guid;
            model.EMail = guid;
            model.Address = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.RoleTypeName = guid;
            model.IsPartial = true;
            model.IsPasswordSet = true;
            model.IsTechnician = true;
            model.Index = 1;
            model.IsAutoPassword = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _UserBL.DMLUserPassword(UserManager.UserInfo, model);

            var filter = new UserIndexViewModel();
            filter.UserId = result.Model.UserId;
            filter.DealerId = UserManager.UserInfo.GetUserDealerId().ToString();
            filter.UserCode = guid;
            filter.DealerId = UserManager.UserInfo.DealerID.ToString();
            filter.LanguageCode = guid;

            var resultGet = _UserBL.GetUserView(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.UserFirstName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void UserBL_GetUserByTCIdentityNo_GetModel()
        {
            var resultGet = _UserBL.GetUserByTCIdentityNo(UserManager.UserInfo, "48913271162");

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.EMail != String.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void UserBL_GetUserByPassportNo_GetModel()
        {
            var resultGet = _UserBL.GetUserByPassportNo(UserManager.UserInfo, "343rerere");

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.EMail != String.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void UserBL_ListUsers_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new UserIndexViewModel();
            model.DealerId = UserManager.UserInfo.GetUserDealerId().ToString();
            model.UserCode = guid;
            model.Password = guid;
            model.DealerId = UserManager.UserInfo.DealerID.ToString();
            model.DealerName = guid;
            model.LanguageCode = guid;
            model.UserFirstName = guid;
            model.UserMidName = guid;
            model.UserLastName = guid;
            model.PassportNo = guid;
            model.IdentityNo = guid;
            model.Sex = guid;
            model.MaritalStatus = guid;
            model.PhotoDocId = 1;
            model.Phone = guid;
            model.Mobile = guid;
            model.Extension = guid;
            model.EMail = guid;
            model.Address = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.RoleTypeName = guid;
            model.IsPartial = true;
            model.IsPasswordSet = true;
            model.IsTechnician = true;
            model.Index = 1;
            model.IsAutoPassword = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _UserBL.DMLUserPassword(UserManager.UserInfo, model);

            int count = 0;
            var filter = new UserListModel();
            filter.DealerId = UserManager.UserInfo.UserId;
            filter.UserCode = guid;
            filter.DealerId = UserManager.UserInfo.DealerID;

            var resultGet = _UserBL.ListUsers(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void UserBL_DMLUserPassword_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new UserIndexViewModel();
            model.UserId = UserManager.UserInfo.UserId;
            model.UserCode = guid;
            model.Password = guid;
            model.DealerId = UserManager.UserInfo.DealerID.ToString();
            model.DealerName = guid;
            model.LanguageCode = guid;
            model.UserFirstName = guid;
            model.UserMidName = guid;
            model.UserLastName = guid;
            model.PassportNo = guid;
            model.IdentityNo = guid;
            model.Sex = guid;
            model.MaritalStatus = guid;
            model.PhotoDocId = 1;
            model.Phone = guid;
            model.Mobile = guid;
            model.Extension = guid;
            model.EMail = guid;
            model.Address = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.RoleTypeName = guid;
            model.IsPartial = true;
            model.IsPasswordSet = true;
            model.IsTechnician = true;
            model.Index = 1;
            model.IsAutoPassword = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _UserBL.DMLUserPassword(UserManager.UserInfo, model);

            var filter = new UserListModel();
            filter.DealerId = UserManager.UserInfo.UserId;
            filter.UserCode = guid;
            filter.DealerId = UserManager.UserInfo.DealerID;

            int count = 0;
            var resultGet = _UserBL.ListUsers(UserManager.UserInfo, filter, out count);

            var modelUpdate = new UserIndexViewModel();
            modelUpdate.UserId = resultGet.Data.First().UserId;
            modelUpdate.UserCode = guid;
            modelUpdate.Password = guid;
            modelUpdate.DealerId = UserManager.UserInfo.DealerID.ToString();
            modelUpdate.DealerName = guid;
            modelUpdate.LanguageCode = guid;
            modelUpdate.UserFirstName = guid;
            modelUpdate.UserMidName = guid;
            modelUpdate.UserLastName = guid;
            modelUpdate.PassportNo = guid;
            modelUpdate.IdentityNo = guid;
            modelUpdate.Sex = guid;
            modelUpdate.MaritalStatus = guid;

            modelUpdate.Phone = guid;
            modelUpdate.Mobile = guid;
            modelUpdate.Extension = guid;
            modelUpdate.EMail = guid;
            modelUpdate.Address = guid;
            modelUpdate.IsActiveName = guid;
            modelUpdate.RoleTypeName = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _UserBL.DMLUserPassword(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void UserBL_DMLUserPassword_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new UserIndexViewModel();
            model.UserId = UserManager.UserInfo.UserId;
            model.UserCode = guid;
            model.Password = guid;
            model.DealerId = UserManager.UserInfo.DealerID.ToString();
            model.DealerName = guid;
            model.LanguageCode = guid;
            model.UserFirstName = guid;
            model.UserMidName = guid;
            model.UserLastName = guid;
            model.PassportNo = guid;
            model.IdentityNo = guid;
            model.Sex = guid;
            model.MaritalStatus = guid;
            model.PhotoDocId = 1;
            model.Phone = guid;
            model.Mobile = guid;
            model.Extension = guid;
            model.EMail = guid;
            model.Address = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.RoleTypeName = guid;
            model.IsPartial = true;
            model.IsPasswordSet = true;
            model.IsTechnician = true;
            model.Index = 1;
            model.IsAutoPassword = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _UserBL.DMLUserPassword(UserManager.UserInfo, model);

            var filter = new UserListModel();
            filter.DealerId = UserManager.UserInfo.UserId;
            filter.UserCode = guid;
            filter.DealerId = UserManager.UserInfo.DealerID;

            int count = 0;
            var resultGet = _UserBL.ListUsers(UserManager.UserInfo, filter, out count);

            var modelDelete = new UserIndexViewModel();
            modelDelete.UserId = resultGet.Data.First().UserId;
            modelDelete.UserCode = guid;
            modelDelete.Password = guid;
            modelDelete.DealerId = UserManager.UserInfo.DealerID.ToString();
            modelDelete.DealerName = guid;
            modelDelete.LanguageCode = guid;
            modelDelete.UserFirstName = guid;
            modelDelete.UserMidName = guid;
            modelDelete.UserLastName = guid;
            modelDelete.PassportNo = guid;
            modelDelete.IdentityNo = guid;
            modelDelete.Sex = guid;
            modelDelete.MaritalStatus = guid;
            modelDelete.Phone = guid;
            modelDelete.Mobile = guid;
            modelDelete.Extension = guid;
            modelDelete.EMail = guid;
            modelDelete.Address = guid;
            modelDelete.IsActiveName = guid;
            modelDelete.RoleTypeName = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _UserBL.DMLUserPassword(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}

