using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.PDIVehicleResult;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class PDIVehicleResultBLTest
	{

		PDIVehicleResultBL _PDIVehicleResultBL = new PDIVehicleResultBL();

		[TestMethod]
		public void PDIVehicleResultBL_DMLPDIVehicleResult_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PDIVehicleResultViewModel();
			model.PDIVehicleResultId= 1; 
			model.WorkOrderId= 1; 
			model.WorkOrderDetailId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.VehicleId = 29627; 
			model.VinNo= guid; 
			model.StatusName= guid; 
			model.EngineNo= guid; 
			model.TransmissionSerialNo= guid; 
			model.DifferentialSerialNo= guid; 
			model.PDICheckNote= guid; 
			model.ApprovalNote= guid; 
			model.ApprovalUserName= guid; 
			model.CreateDate= DateTime.Now; 
			model.GroupName= guid; 
			model.TypeName= guid; 
			model.ModelKod = "ATLAS"; 
			model.CustomerId= 1; 
			model.CustomerName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PDIVehicleResultBL.DMLPDIVehicleResult(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void PDIVehicleResultBL_GetPDIVehicleResult_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PDIVehicleResultViewModel();
			model.PDIVehicleResultId= 1; 
			model.WorkOrderId= 1; 
			model.WorkOrderDetailId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.VehicleId = 29627; 
			model.VinNo= guid; 
			model.StatusName= guid; 
			model.EngineNo= guid; 
			model.TransmissionSerialNo= guid; 
			model.DifferentialSerialNo= guid; 
			model.PDICheckNote= guid; 
			model.ApprovalNote= guid; 
			model.ApprovalUserName= guid; 
			model.CreateDate= DateTime.Now; 
			model.GroupName= guid; 
			model.TypeName= guid; 
			model.ModelKod = "ATLAS"; 
			model.CustomerId= 1; 
			model.CustomerName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PDIVehicleResultBL.DMLPDIVehicleResult(UserManager.UserInfo, model);
			
			var filter = new PDIVehicleResultViewModel();
			filter.PDIVehicleResultId = result.Model.PDIVehicleResultId;
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.VehicleId = 29627; 
			filter.ModelKod = "ATLAS"; 
			
			 var resultGet = _PDIVehicleResultBL.GetPDIVehicleResult(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.PDIVehicleResultId > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void PDIVehicleResultBL_ListPDIVehicleResult_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PDIVehicleResultViewModel();
			model.PDIVehicleResultId= 1; 
			model.WorkOrderId= 1; 
			model.WorkOrderDetailId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.VehicleId = 29627; 
			model.VinNo= guid; 
			model.StatusName= guid; 
			model.EngineNo= guid; 
			model.TransmissionSerialNo= guid; 
			model.DifferentialSerialNo= guid; 
			model.PDICheckNote= guid; 
			model.ApprovalNote= guid; 
			model.ApprovalUserName= guid; 
			model.CreateDate= DateTime.Now; 
			model.GroupName= guid; 
			model.TypeName= guid; 
			model.ModelKod = "ATLAS"; 
			model.CustomerId= 1; 
			model.CustomerName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PDIVehicleResultBL.DMLPDIVehicleResult(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new PDIVehicleResultListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.VehicleId = 29627; 
			filter.ModelKod = "ATLAS"; 
			
			 var resultGet = _PDIVehicleResultBL.ListPDIVehicleResult(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void PDIVehicleResultBL_DMLPDIVehicleResult_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PDIVehicleResultViewModel();
			model.PDIVehicleResultId= 1; 
			model.WorkOrderId= 1; 
			model.WorkOrderDetailId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.VehicleId = 29627; 
			model.VinNo= guid; 
			model.StatusName= guid; 
			model.EngineNo= guid; 
			model.TransmissionSerialNo= guid; 
			model.DifferentialSerialNo= guid; 
			model.PDICheckNote= guid; 
			model.ApprovalNote= guid; 
			model.ApprovalUserName= guid; 
			model.CreateDate= DateTime.Now; 
			model.GroupName= guid; 
			model.TypeName= guid; 
			model.ModelKod = "ATLAS"; 
			model.CustomerId= 1; 
			model.CustomerName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PDIVehicleResultBL.DMLPDIVehicleResult(UserManager.UserInfo, model);
			
			var filter = new PDIVehicleResultListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.VehicleId = 29627; 
			filter.ModelKod = "ATLAS"; 
			
			int count = 0;
			 var resultGet = _PDIVehicleResultBL.ListPDIVehicleResult(UserManager.UserInfo, filter, out count);
			
			var modelUpdate = new PDIVehicleResultViewModel();
			modelUpdate.PDIVehicleResultId = resultGet.Data.First().PDIVehicleResultId;
			modelUpdate.DealerId = UserManager.UserInfo.DealerID; 
			modelUpdate.DealerName= guid; 
			modelUpdate.VehicleId = 29627; 
			modelUpdate.VinNo= guid; 
			modelUpdate.StatusName= guid; 
			modelUpdate.EngineNo= guid; 
			modelUpdate.TransmissionSerialNo= guid; 
			modelUpdate.DifferentialSerialNo= guid; 
			modelUpdate.PDICheckNote= guid; 
			modelUpdate.ApprovalNote= guid; 
			modelUpdate.ApprovalUserName= guid; 
			modelUpdate.GroupName= guid; 
			modelUpdate.TypeName= guid; 
			modelUpdate.ModelKod = "ATLAS"; 
			modelUpdate.CustomerName= guid; 
			modelUpdate.CommandType = "U";
			 var resultUpdate = _PDIVehicleResultBL.DMLPDIVehicleResult(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void PDIVehicleResultBL_DMLPDIVehicleResult_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PDIVehicleResultViewModel();
			model.PDIVehicleResultId= 1; 
			model.WorkOrderId= 1; 
			model.WorkOrderDetailId= 1; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.VehicleId = 29627; 
			model.VinNo= guid; 
			model.StatusName= guid; 
			model.EngineNo= guid; 
			model.TransmissionSerialNo= guid; 
			model.DifferentialSerialNo= guid; 
			model.PDICheckNote= guid; 
			model.ApprovalNote= guid; 
			model.ApprovalUserName= guid; 
			model.CreateDate= DateTime.Now; 
			model.GroupName= guid; 
			model.TypeName= guid; 
			model.ModelKod = "ATLAS"; 
			model.CustomerId= 1; 
			model.CustomerName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PDIVehicleResultBL.DMLPDIVehicleResult(UserManager.UserInfo, model);
			
			var filter = new PDIVehicleResultListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.VehicleId = 29627; 
			filter.ModelKod = "ATLAS"; 
			
			int count = 0;
			 var resultGet = _PDIVehicleResultBL.ListPDIVehicleResult(UserManager.UserInfo, filter, out count);
			
			var modelDelete = new PDIVehicleResultViewModel();
			modelDelete.PDIVehicleResultId = resultGet.Data.First().PDIVehicleResultId;
			modelDelete.DealerId = UserManager.UserInfo.DealerID; 
			modelDelete.DealerName= guid; 
			modelDelete.VehicleId = 29627; 
			modelDelete.VinNo= guid; 
			modelDelete.StatusName= guid; 
			modelDelete.EngineNo= guid; 
			modelDelete.TransmissionSerialNo= guid; 
			modelDelete.DifferentialSerialNo= guid; 
			modelDelete.PDICheckNote= guid; 
			modelDelete.ApprovalNote= guid; 
			modelDelete.ApprovalUserName= guid; 
			modelDelete.GroupName= guid; 
			modelDelete.TypeName= guid; 
			modelDelete.ModelKod = "ATLAS"; 
			modelDelete.CustomerName= guid; 
			modelDelete.CommandType = "D";
			 var resultDelete = _PDIVehicleResultBL.DMLPDIVehicleResult(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}

