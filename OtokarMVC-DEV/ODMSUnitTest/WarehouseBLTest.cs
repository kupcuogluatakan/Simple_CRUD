using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.Warehouse;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class WarehouseBLTest
    {

        WarehouseBL _WarehouseBL = new WarehouseBL();

        [TestMethod]
        public void WarehouseBL_DMLWarehouse_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new WarehouseDetailModel();
            model.Code = guid;
            model.Name = guid;
            model.DealerName = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.StorageTypeName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _WarehouseBL.DMLWarehouse(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void WarehouseBL_GetWarehouse_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new WarehouseDetailModel();
            model.Code = guid;
            model.Name = guid;
            model.DealerName = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.StorageTypeName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _WarehouseBL.DMLWarehouse(UserManager.UserInfo, model);

            var filter = new WarehouseDetailModel();
            filter.Id = result.Model.Id;
            filter.Code = guid;
            filter.DealerId = UserManager.UserInfo.DealerID;

            var resultGet = _WarehouseBL.GetWarehouse(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Id > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WarehouseBL_GetWarehouseIndexModel_GetModel()
        {
            var resultGet = _WarehouseBL.GetWarehouseIndexModel(UserManager.UserInfo.DealerID);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.DealerList.Count > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WarehouseBL_ListWarehouses_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new WarehouseDetailModel();
            model.Code = guid;
            model.Name = guid;
            model.DealerName = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.StorageTypeName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _WarehouseBL.DMLWarehouse(UserManager.UserInfo, model);

            int count = 0;
            var filter = new WarehouseListModel();
            filter.Code = guid;
            filter.DealerId = UserManager.UserInfo.DealerID;

            var resultGet = _WarehouseBL.ListWarehouses(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void WarehouseBL_DMLWarehouse_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new WarehouseDetailModel();
            model.Code = guid;
            model.Name = guid;
            model.DealerName = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.StorageTypeName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _WarehouseBL.DMLWarehouse(UserManager.UserInfo, model);

            var filter = new WarehouseListModel();
            filter.Code = guid;
            filter.DealerId = UserManager.UserInfo.DealerID;

            int count = 0;
            var resultGet = _WarehouseBL.ListWarehouses(UserManager.UserInfo, filter, out count);

            var modelUpdate = new WarehouseDetailModel();
            modelUpdate.Id = resultGet.Data.First().Id;
            modelUpdate.Code = guid;
            modelUpdate.Name = guid;
            modelUpdate.DealerName = guid;
            modelUpdate.DealerId = UserManager.UserInfo.DealerID;
            modelUpdate.StorageTypeName = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _WarehouseBL.DMLWarehouse(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void WarehouseBL_DMLWarehouse_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new WarehouseDetailModel();
            model.Code = guid;
            model.Name = guid;
            model.DealerName = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.StorageTypeName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _WarehouseBL.DMLWarehouse(UserManager.UserInfo, model);

            var filter = new WarehouseListModel();
            filter.Code = guid;
            filter.DealerId = UserManager.UserInfo.DealerID;

            int count = 0;
            var resultGet = _WarehouseBL.ListWarehouses(UserManager.UserInfo, filter, out count);

            var modelDelete = new WarehouseDetailModel();
            modelDelete.Id = resultGet.Data.First().Id;
            modelDelete.Code = guid;
            modelDelete.Name = guid;
            modelDelete.DealerName = guid;
            modelDelete.DealerId = UserManager.UserInfo.DealerID;
            modelDelete.StorageTypeName = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _WarehouseBL.DMLWarehouse(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }

        [TestMethod]
        public void WarehouseBL_ListWarehousesOfDealerAsSelectList_GetAll()
        {
            var resultGet = WarehouseBL.ListWarehousesOfDealerAsSelectList(null);

            Assert.IsTrue(resultGet.Total > 0);
        }
        
    }

}

