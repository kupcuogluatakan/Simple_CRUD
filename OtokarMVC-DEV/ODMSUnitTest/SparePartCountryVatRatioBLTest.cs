using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.SparePartCountryVatRatio;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class SparePartCountryVatRatioBLTest
	{

		SparePartCountryVatRatioBL _SparePartCountryVatRatioBL = new SparePartCountryVatRatioBL();

		[TestMethod]
		public void SparePartCountryVatRatioBL_DMLSparePartCountryVatRatio_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartCountryVatRatioViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.CountryName= guid; 
			model.IsActive= true; 
			model.IsActiveName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _SparePartCountryVatRatioBL.DMLSparePartCountryVatRatio(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void SparePartCountryVatRatioBL_GetSparePartCountryVatRatio_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartCountryVatRatioViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.CountryName= guid; 
			model.IsActive= true; 
			model.IsActiveName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _SparePartCountryVatRatioBL.DMLSparePartCountryVatRatio(UserManager.UserInfo, model);
			
			var filter = new SparePartCountryVatRatioViewModel();
		    filter.IdPart = result.Model.IdPart;
            filter.MultiLanguageContentAsText = "TR || TEST"; 
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			 var resultGet = _SparePartCountryVatRatioBL.GetSparePartCountryVatRatio(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.IdPart > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void SparePartCountryVatRatioBL_ListSparePartCountryVatRatio_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartCountryVatRatioViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.CountryName= guid; 
			model.IsActive= true; 
			model.IsActiveName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _SparePartCountryVatRatioBL.DMLSparePartCountryVatRatio(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new SparePartCountryVatRatioListModel();
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			 var resultGet = _SparePartCountryVatRatioBL.ListSparePartCountryVatRatio(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void SparePartCountryVatRatioBL_DMLSparePartCountryVatRatio_Update()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartCountryVatRatioViewModel();
			model.MultiLanguageContentAsText = "TR || TEST"; 
			model.PartCode = "M.162127"; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.CountryName= guid; 
			model.IsActive= true; 
			model.IsActiveName= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _SparePartCountryVatRatioBL.DMLSparePartCountryVatRatio(UserManager.UserInfo, model);
			
			var filter = new SparePartCountryVatRatioListModel();
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			int count = 0;
			 var resultGet = _SparePartCountryVatRatioBL.ListSparePartCountryVatRatio(UserManager.UserInfo, filter, out count);
			
			var modelUpdate = new SparePartCountryVatRatioViewModel();
			modelUpdate.IdPart = resultGet.Data.First().IdPart;
			modelUpdate.MultiLanguageContentAsText = "TR || TEST"; 
			modelUpdate.PartCode = "M.162127"; 
			modelUpdate.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			modelUpdate.CountryName= guid; 
			
			modelUpdate.IsActiveName= guid; 
			
			
			modelUpdate.CommandType = "U";
			 var resultUpdate = _SparePartCountryVatRatioBL.DMLSparePartCountryVatRatio(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}
        
		[TestMethod]
		public void SparePartCountryVatRatioBL_ListCountryNameAsSelectListItem_GetAll()
		{
			 var resultGet = SparePartCountryVatRatioBL.ListCountryNameAsSelectListItem(UserManager.UserInfo);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        
		[TestMethod]
		public void SparePartCountryVatRatioBL_DMLSparePartCountryVatRatio_Delete()
		{
		    var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var modelDelete = new SparePartCountryVatRatioViewModel();
			modelDelete.MultiLanguageContentAsText = "TR || TEST"; 
			modelDelete.PartCode = "M.162127"; 
			modelDelete.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			modelDelete.CountryName= guid; 
			
			modelDelete.IsActiveName= guid; 
			
			
			modelDelete.CommandType = "D";
			 var resultDelete = _SparePartCountryVatRatioBL.DMLSparePartCountryVatRatio(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}

