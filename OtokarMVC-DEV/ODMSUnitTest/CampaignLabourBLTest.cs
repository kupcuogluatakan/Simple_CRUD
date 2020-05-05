using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.CampaignLabour;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class CampaignLabourBLTest
	{

		CampaignLabourBL _CampaignLabourBL = new CampaignLabourBL();

		[TestMethod]
		public void CampaignLabourBL_DMLCampaignLabour_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new CampaignLabourViewModel();
			model.CampaignCode = "508"; 
			model.LabourId = 211; 
			model.LabourCode= guid; 
			model.LabourTypeDesc= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _CampaignLabourBL.DMLCampaignLabour(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void CampaignLabourBL_GetCampaignLabour_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new CampaignLabourViewModel();
			model.CampaignCode = "508"; 
			model.LabourId = 211; 
			model.LabourCode= guid; 
			model.LabourTypeDesc= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _CampaignLabourBL.DMLCampaignLabour(UserManager.UserInfo, model);
			
			var filter = new CampaignLabourViewModel();
			filter.CampaignCode = "508"; 
			filter.LabourId = 211; 
			filter.LabourCode = guid; 
			
			 var resultGet = _CampaignLabourBL.GetCampaignLabour(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.Quantity > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void CampaignLabourBL_ListCampaignLabours_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new CampaignLabourViewModel();
			model.CampaignCode = "508"; 
			model.LabourId = 211; 
			model.LabourCode= guid; 
			model.LabourTypeDesc= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _CampaignLabourBL.DMLCampaignLabour(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new CampaignLabourListModel();
			filter.CampaignCode = "508"; 
			filter.LabourId = 211; 
			filter.LabourCode = guid; 
			
			 var resultGet = _CampaignLabourBL.ListCampaignLabours(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void CampaignLabourBL_ListLabourTimeAsSelectList_GetAll()
		{
			 var resultGet = _CampaignLabourBL.ListLabourTimeAsSelectList(UserManager.UserInfo, "508", 211);
			
			Assert.IsTrue(resultGet.Total > 0);
		}


	}

}

