using ODMSBusiness;
using ODMSModel.ServiceCallSchedule;
using ODMSModel.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml;
using System.Xml.Linq;
using NUnit.Framework;
using ODMSData.Utility;

namespace ODMSServiceTests
{
    [TestFixture]
    public class CommonTests
    {
        [Test]
        public void DealerExists()
        {
            var dbhelper = new DbHelper();
            var isDealerExists = dbhelper.ExecuteScalar<bool>("P_EXISTS_DEALER_BY_DEALER_SSID", "0000200162");
            Assert.IsTrue(isDealerExists);

        }
    }
}
