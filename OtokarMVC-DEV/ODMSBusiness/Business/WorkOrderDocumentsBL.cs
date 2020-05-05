using System.Collections.Generic;
using System.IO;
using ODMSData;
using ODMSModel.WorkOrderDocuments;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class WorkOrderDocumentsBL : BaseBusiness
    {
        private readonly WorkOrderDocumentsData data = new WorkOrderDocumentsData();

        public ResponseModel<WorkOrderDocumentsListModel> ListWorkOrderDocuments(UserInfo user, WorkOrderDocumentsListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<WorkOrderDocumentsListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListWorkOrderDocuments(user, filter, out totalCnt);
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

        public ResponseModel<WorkOrderDocumentsViewModel> DMLWorkOrderDocuments(UserInfo user, Stream s, WorkOrderDocumentsViewModel model)
        {
            var response = new ResponseModel<WorkOrderDocumentsViewModel>();
            try
            {
                byte[] image = new byte[s.Length];
                s.Read(image, 0, image.Length);
                model.DocImage = image;

                data.DMLWorkOrderDocuments(user, model);

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

        public ResponseModel<WorkOrderDocumentsViewModel> DMLWorkOrderDocuments(UserInfo user, WorkOrderDocumentsViewModel model)
        {
            var response = new ResponseModel<WorkOrderDocumentsViewModel>();
            try
            {
                data.DMLWorkOrderDocuments(user, model);
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

        public ResponseModel<WorkOrderDocumentsViewModel> GetWorkOrderDocument(WorkOrderDocumentsViewModel filter)
        {
            var response = new ResponseModel<WorkOrderDocumentsViewModel>();
            try
            {
                data.GetWorkOrderDocument(filter);
                response.Model = filter;
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


    }
}
