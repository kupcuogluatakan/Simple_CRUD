using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSModel.DeliveryGoodsPlacement;
using ODMSModel.DeliveryListPart;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class DeliveryGoodsPlacementBL : BaseBusiness
    {
        private readonly DeliveryGoodsPlacementData data = new DeliveryGoodsPlacementData();

        public ResponseModel<DeliveryGoodsPlacementListModel> ListDeliveryGoodsPlacement(UserInfo user,DeliveryGoodsPlacementListModel filter, out int totalCount)
        {
            var response = new ResponseModel<DeliveryGoodsPlacementListModel>();
            totalCount = 0;
            try
            {
                response.Data = data.ListDeliveryGoodsPlacement(user,filter, out totalCount);
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

        public ResponseModel<PartsPlacementListModel> ListPartsPlacement(UserInfo user, PartsPlacementListModel filter, out int totalCount)
        {
            var response = new ResponseModel<PartsPlacementListModel>();
            totalCount = 0;
            try
            {
                response.Data = data.ListPartsPlacement(user, filter, out totalCount);
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

        public ResponseModel<SelectListItem> ListRackWarehouseByDetId(UserInfo user, int id)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListRackWarehouseByDetId(user, id);
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

        public ResponseModel<PartsPlacementViewModel> DeletePartsPlacement(UserInfo user, PartsPlacementViewModel model)
        {
            var response = new ResponseModel<PartsPlacementViewModel>();
            try
            {
                data.DeletePartsPlacement(user, model);
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

        public ResponseModel<PartsPlacementViewModel> DMLPartsPlacement(UserInfo user, PartsPlacementViewModel model)
        {
            var response = new ResponseModel<PartsPlacementViewModel>();
            try
            {
                data.DMLPartsPlacement(user, model);
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

        public ResponseModel<DeliveryListPartSubViewModel> CompleteDeliveryGoodsPlacement(UserInfo user, DeliveryListPartSubViewModel model)
        {
            var response = new ResponseModel<DeliveryListPartSubViewModel>();
            try
            {
                data.CompleteDeliveryGoodsPlacement(user, model);
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

        public ResponseModel<DeliveryListPartSubViewModel> DMLPartsPlacemntDefault(UserInfo user, DeliveryListPartSubViewModel model)
        {
            var response = new ResponseModel<DeliveryListPartSubViewModel>();
            try
            {
                data.DMLPartsPlacementDefault(user, model);
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

        public ResponseModel<DeliveryListPartSubViewModel> DoReverseTransaction(UserInfo user, DeliveryListPartSubViewModel model)
        {
            var response = new ResponseModel<DeliveryListPartSubViewModel>();
            try
            {
                data.DoReverseTransaction(user, model);
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
