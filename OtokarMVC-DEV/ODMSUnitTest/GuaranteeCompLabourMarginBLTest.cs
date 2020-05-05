using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.GuaranteeCompLabourMargin;


namespace ODMSUnitTest
{

    [TestClass]
    public class GuaranteeCompLabourMarginBLTest
    {

        GuaranteeCompLabourMarginBL _GuaranteeCompLabourMarginBL = new GuaranteeCompLabourMarginBL();

        [TestMethod]
        public void GuaranteeCompLabourMarginBL_DMLGuaranteeCompLabourMargin_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GuaranteeCompLabourMarginViewModel();
            model.IdGrntLabourMrgn = 1;
            model.CountryId = 1;
            model.CreateUser = guid;
            model.CreateDate = DateTime.Now;
            model.UpdateUser = guid;
            model.CountryName = guid;
            model.CurrencyName = guid;
            model.CurrencyCode = guid;
            model.ShortCode = guid;
            model.UpdateUser = "1";
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _GuaranteeCompLabourMarginBL.DMLGuaranteeCompLabourMargin(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void GuaranteeCompLabourMarginBL_GetGuaranteeCompLabourMargin_GetModel()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GuaranteeCompLabourMarginViewModel();
            model.IdGrntLabourMrgn = 1;
            model.CountryId = 1;
            model.CreateUser = guid;
            model.CreateDate = DateTime.Now;
            model.UpdateUser = guid;
            model.CountryName = guid;
            model.CurrencyName = guid;
            model.CurrencyCode = guid;
            model.ShortCode = guid;
            model.UpdateUser = "1";
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _GuaranteeCompLabourMarginBL.DMLGuaranteeCompLabourMargin(UserManager.UserInfo, model);

            var resultGet = _GuaranteeCompLabourMarginBL.GetGuaranteeCompLabourMargin(UserManager.UserInfo, result.Model.IdGrntLabourMrgn);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.GrntRatio > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void GuaranteeCompLabourMarginBL_ListGuaranteeCompLabourMargin_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GuaranteeCompLabourMarginViewModel();
            model.IdGrntLabourMrgn = 1;
            model.CountryId = 1;
            model.CreateUser = guid;
            model.CreateDate = DateTime.Now;
            model.UpdateUser = guid;
            model.CountryName = guid;
            model.CurrencyName = guid;
            model.CurrencyCode = guid;
            model.ShortCode = guid;
            model.UpdateUser = "1";
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _GuaranteeCompLabourMarginBL.DMLGuaranteeCompLabourMargin(UserManager.UserInfo, model);

            int count = 0;
            var filter = new GuaranteeCompLabourMarginListModel();
            filter.CurrencyCode = guid;
            filter.ShortCode = guid;
            filter.GrntPriceAndCurrencyCode = guid;

            var resultGet = _GuaranteeCompLabourMarginBL.ListGuaranteeCompLabourMargin(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void GuaranteeCompLabourMarginBL_DMLGuaranteeCompLabourMargin_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GuaranteeCompLabourMarginViewModel();
            model.IdGrntLabourMrgn = 1;
            model.CountryId = 1;
            model.CreateUser = guid;
            model.CreateDate = DateTime.Now;
            model.UpdateUser = guid;
            model.CountryName = guid;
            model.CurrencyName = guid;
            model.CurrencyCode = guid;
            model.ShortCode = guid;
            model.UpdateUser = "1";
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _GuaranteeCompLabourMarginBL.DMLGuaranteeCompLabourMargin(UserManager.UserInfo, model);

            var filter = new GuaranteeCompLabourMarginListModel();
            filter.CurrencyCode = guid;
            filter.ShortCode = guid;
            filter.GrntPriceAndCurrencyCode = guid;

            int count = 0;
            var resultGet = _GuaranteeCompLabourMarginBL.ListGuaranteeCompLabourMargin(UserManager.UserInfo, filter, out count);

            var modelUpdate = new GuaranteeCompLabourMarginViewModel();
            modelUpdate.IdGrntLabourMrgn = resultGet.Data.First().IdGrntLabourMrgn;
            modelUpdate.CountryId = 1;
            modelUpdate.CreateUser = guid;
            modelUpdate.UpdateUser = guid;
            modelUpdate.CountryName = guid;
            modelUpdate.CurrencyName = guid;
            modelUpdate.CurrencyCode = guid;
            modelUpdate.ShortCode = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _GuaranteeCompLabourMarginBL.DMLGuaranteeCompLabourMargin(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void GuaranteeCompLabourMarginBL_DMLGuaranteeCompLabourMargin_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GuaranteeCompLabourMarginViewModel();
            model.IdGrntLabourMrgn = 1;
            model.CountryId = 1;
            model.CreateUser = guid;
            model.CreateDate = DateTime.Now;
            model.UpdateUser = guid;
            model.CountryName = guid;
            model.CurrencyName = guid;
            model.CurrencyCode = guid;
            model.ShortCode = guid;
            model.UpdateUser = "1";
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _GuaranteeCompLabourMarginBL.DMLGuaranteeCompLabourMargin(UserManager.UserInfo, model);

            var filter = new GuaranteeCompLabourMarginListModel();
            filter.CountryId = 1;
            filter.CurrencyCode = guid;
            filter.ShortCode = guid;
            filter.GrntPriceAndCurrencyCode = guid;

            int count = 0;
            var resultGet = _GuaranteeCompLabourMarginBL.ListGuaranteeCompLabourMargin(UserManager.UserInfo, filter, out count);

            var modelDelete = new GuaranteeCompLabourMarginViewModel();
            modelDelete.IdGrntLabourMrgn = resultGet.Data.First().IdGrntLabourMrgn;

            modelDelete.CountryId = 1;
            modelDelete.CreateUser = guid;
            modelDelete.UpdateUser = guid;
            modelDelete.CountryName = guid;
            modelDelete.CurrencyName = guid;
            modelDelete.CurrencyCode = guid;
            modelDelete.ShortCode = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _GuaranteeCompLabourMarginBL.DMLGuaranteeCompLabourMargin(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}

