using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using NUnit;
using NUnit.Framework;
using ODMSBusiness;
using ODMSModel.ServiceCallSchedule;

namespace ODMSServiceTests
{
    [TestFixture]
    public class ManualDenyReasonService
    {
        [Test]
        public void Run()
        {
            #region Arrange

            var poDetBl = new PurchaseOrderDetailBL();
            var logModel = new ServiceCallLogModel();
            var rValue = new XmlDocument();
            rValue.Load(@"C:\DENY_SERVICE.xml");
            var xmlReader = new XmlNodeReader(rValue);
            var ds = new DataSet();
            ds.ReadXml(xmlReader);
            DataTable dtMaster = ds.Tables["Table1"];

            #endregion

            #region Act
            if (dtMaster != null)
            {
                logModel = poDetBl.SaveDenyReasons(dtMaster);
            }
            #endregion

            #region Assert

            Assert.NotNull(logModel);

            #endregion
        }
    }
}
