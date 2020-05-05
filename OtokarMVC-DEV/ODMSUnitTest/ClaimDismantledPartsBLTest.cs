using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.ClaimDismantledParts;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class ClaimDismantledPartsBLTest
    {

        ClaimDismantledPartsBL _ClaimDismantledPartsBL = new ClaimDismantledPartsBL();

        [TestMethod]
        public void ClaimDismantledPartsBL_DMLClaimDismantledParts_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new ClaimDismantledPartsViewModel();
            model.ClaimDismantledPartId = 1;
            model.ClaimWaybillId = 1;
            model.PartId = 1;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.DealerId = UserManager.UserInfo.DealerID;
            model.WorkOrderDetailId = 1;
            model.FirmActionDate = DateTime.Now;
            model.ClaimRecallPeriodId = 1;
            model.SupplierCode = guid;
            model.GuaranteeId = 1;
            model.GuaranteeSeq = 1;
            model.BarcodeFirstPrintDate = DateTime.Now;
            model.DealerScrapDate = DateTime.Now;
            model.IsApproved = true;
            model.DismantledPartId = 1;
            model.DismantledPartName = guid;
            model.Quantity = 1;
            model.DismantledPartSerialNo = guid;
            model.Barcode = guid;
            model.CreateDate = DateTime.Now;
            model.FirmActionId = 1;
            model.FirmActionName = guid;
            model.FirmActionExplanation = guid;
            model.RackCode = guid;
            model.IdList = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _ClaimDismantledPartsBL.DMLClaimDismantledParts(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void ClaimDismantledPartsBL_DMLClaimWaybill_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new ClaimWaybillViewModel();
            model.ClaimWaybillId = 1;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.WaybillText = guid;
            model.WaybillSerialNo = guid;
            model.WaybillNo = guid;
            model.WaybillDate = DateTime.Now;
            model.AcceptUser = guid;
            model.AcceptDate = DateTime.Now;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _ClaimDismantledPartsBL.DMLClaimWaybill(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }


        [TestMethod]
        public void ClaimDismantledPartsBL_GetClaimDismantledParts_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new ClaimDismantledPartsViewModel();
            model.ClaimDismantledPartId = 1;
            model.ClaimWaybillId = 1;
            model.PartId = 1;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.DealerId = UserManager.UserInfo.DealerID;
            model.WorkOrderDetailId = 1;
            model.FirmActionDate = DateTime.Now;
            model.ClaimRecallPeriodId = 1;
            model.SupplierCode = guid;
            model.GuaranteeId = 1;
            model.GuaranteeSeq = 1;
            model.BarcodeFirstPrintDate = DateTime.Now;
            model.DealerScrapDate = DateTime.Now;
            model.IsApproved = true;
            model.DismantledPartId = 1;
            model.DismantledPartName = guid;
            model.Quantity = 1;
            model.DismantledPartSerialNo = guid;
            model.Barcode = guid;
            model.CreateDate = DateTime.Now;
            model.FirmActionId = 1;
            model.FirmActionName = guid;
            model.FirmActionExplanation = guid;
            model.RackCode = guid;
            model.IdList = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _ClaimDismantledPartsBL.UpdateClaimDismantledParts(UserManager.UserInfo, model);

            var filter = new ClaimDismantledPartsViewModel();
            filter.ClaimDismantledPartId = result.Model.ClaimDismantledPartId;
            filter.PartId = 39399;
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            filter.DealerId = UserManager.UserInfo.DealerID;
            filter.SupplierCode = guid;
            filter.RackCode = guid;

            var resultGet = _ClaimDismantledPartsBL.GetClaimDismantledParts(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Quantity > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void ClaimDismantledPartsBL_GetClaimWaybill_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new ClaimDismantledPartsViewModel();
            model.ClaimDismantledPartId = 1;
            model.ClaimWaybillId = 1;
            model.PartId = 1;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.DealerId = UserManager.UserInfo.DealerID;
            model.WorkOrderDetailId = 1;
            model.FirmActionDate = DateTime.Now;
            model.ClaimRecallPeriodId = 1;
            model.SupplierCode = guid;
            model.GuaranteeId = 1;
            model.GuaranteeSeq = 1;
            model.BarcodeFirstPrintDate = DateTime.Now;
            model.DealerScrapDate = DateTime.Now;
            model.IsApproved = true;
            model.DismantledPartId = 1;
            model.DismantledPartName = guid;
            model.Quantity = 1;
            model.DismantledPartSerialNo = guid;
            model.Barcode = guid;
            model.CreateDate = DateTime.Now;
            model.FirmActionId = 1;
            model.FirmActionName = guid;
            model.FirmActionExplanation = guid;
            model.RackCode = guid;
            model.IdList = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _ClaimDismantledPartsBL.UpdateClaimDismantledParts(UserManager.UserInfo, model);

            var filter = new ClaimWaybillViewModel();
            filter.ClaimWaybillId = result.Model.ClaimWaybillId;
            filter.DealerId = UserManager.UserInfo.DealerID;

            var resultGet = _ClaimDismantledPartsBL.GetClaimWaybill(filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.WaybillNo != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void ClaimDismantledPartsBL_ListClaimDismantledPartss_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new ClaimDismantledPartsViewModel();
            model.ClaimDismantledPartId = 1;
            model.ClaimWaybillId = 1;
            model.PartId = 1;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.DealerId = UserManager.UserInfo.DealerID;
            model.WorkOrderDetailId = 1;
            model.FirmActionDate = DateTime.Now;
            model.ClaimRecallPeriodId = 1;
            model.SupplierCode = guid;
            model.GuaranteeId = 1;
            model.GuaranteeSeq = 1;
            model.BarcodeFirstPrintDate = DateTime.Now;
            model.DealerScrapDate = DateTime.Now;
            model.IsApproved = true;
            model.DismantledPartId = 1;
            model.DismantledPartName = guid;
            model.Quantity = 1;
            model.DismantledPartSerialNo = guid;
            model.Barcode = guid;
            model.CreateDate = DateTime.Now;
            model.FirmActionId = 1;
            model.FirmActionName = guid;
            model.FirmActionExplanation = guid;
            model.RackCode = guid;
            model.IdList = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _ClaimDismantledPartsBL.UpdateClaimDismantledParts(UserManager.UserInfo, model);

            int count = 0;
            var filter = new ClaimDismantledPartsListModel();
            filter.PartId = 39399;
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            filter.RackCode = guid;

            var resultGet = _ClaimDismantledPartsBL.ListClaimDismantledPartss(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }
        

    }

}

