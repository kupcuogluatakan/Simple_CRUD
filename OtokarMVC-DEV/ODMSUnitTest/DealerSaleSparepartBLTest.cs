using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.DealerSaleSparepart;


namespace ODMSUnitTest
{

    [TestClass]
    public class DealerSaleSparepartBLTest
    {

        DealerSaleSparepartBL _DealerSaleSparepartBL = new DealerSaleSparepartBL();

        [TestMethod]
        public void DealerSaleSparepartBL_DMLDealerSaleSparepart_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerSaleSparepartIndexViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.DiscountRatioString = guid;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.PartCode = "M.162127";
            model.IsActive = true;
            model.IsActiveName = guid;
            model.ShipQty = 1;
            model.Unit = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _DealerSaleSparepartBL.DMLDealerSaleSparepart(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void DealerSaleSparepartBL_DMLDealerSaleSparepartList_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new ListDealerSaleSparepartIndexViewModel();
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerSaleSparepartBL.DMLDealerSaleSparepartList(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void DealerSaleSparepartBL_GetDealerSaleSparepart_GetModel()
        {
            var filter = new DealerSaleSparepartIndexViewModel();
            filter.IdPart = 39399;
            filter.IdDealer = UserManager.UserInfo.GetUserDealerId();

            var resultGet = _DealerSaleSparepartBL.GetDealerSaleSparepart(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.SalePrice > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void DealerSaleSparepartBL_ListDealerSaleSparepart_GetAll()
        {
            int count = 0;
            var filter = new DealerSaleSparepartListModel();
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            filter.PartCode = "M.162127";
            filter.MultiLanguageContentAsText = "TR || TEST";
            filter.IdDealer = UserManager.UserInfo.GetUserDealerId();
            var resultGet = _DealerSaleSparepartBL.ListDealerSaleSparepart(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void DealerSaleSparepartBL_GetSparepartListPrice_GetAll()
        {
            var resultGet = _DealerSaleSparepartBL.GetSparepartListPrice(UserManager.UserInfo, null);

            Assert.IsTrue(resultGet.Total > 0);
        }

    }

}

