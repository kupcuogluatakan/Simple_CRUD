using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.WorkOrderInvoiceList;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class WorkOrderInvoiceListBLTest
	{

		WorkOrderInvoiceListBL _WorkOrderInvoiceListBL = new WorkOrderInvoiceListBL();

		[TestMethod]
		public void WorkOrderInvoiceListBL_DMLWorkOrderInvoiceList_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new WorkOrderInvoiceListViewModel();
			model.IdWorkOrderInv= 1; 
			model.CustomerName= guid; 
			model.CustomerLastName= guid; 
			model.InvoiceNo= guid; 
			model.Plate= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _WorkOrderInvoiceListBL.DMLWorkOrderInvoiceList(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void WorkOrderInvoiceListBL_ListWorkOrderInvoiceList_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new WorkOrderInvoiceListViewModel();
			model.IdWorkOrderInv= 1; 
			model.CustomerName= guid; 
			model.CustomerLastName= guid; 
			model.InvoiceNo= guid; 
			model.Plate= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _WorkOrderInvoiceListBL.DMLWorkOrderInvoiceList(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new WorkOrderInvoiceListListModel();
			
			 var resultGet = _WorkOrderInvoiceListBL.ListWorkOrderInvoiceList(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void WorkOrderInvoiceListBL_DMLWorkOrderInvoiceList_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new WorkOrderInvoiceListViewModel();
			model.IdWorkOrderInv= 1; 
			model.CustomerName= guid; 
			model.CustomerLastName= guid; 
			model.InvoiceNo= guid; 
			model.Plate= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _WorkOrderInvoiceListBL.DMLWorkOrderInvoiceList(UserManager.UserInfo, model);
			
			var filter = new WorkOrderInvoiceListListModel();
			
			int count = 0;
			 var resultGet = _WorkOrderInvoiceListBL.ListWorkOrderInvoiceList(UserManager.UserInfo, filter, out count);
			
			var modelUpdate = new WorkOrderInvoiceListViewModel();
			modelUpdate.IdWorkOrder = resultGet.Data.First().IdWorkOrder;
			modelUpdate.CustomerName= guid; 
			modelUpdate.CustomerLastName= guid; 
			modelUpdate.InvoiceNo= guid; 
			modelUpdate.Plate= guid; 
			
			
			
			modelUpdate.CommandType = "U";
			 var resultUpdate = _WorkOrderInvoiceListBL.DMLWorkOrderInvoiceList(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void WorkOrderInvoiceListBL_DMLWorkOrderInvoiceList_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new WorkOrderInvoiceListViewModel();
			model.IdWorkOrderInv= 1; 
			model.CustomerName= guid; 
			model.CustomerLastName= guid; 
			model.InvoiceNo= guid; 
			model.Plate= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _WorkOrderInvoiceListBL.DMLWorkOrderInvoiceList(UserManager.UserInfo, model);
			
			var filter = new WorkOrderInvoiceListListModel();
			
			int count = 0;
			 var resultGet = _WorkOrderInvoiceListBL.ListWorkOrderInvoiceList(UserManager.UserInfo, filter, out count);
			
			var modelDelete = new WorkOrderInvoiceListViewModel();
			modelDelete.IdWorkOrder = resultGet.Data.First().IdWorkOrder;
			
			modelDelete.CustomerName= guid; 
			modelDelete.CustomerLastName= guid; 
			modelDelete.InvoiceNo= guid; 
			modelDelete.Plate= guid; 
			
			
			
			modelDelete.CommandType = "D";
			 var resultDelete = _WorkOrderInvoiceListBL.DMLWorkOrderInvoiceList(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}

