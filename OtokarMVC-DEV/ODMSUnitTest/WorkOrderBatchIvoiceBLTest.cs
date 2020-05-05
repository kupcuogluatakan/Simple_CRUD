using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.WorkOrderBatchInvoice;


namespace ODMSUnitTest
{

    [TestClass]
    public class WorkOrderBatchIvoiceBLTest
    {

        WorkOrderBatchIvoiceBL _WorkOrderBatchIvoiceBL = new WorkOrderBatchIvoiceBL();

        [TestMethod]
        public void WorkOrderBatchIvoiceBL_List_GetAll()
        {
            var filter = new WorkOrderBatchInvoiceList();
            filter.DealerId = UserManager.UserInfo.DealerID;
            var resultGet = _WorkOrderBatchIvoiceBL.List(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

