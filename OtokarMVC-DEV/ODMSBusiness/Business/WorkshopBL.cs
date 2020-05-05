using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSModel.Workshop;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;
using System.Linq;

namespace ODMSBusiness
{
    public class WorkshopBL : BaseBusiness
    {
        private readonly WorkshopData data = new WorkshopData();

        public ResponseModel<WorkshopIndexModel> GetWorkshopIndexModel()
        {
            var response = new ResponseModel<WorkshopIndexModel>();
            try
            {
                response.Model = data.GetWorkshopIndexModel();
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

        public ResponseModel<WorkshopDetailModel> DMLWorkshop(UserInfo user, WorkshopDetailModel model)
        {
            var response = new ResponseModel<WorkshopDetailModel>();
            try
            {
                data.DMLWorkshop(user, model);
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

        public ResponseModel<WorkshopDetailModel> GetWorkshop(WorkshopDetailModel filter)
        {
            var response = new ResponseModel<WorkshopDetailModel>();
            try
            {
                response.Model = data.GetWorkshop(filter);
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

        public ResponseModel<WorkshopListModel> ListWorkshops(WorkshopListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<WorkshopListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListWorkshops(filter, out totalCnt);
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

        public ResponseModel<SelectListItem> GetDealerList()
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.GetDealerList().ToList();
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
