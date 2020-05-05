using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.CampaignDismantlePart;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class CampaignDismantlePartBLTest
    {

        CampaignDismantlePartBL _CampaignDismantlePartBL = new CampaignDismantlePartBL();

        [TestMethod]
        public void CampaignDismantlePartBL_DMLCampaignDismantlePart_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CampaignDismantlePartViewModel();
            model.CampaignCode = "508";
            model.PartTypeDesc = guid;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.Note = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CampaignDismantlePartBL.DMLCampaignDismantlePart(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void CampaignDismantlePartBL_GetCampaignDismantlePart_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CampaignDismantlePartViewModel();
            model.CampaignCode = "508";
            model.PartTypeDesc = guid;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.Note = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CampaignDismantlePartBL.DMLCampaignDismantlePart(UserManager.UserInfo, model);

            var filter = new CampaignDismantlePartViewModel();
            filter.CampaignCode = "508";
            filter.PartId = 39399;

            var resultGet = _CampaignDismantlePartBL.GetCampaignDismantlePart(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.PartName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void CampaignDismantlePartBL_ListCampaignDismantlePart_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CampaignDismantlePartViewModel();
            model.CampaignCode = "508";
            model.PartTypeDesc = guid;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.Note = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CampaignDismantlePartBL.DMLCampaignDismantlePart(UserManager.UserInfo, model);

            int count = 0;
            var filter = new CampaignDismantlePartListModel();
            filter.PartCode = "M.162127";
            filter.CampaignCode = "508";
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            filter.PartId = 39399;

            var resultGet = _CampaignDismantlePartBL.ListCampaignDismantlePart(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

    }

}

