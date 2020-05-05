using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSModel;
using ODMSModel.DealerAccountInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSBusiness.Business
{
    public class DealerAccountInfoBL : BaseBusiness
    {
        private readonly DealerAccountInfoData data = new DealerAccountInfoData();

        public ResponseModel<DealerAccountListModel> List(UserInfo user, int? dealerAccountInfoId)
        {
            var response = new ResponseModel<DealerAccountListModel>();
            try
            {
                response.Data = data.List(user, dealerAccountInfoId);
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

        public ResponseModel<DealerAccountListModel> DealerAccountInfoDml(UserInfo user, DealerAccountListModel model)
        {
            var response = new ResponseModel<DealerAccountListModel>();
            try
            {
                data.DMLDealerAccountInfo(user, model);
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
