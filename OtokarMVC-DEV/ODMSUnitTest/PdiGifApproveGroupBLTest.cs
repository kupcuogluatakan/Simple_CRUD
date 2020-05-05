using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.PdiGifApproveGroup;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class PdiGifApproveGroupBLTest
	{

		PdiGifApproveGroupBL _PdiGifApproveGroupBL = new PdiGifApproveGroupBL();

		[TestMethod]
		public void PdiGifApproveGroupBL_DMLPdiGifApproveGroup_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PdiGifApproveGroupViewModel();
			model.GroupId= 1; 
			model.GroupName= guid; 
			model.MailList= guid; 
			model.IsActiveString= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PdiGifApproveGroupBL.DMLPdiGifApproveGroup(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void PdiGifApproveGroupBL_GetPdiGifApproveGroup_GetModel()
		{
			 var resultGet = _PdiGifApproveGroupBL.GetPdiGifApproveGroup(UserManager.UserInfo, 3);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.GroupId > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void PdiGifApproveGroupBL_ListPdiGifApproveGroups_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PdiGifApproveGroupViewModel();
			model.GroupId= 1; 
			model.GroupName= guid; 
			model.MailList= guid; 
			model.IsActiveString= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PdiGifApproveGroupBL.DMLPdiGifApproveGroup(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new PdiGifApproveGroupListModel();
			
			 var resultGet = _PdiGifApproveGroupBL.ListPdiGifApproveGroups(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void PdiGifApproveGroupBL_DMLPdiGifApproveGroup_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PdiGifApproveGroupViewModel();
			model.GroupId= 1; 
			model.GroupName= guid; 
			model.MailList= guid; 
			model.IsActiveString= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PdiGifApproveGroupBL.DMLPdiGifApproveGroup(UserManager.UserInfo, model);
			
			var filter = new PdiGifApproveGroupListModel();
			
			int count = 0;
			 var resultGet = _PdiGifApproveGroupBL.ListPdiGifApproveGroups(UserManager.UserInfo, filter, out count);
			
			var modelUpdate = new PdiGifApproveGroupViewModel();
			modelUpdate.GroupId = resultGet.Data.First().GroupId;
			
			modelUpdate.GroupName= guid; 
			modelUpdate.MailList= guid; 
			modelUpdate.IsActiveString= guid; 
			
			
			
			modelUpdate.CommandType = "U";
			 var resultUpdate = _PdiGifApproveGroupBL.DMLPdiGifApproveGroup(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void PdiGifApproveGroupBL_DMLPdiGifApproveGroup_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PdiGifApproveGroupViewModel();
			model.GroupId= 1; 
			model.GroupName= guid; 
			model.MailList= guid; 
			model.IsActiveString= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PdiGifApproveGroupBL.DMLPdiGifApproveGroup(UserManager.UserInfo, model);
			
			var filter = new PdiGifApproveGroupListModel();
			
			int count = 0;
			 var resultGet = _PdiGifApproveGroupBL.ListPdiGifApproveGroups(UserManager.UserInfo, filter, out count);
			
			var modelDelete = new PdiGifApproveGroupViewModel();
			modelDelete.GroupId = resultGet.Data.First().GroupId;
			
			modelDelete.GroupName= guid; 
			modelDelete.MailList= guid; 
			modelDelete.IsActiveString= guid; 
			
			
			
			modelDelete.CommandType = "D";
			 var resultDelete = _PdiGifApproveGroupBL.DMLPdiGifApproveGroup(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}

		[TestMethod]
		public void PdiGifApproveGroupBL_ListPdiGifApproveGroupsAsSelectItem_GetAll()
		{
			 var resultGet = PdiGifApproveGroupBL.ListPdiGifApproveGroupsAsSelectItem(UserManager.UserInfo);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        


	}

}

