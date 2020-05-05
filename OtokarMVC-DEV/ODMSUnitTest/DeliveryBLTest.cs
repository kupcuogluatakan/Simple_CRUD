using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.Delivery;
using System.Collections.Generic;


namespace ODMSUnitTest
{

    [TestClass]
    public class DeliveryBLTest
    {

        DeliveryBL _DeliveryBL = new DeliveryBL();

        [TestMethod]
        public void DeliveryBL_DMLDelivery_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DeliveryCreateModel();
            model.SupplierName = guid;
            model.SupplierId = 782;
            model.WayBillNo = guid;
            model.TotalPrice = 1;
            model.SapDeliveryNo = guid;
            model.DeliveryId = 1;
            model.PurchaseNo = guid;
            model.InvoiceSerialNo = guid;
            model.InvoiceNo = guid;
            model.HasDeleteItem = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DeliveryBL.DMLDelivery(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void DeliveryBL_DMLDeliveryAndDetails_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DeliveryCreateModel();
            model.SupplierName = guid;
            model.SupplierId = 782;
            model.WayBillNo = guid;
            model.TotalPrice = 1;
            model.SapDeliveryNo = guid;
            model.DeliveryId = 1;
            model.PurchaseNo = guid;
            model.InvoiceSerialNo = guid;
            model.InvoiceNo = guid;
            model.HasDeleteItem = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DeliveryBL.DMLDeliveryAndDetails(UserManager.UserInfo, model, new List<ODMSModel.DeliveryListPart.DeliveryListPartSubViewModel>());

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void DeliveryBL_GetDelivery_GetModel()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DeliveryCreateModel();
            model.SupplierName = guid;
            model.SupplierId = 782;
            model.WayBillNo = guid;
            model.TotalPrice = 1;
            model.SapDeliveryNo = guid;
            model.DeliveryId = 1;
            model.PurchaseNo = guid;
            model.InvoiceSerialNo = guid;
            model.InvoiceNo = guid;
            model.HasDeleteItem = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DeliveryBL.DMLDeliveryAndDetails(UserManager.UserInfo, model, new List<ODMSModel.DeliveryListPart.DeliveryListPartSubViewModel>());


            var resultGet = _DeliveryBL.GetDelivery(UserManager.UserInfo, result.Model.DeliveryId);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.DealerName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void DeliveryBL_ListDelivery_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DeliveryCreateModel();
            model.SupplierName = guid;
            model.SupplierId = 782;
            model.WayBillNo = guid;
            model.TotalPrice = 1;
            model.SapDeliveryNo = guid;
            model.DeliveryId = 1;
            model.PurchaseNo = guid;
            model.InvoiceSerialNo = guid;
            model.InvoiceNo = guid;
            model.HasDeleteItem = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DeliveryBL.DMLDeliveryAndDetails(UserManager.UserInfo, model, new List<ODMSModel.DeliveryListPart.DeliveryListPartSubViewModel>());

            int count = 0;
            var filter = new DeliveryListModel();
            filter.SupplierId = 782;

            var resultGet = _DeliveryBL.ListDelivery(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void DeliveryBL_DMLDeliveryAndDetails_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DeliveryCreateModel();
            model.SupplierName = guid;
            model.SupplierId = 782;
            model.WayBillNo = guid;
            model.TotalPrice = 1;
            model.SapDeliveryNo = guid;
            model.DeliveryId = 1;
            model.PurchaseNo = guid;
            model.InvoiceSerialNo = guid;
            model.InvoiceNo = guid;
            model.HasDeleteItem = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DeliveryBL.DMLDeliveryAndDetails(UserManager.UserInfo, model, new List<ODMSModel.DeliveryListPart.DeliveryListPartSubViewModel>());

            var filter = new DeliveryListModel();
            filter.SupplierId = 782;

            int count = 0;
            var resultGet = _DeliveryBL.ListDelivery(UserManager.UserInfo, filter, out count);

            var modelUpdate = new DeliveryCreateModel();
            modelUpdate.DeliveryId = resultGet.Data.First().DeliveryId;
            modelUpdate.SupplierName = guid;
            modelUpdate.SupplierId = 782;
            modelUpdate.WayBillNo = guid;
            modelUpdate.SapDeliveryNo = guid;
            modelUpdate.PurchaseNo = guid;
            modelUpdate.InvoiceSerialNo = guid;
            modelUpdate.InvoiceNo = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _DeliveryBL.DMLDeliveryAndDetails(UserManager.UserInfo, modelUpdate, new List<ODMSModel.DeliveryListPart.DeliveryListPartSubViewModel>());
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void DeliveryBL_DMLDeliveryAndDetails_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DeliveryCreateModel();
            model.SupplierName = guid;
            model.SupplierId = 782;
            model.WayBillNo = guid;
            model.TotalPrice = 1;
            model.SapDeliveryNo = guid;
            model.DeliveryId = 1;
            model.PurchaseNo = guid;
            model.InvoiceSerialNo = guid;
            model.InvoiceNo = guid;
            model.HasDeleteItem = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DeliveryBL.DMLDeliveryAndDetails(UserManager.UserInfo, model, new List<ODMSModel.DeliveryListPart.DeliveryListPartSubViewModel>());

            var filter = new DeliveryListModel();
            filter.SupplierId = 782;

            int count = 0;
            var resultGet = _DeliveryBL.ListDelivery(UserManager.UserInfo, filter, out count);

            var modelDelete = new DeliveryCreateModel();
            modelDelete.DeliveryId = resultGet.Data.First().DeliveryId;
            modelDelete.SupplierName = guid;
            modelDelete.SupplierId = 782;
            modelDelete.WayBillNo = guid;
            modelDelete.SapDeliveryNo = guid;
            modelDelete.PurchaseNo = guid;
            modelDelete.InvoiceSerialNo = guid;
            modelDelete.InvoiceNo = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _DeliveryBL.DMLDeliveryAndDetails(UserManager.UserInfo, modelDelete, new List<ODMSModel.DeliveryListPart.DeliveryListPartSubViewModel>());
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}

