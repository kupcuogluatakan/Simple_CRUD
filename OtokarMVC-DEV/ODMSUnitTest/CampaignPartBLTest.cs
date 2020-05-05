using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.CampaignPart;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class CampaignPartBLTest
	{

		CampaignPartBL _CampaignPartBL = new CampaignPartBL();

		[TestMethod]
		public void CampaignPartBL_DMLCampaignPart_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new CampaignPartViewModel();
			model.CampaignCode = "508"; 
			model.PartCode = "M.162127"; 
			model.PartTypeDesc= guid; 
			model.SupplyTypeName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _CampaignPartBL.DMLCampaignPart(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void CampaignPartBL_GetCampaignPart_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new CampaignPartViewModel();
			model.CampaignCode = "508"; 
			model.PartCode = "M.162127"; 
			model.PartTypeDesc= guid; 
			model.SupplyTypeName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _CampaignPartBL.DMLCampaignPart(UserManager.UserInfo, model);
			
			var filter = new CampaignPartViewModel();
			filter.CampaignCode = "508"; 
			filter.PartId = 39399; 
			filter.PartCode = "M.162127"; 
			
			 var resultGet = _CampaignPartBL.GetCampaignPart(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.Quantity > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void CampaignPartBL_ListCampaignParts_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new CampaignPartViewModel();
			model.CampaignCode = "508"; 
			model.PartCode = "M.162127"; 
			model.PartTypeDesc= guid; 
			model.SupplyTypeName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _CampaignPartBL.DMLCampaignPart(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new CampaignPartListModel();
			filter.CampaignCode = "508"; 
			filter.PartId = 39399; 
			filter.PartCode = "M.162127"; 
			
			 var resultGet = _CampaignPartBL.ListCampaignParts(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}


	}

}

