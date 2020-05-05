using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.AppointmentIndicatorCategory;
using System.Collections.Generic;


namespace ODMSUnitTest
{

    [TestClass]
    public class AppointmentIndicatorCategoryBLTest
    {

        AppointmentIndicatorCategoryBL _AppointmentIndicatorCategoryBL = new AppointmentIndicatorCategoryBL();

        [TestMethod]
        public void AppointmentIndicatorCategoryBL_DMLAppointmentIndicatorCategory_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AppointmentIndicatorCategoryViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.AppointmentIndicatorCategoryId = 1;
            model._AppointmentIndicatorCategoryName = guid;
            model.AppointmentIndicatorMainCategoryName = guid;
            model.Code = guid;
            model.AdminDesc = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _AppointmentIndicatorCategoryBL.DMLAppointmentIndicatorCategory(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }


        [TestMethod]
        public void AppointmentIndicatorCategoryBL_GetAppointmentIndicatorCategory_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AppointmentIndicatorCategoryViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.AppointmentIndicatorCategoryId = 1;
            model._AppointmentIndicatorCategoryName = guid;
            model.AppointmentIndicatorMainCategoryName = guid;
            model.Code = guid;
            model.AdminDesc = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _AppointmentIndicatorCategoryBL.DMLAppointmentIndicatorCategory(UserManager.UserInfo, model);

            var filter = new AppointmentIndicatorCategoryViewModel();
            filter.MultiLanguageContentAsText = "TR || TEST";
            filter.Code = guid;

            var resultGet = _AppointmentIndicatorCategoryBL.GetAppointmentIndicatorCategory(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.AdminDesc != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void AppointmentIndicatorCategoryBL_GetAppointmentIndicatorCategoryByCode_GetModel()
        {
            var resultGet = _AppointmentIndicatorCategoryBL.GetAppointmentIndicatorCategoryByCode(UserManager.UserInfo, "MODEL");

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.AdminDesc != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void AppointmentIndicatorCategoryBL_ListAppointmentIndicatorCategories_GetAll()
        {
            var resultGet = AppointmentIndicatorCategoryBL.ListAppointmentIndicatorCategories(UserManager.UserInfo, null,null);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void AppointmentIndicatorCategoryBL_ListAppointmentIndicatorCategories_1_GetAll()
        {
            var resultGet = AppointmentIndicatorCategoryBL.ListAppointmentIndicatorCategories(UserManager.UserInfo, 1, true);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void AppointmentIndicatorCategoryBL_ListAppointmentIndicatorCategoryCodes_GetAll()
        {
            var resultGet = AppointmentIndicatorCategoryBL.ListAppointmentIndicatorCategoryCodes(UserManager.UserInfo, true);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void AppointmentIndicatorCategoryBL_GetAppointmentIndicatorCategoryList_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AppointmentIndicatorCategoryViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.AppointmentIndicatorCategoryId = 1;
            model._AppointmentIndicatorCategoryName = guid;
            model.AppointmentIndicatorMainCategoryName = guid;
            model.Code = guid;
            model.AdminDesc = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _AppointmentIndicatorCategoryBL.DMLAppointmentIndicatorCategory(UserManager.UserInfo, model);

            int count = 0;
            var filter = new AppointmentIndicatorCategoryListModel();
            filter.Code = guid;

            var resultGet = _AppointmentIndicatorCategoryBL.GetAppointmentIndicatorCategoryList(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void AppointmentIndicatorCategoryBL_GetAppointmentIndicatorCategoryList_1_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AppointmentIndicatorCategoryViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.AppointmentIndicatorCategoryId = 1;
            model._AppointmentIndicatorCategoryName = guid;
            model.AppointmentIndicatorMainCategoryName = guid;
            model.Code = guid;
            model.AdminDesc = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _AppointmentIndicatorCategoryBL.DMLAppointmentIndicatorCategory(UserManager.UserInfo, model);

            int count = 0;
            var filter = new AppointmentIndicatorCategoryListModel();
            filter.Code = guid;

            var resultGet = _AppointmentIndicatorCategoryBL.GetAppointmentIndicatorCategoryList(UserManager.UserInfo, filter, out count, true);

            Assert.IsTrue(resultGet.Total > 0);
        }

    }

}

