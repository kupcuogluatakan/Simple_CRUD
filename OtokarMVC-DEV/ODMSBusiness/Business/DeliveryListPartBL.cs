using System.Collections.Generic;
using ODMSData;
using ODMSModel.DeliveryListPart;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class DeliveryListPartBL : BaseBusiness
    {
        private readonly DeliveryListPartData data = new DeliveryListPartData();

        public ResponseModel<DeliveryListPartListModel> ListDeliveryListPart(UserInfo user,DeliveryListPartListModel filter, out int totalCount)
        {
            var response = new ResponseModel<DeliveryListPartListModel>();
            totalCount = 0;
            try
            {
                response.Data = data.ListDeliveryListPart(user,filter, out totalCount);
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

        public ResponseModel<DeliveryListPartViewModel> DMLDeliveryListPart(UserInfo user, DeliveryListPartViewModel model)
        {
            var response = new ResponseModel<DeliveryListPartViewModel>();
            try
            {
                data.DMLDeliveryListPart(user, model);
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

        public ResponseModel<DeliveryListPartSubViewModel> DMLDeliveryListDetail(UserInfo user, DeliveryListPartSubViewModel model)
        {
            var response = new ResponseModel<DeliveryListPartSubViewModel>();
            try
            {
                data.DMLDeliveryListDetail(user, model);
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

        public ResponseModel<DeliveryListPartViewModel> CompleteDeliveryListPart(UserInfo user, DeliveryListPartViewModel model)
        {
            var response = new ResponseModel<DeliveryListPartViewModel>();
            try
            {
                data.CompleteDeliveryListPart(user, model);
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
