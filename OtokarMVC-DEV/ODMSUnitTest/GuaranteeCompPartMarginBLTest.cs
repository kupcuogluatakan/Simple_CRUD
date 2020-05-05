using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.GuaranteeCompPartMargin;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class GuaranteeCompPartMarginBLTest
    {

        GuaranteeCompPartMarginBL _GuaranteeCompPartMarginBL = new GuaranteeCompPartMarginBL();

        [TestMethod]
        public void GuaranteeCompPartMarginBL_DMLGuaranteeCompPartMargin_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GuaranteeCompPartMarginViewModel();
            model.IdGrntPartMrgn = 1;
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
            var result = _GuaranteeCompPartMarginBL.DMLGuaranteeCompPartMargin(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void GuaranteeCompPartMarginBL_GetGuaranteeCompPartMargin_GetModel()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GuaranteeCompPartMarginViewModel();
            model.IdGrntPartMrgn = 1;
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
            var result = _GuaranteeCompPartMarginBL.DMLGuaranteeCompPartMargin(UserManager.UserInfo, model);

            var resultGet = _GuaranteeCompPartMarginBL.GetGuaranteeCompPartMargin(UserManager.UserInfo, result.Model.IdGrntPartMrgn);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.CountryName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void GuaranteeCompPartMarginBL_ListGuaranteeCompPartMargin_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GuaranteeCompPartMarginViewModel();
            model.IdGrntPartMrgn = 1;
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
            var result = _GuaranteeCompPartMarginBL.DMLGuaranteeCompPartMargin(UserManager.UserInfo, model);

            int count = 0;
            var filter = new GuaranteeCompPartMarginListModel();
            filter.CurrencyCode = guid;
            filter.ShortCode = guid;
            filter.GrntPriceAndCurrencyCode = guid;

            var resultGet = _GuaranteeCompPartMarginBL.ListGuaranteeCompPartMargin(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void GuaranteeCompPartMarginBL_DMLGuaranteeCompPartMargin_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GuaranteeCompPartMarginViewModel();
            model.IdGrntPartMrgn = 1;
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
            var result = _GuaranteeCompPartMarginBL.DMLGuaranteeCompPartMargin(UserManager.UserInfo, model);

            var filter = new GuaranteeCompPartMarginListModel();
            filter.CurrencyCode = guid;
            filter.ShortCode = guid;
            filter.GrntPriceAndCurrencyCode = guid;

            int count = 0;
            var resultGet = _GuaranteeCompPartMarginBL.ListGuaranteeCompPartMargin(UserManager.UserInfo, filter, out count);

            var modelUpdate = new GuaranteeCompPartMarginViewModel();
            modelUpdate.IdGrntPartMrgn = resultGet.Data.First().IdGrntPartMrgn;
            modelUpdate.CountryId = 1;
            modelUpdate.CreateUser = guid;
            modelUpdate.UpdateUser = guid;
            modelUpdate.CountryName = guid;
            modelUpdate.CurrencyName = guid;
            modelUpdate.CurrencyCode = guid;
            modelUpdate.ShortCode = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _GuaranteeCompPartMarginBL.DMLGuaranteeCompPartMargin(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void GuaranteeCompPartMarginBL_DMLGuaranteeCompPartMargin_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GuaranteeCompPartMarginViewModel();
            model.IdGrntPartMrgn = 1;
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
            var result = _GuaranteeCompPartMarginBL.DMLGuaranteeCompPartMargin(UserManager.UserInfo, model);

            var filter = new GuaranteeCompPartMarginListModel();
            filter.CountryId = 1;
            filter.CurrencyCode = guid;
            filter.ShortCode = guid;
            filter.GrntPriceAndCurrencyCode = guid;

            int count = 0;
            var resultGet = _GuaranteeCompPartMarginBL.ListGuaranteeCompPartMargin(UserManager.UserInfo, filter, out count);

            var modelDelete = new GuaranteeCompPartMarginViewModel();
            modelDelete.IdGrntPartMrgn = resultGet.Data.First().IdGrntPartMrgn;
            modelDelete.CountryId = 1;
            modelDelete.CreateUser = guid;
            modelDelete.UpdateUser = guid;
            modelDelete.CountryName = guid;
            modelDelete.CurrencyName = guid;
            modelDelete.CurrencyCode = guid;
            modelDelete.ShortCode = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _GuaranteeCompPartMarginBL.DMLGuaranteeCompPartMargin(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}

