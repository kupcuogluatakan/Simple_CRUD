using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.DeliveryGoodsPlacement;
using ODMSModel.DeliveryListPart;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class DeliveryGoodsPlacementBLTest
    {

        DeliveryGoodsPlacementBL _DeliveryGoodsPlacementBL = new DeliveryGoodsPlacementBL();

        [TestMethod]
        public void DeliveryGoodsPlacementBL_DMLPartsPlacement_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new PartsPlacementViewModel();
            model.PlacementId = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DeliveryGoodsPlacementBL.DMLPartsPlacement(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void DeliveryGoodsPlacementBL_DMLPartsPlacemntDefault_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DeliveryListPartSubViewModel();
            model.DeliverySeqNo = 1;
            model.DeliveryId = 1;
            model.Quantity = 1;
            model.ShipQnty = 1;
            model.ReceiveQnty = 1;
            model.PoDetSeqNo = 1;
            model.SapOfferNo = guid;
            model.SapRowNo = guid;
            model.SapOriginalRowNo = guid;
            model.InvoicePrice = 1;
            model.DefaultType = guid;
            model.WarehouseId = 1;
            model.RackId = 1;
            model.StockTypeId = 1;
            model.Qty = 1;
            model.PartId = 1;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerPrice = 1;
            model.TransactionDescription = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DeliveryGoodsPlacementBL.DMLPartsPlacemntDefault(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void DeliveryGoodsPlacementBL_ListDeliveryGoodsPlacement_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DeliveryListPartSubViewModel();
            model.DeliverySeqNo = 1;
            model.DeliveryId = 1;
            model.Quantity = 1;
            model.ShipQnty = 1;
            model.ReceiveQnty = 1;
            model.PoDetSeqNo = 1;
            model.SapOfferNo = guid;
            model.SapRowNo = guid;
            model.SapOriginalRowNo = guid;
            model.InvoicePrice = 1;
            model.DefaultType = guid;
            model.WarehouseId = 1;
            model.RackId = 1;
            model.StockTypeId = 1;
            model.Qty = 1;
            model.PartId = 1;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerPrice = 1;
            model.TransactionDescription = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DeliveryGoodsPlacementBL.DMLPartsPlacemntDefault(UserManager.UserInfo, model);

            int count = 0;
            var filter = new DeliveryGoodsPlacementListModel();
            filter.StatusId = 3;

            var resultGet = _DeliveryGoodsPlacementBL.ListDeliveryGoodsPlacement(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void DeliveryGoodsPlacementBL_DMLPartsPlacemntDefault_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DeliveryListPartSubViewModel();
            model.DeliverySeqNo = 1;
            model.DeliveryId = 1;
            model.Quantity = 1;
            model.ShipQnty = 1;
            model.ReceiveQnty = 1;
            model.PoDetSeqNo = 1;
            model.SapOfferNo = guid;
            model.SapRowNo = guid;
            model.SapOriginalRowNo = guid;
            model.InvoicePrice = 1;
            model.DefaultType = guid;
            model.WarehouseId = 1;
            model.RackId = 1;
            model.StockTypeId = 1;
            model.Qty = 1;
            model.PartId = 1;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerPrice = 1;
            model.TransactionDescription = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DeliveryGoodsPlacementBL.DMLPartsPlacemntDefault(UserManager.UserInfo, model);

            var filter = new DeliveryGoodsPlacementListModel();

            int count = 0;
            var resultGet = _DeliveryGoodsPlacementBL.ListDeliveryGoodsPlacement(UserManager.UserInfo, filter, out count);

            var modelUpdate = new DeliveryListPartSubViewModel();
            modelUpdate.DeliveryId = resultGet.Data.First().DeliveryId;
            modelUpdate.SapOfferNo = guid;
            modelUpdate.SapRowNo = guid;
            modelUpdate.SapOriginalRowNo = guid;
            modelUpdate.DefaultType = guid;
            modelUpdate.DealerId = UserManager.UserInfo.DealerID;
            modelUpdate.TransactionDescription = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _DeliveryGoodsPlacementBL.DMLPartsPlacemntDefault(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void DeliveryGoodsPlacementBL_DMLPartsPlacemntDefault_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DeliveryListPartSubViewModel();
            model.DeliverySeqNo = 1;
            model.DeliveryId = 1;
            model.Quantity = 1;
            model.ShipQnty = 1;
            model.ReceiveQnty = 1;
            model.PoDetSeqNo = 1;
            model.SapOfferNo = guid;
            model.SapRowNo = guid;
            model.SapOriginalRowNo = guid;
            model.InvoicePrice = 1;
            model.DefaultType = guid;
            model.WarehouseId = 1;
            model.RackId = 1;
            model.StockTypeId = 1;
            model.Qty = 1;
            model.PartId = 1;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerPrice = 1;
            model.TransactionDescription = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DeliveryGoodsPlacementBL.DMLPartsPlacemntDefault(UserManager.UserInfo, model);

            var filter = new DeliveryGoodsPlacementListModel();

            int count = 0;
            var resultGet = _DeliveryGoodsPlacementBL.ListDeliveryGoodsPlacement(UserManager.UserInfo, filter, out count);

            var modelDelete = new DeliveryListPartSubViewModel();
            modelDelete.DeliveryId = resultGet.Data.First().DeliveryId;
            modelDelete.SapOfferNo = guid;
            modelDelete.SapRowNo = guid;
            modelDelete.SapOriginalRowNo = guid;
            modelDelete.DefaultType = guid;
            modelDelete.DealerId = UserManager.UserInfo.DealerID;
            modelDelete.TransactionDescription = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _DeliveryGoodsPlacementBL.DMLPartsPlacemntDefault(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }

        [TestMethod]
        public void DeliveryGoodsPlacementBL_ListPartsPlacement_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DeliveryListPartSubViewModel();
            model.DeliverySeqNo = 1;
            model.DeliveryId = 1;
            model.Quantity = 1;
            model.ShipQnty = 1;
            model.ReceiveQnty = 1;
            model.PoDetSeqNo = 1;
            model.SapOfferNo = guid;
            model.SapRowNo = guid;
            model.SapOriginalRowNo = guid;
            model.InvoicePrice = 1;
            model.DefaultType = guid;
            model.WarehouseId = 1;
            model.RackId = 1;
            model.StockTypeId = 1;
            model.Qty = 1;
            model.PartId = 1;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerPrice = 1;
            model.TransactionDescription = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DeliveryGoodsPlacementBL.DMLPartsPlacemntDefault(UserManager.UserInfo, model);

            int count = 0;
            var filter = new PartsPlacementListModel();
            filter.DeliveryId = result.Model.DeliveryId;
            var resultGet = _DeliveryGoodsPlacementBL.ListPartsPlacement(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void DeliveryGoodsPlacementBL_ListRackWarehouseByDetId_GetAll()
        {
            var resultGet = _DeliveryGoodsPlacementBL.ListRackWarehouseByDetId(UserManager.UserInfo, 1);

            Assert.IsTrue(resultGet.Total > 0);
        }
        

    }

}

