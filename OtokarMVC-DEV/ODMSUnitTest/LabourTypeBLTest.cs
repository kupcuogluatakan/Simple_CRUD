using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.LabourType;

namespace ODMSUnitTest
{

    [TestClass]
    public class LabourTypeBLTest
    {

        LabourTypeBL _LabourTypeBL = new LabourTypeBL();

        [TestMethod]
        public void LabourTypeBL_DMLLabourType_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourTypeDetailModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.Name = guid;
            model.VatRatio = 1;
            model.VatRatioString = guid;
            model.Description = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _LabourTypeBL.DMLLabourType(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void LabourTypeBL_GetLabourType_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourTypeDetailModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.Name = guid;
            model.VatRatio = 1;
            model.VatRatioString = guid;
            model.Description = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _LabourTypeBL.DMLLabourType(UserManager.UserInfo, model);

            var filter = new LabourTypeDetailModel();
            filter.Id = result.Model.Id;
            filter.MultiLanguageContentAsText = "TR || TEST";

            var resultGet = _LabourTypeBL.GetLabourType(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Id > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void LabourTypeBL_ListLabourTypes_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourTypeDetailModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.Name = guid;
            model.VatRatio = 1;
            model.VatRatioString = guid;
            model.Description = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _LabourTypeBL.DMLLabourType(UserManager.UserInfo, model);

            int count = 0;
            var filter = new LabourTypeListModel();

            var resultGet = _LabourTypeBL.ListLabourTypes(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void LabourTypeBL_DMLLabourType_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourTypeDetailModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.Name = guid;
            model.VatRatio = 1;
            model.VatRatioString = guid;
            model.Description = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _LabourTypeBL.DMLLabourType(UserManager.UserInfo, model);

            var filter = new LabourTypeListModel();

            int count = 0;
            var resultGet = _LabourTypeBL.ListLabourTypes(UserManager.UserInfo, filter, out count);

            var modelUpdate = new LabourTypeDetailModel();
            modelUpdate.Id = resultGet.Data.First().Id;
            modelUpdate.MultiLanguageContentAsText = "TR || TEST";
            modelUpdate.Name = guid;

            modelUpdate.VatRatioString = guid;
            modelUpdate.Description = guid;



            modelUpdate.CommandType = "U";
            var resultUpdate = _LabourTypeBL.DMLLabourType(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void LabourTypeBL_DMLLabourType_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourTypeDetailModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.Name = guid;
            model.VatRatio = 1;
            model.VatRatioString = guid;
            model.Description = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _LabourTypeBL.DMLLabourType(UserManager.UserInfo, model);

            var filter = new LabourTypeListModel();

            int count = 0;
            var resultGet = _LabourTypeBL.ListLabourTypes(UserManager.UserInfo, filter, out count);

            var modelDelete = new LabourTypeDetailModel();
            modelDelete.Id = resultGet.Data.First().Id;
            modelDelete.MultiLanguageContentAsText = "TR || TEST";
            modelDelete.Name = guid;
            modelDelete.VatRatioString = guid; 
            modelDelete.Description = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _LabourTypeBL.DMLLabourType(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}

