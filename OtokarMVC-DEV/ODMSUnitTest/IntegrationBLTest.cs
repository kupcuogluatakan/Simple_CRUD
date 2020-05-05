using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.Integration;
using ODMSBusiness.Business;

namespace ODMSUnitTest
{

    [TestClass]
    public class IntegrationBLTest
    {

        IntegrationBL _IntegrationBL = new IntegrationBL();

        [TestMethod]
        public void IntegrationBL_GetIntegrationList_GetAll()
        {
            var filter = new IntegrationListModel();
            filter.IntegrationTypeId = 1;
            var resultGet = _IntegrationBL.GetIntegrationList(UserManager.UserInfo, filter);
            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void IntegrationBL_GetIntegrationDetailList_GetAll()
        {
            var filter = new IntegrationDetailListModel();
            filter.IntegrationTypeId = 1;
            var resultGet = _IntegrationBL.GetIntegrationDetailList(UserManager.UserInfo, filter);
            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

