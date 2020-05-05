using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.Title;
using System;
using ODMSBusiness.Business;

namespace ODMSUnitTest
{

    [TestClass]
    public class TitleBLTest
    {

        TitleBL _TitleBL = new TitleBL();

        [TestMethod]
        public void TitleBL_DMLTitle_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new TitleIndexViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.TitleId = 1;
            model.AdminDesc = guid;
            model._Status = true;
            model.StatusName = guid;
            model.IsTechnician = 1;
            model.IsTechnicianName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _TitleBL.DMLTitle(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void TitleBL_GetTitle_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new TitleIndexViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.TitleId = 1;
            model.AdminDesc = guid;
            model._Status = true;
            model.StatusName = guid;
            model.IsTechnician = 1;
            model.IsTechnicianName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _TitleBL.DMLTitle(UserManager.UserInfo, model);

            var filter = new TitleIndexViewModel();
            filter.TitleId = result.Model.TitleId;
            filter.MultiLanguageContentAsText = "TR || TEST";

            var resultGet = _TitleBL.GetTitle(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.AdminDesc != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void TitleBL_ListTitle_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new TitleIndexViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.TitleId = 1;
            model.AdminDesc = guid;
            model._Status = true;
            model.StatusName = guid;
            model.IsTechnician = 1;
            model.IsTechnicianName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _TitleBL.DMLTitle(UserManager.UserInfo, model);

            int count = 0;
            var filter = new TitleListModel();
            filter.MultiLanguageContentAsText = "TR || TEST";

            var resultGet = _TitleBL.ListTitle(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void TitleBL_DMLTitle_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new TitleIndexViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.TitleId = 1;
            model.AdminDesc = guid;
            model._Status = true;
            model.StatusName = guid;
            model.IsTechnician = 1;
            model.IsTechnicianName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _TitleBL.DMLTitle(UserManager.UserInfo, model);

            var filter = new TitleListModel();
            filter.MultiLanguageContentAsText = "TR || TEST";

            int count = 0;
            var resultGet = _TitleBL.ListTitle(UserManager.UserInfo, filter, out count);

            var modelUpdate = new TitleIndexViewModel();
            modelUpdate.TitleId = resultGet.Data.First().TitleId;
            modelUpdate.MultiLanguageContentAsText = "TR || TEST";
            modelUpdate.AdminDesc = guid;
            modelUpdate.StatusName = guid;
            modelUpdate.IsTechnicianName = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _TitleBL.DMLTitle(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void TitleBL_DMLTitle_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new TitleIndexViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.TitleId = 1;
            model.AdminDesc = guid;
            model._Status = true;
            model.StatusName = guid;
            model.IsTechnician = 1;
            model.IsTechnicianName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _TitleBL.DMLTitle(UserManager.UserInfo, model);

            var filter = new TitleListModel();
            filter.MultiLanguageContentAsText = "TR || TEST";

            int count = 0;
            var resultGet = _TitleBL.ListTitle(UserManager.UserInfo, filter, out count);

            var modelDelete = new TitleIndexViewModel();
            modelDelete.TitleId = resultGet.Data.First().TitleId;
            modelDelete.MultiLanguageContentAsText = "TR || TEST";
            modelDelete.AdminDesc = guid;
            modelDelete.StatusName = guid;
            modelDelete.IsTechnicianName = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _TitleBL.DMLTitle(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }

        [TestMethod]
        public void TitleBL_ListUserIncludedInTitle_GetAll()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new TitleIndexViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.TitleId = 1;
            model.AdminDesc = guid;
            model._Status = true;
            model.StatusName = guid;
            model.IsTechnician = 1;
            model.IsTechnicianName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _TitleBL.DMLTitle(UserManager.UserInfo, model);

            var resultGet = _TitleBL.ListUserIncludedInTitle(UserManager.UserInfo, result.Model.TitleId);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void TitleBL_ListTitleTypeAsSelectListItem_GetAll()
        {
            var resultGet = TitleBL.ListTitleTypeAsSelectListItem(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void TitleBL_ListTitleTypeCombo_GetAll()
        {
            var resultGet = TitleBL.ListTitleTypeCombo(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

