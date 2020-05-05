using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.Title;
using ODMSBusiness.Business;

namespace ODMSUnitTest
{

    [TestClass]
    public class UserTitleBLTest
    {

        UserTitleBL _UserTitleBL = new UserTitleBL();

        [TestMethod]
        public void UserTitleBL_DMLUserTitle_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new UserTitleViewModel();
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _UserTitleBL.DMLUserTitle(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }


    }

}

