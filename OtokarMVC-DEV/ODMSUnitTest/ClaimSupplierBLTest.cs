using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.ClaimSupplier;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class ClaimSupplierBLTest
    {

        ClaimSupplierBL _ClaimSupplierBL = new ClaimSupplierBL();

        [TestMethod]
        public void ClaimSupplierBL_DMLClaimSupplier_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new ClaimSupplierViewModel();
            model.SupplierCode = guid;
            model.SupplierName = guid;
            model.ClaimRackCode = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _ClaimSupplierBL.DMLClaimSupplier(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void ClaimSupplierBL_GetClaimSupplier_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new ClaimSupplierViewModel();
            model.SupplierCode = guid;
            model.SupplierName = guid;
            model.ClaimRackCode = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _ClaimSupplierBL.DMLClaimSupplier(UserManager.UserInfo, model);

            var filter = new ClaimSupplierViewModel();
            filter.SupplierCode = guid;
            filter.ClaimRackCode = guid;

            var resultGet = _ClaimSupplierBL.GetClaimSupplier(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.SupplierName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void ClaimSupplierBL_ListClaimSupplier_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new ClaimSupplierViewModel();
            model.SupplierCode = guid;
            model.SupplierName = guid;
            model.ClaimRackCode = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _ClaimSupplierBL.DMLClaimSupplier(UserManager.UserInfo, model);

            int count = 0;
            var filter = new ClaimSupplierListModel();
            filter.SupplierCode = guid;
            filter.ClaimRackCode = guid;

            var resultGet = _ClaimSupplierBL.ListClaimSupplier(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

