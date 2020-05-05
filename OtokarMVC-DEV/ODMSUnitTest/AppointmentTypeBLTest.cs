using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;


namespace ODMSUnitTest
{

    [TestClass]
    public class AppointmentTypeBLTest
    {

        AppointmentTypeBL _AppointmentTypeBL = new AppointmentTypeBL();

        [TestMethod]
        public void AppointmentTypeBL_ListAppointmentTypeAsSelectListItem_GetAll()
        {
            var resultGet = AppointmentTypeBL.ListAppointmentTypeAsSelectListItem(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

