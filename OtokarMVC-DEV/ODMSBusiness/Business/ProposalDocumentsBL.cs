using ODMSData;
using ODMSModel.ProposalDocuments;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness.Business
{
    public class ProposalDocumentsBL : BaseBusiness
    {
        private readonly ProposalDocumentsData data = new ProposalDocumentsData();
        public ResponseModel<ProposalDocumentsListModel> ListProposalDocuments(UserInfo user, ProposalDocumentsListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<ProposalDocumentsListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListProposalDocuments(user, filter, out totalCnt);
                response.Total = totalCnt;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
           
        }

        public ResponseModel<ProposalDocumentsViewModel> DMLProposalDocuments(UserInfo user,Stream s, ProposalDocumentsViewModel model)
        {
            var response = new ResponseModel<ProposalDocumentsViewModel>();
            byte[] image = new byte[s.Length];
            s.Read(image, 0, image.Length);
            model.DocImage = image;

            try
            {
                data.DMLProposalDocuments(user, model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }
        public ResponseModel<ProposalDocumentsViewModel> DMLProposalDocuments(UserInfo user,ProposalDocumentsViewModel model)
        {
            var response = new ResponseModel<ProposalDocumentsViewModel>();
            try
            {
                data.DMLProposalDocuments(user, model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<ProposalDocumentsViewModel> GetProposalDocument(ProposalDocumentsViewModel model)
        {
            var response = new ResponseModel<ProposalDocumentsViewModel>();
            try
            {
                data.GetProposalDocument(model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
            
        }
    }
}
