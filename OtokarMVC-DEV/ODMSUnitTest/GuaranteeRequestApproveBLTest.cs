using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.GuaranteeRequestApprove;


namespace ODMSUnitTest
{

    [TestClass]
    public class GuaranteeRequestApproveBLTest
    {

        GuaranteeRequestApproveBL _GuaranteeRequestApproveBL = new GuaranteeRequestApproveBL();

        [TestMethod]
        public void GuaranteeRequestApproveBL_ListGuaranteeRequestApprove_GetAll()
        {

            int count = 0;
            var filter = new GuaranteeRequestApproveListModel();
            filter.IdDealer = UserManager.UserInfo.GetUserDealerId();
            filter.IdUser = UserManager.UserInfo.UserId;

            var resultGet = _GuaranteeRequestApproveBL.ListGuaranteeRequestApprove(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

