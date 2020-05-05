using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.PDIGOSApproveGroupDealers;


namespace ODMSUnitTest
{

	[TestClass]
	public class PDIGOSApproveGroupDealersBLTest
	{

		PDIGOSApproveGroupDealersBL _PDIGOSApproveGroupDealersBL = new PDIGOSApproveGroupDealersBL();

		[TestMethod]
		public void PDIGOSApproveGroupDealersBL_SavePDIGOSApproveGroupDealers_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PDIGOSApproveGroupDealersSaveModel();
			model.id= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PDIGOSApproveGroupDealersBL.SavePDIGOSApproveGroupDealers(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void PDIGOSApproveGroupDealersBL_ListPDIGOSApproveGroupDealers_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PDIGOSApproveGroupDealersSaveModel();
			model.id= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PDIGOSApproveGroupDealersBL.SavePDIGOSApproveGroupDealers(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new PDIGOSApproveGroupDealersListModel();
			
			 var resultGet = _PDIGOSApproveGroupDealersBL.ListPDIGOSApproveGroupDealers(filter);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void PDIGOSApproveGroupDealersBL_SavePDIGOSApproveGroupDealers_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PDIGOSApproveGroupDealersSaveModel();
			model.id= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PDIGOSApproveGroupDealersBL.SavePDIGOSApproveGroupDealers(UserManager.UserInfo, model);
			
			var filter = new PDIGOSApproveGroupDealersListModel();
			
			int count = 0;
			 var resultGet = _PDIGOSApproveGroupDealersBL.ListPDIGOSApproveGroupDealers(filter);
			
			var modelUpdate = new PDIGOSApproveGroupDealersSaveModel();
			modelUpdate.id = resultGet.Data.First().IdGroup.Value;
			modelUpdate.CommandType = "U";
			 var resultUpdate = _PDIGOSApproveGroupDealersBL.SavePDIGOSApproveGroupDealers(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void PDIGOSApproveGroupDealersBL_SavePDIGOSApproveGroupDealers_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PDIGOSApproveGroupDealersSaveModel();
			model.id= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PDIGOSApproveGroupDealersBL.SavePDIGOSApproveGroupDealers(UserManager.UserInfo, model);
			
			var filter = new PDIGOSApproveGroupDealersListModel();
			
			int count = 0;
			 var resultGet = _PDIGOSApproveGroupDealersBL.ListPDIGOSApproveGroupDealers(filter);
			
			var modelDelete = new PDIGOSApproveGroupDealersSaveModel();
			modelDelete.id = resultGet.Data.First().IdGroup.Value;
			modelDelete.CommandType = "D";
			 var resultDelete = _PDIGOSApproveGroupDealersBL.SavePDIGOSApproveGroupDealers(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}

		[TestMethod]
		public void PDIGOSApproveGroupDealersBL_ListPDIGOSApproveGroupDealersNotInclude_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PDIGOSApproveGroupDealersSaveModel();
			model.id= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PDIGOSApproveGroupDealersBL.SavePDIGOSApproveGroupDealers(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new PDIGOSApproveGroupDealersListModel();
			
			 var resultGet = _PDIGOSApproveGroupDealersBL.ListPDIGOSApproveGroupDealersNotInclude(filter);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        
	}

}

