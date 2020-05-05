using System.Collections.Generic;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSCommon.Security;
using ODMSModel.AppointmentDetails;
using ODMSModel.PriceListCountryMatch;

namespace ODMSBusiness
{
    public class PriceListCountryMatchBL : BaseBusiness
    {
        private readonly PriceListCountryMatchData data = new PriceListCountryMatchData();
        public ResponseModel<SelectListItem> GetPriceLists()
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.GetPriceList();
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

        public ResponseModel<SelectListItem> GetCountriesIncluded(UserInfo user,int priceListId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.GetCountriesIncluded(user,priceListId);
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

        public ResponseModel<SelectListItem> GetCountriesExcluded(UserInfo user, int priceListId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.GetCountriesExcluded(user, priceListId);
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
        /// save current list and returs countries that has no default price list 
        /// </summary>
        /// <param name="model"></param>

        public ResponseModel<PriceListCountryMatchSaveModel> Save(UserInfo user,PriceListCountryMatchSaveModel model)
        {
            var response = new ResponseModel<PriceListCountryMatchSaveModel>();
            try
            {
                data.Save(user, model);
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
