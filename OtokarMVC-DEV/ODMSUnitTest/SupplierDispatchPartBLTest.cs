using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.SupplierDispatchPart;
using System.Collections.Generic;


namespace ODMSUnitTest
{

	[TestClass]
	public class SupplierDispatchPartBLTest
	{

		SupplierDispatchPartBL _SupplierDispatchPartBL = new SupplierDispatchPartBL();

		[TestMethod]
		public void SupplierDispatchPartBL_Insert_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SupplierDispatchPartViewModel();
			model.DeliverySeqNo= 1; 
			model.DeliveryId= 1; 
			model.PartId= 1; 
			model.PartCode = "M.162127"; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SupplierDispatchPartBL.Insert(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void SupplierDispatchPartBL_Insert_Insert_1()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SupplierDispatchPartOrderViewModel();
			model.SupplierId = 782; 
			model.DeliveryId= 1; 
			model.WayBillNo= guid; 
			model.WayBillDate= guid; 
			model.PurchaseNo= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SupplierDispatchPartBL.Insert(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void SupplierDispatchPartBL_Update_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SupplierDispatchPartViewModel();
			model.DeliverySeqNo= 1; 
			model.DeliveryId= 1; 
			model.PartId= 1; 
			model.PartCode = "M.162127"; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SupplierDispatchPartBL.Update(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void SupplierDispatchPartBL_Insert_Insert_2()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SupplierDispatchPartViewModel();
			model.DeliverySeqNo= 1; 
			model.DeliveryId= 1; 
			model.PartId= 1; 
			model.PartCode = "M.162127"; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SupplierDispatchPartBL.Insert(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void SupplierDispatchPartBL_Update_Insert_1()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SupplierDispatchPartViewModel();
			model.DeliverySeqNo= 1; 
			model.DeliveryId= 1; 
			model.PartId= 1; 
			model.PartCode = "M.162127"; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SupplierDispatchPartBL.Update(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void SupplierDispatchPartBL_Get_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SupplierDispatchPartViewModel();
			model.DeliverySeqNo= 1; 
			model.DeliveryId= 1; 
			model.PartId= 1; 
			model.PartCode = "M.162127"; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SupplierDispatchPartBL.Update(UserManager.UserInfo, model);
			
			var filter = new SupplierDispatchPartViewModel();
			filter.DeliverySeqNo = result.Model.DeliverySeqNo;
			filter.PartId = 39399; 
			filter.PartCode = "M.162127"; 
			
			 var resultGet = _SupplierDispatchPartBL.Get(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.DeliverySeqNo > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void SupplierDispatchPartBL_List_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SupplierDispatchPartViewModel();
			model.DeliverySeqNo= 1; 
			model.DeliveryId= 1; 
			model.PartId= 1; 
			model.PartCode = "M.162127"; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SupplierDispatchPartBL.Update(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new SupplierDispatchPartListModel();
			filter.SupplierId = 782; 
			filter.PartCode = "M.162127"; 
			filter.PartId = 39399; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			filter.DesirePartCode = guid; 
			
			 var resultGet = _SupplierDispatchPartBL.List(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void SupplierDispatchPartBL_Update_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SupplierDispatchPartViewModel();
			model.DeliverySeqNo= 1; 
			model.DeliveryId= 1; 
			model.PartId= 1; 
			model.PartCode = "M.162127"; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SupplierDispatchPartBL.Update(UserManager.UserInfo, model);
			
			var filter = new SupplierDispatchPartListModel();
			filter.SupplierId = 782; 
			filter.PartCode = "M.162127"; 
			filter.PartId = 39399; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			filter.DesirePartCode = guid; 
			
			int count = 0;
			 var resultGet = _SupplierDispatchPartBL.List(UserManager.UserInfo, filter, out count);
			
			var modelUpdate = new SupplierDispatchPartViewModel();
			modelUpdate.DeliverySeqNo = resultGet.Data.First().DeliverySeqNo;
			modelUpdate.PartCode = "M.162127"; 
			modelUpdate.CommandType = "U";
			 var resultUpdate = _SupplierDispatchPartBL.Update(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void SupplierDispatchPartBL_Update_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SupplierDispatchPartViewModel();
			model.DeliverySeqNo= 1; 
			model.DeliveryId= 1; 
			model.PartId= 1; 
			model.PartCode = "M.162127"; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SupplierDispatchPartBL.Update(UserManager.UserInfo, model);
			
			var filter = new SupplierDispatchPartListModel();
			filter.SupplierId = 782; 
			filter.PartCode = "M.162127"; 
			filter.PartId = 39399; 
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			filter.DesirePartCode = guid; 
			
			int count = 0;
			 var resultGet = _SupplierDispatchPartBL.List(UserManager.UserInfo, filter, out count);
			
			var modelDelete = new SupplierDispatchPartViewModel();
			modelDelete.DeliverySeqNo = resultGet.Data.First().DeliverySeqNo;
			modelDelete.PartCode = "M.162127"; 
			modelDelete.CommandType = "D";
			 var resultDelete = _SupplierDispatchPartBL.Update(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}

		[TestMethod]
		public void SupplierDispatchPartBL_List_GetAll_1()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SupplierDispatchPartViewModel();
			model.DeliverySeqNo= 1; 
			model.DeliveryId= 1; 
			model.PartId= 1; 
			model.PartCode = "M.162127"; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SupplierDispatchPartBL.Update(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new SupplierDispatchPartViewModel();
			filter.PartId = 39399; 
			filter.PartCode = "M.162127"; 
			
			 var resultGet = _SupplierDispatchPartBL.List(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void SupplierDispatchPartBL_Update_Update_1()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SupplierDispatchPartViewModel();
			model.DeliverySeqNo= 1; 
			model.DeliveryId= 1; 
			model.PartId= 1; 
			model.PartCode = "M.162127"; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SupplierDispatchPartBL.Update(UserManager.UserInfo, model);
			var filter = new SupplierDispatchPartViewModel();
			filter.PartId = 39399; 
			filter.PartCode = "M.162127"; 
			int count = 0;
			 var resultGet = _SupplierDispatchPartBL.List(UserManager.UserInfo, filter);
			var modelUpdate = new SupplierDispatchPartViewModel();
			modelUpdate.DeliverySeqNo = resultGet.Data.First().DeliverySeqNo;
			modelUpdate.PartCode = "M.162127"; 
			modelUpdate.CommandType = "U";
			 var resultUpdate = _SupplierDispatchPartBL.Update(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void SupplierDispatchPartBL_Update_Delete_1()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new SupplierDispatchPartViewModel();
			model.DeliverySeqNo= 1; 
			model.DeliveryId= 1; 
			model.PartId= 1; 
			model.PartCode = "M.162127"; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _SupplierDispatchPartBL.Update(UserManager.UserInfo, model);
			
			var filter = new SupplierDispatchPartViewModel();
			filter.PartId = 39399; 
			filter.PartCode = "M.162127"; 
			
			int count = 0;
			 var resultGet = _SupplierDispatchPartBL.List(UserManager.UserInfo, filter);
			
			var modelDelete = new SupplierDispatchPartViewModel();
			modelDelete.DeliverySeqNo = resultGet.Data.First().DeliverySeqNo;
			modelDelete.PartCode = "M.162127"; 
			modelDelete.CommandType = "D";
			 var resultDelete = _SupplierDispatchPartBL.Update(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}

