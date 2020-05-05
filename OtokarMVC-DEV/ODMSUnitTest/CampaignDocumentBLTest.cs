using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.CampaignDocument;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class CampaignDocumentBLTest
    {

        CampaignDocumentBL _CampaignDocumentBL = new CampaignDocumentBL();

        [TestMethod]
        public void CampaignDocumentBL_DMLCampaignDocument_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CampaignDocumentViewModel();
            model.CampaignCode = "508";
            model.DocId = 1;
            model.DocName = guid;
            model.LanguageCode = guid;
            model.LanguageName = guid;
            model.DocumentDesc = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CampaignDocumentBL.DMLCampaignDocument(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void CampaignDocumentBL_GetCampaignDocument_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CampaignDocumentViewModel();
            model.CampaignCode = "508";
            model.DocId = 1;
            model.DocName = guid;
            model.LanguageCode = guid;
            model.LanguageName = guid;
            model.DocumentDesc = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CampaignDocumentBL.DMLCampaignDocument(UserManager.UserInfo, model);

            var filter = new CampaignDocumentViewModel();
            filter.DocId = result.Model.DocId;
            filter.CampaignCode = "508";
            filter.LanguageCode = guid;

            var resultGet = _CampaignDocumentBL.GetCampaignDocument(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.DocName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void CampaignDocumentBL_ListCampaignDocuments_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CampaignDocumentViewModel();
            model.CampaignCode = "508";
            model.DocId = 1;
            model.DocName = guid;
            model.LanguageCode = guid;
            model.LanguageName = guid;
            model.DocumentDesc = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _CampaignDocumentBL.DMLCampaignDocument(UserManager.UserInfo, model);

            int count = 0;
            var filter = new CampaignDocumentListModel();
            filter.CampaignCode = "508";
            filter.LanguageCode = guid;

            var resultGet = _CampaignDocumentBL.ListCampaignDocuments(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

