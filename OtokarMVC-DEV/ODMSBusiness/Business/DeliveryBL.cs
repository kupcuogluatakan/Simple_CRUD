using System;
using System.Collections.Generic;
using ODMSData;
using ODMSModel;
using ODMSModel.Delivery;
using ODMSModel.DeliveryListPart;
using ODMSData.Utility;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class DeliveryBL : BaseBusiness
    {
        private readonly DeliveryData data = new DeliveryData();

        public ResponseModel<DeliveryListModel> ListDelivery(UserInfo user, DeliveryListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<DeliveryListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListDelivery(user, filter, out totalCnt);
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

        public ResponseModel<DeliveryCreateModel> DMLDelivery(UserInfo user, DeliveryCreateModel model)
        {
            var response = new ResponseModel<DeliveryCreateModel>();
            try
            {
                data.DMLDelivery(user, model);
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

        public ResponseModel<DeliveryCreateModel> DMLDeliveryAndDetails(UserInfo user, DeliveryCreateModel model, List<DeliveryListPartSubViewModel> filter)
        {
            var response = new ResponseModel<DeliveryCreateModel>();
            try
            {
                data.DMLDeliveryAndDetails(user, model, filter);
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

        public ResponseModel<DeliveryViewModel> GetDelivery(UserInfo user, long id)
        {
            var response = new ResponseModel<DeliveryViewModel>();
            try
            {
                response.Model = data.GetDelivery(user, id);
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

        public ResponseModel<ModelBase> CancelDelivery(UserInfo user, long deliveryId)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = data.CancelDelivery(user, deliveryId);
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

        public ResponseModel<DeliveryCreateModel> CheckDeleteItem(UserInfo user, DeliveryCreateModel model)
        {
            var response = new ResponseModel<DeliveryCreateModel>();
            try
            {
                response.Model = data.CheckDeteleItem(user, model);
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

        public ResponseModel<DeliveryCreateModel> DeleteDeliveryItem(DeliveryCreateModel model)
        {
            var response = new ResponseModel<DeliveryCreateModel>();
            try
            {
                data.DeleteDeliveryItem(model);
                response.Model = model;
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

        public ResponseModel<bool> Exists(long supplierId, string wayBillNo, int dealerId)
        {
            var response = new ResponseModel<bool>();
            try
            {
                response.Model = data.Exists(supplierId, wayBillNo, dealerId);
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
