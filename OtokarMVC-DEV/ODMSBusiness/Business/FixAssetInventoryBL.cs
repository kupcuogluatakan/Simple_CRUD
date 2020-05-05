using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSModel.FixAssetInventory;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class FixAssetInventoryBL : BaseBusiness
    {
        private readonly FixAssetInventoryData data = new FixAssetInventoryData();

        public ResponseModel<FixAssetInventoryListModel> ListFixAssetInventorys(UserInfo user, FixAssetInventoryListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<FixAssetInventoryListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListFixAssetInventory(user, filter, out totalCnt);
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

        public ResponseModel<FixAssetInventoryViewModel> GetFixAssetInventory(UserInfo user, FixAssetInventoryViewModel filter)
        {
            var response = new ResponseModel<FixAssetInventoryViewModel>();
            try
            {
                response.Model = data.GetFixAssetInventory(user, filter);
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

        public ResponseModel<FixAssetInventoryViewModel> DMLFixAssetInventory(UserInfo user, FixAssetInventoryViewModel model)
        {
            var response = new ResponseModel<FixAssetInventoryViewModel>();
            try
            {
                data.DMLFixAssetInventory(user, model);
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

        public static ResponseModel<SelectListItem> ListEquipmentTypeAsSelectList(UserInfo user)
        {
            FixAssetInventoryData data = new FixAssetInventoryData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListEquipmentTypeAsSelectList(user);
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
