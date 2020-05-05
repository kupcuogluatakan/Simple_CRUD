using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.ListModel;
using ODMSBusiness.Business;

namespace ODMSUnitTest
{

    [TestClass]
    public class VehicleHistoryBLTest
    {

        VehicleHistoryBL _VehicleHistoryBL = new VehicleHistoryBL();

        [TestMethod]
        public void VehicleHistoryBL_ListVehicleHistory_GetAll()
        {

            int count = 0;
            var filter = new VehicleHistoryListModel();
            filter.VehicleId = 29627;
            filter.DealerId = UserManager.UserInfo.DealerID;
            var resultGet = _VehicleHistoryBL.ListVehicleHistory(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

