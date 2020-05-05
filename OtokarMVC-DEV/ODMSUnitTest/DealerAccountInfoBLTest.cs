using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using System;
using ODMSBusiness.Business;

namespace ODMSUnitTest
{

    [TestClass]
    public class DealerAccountInfoBLTest
    {

        DealerAccountInfoBL _DealerAccountInfoBL = new DealerAccountInfoBL();

        [TestMethod]
        public void DealerAccountInfoBL_List_GetAll()
        {
            var resultGet = _DealerAccountInfoBL.List(UserManager.UserInfo, null);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

