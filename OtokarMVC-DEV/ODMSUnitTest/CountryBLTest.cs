using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class CountryBLTest
    {

        CountryBL _CountryBL = new CountryBL();

        [TestMethod]
        public void CountryBL_GetCountryList_GetAll()
        {
            var resultGet = _CountryBL.GetCountryList(UserManager.UserInfo.LanguageCode);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void CountryBL_GetCountryList_Duration_GetAll()
        {
            var resultGet = _CountryBL.GetCountryList_Duration(UserManager.UserInfo.LanguageCode);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

