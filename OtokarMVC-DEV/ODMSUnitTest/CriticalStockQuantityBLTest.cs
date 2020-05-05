using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.CriticalStockQuantity;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class CriticalStockQuantityBLTest
    {

        CriticalStockQuantityBL _CriticalStockQuantityBL = new CriticalStockQuantityBL();

        [TestMethod]
        public void CriticalStockQuantityBL_ListCriticalStockQuantity_GetAll()
        {

            int count = 0;
            var filter = new CriticalStockQuantityListModel();
            filter.PartId = 39399;
            filter.PartCode = "M.162127";
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            filter.DealerId = UserManager.UserInfo.DealerID;

            var resultGet = _CriticalStockQuantityBL.ListCriticalStockQuantity(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

