using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSModel.TechnicianOperationControl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ODMSBusiness
{
    public class TechnicianOperationControlBL : BaseService<TechnicianOperationViewModel>
    {

        private TechnicianOperationControlData data = new TechnicianOperationControlData();

        public new ResponseModel<TechnicianOperationViewModel> Get(UserInfo user, TechnicianOperationViewModel filter)
        {
            var response = new ResponseModel<TechnicianOperationViewModel>();
            try
            {
                response.Model = data.Get(user, filter);
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

        public ResponseModel<TechnicianOperationListModel> List(UserInfo user, TechnicianOperationListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<TechnicianOperationListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.List(user, filter, out totalCnt).ToList();
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

        public new ResponseModel<TechnicianOperationViewModel> Insert(UserInfo user, TechnicianOperationViewModel model)
        {
            var response = new ResponseModel<TechnicianOperationViewModel>();
            try
            {
                if (model.ProcessType == ProcessType.CheckIn)
                    model.CheckInDate = DateTime.Now;
                else
                    model.CheckOutDate = DateTime.Now;

                data.Insert(user, model);

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
