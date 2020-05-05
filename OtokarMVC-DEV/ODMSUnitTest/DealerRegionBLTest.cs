using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.DealerRegion;
using ODMSModel.ListModel;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class DealerRegionBLTest
    {

        DealerRegionBL _DealerRegionBL = new DealerRegionBL();

        [TestMethod]
        public void DealerRegionBL_DMLDealerRegion_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerRegionIndexViewModel();
            model.DealerRegionId = 1;
            model.DealerRegionName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerRegionBL.DMLDealerRegion(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void DealerRegionBL_GetDealerRegion_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerRegionIndexViewModel();
            model.DealerRegionName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerRegionBL.DMLDealerRegion(UserManager.UserInfo, model);

            var filter = new DealerRegionIndexViewModel();
            filter.DealerRegionId = result.Model.DealerRegionId;

            var resultGet = _DealerRegionBL.GetDealerRegion(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.DealerRegionName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void DealerRegionBL_ListDealerRegions_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerRegionIndexViewModel();
            model.DealerRegionName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerRegionBL.DMLDealerRegion(UserManager.UserInfo, model);

            int count = 0;
            var filter = new DealerRegionListModel();

            var resultGet = _DealerRegionBL.ListDealerRegions(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void DealerRegionBL_DMLDealerRegion_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerRegionIndexViewModel();
            model.DealerRegionName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerRegionBL.DMLDealerRegion(UserManager.UserInfo, model);

            var filter = new DealerRegionListModel();

            int count = 0;
            var resultGet = _DealerRegionBL.ListDealerRegions(UserManager.UserInfo, filter, out count);

            var modelUpdate = new DealerRegionIndexViewModel();
            modelUpdate.DealerRegionId = resultGet.Data.First().DealerRegionId;
            modelUpdate.DealerRegionName = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _DealerRegionBL.DMLDealerRegion(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void DealerRegionBL_DMLDealerRegion_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DealerRegionIndexViewModel();
            model.DealerRegionName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DealerRegionBL.DMLDealerRegion(UserManager.UserInfo, model);

            var filter = new DealerRegionListModel();

            int count = 0;
            var resultGet = _DealerRegionBL.ListDealerRegions(UserManager.UserInfo, filter, out count);

            var modelDelete = new DealerRegionIndexViewModel();
            modelDelete.DealerRegionId = resultGet.Data.First().DealerRegionId;

            modelDelete.DealerRegionName = guid;



            modelDelete.CommandType = "D";
            var resultDelete = _DealerRegionBL.DMLDealerRegion(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}

