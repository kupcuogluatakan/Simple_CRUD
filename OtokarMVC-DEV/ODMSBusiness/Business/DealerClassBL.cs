using System.Web.Mvc;
using ODMSData;
using ODMSModel.DealerClass;
using System.Collections.Generic;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class DealerClassBL : BaseBusiness
    {
        private readonly DealerClassData data = new DealerClassData();

        public ResponseModel<DealerClassListModel> ListDealerClass(UserInfo user,DealerClassListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<DealerClassListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListDealerClass(user,filter, out totalCnt);
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

        public ResponseModel<DealerClassViewModel> GetDealerClass(UserInfo user, DealerClassViewModel filter)
        {
            var response = new ResponseModel<DealerClassViewModel>();
            try
            {
                response.Model = data.GetDealerClass(user, filter);
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

        public ResponseModel<DealerClassViewModel> DMLDealerClass(UserInfo user, DealerClassViewModel model)
        {
            var response = new ResponseModel<DealerClassViewModel>();
            try
            {
                data.DMLDealerClass(user, model);
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

        public static ResponseModel<SelectListItem> ListDealerClassesAsSelectListItem()
        {
            var response = new ResponseModel<SelectListItem>();
            var data = new DealerClassData();
            try
            {
                response.Data = data.ListDealerClassesAsSelectListItem();
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
