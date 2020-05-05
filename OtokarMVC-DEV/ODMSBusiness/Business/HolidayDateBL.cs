using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSModel.HolidayDate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSBusiness.Business
{
    public class HolidayDateBL : BaseBusiness
    {
        private readonly HolidayDateData data = new HolidayDateData();
        public HolidayDateBL()
        {
        }

        public ResponseModel<HolidayDateListModel> ListHolidayDate(UserInfo user, HolidayDateListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<HolidayDateListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListHolidayDate(user, filter, out totalCnt);
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

        public ResponseModel<HolidayDateViewModel> GetHolidayDate(UserInfo user, HolidayDateViewModel filter)
        {
            var response = new ResponseModel<HolidayDateViewModel>();
            try
            {
                response.Model = data.GetHolidayDate(user, filter);
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

        public ResponseModel<HolidayDateViewModel> DMLHolidayDate(UserInfo user, HolidayDateViewModel model)
        {
            var response = new ResponseModel<HolidayDateViewModel>();
            try
            {
                data.DMLHolidayDate(user, model);
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
