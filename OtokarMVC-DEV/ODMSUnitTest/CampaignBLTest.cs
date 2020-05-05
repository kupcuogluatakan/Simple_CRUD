using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.Campaign;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class CampaignBLTest
    {

        CampaignBL _CampaignBL = new CampaignBL();

        [TestMethod]
        public void CampaignBL_DMLCampaign_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CampaignViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.CampaignCode = guid;
            model.AdminDesc = guid;
            model.StartDate = DateTime.Now;
            model.EndDate = DateTime.Now;
            model.ModelKod = "ATLAS";
            model.MainFailureDesc = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.IsValidAbroadName = guid;
            model.SubCategoryName = guid;
            model.IsMust = true;
            model.IsMustName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _CampaignBL.DMLCampaign(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void CampaignBL_GetCampaign_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CampaignViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.CampaignCode = guid;
            model.AdminDesc = guid;
            model.StartDate = DateTime.Now;
            model.EndDate = DateTime.Now;
            model.ModelKod = "ATLAS";
            model.MainFailureDesc = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.IsValidAbroadName = guid;
            model.SubCategoryName = guid;
            model.IsMust = true;
            model.IsMustName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _CampaignBL.DMLCampaign(UserManager.UserInfo, model);

            var filter = new CampaignViewModel();
            filter.MultiLanguageContentAsText = "TR || TEST";
            filter.CampaignCode = guid;
            filter.ModelKod = "ATLAS";

            var resultGet = _CampaignBL.GetCampaign(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.AdminDesc != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void CampaignBL_ListCampaignAsSelectListItem_GetAll()
        {
            var resultGet = CampaignBL.ListCampaignAsSelectListItem(UserManager.UserInfo, "ATLAS");

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void CampaignBL_ListCampaigns_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CampaignViewModel();
            model.MultiLanguageContentAsText = "TR || TEST";
            model.CampaignCode = guid;
            model.AdminDesc = guid;
            model.StartDate = DateTime.Now;
            model.EndDate = DateTime.Now;
            model.ModelKod = "ATLAS";
            model.MainFailureDesc = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.IsValidAbroadName = guid;
            model.SubCategoryName = guid;
            model.IsMust = true;
            model.IsMustName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _CampaignBL.DMLCampaign(UserManager.UserInfo, model);

            int count = 0;
            var filter = new CampaignListModel();
            filter.CampaignCode = guid;
            filter.ModelKod = "ATLAS";
            filter.MainFailureCode = guid;
            filter.IndicatorCode = guid;

            var resultGet = _CampaignBL.ListCampaigns(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

