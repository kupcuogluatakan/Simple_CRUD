using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.SparePartClassCode;
using System;
using ODMSBusiness.Business;


namespace ODMSUnitTest
{

	[TestClass]
	public class PartWithModelPairingBLTest
	{

		PartWithModelPairingBL _PartWithModelPairingBL = new PartWithModelPairingBL();

		[TestMethod]
		public void PartWithModelPairingBL_Save_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SaveModel();
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PartWithModelPairingBL.Save(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void PartWithModelPairingBL_ListCodes_GetAll()
		{
            var resultGet = _PartWithModelPairingBL.ListCodes(UserManager.UserInfo);
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void PartWithModelPairingBL_Save_Update()
		{
			var modelUpdate = new SaveModel();
			modelUpdate.CommandType = "U";
			 var resultUpdate = _PartWithModelPairingBL.Save(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}
        
		[TestMethod]
		public void PartWithModelPairingBL_ListCodesCombo_GetAll()
		{
			 var resultGet = _PartWithModelPairingBL.ListCodesCombo(UserManager.UserInfo);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
		[TestMethod]
		public void PartWithModelPairingBL_ListIncludedCode_GetAll()
		{
			 var resultGet = _PartWithModelPairingBL.ListIncludedCode(UserManager.UserInfo, "10");
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        
		[TestMethod]
		public void PartWithModelPairingBL_ListNotIncludedCode_GetAll()
		{
			 var resultGet = _PartWithModelPairingBL.ListNotIncludedCode(UserManager.UserInfo, "10");
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		
		[TestMethod]
		public void PartWithModelPairingBL_Save_Delete()
		{
			
			var modelDelete = new SaveModel();
			modelDelete.Code = "10";
			modelDelete.CommandType = "D";
			 var resultDelete = _PartWithModelPairingBL.Save(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}

