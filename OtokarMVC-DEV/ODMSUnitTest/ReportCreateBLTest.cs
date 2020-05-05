using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.Reports;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class ReportCreateBLTest
	{

		ReportCreateBL _ReportCreateBL = new ReportCreateBL();

		[TestMethod]
		public void ReportCreateBL_DMLReportCreate_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ReportCreateModel();
			model.FilePath= guid; 
			model.Columns= guid; 
			model.ReportType= 1; 
			model.ParametersString= guid; 
			model.CreateUser= guid; 
			model.CommandType = "I";
			 var result = _ReportCreateBL.DMLReportCreate(model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void ReportCreateBL_GetAllReportCreate_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ReportCreateModel();
			model.FilePath= guid; 
			model.Columns= guid; 
			model.ReportType= 1; 
			model.ParametersString= guid; 
			model.CreateUser= guid; 
			model.CommandType = "I";
			 var result = _ReportCreateBL.DMLReportCreate(model);
			
			var filter = new ReportCreateModel();
			filter.Id = result.Model.Id;
			int totalCnt = 0;
			 var resultGet = _ReportCreateBL.GetAllReportCreate(filter,out totalCnt);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.Id > 0 && resultGet.IsSuccess);
		}


	}

}

