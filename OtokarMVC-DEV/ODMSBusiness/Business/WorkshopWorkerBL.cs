using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSModel.WorkshopWorker;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class WorkshopWorkerBL : BaseBusiness
    {
        private readonly WorkshopWorkerData data = new WorkshopWorkerData();

        public ResponseModel<WorkshopWorkerIndexModel> GetWorkshopWorkerIndexModel()
        {
            var response = new ResponseModel<WorkshopWorkerIndexModel>();
            try
            {
                response.Model = data.GetWorkshopWorkerIndexModel();
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

        public ResponseModel<WorkshopWorkerListModel> ListWorkshopWorkers(WorkshopWorkerListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<WorkshopWorkerListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListWorkshopWorkers(filter, out totalCnt);
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

        public ResponseModel<WorkshopWorkerDetailModel> DMLWorkshopWorker(UserInfo user, WorkshopWorkerDetailModel model)
        {
            var response = new ResponseModel<WorkshopWorkerDetailModel>();
            try
            {
                data.DMLWorkshopWorker(user, model);
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

        public ResponseModel<SelectListItem> GetWorkshopList()
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.GetWorkshopList();
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

        public ResponseModel<SelectListItem> GetWorkerList()
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.GetWorkerList();
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

        public ResponseModel<WorkshopWorkerDetailModel> GetWorkshopWorker(WorkshopWorkerDetailModel filter)
        {
            var response = new ResponseModel<WorkshopWorkerDetailModel>();
            try
            {
                response.Model = data.GetWorkshopWorker(filter);
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
