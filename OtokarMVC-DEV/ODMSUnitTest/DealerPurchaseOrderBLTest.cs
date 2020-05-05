using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.DealerPurchaseOrder;
using System;
using ODMSModel.DealerSaleSparepart;


namespace ODMSUnitTest
{

    [TestClass]
    public class DealerPurchaseOrderBLTest
    {

        DealerPurchaseOrderBL _DealerPurchaseOrderBL = new DealerPurchaseOrderBL();

        [TestMethod]
        public void DealerPurchaseOrderBL_DMLDealerPurchaseOrder_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerPurchaseOrderViewModel();
            model.PurchaseOrderId = 1;
            model.PoDetSeqNo = 1;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.SupplierId = 782;
            model.IsViaCenter = true;
            model.VehicleId = 29627;
            model.VehicleName = guid;
            model.PurchaseStatus = 1;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerPurchaseOrderBL.DMLDealerPurchaseOrder(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void DealerPurchaseOrderBL_GetDealerPurchaseOrder_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerPurchaseOrderViewModel();
            model.PurchaseOrderId = 1;
            model.PoDetSeqNo = 1;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.SupplierId = 782;
            model.IsViaCenter = true;
            model.VehicleId = 29627;
            model.VehicleName = guid;
            model.PurchaseStatus = 1;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerPurchaseOrderBL.DMLDealerPurchaseOrder(UserManager.UserInfo, model);

            var filter = new DealerPurchaseOrderViewModel();
            filter.PurchaseOrderId = result.Model.PurchaseOrderId;
            filter.DealerId = UserManager.UserInfo.DealerID;
            filter.SupplierId = 782;
            filter.VehicleId = 29627;
            filter.PartId = 39399;
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";

            var resultGet = _DealerPurchaseOrderBL.GetDealerPurchaseOrder(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.VehicleName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void DealerPurchaseOrderBL_GetPartDetails_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerPurchaseOrderViewModel();
            model.PurchaseOrderId = 1;
            model.PoDetSeqNo = 1;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.SupplierId = 782;
            model.IsViaCenter = true;
            model.VehicleId = 29627;
            model.VehicleName = guid;
            model.PurchaseStatus = 1;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerPurchaseOrderBL.DMLDealerPurchaseOrder(UserManager.UserInfo, model);

            var filter = new DealerPurchaseOrderViewModel();
            filter.PurchaseOrderId = result.Model.PurchaseOrderId;
            filter.DealerId = UserManager.UserInfo.DealerID;
            filter.SupplierId = 782;
            filter.VehicleId = 29627;
            filter.PartId = 39399;


            var resultGet = _DealerPurchaseOrderBL.GetPartDetails(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.PartName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void DealerPurchaseOrderBL_GetVehicleMustByPOType_GetModel()
        {
            var resultGet = _DealerPurchaseOrderBL.GetVehicleMustByPOType(4);

            Assert.IsTrue(resultGet.Model > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void DealerPurchaseOrderBL_ListDealerOrderPart_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerPurchaseOrderViewModel();
            model.PurchaseOrderId = 1;
            model.PoDetSeqNo = 1;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.SupplierId = 782;
            model.IsViaCenter = true;
            model.VehicleId = 29627;
            model.VehicleName = guid;
            model.PurchaseStatus = 1;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerPurchaseOrderBL.DMLDealerPurchaseOrder(UserManager.UserInfo, model);

            int count = 0;
            var filter = new DealerSaleSparepartListModel();
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            filter.PartCode = "M.162127";
            filter.MultiLanguageContentAsText = "TR || TEST";

            var resultGet = _DealerPurchaseOrderBL.ListDealerOrderPart(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void DealerPurchaseOrderBL_DMLDealerPurchaseOrder_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerPurchaseOrderViewModel();
            model.PurchaseOrderId = 1;
            model.PoDetSeqNo = 1;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.SupplierId = 782;
            model.IsViaCenter = true;
            model.VehicleId = 29627;
            model.VehicleName = guid;
            model.PurchaseStatus = 1;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerPurchaseOrderBL.DMLDealerPurchaseOrder(UserManager.UserInfo, model);

            var filter = new DealerSaleSparepartListModel();
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            filter.PartCode = "M.162127";
            filter.MultiLanguageContentAsText = "TR || TEST";

            int count = 0;
            var resultGet = _DealerPurchaseOrderBL.ListDealerOrderPart(UserManager.UserInfo, filter, out count);

            var modelUpdate = new DealerPurchaseOrderViewModel();
            modelUpdate.PurchaseOrderId = resultGet.Data.First().PurchaseOrderId ?? 0;


            modelUpdate.DealerId = UserManager.UserInfo.DealerID;
            modelUpdate.SupplierId = 782;

            modelUpdate.VehicleId = 29627;
            modelUpdate.VehicleName = guid;

            modelUpdate.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";



            modelUpdate.CommandType = "U";
            var resultUpdate = _DealerPurchaseOrderBL.DMLDealerPurchaseOrder(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void DealerPurchaseOrderBL_DMLDealerPurchaseOrder_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerPurchaseOrderViewModel();
            model.PurchaseOrderId = 1;
            model.PoDetSeqNo = 1;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.SupplierId = 782;
            model.IsViaCenter = true;
            model.VehicleId = 29627;
            model.VehicleName = guid;
            model.PurchaseStatus = 1;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerPurchaseOrderBL.DMLDealerPurchaseOrder(UserManager.UserInfo, model);

            var filter = new DealerSaleSparepartListModel();
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            filter.PartCode = "M.162127";
            filter.MultiLanguageContentAsText = "TR || TEST";

            int count = 0;
            var resultGet = _DealerPurchaseOrderBL.ListDealerOrderPart(UserManager.UserInfo, filter, out count);

            var modelDelete = new DealerPurchaseOrderViewModel();
            modelDelete.PurchaseOrderId = resultGet.Data.First().PurchaseOrderId ?? 0;


            modelDelete.DealerId = UserManager.UserInfo.DealerID;
            modelDelete.SupplierId = 782;

            modelDelete.VehicleId = 29627;
            modelDelete.VehicleName = guid;

            modelDelete.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";



            modelDelete.CommandType = "D";
            var resultDelete = _DealerPurchaseOrderBL.DMLDealerPurchaseOrder(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}

