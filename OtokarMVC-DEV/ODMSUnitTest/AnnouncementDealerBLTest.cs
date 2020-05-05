using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.AnnouncementDealer;
using System;
using System.Collections.Generic;

namespace ODMSUnitTest
{

    [TestClass]
    public class AnnouncementDealerBLTest
    {

        AnnouncementDealerBL _AnnouncementDealerBL = new AnnouncementDealerBL();

        [TestMethod]
        public void AnnouncementDealerBL_Save_Insert()
        {
            var dealerList = new List<int>();
            dealerList.Add(UserManager.UserInfo.DealerID);

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AnnouncementDealerModel();
            model.AnnouncementId = 4;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            model.DealerList = dealerList;
            var result = _AnnouncementDealerBL.Save(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void AnnouncementDealerBL_ListAnnouncementDealersIncluded_GetAll()
        {
            var dealerList = new List<int>();
            dealerList.Add(UserManager.UserInfo.DealerID);


            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AnnouncementDealerModel();
            model.AnnouncementId = 4;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.DealerList = dealerList;
            model.CommandType = "I";
            var result = _AnnouncementDealerBL.Save(UserManager.UserInfo, model);

            var resultGet = _AnnouncementDealerBL.ListAnnouncementDealersIncluded(result.Model.AnnouncementId);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void AnnouncementDealerBL_ListAnnouncementDealersExcluded_GetAll()
        {
            var dealerList = new List<int>();
            dealerList.Add(UserManager.UserInfo.DealerID);

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AnnouncementDealerModel();
            model.AnnouncementId = 4;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.DealerList = dealerList;
            model.CommandType = "I";
            var result = _AnnouncementDealerBL.Save(UserManager.UserInfo, model);

            var resultGet = _AnnouncementDealerBL.ListAnnouncementDealersExcluded(result.Model.AnnouncementId, 1, "ATLAS");

            Assert.IsTrue(resultGet.Total > 0);
        }

    }

}

