using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSModel.GuaranteeRequestApprove;
using System.Collections.Generic;

namespace ODMSBusiness
{
    public class GuaranteeRequestApproveBL : BaseBusiness
    {
        private readonly GuaranteeRequestApproveData data = new GuaranteeRequestApproveData();

        [BusinessLog]
        public ResponseModel<GuaranteeRequestApproveListModel> ListGuaranteeRequestApprove(UserInfo user, GuaranteeRequestApproveListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<GuaranteeRequestApproveListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListGuaranteeRequestApprove(user, filter, out totalCnt);
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

    }
}
