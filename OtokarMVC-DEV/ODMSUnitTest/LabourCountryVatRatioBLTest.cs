using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.LabourCountryVatRatio;


namespace ODMSUnitTest
{

    [TestClass]
    public class LabourCountryVatRatioBLTest
    {

        LabourCountryVatRatioBL _LabourCountryVatRatioBL = new LabourCountryVatRatioBL();

        [TestMethod]
        public void LabourCountryVatRatioBL_DMLLabourCountryVatRatio_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourCountryVatRatioViewModel();
            model.LabourId = 211;
            model.LabourName = guid;
            model.CountryName = guid;
            model.CountryId = 1;
            model.IsActiveString = guid;
            model.subCategoryId = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _LabourCountryVatRatioBL.DMLLabourCountryVatRatio(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void LabourCountryVatRatioBL_GetLabourCountryVatRatio_GetModel()
        {
            var resultGet = _LabourCountryVatRatioBL.GetLabourCountryVatRatio(UserManager.UserInfo, 211, 1);
            Assert.IsTrue(resultGet.Model != null && resultGet.Model.LabourName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void LabourCountryVatRatioBL_ListLabourCountryVatRatios_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourCountryVatRatioViewModel();
            model.LabourId = 211;
            model.LabourName = guid;
            model.CountryName = guid;
            model.CountryId = 1;
            model.IsActiveString = guid;
            model.subCategoryId = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _LabourCountryVatRatioBL.DMLLabourCountryVatRatio(UserManager.UserInfo, model);

            int count = 0;
            var filter = new LabourCountryVatRatioListModel();

            filter.LabourId = 211;

            var resultGet = _LabourCountryVatRatioBL.ListLabourCountryVatRatios(UserManager.UserInfo, filter, out count);
            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void LabourCountryVatRatioBL_DMLLabourCountryVatRatio_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourCountryVatRatioViewModel();
            model.LabourId = 211;
            model.LabourName = guid;
            model.CountryName = guid;
            model.CountryId = 1;
            model.IsActiveString = guid;
            model.subCategoryId = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _LabourCountryVatRatioBL.DMLLabourCountryVatRatio(UserManager.UserInfo, model);

            var filter = new LabourCountryVatRatioListModel();
            filter.LabourId = 211;

            int count = 0;
            var resultGet = _LabourCountryVatRatioBL.ListLabourCountryVatRatios(UserManager.UserInfo, filter, out count);

            var modelUpdate = new LabourCountryVatRatioViewModel();
            modelUpdate.LabourId = 211;
            modelUpdate.LabourName = guid;
            modelUpdate.CountryName = guid;
            modelUpdate.CountryId = 1;
            modelUpdate.IsActiveString = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _LabourCountryVatRatioBL.DMLLabourCountryVatRatio(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void LabourCountryVatRatioBL_DMLLabourCountryVatRatio_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourCountryVatRatioViewModel();
            model.LabourId = 211;
            model.LabourName = guid;
            model.CountryName = guid;
            model.CountryId = 1;
            model.IsActiveString = guid;
            model.subCategoryId = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _LabourCountryVatRatioBL.DMLLabourCountryVatRatio(UserManager.UserInfo, model);

            var filter = new LabourCountryVatRatioListModel();
            filter.LabourId = 211;
            filter.CountryId = 1;

            int count = 0;
            var resultGet = _LabourCountryVatRatioBL.ListLabourCountryVatRatios(UserManager.UserInfo, filter, out count);

            var modelDelete = new LabourCountryVatRatioViewModel();
            modelDelete.LabourId = 211;
            modelDelete.LabourName = guid;
            modelDelete.CountryName = guid;
            modelDelete.CountryId = 1;
            modelDelete.IsActiveString = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _LabourCountryVatRatioBL.DMLLabourCountryVatRatio(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }

        [TestMethod]
        public void LabourCountryVatRatioBL_ListLaboursBySubGroup_GetAll()
        {
            var resultGet = _LabourCountryVatRatioBL.ListLaboursBySubGroup(UserManager.UserInfo, 1);

            Assert.IsTrue(resultGet.Total > 0);
        }
        

    }

}

