using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.WorkshopWorker;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class WorkshopWorkerBLTest
	{

		WorkshopWorkerBL _WorkshopWorkerBL = new WorkshopWorkerBL();

		[TestMethod]
		public void WorkshopWorkerBL_DMLWorkshopWorker_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new WorkshopWorkerDetailModel();
			model.WorkshopName= guid; 
			model.WorkerName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _WorkshopWorkerBL.DMLWorkshopWorker(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void WorkshopWorkerBL_GetWorkshopWorker_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new WorkshopWorkerDetailModel();
			model.WorkshopName= guid; 
			model.WorkerName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _WorkshopWorkerBL.DMLWorkshopWorker(UserManager.UserInfo, model);
			
			var filter = new WorkshopWorkerDetailModel();
			filter.WorkshopId = result.Model.WorkshopId;
			 var resultGet = _WorkshopWorkerBL.GetWorkshopWorker(filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.WorkshopId > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void WorkshopWorkerBL_ListWorkshopWorkers_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new WorkshopWorkerDetailModel();
			model.WorkshopName= guid; 
			model.WorkerName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _WorkshopWorkerBL.DMLWorkshopWorker(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new WorkshopWorkerListModel();
			
			 var resultGet = _WorkshopWorkerBL.ListWorkshopWorkers(filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void WorkshopWorkerBL_DMLWorkshopWorker_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new WorkshopWorkerDetailModel();
			model.WorkshopName= guid; 
			model.WorkerName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _WorkshopWorkerBL.DMLWorkshopWorker(UserManager.UserInfo, model);
			
			var filter = new WorkshopWorkerListModel();
			
			int count = 0;
			 var resultGet = _WorkshopWorkerBL.ListWorkshopWorkers(filter, out count);
			
			var modelUpdate = new WorkshopWorkerDetailModel();
			modelUpdate.WorkshopId = resultGet.Data.First().WorkshopId;
			modelUpdate.WorkshopName= guid; 
			modelUpdate.WorkerName= guid; 
			
			
			
			modelUpdate.CommandType = "U";
			 var resultUpdate = _WorkshopWorkerBL.DMLWorkshopWorker(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void WorkshopWorkerBL_DMLWorkshopWorker_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new WorkshopWorkerDetailModel();
			model.WorkshopName= guid; 
			model.WorkerName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _WorkshopWorkerBL.DMLWorkshopWorker(UserManager.UserInfo, model);
			
			var filter = new WorkshopWorkerListModel();
			
			int count = 0;
			 var resultGet = _WorkshopWorkerBL.ListWorkshopWorkers(filter, out count);
			
			var modelDelete = new WorkshopWorkerDetailModel();
			modelDelete.WorkshopId = resultGet.Data.First().WorkshopId;
			modelDelete.WorkshopName= guid; 
			modelDelete.WorkerName= guid; 
			modelDelete.CommandType = "D";
			 var resultDelete = _WorkshopWorkerBL.DMLWorkshopWorker(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}

