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

    [TestClass]
    public class DealerTechnicianGroupTechnicianBLTest
    {

        DealerTechnicianGroupTechnicianBL _DealerTechnicianGroupTechnicianBL = new DealerTechnicianGroupTechnicianBL();

        [TestMethod]
        public void DealerTechnicianGroupTechnicianBL_SaveTechnicianGroupTechnicians_Insert()
        {
            var list = new List<int>();
            list.Add(UserManager.UserInfo.UserId);

            var result = _DealerTechnicianGroupTechnicianBL.SaveTechnicianGroupTechnicians(UserManager.UserInfo, 1, list);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void DealerTechnicianGroupTechnicianBL_ListTechnicianGroupTechniciansIncluded_GetAll()
        {
            var resultGet = _DealerTechnicianGroupTechnicianBL.ListTechnicianGroupTechniciansIncluded(UserManager.UserInfo, 1);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void DealerTechnicianGroupTechnicianBL_ListTechnicianGroupTechniciansExcluded_GetAll()
        {
            var resultGet = _DealerTechnicianGroupTechnicianBL.ListTechnicianGroupTechniciansExcluded(UserManager.UserInfo,1);

            Assert.IsTrue(resultGet.Total > 0);
        }


        [TestMethod]
        public void DealerTechnicianGroupTechnicianBL_ListDealerTechnicianGroupsAsSelectListItem_GetAll()
        {
            var resultGet = _DealerTechnicianGroupTechnicianBL.ListDealerTechnicianGroupsAsSelectListItem(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

