using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.VatRatio;


namespace ODMSUnitTest
{

    [TestClass]
    public class VatRatioBLTest
    {

        VatRatioBL _VatRatioBL = new VatRatioBL();

        [TestMethod]
        public void VatRatioBL_DMLVatRatio_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VatRatioModel();
            model.InvoiceLabel = guid;
            model.VatRatio = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _VatRatioBL.DMLVatRatio(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void VatRatioBL_DMLVatRatioExp_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VatRatioExpModel();
            model.VatRatio = 1;
            model.CountryId = 1;
            model.Country = guid;
            model.Explation = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _VatRatioBL.DMLVatRatioExp(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void VatRatioBL_GetVatRatioExp_GetModel()
        {
            var resultGet = _VatRatioBL.GetVatRatioExp(UserManager.UserInfo, 18, 1);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.VatRatio > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void VatRatioBL_ListVatRatios_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VatRatioExpModel();
            model.VatRatio = 1;
            model.CountryId = 1;
            model.Country = guid;
            model.Explation = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _VatRatioBL.DMLVatRatioExp(UserManager.UserInfo, model);

            int count = 0;
            var filter = new VatRatioListModel();

            var resultGet = _VatRatioBL.ListVatRatios(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void VatRatioBL_DMLVatRatioExp_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VatRatioExpModel();
            model.VatRatio = 1;
            model.CountryId = 1;
            model.Country = guid;
            model.Explation = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _VatRatioBL.DMLVatRatioExp(UserManager.UserInfo, model);

            var filter = new VatRatioListModel();

            int count = 0;
            var resultGet = _VatRatioBL.ListVatRatios(UserManager.UserInfo, filter, out count);

            var modelUpdate = new VatRatioExpModel();
            modelUpdate.CountryId = 1;
            modelUpdate.Country = guid;
            modelUpdate.Explation = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _VatRatioBL.DMLVatRatioExp(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void VatRatioBL_DMLVatRatioExp_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VatRatioExpModel();
            model.VatRatio = 1;
            model.CountryId = 1;
            model.Country = guid;
            model.Explation = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _VatRatioBL.DMLVatRatioExp(UserManager.UserInfo, model);

            var filter = new VatRatioListModel();

            int count = 0;
            var resultGet = _VatRatioBL.ListVatRatios(UserManager.UserInfo, filter, out count);

            var modelDelete = new VatRatioExpModel();
            modelDelete.CountryId = 1;
            modelDelete.Country = guid;
            modelDelete.Explation = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _VatRatioBL.DMLVatRatioExp(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }

        [TestMethod]
        public void VatRatioBL_ListVatRatioExps_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VatRatioExpModel();
            model.VatRatio = 1;
            model.CountryId = 1;
            model.Country = guid;
            model.Explation = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _VatRatioBL.DMLVatRatioExp(UserManager.UserInfo, model);

            int count = 0;
            var filter = new VatRatioExpListModel();

            var resultGet = _VatRatioBL.ListVatRatioExps(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void VatRatioBL_ListVatRatioAsSelectList_GetAll()
        {
            var resultGet = VatRatioBL.ListVatRatioAsSelectList(1, 1);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

