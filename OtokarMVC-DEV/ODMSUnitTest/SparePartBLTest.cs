using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.SparePart;
using System;
using System.Collections.Generic;
using ODMSModel.ListModel;


namespace ODMSUnitTest
{

    [TestClass]
    public class SparePartBLTest
    {

        SparePartBL _SparePartBL = new SparePartBL();

        [TestMethod]
        public void SparePartBL_DMLSparePart_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new SparePartIndexViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.PartId = 1;
            model.IsOriginalName = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.OriginalPartId = 1;
            model.OriginalPartName = guid;
            model.PartTypeCode = guid;
            model.PartTypeName = guid;
            model.PartNameInLanguage = guid;
            model.CompatibleGuaranteeUsageName = guid;
            model.VatRatio = guid;
            model.UnitName = guid;
            model.Brand = guid;
            model.Weight = 1;
            model.Volume = 1;
            model.NSN = guid;
            model.PartSection = guid;
            model.PartCode = "M.162127";
            model.AdminDesc = guid;
            model.ClassCode = guid;
            model.DiscountRatio = 1;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.PartName.MultiLanguageContentAsText = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.GuaranteeAuthorityNeedName = guid;
            model.IsOrderAllowed = true;
            model.Barcode = guid;
            model.AlternatePart = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _SparePartBL.DMLSparePart(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void SparePartBL_UpdateSparePartUnserve_Insert()
        {
            var result = _SparePartBL.UpdateSparePartUnserve(UserManager.UserInfo, 76, 257793, 1, 2, "TEST", 11);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void SparePartBL_GetSparePart_GetModel()
        {
            var result = _SparePartBL.UpdateSparePartUnserve(UserManager.UserInfo, 76, 257793, 1, 2, "TEST", 11);
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);

            var filter = new SparePartIndexViewModel();
            filter.MultiLanguageContentAsText = "TR || TEST";
            filter.PartId = 39399;
            filter.DealerId = UserManager.UserInfo.DealerID;
            filter.PartTypeCode = guid;
            filter.PartCode = "M.162127";
            filter.ClassCode = guid;
            filter.PartName.MultiLanguageContentAsText = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";

            var resultGet = _SparePartBL.GetSparePart(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void SparePartBL_GetSparePartFromTable_GetModel()
        {
            List<SparePartIndexViewModel> list = new List<SparePartIndexViewModel>();
            var resultGet = _SparePartBL.GetSparePartFromTable(list);

            Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void SparePartBL_GetFreeQuantity_GetModel()
        {
            var resultGet = _SparePartBL.GetFreeQuantity(257793, 76, 1);

            Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void SparePartBL_GetDiscountPrice_GetModel()
        {
            var resultGet = _SparePartBL.GetDiscountPrice(257793, 76, 1);

            Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void SparePartBL_GetCustomerPartDiscount_GetModel()
        {
            var resultGet = _SparePartBL.GetCustomerPartDiscount(257793, 76, 1, "TEST");

            Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void SparePartBL_ListStockCardsAsSelectListItem_GetAll()
        {
            var resultGet = SparePartBL.ListStockCardsAsSelectListItem(UserManager.UserInfo, true);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void SparePartBL_ListSpareParts_GetAll()
        {
            var result = _SparePartBL.UpdateSparePartUnserve(UserManager.UserInfo, 76, 257793, 1, 2, "TEST", 11);
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            int count = 0;
            var filter = new SparePartListModel();
            filter.PartId = 39399;
            filter.PartCode = "M.162127";
            filter.DealerId = UserManager.UserInfo.DealerID;
            filter.PartTypeCode = guid;
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";

            var resultGet = _SparePartBL.ListSpareParts(UserManager.UserInfo,filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void SparePartBL_ListSparePartAsAutoCompSearch_Get_ByPartCode()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var resultGet = SparePartBL.ListSparePartAsAutoCompSearch(UserManager.UserInfo,"AAA", "143");

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void SparePartBL_ListSparePartAsAutoCompSearch_GetAll()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var resultGet = SparePartBL.ListSparePartAsAutoCompSearch(UserManager.UserInfo, guid, guid);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void SparePartBL_ListOriginalSparePartAsAutoCompSearch_GetAll()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var resultGet = SparePartBL.ListOriginalSparePartAsAutoCompSearch(UserManager.UserInfo, guid);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void SparePartBL_ListNotOriginalSparePartAsAutoCompSearch_GetAll()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var resultGet = SparePartBL.ListNotOriginalSparePartAsAutoCompSearch(UserManager.UserInfo, guid);

            Assert.IsTrue(resultGet.Total > 0);
        }
        
        [TestMethod]
        public void SparePartBL_ListSparePartsSupplyDiscountRatios_GetAll()
        {
            
            var result = _SparePartBL.UpdateSparePartUnserve(UserManager.UserInfo, 76, 257793, 1, 2, "TEST", 11); 

            int count = 0;
            var filter = new SparePartSupplyDiscountRatioListModel();
            filter.PartId = 39399;

            var resultGet = _SparePartBL.ListSparePartsSupplyDiscountRatios(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }
        
        [TestMethod]
        public void SparePartBL_ListSparePartsSplitting_GetAll()
        {
            var resultGet = _SparePartBL.ListSparePartsSplitting(257793);

            Assert.IsTrue(resultGet.Total > 0);
        }
        
        [TestMethod]
        public void SparePartBL_ListStockCardPartsAsAutoCompSearch_GetAll()
        {
            var resultGet = SparePartBL.ListStockCardPartsAsAutoCompSearch(UserManager.UserInfo, "");

            Assert.IsTrue(resultGet.Total > 0);
        }
        
        [TestMethod]
        public void SparePartBL_GetPriceListSsidByPriceListId_GetAll()
        {
            var resultGet = _SparePartBL.GetPriceListSsidByPriceListId(1);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void SparePartBL_UpdateSparePartUnserve_Update()
        {
            var resultUpdate = _SparePartBL.UpdateSparePartUnserve(UserManager.UserInfo, 76, 257793, 1, 2, "TEST", 11);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void SparePartBL_UpdateSparePartUnserve_Delete()
        {
            var resultDelete = _SparePartBL.UpdateSparePartUnserve(UserManager.UserInfo, 76, 257793, 1, 2, "TEST", 11);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}

