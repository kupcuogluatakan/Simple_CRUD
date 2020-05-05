using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.FixAssetInventory;


namespace ODMSUnitTest
{

    [TestClass]
    public class FixAssetInventoryBLTest
    {

        FixAssetInventoryBL _FixAssetInventoryBL = new FixAssetInventoryBL();

        [TestMethod]
        public void FixAssetInventoryBL_DMLFixAssetInventory_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FixAssetInventoryViewModel();
            model.FixAssetInventoryId = 1;
            model.EquipmentTypeName = guid;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.StockTypeName = guid;
            model.RackName = guid;
            model.WarehouseName = guid;
            model.RackWarehouse = guid;
            model.VehicleGroupName = guid;
            model.Code = guid;
            model.Name = guid;
            model.SerialNo = guid;
            model.Description = guid;
            model.Unit = guid;
            model.RestockReason = guid;
            model.StatusName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FixAssetInventoryBL.DMLFixAssetInventory(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void FixAssetInventoryBL_GetFixAssetInventory_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FixAssetInventoryViewModel();
            model.FixAssetInventoryId = 1;
            model.EquipmentTypeName = guid;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.StockTypeName = guid;
            model.RackName = guid;
            model.WarehouseName = guid;
            model.RackWarehouse = guid;
            model.VehicleGroupName = guid;
            model.Code = guid;
            model.Name = guid;
            model.SerialNo = guid;
            model.Description = guid;
            model.Unit = guid;
            model.RestockReason = guid;
            model.StatusName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FixAssetInventoryBL.DMLFixAssetInventory(UserManager.UserInfo, model);

            var filter = new FixAssetInventoryViewModel();
            filter.FixAssetInventoryId = result.Model.FixAssetInventoryId;
            filter.PartId = 39399;
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            filter.Code = guid;

            var resultGet = _FixAssetInventoryBL.GetFixAssetInventory(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Description != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void FixAssetInventoryBL_ListFixAssetInventorys_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FixAssetInventoryViewModel();
            model.FixAssetInventoryId = 1;
            model.EquipmentTypeName = guid;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.StockTypeName = guid;
            model.RackName = guid;
            model.WarehouseName = guid;
            model.RackWarehouse = guid;
            model.VehicleGroupName = guid;
            model.Code = guid;
            model.Name = guid;
            model.SerialNo = guid;
            model.Description = guid;
            model.Unit = guid;
            model.RestockReason = guid;
            model.StatusName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FixAssetInventoryBL.DMLFixAssetInventory(UserManager.UserInfo, model);

            int count = 0;
            var filter = new FixAssetInventoryListModel();
            filter.PartId = 39399;
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            filter.PartCode = "M.162127";
            filter.Code = guid;

            var resultGet = _FixAssetInventoryBL.ListFixAssetInventorys(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void FixAssetInventoryBL_DMLFixAssetInventory_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FixAssetInventoryViewModel();
            model.FixAssetInventoryId = 1;
            model.EquipmentTypeName = guid;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.StockTypeName = guid;
            model.RackName = guid;
            model.WarehouseName = guid;
            model.RackWarehouse = guid;
            model.VehicleGroupName = guid;
            model.Code = guid;
            model.Name = guid;
            model.SerialNo = guid;
            model.Description = guid;
            model.Unit = guid;
            model.RestockReason = guid;
            model.StatusName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FixAssetInventoryBL.DMLFixAssetInventory(UserManager.UserInfo, model);

            var filter = new FixAssetInventoryListModel();
            filter.PartId = 39399;
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            filter.PartCode = "M.162127";
            filter.Code = guid;

            int count = 0;
            var resultGet = _FixAssetInventoryBL.ListFixAssetInventorys(UserManager.UserInfo, filter, out count);

            var modelUpdate = new FixAssetInventoryViewModel();
            modelUpdate.FixAssetInventoryId = resultGet.Data.First().FixAssetInventoryId;

            modelUpdate.EquipmentTypeName = guid;
            modelUpdate.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            modelUpdate.StockTypeName = guid;
            modelUpdate.RackName = guid;
            modelUpdate.WarehouseName = guid;
            modelUpdate.RackWarehouse = guid;
            modelUpdate.VehicleGroupName = guid;
            modelUpdate.Code = guid;
            modelUpdate.Name = guid;
            modelUpdate.SerialNo = guid;
            modelUpdate.Description = guid;
            modelUpdate.Unit = guid;
            modelUpdate.RestockReason = guid;
            modelUpdate.StatusName = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _FixAssetInventoryBL.DMLFixAssetInventory(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void FixAssetInventoryBL_DMLFixAssetInventory_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FixAssetInventoryViewModel();
            model.FixAssetInventoryId = 1;
            model.EquipmentTypeName = guid;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.StockTypeName = guid;
            model.RackName = guid;
            model.WarehouseName = guid;
            model.RackWarehouse = guid;
            model.VehicleGroupName = guid;
            model.Code = guid;
            model.Name = guid;
            model.SerialNo = guid;
            model.Description = guid;
            model.Unit = guid;
            model.RestockReason = guid;
            model.StatusName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FixAssetInventoryBL.DMLFixAssetInventory(UserManager.UserInfo, model);

            var filter = new FixAssetInventoryListModel();
            filter.PartId = 39399;
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            filter.PartCode = "M.162127";
            filter.Code = guid;

            int count = 0;
            var resultGet = _FixAssetInventoryBL.ListFixAssetInventorys(UserManager.UserInfo, filter, out count);

            var modelDelete = new FixAssetInventoryViewModel();
            modelDelete.FixAssetInventoryId = resultGet.Data.First().FixAssetInventoryId;

            modelDelete.EquipmentTypeName = guid;
            modelDelete.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            modelDelete.StockTypeName = guid;
            modelDelete.RackName = guid;
            modelDelete.WarehouseName = guid;
            modelDelete.RackWarehouse = guid;
            modelDelete.VehicleGroupName = guid;
            modelDelete.Code = guid;
            modelDelete.Name = guid;
            modelDelete.SerialNo = guid;
            modelDelete.Description = guid;
            modelDelete.Unit = guid;
            modelDelete.RestockReason = guid;
            modelDelete.StatusName = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _FixAssetInventoryBL.DMLFixAssetInventory(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }

        [TestMethod]
        public void FixAssetInventoryBL_ListEquipmentTypeAsSelectList_GetAll()
        {
            var resultGet = FixAssetInventoryBL.ListEquipmentTypeAsSelectList(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

