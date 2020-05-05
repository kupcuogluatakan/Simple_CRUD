using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;


namespace ODMSUnitTest
{

    [TestClass]
    public class ActiveAnnouncementsBLTest
    {

        ActiveAnnouncementsBL _ActiveAnnouncementsBL = new ActiveAnnouncementsBL();

        [TestMethod]
        public void ActiveAnnouncementsBL_GetActiveAnnouncementCount_GetModel()
        {
            var count = 0;
            var resultGet = _ActiveAnnouncementsBL.GetActiveAnnouncementCount(UserManager.UserInfo, out count);

            Assert.IsTrue(resultGet.Model > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void ActiveAnnouncementsBL_ListActiveAnnouncements_GetAll()
        {
            var resultGet = _ActiveAnnouncementsBL.ListActiveAnnouncements(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

