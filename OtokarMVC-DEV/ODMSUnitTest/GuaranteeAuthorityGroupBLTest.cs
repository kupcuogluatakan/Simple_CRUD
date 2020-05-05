using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.GuaranteeAuthorityGroup;


namespace ODMSUnitTest
{

    [TestClass]
    public class GuaranteeAuthorityGroupBLTest
    {

        GuaranteeAuthorityGroupBL _GuaranteeAuthorityGroupBL = new GuaranteeAuthorityGroupBL();

        [TestMethod]
        public void GuaranteeAuthorityGroupBL_DMLGuaranteeAuthorityGroup_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GuaranteeAuthorityGroupViewModel();
            model.GroupId = 1;
            model.GroupName = guid;
            model.MailList = guid;
            model.IsActiveString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _GuaranteeAuthorityGroupBL.DMLGuaranteeAuthorityGroup(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void GuaranteeAuthorityGroupBL_GetGuaranteeAuthorityGroup_GetModel()
        {
            var resultGet = _GuaranteeAuthorityGroupBL.GetGuaranteeAuthorityGroup(UserManager.UserInfo, 0);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.GroupName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void GuaranteeAuthorityGroupBL_ListGuaranteeAuthorityGroups_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GuaranteeAuthorityGroupViewModel();
            model.GroupId = 1;
            model.GroupName = guid;
            model.MailList = guid;
            model.IsActiveString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _GuaranteeAuthorityGroupBL.DMLGuaranteeAuthorityGroup(UserManager.UserInfo, model);

            int count = 0;
            var filter = new GuaranteeAuthorityGroupListModel();

            var resultGet = _GuaranteeAuthorityGroupBL.ListGuaranteeAuthorityGroups(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void GuaranteeAuthorityGroupBL_DMLGuaranteeAuthorityGroup_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GuaranteeAuthorityGroupViewModel();
            model.GroupId = 1;
            model.GroupName = guid;
            model.MailList = guid;
            model.IsActiveString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _GuaranteeAuthorityGroupBL.DMLGuaranteeAuthorityGroup(UserManager.UserInfo, model);

            var filter = new GuaranteeAuthorityGroupListModel();

            int count = 0;
            var resultGet = _GuaranteeAuthorityGroupBL.ListGuaranteeAuthorityGroups(UserManager.UserInfo, filter, out count);

            var modelUpdate = new GuaranteeAuthorityGroupViewModel();
            modelUpdate.GroupId = resultGet.Data.First().GroupId;
            modelUpdate.GroupName = guid;
            modelUpdate.MailList = guid;
            modelUpdate.IsActiveString = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _GuaranteeAuthorityGroupBL.DMLGuaranteeAuthorityGroup(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void GuaranteeAuthorityGroupBL_DMLGuaranteeAuthorityGroup_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GuaranteeAuthorityGroupViewModel();
            model.GroupId = 1;
            model.GroupName = guid;
            model.MailList = guid;
            model.IsActiveString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _GuaranteeAuthorityGroupBL.DMLGuaranteeAuthorityGroup(UserManager.UserInfo, model);

            var filter = new GuaranteeAuthorityGroupListModel();

            int count = 0;
            var resultGet = _GuaranteeAuthorityGroupBL.ListGuaranteeAuthorityGroups(UserManager.UserInfo, filter, out count);

            var modelDelete = new GuaranteeAuthorityGroupViewModel();
            modelDelete.GroupId = resultGet.Data.First().GroupId;
            modelDelete.GroupName = guid;
            modelDelete.MailList = guid;
            modelDelete.IsActiveString = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _GuaranteeAuthorityGroupBL.DMLGuaranteeAuthorityGroup(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}

