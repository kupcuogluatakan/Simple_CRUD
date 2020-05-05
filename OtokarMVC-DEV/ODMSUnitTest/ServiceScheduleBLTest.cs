using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using System;


namespace ODMSUnitTest
{

	[TestClass]
	public class ServiceScheduleBLTest
	{

		ServiceScheduleBL _ServiceScheduleBL = new ServiceScheduleBL();

		[TestMethod]
		public void ServiceScheduleBL_GetServiceScheduleList_GetAll()
		{
		    int totalCnt = 0;
			 var resultGet = _ServiceScheduleBL.GetServiceScheduleList(out totalCnt);
			
			Assert.IsTrue(resultGet.Total > 0);
		}


	}

}

