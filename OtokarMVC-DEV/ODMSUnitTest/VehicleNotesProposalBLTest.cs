using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.VehicleNoteProposal;


namespace ODMSUnitTest
{

    [TestClass]
    public class VehicleNotesProposalBLTest
    {

        VehicleNotesProposalBL _VehicleNotesProposalBL = new VehicleNotesProposalBL();

        [TestMethod]
        public void VehicleNotesProposalBL_DMLVehicleNotesProposal_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleNotesProposalModel();
            model.VehicleNotesId = 1;
            model.VehicleId = 29627;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.Note = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _VehicleNotesProposalBL.DMLVehicleNotesProposal(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void VehicleNotesProposalBL_GetVehicleNotesProposal_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleNotesProposalModel();
            model.VehicleNotesId = 1;
            model.VehicleId = 29627;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.Note = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _VehicleNotesProposalBL.DMLVehicleNotesProposal(UserManager.UserInfo, model);

            var filter = new VehicleNotesProposalModel();
            filter.VehicleNotesId = result.Model.VehicleNotesId;
            filter.VehicleId = 29627;
            filter.DealerId = UserManager.UserInfo.DealerID;

            var resultGet = _VehicleNotesProposalBL.GetVehicleNotesProposal(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.DealerName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void VehicleNotesProposalBL_ListVehicleNotesProposal_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleNotesProposalModel();
            model.VehicleNotesId = 1;
            model.VehicleId = 29627;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.Note = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _VehicleNotesProposalBL.DMLVehicleNotesProposal(UserManager.UserInfo, model);

            int count = 0;
            var filter = new VehicleNotesProposalListModel();
            filter.VehicleId = 29627;

            var resultGet = _VehicleNotesProposalBL.ListVehicleNotesProposal(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void VehicleNotesProposalBL_DMLVehicleNotesProposal_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleNotesProposalModel();
            model.VehicleNotesId = 1;
            model.VehicleId = 29627;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.Note = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _VehicleNotesProposalBL.DMLVehicleNotesProposal(UserManager.UserInfo, model);

            var filter = new VehicleNotesProposalListModel();
            filter.VehicleId = 29627;

            int count = 0;
            var resultGet = _VehicleNotesProposalBL.ListVehicleNotesProposal(UserManager.UserInfo, filter, out count);

            var modelUpdate = new VehicleNotesProposalModel();
            modelUpdate.VehicleNotesId = resultGet.Data.First().VehicleNotesId;
            modelUpdate.VehicleId = 29627;
            modelUpdate.DealerId = UserManager.UserInfo.DealerID;
            modelUpdate.DealerName = guid;
            modelUpdate.Note = guid;
            modelUpdate.IsActiveName = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _VehicleNotesProposalBL.DMLVehicleNotesProposal(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void VehicleNotesProposalBL_DMLVehicleNotesProposal_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleNotesProposalModel();
            model.VehicleNotesId = 1;
            model.VehicleId = 29627;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.Note = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _VehicleNotesProposalBL.DMLVehicleNotesProposal(UserManager.UserInfo, model);

            var filter = new VehicleNotesProposalListModel();
            filter.VehicleId = 29627;

            int count = 0;
            var resultGet = _VehicleNotesProposalBL.ListVehicleNotesProposal(UserManager.UserInfo, filter, out count);

            var modelDelete = new VehicleNotesProposalModel();
            modelDelete.VehicleNotesId = resultGet.Data.First().VehicleNotesId;
            modelDelete.VehicleId = 29627;
            modelDelete.DealerId = UserManager.UserInfo.DealerID;
            modelDelete.DealerName = guid;
            modelDelete.Note = guid;
            modelDelete.IsActiveName = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _VehicleNotesProposalBL.DMLVehicleNotesProposal(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}

