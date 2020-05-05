using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.CampaignRequestApprove;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class CampaignRequestApproveBLTest
    {

        CampaignRequestApproveBL _CampaignRequestApproveBL = new CampaignRequestApproveBL();

        [TestMethod]
        public void CampaignRequestApproveBL_DMLCampaignRequestApprove_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CampaignRequestApproveViewModel();
            model.CampaignRequestId = 1;
            model.RequestDealerName = guid;
            model.SupplierDealerName = guid;
            model.ModelKod = "ATLAS";
            model.CampaignCode = "508";
            model.CampaignName = guid;
            model.RequestStatusId = 1;
            model.RequestStatusName = guid;
            model.SupplierTypeName = guid;
            model.RequestNote = guid;
            model.IsWorkOrderRelated = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CampaignRequestApproveBL.DMLCampaignRequestApprove(UserManager.UserInfo,model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void CampaignRequestApproveBL_GetCampaignRequestApprove_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CampaignRequestApproveViewModel();
            model.CampaignRequestId = 1;
            model.RequestDealerName = guid;
            model.SupplierDealerName = guid;
            model.ModelKod = "ATLAS";
            model.CampaignCode = "508";
            model.CampaignName = guid;
            model.RequestStatusId = 1;
            model.RequestStatusName = guid;
            model.SupplierTypeName = guid;
            model.RequestNote = guid;
            model.IsWorkOrderRelated = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CampaignRequestApproveBL.DMLCampaignRequestApprove(UserManager.UserInfo, model);

            var filter = new CampaignRequestApproveViewModel();
            filter.CampaignRequestId = result.Model.CampaignRequestId;
            filter.ModelKod = "ATLAS";
            filter.CampaignCode = "508";

            var resultGet = _CampaignRequestApproveBL.GetCampaignRequestApprove(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Quantity > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void CampaignRequestApproveBL_ListSupplierDealerAsSelectListItem_GetAll()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CampaignRequestApproveViewModel();
            model.CampaignRequestId = 1;
            model.RequestDealerName = guid;
            model.SupplierDealerName = guid;
            model.ModelKod = "ATLAS";
            model.CampaignCode = "508";
            model.CampaignName = guid;
            model.RequestStatusId = 1;
            model.RequestStatusName = guid;
            model.SupplierTypeName = guid;
            model.RequestNote = guid;
            model.IsWorkOrderRelated = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CampaignRequestApproveBL.DMLCampaignRequestApprove(UserManager.UserInfo, model);

            var resultGet = CampaignRequestApproveBL.ListSupplierDealerAsSelectListItem(0, result.Model.CampaignRequestId);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void CampaignRequestApproveBL_ListCampaignRequestApprove_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CampaignRequestApproveViewModel();
            model.CampaignRequestId = 1;
            model.RequestDealerName = guid;
            model.SupplierDealerName = guid;
            model.ModelKod = "ATLAS";
            model.CampaignCode = "508";
            model.CampaignName = guid;
            model.RequestStatusId = 1;
            model.RequestStatusName = guid;
            model.SupplierTypeName = guid;
            model.RequestNote = guid;
            model.IsWorkOrderRelated = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CampaignRequestApproveBL.DMLCampaignRequestApprove(UserManager.UserInfo, model);

            int count = 0;
            var filter = new CampaignRequestApproveListModel();
            filter.ModelKod = "ATLAS";
            filter.CampaignCode = "508";

            var resultGet = _CampaignRequestApproveBL.ListCampaignRequestApprove(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

