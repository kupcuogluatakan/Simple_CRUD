using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.Schedule;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class SchedulerBLTest
	{

		SchedulerBL _SchedulerBL = new SchedulerBL();

		
		[TestMethod]
		public void SchedulerBL_Insert_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ScheduleViewModel();
			model.Title= guid; 
			model.Description= guid; 
			model.IsAllDay= true; 
			model.Start= DateTime.Now; 
			model.End= DateTime.Now; 
			model.RecurrenceRule= guid; 
			model.RecurrenceException= guid; 
			model.WorkStat= true; 
			model.Monday= true; 
			model.Tuesday= true; 
			model.Wednesday= true; 
			model.Thursday= true; 
			model.Friday= true; 
			model.Saturday= true; 
			model.Sunday= true; 
			model.WorkHourStart= DateTime.Now; 
			model.WorkHourEnd= DateTime.Now; 
			model.LunchBreakStart= DateTime.Now; 
			model.LunchBreakEnd= DateTime.Now; 
			model.CurrentWeek= DateTime.Now; 
			model.isToday= true; 
			model.Qty= 1; 
			model.NonAppId= guid; 
			model.OptionValue= guid; 
			model.VehicleId = 29627; 
			model.VinNo= guid; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.VehicleModelName= guid; 
			model.VehicleTypeName= guid; 
			model.CustomerName= guid; 
			model.AppointmentTypeName= guid; 
			model.DealerName= guid; 
			model.VehicleIdVehiclePlate= guid; 
			model.ContactName= guid; 
			model.ContactLastName= guid; 
			model.ContactPhone= guid; 
			model.ContactAddress= guid; 
			model.VehiclePlate= guid; 
			model.VehicleColor= guid; 
			model.VehicleModelCode= guid; 
			model.VehicleType= guid; 
			model.AppointmentDate= DateTime.Now; 
			model.ComplaintDescription= guid; 
			model.AppointmentStartDate= DateTime.Now; 
			model.AppointmentEndDate= DateTime.Now; 
			model.DeliveryEstimateDateString= guid; 
			model.VehicleVin= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SchedulerBL.Insert(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void SchedulerBL_Update_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ScheduleViewModel();
			model.Title= guid; 
			model.Description= guid; 
			model.IsAllDay= true; 
			model.Start= DateTime.Now; 
			model.End= DateTime.Now; 
			model.RecurrenceRule= guid; 
			model.RecurrenceException= guid; 
			model.WorkStat= true; 
			model.Monday= true; 
			model.Tuesday= true; 
			model.Wednesday= true; 
			model.Thursday= true; 
			model.Friday= true; 
			model.Saturday= true; 
			model.Sunday= true; 
			model.WorkHourStart= DateTime.Now; 
			model.WorkHourEnd= DateTime.Now; 
			model.LunchBreakStart= DateTime.Now; 
			model.LunchBreakEnd= DateTime.Now; 
			model.CurrentWeek= DateTime.Now; 
			model.isToday= true; 
			model.Qty= 1; 
			model.NonAppId= guid; 
			model.OptionValue= guid; 
			model.VehicleId = 29627; 
			model.VinNo= guid; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.VehicleModelName= guid; 
			model.VehicleTypeName= guid; 
			model.CustomerName= guid; 
			model.AppointmentTypeName= guid; 
			model.DealerName= guid; 
			model.VehicleIdVehiclePlate= guid; 
			model.ContactName= guid; 
			model.ContactLastName= guid; 
			model.ContactPhone= guid; 
			model.ContactAddress= guid; 
			model.VehiclePlate= guid; 
			model.VehicleColor= guid; 
			model.VehicleModelCode= guid; 
			model.VehicleType= guid; 
			model.AppointmentDate= DateTime.Now; 
			model.ComplaintDescription= guid; 
			model.AppointmentStartDate= DateTime.Now; 
			model.AppointmentEndDate= DateTime.Now; 
			model.DeliveryEstimateDateString= guid; 
			model.VehicleVin= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SchedulerBL.Update(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void SchedulerBL_Get_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ScheduleViewModel();
			model.Title= guid; 
			model.Description= guid; 
			model.IsAllDay= true; 
			model.Start= DateTime.Now; 
			model.End= DateTime.Now; 
			model.RecurrenceRule= guid; 
			model.RecurrenceException= guid; 
			model.WorkStat= true; 
			model.Monday= true; 
			model.Tuesday= true; 
			model.Wednesday= true; 
			model.Thursday= true; 
			model.Friday= true; 
			model.Saturday= true; 
			model.Sunday= true; 
			model.WorkHourStart= DateTime.Now; 
			model.WorkHourEnd= DateTime.Now; 
			model.LunchBreakStart= DateTime.Now; 
			model.LunchBreakEnd= DateTime.Now; 
			model.CurrentWeek= DateTime.Now; 
			model.isToday= true; 
			model.Qty= 1; 
			model.NonAppId= guid; 
			model.OptionValue= guid; 
			model.VehicleId = 29627; 
			model.VinNo= guid; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.VehicleModelName= guid; 
			model.VehicleTypeName= guid; 
			model.CustomerName= guid; 
			model.AppointmentTypeName= guid; 
			model.DealerName= guid; 
			model.VehicleIdVehiclePlate= guid; 
			model.ContactName= guid; 
			model.ContactLastName= guid; 
			model.ContactPhone= guid; 
			model.ContactAddress= guid; 
			model.VehiclePlate= guid; 
			model.VehicleColor= guid; 
			model.VehicleModelCode= guid; 
			model.VehicleType= guid; 
			model.AppointmentDate= DateTime.Now; 
			model.ComplaintDescription= guid; 
			model.AppointmentStartDate= DateTime.Now; 
			model.AppointmentEndDate= DateTime.Now; 
			model.DeliveryEstimateDateString= guid; 
			model.VehicleVin= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SchedulerBL.Update(UserManager.UserInfo, model);
			
			var filter = new ScheduleViewModel();
			filter.AppointmentId = result.Model.AppointmentId;
			filter.VehicleId = 29627; 
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.VehicleModelCode = guid; 
			
			 var resultGet = _SchedulerBL.Get(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.AppointmentId > 0 && resultGet.IsSuccess);
		}
        
		[TestMethod]
		public void SchedulerBL_Update_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ScheduleViewModel();
			model.Title= guid; 
			model.Description= guid; 
			model.IsAllDay= true; 
			model.Start= DateTime.Now; 
			model.End= DateTime.Now; 
			model.RecurrenceRule= guid; 
			model.RecurrenceException= guid; 
			model.WorkStat= true; 
			model.Monday= true; 
			model.Tuesday= true; 
			model.Wednesday= true; 
			model.Thursday= true; 
			model.Friday= true; 
			model.Saturday= true; 
			model.Sunday= true; 
			model.WorkHourStart= DateTime.Now; 
			model.WorkHourEnd= DateTime.Now; 
			model.LunchBreakStart= DateTime.Now; 
			model.LunchBreakEnd= DateTime.Now; 
			model.CurrentWeek= DateTime.Now; 
			model.isToday= true; 
			model.Qty= 1; 
			model.NonAppId= guid; 
			model.OptionValue= guid; 
			model.VehicleId = 29627; 
			model.VinNo= guid; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.VehicleModelName= guid; 
			model.VehicleTypeName= guid; 
			model.CustomerName= guid; 
			model.AppointmentTypeName= guid; 
			model.DealerName= guid; 
			model.VehicleIdVehiclePlate= guid; 
			model.ContactName= guid; 
			model.ContactLastName= guid; 
			model.ContactPhone= guid; 
			model.ContactAddress= guid; 
			model.VehiclePlate= guid; 
			model.VehicleColor= guid; 
			model.VehicleModelCode= guid; 
			model.VehicleType= guid; 
			model.AppointmentDate= DateTime.Now; 
			model.ComplaintDescription= guid; 
			model.AppointmentStartDate= DateTime.Now; 
			model.AppointmentEndDate= DateTime.Now; 
			model.DeliveryEstimateDateString= guid; 
			model.VehicleVin= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SchedulerBL.Update(UserManager.UserInfo, model);
			
			var filter = new ScheduleViewModel();
			filter.VehicleId = 29627; 
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.VehicleModelCode = guid; 
			
			int count = 0;
			 var resultGet = _SchedulerBL.List(UserManager.UserInfo, filter, out count);
			
			var modelDelete = new ScheduleViewModel();
			modelDelete.Title= guid; 
			modelDelete.Description= guid; 
			modelDelete.RecurrenceRule= guid; 
			modelDelete.RecurrenceException= guid; 
			modelDelete.NonAppId= guid; 
			modelDelete.OptionValue= guid; 
			modelDelete.VehicleId = 29627; 
			modelDelete.VinNo= guid; 
			modelDelete.DealerId = UserManager.UserInfo.DealerID; 
			modelDelete.VehicleModelName= guid; 
			modelDelete.VehicleTypeName= guid; 
			modelDelete.CustomerName= guid; 
			modelDelete.AppointmentTypeName= guid; 
			modelDelete.DealerName= guid; 
			modelDelete.VehicleIdVehiclePlate= guid; 
			modelDelete.ContactName= guid; 
			modelDelete.ContactLastName= guid; 
			modelDelete.ContactPhone= guid; 
			modelDelete.ContactAddress= guid; 
			modelDelete.VehiclePlate= guid; 
			modelDelete.VehicleColor= guid; 
			modelDelete.VehicleModelCode= guid; 
			modelDelete.VehicleType= guid; 
			
			modelDelete.ComplaintDescription= guid; 
			
			
			modelDelete.DeliveryEstimateDateString= guid; 
			modelDelete.VehicleVin= guid; 
			
			
			
			modelDelete.CommandType = "D";
			 var resultDelete = _SchedulerBL.Update(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}

		[TestMethod]
		public void SchedulerBL_List_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ScheduleViewModel();
			model.Title= guid; 
			model.Description= guid; 
			model.IsAllDay= true; 
			model.Start= DateTime.Now; 
			model.End= DateTime.Now; 
			model.RecurrenceRule= guid; 
			model.RecurrenceException= guid; 
			model.WorkStat= true; 
			model.Monday= true; 
			model.Tuesday= true; 
			model.Wednesday= true; 
			model.Thursday= true; 
			model.Friday= true; 
			model.Saturday= true; 
			model.Sunday= true; 
			model.WorkHourStart= DateTime.Now; 
			model.WorkHourEnd= DateTime.Now; 
			model.LunchBreakStart= DateTime.Now; 
			model.LunchBreakEnd= DateTime.Now; 
			model.CurrentWeek= DateTime.Now; 
			model.isToday= true; 
			model.Qty= 1; 
			model.NonAppId= guid; 
			model.OptionValue= guid; 
			model.VehicleId = 29627; 
			model.VinNo= guid; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.VehicleModelName= guid; 
			model.VehicleTypeName= guid; 
			model.CustomerName= guid; 
			model.AppointmentTypeName= guid; 
			model.DealerName= guid; 
			model.VehicleIdVehiclePlate= guid; 
			model.ContactName= guid; 
			model.ContactLastName= guid; 
			model.ContactPhone= guid; 
			model.ContactAddress= guid; 
			model.VehiclePlate= guid; 
			model.VehicleColor= guid; 
			model.VehicleModelCode= guid; 
			model.VehicleType= guid; 
			model.AppointmentDate= DateTime.Now; 
			model.ComplaintDescription= guid; 
			model.AppointmentStartDate= DateTime.Now; 
			model.AppointmentEndDate= DateTime.Now; 
			model.DeliveryEstimateDateString= guid; 
			model.VehicleVin= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SchedulerBL.Update(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new ScheduleViewModel();
			filter.VehicleId = 29627; 
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.VehicleModelCode = guid; 
			
			 var resultGet = _SchedulerBL.List(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void SchedulerBL_Update_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ScheduleViewModel();
			model.Title= guid; 
			model.Description= guid; 
			model.IsAllDay= true; 
			model.Start= DateTime.Now; 
			model.End= DateTime.Now; 
			model.RecurrenceRule= guid; 
			model.RecurrenceException= guid; 
			model.WorkStat= true; 
			model.Monday= true; 
			model.Tuesday= true; 
			model.Wednesday= true; 
			model.Thursday= true; 
			model.Friday= true; 
			model.Saturday= true; 
			model.Sunday= true; 
			model.WorkHourStart= DateTime.Now; 
			model.WorkHourEnd= DateTime.Now; 
			model.LunchBreakStart= DateTime.Now; 
			model.LunchBreakEnd= DateTime.Now; 
			model.CurrentWeek= DateTime.Now; 
			model.isToday= true; 
			model.Qty= 1; 
			model.NonAppId= guid; 
			model.OptionValue= guid; 
			model.VehicleId = 29627; 
			model.VinNo= guid; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.VehicleModelName= guid; 
			model.VehicleTypeName= guid; 
			model.CustomerName= guid; 
			model.AppointmentTypeName= guid; 
			model.DealerName= guid; 
			model.VehicleIdVehiclePlate= guid; 
			model.ContactName= guid; 
			model.ContactLastName= guid; 
			model.ContactPhone= guid; 
			model.ContactAddress= guid; 
			model.VehiclePlate= guid; 
			model.VehicleColor= guid; 
			model.VehicleModelCode= guid; 
			model.VehicleType= guid; 
			model.AppointmentDate= DateTime.Now; 
			model.ComplaintDescription= guid; 
			model.AppointmentStartDate= DateTime.Now; 
			model.AppointmentEndDate= DateTime.Now; 
			model.DeliveryEstimateDateString= guid; 
			model.VehicleVin= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SchedulerBL.Update(UserManager.UserInfo, model);
			
			var filter = new ScheduleViewModel();
			filter.VehicleId = 29627; 
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.VehicleModelCode = guid; 
			
			int count = 0;
			 var resultGet = _SchedulerBL.List(UserManager.UserInfo, filter);
			
			var modelUpdate = new ScheduleViewModel();
			modelUpdate.Title= guid; 
			modelUpdate.Description= guid; 
			modelUpdate.RecurrenceRule= guid; 
			modelUpdate.RecurrenceException= guid; 
			modelUpdate.NonAppId= guid; 
			modelUpdate.OptionValue= guid; 
			modelUpdate.VehicleId = 29627; 
			modelUpdate.VinNo= guid; 
			modelUpdate.DealerId = UserManager.UserInfo.DealerID; 
			modelUpdate.VehicleModelName= guid; 
			modelUpdate.VehicleTypeName= guid; 
			modelUpdate.CustomerName= guid; 
			modelUpdate.AppointmentTypeName= guid; 
			modelUpdate.DealerName= guid; 
			modelUpdate.VehicleIdVehiclePlate= guid; 
			modelUpdate.ContactName= guid; 
			modelUpdate.ContactLastName= guid; 
			modelUpdate.ContactPhone= guid; 
			modelUpdate.ContactAddress= guid; 
			modelUpdate.VehiclePlate= guid; 
			modelUpdate.VehicleColor= guid; 
			modelUpdate.VehicleModelCode= guid; 
			modelUpdate.VehicleType= guid; 
			modelUpdate.ComplaintDescription= guid; 
			modelUpdate.DeliveryEstimateDateString= guid; 
			modelUpdate.VehicleVin= guid; 
			modelUpdate.CommandType = "U";
			 var resultUpdate = _SchedulerBL.Update(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void SchedulerBL_Update_Delete_1()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ScheduleViewModel();
			model.Title= guid; 
			model.Description= guid; 
			model.IsAllDay= true; 
			model.Start= DateTime.Now; 
			model.End= DateTime.Now; 
			model.RecurrenceRule= guid; 
			model.RecurrenceException= guid; 
			model.WorkStat= true; 
			model.Monday= true; 
			model.Tuesday= true; 
			model.Wednesday= true; 
			model.Thursday= true; 
			model.Friday= true; 
			model.Saturday= true; 
			model.Sunday= true; 
			model.WorkHourStart= DateTime.Now; 
			model.WorkHourEnd= DateTime.Now; 
			model.LunchBreakStart= DateTime.Now; 
			model.LunchBreakEnd= DateTime.Now; 
			model.CurrentWeek= DateTime.Now; 
			model.isToday= true; 
			model.Qty= 1; 
			model.NonAppId= guid; 
			model.OptionValue= guid; 
			model.VehicleId = 29627; 
			model.VinNo= guid; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.VehicleModelName= guid; 
			model.VehicleTypeName= guid; 
			model.CustomerName= guid; 
			model.AppointmentTypeName= guid; 
			model.DealerName= guid; 
			model.VehicleIdVehiclePlate= guid; 
			model.ContactName= guid; 
			model.ContactLastName= guid; 
			model.ContactPhone= guid; 
			model.ContactAddress= guid; 
			model.VehiclePlate= guid; 
			model.VehicleColor= guid; 
			model.VehicleModelCode= guid; 
			model.VehicleType= guid; 
			model.AppointmentDate= DateTime.Now; 
			model.ComplaintDescription= guid; 
			model.AppointmentStartDate= DateTime.Now; 
			model.AppointmentEndDate= DateTime.Now; 
			model.DeliveryEstimateDateString= guid; 
			model.VehicleVin= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SchedulerBL.Update(UserManager.UserInfo, model);
			
			var filter = new ScheduleViewModel();
			filter.VehicleId = 29627; 
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.VehicleModelCode = guid; 
			
			int count = 0;
			 var resultGet = _SchedulerBL.List(UserManager.UserInfo, filter);
			
			var modelDelete = new ScheduleViewModel();
			modelDelete.Title= guid; 
			modelDelete.Description= guid; 
			modelDelete.RecurrenceRule= guid; 
			modelDelete.RecurrenceException= guid; 
			modelDelete.NonAppId= guid; 
			modelDelete.OptionValue= guid; 
			modelDelete.VehicleId = 29627; 
			modelDelete.VinNo= guid; 
			modelDelete.DealerId = UserManager.UserInfo.DealerID; 
			modelDelete.VehicleModelName= guid; 
			modelDelete.VehicleTypeName= guid; 
			modelDelete.CustomerName= guid; 
			modelDelete.AppointmentTypeName= guid; 
			modelDelete.DealerName= guid; 
			modelDelete.VehicleIdVehiclePlate= guid; 
			modelDelete.ContactName= guid; 
			modelDelete.ContactLastName= guid; 
			modelDelete.ContactPhone= guid; 
			modelDelete.ContactAddress= guid; 
			modelDelete.VehiclePlate= guid; 
			modelDelete.VehicleColor= guid; 
			modelDelete.VehicleModelCode= guid; 
			modelDelete.VehicleType= guid; 
			modelDelete.ComplaintDescription= guid; 
			modelDelete.DeliveryEstimateDateString= guid; modelDelete.VehicleVin= guid; 
			modelDelete.CommandType = "D";
			 var resultDelete = _SchedulerBL.Update(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}

