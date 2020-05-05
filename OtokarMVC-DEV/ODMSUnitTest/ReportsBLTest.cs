using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.Reports;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class ReportsBLTest
	{

		ReportsBL _ReportsBL = new ReportsBL();

		[TestMethod]
		public void ReportsBL_ListWorkOrderDetailReport_GetAll()
		{
		    var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            int count = 0;
			var filter = new WorkOrderDetailReportListModel();
			filter.CurrencyCode = guid; 
			filter.ModelKod = "ATLAS"; 
			filter.FleetCode = guid; 
			filter.SAPCode = guid; 
			filter.VehicleId = 29627; 
			filter.PartCode = "M.162127"; 
			filter.LabourCode = guid;
		    int totalVehicle = 0;
            var resultGet = _ReportsBL.ListWorkOrderDetailReport(UserManager.UserInfo, filter, out count, out totalVehicle);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void ReportsBL_ListWorkOrderMaintReport_GetAll()
		{
			
			int count = 0;
			var filter = new WorkOrderMaintReportListModel();
			filter.ModelKod = "ATLAS"; 
			filter.VehicleId = 29627; 
			
			 var resultGet = _ReportsBL.ListWorkOrderMaintReport(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void ReportsBL_ListPartInfo_GetAll()
		{
			
			int count = 0;
			var filter = new PartInfoRequest();
			filter.DealerId = UserManager.UserInfo.DealerID.ToString(); 
			filter.PartId = 39399; 
			filter.PartCode = "M.162127"; 
			
			 var resultGet = _ReportsBL.ListPartInfo(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void ReportsBL_ListSaleReport_GetAll()
		{
			
			int count = 0;
			var filter = new SaleReportFilterRequest();
			filter.DealerId = UserManager.UserInfo.DealerID.ToString(); 
			filter.PartCode = "M.162127"; 
			
			 var resultGet = _ReportsBL.ListSaleReport(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void ReportsBL_ListInvoiceNoForAutoComplete_GetAll()
		{
			 var resultGet = _ReportsBL.ListInvoiceNoForAutoComplete("A","86");
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void ReportsBL_ListInvoiceInfoReport_GetAll()
		{
			
			int count = 0;
			var filter = new InvoiceInfoFilterRequest();
			filter.PartCode = "M.162127"; 
			
			 var resultGet = _ReportsBL.ListInvoiceInfoReport(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void ReportsBL_ListPersonnelInfoReport_GetAll()
		{
		    var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            int count = 0;
			var filter = new PersonnelInfoReportFilterRequest();
			filter.UserCodeList = guid; 
			filter.EducationCode = guid; 
			
			 var resultGet = _ReportsBL.ListPersonnelInfoReport(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void ReportsBL_ListTcIdentityNoForAutoComplete_GetAll()
		{
			 var resultGet = _ReportsBL.ListTcIdentityNoForAutoComplete("48913271162");
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void ReportsBL_ListUserCodeForAutoComplete_GetAll()
		{
			 var resultGet = _ReportsBL.ListUserCodeForAutoComplete("merkez");
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void ReportsBL_ListUserNameForAutoComplete_GetAll()
		{
			 var resultGet = _ReportsBL.ListUserNameForAutoComplete("merkez");
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void ReportsBL_ListDealerCustomersForAutoComplete_GetAll()
		{
			 var resultGet = _ReportsBL.ListDealerCustomersForAutoComplete("14101","86");
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void ReportsBL_ListWorkOrderInfo_GetAll()
		{
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
			int count = 0;
			var filter = new WorkOrderInfoRequest();
			filter.VehicleId = 29627; 
			filter.GroupCode = guid; 
			
			 var resultGet = _ReportsBL.ListWorkOrderInfo(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void ReportsBL_ListWorkOrderDetailKilometer_GetAll()
		{
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
			int count = 0;
			var filter = new WorkOrderDetailKilometerRequest();
			filter.GroupCode = guid; 
			
			 var resultGet = _ReportsBL.ListWorkOrderDetailKilometer(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void ReportsBL_ListCampaignSummaryInfo_GetAll()
		{
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
			int count = 0;
			var filter = new CampaignSummaryInfoRequest();
			filter.DealerId = UserManager.UserInfo.DealerID; 
			filter.CampaignCode = "508"; 
			filter.GroupCode = guid; 
			filter.CurrencyCode = guid; 
			
			 var resultGet = _ReportsBL.ListCampaignSummaryInfo(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}


	}

}

