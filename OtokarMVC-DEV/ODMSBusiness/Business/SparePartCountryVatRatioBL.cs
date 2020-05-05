using ODMSCommon.Security;
using ODMSData;
using ODMSModel.SparePartCountryVatRatio;
using System.Collections.Generic;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class SparePartCountryVatRatioBL : BaseBusiness
    {
        private readonly SparePartCountryVatRatioData data = new SparePartCountryVatRatioData();

        public ResponseModel<SparePartCountryVatRatioListModel> ListSparePartCountryVatRatio(UserInfo user,SparePartCountryVatRatioListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<SparePartCountryVatRatioListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListSparePartCountryVatRatio(user,filter, out totalCnt);
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
        public ResponseModel<SparePartCountryVatRatioViewModel> GetSparePartCountryVatRatio(UserInfo user,SparePartCountryVatRatioViewModel filter)
        {
            var response = new ResponseModel<SparePartCountryVatRatioViewModel>();
            try
            {
                response.Model = data.GetSparePartCountryVatRatio(user, filter);
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
        public ResponseModel<SparePartCountryVatRatioViewModel> DMLSparePartCountryVatRatio(UserInfo user,SparePartCountryVatRatioViewModel filter)
        {
            var response = new ResponseModel<SparePartCountryVatRatioViewModel>();
            try
            {
                data.DMLSparePartCountryVatRatio(user, filter);
                response.Model = filter;
                response.Message = MessageResource.Global_Display_Success;
                if (filter.ErrorNo > 0)
                    throw new System.Exception(filter.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public static ResponseModel<SelectListItem> ListCountryNameAsSelectListItem(UserInfo user)
        {
            var data = new SparePartCountryVatRatioData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListCountryNameAsSelectListItem(user);
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
