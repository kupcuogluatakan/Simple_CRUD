using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.AppointmentIndicatorMainCategory;


namespace ODMSUnitTest
{

    [TestClass]
    public class AppointmentIndicatorMainCategoryBLTest
    {

        AppointmentIndicatorMainCategoryBL _AppointmentIndicatorMainCategoryBL = new AppointmentIndicatorMainCategoryBL();

        [TestMethod]
        public void AppointmentIndicatorMainCategoryBL_DMLAppointmentIndicatorMainCategory_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AppointmentIndicatorMainCategoryViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.AppointmentIndicatorMainCategoryId = 1;
            model.MainCode = guid;
            model.Code = guid;
            model.SubCode = guid;
            model.IndicatorTypeCode = "IT_C";
            model.AdminDesc = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _AppointmentIndicatorMainCategoryBL.DMLAppointmentIndicatorMainCategory(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void AppointmentIndicatorMainCategoryBL_GetAppointmentIndicatorMainCategory_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AppointmentIndicatorMainCategoryViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.AppointmentIndicatorMainCategoryId = 1;
            model.MainCode = guid;
            model.Code = guid;
            model.SubCode = guid;
            model.IndicatorTypeCode = "IT_C";
            model.AdminDesc = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _AppointmentIndicatorMainCategoryBL.DMLAppointmentIndicatorMainCategory(UserManager.UserInfo, model);

            var filter = new AppointmentIndicatorMainCategoryViewModel();
            filter.AppointmentIndicatorMainCategoryId = result.Model.AppointmentIndicatorMainCategoryId;
            filter.MultiLanguageContentAsText = "TR || TEST";
            filter.MainCode = guid;
            filter.Code = guid;
            filter.SubCode = guid;
            filter.IndicatorTypeCode = "IT_C";

            var resultGet = _AppointmentIndicatorMainCategoryBL.GetAppointmentIndicatorMainCategory(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.AdminDesc != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void AppointmentIndicatorMainCategoryBL_GetAppointmentIndicatorMainCategoryByMainCode_GetModel()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AppointmentIndicatorMainCategoryViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.AppointmentIndicatorMainCategoryId = 1;
            model.MainCode = guid;
            model.Code = guid;
            model.SubCode = guid;
            model.IndicatorTypeCode = "IT_C";
            model.AdminDesc = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _AppointmentIndicatorMainCategoryBL.DMLAppointmentIndicatorMainCategory(UserManager.UserInfo, model);

            var resultGet = _AppointmentIndicatorMainCategoryBL.GetAppointmentIndicatorMainCategoryByMainCode(UserManager.UserInfo, guid);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.AdminDesc != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void AppointmentIndicatorMainCategoryBL_ListAppointmentIndicatorMainCategories_GetAll()
        {
            var resultGet = AppointmentIndicatorMainCategoryBL.ListAppointmentIndicatorMainCategories(UserManager.UserInfo, true);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void AppointmentIndicatorMainCategoryBL_DictioanryListAppointmentIndicatorMainCategories_GetAll()
        {
            var resultGet = AppointmentIndicatorMainCategoryBL.DictioanryListAppointmentIndicatorMainCategories(UserManager.UserInfo, true);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void AppointmentIndicatorMainCategoryBL_ListAppointmentIndicatorMainCategoryCodes_GetAll()
        {
            var resultGet = AppointmentIndicatorMainCategoryBL.ListAppointmentIndicatorMainCategoryCodes(UserManager.UserInfo, true);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void AppointmentIndicatorMainCategoryBL_GetAppointmentIndicatorMainCategoryList_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AppointmentIndicatorMainCategoryViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.AppointmentIndicatorMainCategoryId = 1;
            model.MainCode = guid;
            model.Code = guid;
            model.SubCode = guid;
            model.IndicatorTypeCode = "IT_C";
            model.AdminDesc = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _AppointmentIndicatorMainCategoryBL.DMLAppointmentIndicatorMainCategory(UserManager.UserInfo, model);

            int count = 0;
            var filter = new AppointmentIndicatorMainCategoryListModel();
            filter.MainCode = guid;

            var resultGet = _AppointmentIndicatorMainCategoryBL.GetAppointmentIndicatorMainCategoryList(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void AppointmentIndicatorMainCategoryBL_ListAppointmentIndicatorMainCategories_GetAll_1()
        {
            var resultGet = AppointmentIndicatorMainCategoryBL.ListAppointmentIndicatorMainCategories(UserManager.UserInfo, true);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

