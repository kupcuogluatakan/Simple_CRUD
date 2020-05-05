using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.BodyworkDetail;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class BodyworkDetailBLTest
    {

        BodyworkDetailBL _BodyworkDetailBL = new BodyworkDetailBL();

        [TestMethod]
        public void BodyworkDetailBL_DMLBodyworkDetail_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new BodyworkDetailViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.BodyworkCode = guid;
            model.Descripion = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _BodyworkDetailBL.DMLBodyworkDetail(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void BodyworkDetailBL_GetBodyworkDetail_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new BodyworkDetailViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.BodyworkCode = guid;
            model.Descripion = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _BodyworkDetailBL.DMLBodyworkDetail(UserManager.UserInfo, model);

            var filter = new BodyworkDetailViewModel();
            filter.MultiLanguageContentAsText = "TR || TEST";
            filter.BodyworkCode = guid;

            var resultGet = _BodyworkDetailBL.GetBodyworkDetail(filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Descripion != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void BodyworkDetailBL_GetBodyworkDetailList_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new BodyworkDetailViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.BodyworkCode = guid;
            model.Descripion = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _BodyworkDetailBL.DMLBodyworkDetail(UserManager.UserInfo, model);

            int count = 0;
            var filter = new BodyworkDetailListModel();
            filter.BodyworkCode = guid;
            filter.BodyworkCodeName = guid;

            var resultGet = _BodyworkDetailBL.GetBodyworkDetailList(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void BodyworkDetailBL_DMLBodyworkDetail_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new BodyworkDetailViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.BodyworkCode = guid;
            model.Descripion = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _BodyworkDetailBL.DMLBodyworkDetail(UserManager.UserInfo, model);

            var filter = new BodyworkDetailListModel();
            filter.BodyworkCode = guid;
            filter.BodyworkCodeName = guid;

            int count = 0;
            var resultGet = _BodyworkDetailBL.GetBodyworkDetailList(UserManager.UserInfo, filter, out count);

            var modelUpdate = new BodyworkDetailViewModel();
            modelUpdate.MultiLanguageContentAsText = "TR || TEST";
            modelUpdate.BodyworkCode = guid;
            modelUpdate.Descripion = guid;



            modelUpdate.CommandType = "U";
            var resultUpdate = _BodyworkDetailBL.DMLBodyworkDetail(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void BodyworkDetailBL_DMLBodyworkDetail_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new BodyworkDetailViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.BodyworkCode = guid;
            model.Descripion = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _BodyworkDetailBL.DMLBodyworkDetail(UserManager.UserInfo, model);

            var filter = new BodyworkDetailListModel();
            filter.BodyworkCode = guid;
            filter.BodyworkCodeName = guid;

            int count = 0;
            var resultGet = _BodyworkDetailBL.GetBodyworkDetailList(UserManager.UserInfo, filter, out count);

            var modelDelete = new BodyworkDetailViewModel();
            modelDelete.MultiLanguageContentAsText = "TR || TEST";
            modelDelete.BodyworkCode = guid;
            modelDelete.Descripion = guid;



            modelDelete.CommandType = "D";
            var resultDelete = _BodyworkDetailBL.DMLBodyworkDetail(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}

