using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.Rack;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class RackBLTest
	{

		RackBL _RackBL = new RackBL();

		[TestMethod]
		public void RackBL_DMLRack_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new RackDetailModel();
			model.WarehouseCode= guid; 
			model.WarehouseName= guid; 
			model.Code= guid; 
			model.Name= guid; 
			model.HaveStockRackDetail= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _RackBL.DMLRack(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void RackBL_GetRackIndexModel_GetModel()
		{
			 var resultGet = _RackBL.GetRackIndexModel(1);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.DealerId > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void RackBL_GetRack_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new RackDetailModel();
			model.WarehouseCode= guid; 
			model.WarehouseName= guid; 
			model.Code= guid; 
			model.Name= guid; 
			model.HaveStockRackDetail= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _RackBL.DMLRack(UserManager.UserInfo, model);
			
			var filter = new RackDetailModel();
			filter.Id = result.Model.Id;
			filter.WarehouseCode = guid; 
			filter.Code = guid; 
			
			 var resultGet = _RackBL.GetRack(filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.Id > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void RackBL_ListWarehousesOfDealer_GetAll()
		{
			 var resultGet = _RackBL.ListWarehousesOfDealer(1);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void RackBL_ListRacks_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new RackDetailModel();
			model.WarehouseCode= guid; 
			model.WarehouseName= guid; 
			model.Code= guid; 
			model.Name= guid; 
			model.HaveStockRackDetail= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _RackBL.DMLRack(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new RackListModel();
			filter.WarehouseCode = guid; 
			filter.DealerId = UserManager.UserInfo.DealerID.ToString(); 
			filter.Code = guid; 
			
			 var resultGet = _RackBL.ListRacks(filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void RackBL_DMLRack_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new RackDetailModel();
			model.WarehouseCode= guid; 
			model.WarehouseName= guid; 
			model.Code= guid; 
			model.Name= guid; 
			model.HaveStockRackDetail= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _RackBL.DMLRack(UserManager.UserInfo, model);
			
			var filter = new RackListModel();
			filter.WarehouseCode = guid; 
			filter.DealerId = UserManager.UserInfo.DealerID.ToString(); 
			filter.Code = guid; 
			
			int count = 0;
			 var resultGet = _RackBL.ListRacks(filter, out count);
			
			var modelUpdate = new RackDetailModel();
			modelUpdate.Id = resultGet.Data.First().Id;
			modelUpdate.WarehouseCode= guid; 
			modelUpdate.WarehouseName= guid; 
			modelUpdate.Code= guid; 
			modelUpdate.Name= guid; 
			
			
			
			
			modelUpdate.CommandType = "U";
			 var resultUpdate = _RackBL.DMLRack(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void RackBL_DMLRack_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new RackDetailModel();
			model.WarehouseCode= guid; 
			model.WarehouseName= guid; 
			model.Code= guid; 
			model.Name= guid; 
			model.HaveStockRackDetail= true; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _RackBL.DMLRack(UserManager.UserInfo, model);
			
			var filter = new RackListModel();
			filter.WarehouseCode = guid; 
			filter.DealerId = UserManager.UserInfo.DealerID.ToString(); 
			filter.Code = guid; 
			
			int count = 0;
			 var resultGet = _RackBL.ListRacks(filter, out count);
			
			var modelDelete = new RackDetailModel();
			modelDelete.Id = resultGet.Data.First().Id;
			modelDelete.WarehouseCode= guid; 
			modelDelete.WarehouseName= guid; 
			modelDelete.Code= guid; 
			modelDelete.Name= guid; 
			
			
			
			
			modelDelete.CommandType = "D";
			 var resultDelete = _RackBL.DMLRack(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}

