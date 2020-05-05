using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.EducationRequest;


namespace ODMSUnitTest
{

    [TestClass]
    public class EducationRequestBLTest
    {

        EducationRequestBL _EducationRequestBL = new EducationRequestBL();

        [TestMethod]
        public void EducationRequestBL_SaveEducationRequest_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new EducationRequestDetailModel();
            model.EducationCode = guid;
            model.EducationName = guid;
            model.CreateDate = DateTime.Now;
            model.WorkerId = 1;
            model.WorkerName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _EducationRequestBL.SaveEducationRequest(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void EducationRequestBL_GetEducationRequestIndexModel_GetModel()
        {
            var resultGet = _EducationRequestBL.GetEducationRequestIndexModel(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.EducationList.Count > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void EducationRequestBL_GetEducationRequest_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new EducationRequestDetailModel();
            model.EducationCode = guid;
            model.EducationName = guid;
            model.CreateDate = DateTime.Now;
            model.WorkerId = 1;
            model.WorkerName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _EducationRequestBL.SaveEducationRequest(UserManager.UserInfo, model);

            var filter = new EducationRequestDetailModel();
            filter.Id = result.Model.Id;
            filter.EducationCode = guid;

            var resultGet = _EducationRequestBL.GetEducationRequest(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Id > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void EducationRequestBL_ListEducationRequests_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new EducationRequestDetailModel();
            model.EducationCode = guid;
            model.EducationName = guid;
            model.CreateDate = DateTime.Now;
            model.WorkerId = 1;
            model.WorkerName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _EducationRequestBL.SaveEducationRequest(UserManager.UserInfo, model);

            int count = 0;
            var filter = new EducationRequestListModel();
            filter.EducationCode = guid;

            var resultGet = _EducationRequestBL.ListEducationRequests(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void EducationRequestBL_SaveEducationRequest_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new EducationRequestDetailModel();
            model.EducationCode = guid;
            model.EducationName = guid;
            model.CreateDate = DateTime.Now;
            model.WorkerId = 1;
            model.WorkerName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _EducationRequestBL.SaveEducationRequest(UserManager.UserInfo, model);

            var filter = new EducationRequestListModel();
            filter.EducationCode = guid;

            int count = 0;
            var resultGet = _EducationRequestBL.ListEducationRequests(UserManager.UserInfo, filter, out count);

            var modelUpdate = new EducationRequestDetailModel();
            modelUpdate.Id = resultGet.Data.First().Id;
            modelUpdate.EducationCode = guid;
            modelUpdate.EducationName = guid;
            modelUpdate.WorkerName = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _EducationRequestBL.SaveEducationRequest(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void EducationRequestBL_SaveEducationRequest_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new EducationRequestDetailModel();
            model.EducationCode = guid;
            model.EducationName = guid;
            model.CreateDate = DateTime.Now;
            model.WorkerId = 1;
            model.WorkerName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _EducationRequestBL.SaveEducationRequest(UserManager.UserInfo, model);

            var filter = new EducationRequestListModel();
            filter.EducationCode = guid;

            int count = 0;
            var resultGet = _EducationRequestBL.ListEducationRequests(UserManager.UserInfo, filter, out count);

            var modelDelete = new EducationRequestDetailModel();
            modelDelete.Id = resultGet.Data.First().Id;
            modelDelete.EducationCode = guid;
            modelDelete.EducationName = guid;


            modelDelete.WorkerName = guid;



            modelDelete.CommandType = "D";
            var resultDelete = _EducationRequestBL.SaveEducationRequest(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }

        [TestMethod]
        public void EducationRequestBL_GetEducationList_GetAll()
        {
            var resultGet = _EducationRequestBL.GetEducationList(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void EducationRequestBL_GetWorkerList_GetAll()
        {
            var resultGet = _EducationRequestBL.GetWorkerList(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }

    }

}

