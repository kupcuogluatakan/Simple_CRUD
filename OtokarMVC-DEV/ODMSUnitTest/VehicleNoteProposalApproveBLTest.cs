using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.VehicleNoteProposalApprove;


namespace ODMSUnitTest
{

    [TestClass]
    public class VehicleNoteProposalApproveBLTest
    {

        VehicleNoteProposalApproveBL _VehicleNoteProposalApproveBL = new VehicleNoteProposalApproveBL();

        [TestMethod]
        public void VehicleNoteProposalApproveBL_GetVehicleNoteProposalApprove_GetModel()
        {
            var filter = new VehicleNoteProposalApproveModel();
            filter.VehicleId = 29627;
            filter.DealerId = UserManager.UserInfo.DealerID;

            var resultGet = _VehicleNoteProposalApproveBL.GetVehicleNoteProposalApprove(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.DealerName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void VehicleNoteProposalApproveBL_ListVehicleNoteProposalApprove_GetAll()
        {

            int count = 0;
            var filter = new VehicleNoteProposalApproveListModel();
            filter.VehicleId = 29627;
            filter.DealerId = UserManager.UserInfo.DealerID.ToString();

            var resultGet = _VehicleNoteProposalApproveBL.ListVehicleNoteProposalApprove(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void VehicleNoteProposalApproveBL_ListVehicleNoteProposalApproveAsSelected_GetAll()
        {
            var resultGet = VehicleNoteProposalApproveBL.ListVehicleNoteProposalApproveAsSelected(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

