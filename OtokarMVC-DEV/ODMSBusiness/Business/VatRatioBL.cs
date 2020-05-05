using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSModel.VatRatio;
using ODMSData.Utility;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class VatRatioBL : BaseBusiness
    {
        private readonly VatRatioData data = new VatRatioData();

        public ResponseModel<VatRatioListModel> ListVatRatios(UserInfo user, VatRatioListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<VatRatioListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListVatRatios(user, filter, out totalCnt);
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

        public ResponseModel<VatRatioModel> DMLVatRatio(UserInfo user, VatRatioModel model)
        {
            var response = new ResponseModel<VatRatioModel>();
            try
            {
                data.DMLVatRatio(user, model);
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

        public ResponseModel<VatRatioExpModel> DMLVatRatioExp(UserInfo user, VatRatioExpModel model)
        {
            var response = new ResponseModel<VatRatioExpModel>();
            try
            {
                data.DMLVatRatioExp(user, model);
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

        public ResponseModel<VatRatioExpModel> GetVatRatioExp(UserInfo user, decimal vatRatio, int countryId)
        {
            var response = new ResponseModel<VatRatioExpModel>();
            try
            {
                response.Model = data.GetVatRatioExp(user, vatRatio, countryId);
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

        public ResponseModel<VatRatioExpListModel> ListVatRatioExps(UserInfo user, VatRatioExpListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<VatRatioExpListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListVatRatioExps(user, filter, out totalCnt);
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

        public static ResponseModel<SelectListItem> ListLabelsAsSelectList()
        {
            var data = new VatRatioData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListLabelsAsSelectList();
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

        public static ResponseModel<SelectListItem> ListVatRatioAsSelectList(int sparePartId, int countryId)
        {
            var data = new VatRatioData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListVatRatiosAsSelectList(sparePartId, countryId);
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
