using System.Collections.Generic;
using ODMSBusiness.Reports;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.CriticalStockCard;
using ODMSModel.CycleCount;
using ODMSModel.CycleCountPlan;
using ODMSModel.StockCard;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class CycleCountBL : BaseBusiness
    {
        private readonly CycleCountData data = new CycleCountData();
        private readonly StockCardBL dataStockCard = new StockCardBL();

        public ResponseModel<CycleCountViewModel> GetCycleCount(UserInfo user, CycleCountViewModel filter)
        {
            var response = new ResponseModel<CycleCountViewModel>();
            try
            {
                response.Model = data.GetCycleCount(user, filter);
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

        public ResponseModel<CycleCountViewModel> DMLCycleCount(UserInfo user, CycleCountViewModel model)
        {
            var response = new ResponseModel<CycleCountViewModel>();
            try
            {
                data.DMLCycleCount(user, model);
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

        public ResponseModel<CycleCountViewModel> StartCycleCount(UserInfo user, CycleCountViewModel filter)
        {
            var response = new ResponseModel<CycleCountViewModel>();
            try
            {
                data.StartCycleCount(user, filter);
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

        public ResponseModel<CycleCountViewModel> ApproveCycleCount(UserInfo user, CycleCountViewModel filter)
        {
            var response = new ResponseModel<CycleCountViewModel>();
            try
            {
                data.ApproveCycleCount(user, filter);
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

        public ResponseModel<CycleCountViewModel> ListCycleCount(UserInfo user, CycleCountListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<CycleCountViewModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListCycleCount(user, filter, out totalCnt);
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

        public byte[] GetCycleCountReport(int cycleCountId)
        {
            return ReportManager.GetReport(ReportType.CycleCountReport, cycleCountId);
        }

        public ResponseModel<List<CycleCountPlanViewModel>> LockRack(List<CycleCountPlanViewModel> filter, LockType type)
        {
            var response = new ResponseModel<List<CycleCountPlanViewModel>>();
            foreach (var item in filter)
            {
                try
                {
                    int partId = item.StockCardId ?? 0;
                    if (partId > 0)
                    {
                        partId = dataStockCard.GetStockCardById(new StockCardViewModel() { StockCardId = item.StockCardId.Value }).Model.PartId.Value;
                    }

                    item.Type = type;
                    item.PartId = partId;
                    data.LockRack(item);

                    response.Model = filter;
                    response.Message = MessageResource.Global_Display_Success;
                }
                catch (System.Exception ex)
                {
                    response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                    response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
                    break;
                }
            }

            return response;
        }

    }
}
