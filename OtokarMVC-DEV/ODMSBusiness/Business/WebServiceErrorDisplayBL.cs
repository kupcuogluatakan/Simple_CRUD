using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSModel.WebServiceErrorDisplay;

namespace ODMSBusiness
{
    public class WebServiceErrorDisplayBL : BaseService<WebServiceErrorDisplayViewModel>
    {
        private readonly WebServiceErrorDisplayData data = new WebServiceErrorDisplayData();

        public override ResponseModel<WebServiceErrorDisplayViewModel> Get(UserInfo user,WebServiceErrorDisplayViewModel filter)
        {
            var response = new ResponseModel<WebServiceErrorDisplayViewModel>();
            try
            {
                response.Model = data.Get(filter);
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

        public ResponseModel<WebServiceErrorDisplayViewModel> GetDetail(WebServiceErrorDisplayViewModel filter, XmlErrorType type)
        {
            var response = new ResponseModel<WebServiceErrorDisplayViewModel>();
            try
            {
                response.Model = data.GetDetail(filter, type);
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
