using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.SparePartGuaranteeAuthorityNeed;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class SparePartGuaranteeAuthorityNeedBLTest
	{

		SparePartGuaranteeAuthorityNeedBL _SparePartGuaranteeAuthorityNeedBL = new SparePartGuaranteeAuthorityNeedBL();

		[TestMethod]
		public void SparePartGuaranteeAuthorityNeedBL_DMLSparePartGuaranteeAuthorityNeed_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartGuaranteeAuthorityNeedViewModel();
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.GuaranteeAuthorityNeedName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SparePartGuaranteeAuthorityNeedBL.DMLSparePartGuaranteeAuthorityNeed(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void SparePartGuaranteeAuthorityNeedBL_ListSparePartGuaranteeAuthorityNeeds_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartGuaranteeAuthorityNeedViewModel();
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.GuaranteeAuthorityNeedName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SparePartGuaranteeAuthorityNeedBL.DMLSparePartGuaranteeAuthorityNeed(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new SparePartGuaranteeAuthorityNeedListModel();
			filter.PartId = 39399; 
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			 var resultGet = _SparePartGuaranteeAuthorityNeedBL.ListSparePartGuaranteeAuthorityNeeds(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void SparePartGuaranteeAuthorityNeedBL_DMLSparePartGuaranteeAuthorityNeed_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartGuaranteeAuthorityNeedViewModel();
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.GuaranteeAuthorityNeedName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SparePartGuaranteeAuthorityNeedBL.DMLSparePartGuaranteeAuthorityNeed(UserManager.UserInfo, model);
			
			var filter = new SparePartGuaranteeAuthorityNeedListModel();
			filter.PartId = 39399; 
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			int count = 0;
			 var resultGet = _SparePartGuaranteeAuthorityNeedBL.ListSparePartGuaranteeAuthorityNeeds(UserManager.UserInfo, filter, out count);
			
			var modelUpdate = new SparePartGuaranteeAuthorityNeedViewModel();
			modelUpdate.PartId = resultGet.Data.First().PartId;
			modelUpdate.PartCode = "M.162127"; 
			modelUpdate.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			modelUpdate.GuaranteeAuthorityNeedName= guid; 
			
			
			
			modelUpdate.CommandType = "U";
			 var resultUpdate = _SparePartGuaranteeAuthorityNeedBL.DMLSparePartGuaranteeAuthorityNeed(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void SparePartGuaranteeAuthorityNeedBL_DMLSparePartGuaranteeAuthorityNeed_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartGuaranteeAuthorityNeedViewModel();
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.GuaranteeAuthorityNeedName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SparePartGuaranteeAuthorityNeedBL.DMLSparePartGuaranteeAuthorityNeed(UserManager.UserInfo, model);
			
			var filter = new SparePartGuaranteeAuthorityNeedListModel();
			filter.PartId = 39399; 
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			int count = 0;
			 var resultGet = _SparePartGuaranteeAuthorityNeedBL.ListSparePartGuaranteeAuthorityNeeds(UserManager.UserInfo, filter, out count);
			
			var modelDelete = new SparePartGuaranteeAuthorityNeedViewModel();
			modelDelete.PartId = resultGet.Data.First().PartId;
			modelDelete.PartCode = "M.162127"; 
			modelDelete.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			modelDelete.GuaranteeAuthorityNeedName= guid; 
			
			
			
			modelDelete.CommandType = "D";
			 var resultDelete = _SparePartGuaranteeAuthorityNeedBL.DMLSparePartGuaranteeAuthorityNeed(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}

