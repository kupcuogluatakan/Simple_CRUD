using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.PurchaseOrder;
using System.Collections.Generic;
using ODMSModel.DealerPurchaseOrderPartConfirm;


namespace ODMSUnitTest
{

    [TestClass]
    public class DealerPurchaseOrderPartConfirmBLTest
    {

        DealerPurchaseOrderPartConfirmBL _DealerPurchaseOrderPartConfirmBL = new DealerPurchaseOrderPartConfirmBL();

        [TestMethod]
        public void DealerPurchaseOrderPartConfirmBL_DMLDealerPurchaseOrderPartConfirm_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new PurchaseOrderViewModel();
            model.Location = guid;
            model.PoTypeName = guid;
            model.StatusName = guid;
            model.IsProposal = true;
            model.IsProposalName = guid;
            model.DealerName = guid;
            model.SupplierName = guid;
            model.IsPriceFixed = true;
            model.StockTypeName = guid;
            model.VehiclePlateVinNo = guid;
            model.SupplyTypeName = guid;
            model.VehicleId = 29627;
            model.SalesOrganization = guid;
            model.ProposalType = guid;
            model.DeliveryPriority = 1;
            model.OrderReason = guid;
            model.ItemCategory = guid;
            model.DistrChan = guid;
            model.Division = guid;
            model.StatusDetail = 1;
            model.IsBranchOrder = true;
            model.DealerBranchSSID = guid;
            model.Description = guid;
            model.DealerSSID = guid;
            model.BranchSSID = guid;
            model.AllDetParts = guid;
            model.CreateDate = DateTime.Now;
            model.OrderNo = 1;
            model.SupplierDealerConfirm = 1;
            model.ModelKod = "ATLAS";
            model.CurrencyCode = guid;
            model.VinNo = guid;
            model.UpdateUser = guid;
            model.UpdateUser = "1";
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerPurchaseOrderPartConfirmBL.DMLDealerPurchaseOrderPartConfirm(UserManager.UserInfo, model, new List<ODMSModel.PurchaseOrderDetail.PurchaseOrderDetailViewModel>());

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void DealerPurchaseOrderPartConfirmBL_ListDealerPurchaseOrderPartConfirms_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new PurchaseOrderViewModel();
            model.Location = guid;
            model.PoTypeName = guid;
            model.StatusName = guid;
            model.IsProposal = true;
            model.IsProposalName = guid;
            model.DealerName = guid;
            model.SupplierName = guid;
            model.IsPriceFixed = true;
            model.StockTypeName = guid;
            model.VehiclePlateVinNo = guid;
            model.SupplyTypeName = guid;
            model.VehicleId = 29627;
            model.SalesOrganization = guid;
            model.ProposalType = guid;
            model.DeliveryPriority = 1;
            model.OrderReason = guid;
            model.ItemCategory = guid;
            model.DistrChan = guid;
            model.Division = guid;
            model.StatusDetail = 1;
            model.IsBranchOrder = true;
            model.DealerBranchSSID = guid;
            model.Description = guid;
            model.DealerSSID = guid;
            model.BranchSSID = guid;
            model.AllDetParts = guid;
            model.CreateDate = DateTime.Now;
            model.OrderNo = 1;
            model.SupplierDealerConfirm = 1;
            model.ModelKod = "ATLAS";
            model.CurrencyCode = guid;
            model.VinNo = guid;
            model.UpdateUser = guid;
            model.UpdateUser = "1";
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerPurchaseOrderPartConfirmBL.DMLDealerPurchaseOrderPartConfirm(UserManager.UserInfo, model, new List<ODMSModel.PurchaseOrderDetail.PurchaseOrderDetailViewModel>());

            int count = 0;
            var filter = new DealerPurchaseOrderPartConfirmListModel();
            filter.PartId = 39399;
            filter.PartCodeName = guid;
            filter.DealerId = UserManager.UserInfo.DealerID;
            filter.SupplierDealerConfirm = 1;

