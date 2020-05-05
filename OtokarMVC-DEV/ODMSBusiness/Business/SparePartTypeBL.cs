using System.Collections.Generic;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.ListModel;
using ODMSModel.SparePartType;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class SparePartTypeBL : BaseBusiness
    {
        private readonly SparePartTypeData data = new SparePartTypeData();

        public static ResponseModel<SelectListItem> ListSparePartTypeAsSelectListItem(UserInfo user)
        {
            var data = new SparePartTypeData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListSparePartTypeAsSelectListItem(user);
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
        public ResponseModel<SparePartTypeListModel> ListSparePartTypes(UserInfo user, SparePartTypeListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<SparePartTypeListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListSparePartType(user,filter, out totalCnt);
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
        public ResponseModel<SparePartTypeIndexViewModel> GetSparePartType(UserInfo user, SparePartTypeIndexViewModel filter)
        {
            var response = new ResponseModel<SparePartTypeIndexViewModel>();
            try
            {
                response.Model = data.GetSparePartType(user, filter);
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
        public ResponseModel<SparePartTypeIndexViewModel> DMLSparePartType(UserInfo user, SparePartTypeIndexViewModel model)
        {
            var response = new ResponseModel<SparePartTypeIndexViewModel>();
            try
            {
                data.DMLSparePartType(user, model);
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
