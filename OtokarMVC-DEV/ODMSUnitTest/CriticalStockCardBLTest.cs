using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.CriticalStockCard;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class CriticalStockCardBLTest
    {

        CriticalStockCardBL _CriticalStockCardBL = new CriticalStockCardBL();

        [TestMethod]
        public void CriticalStockCardBL_DMLCriticalStockCard_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CriticalStockCardViewModel();
            model.StockCardId = 1;
            model.DealerName = guid;
            model.PartCode = "M.162127";
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.SSID = guid;
            model.ShipQty = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CriticalStockCardBL.DMLCriticalStockCard(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void CriticalStockCardBL_GetCriticalStockCard_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CriticalStockCardViewModel();
            model.StockCardId = 1;
            model.DealerName = guid;
            model.PartCode = "M.162127";
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.SSID = guid;
            model.ShipQty = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CriticalStockCardBL.DMLCriticalStockCard(UserManager.UserInfo, model);

            var filter = new CriticalStockCardViewModel();
            filter.IdPart = result.Model.IdPart;
            filter.PartCode = "M.162127";

            var resultGet = _CriticalStockCardBL.GetCriticalStockCard(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.PartName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void CriticalStockCardBL_ListCriticalStockCard_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CriticalStockCardViewModel();
            model.StockCardId = 1;
            model.DealerName = guid;
            model.PartCode = "M.162127";
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.SSID = guid;
            model.ShipQty = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CriticalStockCardBL.DMLCriticalStockCard(UserManager.UserInfo, model);

            int count = 0;
            var filter = new CriticalStockCardListModel();
            filter.PartCode = "M.162127";
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";

            var resultGet = _CriticalStockCardBL.ListCriticalStockCard(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

    }

}

