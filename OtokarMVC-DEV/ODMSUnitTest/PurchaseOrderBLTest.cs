using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.PurchaseOrder;
using System.Collections.Generic;
using ODMSModel.ServiceCallSchedule;


namespace ODMSUnitTest
{

    [TestClass]
    public class PurchaseOrderBLTest
    {

        PurchaseOrderBL _PurchaseOrderBL = new PurchaseOrderBL();

        [TestMethod]
        public void PurchaseOrderBL_DMLPurchaseOrder_Insert()
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
            model.UpdateUser = UserManager.UserInfo.UserName;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _PurchaseOrderBL.DMLPurchaseOrder(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void PurchaseOrderBL_UpdatePurchaseOrderIsSASNoSentValue_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new ServiceCallLogModel();
            model.LogId = 1;
            model.ServiceName = guid;
            model.LogType = guid;
            model.IsManuel = true;
            model.LogErrorDesc = guid;
            model.IsSuccess = true;
            model.IsSuccess2 = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            List<string> poList = new List<string>();
            var result = _PurchaseOrderBL.UpdatePurchaseOrderIsSASNoSentValue(UserManager.UserInfo, poList, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void PurchaseOrderBL_GetPurchaseOrder_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new ServiceCallLogModel();
            model.LogId = 1;
            model.ServiceName = guid;
            model.LogType = guid;
            model.IsManuel = true;
            model.LogErrorDesc = guid;
            model.IsSuccess = true;
            model.IsSuccess2 = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            List<string> poNumberList = new List<string>();
            var result = _PurchaseOrderBL.UpdatePurchaseOrderIsSASNoSentValue(UserManager.UserInfo, poNumberList, model);

            var filter = new PurchaseOrderViewModel();
            filter.VehicleId = 29627;
            filter.SupplierDealerConfirm = 1;
            filter.ModelKod = "ATLAS";
            filter.CurrencyCode = guid;

            var resultGet = _PurchaseOrderBL.GetPurchaseOrder(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void PurchaseOrderBL_GetPurchaseOrderTypeStockType_GetModel()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var resultGet = _PurchaseOrderBL.GetPurchaseOrderTypeStockType(UserManager.UserInfo, guid);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Id > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void PurchaseOrderBL_GetOfferNo_GetModel()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var resultGet = _PurchaseOrderBL.GetOfferNo(guid);

            Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void PurchaseOrderBL_GetPurchaseOrderSAPOfferNo_GetModel()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var resultGet = _PurchaseOrderBL.GetPurchaseOrderSAPOfferNo(UserManager.UserInfo, guid);
            Assert.IsTrue(resultGet.Model != null && resultGet.Total > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void PurchaseOrderBL_ListPurchaseOrder_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new ServiceCallLogModel();
            model.LogId = 1;
            model.ServiceName = guid;
            model.LogType = guid;
            model.IsManuel = true;
            model.LogErrorDesc = guid;
            model.IsSuccess = true;
            model.IsSuccess2 = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            List<string> poNumberList=new List<string>();
            var result = _PurchaseOrderBL.UpdatePurchaseOrderIsSASNoSentValue(UserManager.UserInfo, poNumberList, model);

            int count = 0;
            var filter = new PurchaseOrderListModel();
            filter.SupplierDealerConfirm = 1;
            filter.PartCode = "M.162127";
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            filter.CampaignCode = "508";

            var resultGet = _PurchaseOrderBL.ListPurchaseOrder(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void PurchaseOrderBL_UpdatePurchaseOrderIsSASNoSentValue_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new ServiceCallLogModel();
            model.LogId = 1;
            model.ServiceName = guid;
            model.LogType = guid;
            model.IsManuel = true;
            model.LogErrorDesc = guid;
            model.IsSuccess = true;
            model.IsSuccess2 = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            List<string> poNumberList = new List<string>();
            var result = _PurchaseOrderBL.UpdatePurchaseOrderIsSASNoSentValue(UserManager.UserInfo, poNumberList, model);

            var filter = new PurchaseOrderListModel();
            filter.SupplierDealerConfirm = 1;
            filter.PartCode = "M.162127";
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            filter.CampaignCode = "508";

            int count = 0;
            var resultGet = _PurchaseOrderBL.ListPurchaseOrder(UserManager.UserInfo, filter, out count);

            var modelUpdate = new ServiceCallLogModel();
            modelUpdate.ServiceName = guid;
            modelUpdate.LogType = guid;
            modelUpdate.LogErrorDesc = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _PurchaseOrderBL.UpdatePurchaseOrderIsSASNoSentValue(UserManager.UserInfo, poNumberList, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void PurchaseOrderBL_UpdatePurchaseOrderIsSASNoSentValue_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new ServiceCallLogModel();
            model.LogId = 1;
            model.ServiceName = guid;
            model.LogType = guid;
            model.IsManuel = true;
            model.LogErrorDesc = guid;
            model.IsSuccess = true;
            model.IsSuccess2 = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            List<string> poNumberList = new List<string>();
            var result = _PurchaseOrderBL.UpdatePurchaseOrderIsSASNoSentValue(UserManager.UserInfo, poNumberList, model);

            var filter = new PurchaseOrderListModel();
            filter.SupplierDealerConfirm = 1;
            filter.PartCode = "M.162127";
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            filter.CampaignCode = "508";

            int count = 0;
            var resultGet = _PurchaseOrderBL.ListPurchaseOrder(UserManager.UserInfo, filter, out count);

            var modelDelete = new ServiceCallLogModel();
            model.LogId = 1;
            modelDelete.ServiceName = guid;
            modelDelete.LogType = guid;
            modelDelete.IsManuel = true;
            modelDelete.LogErrorDesc = guid;
            modelDelete.IsSuccess = true;
            modelDelete.IsSuccess2 = true;
            modelDelete.UpdateUser = 1;
            modelDelete.UpdateDate = DateTime.Now;
            modelDelete.IsActive = true;
            modelDelete.ServiceName = guid;
            modelDelete.LogType = guid;
            modelDelete.LogErrorDesc = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _PurchaseOrderBL.UpdatePurchaseOrderIsSASNoSentValue(UserManager.UserInfo,poNumberList, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}

