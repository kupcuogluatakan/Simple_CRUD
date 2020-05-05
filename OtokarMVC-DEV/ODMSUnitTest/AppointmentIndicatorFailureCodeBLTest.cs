using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.AppointmentIndicatorFailureCode;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class AppointmentIndicatorFailureCodeBLTest
	{

		AppointmentIndicatorFailureCodeBL _AppointmentIndicatorFailureCodeBL = new AppointmentIndicatorFailureCodeBL();

		[TestMethod]
		public void AppointmentIndicatorFailureCodeBL_DMLAppointmentIndicatorFailureCode_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new AppointmentIndicatorFailureCodeViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.IdAppointmentIndicatorFailureCode= 1; 
			model.Code= guid; 
			model.AdminDesc= guid; 
			model.Description= guid; 
			model.IsActive= true; 
			model.IsActiveName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _AppointmentIndicatorFailureCodeBL.DMLAppointmentIndicatorFailureCode(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void AppointmentIndicatorFailureCodeBL_GetAppointmentIndicatorFailureCode_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new AppointmentIndicatorFailureCodeViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.IdAppointmentIndicatorFailureCode= 1; 
			model.Code= guid; 
			model.AdminDesc= guid; 
			model.Description= guid; 
			model.IsActive= true; 
			model.IsActiveName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _AppointmentIndicatorFailureCodeBL.DMLAppointmentIndicatorFailureCode(UserManager.UserInfo, model);
			
			var filter = new AppointmentIndicatorFailureCodeViewModel();
			filter.Code = guid; 
			
			 var resultGet = _AppointmentIndicatorFailureCodeBL.GetAppointmentIndicatorFailureCode(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.IdAppointmentIndicatorFailureCode > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void AppointmentIndicatorFailureCodeBL_ListAppointmentIndicatorFailureCode_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new AppointmentIndicatorFailureCodeViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.IdAppointmentIndicatorFailureCode= 1; 
			model.Code= guid; 
			model.AdminDesc= guid; 
			model.Description= guid; 
			model.IsActive= true; 
			model.IsActiveName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _AppointmentIndicatorFailureCodeBL.DMLAppointmentIndicatorFailureCode(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new AppointmentIndicatorFailureCodeListModel();
			filter.IdAppointmentIndicatorFailureCode = result.Model.IdAppointmentIndicatorFailureCode; 
			
			 var resultGet = _AppointmentIndicatorFailureCodeBL.ListAppointmentIndicatorFailureCode(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}


	}

}

