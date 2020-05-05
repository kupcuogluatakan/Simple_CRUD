using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.AnnouncementRole;
using System.Collections.Generic;

namespace ODMSUnitTest
{

    [TestClass]
    public class AnnouncementRoleBLTest
    {

        AnnouncementRoleBL _AnnouncementRoleBL = new AnnouncementRoleBL();

        [TestMethod]
        public void AnnouncementRoleBL_SaveAnnouncementRole_Insert()
        {
            var list = new List<int>();
            list.Add(1128);
            list.Add(3157);

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AnnouncementRoleSaveModel();
            model.IdAnnouncement = 4;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.RoleTypeIdList = list;
            model.CommandType = "I";
            var result = _AnnouncementRoleBL.SaveAnnouncementRole(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void AnnouncementRoleBL_ListAnnouncementRole_GetAll()
        {
            var list = new List<int>();
            list.Add(1128);
            list.Add(3157);

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AnnouncementRoleSaveModel();
            model.IdAnnouncement = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.RoleTypeIdList = list;
            model.CommandType = "I";
            var result = _AnnouncementRoleBL.SaveAnnouncementRole(UserManager.UserInfo, model);

            var filter = new AnnouncementRoleListModel();
            filter.IdAnnouncement = result.Model.IdAnnouncement;
            var resultGet = _AnnouncementRoleBL.ListAnnouncementRole(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Total > 0);
        }
       
        [TestMethod]
        public void AnnouncementRoleBL_ListRoleTypeWithoutAnnouncement_GetAll()
        {
            var list = new List<int>();
            list.Add(1128);
            list.Add(3157);

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AnnouncementRoleSaveModel();
            model.IdAnnouncement = 4;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.RoleTypeIdList = list;
            model.CommandType = "I";
            var result = _AnnouncementRoleBL.SaveAnnouncementRole(UserManager.UserInfo, model);

            var filter = new AnnouncementRoleListModel();
            filter.IdAnnouncement = result.Model.IdAnnouncement;
            var resultGet = _AnnouncementRoleBL.ListRoleTypeWithoutAnnouncement(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Total > 0);
        }
       
    }

}

