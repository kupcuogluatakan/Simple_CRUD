using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.GuaranteeAuthorityLimitations;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class GuaranteeAuthorityLimitationsBLTest
    {

        GuaranteeAuthorityLimitationsBL _GuaranteeAuthorityLimitationsBL = new GuaranteeAuthorityLimitationsBL();

        [TestMethod]
        public void GuaranteeAuthorityLimitationsBL_DMLGuaranteeAuthorityLimitations_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GuaranteeAuthorityLimitationsViewModel();
            model.ModelKod = "ATLAS";
            model.ModelName = guid;
            model.CurrencyCode = guid;
            model.CurrencyName = guid;
            model.Amount = 1;
            model.CumulativeAmount = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _GuaranteeAuthorityLimitationsBL.DMLGuaranteeAuthorityLimitations(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void GuaranteeAuthorityLimitationsBL_GetGuaranteeAuthorityLimitations_GetModel()
        {
            var resultGet = _GuaranteeAuthorityLimitationsBL.GetGuaranteeAuthorityLimitations(UserManager.UserInfo, "TR", "ATLAS");

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.ModelName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void GuaranteeAuthorityLimitationsBL_ListGuaranteeAuthorityLimitations_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GuaranteeAuthorityLimitationsViewModel();
            model.ModelKod = "ATLAS";
            model.ModelName = guid;
            model.CurrencyCode = guid;
            model.CurrencyName = guid;
            model.Amount = 1;
            model.CumulativeAmount = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _GuaranteeAuthorityLimitationsBL.DMLGuaranteeAuthorityLimitations(UserManager.UserInfo, model);

            int count = 0;
            var filter = new GuaranteeAuthorityLimitationsListModel();
            filter.ModelKod = "ATLAS";
            filter.CurrencyCode = guid;

            var resultGet = _GuaranteeAuthorityLimitationsBL.ListGuaranteeAuthorityLimitations(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

