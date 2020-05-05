using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.Announcement;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class AnnouncementBLTest
    {

        AnnouncementBL _AnnouncementBL = new AnnouncementBL();

        [TestMethod]
        public void AnnouncementBL_DMLAnnouncement_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AnnouncementViewModel();
            model.AnnouncementId = 1;
            model.Title = guid;
            model.Body = guid;
            model.IsActive = 1;
            model.IsActiveName = guid;
            model.IsUrgent = true;
            model.IsUrgentName = guid;
            model.SendMail = true;
            model.SendMailName = guid;
            model.PublishUserName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = 1;
            model.StartDate = DateTime.Now;
            model.CommandType = "I";
            var result = _AnnouncementBL.DMLAnnouncement(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void AnnouncementBL_GetAnnouncement_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AnnouncementViewModel();
            model.AnnouncementId = 1;
            model.Title = guid;
            model.Body = guid;
            model.IsActive = 1;
            model.IsActiveName = guid;
            model.IsUrgent = true;
            model.IsUrgentName = guid;
            model.SendMail = true;
            model.SendMailName = guid;
            model.PublishUserName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = 1;
            model.StartDate = DateTime.Now;
            model.CommandType = "I";
            var result = _AnnouncementBL.DMLAnnouncement(UserManager.UserInfo, model);

            var filter = new AnnouncementViewModel();
            filter.AnnouncementId = result.Model.AnnouncementId;

            var resultGet = _AnnouncementBL.GetAnnouncement(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.AnnouncementId > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void AnnouncementBL_ListAnnouncements_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AnnouncementViewModel();
            model.Title = guid;
            model.Body = guid;
            model.IsActive = 1;
            model.IsActiveName = guid;
            model.IsUrgent = true;
            model.IsUrgentName = guid;
            model.SendMail = true;
            model.SendMailName = guid;
            model.PublishUserName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = 1;
            model.StartDate = DateTime.Now;
            model.CommandType = "I";
            var result = _AnnouncementBL.DMLAnnouncement(UserManager.UserInfo, model);

            var filter = new AnnouncementListModel();

            var resultGet = _AnnouncementBL.ListAnnouncements(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void AnnouncementBL_ListMailUsers_GetAll()
        {
            var resultGet = _AnnouncementBL.ListMailUsers(4);

            Assert.IsTrue(resultGet.Total > 0);
        }

    }

}

