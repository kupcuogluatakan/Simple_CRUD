using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.GuaranteeAuthorityGroupVehicleModels;


namespace ODMSUnitTest
{

	[TestClass]
	public class GuaranteeAuthorityGroupVehicleBLTest
	{

		GuaranteeAuthorityGroupVehicleBL _GuaranteeAuthorityGroupVehicleBL = new GuaranteeAuthorityGroupVehicleBL();

		[TestMethod]
		public void GuaranteeAuthorityGroupVehicleBL_SaveGuaranteeAuthorityGroupVehicle_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new GuaranteeAuthorityGroupVehicleSaveModel();
			model.id= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _GuaranteeAuthorityGroupVehicleBL.SaveGuaranteeAuthorityGroupVehicle(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void GuaranteeAuthorityGroupVehicleBL_ListGuaranteeAuthorityGroupVehicle_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new GuaranteeAuthorityGroupVehicleSaveModel();
			model.id= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _GuaranteeAuthorityGroupVehicleBL.SaveGuaranteeAuthorityGroupVehicle(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new GuaranteeAuthorityGroupVehicleListModel();
			filter.ModelKod = "ATLAS"; 
			
			 var resultGet = _GuaranteeAuthorityGroupVehicleBL.ListGuaranteeAuthorityGroupVehicle(filter);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

	
		[TestMethod]
		public void GuaranteeAuthorityGroupVehicleBL_ListGuaranteeAuthorityGroupVehicleNotInclude_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new GuaranteeAuthorityGroupVehicleSaveModel();
			model.id= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _GuaranteeAuthorityGroupVehicleBL.SaveGuaranteeAuthorityGroupVehicle(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new GuaranteeAuthorityGroupVehicleListModel();
			filter.ModelKod = "ATLAS"; 
			
			 var resultGet = _GuaranteeAuthorityGroupVehicleBL.ListGuaranteeAuthorityGroupVehicleNotInclude(filter);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		

	}

}

