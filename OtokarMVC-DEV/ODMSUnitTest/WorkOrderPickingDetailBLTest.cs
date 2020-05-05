using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.WorkOrderPickingDetail;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class WorkOrderPickingDetailBLTest
	{

		WorkOrderPickingDetailBL _WorkOrderPickingDetailBL = new WorkOrderPickingDetailBL();

		[TestMethod]
		public void WorkOrderPickingDetailBL_DMLWOPDetSub_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new WOPDetSubViewModel();
			model.ResultId= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _WorkOrderPickingDetailBL.DMLWOPDetSub(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void WorkOrderPickingDetailBL_DMLWorkOrderPickingDetail_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new WorkOrderPickingDetailViewModel();
			model.WorkOrderPickingDetailId= 1; 
			model.WorkOrderPickingMstId= 1; 
			model.PartId= 1; 
			model.StockTypeId= 1; 
			model.RequiredQuantity= 1; 
			model.PickQuantity= 1; 
			model.PickClosureDescription= guid; 
			model.WorkOrderDetailId= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _WorkOrderPickingDetailBL.DMLWorkOrderPickingDetail(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void WorkOrderPickingDetailBL_ListWorkOrderPickingDetail_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new WorkOrderPickingDetailViewModel();
			model.WorkOrderPickingDetailId= 1; 
			model.WorkOrderPickingMstId= 1; 
			model.PartId= 1; 
			model.StockTypeId= 1; 
			model.RequiredQuantity= 1; 
			model.PickQuantity= 1; 
			model.PickClosureDescription= guid; 
			model.WorkOrderDetailId= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _WorkOrderPickingDetailBL.DMLWorkOrderPickingDetail(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new WorkOrderPickingDetailListModel();
			filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140"; 
			filter.PartId = 39399; 
			filter.PartCodeName = guid; 
			
			 var resultGet = _WorkOrderPickingDetailBL.ListWorkOrderPickingDetail(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        [TestMethod]
		public void WorkOrderPickingDetailBL_ListWorkOrderPickingDetailSub_GetAll()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new WorkOrderPickingDetailViewModel();
			model.WorkOrderPickingDetailId= 1; 
			model.WorkOrderPickingMstId= 1; 
			model.PartId= 1; 
			model.StockTypeId= 1; 
			model.RequiredQuantity= 1; 
			model.PickQuantity= 1; 
			model.PickClosureDescription= guid; 
			model.WorkOrderDetailId= 1; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _WorkOrderPickingDetailBL.DMLWorkOrderPickingDetail(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new WOPDetSubListModel();
			
			 var resultGet = _WorkOrderPickingDetailBL.ListWorkOrderPickingDetailSub(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        
		[TestMethod]
		public void WorkOrderPickingDetailBL_ListRackWarehouseByDetId_GetAll()
		{
			 var resultGet = _WorkOrderPickingDetailBL.ListRackWarehouseByDetId(UserManager.UserInfo, 1);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void WorkOrderPickingDetailBL_DMLWorkOrderPickingDetail_Update()
		{
		    var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var modelUpdate = new WorkOrderPickingDetailViewModel();
			modelUpdate.WorkOrderPickingDetailId = 1;
			modelUpdate.PickClosureDescription= guid; 
			modelUpdate.CommandType = "U";
			 var resultUpdate = _WorkOrderPickingDetailBL.DMLWorkOrderPickingDetail(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void WorkOrderPickingDetailBL_DMLWorkOrderPickingDetail_Delete()
		{
		    var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var modelDelete = new WorkOrderPickingDetailViewModel();
			modelDelete.WorkOrderPickingDetailId = 1;
			modelDelete.PickClosureDescription= guid; 
			modelDelete.CommandType = "D";
			 var resultDelete = _WorkOrderPickingDetailBL.DMLWorkOrderPickingDetail(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}

