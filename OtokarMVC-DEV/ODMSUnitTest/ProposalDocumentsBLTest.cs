using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using System.IO;
using ODMSBusiness.Business;
using ODMSCommon;
using ODMSModel.ProposalDocuments;


namespace ODMSUnitTest
{

	[TestClass]
	public class ProposalDocumentsBLTest
	{

		ProposalDocumentsBL _ProposalDocumentsBL = new ProposalDocumentsBL();

		[TestMethod]
		public void ProposalDocumentsBL_DMLProposalDocuments_Insert()
		{
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ProposalDocumentsViewModel();
			model.IsRequestRoot= true; 
			model.ProposalDocId= 1; 
			model.ProposalId= 1; 
			model.ProposalSeq= 1; 
			model.DocId= 1; 
			model.DocName= guid; 
			model.DocMimeType= guid; 
			model.Description= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ProposalDocumentsBL.DMLProposalDocuments(UserManager.UserInfo, model);
			
			Assert.IsTrue(result.IsSuccess);
		}

		[TestMethod]
		public void ProposalDocumentsBL_GetProposalDocument_GetModel()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ProposalDocumentsViewModel();
			model.IsRequestRoot= true; 
			model.ProposalDocId= 1; 
			model.ProposalId= 1; 
			model.ProposalSeq= 1; 
			model.DocId= 1; 
			model.DocName= guid; 
			model.DocMimeType= guid; 
			model.Description= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ProposalDocumentsBL.DMLProposalDocuments(UserManager.UserInfo, model);
			
			var filter = new ProposalDocumentsViewModel();
			filter.ProposalId = result.Model.ProposalId;
			
			 var resultGet = _ProposalDocumentsBL.GetProposalDocument(filter);
			
			Assert.IsTrue(resultGet.Model != null && resultGet.Model.ProposalId > 0 && resultGet.IsSuccess);
		}

		[TestMethod]
		public void ProposalDocumentsBL_ListProposalDocuments_GetAll()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ProposalDocumentsViewModel();
			model.IsRequestRoot= true; 
			model.ProposalDocId= 1; 
			model.ProposalId= 1; 
			model.ProposalSeq= 1; 
			model.DocId= 1; 
			model.DocName= guid; 
			model.DocMimeType= guid; 
			model.Description= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ProposalDocumentsBL.DMLProposalDocuments(UserManager.UserInfo, model);
			
			int count = 0;
			var filter = new ProposalDocumentsListModel();
			
			 var resultGet = _ProposalDocumentsBL.ListProposalDocuments(UserManager.UserInfo, filter, out count);
			
			Assert.IsTrue(resultGet.Total > 0);
		}

		[TestMethod]
		public void ProposalDocumentsBL_DMLProposalDocuments_Update()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ProposalDocumentsViewModel();
			model.IsRequestRoot= true; 
			model.ProposalDocId= 1; 
			model.ProposalId= 1; 
			model.ProposalSeq= 1; 
			model.DocId= 1; 
			model.DocName= guid; 
			model.DocMimeType= guid; 
			model.Description= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ProposalDocumentsBL.DMLProposalDocuments(UserManager.UserInfo, model);
			
			var filter = new ProposalDocumentsListModel();
			
			int count = 0;
			 var resultGet = _ProposalDocumentsBL.ListProposalDocuments(UserManager.UserInfo, filter, out count);
			
			var modelUpdate = new ProposalDocumentsViewModel();
		    modelUpdate.ProposalId = (int)resultGet.Data.First().ProposalId;
            modelUpdate.DocName= guid; 
			modelUpdate.DocMimeType= guid; 
			modelUpdate.Description= guid; 
			modelUpdate.CommandType = "U";
			 var resultUpdate = _ProposalDocumentsBL.DMLProposalDocuments(UserManager.UserInfo, modelUpdate);
			Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
		}

		[TestMethod]
		public void ProposalDocumentsBL_DMLProposalDocuments_Delete()
		{
			
			var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); 
			var model = new ProposalDocumentsViewModel();
			model.IsRequestRoot= true; 
			model.ProposalDocId= 1; 
			model.ProposalId= 1; 
			model.ProposalSeq= 1; 
			model.DocId= 1; 
			model.DocName= guid; 
			model.DocMimeType= guid; 
			model.Description= guid; 
			model.UpdateUser= 1; 
			model.UpdateDate= DateTime.Now; 
			model.IsActive= true; 
			model.CommandType = "I";
			 var result = _ProposalDocumentsBL.DMLProposalDocuments(UserManager.UserInfo, model);
			
			var filter = new ProposalDocumentsListModel();
			
			int count = 0;
			 var resultGet = _ProposalDocumentsBL.ListProposalDocuments(UserManager.UserInfo, filter, out count);
			
			var modelDelete = new ProposalDocumentsViewModel();
			modelDelete.ProposalId = (int) resultGet.Data.First().ProposalId;
			modelDelete.DocName= guid; 
			modelDelete.DocMimeType= guid; 
			modelDelete.Description= guid; 
			modelDelete.CommandType = "D";
			 var resultDelete = _ProposalDocumentsBL.DMLProposalDocuments(UserManager.UserInfo, modelDelete);
			Assert.IsTrue(resultDelete.IsSuccess);
		}


	}

}

