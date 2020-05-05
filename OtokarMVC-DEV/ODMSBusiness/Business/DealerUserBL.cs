using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSModel.User;

namespace ODMSBusiness
{
    public class DealerUserBL : BaseBusiness
    {
        private readonly DealerUserData data = new DealerUserData();

        public ResponseModel<UserIndexViewModel> SetUserInactive(UserInfo user, UserIndexViewModel model)
        {
            var response = new ResponseModel<UserIndexViewModel>();
            try
            {
                data.SetUserInactive(user, model);
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
