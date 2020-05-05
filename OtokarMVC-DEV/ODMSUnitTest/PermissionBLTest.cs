using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.Permission;
using System;
using ODMSModel.Shared;


namespace ODMSUnitTest
{

	[TestClass]
	public class PermissionBLTest
	{

		PermissionBL _PermissionBL = new PermissionBL();

		[TestMethod]
		public void PermissionBL_GetPermission_GetModel()
		{
			
			var filter = new PermissionIndexViewModel();
			filter.PermissionId = 9;
			filter.MultiLanguageContentAsText = "TR || TEST"; 
			filter.PermissionCode = "Bayi Tanýmlama Ekran yetkisi"; 
			
			 var resultGet = _PermissionBL.GetPermission(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.PermissionId > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void PermissionBL_GetUserPermissions_GetModel()
		{
			 var resultGet = _PermissionBL.GetUserPermissions(UserManager.UserInfo, UserManager.UserInfo.UserId);
			
			Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void PermissionBL_ListPermissions_GetAll()
		{
		    var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            int count = 0;
			var filter = new PermissionListModel();
			filter.PermissionCode = guid; 
			 var resultGet = _PermissionBL.ListPermissions(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}


	}

}

