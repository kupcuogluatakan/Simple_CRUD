using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.DeliveryList;


namespace ODMSUnitTest
{

    [TestClass]
    public class DeliveryListBLTest
    {

        DeliveryListBL _DeliveryListBL = new DeliveryListBL();

        [TestMethod]
        public void DeliveryListBL_ListDeliveryList_GetAll()
        {

            int count = 0;
            var filter = new DeliveryListListModel();

            var resultGet = _DeliveryListBL.ListDeliveryList(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

