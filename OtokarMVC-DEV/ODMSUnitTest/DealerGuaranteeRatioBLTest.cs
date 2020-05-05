using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.DealerGuaranteeRatio;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class DealerGuaranteeRatioBLTest
    {

        DealerGuaranteeRatioBL _DealerGuaranteeRatioBL = new DealerGuaranteeRatioBL();

        [TestMethod]
        public void DealerGuaranteeRatioBL_DMLDealerGuaranteeRatio_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerGuaranteeRatioIndexViewModel();
            model.DealerName = guid;
            model.DealerSSID = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerGuaranteeRatioBL.DMLDealerGuaranteeRatio(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void DealerGuaranteeRatioBL_GetDealerGuaranteeRatio_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerGuaranteeRatioIndexViewModel();
            model.IdDealer = UserManager.UserInfo.GetUserDealerId();
            model.DealerName = guid;
            model.DealerSSID = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerGuaranteeRatioBL.DMLDealerGuaranteeRatio(UserManager.UserInfo, model);

            var filter = new DealerGuaranteeRatioIndexViewModel();
            filter.IdDealer = result.Model.IdDealer;

            var resultGet = _DealerGuaranteeRatioBL.GetDealerGuaranteeRatio(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.GuaranteeRatio > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void DealerGuaranteeRatioBL_ListDealerGuaranteeRatio_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerGuaranteeRatioIndexViewModel();
            model.IdDealer = UserManager.UserInfo.GetUserDealerId();
            model.DealerName = guid;
            model.DealerSSID = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerGuaranteeRatioBL.DMLDealerGuaranteeRatio(UserManager.UserInfo, model);

            int count = 0;
            var filter = new DealerGuaranteeRatioListModel();
            filter.IdDealer = result.Model.IdDealer;
            var resultGet = _DealerGuaranteeRatioBL.ListDealerGuaranteeRatio(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void DealerGuaranteeRatioBL_DMLDealerGuaranteeRatio_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerGuaranteeRatioIndexViewModel();
            model.IdDealer = UserManager.UserInfo.GetUserDealerId();
            model.DealerName = guid;
            model.DealerSSID = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerGuaranteeRatioBL.DMLDealerGuaranteeRatio(UserManager.UserInfo, model);

            var filter = new DealerGuaranteeRatioListModel();
            filter.IdDealer = result.Model.IdDealer;

            int count = 0;
            var resultGet = _DealerGuaranteeRatioBL.ListDealerGuaranteeRatio(UserManager.UserInfo, filter, out count);

            var modelUpdate = new DealerGuaranteeRatioIndexViewModel();
            modelUpdate.DealerName = guid;
            modelUpdate.DealerSSID = guid;
            modelUpdate.IdDealer = UserManager.UserInfo.GetUserDealerId();
            modelUpdate.GuaranteeRatio = 80;
            modelUpdate.CommandType = "U";
            var resultUpdate = _DealerGuaranteeRatioBL.DMLDealerGuaranteeRatio(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void DealerGuaranteeRatioBL_DMLDealerGuaranteeRatio_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerGuaranteeRatioIndexViewModel();
            model.IdDealer = UserManager.UserInfo.GetUserDealerId();
            model.DealerName = guid;
            model.DealerSSID = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerGuaranteeRatioBL.DMLDealerGuaranteeRatio(UserManager.UserInfo, model);

            var filter = new DealerGuaranteeRatioListModel();
            filter.IdDealer = result.Model.IdDealer;
            int count = 0;
            var resultGet = _DealerGuaranteeRatioBL.ListDealerGuaranteeRatio(UserManager.UserInfo, filter, out count);

            var modelDelete = new DealerGuaranteeRatioIndexViewModel();
            modelDelete.DealerName = guid;
            modelDelete.DealerSSID = guid;
            modelDelete.IdDealer = UserManager.UserInfo.GetUserDealerId();
            modelDelete.GuaranteeRatio = 80;
            modelDelete.CommandType = "D";
            var resultDelete = _DealerGuaranteeRatioBL.DMLDealerGuaranteeRatio(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}

