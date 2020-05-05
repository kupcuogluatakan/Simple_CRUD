using System.Collections.Generic;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.AppointmentDetails;
using ODMSModel.Rack;
using ODMSCommon.Security;
using System.Diagnostics;

namespace ODMSBusiness
{
    public class RackBL : BaseBusiness
    {
        private readonly RackData data = new RackData();
        public ResponseModel<RackIndexModel> GetRackIndexModel(int dealerId)
        {
            var response = new ResponseModel<RackIndexModel>();
            try
            {
                response.Model = data.GetRackIndexModel(dealerId);
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

        public ResponseModel<SelectListItem> ListWarehousesOfDealer(int dealerId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListWarehousesOfDealer(dealerId);
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

        public ResponseModel<RackListModel> ListRacks(RackListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<RackListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListRacks(filter, out totalCnt);
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

        public ResponseModel<RackDetailModel> GetRack(RackDetailModel filter)
        {
            var response = new ResponseModel<RackDetailModel>();
            try
            {
                response.Model = data.GetRack(filter);
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

        public ResponseModel<RackDetailModel> DMLRack(UserInfo user,RackDetailModel model)
        {
            var response = new ResponseModel<RackDetailModel>();
            try
            {
                data.DMLRack(user, model);
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
