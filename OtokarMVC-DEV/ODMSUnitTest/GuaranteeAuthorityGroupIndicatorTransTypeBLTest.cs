using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.GuaranteeAuthorityGroupIndicatorTransTypes;


namespace ODMSUnitTest
{

    [TestClass]
    public class GuaranteeAuthorityGroupIndicatorTransTypeBLTest
    {

        GuaranteeAuthorityGroupIndicatorTransTypeBL _GuaranteeAuthorityGroupIndicatorTransTypeBL = new GuaranteeAuthorityGroupIndicatorTransTypeBL();

        [TestMethod]
        public void GuaranteeAuthorityGroupIndicatorTransTypeBL_Save_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GuaranteeAuthorityGroupIndicatorTransTypesModel();
            model.GroupId = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _GuaranteeAuthorityGroupIndicatorTransTypeBL.Save(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void GuaranteeAuthorityGroupIndicatorTransTypeBL_ListGuaranteeAuthorityGroupIndicatorTransTypesIncluded_GetAll()
        {
            var resultGet = _GuaranteeAuthorityGroupIndicatorTransTypeBL.ListGuaranteeAuthorityGroupIndicatorTransTypesIncluded(UserManager.UserInfo, 1);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void GuaranteeAuthorityGroupIndicatorTransTypeBL_ListGuaranteeAuthorityGroupIndicatorTransTypesExcluded_GetAll()
        {
            var resultGet = _GuaranteeAuthorityGroupIndicatorTransTypeBL.ListGuaranteeAuthorityGroupIndicatorTransTypesExcluded(UserManager.UserInfo, 1);

            Assert.IsTrue(resultGet.Total > 0);
        }

    }

}

