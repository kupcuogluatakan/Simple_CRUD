using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSModel.CountryVatRatio;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class CountryBL : BaseBusiness
    {
        private readonly CountryData data = new CountryData();

        /// <summary>
        /// Dil koduna göre Ülke listesini verir.
        /// </summary>
        /// <param name="languageCode">Dil kodu</param>
        [BusinessCache(VersionControlClass = typeof(CountryData), VersionControlMethod = "ListCountryLastVersion")]
        public ResponseModel<CountryListModel> GetCountryList(string languageCode)
        {
            var response = new ResponseModel<CountryListModel>();
            try
            {
                response.Data = data.ListCountry(languageCode);
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

        [BusinessCache(DurationMunite = 1)]
        public ResponseModel<CountryListModel> GetCountryList_Duration(string languageCode)
        {
            var response = new ResponseModel<CountryListModel>();
            try
            {
                response.Data = data.ListCountry(languageCode);
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
