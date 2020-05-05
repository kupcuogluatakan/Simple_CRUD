using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.WorkOrderPicking;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class WorkOrderPickingBLTest
	{

		WorkOrderPickingBL _WorkOrderPickingBL = new WorkOrderPickingBL();

		[TestMethod]
		public void WorkOrderPickingBL_DMLWorkOrderPicking_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new WorkOrderPickingViewModel();
			model.WorkOrderPickingId= 1; 
			model.WorkOrderId= 1; 
			model.WorkOrderPlate= guid; 
			model.StatusIds= guid; 
			model.CreateDate= DateTime.Now; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.PartCode = "M.162127"; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _WorkOrderPickingBL.DMLWorkOrderPicking(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void WorkOrderPickingBL_GetWorkOrderPicking_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new WorkOrderPickingViewModel();
			model.WorkOrderPickingId= 1; 
			model.WorkOrderId= 1; 
			model.WorkOrderPlate= guid; 
			model.StatusIds= guid; 
			model.CreateDate= DateTime.Now; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.PartCode = "M.162127"; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _WorkOrderPickingBL.DMLWorkOrderPicking(UserManager.UserInfo, model);
			
			var filter = new WorkOrderPickingViewModel();
			filter.WorkOrderId = result.Model.WorkOrderId;
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			filter.PartCode = "M.162127"; 
			filter.DealerId = UserManager.UserInfo.DealerID; 
			
			 var resultGet = _WorkOrderPickingBL.GetWorkOrderPicking(filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.WorkOrderId > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void WorkOrderPickingBL_ListWorkOrderPicking_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new WorkOrderPickingViewModel();
			model.WorkOrderPickingId= 1; 
			model.WorkOrderId= 1; 
			model.WorkOrderPlate= guid; 
			model.StatusIds= guid; 
			model.CreateDate= DateTime.Now; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.PartCode = "M.162127"; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _WorkOrderPickingBL.DMLWorkOrderPicking(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new WorkOrderPickingListModel();
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			filter.PartCode = "M.162127"; 
			
			 var resultGet = _WorkOrderPickingBL.ListWorkOrderPicking(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void WorkOrderPickingBL_DMLWorkOrderPicking_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new WorkOrderPickingViewModel();
			model.WorkOrderPickingId= 1; 
			model.WorkOrderId= 1; 
			model.WorkOrderPlate= guid; 
			model.StatusIds= guid; 
			model.CreateDate= DateTime.Now; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.PartCode = "M.162127"; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _WorkOrderPickingBL.DMLWorkOrderPicking(UserManager.UserInfo, model);
			
			var filter = new WorkOrderPickingListModel();
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			filter.PartCode = "M.162127"; 
			
			int count = 0;
			 var resultGet = _WorkOrderPickingBL.ListWorkOrderPicking(UserManager.UserInfo, filter, out count);
			
			var modelUpdate = new WorkOrderPickingViewModel();
			modelUpdate.WorkOrderPickingId = resultGet.Data.First().WorkOrderPickingId;
			
			
			modelUpdate.WorkOrderPlate= guid; 
			modelUpdate.StatusIds= guid; 
			
			modelUpdate.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			modelUpdate.PartCode = "M.162127"; 
			modelUpdate.DealerId = UserManager.UserInfo.DealerID; 
			
			
			
			modelUpdate.CommandType = "U";
			 var resultUpdate = _WorkOrderPickingBL.DMLWorkOrderPicking(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void WorkOrderPickingBL_DMLWorkOrderPicking_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new WorkOrderPickingViewModel();
			model.WorkOrderPickingId= 1; 
			model.WorkOrderId= 1; 
			model.WorkOrderPlate= guid; 
			model.StatusIds= guid; 
			model.CreateDate= DateTime.Now; 
			model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			model.PartCode = "M.162127"; 
			model.DealerId = UserManager.UserInfo.DealerID; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _WorkOrderPickingBL.DMLWorkOrderPicking(UserManager.UserInfo, model);
			
			var filter = new WorkOrderPickingListModel();
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			filter.PartCode = "M.162127"; 
			
			int count = 0;
			 var resultGet = _WorkOrderPickingBL.ListWorkOrderPicking(UserManager.UserInfo, filter, out count);
			
			var modelDelete = new WorkOrderPickingViewModel();
			modelDelete.WorkOrderPickingId = resultGet.Data.First().WorkOrderPickingId;
			
			
			modelDelete.WorkOrderPlate= guid; 
			modelDelete.StatusIds= guid; 
			
			modelDelete.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			modelDelete.PartCode = "M.162127"; 
			modelDelete.DealerId = UserManager.UserInfo.DealerID; 
			
			
			
			modelDelete.CommandType = "D";
			 var resultDelete = _WorkOrderPickingBL.DMLWorkOrderPicking(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}

