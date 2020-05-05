using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSModel.LabourTechnician;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class LabourTechnicianBL : BaseBusiness
    {
        private readonly LabourTechnicianData data = new LabourTechnicianData();

        public ResponseModel<LabourTechnicianViewModel> DMLLabourTechnician(UserInfo user, LabourTechnicianViewModel model)
        {
            var response = new ResponseModel<LabourTechnicianViewModel>();
            try
            {
                data.DMLLabourTechnician(user, model);
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

        public ResponseModel<LabourTechnicianViewModel> DMLLabourTechnicianStartFinish(LabourTechnicianViewModel model)
        {
            var response = new ResponseModel<LabourTechnicianViewModel>();
            try
            {
                data.DMLLabourTechnicianStartFinish(model);
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
        
        public ResponseModel<LabourTechnicianViewModel> GetLabourTechnician(UserInfo user, LabourTechnicianViewModel filter)
        {
            var response = new ResponseModel<LabourTechnicianViewModel>();
            try
            {
                response.Model = data.GetLabourTechnician(user, filter);
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

        public ResponseModel<LabourTechnicianViewModel> CancelLabourTecnician(UserInfo user, LabourTechnicianViewModel filter)
        {
            var response = new ResponseModel<LabourTechnicianViewModel>();
            try
            {
                response.Model = data.CancelLabourTecnician(user, filter);
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

        public ResponseModel<LabourTechnicianViewModel> GetLabourTechnicianStartFinish(LabourTechnicianViewModel filter)
        {
            var response = new ResponseModel<LabourTechnicianViewModel>();
            try
            {
                response.Data = data.GetLabourTechnicianStartFinish(filter);
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

        public ResponseModel<LabourTechnicianListModel> ListLabourTechnicians(UserInfo user, LabourTechnicianListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<LabourTechnicianListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListLabourTechnicians(user, filter, out totalCnt);
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

        public static ResponseModel<SelectListItem> ListTechnicianAsSelectList(int? dealerId)
        {
            var data = new LabourTechnicianData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListTechnicianAsSelectList(dealerId);
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

        public ResponseModel<LabourTecnicianUserModel> GetLabourTecnicianInfo(int labourTechnicianId)
        {
            var response = new ResponseModel<LabourTecnicianUserModel>();
            try
            {
                response.Data = data.GetLabourTecnicianInfo(labourTechnicianId);
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
