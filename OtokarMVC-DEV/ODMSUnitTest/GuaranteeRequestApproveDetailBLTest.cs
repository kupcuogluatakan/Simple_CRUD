using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.GuaranteeRequestApproveDetail;


namespace ODMSUnitTest
{

    [TestClass]
    public class GuaranteeRequestApproveDetailBLTest
    {

        GuaranteeRequestApproveDetailBL _GuaranteeRequestApproveDetailBL = new GuaranteeRequestApproveDetailBL();


        [TestMethod]
        public void GuaranteeRequestApproveDetailBL_DMLSaveGuaranteeParts_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GuaranteePartsLabourViewModel();
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _GuaranteeRequestApproveDetailBL.DMLSaveGuaranteeParts(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void GuaranteeRequestApproveDetailBL_DMLSaveGuaranteeLabour_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GuaranteePartsLabourViewModel();
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _GuaranteeRequestApproveDetailBL.DMLSaveGuaranteeLabour(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void GuaranteeRequestApproveDetailBL_GuaranteeUpdateDescription_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GRADMstViewModel();
            model.GuaranteeId = 1;
            model.GuaranteeSeq = 1;
            model.WorkOrderId = 1;
            model.WarrantyStatus = 1;
            model.IsEditable = true;
            model.HasPdiVehicle = 1;
            model.IsInvoiced = true;
            model.ConfirmDesc = guid;
            model.CategoryName = guid;
            model.IsShowCategory = true;
            model.CurrencySymbol = guid;
            model.GuaranteeDealer = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.GuaranteeCustomer = guid;
            model.TechnicalDesc = guid;
            model.CustomerDesc = guid;
            model.VehicleId = "29627";
            model.VehicleNoteCount = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _GuaranteeRequestApproveDetailBL.GuaranteeUpdateDescription(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }


        [TestMethod]
        public void GuaranteeRequestApproveDetailBL_GetPartInfos_GetModel()
        {
            var resultGet = _GuaranteeRequestApproveDetailBL.GetPartInfos(39399);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.PartName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void GuaranteeRequestApproveDetailBL_ListGuaranteeParts_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GRADMstViewModel();
            model.GuaranteeId = 1;
            model.GuaranteeSeq = 1;
            model.WorkOrderId = 1;
            model.WarrantyStatus = 1;
            model.IsEditable = true;
            model.HasPdiVehicle = 1;
            model.IsInvoiced = true;
            model.ConfirmDesc = guid;
            model.CategoryName = guid;
            model.IsShowCategory = true;
            model.CurrencySymbol = guid;
            model.GuaranteeDealer = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.GuaranteeCustomer = guid;
            model.TechnicalDesc = guid;
            model.CustomerDesc = guid;
            model.VehicleId = "29627";
            model.VehicleNoteCount = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _GuaranteeRequestApproveDetailBL.GuaranteeUpdateDescription(UserManager.UserInfo, model);

            int count = 0;
            var filter = new GuaranteePartsListModel();
            filter.PartId = 39399;
            filter.PartCodeName = guid;

            var resultGet = _GuaranteeRequestApproveDetailBL.ListGuaranteeParts(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void GuaranteeRequestApproveDetailBL_ListGuaranteeLabours_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GRADMstViewModel();
            model.GuaranteeId = 1;
            model.GuaranteeSeq = 1;
            model.WorkOrderId = 1;
            model.WarrantyStatus = 1;
            model.IsEditable = true;
            model.HasPdiVehicle = 1;
            model.IsInvoiced = true;
            model.ConfirmDesc = guid;
            model.CategoryName = guid;
            model.IsShowCategory = true;
            model.CurrencySymbol = guid;
            model.GuaranteeDealer = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.GuaranteeCustomer = guid;
            model.TechnicalDesc = guid;
            model.CustomerDesc = guid;
            model.VehicleId = "29627";
            model.VehicleNoteCount = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _GuaranteeRequestApproveDetailBL.GuaranteeUpdateDescription(UserManager.UserInfo, model);

            int count = 0;
            var filter = new GuaranteeLaboursListModel();

            var resultGet = _GuaranteeRequestApproveDetailBL.ListGuaranteeLabours(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }


        [TestMethod]
        public void GuaranteeRequestApproveDetailBL_ListGuaranteeDescriptionHistory_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GRADMstViewModel();
            model.GuaranteeId = 1;
            model.GuaranteeSeq = 1;
            model.WorkOrderId = 1;
            model.WarrantyStatus = 1;
            model.IsEditable = true;
            model.HasPdiVehicle = 1;
            model.IsInvoiced = true;
            model.ConfirmDesc = guid;
            model.CategoryName = guid;
            model.IsShowCategory = true;
            model.CurrencySymbol = guid;
            model.GuaranteeDealer = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.GuaranteeCustomer = guid;
            model.TechnicalDesc = guid;
            model.CustomerDesc = guid;
            model.VehicleId = "29627";
            model.VehicleNoteCount = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _GuaranteeRequestApproveDetailBL.GuaranteeUpdateDescription(UserManager.UserInfo, model);

            int count = 0;
            var filter = new GuaranteeDescriptionHistoryListModel();

            var resultGet = _GuaranteeRequestApproveDetailBL.ListGuaranteeDescriptionHistory(filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void GuaranteeRequestApproveDetailBL_ListRemovalPart_GetAll()
        {
            var resultGet = _GuaranteeRequestApproveDetailBL.ListRemovalPart(UserManager.UserInfo, 39399);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void GuaranteeRequestApproveDetailBL_ListGRADGifHistory_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GRADMstViewModel();
            model.GuaranteeId = 1;
            model.GuaranteeSeq = 1;
            model.WorkOrderId = 1;
            model.WarrantyStatus = 1;
            model.IsEditable = true;
            model.HasPdiVehicle = 1;
            model.IsInvoiced = true;
            model.ConfirmDesc = guid;
            model.CategoryName = guid;
            model.IsShowCategory = true;
            model.CurrencySymbol = guid;
            model.GuaranteeDealer = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.GuaranteeCustomer = guid;
            model.TechnicalDesc = guid;
            model.CustomerDesc = guid;
            model.VehicleId = "29627";
            model.VehicleNoteCount = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _GuaranteeRequestApproveDetailBL.GuaranteeUpdateDescription(UserManager.UserInfo, model);

            int count = 0;
            var filter = new GRADGifHistoryModel();
            filter.IndicatorCode = guid;
            filter.ProcessCode = guid;
            filter.ApprovedUserCode = guid;

            var resultGet = _GuaranteeRequestApproveDetailBL.ListGRADGifHistory(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }


        [TestMethod]
        public void GuaranteeRequestApproveDetailBL_ListGRADGifHistoryDet_GetAll()
        {
            var count = 0;
            var resultGet = _GuaranteeRequestApproveDetailBL.ListGRADGifHistoryDet(UserManager.UserInfo, 1, out count);
            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

