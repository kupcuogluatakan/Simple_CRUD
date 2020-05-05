using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.DealerSaleChannelDiscountRatio;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class DealerSaleChannelDiscountRatioBLTest
    {

        DealerSaleChannelDiscountRatioBL _DealerSaleChannelDiscountRatioBL = new DealerSaleChannelDiscountRatioBL();

        [TestMethod]
        public void DealerSaleChannelDiscountRatioBL_DMLDealerSaleChannelDiscountRatio_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerSaleChannelDiscountRatioViewModel();
            model.DealerClassCode = guid;
            model.DealerClassName = guid;
            model.ChannelCode = guid;
            model.ChannelName = guid;
            model.SparePartClassCode = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerSaleChannelDiscountRatioBL.DMLDealerSaleChannelDiscountRatio(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void DealerSaleChannelDiscountRatioBL_GetDealerSaleChannelDiscountRatio_GetModel()
        {
            var resultGet = _DealerSaleChannelDiscountRatioBL.GetDealerSaleChannelDiscountRatio(UserManager.UserInfo, "5A", "2500", "00");

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.ChannelName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void DealerSaleChannelDiscountRatioBL_ListDealerSaleChannelDiscountRatios_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerSaleChannelDiscountRatioViewModel();
            model.DealerClassCode = guid;
            model.DealerClassName = guid;
            model.ChannelCode = guid;
            model.ChannelName = guid;
            model.SparePartClassCode = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerSaleChannelDiscountRatioBL.DMLDealerSaleChannelDiscountRatio(UserManager.UserInfo, model);

            int count = 0;
            var filter = new DealerSaleChannelDiscountRatioListModel();
            filter.DealerClassCode = guid;
            filter.ChannelCode = guid;
            filter.SparePartClassCode = guid;

            var resultGet = _DealerSaleChannelDiscountRatioBL.ListDealerSaleChannelDiscountRatios(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void DealerSaleChannelDiscountRatioBL_DMLDealerSaleChannelDiscountRatio_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerSaleChannelDiscountRatioViewModel();
            model.DealerClassCode = guid;
            model.DealerClassName = guid;
            model.ChannelCode = guid;
            model.ChannelName = guid;
            model.SparePartClassCode = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerSaleChannelDiscountRatioBL.DMLDealerSaleChannelDiscountRatio(UserManager.UserInfo, model);

            var filter = new DealerSaleChannelDiscountRatioListModel();
            filter.DealerClassCode = guid;
            filter.ChannelCode = guid;
            filter.SparePartClassCode = guid;

            int count = 0;
            var resultGet = _DealerSaleChannelDiscountRatioBL.ListDealerSaleChannelDiscountRatios(UserManager.UserInfo, filter, out count);

            var modelUpdate = new DealerSaleChannelDiscountRatioViewModel();
            modelUpdate.DealerClassCode = guid;
            modelUpdate.DealerClassName = guid;
            modelUpdate.ChannelCode = guid;
            modelUpdate.ChannelName = guid;
            modelUpdate.SparePartClassCode = guid;



            modelUpdate.CommandType = "U";
            var resultUpdate = _DealerSaleChannelDiscountRatioBL.DMLDealerSaleChannelDiscountRatio(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void DealerSaleChannelDiscountRatioBL_DMLDealerSaleChannelDiscountRatio_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerSaleChannelDiscountRatioViewModel();
            model.DealerClassCode = guid;
            model.DealerClassName = guid;
            model.ChannelCode = guid;
            model.ChannelName = guid;
            model.SparePartClassCode = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerSaleChannelDiscountRatioBL.DMLDealerSaleChannelDiscountRatio(UserManager.UserInfo, model);

            var filter = new DealerSaleChannelDiscountRatioListModel();
            filter.DealerClassCode = guid;
            filter.ChannelCode = guid;
            filter.SparePartClassCode = guid;

            int count = 0;
            var resultGet = _DealerSaleChannelDiscountRatioBL.ListDealerSaleChannelDiscountRatios(UserManager.UserInfo, filter, out count);

            var modelDelete = new DealerSaleChannelDiscountRatioViewModel();
            modelDelete.DealerClassCode = guid;
            modelDelete.DealerClassName = guid;
            modelDelete.ChannelCode = guid;
            modelDelete.ChannelName = guid;
            modelDelete.SparePartClassCode = guid;



            modelDelete.CommandType = "D";
            var resultDelete = _DealerSaleChannelDiscountRatioBL.DMLDealerSaleChannelDiscountRatio(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}

