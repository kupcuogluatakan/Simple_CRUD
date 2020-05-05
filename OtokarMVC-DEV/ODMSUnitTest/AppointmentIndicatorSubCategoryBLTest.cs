using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.AppointmentIndicatorSubCategory;
using System.Collections.Generic;


namespace ODMSUnitTest
{

    [TestClass]
    public class AppointmentIndicatorSubCategoryBLTest
    {

        AppointmentIndicatorSubCategoryBL _AppointmentIndicatorSubCategoryBL = new AppointmentIndicatorSubCategoryBL();

        [TestMethod]
        public void AppointmentIndicatorSubCategoryBL_DMLAppointmentIndicatorSubCategory_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AppointmentIndicatorSubCategoryViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model._AppointmentIndicatorSubCategoryName = guid;
            model.AppointmentIndicatorSubCategoryId = 1;
            model.AppointmentIndicatorMainCategoryName = guid;
            model.AppointmentIndicatorCategoryName = guid;
            model.SubCode = guid;
            model.AdminDesc = guid;
            model.IsAutoCreateName = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.IndicatorTypeCode = "IT_C";
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _AppointmentIndicatorSubCategoryBL.DMLAppointmentIndicatorSubCategory(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void AppointmentIndicatorSubCategoryBL_DMLAppointmentIndicatorSubCategory_Insert_1()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AppointmentIndicatorSubCategoryViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model._AppointmentIndicatorSubCategoryName = guid;
            model.AppointmentIndicatorSubCategoryId = 1;
            model.AppointmentIndicatorMainCategoryName = guid;
            model.AppointmentIndicatorCategoryName = guid;
            model.SubCode = guid;
            model.AdminDesc = guid;
            model.IsAutoCreateName = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.IndicatorTypeCode = "IT_C";
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _AppointmentIndicatorSubCategoryBL.DMLAppointmentIndicatorSubCategory(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void AppointmentIndicatorSubCategoryBL_GetAppointmentIndicatorSubCategory_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AppointmentIndicatorSubCategoryViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model._AppointmentIndicatorSubCategoryName = guid;
            model.AppointmentIndicatorSubCategoryId = 1;
            model.AppointmentIndicatorMainCategoryName = guid;
            model.AppointmentIndicatorCategoryName = guid;
            model.SubCode = guid;
            model.AdminDesc = guid;
            model.IsAutoCreateName = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.IndicatorTypeCode = "IT_C";
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _AppointmentIndicatorSubCategoryBL.DMLAppointmentIndicatorSubCategory(UserManager.UserInfo, model);

            var filter = new AppointmentIndicatorSubCategoryViewModel();
            filter.AppointmentIndicatorSubCategoryId = result.Model.AppointmentIndicatorSubCategoryId;

            var resultGet = _AppointmentIndicatorSubCategoryBL.GetAppointmentIndicatorSubCategory(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.AdminDesc != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void AppointmentIndicatorSubCategoryBL_GetAppointmentIndicatorSubCategoryBySubCode_GetModel()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AppointmentIndicatorSubCategoryViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model._AppointmentIndicatorSubCategoryName = guid;
            model.AppointmentIndicatorSubCategoryId = 1;
            model.AppointmentIndicatorMainCategoryName = guid;
            model.AppointmentIndicatorCategoryName = guid;
            model.SubCode = guid;
            model.AdminDesc = guid;
            model.IsAutoCreateName = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.IndicatorTypeCode = "IT_C";
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _AppointmentIndicatorSubCategoryBL.DMLAppointmentIndicatorSubCategory(UserManager.UserInfo, model);

            var resultGet = _AppointmentIndicatorSubCategoryBL.GetAppointmentIndicatorSubCategoryBySubCode(UserManager.UserInfo, guid);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.AdminDesc != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void AppointmentIndicatorSubCategoryBL_ListAppointmentIndicatorSubCategories_GetAll()
        {
            var resultGet = AppointmentIndicatorSubCategoryBL.ListAppointmentIndicatorSubCategories(UserManager.UserInfo, null, true);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void AppointmentIndicatorSubCategoryBL_ListAppointmentIndicatorSubCategories_GetAll_1()
        {
            var resultGet = AppointmentIndicatorSubCategoryBL.ListAppointmentIndicatorSubCategories(UserManager.UserInfo, null, true);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void AppointmentIndicatorSubCategoryBL_ListAppointmentIndicatorSubCategoryCodes_GetAll()
        {
            var resultGet = AppointmentIndicatorSubCategoryBL.ListAppointmentIndicatorSubCategoryCodes(UserManager.UserInfo, true);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void AppointmentIndicatorSubCategoryBL_GetAppointmentIndicatorSubCategoryList_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AppointmentIndicatorSubCategoryViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model._AppointmentIndicatorSubCategoryName = guid;
            model.AppointmentIndicatorSubCategoryId = 1;
            model.AppointmentIndicatorMainCategoryName = guid;
            model.AppointmentIndicatorCategoryName = guid;
            model.SubCode = guid;
            model.AdminDesc = guid;
            model.IsAutoCreateName = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.IndicatorTypeCode = "IT_C";
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _AppointmentIndicatorSubCategoryBL.DMLAppointmentIndicatorSubCategory(UserManager.UserInfo, model);

            int count = 0;
            var filter = new AppointmentIndicatorSubCategoryListModel();
            filter.SubCode = guid;

            var resultGet = _AppointmentIndicatorSubCategoryBL.GetAppointmentIndicatorSubCategoryList(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void AppointmentIndicatorSubCategoryBL_ListOfIndicatorTypeCode_GetAll()
        {
            var resultGet = _AppointmentIndicatorSubCategoryBL.ListOfIndicatorTypeCode(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void AppointmentIndicatorSubCategoryBL_ListOfIndicatorTypeCodeAsSelectListItem_GetAll()
        {
            var resultGet = _AppointmentIndicatorSubCategoryBL.ListOfIndicatorTypeCodeAsSelectListItem(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

