using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.Equipment;


namespace ODMSUnitTest
{

    [TestClass]
    public class EquipmentTypeBLTest
    {

        EquipmentTypeBL _EquipmentTypeBL = new EquipmentTypeBL();

        [TestMethod]
        public void EquipmentTypeBL_Insert_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new EquipmentViewModel();
            model.EquipmentId = 1;
            model.EquipmentTypeName = guid;
            model.EquipmentTypeDesc = guid;
            model.EquipmentTypeLangCode = guid;
            model.MultiLanguageContentAsText = "TR || TEST";
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _EquipmentTypeBL.Insert(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void EquipmentTypeBL_Get_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new EquipmentViewModel();
            model.EquipmentId = 1;
            model.EquipmentTypeName = guid;
            model.EquipmentTypeDesc = guid;
            model.EquipmentTypeLangCode = guid;
            model.MultiLanguageContentAsText = "TR || TEST";
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _EquipmentTypeBL.Insert(UserManager.UserInfo, model);

            var filter = new EquipmentViewModel();
            filter.EquipmentId = result.Model.EquipmentId;
            filter.EquipmentTypeLangCode = guid;
            filter.MultiLanguageContentAsText = "TR || TEST";

            var resultGet = _EquipmentTypeBL.Get(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.EquipmentTypeName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void EquipmentTypeBL_List_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new EquipmentViewModel();
            model.EquipmentId = 1;
            model.EquipmentTypeName = guid;
            model.EquipmentTypeDesc = guid;
            model.EquipmentTypeLangCode = guid;
            model.MultiLanguageContentAsText = "TR || TEST";
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _EquipmentTypeBL.Update(UserManager.UserInfo, model);

            int count = 0;
            var filter = new EquipmentTypeListModel();

            var resultGet = _EquipmentTypeBL.List(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void EquipmentTypeBL_Update_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new EquipmentViewModel();
            model.EquipmentId = 1;
            model.EquipmentTypeName = guid;
            model.EquipmentTypeDesc = guid;
            model.EquipmentTypeLangCode = guid;
            model.MultiLanguageContentAsText = "TR || TEST";
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _EquipmentTypeBL.Update(UserManager.UserInfo, model);

            var filter = new EquipmentTypeListModel();

            int count = 0;
            var resultGet = _EquipmentTypeBL.List(UserManager.UserInfo, filter, out count);

            var modelUpdate = new EquipmentViewModel();
            modelUpdate.EquipmentId = resultGet.Data.First().EquipmentId;
            modelUpdate.EquipmentTypeName = guid;
            modelUpdate.EquipmentTypeDesc = guid;
            modelUpdate.EquipmentTypeLangCode = guid;
            modelUpdate.MultiLanguageContentAsText = "TR || TEST";
            modelUpdate.CommandType = "U";
            var resultUpdate = _EquipmentTypeBL.Update(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void EquipmentTypeBL_Update_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new EquipmentViewModel();
            model.EquipmentId = 1;
            model.EquipmentTypeName = guid;
            model.EquipmentTypeDesc = guid;
            model.EquipmentTypeLangCode = guid;
            model.MultiLanguageContentAsText = "TR || TEST";
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _EquipmentTypeBL.Update(UserManager.UserInfo, model);

            var filter = new EquipmentTypeListModel();

            int count = 0;
            var resultGet = _EquipmentTypeBL.List(UserManager.UserInfo, filter, out count);

            var modelDelete = new EquipmentViewModel();
            modelDelete.EquipmentId = resultGet.Data.First().EquipmentId;
            modelDelete.EquipmentTypeName = guid;
            modelDelete.EquipmentTypeDesc = guid;
            modelDelete.EquipmentTypeLangCode = guid;
            modelDelete.MultiLanguageContentAsText = "TR || TEST";
            modelDelete.CommandType = "D";
            var resultDelete = _EquipmentTypeBL.Update(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }

        [TestMethod]
        public void EquipmentTypeBL_List_GetAll_1()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new EquipmentViewModel();
            model.EquipmentId = 1;
            model.EquipmentTypeName = guid;
            model.EquipmentTypeDesc = guid;
            model.EquipmentTypeLangCode = guid;
            model.MultiLanguageContentAsText = "TR || TEST";
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _EquipmentTypeBL.Update(UserManager.UserInfo, model);

            int count = 0;
            var filter = new EquipmentViewModel();
            filter.EquipmentTypeLangCode = guid;
            filter.MultiLanguageContentAsText = "TR || TEST";

            var resultGet = _EquipmentTypeBL.List(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Total > 0);
        }

    }

}

