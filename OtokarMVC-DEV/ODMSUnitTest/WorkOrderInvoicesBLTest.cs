using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.WorkOrderInvoice;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class WorkOrderInvoicesBLTest
	{

		WorkOrderInvoicesBL _WorkOrderInvoicesBL = new WorkOrderInvoicesBL();

		[TestMethod]
		public void WorkOrderInvoicesBL_DMLWorkOrderInvoices_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new WorkOrderInvoicesViewModel();
			model.HideElements= true; 
			model.WorkOrderId= 1; 
			model.WorkOrderInvoiceId= 1; 
			model.CustomerId= 1; 
			model.CustomerName= guid; 
			model.Address= guid; 
			model.VatRatio= 1; 
			model.InvoiceSerialNo= guid; 
			model.InvoiceNo= guid; 
			model.InvoiceDate= DateTime.Now; 
			model.InvoiceAmount= 1; 
			model.Currrency= guid; 
			model.HasWitholding= true; 
			model.WitholdId= guid; 
			model.WitholdAmount= 1; 
			model.WitholdDivident= 1; 
			model.WitholdDivisor= 1; 
			model.InvoiceTypeId= 1; 
			model.SpecialInvoiceAmount= 1; 
			model.SpecialInvoiceVatAmount= 1; 
			model.SpecialInvoiceDescription= guid; 
			model.WorkOrderIds= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _WorkOrderInvoicesBL.DMLWorkOrderInvoices(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void WorkOrderInvoicesBL_UpdateInvoiceIds_Insert()
		{
            string invType = string.Empty;
            var result = _WorkOrderInvoicesBL.UpdateInvoiceIds(UserManager.UserInfo, 4,6,"38","D",out invType);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void WorkOrderInvoicesBL_GetWorkOrderInvoices_GetModel()
		{
			 var resultGet = _WorkOrderInvoicesBL.GetWorkOrderInvoices(UserManager.UserInfo, 6);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.WorkOrderInvoiceId > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void WorkOrderInvoicesBL_GetWorkOrderInvoiceAmount_GetModel()
		{
			 var resultGet = _WorkOrderInvoicesBL.GetWorkOrderInvoiceAmount(4,14263,6,"38");
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Total > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void WorkOrderInvoicesBL_GetSuggestedInvoiceData_GetModel()
		{
			 var resultGet = _WorkOrderInvoicesBL.GetSuggestedInvoiceData(4,"38");
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Total > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void WorkOrderInvoicesBL_GetLastWorkOrderInvoiceId_GetModel()
		{
			 var resultGet = _WorkOrderInvoicesBL.GetLastWorkOrderInvoiceId(4);
			
			Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void WorkOrderInvoicesBL_ListWorkOrderInvoices_GetAll()
		{
		    string invType = string.Empty;
             var result = _WorkOrderInvoicesBL.UpdateInvoiceIds(UserManager.UserInfo, 4, 6, "38", "D", out invType);
			int count = 0;
			var filter = new WorkOrderInvoicesListModel();
			
			 var resultGet = _WorkOrderInvoicesBL.ListWorkOrderInvoices(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        
		[TestMethod]
		public void WorkOrderInvoicesBL_ListCutomerAddresses_GetAll()
		{
			 var resultGet = _WorkOrderInvoicesBL.ListCutomerAddresses(UserManager.UserInfo, 17481);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        
		[TestMethod]
		public void WorkOrderInvoicesBL_GetWitholdingListForDealer_GetAll()
		{
			 var resultGet = _WorkOrderInvoicesBL.GetWitholdingListForDealer(143);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        
		[TestMethod]
		public void WorkOrderInvoicesBL_ListWorkOrderInvoiceItems_GetAll()
		{
			 var resultGet = _WorkOrderInvoicesBL.ListWorkOrderInvoiceItems(UserManager.UserInfo, 4,"38");
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        [TestMethod]
		public void WorkOrderInvoicesBL_ListInvoices_GetAll()
		{
			 var resultGet = _WorkOrderInvoicesBL.ListInvoices(UserManager.UserInfo, 4, "38");

            Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void WorkOrderInvoicesBL_UpdateInvoiceIds_Update()
		{
		    string invType = string.Empty;
			 var resultUpdate = _WorkOrderInvoicesBL.UpdateInvoiceIds(UserManager.UserInfo, 4, 6, "38", "D", out invType);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void WorkOrderInvoicesBL_UpdateInvoiceIds_Delete()
		{
		    string invType = string.Empty;
            var resultDelete = _WorkOrderInvoicesBL.UpdateInvoiceIds(UserManager.UserInfo, 4, 6, "38", "D", out invType);
            Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}

