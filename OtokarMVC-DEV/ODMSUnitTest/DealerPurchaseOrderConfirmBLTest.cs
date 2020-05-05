using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.DealerPurchaseOrderConfirm;
using ODMSModel.SparePartSale;
using System.Collections.Generic;


namespace ODMSUnitTest
{

    [TestClass]
    public class DealerPurchaseOrderConfirmBLTest
    {

        DealerPurchaseOrderConfirmBL _DealerPurchaseOrderConfirmBL = new DealerPurchaseOrderConfirmBL();

        [TestMethod]
        public void DealerPurchaseOrderConfirmBL_DMLDealerPurchaseOrderConfirm_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerPurchaseOrderConfirmViewModel();
            model.StatusName = guid;
            model.SupplierDealerConfirm = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerPurchaseOrderConfirmBL.DMLDealerPurchaseOrderConfirm(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void DealerPurchaseOrderConfirmBL_DMLDealerPurchaseOrderConfirmSave_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new SparePartSaleViewModel();
            model.SparePartSaleId = 1;
            model.CustomerTypeId = 2;
            model.CustomerType = guid;
            model.CurrencyCode = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.BillingAddress = guid;
            model.ShippingAddress = guid;
            model.SaleResponsible = guid;
            model.WayBillSerialNo = guid;
            model.InvoiceNo = guid;
            model.WaybillNo = guid;
            model.PaymentType = guid;
            model.Bank = guid;
            model.IsPrintActualDispatchDate = true;
            model.TransmitNo = guid;
            model.SaleStatusLookKey = 1;
            model.SaleStatusLookVal = guid;
            model.CustomerId = 1;
            model.CustomerName = guid;
            model.InvoiceSerialNo = guid;
            model.PartCode = "M.162127";
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.IsTenderSaleName = guid;
            model.SaleTypeName = guid;
            model.IsReturnName = guid;
            model.VehicleId = 29627;
            model.VehicleName = guid;
            model.StockTypeName = guid;
            model.PriceListId = 14;
            model.IsCustomerDealer = true;
            model.BankRequired = true;
            model.InstalmentNumberRequired = true;
            model.DefermentNumberRequired = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerPurchaseOrderConfirmBL.DMLDealerPurchaseOrderConfirmSave(UserManager.UserInfo, model, new List<ODMSModel.SparePartSaleDetail.SparePartSaleDetailDetailModel>());

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void DealerPurchaseOrderConfirmBL_ListDealerPurchaseOrderConfirm_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new SparePartSaleViewModel();
            model.SparePartSaleId = 1;
            model.CustomerTypeId = 2;
            model.CustomerType = guid;
            model.CurrencyCode = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.BillingAddress = guid;
            model.ShippingAddress = guid;
            model.SaleResponsible = guid;
            model.WayBillSerialNo = guid;
            model.InvoiceNo = guid;
            model.WaybillNo = guid;
            model.PaymentType = guid;
            model.Bank = guid;
            model.IsPrintActualDispatchDate = true;
            model.TransmitNo = guid;
            model.SaleStatusLookKey = 1;
            model.SaleStatusLookVal = guid;
            model.CustomerId = 1;
            model.CustomerName = guid;
            model.InvoiceSerialNo = guid;
            model.PartCode = "M.162127";
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.IsTenderSaleName = guid;
            model.SaleTypeName = guid;
            model.IsReturnName = guid;
            model.VehicleId = 29627;
            model.VehicleName = guid;
            model.StockTypeName = guid;
            model.PriceListId = 14;
            model.IsCustomerDealer = true;
            model.BankRequired = true;
            model.InstalmentNumberRequired = true;
            model.DefermentNumberRequired = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerPurchaseOrderConfirmBL.DMLDealerPurchaseOrderConfirmSave(UserManager.UserInfo, model, new List<ODMSModel.SparePartSaleDetail.SparePartSaleDetailDetailModel>());

            int count = 0;
            var filter = new DealerPurchaseOrderConfirmListModel();
            filter.SupplierDealerConfirm = 1;

            var resultGet = _DealerPurchaseOrderConfirmBL.ListDealerPurchaseOrderConfirm(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void DealerPurchaseOrderConfirmBL_DMLDealerPurchaseOrderConfirmSave_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new SparePartSaleViewModel();
            model.SparePartSaleId = 1;
            model.CustomerTypeId = 2;
            model.CustomerType = guid;
            model.CurrencyCode = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.BillingAddress = guid;
            model.ShippingAddress = guid;
            model.SaleResponsible = guid;
            model.WayBillSerialNo = guid;
            model.InvoiceNo = guid;
            model.WaybillNo = guid;
            model.PaymentType = guid;
            model.Bank = guid;
            model.IsPrintActualDispatchDate = true;
            model.TransmitNo = guid;
            model.SaleStatusLookKey = 1;
            model.SaleStatusLookVal = guid;
            model.CustomerId = 1;
            model.CustomerName = guid;
            model.InvoiceSerialNo = guid;
            model.PartCode = "M.162127";
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.IsTenderSaleName = guid;
            model.SaleTypeName = guid;
            model.IsReturnName = guid;
            model.VehicleId = 29627;
            model.VehicleName = guid;
            model.StockTypeName = guid;
            model.PriceListId = 14;
            model.IsCustomerDealer = true;
            model.BankRequired = true;
            model.InstalmentNumberRequired = true;
            model.DefermentNumberRequired = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerPurchaseOrderConfirmBL.DMLDealerPurchaseOrderConfirmSave(UserManager.UserInfo, model, new List<ODMSModel.SparePartSaleDetail.SparePartSaleDetailDetailModel>());

            var filter = new DealerPurchaseOrderConfirmListModel();
            filter.SupplierDealerConfirm = 1;

            int count = 0;
            var resultGet = _DealerPurchaseOrderConfirmBL.ListDealerPurchaseOrderConfirm(UserManager.UserInfo, filter, out count);

            var modelUpdate = new SparePartSaleViewModel();
            modelUpdate.PoNumber = resultGet.Data.First().PoNumber;

            modelUpdate.CustomerTypeId = 2;
            modelUpdate.CustomerType = guid;
            modelUpdate.CurrencyCode = guid;
            modelUpdate.DealerId = UserManager.UserInfo.DealerID;
            modelUpdate.DealerName = guid;
            modelUpdate.BillingAddress = guid;
            modelUpdate.ShippingAddress = guid;
            modelUpdate.SaleResponsible = guid;
            modelUpdate.WayBillSerialNo = guid;
            modelUpdate.InvoiceNo = guid;
            modelUpdate.WaybillNo = guid;
            modelUpdate.PaymentType = guid;
            modelUpdate.Bank = guid;

            modelUpdate.TransmitNo = guid;

            modelUpdate.SaleStatusLookVal = guid;

            modelUpdate.CustomerName = guid;
            modelUpdate.InvoiceSerialNo = guid;
            modelUpdate.PartCode = "M.162127";
            modelUpdate.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            modelUpdate.IsTenderSaleName = guid;
            modelUpdate.SaleTypeName = guid;
            modelUpdate.IsReturnName = guid;
            modelUpdate.VehicleId = 29627;
            modelUpdate.VehicleName = guid;
            modelUpdate.StockTypeName = guid;
            modelUpdate.PriceListId = 14;
            modelUpdate.CommandType = "U";
            var resultUpdate = _DealerPurchaseOrderConfirmBL.DMLDealerPurchaseOrderConfirmSave(UserManager.UserInfo, modelUpdate, new List<ODMSModel.SparePartSaleDetail.SparePartSaleDetailDetailModel>());
            Assert.IsTrue(resultUpdate.Model > 0 && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void DealerPurchaseOrderConfirmBL_DMLDealerPurchaseOrderConfirmSave_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new SparePartSaleViewModel();
            model.SparePartSaleId = 1;
            model.CustomerTypeId = 2;
            model.CustomerType = guid;
            model.CurrencyCode = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.BillingAddress = guid;
            model.ShippingAddress = guid;
            model.SaleResponsible = guid;
            model.WayBillSerialNo = guid;
            model.InvoiceNo = guid;
            model.WaybillNo = guid;
            model.PaymentType = guid;
            model.Bank = guid;
            model.IsPrintActualDispatchDate = true;
            model.TransmitNo = guid;
            model.SaleStatusLookKey = 1;
            model.SaleStatusLookVal = guid;
            model.CustomerId = 1;
            model.CustomerName = guid;
            model.InvoiceSerialNo = guid;
            model.PartCode = "M.162127";
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.IsTenderSaleName = guid;
            model.SaleTypeName = guid;
            model.IsReturnName = guid;
            model.VehicleId = 29627;
            model.VehicleName = guid;
            model.StockTypeName = guid;
            model.PriceListId = 14;
            model.IsCustomerDealer = true;
            model.BankRequired = true;
            model.InstalmentNumberRequired = true;
            model.DefermentNumberRequired = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerPurchaseOrderConfirmBL.DMLDealerPurchaseOrderConfirmSave(UserManager.UserInfo, model, new List<ODMSModel.SparePartSaleDetail.SparePartSaleDetailDetailModel>());

            var filter = new DealerPurchaseOrderConfirmListModel();
            filter.SupplierDealerConfirm = 1;

            int count = 0;
            var resultGet = _DealerPurchaseOrderConfirmBL.ListDealerPurchaseOrderConfirm(UserManager.UserInfo, filter, out count);

            var modelDelete = new SparePartSaleViewModel();
            modelDelete.PoNumber = resultGet.Data.First().PoNumber;

            modelDelete.CustomerTypeId = 2;
            modelDelete.CustomerType = guid;
            modelDelete.CurrencyCode = guid;
            modelDelete.DealerId = UserManager.UserInfo.DealerID;
            modelDelete.DealerName = guid;
            modelDelete.BillingAddress = guid;
            modelDelete.ShippingAddress = guid;
            modelDelete.SaleResponsible = guid;
            modelDelete.WayBillSerialNo = guid;
            modelDelete.InvoiceNo = guid;
            modelDelete.WaybillNo = guid;
            modelDelete.PaymentType = guid;
            modelDelete.Bank = guid;

            modelDelete.TransmitNo = guid;

            modelDelete.SaleStatusLookVal = guid;

            modelDelete.CustomerName = guid;
            modelDelete.InvoiceSerialNo = guid;
            modelDelete.PartCode = "M.162127";
            modelDelete.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            modelDelete.IsTenderSaleName = guid;
            modelDelete.SaleTypeName = guid;
            modelDelete.IsReturnName = guid;
            modelDelete.VehicleId = 29627;
            modelDelete.VehicleName = guid;
            modelDelete.StockTypeName = guid;
            modelDelete.PriceListId = 14;
            modelDelete.CommandType = "D";
            var resultDelete = _DealerPurchaseOrderConfirmBL.DMLDealerPurchaseOrderConfirmSave(UserManager.UserInfo, modelDelete, new List<ODMSModel.SparePartSaleDetail.SparePartSaleDetailDetailModel>());
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}

