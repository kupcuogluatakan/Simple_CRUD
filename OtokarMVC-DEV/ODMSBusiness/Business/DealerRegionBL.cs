using System.Collections.Generic;
using ODMSData;
using ODMSModel.DealerRegion;
using ODMSModel.ListModel;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class DealerRegionBL : BaseBusiness
    {
        private readonly DealerRegionData data = new DealerRegionData();

        public ResponseModel<DealerRegionListModel> ListDealerRegions(UserInfo user,DealerRegionListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<DealerRegionListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListDealerRegions(user,filter, out totalCnt);
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

        public ResponseModel<DealerRegionIndexViewModel> GetDealerRegion(UserInfo user, DealerRegionIndexViewModel filter)
        {
            var response = new ResponseModel<DealerRegionIndexViewModel>();
            try
            {
                response.Model = data.GetDealerRegion(user, filter);
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

        public ResponseModel<DealerRegionIndexViewModel> DMLDealerRegion(UserInfo user, DealerRegionIndexViewModel model)
        {
            var response = new ResponseModel<DealerRegionIndexViewModel>();
            try
            {
                data.DMLDealerRegion(user, model);
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
