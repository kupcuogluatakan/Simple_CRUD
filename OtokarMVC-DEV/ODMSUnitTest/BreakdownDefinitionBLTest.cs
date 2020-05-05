using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.BreakdownDefinition;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class BreakdownDefinitionBLTest
    {

        BreakdownDefinitionBL _BreakdownDefinitionBL = new BreakdownDefinitionBL();

        [TestMethod]
        public void BreakdownDefinitionBL_DMLBreakdownDefinition_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new BreakdownDefinitionViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.PdiBreakdownCode = guid;
            model.AdminDesc = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.BreakdownDesc = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _BreakdownDefinitionBL.DMLBreakdownDefinition(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void BreakdownDefinitionBL_GetBreakdownDefinition_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new BreakdownDefinitionViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.PdiBreakdownCode = guid;
            model.AdminDesc = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.BreakdownDesc = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _BreakdownDefinitionBL.DMLBreakdownDefinition(UserManager.UserInfo, model);

            var filter = new BreakdownDefinitionViewModel();
            filter.MultiLanguageContentAsText = "TR || TEST";
            filter.PdiBreakdownCode = guid;

            var resultGet = _BreakdownDefinitionBL.GetBreakdownDefinition(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.AdminDesc != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void BreakdownDefinitionBL_ListBreakdownDefinition_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new BreakdownDefinitionViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.PdiBreakdownCode = guid;
            model.AdminDesc = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.BreakdownDesc = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _BreakdownDefinitionBL.DMLBreakdownDefinition(UserManager.UserInfo, model);

            int count = 0;
            var filter = new BreakdownDefinitionListModel();
            filter.PdiBreakdownCode = guid;

            var resultGet = _BreakdownDefinitionBL.ListBreakdownDefinition(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void BreakdownDefinitionBL_DMLBreakdownDefinition_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new BreakdownDefinitionViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.PdiBreakdownCode = guid;
            model.AdminDesc = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.BreakdownDesc = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _BreakdownDefinitionBL.DMLBreakdownDefinition(UserManager.UserInfo, model);

            var filter = new BreakdownDefinitionListModel();
            filter.PdiBreakdownCode = guid;

            int count = 0;
            var resultGet = _BreakdownDefinitionBL.ListBreakdownDefinition(UserManager.UserInfo, filter, out count);

            var modelUpdate = new BreakdownDefinitionViewModel();
            modelUpdate.MultiLanguageContentAsText = "TR || TEST";
            modelUpdate.PdiBreakdownCode = guid;
            modelUpdate.AdminDesc = guid;

            modelUpdate.IsActiveName = guid;
            modelUpdate.BreakdownDesc = guid;


            modelUpdate.CommandType = "U";
            var resultUpdate = _BreakdownDefinitionBL.DMLBreakdownDefinition(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void BreakdownDefinitionBL_DMLBreakdownDefinition_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new BreakdownDefinitionViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.PdiBreakdownCode = guid;
            model.AdminDesc = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.BreakdownDesc = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _BreakdownDefinitionBL.DMLBreakdownDefinition(UserManager.UserInfo, model);

            var filter = new BreakdownDefinitionListModel();
            filter.PdiBreakdownCode = guid;

            int count = 0;
            var resultGet = _BreakdownDefinitionBL.ListBreakdownDefinition(UserManager.UserInfo, filter, out count);

            var modelDelete = new BreakdownDefinitionViewModel();
            modelDelete.MultiLanguageContentAsText = "TR || TEST";
            modelDelete.PdiBreakdownCode = guid;
            modelDelete.AdminDesc = guid;

            modelDelete.IsActiveName = guid;
            modelDelete.BreakdownDesc = guid;


            modelDelete.CommandType = "D";
            var resultDelete = _BreakdownDefinitionBL.DMLBreakdownDefinition(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}

