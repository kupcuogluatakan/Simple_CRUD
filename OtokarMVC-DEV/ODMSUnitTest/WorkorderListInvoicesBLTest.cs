using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.WorkorderListInvoices;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class WorkorderListInvoicesBLTest
	{

		WorkorderListInvoicesBL _WorkorderListInvoicesBL = new WorkorderListInvoicesBL();

		[TestMethod]
		public void WorkorderListInvoicesBL_GetWorkorderInvoicesList_GetAll()
		{
			
			int count = 0;
			var filter = new WorkorderListInvoicesListModel();
			
			 var resultGet = _WorkorderListInvoicesBL.GetWorkorderInvoicesList(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}


	}

}

