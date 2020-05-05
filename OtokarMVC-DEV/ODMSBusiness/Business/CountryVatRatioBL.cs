using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSModel.CountryVatRatio;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class CountryVatRatioBL : BaseBusiness
    {
        private readonly CountryVatRatioData data = new CountryVatRatioData();

        public ResponseModel<CountryVatRatioListModel> ListCountryVatRatios(UserInfo user,CountryVatRatioListModel model, out int totalCnt)
        {
            var response = new ResponseModel<CountryVatRatioListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListCountryVatRatios(user,model, out totalCnt);
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

        public ResponseModel<CountryVatRatioViewModel> DMLCountryVatRatio(UserInfo user, CountryVatRatioViewModel model)
        {
            var response = new ResponseModel<CountryVatRatioViewModel>();
            try
            {
                data.DMLCountryVatRatio(user, model);
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

        public ResponseModel<SelectListItem> GetVatRatioCountries(UserInfo user)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.GetVatRatioCountries(user);
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

        public ResponseModel<CountryVatRatioViewModel> GetCountryVatRatio(int countryId)
        {
            var response = new ResponseModel<CountryVatRatioViewModel>();
            try
            {
                response.Model = data.GetCountryVatRatio(countryId);
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

        public ResponseModel<decimal> GetVatRatioByPartAndCountry(int partId, int? countryId)
        {
            var response = new ResponseModel<decimal>();
            try
            {
                response.Model = data.GetVatRatioByPartAndCountry(partId, countryId);
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
