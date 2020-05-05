using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.MaintenancePart;
using System;
using ODMSModel.Maintenance;

namespace ODMSUnitTest
{

    [TestClass]
    public class MaintenancePartBLTest
    {

        MaintenancePartBL _MaintenancePartBL = new MaintenancePartBL();

        [TestMethod]
        public void MaintenancePartBL_DMLMaintenancePart_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new MaintenancePartViewModel();
            model.IsRequestRoot = true;
            model.MaintId = 1;
            model.MaintName = guid;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.IsAlternateAllow = true;
            model.IsDifBrandAllow = true;
            model.IsMust = true;
            model.IsActive = true;
            model.IsActiveString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _MaintenancePartBL.DMLMaintenancePart(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void MaintenancePartBL_GetMaintenancePartList_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new MaintenancePartViewModel();
            model.IsRequestRoot = true;
            model.MaintId = 1;
            model.MaintName = guid;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.IsAlternateAllow = true;
            model.IsDifBrandAllow = true;
            model.IsMust = true;
            model.IsActive = true;
            model.IsActiveString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _MaintenancePartBL.DMLMaintenancePart(UserManager.UserInfo, model);

            int count = 0;
            var filter = new MaintenancePartListModel();
            filter.PartId = 39399;
            filter.PartCode = "M.162127";
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            filter.FailureCode = guid;

            var resultGet = _MaintenancePartBL.GetMaintenancePartList(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void MaintenancePartBL_DMLMaintenancePart_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new MaintenancePartViewModel();
            model.IsRequestRoot = true;
            model.MaintId = 1;
            model.MaintName = guid;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.IsAlternateAllow = true;
            model.IsDifBrandAllow = true;
            model.IsMust = true;
            model.IsActive = true;
            model.IsActiveString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _MaintenancePartBL.DMLMaintenancePart(UserManager.UserInfo, model);

            var filter = new MaintenancePartListModel();
            filter.PartId = 39399;
            filter.PartCode = "M.162127";
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            filter.FailureCode = guid;

            int count = 0;
            var resultGet = _MaintenancePartBL.GetMaintenancePartList(UserManager.UserInfo, filter, out count);

            var modelUpdate = new MaintenancePartViewModel();
            modelUpdate.PartId = 39399;
            modelUpdate.MaintId = 1;
            modelUpdate.MaintName = guid;
            modelUpdate.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            modelUpdate.IsActiveString = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _MaintenancePartBL.DMLMaintenancePart(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void MaintenancePartBL_DMLMaintenancePart_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new MaintenancePartViewModel();
            model.IsRequestRoot = true;
            model.MaintId = 1;
            model.MaintName = guid;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.IsAlternateAllow = true;
            model.IsDifBrandAllow = true;
            model.IsMust = true;
            model.IsActive = true;
            model.IsActiveString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _MaintenancePartBL.DMLMaintenancePart(UserManager.UserInfo, model);

            var filter = new MaintenancePartListModel();
            filter.PartId = 39399;
            filter.PartCode = "M.162127";
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            filter.FailureCode = guid;

            int count = 0;
            var resultGet = _MaintenancePartBL.GetMaintenancePartList(UserManager.UserInfo, filter, out count);

            var modelDelete = new MaintenancePartViewModel();
            modelDelete.PartId = 39399;
            modelDelete.MaintId = 1;
            modelDelete.MaintName = guid;
            modelDelete.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            modelDelete.IsActiveString = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _MaintenancePartBL.DMLMaintenancePart(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }

      

    }

}

