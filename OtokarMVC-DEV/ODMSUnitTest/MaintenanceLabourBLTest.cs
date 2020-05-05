using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.MaintenanceLabour;
using ODMSModel.Maintenance;

namespace ODMSUnitTest
{

    [TestClass]
    public class MaintenanceLabourBLTest
    {

        MaintenanceLabourBL _MaintenanceLabourBL = new MaintenanceLabourBL();
        MaintenanceBL _MaintenanceBL = new MaintenanceBL();

        [TestMethod]
        public void MaintenanceLabourBL_DMLMaintenanceLabour_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new MaintenanceViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.MaintId = 1;
            model._MaintId = guid;
            model.MaintNameSearch = guid;
            model.VehicleTypeName = guid;
            model.VehicleModelName = guid;
            model.EngineType = guid;
            model.MaintTypeId = guid;
            model.MaintTypeName = guid;
            model.AdminDesc = guid;
            model.IsActive = true;
            model.MainCategoryName = guid;
            model.CategoryName = guid;
            model.SubCategoryName = guid;
            model.FailureCodeName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _MaintenanceBL.DMLMaintenance(UserManager.UserInfo, model);

            var model1 = new MaintenanceLabourViewModel();
            model1.MaintenanceId = result.Model.MaintId;
            model1.MaintenanceName = guid;
            model1.LabourId = 211;
            model1._LabourId = guid;
            model1.LabourName = guid;
            model1.IsMustString = guid;
            model1.IsCreate = true;
            model1.HideElements = true;
            model1.LabourCode = guid;
            model1.IsActiveString = guid;
            model1.UpdateUser = 1;
            model1.UpdateDate = DateTime.Now;
            model1.IsActive = true;
            model1.CommandType = "I";
            var result1 = _MaintenanceLabourBL.DMLMaintenanceLabour(UserManager.UserInfo, model1);

            Assert.IsTrue(result1.IsSuccess);
        }

        [TestMethod]
        public void MaintenanceLabourBL_GetMaintenanceLabour_GetModel()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new MaintenanceViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.MaintId = 1;
            model._MaintId = guid;
            model.MaintNameSearch = guid;
            model.VehicleTypeName = guid;
            model.VehicleModelName = guid;
            model.EngineType = guid;
            model.MaintTypeId = guid;
            model.MaintTypeName = guid;
            model.AdminDesc = guid;
            model.IsActive = true;
            model.MainCategoryName = guid;
            model.CategoryName = guid;
            model.SubCategoryName = guid;
            model.FailureCodeName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _MaintenanceBL.DMLMaintenance(UserManager.UserInfo, model);

            var resultGet = _MaintenanceLabourBL.GetMaintenanceLabour(UserManager.UserInfo, result.Model.MaintId, 211);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Quantity > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void MaintenanceLabourBL_ListMaintenanceLabours_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new MaintenanceLabourViewModel();
            model.MaintenanceId = 1;
            model.MaintenanceName = guid;
            model.LabourId = 211;
            model._LabourId = guid;
            model.LabourName = guid;
            model.IsMustString = guid;
            model.IsCreate = true;
            model.HideElements = true;
            model.LabourCode = guid;
            model.IsActiveString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _MaintenanceLabourBL.DMLMaintenanceLabour(UserManager.UserInfo, model);

            int count = 0;
            var filter = new MaintenanceLabourListModel();
            filter.LabourId = 211;
            filter.LabourCode = guid;
            filter.FailureCode = guid;

            var resultGet = _MaintenanceLabourBL.ListMaintenanceLabours(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void MaintenanceLabourBL_DMLMaintenanceLabour_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new MaintenanceLabourViewModel();
            model.MaintenanceId = 1;
            model.MaintenanceName = guid;
            model.LabourId = 211;
            model._LabourId = guid;
            model.LabourName = guid;
            model.IsMustString = guid;
            model.IsCreate = true;
            model.HideElements = true;
            model.LabourCode = guid;
            model.IsActiveString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _MaintenanceLabourBL.DMLMaintenanceLabour(UserManager.UserInfo, model);

            var filter = new MaintenanceLabourListModel();
            filter.LabourId = 211;
            filter.LabourCode = guid;
            filter.FailureCode = guid;

            int count = 0;
            var resultGet = _MaintenanceLabourBL.ListMaintenanceLabours(UserManager.UserInfo, filter, out count);

            var modelUpdate = new MaintenanceLabourViewModel();
            modelUpdate.MaintenanceName = guid;
            modelUpdate.LabourId = 211;
            modelUpdate._LabourId = guid;
            modelUpdate.LabourName = guid;
            modelUpdate.IsMustString = guid;
            modelUpdate.LabourCode = guid;
            modelUpdate.IsActiveString = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _MaintenanceLabourBL.DMLMaintenanceLabour(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void MaintenanceLabourBL_DMLMaintenanceLabour_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new MaintenanceLabourViewModel();
            model.MaintenanceId = 1;
            model.MaintenanceName = guid;
            model.LabourId = 211;
            model._LabourId = guid;
            model.LabourName = guid;
            model.IsMustString = guid;
            model.IsCreate = true;
            model.HideElements = true;
            model.LabourCode = guid;
            model.IsActiveString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _MaintenanceLabourBL.DMLMaintenanceLabour(UserManager.UserInfo, model);

            var filter = new MaintenanceLabourListModel();
            filter.LabourId = 211;
            filter.LabourCode = guid;
            filter.FailureCode = guid;

            int count = 0;
            var resultGet = _MaintenanceLabourBL.ListMaintenanceLabours(UserManager.UserInfo, filter, out count);

            var modelDelete = new MaintenanceLabourViewModel();
            modelDelete.MaintenanceName = guid;
            modelDelete.LabourId = 211;
            modelDelete._LabourId = guid;
            modelDelete.LabourName = guid;
            modelDelete.IsMustString = guid;
            modelDelete.LabourCode = guid;
            modelDelete.IsActiveString = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _MaintenanceLabourBL.DMLMaintenanceLabour(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }

        [TestMethod]
        public void MaintenanceLabourBL_ListLabours_GetAll()
        {
            var resultGet = _MaintenanceLabourBL.ListLabours(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

