using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSModel.LabourCountryVatRatio;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class LabourCountryVatRatioBL : BaseBusiness
    {
        private readonly LabourCountryVatRatioData data = new LabourCountryVatRatioData();

        public ResponseModel<LabourCountryVatRatioViewModel> DMLLabourCountryVatRatio(UserInfo user, LabourCountryVatRatioViewModel model)
        {
            var response = new ResponseModel<LabourCountryVatRatioViewModel>();
            try
            {
                data.DMLLabourCountryVatRatio(user, model);
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

        public ResponseModel<LabourCountryVatRatioListModel> ListLabourCountryVatRatios(UserInfo user, LabourCountryVatRatioListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<LabourCountryVatRatioListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListLabourCountryVatRatios(user, filter, out totalCnt);
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

        public ResponseModel<SelectListItem> ListLaboursBySubGroup(UserInfo user, int subGroupId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListLaboursBySubGroup(user, subGroupId);
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

        public ResponseModel<LabourCountryVatRatioViewModel> GetLabourCountryVatRatio(UserInfo user, int labourId, int countrId)
        {
            var response = new ResponseModel<LabourCountryVatRatioViewModel>();
            try
            {
                response.Model = data.GetLabourCountryVatRatio(user, countrId, labourId);
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
