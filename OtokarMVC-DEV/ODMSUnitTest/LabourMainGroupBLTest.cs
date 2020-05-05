using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.LabourMainGroup;
using System.Collections.Generic;


namespace ODMSUnitTest
{

    [TestClass]
    public class LabourMainGroupBLTest
    {

        LabourMainGroupBL _LabourMainGroupBL = new LabourMainGroupBL();

        [TestMethod]
        public void LabourMainGroupBL_Insert_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourMainGroupViewModel();
            model.MainGroupId = guid;
            model.LabourGroupName = guid;
            model.Description = guid;
            model.MultiLanguageContentAsText = "TR || TEST";
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _LabourMainGroupBL.Insert(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void LabourMainGroupBL_List_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourMainGroupViewModel();
            model.MainGroupId = guid;
            model.LabourGroupName = guid;
            model.Description = guid;
            model.MultiLanguageContentAsText = "TR || TEST";
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _LabourMainGroupBL.Update(UserManager.UserInfo, model);

            int count = 0;
            var filter = new LabourMainGroupListModel();

            var resultGet = _LabourMainGroupBL.List(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void LabourMainGroupBL_Update_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourMainGroupViewModel();
            model.MainGroupId = guid;
            model.LabourGroupName = guid;
            model.Description = guid;
            model.MultiLanguageContentAsText = "TR || TEST";
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _LabourMainGroupBL.Insert(UserManager.UserInfo, model);

            var filter = new LabourMainGroupListModel();

            int count = 0;
            var resultGet = _LabourMainGroupBL.List(UserManager.UserInfo, filter, out count);

            var modelUpdate = new LabourMainGroupViewModel();
            modelUpdate.MainGroupId = guid;
            modelUpdate.LabourGroupName = guid;
            modelUpdate.Description = guid;
            modelUpdate.MultiLanguageContentAsText = "TR || TEST";
            modelUpdate.CommandType = "U";
            var resultUpdate = _LabourMainGroupBL.Update(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void LabourMainGroupBL_Update_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourMainGroupViewModel();
            model.MainGroupId = guid;
            model.LabourGroupName = guid;
            model.Description = guid;
            model.MultiLanguageContentAsText = "TR || TEST";
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _LabourMainGroupBL.Insert(UserManager.UserInfo, model);

            var filter = new LabourMainGroupListModel();

            int count = 0;
            var resultGet = _LabourMainGroupBL.List(UserManager.UserInfo, filter, out count);

            var modelDelete = new LabourMainGroupViewModel();
            modelDelete.MainGroupId = guid;
            modelDelete.LabourGroupName = guid;
            modelDelete.Description = guid;
            modelDelete.MultiLanguageContentAsText = "TR || TEST";
            modelDelete.CommandType = "D";
            var resultDelete = _LabourMainGroupBL.Update(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }

    }

}

