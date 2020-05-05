using System.Collections.Generic;
using ODMSModel.DealerGuaranteeControl;
using ODMSData;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class DealerGuaranteeControlBL : BaseBusiness
    {
        private readonly DealerGuaranteeControlListData data = new DealerGuaranteeControlListData();

        public ResponseModel<DealerGuaranteeControlListModel> ListGuaranteeRequests(UserInfo user, DealerGuaranteeControlListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<DealerGuaranteeControlListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListDealerGuaranteeControl(user, filter, out totalCnt);
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
