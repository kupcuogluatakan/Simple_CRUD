using System.Collections.Generic;
using System.Web.Mvc;
using ODMSCommon.Security;
using ODMSData;
using ODMSModel.EducationRequest;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class EducationRequestBL : BaseBusiness
    {
        private readonly EducationRequestData data = new EducationRequestData();

        public ResponseModel<EducationRequestIndexModel> GetEducationRequestIndexModel(UserInfo user)
        {
            var response = new ResponseModel<EducationRequestIndexModel>();
            try
            {
                response.Model = data.GetEducationRequestIndexModel(user);
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

        public ResponseModel<EducationRequestDetailModel> SaveEducationRequest(UserInfo user, EducationRequestDetailModel model)
        {
            var response = new ResponseModel<EducationRequestDetailModel>();
            try
            {
                data.SaveEducationRequest(user, model);
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

        public ResponseModel<EducationRequestDetailModel> DeleteWorkerFromEducationRequest(EducationRequestDetailModel model)
        {
            var response = new ResponseModel<EducationRequestDetailModel>();
            try
            {
                data.DeleteWorkerFromEducationRequest(model);
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

        public ResponseModel<EducationRequestDetailModel> GetEducationRequest(UserInfo user, EducationRequestDetailModel referenceModel)
        {
            var response = new ResponseModel<EducationRequestDetailModel>();
            try
            {
                response.Model = data.GetEducationRequest(user, referenceModel);
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

        public ResponseModel<EducationRequestListModel> ListEducationRequests(UserInfo user, EducationRequestListModel filter, out int totalCount)
        {
            var response = new ResponseModel<EducationRequestListModel>();
            totalCount = 0;
            try
            {
                response.Data = data.ListEducationRequests(user, filter, out totalCount);
                response.Total = totalCount;
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

        public ResponseModel<SelectListItem> GetEducationList(UserInfo user)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.GetEducationList(user);
                response.Total = response.Data.Count;
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

        public ResponseModel<SelectListItem> GetWorkerList(UserInfo user)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.GetWorkerList(user);
                response.Total = response.Data.Count;
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
