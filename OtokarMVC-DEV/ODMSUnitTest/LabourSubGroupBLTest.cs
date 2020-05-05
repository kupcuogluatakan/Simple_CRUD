using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.LabourSubGroup;


namespace ODMSUnitTest
{

    [TestClass]
    public class LabourSubGroupBLTest
    {

        LabourSubGroupBL _LabourSubGroupBL = new LabourSubGroupBL();

        [TestMethod]
        public void LabourSubGroupBL_Insert_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourSubGroupViewModel();
            model.SubGroupId = guid;
            model.MainGroupId = guid;
            model.Description = guid;
            model.LabourSubGroupName = guid;
            model.MultiLanguageContentAsText = "TR || TEST";
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _LabourSubGroupBL.Insert(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void LabourSubGroupBL_Insert_Insert_1()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourSubGroupViewModel();
            model.SubGroupId = guid;
            model.MainGroupId = guid;
            model.Description = guid;
            model.LabourSubGroupName = guid;
            model.MultiLanguageContentAsText = "TR || TEST";
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _LabourSubGroupBL.Insert(UserManager.UserInfo, model, new System.Collections.Generic.List<LabourSubGroupViewModel>());

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void LabourSubGroupBL_Get_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourSubGroupViewModel();
            model.SubGroupId = guid;
            model.MainGroupId = guid;
            model.Description = guid;
            model.LabourSubGroupName = guid;
            model.MultiLanguageContentAsText = "TR || TEST";
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _LabourSubGroupBL.Update(UserManager.UserInfo, model);

            var filter = new LabourSubGroupViewModel();
            filter.SubGroupId = result.Model.SubGroupId;
            filter.MultiLanguageContentAsText = "TR || TEST";

            var resultGet = _LabourSubGroupBL.Get(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.LabourSubGroupName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void LabourSubGroupBL_List_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourSubGroupViewModel();
            model.SubGroupId = guid;
            model.MainGroupId = guid;
            model.Description = guid;
            model.LabourSubGroupName = guid;
            model.MultiLanguageContentAsText = "TR || TEST";
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _LabourSubGroupBL.Update(UserManager.UserInfo, model);

            int count = 0;
            var filter = new LabourSubGroupListModel();

            var resultGet = _LabourSubGroupBL.List(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void LabourSubGroupBL_Update_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourSubGroupViewModel();
            model.SubGroupId = guid;
            model.MainGroupId = guid;
            model.Description = guid;
            model.LabourSubGroupName = guid;
            model.MultiLanguageContentAsText = "TR || TEST";
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _LabourSubGroupBL.Update(UserManager.UserInfo, model);

            var filter = new LabourSubGroupListModel();

            int count = 0;
            var resultGet = _LabourSubGroupBL.List(UserManager.UserInfo, filter, out count);

            var modelUpdate = new LabourSubGroupViewModel();
            modelUpdate.SubGroupId = resultGet.Data.First().SubGroupId;
            modelUpdate.SubGroupId = guid;
            modelUpdate.MainGroupId = guid;
            modelUpdate.Description = guid;
            modelUpdate.LabourSubGroupName = guid;
            modelUpdate.MultiLanguageContentAsText = "TR || TEST";



            modelUpdate.CommandType = "U";
            var resultUpdate = _LabourSubGroupBL.Update(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void LabourSubGroupBL_Update_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourSubGroupViewModel();
            model.SubGroupId = guid;
            model.MainGroupId = guid;
            model.Description = guid;
            model.LabourSubGroupName = guid;
            model.MultiLanguageContentAsText = "TR || TEST";
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _LabourSubGroupBL.Update(UserManager.UserInfo, model);

            var filter = new LabourSubGroupListModel();

            int count = 0;
            var resultGet = _LabourSubGroupBL.List(UserManager.UserInfo, filter, out count);

            var modelDelete = new LabourSubGroupViewModel();
            modelDelete.SubGroupId = resultGet.Data.First().SubGroupId;
            modelDelete.SubGroupId = guid;
            modelDelete.MainGroupId = guid;
            modelDelete.Description = guid;
            modelDelete.LabourSubGroupName = guid;
            modelDelete.MultiLanguageContentAsText = "TR || TEST";



            modelDelete.CommandType = "D";
            var resultDelete = _LabourSubGroupBL.Update(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }

       

    }

}

