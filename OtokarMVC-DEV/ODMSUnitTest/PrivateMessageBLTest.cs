using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using System;
using ODMSModel.PrivateMessage;


namespace ODMSUnitTest
{

	[TestClass]
	public class PrivateMessageBLTest
	{

		PrivateMessageBL _PrivateMessageBL = new PrivateMessageBL();

		[TestMethod]
		public void PrivateMessageBL_GetMessageHistory_GetModel()
		{
		    int totalCnt = 0;
			 var resultGet = _PrivateMessageBL.GetMessageHistory(UserManager.UserInfo, 1,1,out totalCnt);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.MessageId > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void PrivateMessageBL_ListMessages_GetAll()
		{
			
			int count = 0;
			var filter = new PrivateMessageListModel();
			
			 var resultGet = _PrivateMessageBL.ListMessages(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void PrivateMessageBL_ListRecievers_GetAll()
		{
		    var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var resultGet = _PrivateMessageBL.ListRecievers(UserManager.UserInfo, guid);
			
			Assert.IsTrue(resultGet.Total > 0);
		}


	}

}

