using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSModel.Warehouse;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class WarehouseBL : BaseBusiness
    {
        private readonly WarehouseData data = new WarehouseData();
        public ResponseModel<WarehouseListModel> ListWarehouses(UserInfo user, WarehouseListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<WarehouseListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListWarehouses(user, filter, out totalCnt);
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

        public ResponseModel<WarehouseDetailModel> GetWarehouse(UserInfo user, WarehouseDetailModel filter)
        {
            var response = new ResponseModel<WarehouseDetailModel>();
            try
            {
                response.Model = data.GetWarehouse(user, filter);
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

        public ResponseModel<WarehouseDetailModel> DMLWarehouse(UserInfo user, WarehouseDetailModel model)
        {
            var response = new ResponseModel<WarehouseDetailModel>();
            try
            {
                data.DMLWarehouse(user, model);
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

        public ResponseModel<WarehouseIndexModel> GetWarehouseIndexModel(int dealerId)
        {
            var response = new ResponseModel<WarehouseIndexModel>();
            try
            {
                response.Model = data.GetWarehouseIndexModel(dealerId); ;
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

        public static ResponseModel<SelectListItem> ListWarehousesOfDealerAsSelectList(int? dealerId)
        {
            var data = new WarehouseData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListWarehousesOfDealerAsSelectList(dealerId);
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
