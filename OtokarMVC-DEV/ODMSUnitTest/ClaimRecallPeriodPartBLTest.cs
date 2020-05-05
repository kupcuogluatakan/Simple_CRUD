using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.ClaimRecallPeriodPart;


namespace ODMSUnitTest
{

    [TestClass]
    public class ClaimRecallPeriodPartBLTest
    {

        ClaimRecallPeriodPartBL _ClaimRecallPeriodPartBL = new ClaimRecallPeriodPartBL();

        [TestMethod]
        public void ClaimRecallPeriodPartBL_DMLClaimRecallPeriodPart_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new ClaimRecallPeriodPartViewModel();
            model.PartId = 1;
            model.PartCode = "M.162127";
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.CreateUserName = guid;
            model.UpdateUserName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _ClaimRecallPeriodPartBL.DMLClaimRecallPeriodPart(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void ClaimRecallPeriodPartBL_GetClaimRecallPeriodPart_GetModel()
        {
            var resultGet = _ClaimRecallPeriodPartBL.GetClaimRecallPeriodPart(UserManager.UserInfo, 39399);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.PartCode != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void ClaimRecallPeriodPartBL_ListClaimRecallPeriodParts_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new ClaimRecallPeriodPartViewModel();
            model.PartId = 1;
            model.PartCode = "M.162127";
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.CreateUserName = guid;
            model.UpdateUserName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _ClaimRecallPeriodPartBL.DMLClaimRecallPeriodPart(UserManager.UserInfo, model);

            int count = 0;
            var filter = new ClaimRecallPeriodPartListModel();
            filter.PartId = 39399;
            filter.PartCode = "M.162127";
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";

            var resultGet = _ClaimRecallPeriodPartBL.ListClaimRecallPeriodParts(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

