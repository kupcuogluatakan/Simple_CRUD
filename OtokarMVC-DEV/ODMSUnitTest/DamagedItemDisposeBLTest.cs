using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.DamagedItemDispose;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class DamagedItemDisposeBLTest
    {

        DamagedItemDisposeBL _DamagedItemDisposeBL = new DamagedItemDisposeBL();

        [TestMethod]
        public void DamagedItemDisposeBL_DMLDamagedItemDispose_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DamagedItemDisposeViewModel();
            model.DamageDisposeId = 1;
            model.WarehouseName = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.RackName = guid;
            model.StockTypeName = guid;
            model.DocName = guid;
            model.Description = guid;
            model.Quantity = 1;
            model.IsOriginalName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DamagedItemDisposeBL.DMLDamagedItemDispose(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void DamagedItemDisposeBL_GetDamagedItemDispose_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DamagedItemDisposeViewModel();
            model.DamageDisposeId = 1;
            model.WarehouseName = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.RackName = guid;
            model.StockTypeName = guid;
            model.DocName = guid;
            model.Description = guid;
            model.Quantity = 1;
            model.IsOriginalName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DamagedItemDisposeBL.DMLDamagedItemDispose(UserManager.UserInfo, model);

            var filter = new DamagedItemDisposeViewModel();
            filter.DamageDisposeId = result.Model.DamageDisposeId;
            filter.DealerId = UserManager.UserInfo.DealerID;
            filter.PartId = 39399;
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";

            var resultGet = _DamagedItemDisposeBL.GetDamagedItemDispose(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.DealerName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void DamagedItemDisposeBL_ListDamagedItemDisposes_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DamagedItemDisposeViewModel();
            model.DamageDisposeId = 1;
            model.WarehouseName = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.RackName = guid;
            model.StockTypeName = guid;
            model.DocName = guid;
            model.Description = guid;
            model.Quantity = 1;
            model.IsOriginalName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DamagedItemDisposeBL.DMLDamagedItemDispose(UserManager.UserInfo, model);

            int count = 0;
            var filter = new DamagedItemDisposeListModel();
            filter.DealerId = UserManager.UserInfo.DealerID;
            filter.PartId = 39399;
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";

            var resultGet = _DamagedItemDisposeBL.ListDamagedItemDisposes(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void DamagedItemDisposeBL_DMLDamagedItemDispose_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DamagedItemDisposeViewModel();
            model.DamageDisposeId = 1;
            model.WarehouseName = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.RackName = guid;
            model.StockTypeName = guid;
            model.DocName = guid;
            model.Description = guid;
            model.Quantity = 1;
            model.IsOriginalName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DamagedItemDisposeBL.DMLDamagedItemDispose(UserManager.UserInfo, model);

            var filter = new DamagedItemDisposeListModel();
            filter.DealerId = UserManager.UserInfo.DealerID;
            filter.PartId = 39399;
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";

            int count = 0;
            var resultGet = _DamagedItemDisposeBL.ListDamagedItemDisposes(UserManager.UserInfo, filter, out count);

            var modelUpdate = new DamagedItemDisposeViewModel();
            modelUpdate.DamageDisposeId = resultGet.Data.First().DamageDisposeId;

            modelUpdate.WarehouseName = guid;
            modelUpdate.DealerId = UserManager.UserInfo.DealerID;
            modelUpdate.DealerName = guid;
            modelUpdate.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            modelUpdate.RackName = guid;
            modelUpdate.StockTypeName = guid;
            modelUpdate.DocName = guid;
            modelUpdate.Description = guid;

            modelUpdate.IsOriginalName = guid;



            modelUpdate.CommandType = "U";
            var resultUpdate = _DamagedItemDisposeBL.DMLDamagedItemDispose(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void DamagedItemDisposeBL_DMLDamagedItemDispose_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DamagedItemDisposeViewModel();
            model.DamageDisposeId = 1;
            model.WarehouseName = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.RackName = guid;
            model.StockTypeName = guid;
            model.DocName = guid;
            model.Description = guid;
            model.Quantity = 1;
            model.IsOriginalName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DamagedItemDisposeBL.DMLDamagedItemDispose(UserManager.UserInfo, model);

            var filter = new DamagedItemDisposeListModel();
            filter.DealerId = UserManager.UserInfo.DealerID;
            filter.PartId = 39399;
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";

            int count = 0;
            var resultGet = _DamagedItemDisposeBL.ListDamagedItemDisposes(UserManager.UserInfo, filter, out count);

            var modelDelete = new DamagedItemDisposeViewModel();
            modelDelete.DamageDisposeId = resultGet.Data.First().DamageDisposeId;

            modelDelete.WarehouseName = guid;
            modelDelete.DealerId = UserManager.UserInfo.DealerID;
            modelDelete.DealerName = guid;
            modelDelete.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            modelDelete.RackName = guid;
            modelDelete.StockTypeName = guid;
            modelDelete.DocName = guid;
            modelDelete.Description = guid;

            modelDelete.IsOriginalName = guid;



            modelDelete.CommandType = "D";
            var resultDelete = _DamagedItemDisposeBL.DMLDamagedItemDispose(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}

