using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class ObjectSearchBLTest
    {

        ObjectSearchBL _ObjectSearchBL = new ObjectSearchBL();

        [TestMethod]
        public void ObjectSearchBL_GetObjectTextWithId_GetModel()
        {
            var resultGet = _ObjectSearchBL.GetObjectTextWithId(UserManager.UserInfo, ODMSCommon.CommonValues.ObjectSearchType.Customer, 1);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model != string.Empty && resultGet.IsSuccess);
        }


    }

}

