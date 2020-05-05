using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.Role;
using ODMSModel.Shared;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class RoleBLTest
	{

		RoleBL _RoleBL = new RoleBL();

		[TestMethod]
		public void RoleBL_DMLRole_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new RoleIndexViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.isValidText= guid; 
			model.RoleId= 1; 
			model.AdminDesc= guid; 
			model.IsSystemRole= true; 
			model.IsSystemRoleName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _RoleBL.DMLRole(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void RoleBL_GetRole_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new RoleIndexViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.isValidText= guid; 
			model.RoleId= 1; 
			model.AdminDesc= guid; 
			model.IsSystemRole= true; 
			model.IsSystemRoleName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _RoleBL.DMLRole(UserManager.UserInfo, model);
			
			var filter = new RoleIndexViewModel();
			filter.RoleId = result.Model.RoleId;
			filter.MultiLanguageContentAsText = "TR || TEST"; 
			
			 var resultGet = _RoleBL.GetRole(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.RoleId > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void RoleBL_ListRoles_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new RoleIndexViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.isValidText= guid; 
			model.RoleId= 1; 
			model.AdminDesc= guid; 
			model.IsSystemRole= true; 
			model.IsSystemRoleName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _RoleBL.DMLRole(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new RoleListModel();
			
			 var resultGet = _RoleBL.ListRoles(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        [TestMethod]
		public void RoleBL_DMLRole_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new RoleIndexViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.isValidText= guid; 
			model.RoleId= 1; 
			model.AdminDesc= guid; 
			model.IsSystemRole= true; 
			model.IsSystemRoleName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _RoleBL.DMLRole(UserManager.UserInfo, model);
			
			var filter = new RoleListModel();
			
			int count = 0;
			 var resultGet = _RoleBL.ListRoles(UserManager.UserInfo, filter, out count);
			
			var modelDelete = new RoleIndexViewModel();
			modelDelete.RoleId = resultGet.Data.First().RoleId;
			modelDelete.MultiLanguageContentAsText = "TR || TEST"; 
			modelDelete.isValidText= guid; 
			
			modelDelete.AdminDesc= guid; 
			
			modelDelete.IsSystemRoleName= guid; 
			
			
			
			modelDelete.CommandType = "D";
			 var resultDelete = _RoleBL.DMLRole(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}

		[TestMethod]
		public void RoleBL_ListRoleTypeAsSelectListItem_GetAll()
		{
			 var resultGet = RoleBL.ListRoleTypeAsSelectListItem(UserManager.UserInfo, true);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        
		[TestMethod]
		public void RoleBL_ListRoleTypeCombo_GetAll()
		{
			 var resultGet = RoleBL.ListRoleTypeCombo(UserManager.UserInfo, true);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        
		[TestMethod]
		public void RoleBL_ListRoleTypeComboByUserType_GetAll()
		{
			 var resultGet = RoleBL.ListRoleTypeComboByUserType(UserManager.UserInfo, true,true);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void RoleBL_DMLRole_Update()
		{
		    var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var modelUpdate = new RoleIndexViewModel();
			modelUpdate.MultiLanguageContentAsText = "TR || TEST"; 
			modelUpdate.isValidText= guid; 
			modelUpdate.AdminDesc= guid; 
			modelUpdate.IsSystemRoleName= guid; 
			modelUpdate.CommandType = "U";
			 var resultUpdate = _RoleBL.DMLRole(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}
        

	}

}

