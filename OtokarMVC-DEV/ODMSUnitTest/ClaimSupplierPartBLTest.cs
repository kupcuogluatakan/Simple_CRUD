using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.ClaimSupplierPart;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class ClaimSupplierPartBLTest
    {

        ClaimSupplierPartBL _ClaimSupplierPartBL = new ClaimSupplierPartBL();

        [TestMethod]
        public void ClaimSupplierPartBL_DMLClaimSupplierPart_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new ClaimSupplierPartViewModel();
            model.ClaimRecallPeriodId = 1;
            model.SupplierCode = guid;
            model.SupplierName = guid;
            model.PartId = 39399;
            model.PartCode = "M.162127";
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _ClaimSupplierPartBL.DMLClaimSupplierPart(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void ClaimSupplierPartBL_GetClaimSupplierPart_GetModel()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new ClaimSupplierPartViewModel();
            model.ClaimRecallPeriodId = 1;
            model.SupplierCode = guid;
            model.SupplierName = guid;
            model.PartId = 39399;
            model.PartCode = "M.162127";
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _ClaimSupplierPartBL.DMLClaimSupplierPart(UserManager.UserInfo, model);

            var resultGet = _ClaimSupplierPartBL.GetClaimSupplierPart(UserManager.UserInfo, 39399, guid);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.PartName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void ClaimSupplierPartBL_ListSupplierCodesAsSelectListItem_GetAll()
        {
            var resultGet = ClaimSupplierPartBL.ListSupplierCodesAsSelectListItem(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void ClaimSupplierPartBL_ListClaimSupplierParts_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new ClaimSupplierPartViewModel();
            model.ClaimRecallPeriodId = 1;
            model.SupplierCode = guid;
            model.SupplierName = guid;
            model.PartId = 1;
            model.PartCode = "M.162127";
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _ClaimSupplierPartBL.DMLClaimSupplierPart(UserManager.UserInfo, model);

            int count = 0;
            var filter = new ClaimSupplierPartListModel();
            filter.SupplierCode = guid;
            filter.PartId = 39399;
            filter.PartCode = "M.162127";
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";

            var resultGet = _ClaimSupplierPartBL.ListClaimSupplierParts(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

    }

}

