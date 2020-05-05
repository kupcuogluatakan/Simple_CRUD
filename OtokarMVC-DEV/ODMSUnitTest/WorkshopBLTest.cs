using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.Workshop;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class WorkshopBLTest
	{

		WorkshopBL _WorkshopBL = new WorkshopBL();

		[TestMethod]
		public void WorkshopBL_DMLWorkshop_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new WorkshopDetailModel();
			model.Name= guid; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _WorkshopBL.DMLWorkshop(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void WorkshopBL_GetWorkshop_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new WorkshopDetailModel();
			model.Name= guid; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _WorkshopBL.DMLWorkshop(UserManager.UserInfo, model);
			
			var filter = new WorkshopDetailModel();
			filter.Id = result.Model.Id;
			filter.DealerId = UserManager.UserInfo.DealerID; 
			
			 var resultGet = _WorkshopBL.GetWorkshop(filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.Id > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void WorkshopBL_ListWorkshops_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new WorkshopDetailModel();
			model.Name= guid; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _WorkshopBL.DMLWorkshop(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new WorkshopListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			
			 var resultGet = _WorkshopBL.ListWorkshops(filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void WorkshopBL_DMLWorkshop_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new WorkshopDetailModel();
			model.Name= guid; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _WorkshopBL.DMLWorkshop(UserManager.UserInfo, model);
			
			var filter = new WorkshopListModel();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			
			int count = 0;
			 var resultGet = _WorkshopBL.ListWorkshops(filter, out count);
			
			var modelUpdate = new WorkshopDetailModel();
			modelUpdate.Id = resultGet.Data.First().Id;
			modelUpdate.Name= guid; 
			modelUpdate.DealerId = UserManager.UserInfo.DealerID; 
			modelUpdate.DealerName= guid; 
			
			
			
			modelUpdate.CommandType = "U";
			 var resultUpdate = _WorkshopBL.DMLWorkshop(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void WorkshopBL_DMLWorkshop_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new WorkshopDetailModel();
			model.Name= guid; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.DealerName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _WorkshopBL.DMLWorkshop(UserManager.UserInfo, model);
			
			var filter = new WorkshopListModel();
			filter.DealerId = UserManager.UserInfo.DealerID;  
			
			int count = 0;
			 var resultGet = _WorkshopBL.ListWorkshops(filter, out count);
			
			var modelDelete = new WorkshopDetailModel();
			modelDelete.Id = resultGet.Data.First().Id;
			modelDelete.Name= guid; 
			modelDelete.DealerId = UserManager.UserInfo.DealerID; 
			modelDelete.DealerName= guid; 
			
			
			
			modelDelete.CommandType = "D";
			 var resultDelete = _WorkshopBL.DMLWorkshop(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}

