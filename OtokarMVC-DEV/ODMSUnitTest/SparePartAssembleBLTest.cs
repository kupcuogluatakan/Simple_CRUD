using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.SparePartAssemble;
using System;
using ODMSModel.SparePartSplitting;


namespace ODMSUnitTest
{

	[TestClass]
	public class SparePartAssembleBLTest
	{

		SparePartAssembleBL _SparePartAssembleBL = new SparePartAssembleBL();

		[TestMethod]
		public void SparePartAssembleBL_DMLSparePartAssemble_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartAssembleIndexViewModel();
			model.PartCode = "M.162127"; 
			model.PartCodeAssemble= guid; 
			model.IsActive= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _SparePartAssembleBL.DMLSparePartAssemble(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void SparePartAssembleBL_GetSparePartAssemble_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartAssembleIndexViewModel();
			model.PartCode = "M.162127"; 
			model.PartCodeAssemble= guid; 
			model.IsActive= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _SparePartAssembleBL.DMLSparePartAssemble(UserManager.UserInfo, model);
			
			var filter = new SparePartAssembleIndexViewModel();
			filter.IdPart = result.Model.IdPart;
			filter.PartCode = "M.162127"; 
			filter.PartCodeAssemble = guid; 
			
			 var resultGet = _SparePartAssembleBL.GetSparePartAssemble(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.IdPart > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void SparePartAssembleBL_ListSparePartAssemble_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartAssembleIndexViewModel();
			model.PartCode = "M.162127"; 
			model.PartCodeAssemble= guid; 
			model.IsActive= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _SparePartAssembleBL.DMLSparePartAssemble(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new SparePartAssembleListModel();
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			filter.PartCodeAssemble = guid; 
			
			 var resultGet = _SparePartAssembleBL.ListSparePartAssemble(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        
		[TestMethod]
		public void SparePartAssembleBL_ListSparePartSplitting_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartAssembleIndexViewModel();
			model.PartCode = "M.162127"; 
			model.PartCodeAssemble= guid; 
			model.IsActive= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _SparePartAssembleBL.DMLSparePartAssemble(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new SparePartSplittingListModel();
			filter.OldPartCode = guid; 
			filter.NewPartCode = guid; 
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			 var resultGet = _SparePartAssembleBL.ListSparePartSplitting(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void SparePartAssembleBL_DMLSparePartAssemble_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartAssembleIndexViewModel();
			model.PartCode = "M.162127"; 
			model.PartCodeAssemble= guid; 
			model.IsActive= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _SparePartAssembleBL.DMLSparePartAssemble(UserManager.UserInfo, model);
			
			var filter = new SparePartSplittingListModel();
			filter.OldPartCode = guid; 
			filter.NewPartCode = guid; 
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			int count = 0;
			 var resultGet = _SparePartAssembleBL.ListSparePartSplitting(UserManager.UserInfo, filter, out count);
			
			var modelUpdate = new SparePartAssembleIndexViewModel();
			modelUpdate.PartCode = "M.162127"; 
			modelUpdate.PartCodeAssemble= guid;
			modelUpdate.CommandType = "U";
			 var resultUpdate = _SparePartAssembleBL.DMLSparePartAssemble(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void SparePartAssembleBL_DMLSparePartAssemble_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SparePartAssembleIndexViewModel();
			model.PartCode = "M.162127"; 
			model.PartCodeAssemble= guid; 
			model.IsActive= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.CommandType = "I";
			 var result = _SparePartAssembleBL.DMLSparePartAssemble(UserManager.UserInfo, model);
			
			var filter = new SparePartSplittingListModel();
			filter.OldPartCode = guid; 
			filter.NewPartCode = guid; 
			filter.PartCode = "M.162127"; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			
			int count = 0;
			 var resultGet = _SparePartAssembleBL.ListSparePartSplitting(UserManager.UserInfo, filter, out count);
			
			var modelDelete = new SparePartAssembleIndexViewModel();
			modelDelete.PartCode = "M.162127"; 
			modelDelete.PartCodeAssemble= guid; 
			
			
			
			modelDelete.CommandType = "D";
			 var resultDelete = _SparePartAssembleBL.DMLSparePartAssemble(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}

