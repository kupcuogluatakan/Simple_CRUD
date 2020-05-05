using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.PdiGifApproveGroupMembers;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class PdiGifApproveGroupMembersBLTest
	{

		PdiGifApproveGroupMembersBL _PdiGifApproveGroupMembersBL = new PdiGifApproveGroupMembersBL();

		[TestMethod]
		public void PdiGifApproveGroupMembersBL_Save_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PdiGifApproveGroupMembersModel();
			model.GroupId= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PdiGifApproveGroupMembersBL.Save(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void PdiGifApproveGroupMembersBL_ListPdiGifApproveGroupMembersIncluded_GetAll()
		{
			 var resultGet = _PdiGifApproveGroupMembersBL.ListPdiGifApproveGroupMembersIncluded(1);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void PdiGifApproveGroupMembersBL_Save_Update()
		{
			
			var modelUpdate = new PdiGifApproveGroupMembersModel();
			modelUpdate.GroupId = 1;
			modelUpdate.CommandType = "U";
			 var resultUpdate = _PdiGifApproveGroupMembersBL.Save(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void PdiGifApproveGroupMembersBL_Save_Delete()
		{
			
			var modelDelete = new PdiGifApproveGroupMembersModel();
			modelDelete.GroupId = 1;
			modelDelete.CommandType = "D";
			 var resultDelete = _PdiGifApproveGroupMembersBL.Save(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}

		[TestMethod]
		public void PdiGifApproveGroupMembersBL_ListPdiGifApproveGroupMembersExcluded_GetAll()
		{
			 var resultGet = _PdiGifApproveGroupMembersBL.ListPdiGifApproveGroupMembersExcluded(1);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        
	}

}

