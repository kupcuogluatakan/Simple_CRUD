using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.RolePermission;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class RolePermissionBLTest
	{

		RolePermissionBL _RolePermissionBL = new RolePermissionBL();

		[TestMethod]
		public void RolePermissionBL_Save_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SaveModel();
			model.RoleId= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _RolePermissionBL.Save(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void RolePermissionBL_ListRoles_GetAll()
		{
			 var resultGet = _RolePermissionBL.ListRoles(UserManager.UserInfo);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        
		[TestMethod]
		public void RolePermissionBL_ListPermissionsIncludedInRole_GetAll()
		{
			 var resultGet = _RolePermissionBL.ListPermissionsIncludedInRole(UserManager.UserInfo, 1);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        
		[TestMethod]
		public void RolePermissionBL_ListPermissionsNotIncludedInRole_GetAll()
		{
			 var resultGet = _RolePermissionBL.ListPermissionsNotIncludedInRole(UserManager.UserInfo, 1);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void RolePermissionBL_Save_Update()
		{
			
			var modelUpdate = new SaveModel();
			modelUpdate.CommandType = "U";
			 var resultUpdate = _RolePermissionBL.Save(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void RolePermissionBL_Save_Delete()
		{
			
			var modelDelete = new SaveModel();
			modelDelete.CommandType = "D";
			 var resultDelete = _RolePermissionBL.Save(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}

