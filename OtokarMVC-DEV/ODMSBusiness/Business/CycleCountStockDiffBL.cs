using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSData;
using ODMSData.Utility;
using ODMSModel.CycleCountResult;
using ODMSModel.CycleCountStockDiff;
using ODMSCommon.Resources;
using ODMSModel.CycleCount;

namespace ODMSBusiness
{
    public class CycleCountStockDiffBL : BaseBusiness
    {
        private readonly CycleCountStockDiffData data = new CycleCountStockDiffData();
        private readonly StockTypeDetailData dataStockTypeDetail = new StockTypeDetailData();
        

        public ResponseModel<CycleCountStockDiffListModel> ListCycleCountStockDiffs(UserInfo user,CycleCountStockDiffListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<CycleCountStockDiffListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListCycleCountStockDiffs(user,filter, out totalCnt);
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

        public ResponseModel<CycleCountStockDiffSearchListModel> SearchCycleCountStockDiffs(UserInfo user, CycleCountStockDiffSearchListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<CycleCountStockDiffSearchListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.SearchCycleCountStockDiffs(user, filter, out totalCnt);
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

        public ResponseModel<CycleCountStockDiffViewModel> DMLCycleCountStockDiff(UserInfo user, CycleCountStockDiffViewModel model)
        {
            var response = new ResponseModel<CycleCountStockDiffViewModel>();
            try
            {
                data.DMLCycleCountStockDiff(user, model);
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

        public ResponseModel<CycleCountResultAuditViewModel> DMLCycleCountStockDiff(UserInfo user, CycleCountResultAuditViewModel model)
        {
            var response = new ResponseModel<CycleCountResultAuditViewModel>();
            try
            {
                data.DMLCycleCountStockDiff(user, model);
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

        public ResponseModel<decimal> GetStockTypeDetailQuantity(UserInfo user, CycleCountStockDiffViewModel filter)
        {
            var response = new ResponseModel<decimal>();
            try
            {
                CycleCountBL cycleCountBo = new CycleCountBL();
                CycleCountViewModel cycleModel = new CycleCountViewModel();
                cycleModel.CycleCountId = filter.CycleCountId.ToString();
                cycleModel = cycleCountBo.GetCycleCount(user, cycleModel).Model;
                response.Model = dataStockTypeDetail.GetStockTypeDetailQuantity(user,filter.StockCardId.GetValue<int>(), filter.WarehouseId.GetValue<int>(), cycleModel.StockTypeId.GetValue<int>());
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

        public ResponseModel<Dictionary<bool, decimal>> Exists(int cycleCountId, int stockCardId, int warehouseId)
        {
            var response = new ResponseModel<Dictionary<bool, decimal>>();
            try
            {
                response.Model = data.Exists(cycleCountId, stockCardId, warehouseId);
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

        public ResponseModel<CycleCountResultPrototypeViewModel> ListStokTypeAudit(UserInfo user, CycleCountResultAuditViewModel filter)
        {
            var response = new ResponseModel<CycleCountResultPrototypeViewModel>();
            try
            {
                response.Data = data.ListStokTypeAudit(user, filter);
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

        public ResponseModel<CycleCountStockDiffViewModel> ApproveCycleCountProcess(UserInfo user, CycleCountStockDiffViewModel filter)
        {
            var response = new ResponseModel<CycleCountStockDiffViewModel>();
            try
            {
                data.ApproveCycleCountProcess(user, filter);
                response.Model = filter;
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

        public ResponseModel<decimal> GetCycleCountStockDiffTotalQuantity(int warehouseId, int stockCardId, int cycleCountId)
        {
            var response = new ResponseModel<decimal>();
            try
            {
                response.Model = data.GetCycleCountStockDiffTotalQuantity(warehouseId, stockCardId, cycleCountId);
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
