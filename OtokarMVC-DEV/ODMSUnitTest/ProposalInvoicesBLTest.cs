using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using System;
using ODMSBusiness.Business;


namespace ODMSUnitTest
{

	[TestClass]
	public class ProposalInvoicesBLTest
	{

		ProposalInvoicesBL _ProposalInvoicesBL = new ProposalInvoicesBL();

		[TestMethod]
		public void ProposalInvoicesBL_GetProposalInvoice_GetModel()
		{
			 var resultGet = _ProposalInvoicesBL.GetProposalInvoice(UserManager.UserInfo, 1);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.ProposalInvoiceId > 0 && resultGet.IsSuccess);
		}


	}

}

