using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.PDIGOSApproveGroupVehicleModels;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class PDIGOSApproveGroupVehicleModelsBLTest
	{

		PDIGOSApproveGroupVehicleModelsBL _PDIGOSApproveGroupVehicleModelsBL = new PDIGOSApproveGroupVehicleModelsBL();

		[TestMethod]
		public void PDIGOSApproveGroupVehicleModelsBL_Save_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PDIGOSApproveGroupVehicleModelsModel();
			model.GroupId= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PDIGOSApproveGroupVehicleModelsBL.Save(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void PDIGOSApproveGroupVehicleModelsBL_ListPDIGOSApproveGroupVehicleModelsIncluded_GetAll()
		{
			 var resultGet = _PDIGOSApproveGroupVehicleModelsBL.ListPDIGOSApproveGroupVehicleModelsIncluded(1);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void PDIGOSApproveGroupVehicleModelsBL_Save_Update()
		{
			
			var modelUpdate = new PDIGOSApproveGroupVehicleModelsModel();
			modelUpdate.GroupId = 1;
			
			
			
			
			modelUpdate.CommandType = "U";
			 var resultUpdate = _PDIGOSApproveGroupVehicleModelsBL.Save(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void PDIGOSApproveGroupVehicleModelsBL_Save_Delete()
		{
			
			var modelDelete = new PDIGOSApproveGroupVehicleModelsModel();
			modelDelete.GroupId =1;
			modelDelete.CommandType = "D";
			 var resultDelete = _PDIGOSApproveGroupVehicleModelsBL.Save(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}

		[TestMethod]
		public void PDIGOSApproveGroupVehicleModelsBL_ListPDIGOSApproveGroupVehicleModelsExcluded_GetAll()
		{
			 var resultGet = _PDIGOSApproveGroupVehicleModelsBL.ListPDIGOSApproveGroupVehicleModelsExcluded(1);
			
			Assert.IsTrue(resultGet.Total > 0);
		}


	}

}

