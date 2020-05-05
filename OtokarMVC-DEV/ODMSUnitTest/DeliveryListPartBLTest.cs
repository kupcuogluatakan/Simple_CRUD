using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.DeliveryListPart;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class DeliveryListPartBLTest
    {

        DeliveryListPartBL _DeliveryListPartBL = new DeliveryListPartBL();

        [TestMethod]
        public void DeliveryListPartBL_DMLDeliveryListPart_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DeliveryListPartViewModel();
            model.DeliveryId = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DeliveryListPartBL.DMLDeliveryListPart(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void DeliveryListPartBL_DMLDeliveryListDetail_Insert()
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
            var result = _DeliveryListPartBL.DMLDeliveryListDetail(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void DeliveryListPartBL_ListDeliveryListPart_GetAll()
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
            var result = _DeliveryListPartBL.DMLDeliveryListDetail(UserManager.UserInfo, model);

            int count = 0;
            var filter = new DeliveryListPartListModel();
            filter.PartCode = "M.162127";
            filter.PartId = 39399;
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";

            var resultGet = _DeliveryListPartBL.ListDeliveryListPart(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void DeliveryListPartBL_DMLDeliveryListDetail_Update()
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
            var result = _DeliveryListPartBL.DMLDeliveryListDetail(UserManager.UserInfo, model);

            var filter = new DeliveryListPartListModel();
            filter.PartCode = "M.162127";
            filter.PartId = 39399;
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";

            int count = 0;
            var resultGet = _DeliveryListPartBL.ListDeliveryListPart(UserManager.UserInfo, filter, out count);

            var modelUpdate = new DeliveryListPartSubViewModel();
            modelUpdate.DeliverySeqNo = resultGet.Data.First().DeliverySeqNo;
            modelUpdate.DeliveryId = resultGet.Data.First().DeliveryId;
            modelUpdate.SapOfferNo = guid;
            modelUpdate.SapRowNo = guid;
            modelUpdate.SapOriginalRowNo = guid;
            modelUpdate.DefaultType = guid;
            modelUpdate.DealerId = UserManager.UserInfo.DealerID;
            modelUpdate.TransactionDescription = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _DeliveryListPartBL.DMLDeliveryListDetail(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void DeliveryListPartBL_DMLDeliveryListDetail_Delete()
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
            var result = _DeliveryListPartBL.DMLDeliveryListDetail(UserManager.UserInfo, model);

            var filter = new DeliveryListPartListModel();
            filter.PartCode = "M.162127";
            filter.PartId = 39399;
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";

            int count = 0;
            var resultGet = _DeliveryListPartBL.ListDeliveryListPart(UserManager.UserInfo, filter, out count);

            var modelDelete = new DeliveryListPartSubViewModel();
            modelDelete.DeliverySeqNo = resultGet.Data.First().DeliverySeqNo;
            modelDelete.DeliveryId = resultGet.Data.First().DeliveryId;
            modelDelete.SapOfferNo = guid;
            modelDelete.SapRowNo = guid;
            modelDelete.SapOriginalRowNo = guid;
            modelDelete.DefaultType = guid;
            modelDelete.DealerId = UserManager.UserInfo.DealerID;
            modelDelete.TransactionDescription = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _DeliveryListPartBL.DMLDeliveryListDetail(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }

        [TestMethod]
        public void DeliveryListPartBL_CompleteDeliveryListPart_GetAll()
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
            var result = _DeliveryListPartBL.DMLDeliveryListDetail(UserManager.UserInfo, model);

            int count = 0;
            var filter = new DeliveryListPartViewModel();

            var resultGet = _DeliveryListPartBL.CompleteDeliveryListPart(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

