using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.ServiceCallSchedule;
using ODMSCommon.Security;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class ServiceCallScheduleBLTest
	{

		ServiceCallScheduleBL _ServiceCallScheduleBL = new ServiceCallScheduleBL();

		[TestMethod]
		public void ServiceCallScheduleBL_Update_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ServiceCallScheduleViewModel();
			model.ServiceId= 1; 
			model.ServiceDescription= guid; 
			model.CallIntervalMinute= 1; 
			model.LastCallDate= guid; 
			model.NextCallDate= DateTime.Now; 
			model.IsTriggerService= true; 
			model.TriggerServiceName= guid; 
			model.IsResponseLogged= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ServiceCallScheduleBL.Update(model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void ServiceCallScheduleBL_Insert_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ServiceCallScheduleViewModel();
			model.ServiceId= 1; 
			model.ServiceDescription= guid; 
			model.CallIntervalMinute= 1; 
			model.LastCallDate= guid; 
			model.NextCallDate= DateTime.Now; 
			model.IsTriggerService= true; 
			model.TriggerServiceName= guid; 
			model.IsResponseLogged= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ServiceCallScheduleBL.Insert(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void ServiceCallScheduleBL_Update_Insert_1()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ServiceCallScheduleViewModel();
			model.ServiceId= 1; 
			model.ServiceDescription= guid; 
			model.CallIntervalMinute= 1; 
			model.LastCallDate= guid; 
			model.NextCallDate= DateTime.Now; 
			model.IsTriggerService= true; 
			model.TriggerServiceName= guid; 
			model.IsResponseLogged= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ServiceCallScheduleBL.Update(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void ServiceCallScheduleBL_Get_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ServiceCallScheduleViewModel();
			model.ServiceId= 1; 
			model.ServiceDescription= guid; 
			model.CallIntervalMinute= 1; 
			model.LastCallDate= guid; 
			model.NextCallDate= DateTime.Now; 
			model.IsTriggerService= true; 
			model.TriggerServiceName= guid; 
			model.IsResponseLogged= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ServiceCallScheduleBL.Update(UserManager.UserInfo, model);
			
			var filter = new ServiceCallScheduleViewModel();
			filter.ServiceId = result.Model.ServiceId;
			
			 var resultGet = _ServiceCallScheduleBL.Get(filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.ServiceId > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void ServiceCallScheduleBL_GetTimeOnly_GetModel()
		{
		    var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var resultGet = _ServiceCallScheduleBL.GetTimeOnly(guid);
			
			Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void ServiceCallScheduleBL_Get_GetModel_1()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ServiceCallScheduleViewModel();
			model.ServiceId= 1; 
			model.ServiceDescription= guid; 
			model.CallIntervalMinute= 1; 
			model.LastCallDate= guid; 
			model.NextCallDate= DateTime.Now; 
			model.IsTriggerService= true; 
			model.TriggerServiceName= guid; 
			model.IsResponseLogged= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ServiceCallScheduleBL.Update(UserManager.UserInfo, model);
			
			var filter = new ServiceCallScheduleViewModel();
			filter.ServiceId = result.Model.ServiceId;
			
			 var resultGet = _ServiceCallScheduleBL.Get(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.ServiceId > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void ServiceCallScheduleBL_List_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ServiceCallScheduleViewModel();
			model.ServiceId= 1; 
			model.ServiceDescription= guid; 
			model.CallIntervalMinute= 1; 
			model.LastCallDate= guid; 
			model.NextCallDate= DateTime.Now; 
			model.IsTriggerService= true; 
			model.TriggerServiceName= guid; 
			model.IsResponseLogged= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ServiceCallScheduleBL.Update(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new ServiceCallScheduleListModel();
			
			 var resultGet = _ServiceCallScheduleBL.List(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void ServiceCallScheduleBL_Update_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ServiceCallScheduleViewModel();
			model.ServiceId= 1; 
			model.ServiceDescription= guid; 
			model.CallIntervalMinute= 1; 
			model.LastCallDate= guid; 
			model.NextCallDate= DateTime.Now; 
			model.IsTriggerService= true; 
			model.TriggerServiceName= guid; 
			model.IsResponseLogged= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ServiceCallScheduleBL.Update(UserManager.UserInfo, model);
			
			var filter = new ServiceCallScheduleListModel();
			
			int count = 0;
			 var resultGet = _ServiceCallScheduleBL.List(UserManager.UserInfo, filter, out count);
			
			var modelUpdate = new ServiceCallScheduleViewModel();
			modelUpdate.ServiceId = resultGet.Data.First().ServiceId;
			modelUpdate.ServiceDescription= guid; 
			modelUpdate.LastCallDate= guid; 
			modelUpdate.TriggerServiceName= guid; 
			modelUpdate.CommandType = "U";
			 var resultUpdate = _ServiceCallScheduleBL.Update(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void ServiceCallScheduleBL_Update_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ServiceCallScheduleViewModel();
			model.ServiceId= 1; 
			model.ServiceDescription= guid; 
			model.CallIntervalMinute= 1; 
			model.LastCallDate= guid; 
			model.NextCallDate= DateTime.Now; 
			model.IsTriggerService= true; 
			model.TriggerServiceName= guid; 
			model.IsResponseLogged= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ServiceCallScheduleBL.Update(UserManager.UserInfo, model);
			
			var filter = new ServiceCallScheduleListModel();
			
			int count = 0;
			 var resultGet = _ServiceCallScheduleBL.List(UserManager.UserInfo, filter, out count);
			
			var modelDelete = new ServiceCallScheduleViewModel();
			modelDelete.ServiceId = resultGet.Data.First().ServiceId;
			modelDelete.ServiceDescription= guid; 
			modelDelete.LastCallDate= guid; 
			modelDelete.TriggerServiceName= guid; 
			modelDelete.CommandType = "D";
			 var resultDelete = _ServiceCallScheduleBL.Update(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}

		[TestMethod]
		public void ServiceCallScheduleBL_List_GetAll_1()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ServiceCallScheduleViewModel();
			model.ServiceId= 1; 
			model.ServiceDescription= guid; 
			model.CallIntervalMinute= 1; 
			model.LastCallDate= guid; 
			model.NextCallDate= DateTime.Now; 
			model.IsTriggerService= true; 
			model.TriggerServiceName= guid; 
			model.IsResponseLogged= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ServiceCallScheduleBL.Update(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new ServiceCallScheduleViewModel();
			
			 var resultGet = _ServiceCallScheduleBL.List(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void ServiceCallScheduleBL_Update_Update_1()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ServiceCallScheduleViewModel();
			model.ServiceId= 1; 
			model.ServiceDescription= guid; 
			model.CallIntervalMinute= 1; 
			model.LastCallDate= guid; 
			model.NextCallDate= DateTime.Now; 
			model.IsTriggerService= true; 
			model.TriggerServiceName= guid; 
			model.IsResponseLogged= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ServiceCallScheduleBL.Update(UserManager.UserInfo, model);
			
			var filter = new ServiceCallScheduleViewModel();
			
			int count = 0;
			 var resultGet = _ServiceCallScheduleBL.List(UserManager.UserInfo, filter);
			
			var modelUpdate = new ServiceCallScheduleViewModel();
			modelUpdate.ServiceId = resultGet.Data.First().ServiceId;
			modelUpdate.ServiceDescription= guid; 
			modelUpdate.LastCallDate= guid; 
			modelUpdate.TriggerServiceName= guid; 
			modelUpdate.CommandType = "U";
			 var resultUpdate = _ServiceCallScheduleBL.Update(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void ServiceCallScheduleBL_Update_Delete_1()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ServiceCallScheduleViewModel();
			model.ServiceId= 1; 
			model.ServiceDescription= guid; 
			model.CallIntervalMinute= 1; 
			model.LastCallDate= guid; 
			model.NextCallDate= DateTime.Now; 
			model.IsTriggerService= true; 
			model.TriggerServiceName= guid; 
			model.IsResponseLogged= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ServiceCallScheduleBL.Update(UserManager.UserInfo, model);
			
			var filter = new ServiceCallScheduleViewModel();
			
			int count = 0;
			 var resultGet = _ServiceCallScheduleBL.List(UserManager.UserInfo, filter);
			
			var modelDelete = new ServiceCallScheduleViewModel();
			modelDelete.ServiceId = resultGet.Data.First().ServiceId;
			modelDelete.ServiceDescription= guid; 
			modelDelete.LastCallDate= guid; 
			modelDelete.TriggerServiceName= guid; 
			modelDelete.CommandType = "D";
			 var resultDelete = _ServiceCallScheduleBL.Update(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}

