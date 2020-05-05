using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.WorkorderInvoicePayments;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class WorkorderInvoicePaymentsBLTest
	{

		WorkorderInvoicePaymentsBL _WorkorderInvoicePaymentsBL = new WorkorderInvoicePaymentsBL();

		[TestMethod]
		public void WorkorderInvoicePaymentsBL_DMLWorkorderInvoicePayments_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new WorkorderInvoicePaymentsDetailModel();
			model.WorkorderInvoiceId= 1; 
			model.WorkorderId= 1; 
			model.BankRequired= true; 
			model.InstalmentNumberRequired= true; 
			model.TransmitNumberRequired= true; 
			model.PaymentTypeName= guid; 
			model.BankName= guid; 
			model.TransmitNumber= guid; 
			model.PaymentDate= DateTime.Now; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _WorkorderInvoicePaymentsBL.DMLWorkorderInvoicePayments(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void WorkorderInvoicePaymentsBL_GetWorkorderInvoicePaymentsIndexModel_GetModel()
		{
			 var resultGet = _WorkorderInvoicePaymentsBL.GetWorkorderInvoicePaymentsIndexModel(UserManager.UserInfo, 6,4);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.WorkorderId > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void WorkorderInvoicePaymentsBL_GetWorkorderInvoicePayments_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new WorkorderInvoicePaymentsDetailModel();
			model.WorkorderInvoiceId= 1; 
			model.WorkorderId= 1; 
			model.BankRequired= true; 
			model.InstalmentNumberRequired= true; 
			model.TransmitNumberRequired= true; 
			model.PaymentTypeName= guid; 
			model.BankName= guid; 
			model.TransmitNumber= guid; 
			model.PaymentDate= DateTime.Now; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _WorkorderInvoicePaymentsBL.DMLWorkorderInvoicePayments(UserManager.UserInfo, model);
			
			var filter = new WorkorderInvoicePaymentsDetailModel();
			filter.Id = result.Model.Id;
			
			 var resultGet = _WorkorderInvoicePaymentsBL.GetWorkorderInvoicePayments(UserManager.UserInfo, filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.Id > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void WorkorderInvoicePaymentsBL_GetPaymentTypeList_GetAll()
		{
			 var resultGet = _WorkorderInvoicePaymentsBL.GetPaymentTypeList(UserManager.UserInfo);
			
			Assert.IsTrue(resultGet.Total > 0);
		}
        
		[TestMethod]
		public void WorkorderInvoicePaymentsBL_ListWorkorderInvoicePayments_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new WorkorderInvoicePaymentsDetailModel();
			model.WorkorderInvoiceId= 1; 
			model.WorkorderId= 1; 
			model.BankRequired= true; 
			model.InstalmentNumberRequired= true; 
			model.TransmitNumberRequired= true; 
			model.PaymentTypeName= guid; 
			model.BankName= guid; 
			model.TransmitNumber= guid; 
			model.PaymentDate= DateTime.Now; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _WorkorderInvoicePaymentsBL.DMLWorkorderInvoicePayments(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new WorkorderInvoicePaymentsListModel();
			
			 var resultGet = _WorkorderInvoicePaymentsBL.ListWorkorderInvoicePayments(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void WorkorderInvoicePaymentsBL_DMLWorkorderInvoicePayments_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new WorkorderInvoicePaymentsDetailModel();
			model.WorkorderInvoiceId= 1; 
			model.WorkorderId= 1; 
			model.BankRequired= true; 
			model.InstalmentNumberRequired= true; 
			model.TransmitNumberRequired= true; 
			model.PaymentTypeName= guid; 
			model.BankName= guid; 
			model.TransmitNumber= guid; 
			model.PaymentDate= DateTime.Now; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _WorkorderInvoicePaymentsBL.DMLWorkorderInvoicePayments(UserManager.UserInfo, model);
			
			var filter = new WorkorderInvoicePaymentsListModel();
			
			int count = 0;
			 var resultGet = _WorkorderInvoicePaymentsBL.ListWorkorderInvoicePayments(UserManager.UserInfo, filter, out count);
			
			var modelUpdate = new WorkorderInvoicePaymentsDetailModel();
			modelUpdate.Id = resultGet.Data.First().Id;
			
			
			
			
			
			modelUpdate.PaymentTypeName= guid; 
			modelUpdate.BankName= guid; 
			modelUpdate.TransmitNumber= guid; 
			
			
			
			
			modelUpdate.CommandType = "U";
			 var resultUpdate = _WorkorderInvoicePaymentsBL.DMLWorkorderInvoicePayments(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void WorkorderInvoicePaymentsBL_DMLWorkorderInvoicePayments_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new WorkorderInvoicePaymentsDetailModel();
			model.WorkorderInvoiceId= 1; 
			model.WorkorderId= 1; 
			model.BankRequired= true; 
			model.InstalmentNumberRequired= true; 
			model.TransmitNumberRequired= true; 
			model.PaymentTypeName= guid; 
			model.BankName= guid; 
			model.TransmitNumber= guid; 
			model.PaymentDate= DateTime.Now; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _WorkorderInvoicePaymentsBL.DMLWorkorderInvoicePayments(UserManager.UserInfo, model);
			
			var filter = new WorkorderInvoicePaymentsListModel();
			
			int count = 0;
			 var resultGet = _WorkorderInvoicePaymentsBL.ListWorkorderInvoicePayments(UserManager.UserInfo, filter, out count);
			
			var modelDelete = new WorkorderInvoicePaymentsDetailModel();
			modelDelete.Id = resultGet.Data.First().Id;
			
			
			
			
			
			modelDelete.PaymentTypeName= guid; 
			modelDelete.BankName= guid; 
			modelDelete.TransmitNumber= guid; 
			
			
			
			
			modelDelete.CommandType = "D";
			 var resultDelete = _WorkorderInvoicePaymentsBL.DMLWorkorderInvoicePayments(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}

