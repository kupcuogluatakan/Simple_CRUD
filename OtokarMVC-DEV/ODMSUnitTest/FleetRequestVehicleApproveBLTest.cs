using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using System.Collections.Generic;


namespace ODMSUnitTest
{

    [TestClass]
    public class FleetRequestVehicleApproveBLTest
    {

        FleetRequestVehicleApproveBL _FleetRequestVehicleApproveBL = new FleetRequestVehicleApproveBL();

        [TestMethod]
        public void FleetRequestVehicleApproveBL_SaveRequests_Insert()
        {
            var error = 0;
            var errorDesc = string.Empty;
            var result = _FleetRequestVehicleApproveBL.SaveRequests(UserManager.UserInfo, new List<ODMSModel.FleetRequestVehicleApprove.FleetRequestApproveListModel>(), out error, out errorDesc);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void FleetRequestVehicleApproveBL_GetFleetRequestData_GetModel()
        {
            var resultGet = _FleetRequestVehicleApproveBL.GetFleetRequestData(UserManager.UserInfo, 1);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Description != string.Empty && resultGet.IsSuccess);
        }


    }

}

