using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using ODMSModel.EducationContributers;


namespace ODMSUnitTest
{

    [TestClass]
    public class EducationContributersBLTest
    {

        EducationContributersBL _EducationContributersBL = new EducationContributersBL();

        [TestMethod]
        public void EducationContributersBL_DMLEducationContributers_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new EducationContributersViewModel();
            model.IsRequestRoot = true;
            model.EducationCode = guid;
            model.RowNumber = 1;
            model.TCIdentity = guid;
            model.FullName = guid;
            model.WorkingCompany = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.Grade = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _EducationContributersBL.DMLEducationContributers(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void EducationContributersBL_GetEducationContList_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new EducationContributersViewModel();
            model.IsRequestRoot = true;
            model.EducationCode = guid;
            model.RowNumber = 1;
            model.TCIdentity = guid;
            model.FullName = guid;
            model.WorkingCompany = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.Grade = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _EducationContributersBL.DMLEducationContributers(UserManager.UserInfo, model);

            int count = 0;
            var filter = new EducationContributersListModel();
            filter.EducationCode = guid;

            var resultGet = _EducationContributersBL.GetEducationContList(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void EducationContributersBL_DMLEducationContributers_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new EducationContributersViewModel();
            model.IsRequestRoot = true;
            model.EducationCode = guid;
            model.RowNumber = 1;
            model.TCIdentity = guid;
            model.FullName = guid;
            model.WorkingCompany = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.Grade = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _EducationContributersBL.DMLEducationContributers(UserManager.UserInfo, model);

            var filter = new EducationContributersListModel();
            filter.EducationCode = guid;

            int count = 0;
            var resultGet = _EducationContributersBL.GetEducationContList(UserManager.UserInfo, filter, out count);

            var modelUpdate = new EducationContributersViewModel();
            modelUpdate.EducationCode = guid;
            modelUpdate.TCIdentity = guid;
            modelUpdate.FullName = guid;
            modelUpdate.WorkingCompany = guid;
            modelUpdate.DealerId = UserManager.UserInfo.DealerID;
            modelUpdate.Grade = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _EducationContributersBL.DMLEducationContributers(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void EducationContributersBL_DMLEducationContributers_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new EducationContributersViewModel();
            model.IsRequestRoot = true;
            model.EducationCode = guid;
            model.RowNumber = 1;
            model.TCIdentity = guid;
            model.FullName = guid;
            model.WorkingCompany = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.Grade = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _EducationContributersBL.DMLEducationContributers(UserManager.UserInfo, model);

            var filter = new EducationContributersListModel();
            filter.EducationCode = guid;

            int count = 0;
            var resultGet = _EducationContributersBL.GetEducationContList(UserManager.UserInfo, filter, out count);

            var modelDelete = new EducationContributersViewModel();
            modelDelete.EducationCode = guid;
            modelDelete.TCIdentity = guid;
            modelDelete.FullName = guid;
            modelDelete.WorkingCompany = guid;
            modelDelete.DealerId = UserManager.UserInfo.DealerID;
            modelDelete.Grade = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _EducationContributersBL.DMLEducationContributers(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}

