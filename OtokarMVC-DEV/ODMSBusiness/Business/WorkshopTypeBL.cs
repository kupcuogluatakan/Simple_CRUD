using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSModel.WorkshopType;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class WorkshopTypeBL : BaseBusiness
    {
        private readonly WorkshopTypeData data = new WorkshopTypeData();

        public ResponseModel<WorkshopTypeListModel> GetListWorkshopType(UserInfo user, WorkshopTypeListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<WorkshopTypeListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetListWorkshopType(user, filter, out totalCnt);
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

        public ResponseModel<WorkshopTypeViewModel> DMLWorkshopType(UserInfo user, WorkshopTypeViewModel model)
        {
            var response = new ResponseModel<WorkshopTypeViewModel>();
            try
            {
                data.DMLWorkshopType(user, model);
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

        public ResponseModel<WorkshopTypeViewModel> GetWorkshopType(UserInfo user, WorkshopTypeViewModel filter)
        {
            var response = new ResponseModel<WorkshopTypeViewModel>();
            try
            {
                data.GetWorkshopType(user, filter);
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

        public static ResponseModel<SelectListItem> ListWorkshopTypeAsSelectList(UserInfo user)
        {
            var data = new WorkshopTypeData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListWorkshopTypeAsSelectList(user);
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
