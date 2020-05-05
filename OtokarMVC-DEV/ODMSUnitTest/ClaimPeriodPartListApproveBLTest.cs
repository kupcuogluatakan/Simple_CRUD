using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using System;
using System.Collections.Generic;


namespace ODMSUnitTest
{

    /// <summary>
    /// TODO : Buna bakalým
    /// </summary>
	[TestClass]
    public class ClaimPeriodPartListApproveBLTest
    {

        ClaimPeriodPartListApproveBL _ClaimPeriodPartListApproveBL = new ClaimPeriodPartListApproveBL();

        [TestMethod]
        public void ClaimPeriodPartListApproveBL_Save_Insert()
        {
            var list = new List<long>();
            list.Add(-1);
            var result = _ClaimPeriodPartListApproveBL.Save(UserManager.UserInfo, 1, list, list);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void ClaimPeriodPartListApproveBL_ListClaimPeriodParts_GetAll()
        {
            var resultGet = _ClaimPeriodPartListApproveBL.ListClaimPeriodParts(UserManager.UserInfo, 17);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void ClaimPeriodPartListApproveBL_ListDismantledParts_GetAll()
        {
            var resultGet = _ClaimPeriodPartListApproveBL.ListDismantledParts(UserManager.UserInfo, 17, 331213);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

