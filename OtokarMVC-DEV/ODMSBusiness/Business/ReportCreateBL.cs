﻿using System.Collections.Generic;
using ODMSData;
using ODMSModel.Bank;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.Reports;

namespace ODMSBusiness
{
    public class ReportCreateBL : BaseBusiness
    {
        private readonly ReportCreateData data = new ReportCreateData();

        public ResponseModel<ReportCreateModel> GetAllReportCreate(ReportCreateModel filter, out int totalCnt)
        {
            var response = new ResponseModel<ReportCreateModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetAllReportCreate(filter, out totalCnt);
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

        public ResponseModel<ReportCreateModel> DMLReportCreate(ReportCreateModel model)
        {
            var response = new ResponseModel<ReportCreateModel>();
            try
            {
                data.DMLReportCreate(model);
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
