using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.PDIGOSApproveGroupIndicatorTransTypes;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class PDIGOSApproveGroupIndicatorTransTypeBLTest
	{

		PDIGOSApproveGroupIndicatorTransTypeBL _PDIGOSApproveGroupIndicatorTransTypeBL = new PDIGOSApproveGroupIndicatorTransTypeBL();

		[TestMethod]
		public void PDIGOSApproveGroupIndicatorTransTypeBL_Save_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new PDIGOSApproveGroupIndicatorTransTypesModel();
			model.GroupId= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _PDIGOSApproveGroupIndicatorTransTypeBL.Save(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void PDIGOSApproveGroupIndicatorTransTypeBL_ListPDIGOSApproveGroupIndicatorTransTypesIncluded_GetAll()
		{
			 var resultGet = _PDIGOSApproveGroupIndicatorTransTypeBL.ListPDIGOSApproveGroupIndicatorTransTypesIncluded(UserManager.UserInfo, 1);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void PDIGOSApproveGroupIndicatorTransTypeBL_Save_Update()
		{
			
			var modelUpdate = new PDIGOSApproveGroupIndicatorTransTypesModel();
			modelUpdate.GroupId = 1;
			modelUpdate.CommandType = "U";
			 var resultUpdate = _PDIGOSApproveGroupIndicatorTransTypeBL.Save(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void PDIGOSApproveGroupIndicatorTransTypeBL_Save_Delete()
		{
			
			var modelDelete = new PDIGOSApproveGroupIndicatorTransTypesModel();
			modelDelete.GroupId =1;
			modelDelete.CommandType = "D";
			 var resultDelete = _PDIGOSApproveGroupIndicatorTransTypeBL.Save(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}

		[TestMethod]
		public void PDIGOSApproveGroupIndicatorTransTypeBL_ListPDIGOSApproveGroupIndicatorTransTypesExcluded_GetAll()
		{
			 var resultGet = _PDIGOSApproveGroupIndicatorTransTypeBL.ListPDIGOSApproveGroupIndicatorTransTypesExcluded(UserManager.UserInfo, 1);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        
	}

}

