using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSModel.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSBusiness.Business
{
    public interface IIntegrationBL
    {
        ResponseModel<IntegrationListModel> GetIntegrationList(UserInfo user,IntegrationListModel filter);

        ResponseModel<IntegrationDetailListModel> GetIntegrationDetailList(UserInfo user,IntegrationDetailListModel filter);
    }

    public class IntegrationBL : BaseBusiness, IIntegrationBL
    {
        private readonly IntegrationData data = new IntegrationData();

        public ResponseModel<IntegrationListModel> GetIntegrationList(UserInfo user,IntegrationListModel filter)
        {
            var response = new ResponseModel<IntegrationListModel>();
            try
            {
                response.Data = data.GetIntegrationList(user,filter);
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

        

        public ResponseModel<IntegrationDetailListModel> GetIntegrationDetailList(UserInfo user,IntegrationDetailListModel filter)
        {
            var response = new ResponseModel<IntegrationDetailListModel>();
            try
            {
                response.Data = data.GetIntegrationDetailList(user,filter);
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
