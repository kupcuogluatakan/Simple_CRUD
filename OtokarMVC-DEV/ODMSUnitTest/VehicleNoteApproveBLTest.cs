using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.VehicleNoteApprove;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class VehicleNoteApproveBLTest
    {

        VehicleNoteApproveBL _VehicleNoteApproveBL = new VehicleNoteApproveBL();

        [TestMethod]
        public void VehicleNoteApproveBL_GetVehicleNoteApprove_GetModel()
        {
            var filter = new VehicleNoteApproveModel();
            filter.VehicleId = 29627;
            filter.DealerId = UserManager.UserInfo.DealerID;

            var resultGet = _VehicleNoteApproveBL.GetVehicleNoteApprove(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.DealerName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void VehicleNoteApproveBL_ListVehicleNoteApprove_GetAll()
        {

            int count = 0;
            var filter = new VehicleNoteApproveListModel();
            filter.VehicleId = 29627;
            filter.DealerId = UserManager.UserInfo.DealerID.ToString();

            var resultGet = _VehicleNoteApproveBL.ListVehicleNoteApprove(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void VehicleNoteApproveBL_ListVehicleNoteApproveAsSelected_GetAll()
        {
            var resultGet = VehicleNoteApproveBL.ListVehicleNoteApproveAsSelected(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