            var resultGet = _DealerPurchaseOrderPartConfirmBL.ListDealerPurchaseOrderPartConfirms(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void DealerPurchaseOrderPartConfirmBL_DMLDealerPurchaseOrderPartConfirm_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new PurchaseOrderViewModel();
            model.Location = guid;
            model.PoTypeName = guid;
            model.StatusName = guid;
            model.IsProposal = true;
            model.IsProposalName = guid;
            model.DealerName = guid;
            model.SupplierName = guid;
            model.IsPriceFixed = true;
            model.StockTypeName = guid;
            model.VehiclePlateVinNo = guid;
            model.SupplyTypeName = guid;
            model.VehicleId = 29627;
            model.SalesOrganization = guid;
            model.ProposalType = guid;
            model.DeliveryPriority = 1;
            model.OrderReason = guid;
            model.ItemCategory = guid;
            model.DistrChan = guid;
            model.Division = guid;
            model.StatusDetail = 1;
            model.IsBranchOrder = true;
            model.DealerBranchSSID = guid;
            model.Description = guid;
            model.DealerSSID = guid;
            model.BranchSSID = guid;
            model.AllDetParts = guid;
            model.CreateDate = DateTime.Now;
            model.OrderNo = 1;
            model.SupplierDealerConfirm = 1;
            model.ModelKod = "ATLAS";
            model.CurrencyCode = guid;
            model.VinNo = guid;
            model.UpdateUser = guid;
            model.UpdateUser = "1";
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerPurchaseOrderPartConfirmBL.DMLDealerPurchaseOrderPartConfirm(UserManager.UserInfo, model, new List<ODMSModel.PurchaseOrderDetail.PurchaseOrderDetailViewModel>());

            var filter = new DealerPurchaseOrderPartConfirmListModel();
            filter.PartId = 39399;
            filter.PartCodeName = guid;
            filter.DealerId = UserManager.UserInfo.DealerID;
            filter.SupplierDealerConfirm = 1;

            int count = 0;
            var resultGet = _DealerPurchaseOrderPartConfirmBL.ListDealerPurchaseOrderPartConfirms(UserManager.UserInfo, filter, out count);

            var modelUpdate = new PurchaseOrderViewModel();
            modelUpdate.PoNumber = resultGet.Data.First().PoNumber;
            modelUpdate.Location = guid;
            modelUpdate.PoTypeName = guid;
            modelUpdate.StatusName = guid;

            modelUpdate.IsProposalName = guid;
            modelUpdate.DealerName = guid;
            modelUpdate.SupplierName = guid;

            modelUpdate.StockTypeName = guid;
            modelUpdate.VehiclePlateVinNo = guid;
            modelUpdate.SupplyTypeName = guid;
            modelUpdate.VehicleId = 29627;
            modelUpdate.SalesOrganization = guid;
            modelUpdate.ProposalType = guid;

            modelUpdate.OrderReason = guid;
            modelUpdate.ItemCategory = guid;
            modelUpdate.DistrChan = guid;
            modelUpdate.Division = guid;


            modelUpdate.DealerBranchSSID = guid;
            modelUpdate.Description = guid;
            modelUpdate.DealerSSID = guid;
            modelUpdate.BranchSSID = guid;
            modelUpdate.AllDetParts = guid;


            modelUpdate.SupplierDealerConfirm = 1;
            modelUpdate.ModelKod = "ATLAS";
            modelUpdate.CurrencyCode = guid;
            modelUpdate.VinNo = guid;
            modelUpdate.UpdateUser = guid;



            modelUpdate.CommandType = "U";
            var resultUpdate = _DealerPurchaseOrderPartConfirmBL.DMLDealerPurchaseOrderPartConfirm(UserManager.UserInfo, modelUpdate, new List<ODMSModel.PurchaseOrderDetail.PurchaseOrderDetailViewModel>());
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void DealerPurchaseOrderPartConfirmBL_DMLDealerPurchaseOrderPartConfirm_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new PurchaseOrderViewModel();
            model.Location = guid;
            model.PoTypeName = guid;
            model.StatusName = guid;
            model.IsProposal = true;
            model.IsProposalName = guid;
            model.DealerName = guid;
            model.SupplierName = guid;
            model.IsPriceFixed = true;
            model.StockTypeName = guid;
            model.VehiclePlateVinNo = guid;
            model.SupplyTypeName = guid;
            model.VehicleId = 29627;
            model.SalesOrganization = guid;
            model.ProposalType = guid;
            model.DeliveryPriority = 1;
            model.OrderReason = guid;
            model.ItemCategory = guid;
            model.DistrChan = guid;
            model.Division = guid;
            model.StatusDetail = 1;
            model.IsBranchOrder = true;
            model.DealerBranchSSID = guid;
            model.Description = guid;
            model.DealerSSID = guid;
            model.BranchSSID = guid;
            model.AllDetParts = guid;
            model.CreateDate = DateTime.Now;
            model.OrderNo = 1;
            model.SupplierDealerConfirm = 1;
            model.ModelKod = "ATLAS";
            model.CurrencyCode = guid;
            model.VinNo = guid;
            model.UpdateUser = guid;
            model.UpdateUser = "1";
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerPurchaseOrderPartConfirmBL.DMLDealerPurchaseOrderPartConfirm(UserManager.UserInfo, model, new List<ODMSModel.PurchaseOrderDetail.PurchaseOrderDetailViewModel>());

            var filter = new DealerPurchaseOrderPartConfirmListModel();
            filter.PartId = 39399;
            filter.PartCodeName = guid;
            filter.DealerId = UserManager.UserInfo.DealerID;
            filter.SupplierDealerConfirm = 1;

            int count = 0;
            var resultGet = _DealerPurchaseOrderPartConfirmBL.ListDealerPurchaseOrderPartConfirms(UserManager.UserInfo, filter, out count);

            var modelDelete = new PurchaseOrderViewModel();
            modelDelete.PoNumber = resultGet.Data.First().PoNumber;
            modelDelete.Location = guid;
            modelDelete.PoTypeName = guid;
            modelDelete.StatusName = guid;

            modelDelete.IsProposalName = guid;
            modelDelete.DealerName = guid;
            modelDelete.SupplierName = guid;

            modelDelete.StockTypeName = guid;
            modelDelete.VehiclePlateVinNo = guid;
            modelDelete.SupplyTypeName = guid;
            modelDelete.VehicleId = 29627;
            modelDelete.SalesOrganization = guid;
            modelDelete.ProposalType = guid;

            modelDelete.OrderReason = guid;
            modelDelete.ItemCategory = guid;
            modelDelete.DistrChan = guid;
            modelDelete.Division = guid;


            modelDelete.DealerBranchSSID = guid;
            modelDelete.Description = guid;
            modelDelete.DealerSSID = guid;
            modelDelete.BranchSSID = guid;
            modelDelete.AllDetParts = guid;


            modelDelete.SupplierDealerConfirm = 1;
            modelDelete.ModelKod = "ATLAS";
            modelDelete.CurrencyCode = guid;
            modelDelete.VinNo = guid;
            modelDelete.UpdateUser = guid;



            modelDelete.CommandType = "D";
            var resultDelete = _DealerPurchaseOrderPartConfirmBL.DMLDealerPurchaseOrderPartConfirm(UserManager.UserInfo, modelDelete, new List<ODMSModel.PurchaseOrderDetail.PurchaseOrderDetailViewModel>());
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}

