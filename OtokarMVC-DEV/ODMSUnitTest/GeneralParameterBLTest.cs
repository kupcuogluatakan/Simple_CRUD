using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.GeneralParameter;


namespace ODMSUnitTest
{

    [TestClass]
    public class GeneralParameterBLTest
    {

        GeneralParameterBL _GeneralParameterBL = new GeneralParameterBL();

        [TestMethod]
        public void GeneralParameterBL_DMLGeneralParameter_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GeneralParameterViewModel();
            model.ParameterId = guid;
            model.Description = guid;
            model.Type = guid;
            model.Value = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _GeneralParameterBL.DMLGeneralParameter(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void GeneralParameterBL_GetGeneralParameter_GetModel()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GeneralParameterViewModel();
            model.ParameterId = guid;
            model.Description = guid;
            model.Type = guid;
            model.Value = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _GeneralParameterBL.DMLGeneralParameter(UserManager.UserInfo, model);


            var resultGet = _GeneralParameterBL.GetGeneralParameter(result.Model.ParameterId);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Description != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void GeneralParameterBL_ListGeneralParameters_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GeneralParameterViewModel();
            model.ParameterId = guid;
            model.Description = guid;
            model.Type = guid;
            model.Value = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _GeneralParameterBL.DMLGeneralParameter(UserManager.UserInfo, model);

            int count = 0;
            var filter = new GeneralParameterListModel();

            var resultGet = _GeneralParameterBL.ListGeneralParameters(filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void GeneralParameterBL_DMLGeneralParameter_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GeneralParameterViewModel();
            model.ParameterId = guid;
            model.Description = guid;
            model.Type = guid;
            model.Value = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _GeneralParameterBL.DMLGeneralParameter(UserManager.UserInfo, model);

            var filter = new GeneralParameterListModel();

            int count = 0;
            var resultGet = _GeneralParameterBL.ListGeneralParameters(filter, out count);

            var modelUpdate = new GeneralParameterViewModel();
            modelUpdate.ParameterId = guid;
            modelUpdate.Description = guid;
            modelUpdate.Type = guid;
            modelUpdate.Value = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _GeneralParameterBL.DMLGeneralParameter(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void GeneralParameterBL_DMLGeneralParameter_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GeneralParameterViewModel();
            model.ParameterId = guid;
            model.Description = guid;
            model.Type = guid;
            model.Value = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _GeneralParameterBL.DMLGeneralParameter(UserManager.UserInfo, model);

            var filter = new GeneralParameterListModel();

            int count = 0;
            var resultGet = _GeneralParameterBL.ListGeneralParameters(filter, out count);

            var modelDelete = new GeneralParameterViewModel();
            modelDelete.ParameterId = guid;
            modelDelete.Description = guid;
            modelDelete.Type = guid;
            modelDelete.Value = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _GeneralParameterBL.DMLGeneralParameter(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}

