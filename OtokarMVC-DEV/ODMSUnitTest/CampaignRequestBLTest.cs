using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.CampaignRequest;


namespace ODMSUnitTest
{

    [TestClass]
    public class CampaignRequestBLTest
    {

        CampaignRequestBL _CampaignRequestBL = new CampaignRequestBL();

        [TestMethod]
        public void CampaignRequestBL_DMLCampaignRequest_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CampaignRequestViewModel();
            model.CampaignCode = "508";
            model.DealerName = guid;
            model.VerihcleModelCode = guid;
            model.CampaignName = guid;
            model.RequestNote = guid;
            model.RequestStatusName = guid;
            model.WorkOrderId = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CampaignRequestBL.DMLCampaignRequest(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void CampaignRequestBL_GetCampaignRequest_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CampaignRequestViewModel();
            model.CampaignCode = "508";
            model.DealerName = guid;
            model.VerihcleModelCode = guid;
            model.CampaignName = guid;
            model.RequestNote = guid;
            model.RequestStatusName = guid;
            model.WorkOrderId = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CampaignRequestBL.DMLCampaignRequest(UserManager.UserInfo, model);

            var filter = new CampaignRequestViewModel();
            filter.IdCampaignRequest = result.Model.IdCampaignRequest;
            filter.CampaignCode = "508";
            filter.VerihcleModelCode = guid;

            var resultGet = _CampaignRequestBL.GetCampaignRequest(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.DealerName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void CampaignRequestBL_ListCampaignRequest_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CampaignRequestViewModel();
            model.CampaignCode = "508";
            model.DealerName = guid;
            model.VerihcleModelCode = guid;
            model.CampaignName = guid;
            model.RequestNote = guid;
            model.RequestStatusName = guid;
            model.WorkOrderId = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CampaignRequestBL.DMLCampaignRequest(UserManager.UserInfo, model);

            int count = 0;
            var filter = new CampaignRequestListModel();
            filter.CampaignCode = "508";
            filter.VerihcleModelCode = guid;

            var resultGet = _CampaignRequestBL.ListCampaignRequest(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void CampaignRequestBL_ListCampaignRequestDetails_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CampaignRequestViewModel();
            model.CampaignCode = "508";
            model.DealerName = guid;
            model.VerihcleModelCode = guid;
            model.CampaignName = guid;
            model.RequestNote = guid;
            model.RequestStatusName = guid;
            model.WorkOrderId = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CampaignRequestBL.DMLCampaignRequest(UserManager.UserInfo, model);

            int count = 0;
            var filter = new CampaignRequestDetailListModel();
            filter.PartId = 39399;
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";

            var resultGet = _CampaignRequestBL.ListCampaignRequestDetails(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void CampaignRequestBL_ListCampaignRequestDetailsAndQuantity_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CampaignRequestViewModel();
            model.CampaignCode = "508";
            model.DealerName = guid;
            model.VerihcleModelCode = guid;
            model.CampaignName = guid;
            model.RequestNote = guid;
            model.RequestStatusName = guid;
            model.WorkOrderId = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CampaignRequestBL.DMLCampaignRequest(UserManager.UserInfo, model);

            int count = 0;
            var filter = new CampaignRequestDetailListModel();
            filter.PartId = 39399;
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";

            var resultGet = _CampaignRequestBL.ListCampaignRequestDetailsAndQuantity(filter);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

