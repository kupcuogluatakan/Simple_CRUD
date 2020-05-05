using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.Proposal;
using System;
using ODMSBusiness.Business;
using ODMSModel.VehicleBodyWorkProposal;


namespace ODMSUnitTest
{

	[TestClass]
	public class ProposalBLTest
	{

		ProposalBL _ProposalBL = new ProposalBL();

		[TestMethod]
		public void ProposalBL_DMLProposal_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ProposalViewModel();
			model.ProposalId= 1; 
			model.CustomerId= 1; 
			model.CustomerName= guid; 
			model.CustomerSurName= guid; 
			model.CustomerPhone= guid; 
			model.VehiclePlate= guid; 
			model.ModelKod = "ATLAS"; 
			model.ModelName= guid; 
			model.VehicleTypeId= 1; 
			model.VehicleTypeName= guid; 
			model.VehicleId = 29627; 
			model.AppointmentTypeId= guid; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.Stuff= guid; 
			model.Note= guid; 
			model.ProposalDate= DateTime.Now; 
			model.ProposalStatusId= 1; 
			model.VehicleKM= 1; 
			model.VinNo= guid; 
			model.DeliveryTime= DateTime.Now; 
			model.VehicleModel= guid; 
			model.AppointmentType= guid; 
			model.FleetId= 1; 
			model.WorkOrderId= 1; 
			model.ProposalSeq= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ProposalBL.DMLProposal(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void ProposalBL_GetDealerUsers_GetModel()
		{
			 var resultGet = _ProposalBL.GetDealerUsers(UserManager.UserInfo.DealerID);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Total > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void ProposalBL_GetProposalViewModel_GetModel()
		{
			 var resultGet = _ProposalBL.GetProposalViewModel(UserManager.UserInfo, 102,3);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.ProposalId > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void ProposalBL_GetProposalData_GetModel()
		{
			 var resultGet = _ProposalBL.GetProposalData(1, "Vehicle");
			
			Assert.IsTrue( resultGet.Total > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void ProposalBL_GetVehicleCustomerId_GetModel()
		{
			 var resultGet = _ProposalBL.GetVehicleCustomerId(29635);
			
			Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void ProposalBL_GetCustomerChangeData_GetModel()
		{
			 var resultGet = _ProposalBL.GetCustomerChangeData(617, 617);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.VehicleCustomerId > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void ProposalBL_GetBodyworkFromVehicleProposal_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ProposalViewModel();
			model.ProposalId= 1; 
			model.CustomerId= 1; 
			model.CustomerName= guid; 
			model.CustomerSurName= guid; 
			model.CustomerPhone= guid; 
			model.VehiclePlate= guid; 
			model.ModelKod = "ATLAS"; 
			model.ModelName= guid; 
			model.VehicleTypeId= 1; 
			model.VehicleTypeName= guid; 
			model.VehicleId = 29627; 
			model.AppointmentTypeId= guid; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.Stuff= guid; 
			model.Note= guid; 
			model.ProposalDate= DateTime.Now; 
			model.ProposalStatusId= 1; 
			model.VehicleKM= 1; 
			model.VinNo= guid; 
			model.DeliveryTime= DateTime.Now; 
			model.VehicleModel= guid; 
			model.AppointmentType= guid; 
			model.FleetId= 1; 
			model.WorkOrderId= 1; 
			model.ProposalSeq= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ProposalBL.DMLProposal(UserManager.UserInfo, model);
			
			var filter = new VehicleBodyworkViewModelProposal();
			filter.BodyworkCode = guid; 
			filter.VehicleId = 29627; 
			filter.CountryId = 1; 
			filter.CityId = 1; 
			filter.DealerId = UserManager.UserInfo.DealerID; 
			
			 var resultGet = _ProposalBL.GetBodyworkFromVehicleProposal(filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.VehicleBodyworkId > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void ProposalBL_ListAppointmentTypes_GetAll()
		{
			 var resultGet = _ProposalBL.ListAppointmentTypes(UserManager.UserInfo);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        
		[TestMethod]
		public void ProposalBL_ListAppointmentTypesForProposal_GetAll()
		{
			 var resultGet = _ProposalBL.ListAppointmentTypesForProposal(UserManager.UserInfo);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void ProposalBL_DMLProposal_Update()
		{
		    var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var modelUpdate = new ProposalViewModel();
			modelUpdate.ProposalId = 1;
			modelUpdate.CustomerName= guid; 
			modelUpdate.CustomerSurName= guid; 
			modelUpdate.CustomerPhone= guid; 
			modelUpdate.VehiclePlate= guid; 
			modelUpdate.ModelKod = "ATLAS"; 
			modelUpdate.ModelName= guid; 
			modelUpdate.VehicleTypeName= guid; 
			modelUpdate.VehicleId = 29627; 
			modelUpdate.AppointmentTypeId= guid; 
			modelUpdate.DealerId = UserManager.UserInfo.DealerID; 
			modelUpdate.DealerName= guid; 
			modelUpdate.Stuff= guid; 
			modelUpdate.Note= guid; 
			modelUpdate.VinNo= guid; 
			modelUpdate.VehicleModel= guid; 
			modelUpdate.AppointmentType= guid; 
			modelUpdate.CommandType = "U";
			 var resultUpdate = _ProposalBL.DMLProposal(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void ProposalBL_DMLProposal_Delete()
		{
		    var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var modelDelete = new ProposalViewModel();
			modelDelete.ProposalId = 1;
			modelDelete.CustomerName= guid; 
			modelDelete.CustomerSurName= guid; 
			modelDelete.CustomerPhone= guid; 
			modelDelete.VehiclePlate= guid; 
			modelDelete.ModelKod = "ATLAS"; 
			modelDelete.ModelName= guid; 
			modelDelete.VehicleTypeName= guid; 
			modelDelete.VehicleId = 29627; 
			modelDelete.AppointmentTypeId= guid; 
			modelDelete.DealerId = UserManager.UserInfo.DealerID; 
			modelDelete.DealerName= guid; 
			modelDelete.Stuff= guid; 
			modelDelete.Note= guid; 
			modelDelete.VinNo= guid; 
			modelDelete.VehicleModel= guid; 
			modelDelete.AppointmentType= guid; 
			
			modelDelete.CommandType = "D";
			 var resultDelete = _ProposalBL.DMLProposal(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}

		[TestMethod]
		public void ProposalBL_ListProposal_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ProposalViewModel();
			model.ProposalId= 1; 
			model.CustomerId= 1; 
			model.CustomerName= guid; 
			model.CustomerSurName= guid; 
			model.CustomerPhone= guid; 
			model.VehiclePlate= guid; 
			model.ModelKod = "ATLAS"; 
			model.ModelName= guid; 
			model.VehicleTypeId= 1; 
			model.VehicleTypeName= guid; 
			model.VehicleId = 29627; 
			model.AppointmentTypeId= guid; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.Stuff= guid; 
			model.Note= guid; 
			model.ProposalDate= DateTime.Now; 
			model.ProposalStatusId= 1; 
			model.VehicleKM= 1; 
			model.VinNo= guid; 
			model.DeliveryTime= DateTime.Now; 
			model.VehicleModel= guid; 
			model.AppointmentType= guid; 
			model.FleetId= 1; 
			model.WorkOrderId= 1; 
			model.ProposalSeq= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ProposalBL.DMLProposal(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new ProposalListModel();
			filter.VehicleId = 29627; 
			filter.VehicleCode = guid; 
			filter.DealerId = UserManager.UserInfo.DealerID; 
			
			 var resultGet = _ProposalBL.ListProposal(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        
		[TestMethod]
		public void ProposalBL_ListProposalStats_GetAll()
		{
			 var resultGet = _ProposalBL.ListProposalStats(UserManager.UserInfo);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		


	}

}

