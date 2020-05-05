using System.Collections.Generic;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.PurchaseOrderType;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class PurchaseOrderTypeBL : BaseService<PurchaseOrderTypeViewModel>
    {
        private readonly PurchaseOrderTypeData data = new PurchaseOrderTypeData();

        public ResponseModel<PurchaseOrderTypeListModel> List(UserInfo user, PurchaseOrderTypeListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<PurchaseOrderTypeListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.List(user, filter, out totalCnt);
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

        public ResponseModel<PurchaseOrderTypeViewModel> Get(UserInfo user, PurchaseOrderTypeViewModel filter)
        {
            var response = new ResponseModel<PurchaseOrderTypeViewModel>();
            try
            {
                response.Model = data.Get(user, filter);
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

        public ResponseModel<PurchaseOrderTypeViewModel> Insert(UserInfo user, PurchaseOrderTypeViewModel model)
        {
            var response = new ResponseModel<PurchaseOrderTypeViewModel>();
            try
            {
                data.Insert(user, model);
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

        public ResponseModel<PurchaseOrderTypeViewModel> Update(UserInfo user, PurchaseOrderTypeViewModel model)
        {
            var response = new ResponseModel<PurchaseOrderTypeViewModel>();
            try
            {
                data.Update(user, model);
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

        public ResponseModel<PurchaseOrderTypeViewModel> Delete(UserInfo user, PurchaseOrderTypeViewModel model)
        {
            var response = new ResponseModel<PurchaseOrderTypeViewModel>();
            try
            {
                data.Delete(user, model);
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

        public static ResponseModel<SelectListItem> ListPurchaseOrderTypeAsSelectListItem(UserInfo user, int? dealerId, bool? dealerOrderAllow, bool? supplierOrderAllow, bool? firmOrderAllow, bool? saleOrderAllow)
        {
            var data = new PurchaseOrderTypeData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListPurchaseOrderTypeAsSelectListItem(user, dealerId, dealerOrderAllow, supplierOrderAllow, firmOrderAllow, saleOrderAllow);
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
        /// <summary>
        /// Lists all purchase order types including passives
        /// </summary>
        public ResponseModel<SelectListItem> PurchaseOrderTypeList(UserInfo user)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.PurchaseOrderTypeList(user);
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

