using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.DealerClass;


namespace ODMSUnitTest
{

    [TestClass]
    public class DealerClassBLTest
    {

        DealerClassBL _DealerClassBL = new DealerClassBL();

        [TestMethod]
        public void DealerClassBL_DMLDealerClass_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerClassViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.DealerClassCode = guid;
            model.SSIdDealerClass = guid;
            model.DealerClassName = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _DealerClassBL.DMLDealerClass(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void DealerClassBL_GetDealerClass_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerClassViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.DealerClassCode = guid;
            model.SSIdDealerClass = guid;
            model.DealerClassName = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _DealerClassBL.DMLDealerClass(UserManager.UserInfo, model);

            var filter = new DealerClassViewModel();
            filter.MultiLanguageContentAsText = "TR || TEST";
            filter.DealerClassCode = guid;

            var resultGet = _DealerClassBL.GetDealerClass(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.DealerClassName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void DealerClassBL_ListDealerClass_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerClassViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.DealerClassCode = guid;
            model.SSIdDealerClass = guid;
            model.DealerClassName = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _DealerClassBL.DMLDealerClass(UserManager.UserInfo, model);

            int count = 0;
            var filter = new DealerClassListModel();
            filter.DealerClassCode = guid;

            var resultGet = _DealerClassBL.ListDealerClass(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void DealerClassBL_DMLDealerClass_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerClassViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.DealerClassCode = guid;
            model.SSIdDealerClass = guid;
            model.DealerClassName = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _DealerClassBL.DMLDealerClass(UserManager.UserInfo, model);

            var filter = new DealerClassListModel();
            filter.DealerClassCode = guid;

            int count = 0;
            var resultGet = _DealerClassBL.ListDealerClass(UserManager.UserInfo, filter, out count);

            var modelUpdate = new DealerClassViewModel();
            modelUpdate.MultiLanguageContentAsText = "TR || TEST";
            modelUpdate.DealerClassCode = guid;
            modelUpdate.SSIdDealerClass = guid;
            modelUpdate.DealerClassName = guid;

            modelUpdate.IsActiveName = guid;


            modelUpdate.CommandType = "U";
            var resultUpdate = _DealerClassBL.DMLDealerClass(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void DealerClassBL_DMLDealerClass_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerClassViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.DealerClassCode = guid;
            model.SSIdDealerClass = guid;
            model.DealerClassName = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _DealerClassBL.DMLDealerClass(UserManager.UserInfo, model);

            var filter = new DealerClassListModel();
            filter.DealerClassCode = guid;

            int count = 0;
            var resultGet = _DealerClassBL.ListDealerClass(UserManager.UserInfo, filter, out count);

            var modelDelete = new DealerClassViewModel();
            modelDelete.MultiLanguageContentAsText = "TR || TEST";
            modelDelete.DealerClassCode = guid;
            modelDelete.SSIdDealerClass = guid;
            modelDelete.DealerClassName = guid;

            modelDelete.IsActiveName = guid;


            modelDelete.CommandType = "D";
            var resultDelete = _DealerClassBL.DMLDealerClass(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}

