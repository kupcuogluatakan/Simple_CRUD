using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.Education;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class EducationBLTest
    {

        EducationBL _EducationBL = new EducationBL();

        [TestMethod]
        public void EducationBL_DMLEducation_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new EducationViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.EducationCode = guid;
            model.EducationTypeName = guid;
            model.EducationDurationDay = 1;
            model.EducationDurationHour = 1;
            model.AdminDesc = guid;
            model.VehicleModelCode = guid;
            model.VehicleModelName = guid;
            model.IsActive = true;
            model.IsMandatory = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _EducationBL.DMLEducation(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void EducationBL_GetEducation_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new EducationViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.EducationCode = guid;
            model.EducationTypeName = guid;
            model.EducationDurationDay = 1;
            model.EducationDurationHour = 1;
            model.AdminDesc = guid;
            model.VehicleModelCode = guid;
            model.VehicleModelName = guid;
            model.IsActive = true;
            model.IsMandatory = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _EducationBL.DMLEducation(UserManager.UserInfo, model);

            var filter = new EducationViewModel();
            filter.MultiLanguageContentAsText = "TR || TEST";
            filter.EducationCode = guid;
            filter.VehicleModelCode = guid;

            var resultGet = _EducationBL.GetEducation(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.AdminDesc != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void EducationBL_GetEducationList_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new EducationViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.EducationCode = guid;
            model.EducationTypeName = guid;
            model.EducationDurationDay = 1;
            model.EducationDurationHour = 1;
            model.AdminDesc = guid;
            model.VehicleModelCode = guid;
            model.VehicleModelName = guid;
            model.IsActive = true;
            model.IsMandatory = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _EducationBL.DMLEducation(UserManager.UserInfo, model);

            int count = 0;
            var filter = new EducationListModel();
            filter.EducationCode = guid;

            var resultGet = _EducationBL.GetEducationList(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void EducationBL_DMLEducation_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new EducationViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.EducationCode = guid;
            model.EducationTypeName = guid;
            model.EducationDurationDay = 1;
            model.EducationDurationHour = 1;
            model.AdminDesc = guid;
            model.VehicleModelCode = guid;
            model.VehicleModelName = guid;
            model.IsActive = true;
            model.IsMandatory = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _EducationBL.DMLEducation(UserManager.UserInfo, model);

            var filter = new EducationListModel();
            filter.EducationCode = guid;

            int count = 0;
            var resultGet = _EducationBL.GetEducationList(UserManager.UserInfo, filter, out count);

            var modelUpdate = new EducationViewModel();
            modelUpdate.MultiLanguageContentAsText = "TR || TEST";
            modelUpdate.EducationCode = guid;
            modelUpdate.EducationTypeName = guid;
            modelUpdate.AdminDesc = guid;
            modelUpdate.VehicleModelCode = guid;
            modelUpdate.VehicleModelName = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _EducationBL.DMLEducation(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void EducationBL_DMLEducation_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new EducationViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.EducationCode = guid;
            model.EducationTypeName = guid;
            model.EducationDurationDay = 1;
            model.EducationDurationHour = 1;
            model.AdminDesc = guid;
            model.VehicleModelCode = guid;
            model.VehicleModelName = guid;
            model.IsActive = true;
            model.IsMandatory = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _EducationBL.DMLEducation(UserManager.UserInfo, model);

            var filter = new EducationListModel();
            filter.EducationCode = guid;

            int count = 0;
            var resultGet = _EducationBL.GetEducationList(UserManager.UserInfo, filter, out count);

            var modelDelete = new EducationViewModel();
            modelDelete.MultiLanguageContentAsText = "TR || TEST";
            modelDelete.EducationCode = guid;
            modelDelete.EducationTypeName = guid;
            modelDelete.AdminDesc = guid;
            modelDelete.VehicleModelCode = guid;
            modelDelete.VehicleModelName = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _EducationBL.DMLEducation(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }

        [TestMethod]
        public void EducationBL_ListEducationTypeAsSelectList_GetAll()
        {
            var resultGet = EducationBL.ListEducationTypeAsSelectList(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }

    }

}

