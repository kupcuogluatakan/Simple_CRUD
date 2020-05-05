using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.Labour;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class LabourBLTest
    {

        LabourBL _LabourBL = new LabourBL();

        [TestMethod]
        public void LabourBL_DMLLabour_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.LabourId = 211;
            model.LabourMainGroupName = guid;
            model.LabourSubGroupName = guid;
            model.RepairCode = guid;
            model.LabourCode = guid;
            model.AdminDesc = guid;
            model.IsDealerDuration = true;
            model.LabourSSID = guid;
            model.IsActive = true;
            model.IsExternal = true;
            model.LabourType = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _LabourBL.DMLLabour(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void LabourBL_GetLabour_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.LabourId = 211;
            model.LabourMainGroupName = guid;
            model.LabourSubGroupName = guid;
            model.RepairCode = guid;
            model.LabourCode = guid;
            model.AdminDesc = guid;
            model.IsDealerDuration = true;
            model.LabourSSID = guid;
            model.IsActive = true;
            model.IsExternal = true;
            model.LabourType = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _LabourBL.DMLLabour(UserManager.UserInfo, model);

            var filter = new LabourViewModel();
            filter.LabourId = result.Model.LabourId;
            filter.MultiLanguageContentAsText = "TR || TEST";
            filter.LabourId = 211;
            filter.RepairCode = guid;
            filter.LabourCode = guid;

            var resultGet = _LabourBL.GetLabour(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.AdminDesc != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void LabourBL_GetLabourList_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.LabourId = 211;
            model.LabourMainGroupName = guid;
            model.LabourSubGroupName = guid;
            model.RepairCode = guid;
            model.LabourCode = guid;
            model.AdminDesc = guid;
            model.IsDealerDuration = true;
            model.LabourSSID = guid;
            model.IsActive = true;
            model.IsExternal = true;
            model.LabourType = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _LabourBL.DMLLabour(UserManager.UserInfo, model);

            int count = 0;
            var filter = new LabourListModel();
            filter.LabourId = 211;
            filter.RepairCode = guid;
            filter.LabourCode = guid;

            var resultGet = _LabourBL.GetLabourList(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void LabourBL_DMLLabour_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.LabourId = 211;
            model.LabourMainGroupName = guid;
            model.LabourSubGroupName = guid;
            model.RepairCode = guid;
            model.LabourCode = guid;
            model.AdminDesc = guid;
            model.IsDealerDuration = true;
            model.LabourSSID = guid;
            model.IsActive = true;
            model.IsExternal = true;
            model.LabourType = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _LabourBL.DMLLabour(UserManager.UserInfo, model);

            var filter = new LabourListModel();
            filter.LabourId = 211;
            filter.RepairCode = guid;
            filter.LabourCode = guid;

            int count = 0;
            var resultGet = _LabourBL.GetLabourList(UserManager.UserInfo, filter, out count);

            var modelUpdate = new LabourViewModel();
            modelUpdate.LabourId = resultGet.Data.First().LabourId;
            modelUpdate.MultiLanguageContentAsText = "TR || TEST";
            modelUpdate.LabourId = 211;
            modelUpdate.LabourMainGroupName = guid;
            modelUpdate.LabourSubGroupName = guid;
            modelUpdate.RepairCode = guid;
            modelUpdate.LabourCode = guid;
            modelUpdate.AdminDesc = guid;

            modelUpdate.LabourSSID = guid;


            modelUpdate.LabourType = guid;


            modelUpdate.CommandType = "U";
            var resultUpdate = _LabourBL.DMLLabour(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void LabourBL_DMLLabour_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.LabourId = 211;
            model.LabourMainGroupName = guid;
            model.LabourSubGroupName = guid;
            model.RepairCode = guid;
            model.LabourCode = guid;
            model.AdminDesc = guid;
            model.IsDealerDuration = true;
            model.LabourSSID = guid;
            model.IsActive = true;
            model.IsExternal = true;
            model.LabourType = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _LabourBL.DMLLabour(UserManager.UserInfo, model);

            var filter = new LabourListModel();
            filter.LabourId = 211;
            filter.RepairCode = guid;
            filter.LabourCode = guid;

            int count = 0;
            var resultGet = _LabourBL.GetLabourList(UserManager.UserInfo, filter, out count);

            var modelDelete = new LabourViewModel();
            modelDelete.LabourId = resultGet.Data.First().LabourId;
            modelDelete.MultiLanguageContentAsText = "TR || TEST";
            modelDelete.LabourId = 211;
            modelDelete.LabourMainGroupName = guid;
            modelDelete.LabourSubGroupName = guid;
            modelDelete.RepairCode = guid;
            modelDelete.LabourCode = guid;
            modelDelete.AdminDesc = guid;
            modelDelete.LabourSSID = guid;
            modelDelete.LabourType = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _LabourBL.DMLLabour(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }

        [TestMethod]
        public void LabourBL_ListSubGrpAsSelectList_GetAll()
        {
            var resultGet = LabourBL.ListSubGrpAsSelectList(UserManager.UserInfo, null);
            Assert.IsTrue(resultGet.Total > 0);
        }


        [TestMethod]
        public void LabourBL_ListLabourNameAsAutoCompleteSearch_GetAll()
        {
            var resultGet = _LabourBL.ListLabourNameAsAutoCompleteSearch(UserManager.UserInfo,"9010010507", string.Empty);
            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void LabourBL_ListWorkOrderLabourNameAsAutoCompleteSearch_GetAll()
        {
            var resultGet = _LabourBL.ListWorkOrderLabourNameAsAutoCompleteSearch(UserManager.UserInfo,"9010010507", string.Empty);
            Assert.IsTrue(resultGet.Total > 0);
        }

    }

}

