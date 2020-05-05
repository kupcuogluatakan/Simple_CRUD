using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.Maintenance;


namespace ODMSUnitTest
{

    [TestClass]
    public class MaintenanceBLTest
    {

        MaintenanceBL _MaintenanceBL = new MaintenanceBL();

        [TestMethod]
        public void MaintenanceBL_DMLMaintenance_Insert()
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

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void MaintenanceBL_GetMaintenanceForMaintId_GetModel()
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

            var filter = new MaintenanceListModel();
            filter.MaintId = result.Model.MaintId;
            filter.FailureCode = guid;
            filter.Part_LabourCode = guid;

            var resultGet = _MaintenanceBL.GetMaintenanceForMaintId(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.MainCategoryName != String.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void MaintenanceBL_GetMaintenanceForPkColumns_GetModel()
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

            var filter = new MaintenanceListModel();
            filter.MaintId = result.Model.MaintId;
            filter.FailureCode = guid;
            filter.Part_LabourCode = guid;

            var resultGet = _MaintenanceBL.GetMaintenanceForPkColumns(filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.MaintId > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void MaintenanceBL_GetMaintenance_GetModel()
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

            var filter = new MaintenanceViewModel();
            filter.MaintId = result.Model.MaintId;
            filter.MultiLanguageContentAsText = "TR || TEST";
            filter.FailureCodeName = guid;

            var resultGet = _MaintenanceBL.GetMaintenance(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.MaintKM > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void MaintenanceBL_GetMaintenanceList_GetAll()
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

            int count = 0;
            var filter = new MaintenanceListModel();
            filter.MaintId = result.Model.MaintId;
            filter.FailureCode = guid;
            filter.Part_LabourCode = guid;

            var resultGet = _MaintenanceBL.GetMaintenanceList(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void MaintenanceBL_DMLMaintenance_Update()
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

            var filter = new MaintenanceListModel();
            filter.FailureCode = guid;
            filter.MaintId = result.Model.MaintId;
            filter.Part_LabourCode = guid;

            int count = 0;
            var resultGet = _MaintenanceBL.GetMaintenanceList(UserManager.UserInfo, filter, out count);

            var modelUpdate = new MaintenanceViewModel();
            modelUpdate.MaintId = resultGet.Data.First().MaintId;
            modelUpdate.MultiLanguageContentAsText = "TR || TEST";

            modelUpdate._MaintId = guid;
            modelUpdate.MaintNameSearch = guid;
            modelUpdate.VehicleTypeName = guid;
            modelUpdate.VehicleModelName = guid;
            modelUpdate.EngineType = guid;
            modelUpdate.MaintTypeId = guid;
            modelUpdate.MaintTypeName = guid;
            modelUpdate.AdminDesc = guid;

            modelUpdate.MainCategoryName = guid;
            modelUpdate.CategoryName = guid;
            modelUpdate.SubCategoryName = guid;
            modelUpdate.FailureCodeName = guid;


            modelUpdate.CommandType = "U";
            var resultUpdate = _MaintenanceBL.DMLMaintenance(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void MaintenanceBL_DMLMaintenance_Delete()
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

            var filter = new MaintenanceListModel();
            filter.FailureCode = guid;
            filter.MaintId = result.Model.MaintId;
            filter.Part_LabourCode = guid;

            int count = 0;
            var resultGet = _MaintenanceBL.GetMaintenanceList(UserManager.UserInfo, filter, out count);

            var modelDelete = new MaintenanceViewModel();
            modelDelete.MaintId = resultGet.Data.First().MaintId;
            modelDelete.MultiLanguageContentAsText = "TR || TEST";

            modelDelete._MaintId = guid;
            modelDelete.MaintNameSearch = guid;
            modelDelete.VehicleTypeName = guid;
            modelDelete.VehicleModelName = guid;
            modelDelete.EngineType = guid;
            modelDelete.MaintTypeId = guid;
            modelDelete.MaintTypeName = guid;
            modelDelete.AdminDesc = guid;

            modelDelete.MainCategoryName = guid;
            modelDelete.CategoryName = guid;
            modelDelete.SubCategoryName = guid;
            modelDelete.FailureCodeName = guid;


            modelDelete.CommandType = "D";
            var resultDelete = _MaintenanceBL.DMLMaintenance(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}

