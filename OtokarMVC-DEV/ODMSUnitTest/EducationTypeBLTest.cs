using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.EducationType;


namespace ODMSUnitTest
{

    [TestClass]
    public class EducationTypeBLTest
    {

        EducationTypeBL _EducationTypeBL = new EducationTypeBL();

        [TestMethod]
        public void EducationTypeBL_DMLEducationType_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new EducationTypeDetailModel();
            model.Description = guid;
            model.MultiLanguageContentAsText = "TR || TEST";
            model.Name = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _EducationTypeBL.DMLEducationType(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void EducationTypeBL_GetEducationType_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new EducationTypeDetailModel();
            model.Description = guid;
            model.MultiLanguageContentAsText = "TR || TEST";
            model.Name = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _EducationTypeBL.DMLEducationType(UserManager.UserInfo, model);

            var filter = new EducationTypeDetailModel();
            filter.Id = result.Model.Id;
            filter.MultiLanguageContentAsText = "TR || TEST";

            var resultGet = _EducationTypeBL.GetEducationType(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Id > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void EducationTypeBL_ListEducationTypes_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new EducationTypeDetailModel();
            model.Description = guid;
            model.MultiLanguageContentAsText = "TR || TEST";
            model.Name = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _EducationTypeBL.DMLEducationType(UserManager.UserInfo, model);

            int count = 0;
            var filter = new EducationTypeListModel();

            var resultGet = _EducationTypeBL.ListEducationTypes(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void EducationTypeBL_DMLEducationType_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new EducationTypeDetailModel();
            model.Description = guid;
            model.MultiLanguageContentAsText = "TR || TEST";
            model.Name = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _EducationTypeBL.DMLEducationType(UserManager.UserInfo, model);

            var filter = new EducationTypeListModel();

            int count = 0;
            var resultGet = _EducationTypeBL.ListEducationTypes(UserManager.UserInfo, filter, out count);

            var modelUpdate = new EducationTypeDetailModel();
            modelUpdate.Id = resultGet.Data.First().Id;
            modelUpdate.Description = guid;
            modelUpdate.MultiLanguageContentAsText = "TR || TEST";
            modelUpdate.Name = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _EducationTypeBL.DMLEducationType(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void EducationTypeBL_DMLEducationType_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new EducationTypeDetailModel();
            model.Description = guid;
            model.MultiLanguageContentAsText = "TR || TEST";
            model.Name = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _EducationTypeBL.DMLEducationType(UserManager.UserInfo, model);

            var filter = new EducationTypeListModel();

            int count = 0;
            var resultGet = _EducationTypeBL.ListEducationTypes(UserManager.UserInfo, filter, out count);

            var modelDelete = new EducationTypeDetailModel();
            modelDelete.Id = resultGet.Data.First().Id;
            modelDelete.Description = guid;
            modelDelete.MultiLanguageContentAsText = "TR || TEST";
            modelDelete.Name = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _EducationTypeBL.DMLEducationType(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}

