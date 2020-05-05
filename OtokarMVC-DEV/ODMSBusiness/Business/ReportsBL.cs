using System;
using System.Collections.Generic;
using ODMSBusiness.Reports.Web;
using ODMSCommon.Security;
using ODMSData;
using ODMSData.Utility;
using ODMSModel.Reports;
using System.Web.Mvc;
using System.Data;
using System.Transactions;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.AppointmentIndicatorFailureCode;

namespace ODMSBusiness
{

    public class ReportsBL : BaseBusiness
    {
        private readonly ReportsData data = new ReportsData();

        [BusinessLog]
        public ResponseModel<WorkOrderDetailReportListModel> ListWorkOrderDetailReport(UserInfo user, WorkOrderDetailReportListModel filter, out int totalCnt, out int totalVehicle)
        {
            var response = new ResponseModel<WorkOrderDetailReportListModel>();
            totalCnt = 0;
            totalVehicle = 0;
            try
            {
                response.Data = data.ListWorkOrderDetailReport(user, filter, 120, out totalCnt, out totalVehicle);
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

        [BusinessLog]
        public ResponseModel<WorkOrderMaintReportListModel> ListWorkOrderMaintReport(UserInfo user, WorkOrderMaintReportListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<WorkOrderMaintReportListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListWorkOrderMaintReport(user, filter, out totalCnt);
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

        [BusinessLog]
        public ResponseModel<PurchaseOrderReport> GetPurchaseOrderReport(UserInfo user, PurchaseOrderFilterRequest filter, out decimal totalPrice, out int totalCnt)
        {
            var response = new ResponseModel<PurchaseOrderReport>();
            totalCnt = 0;
            totalPrice = 0;
            try
            {
                response.Data = data.GetPurchaseOrderReport(user, filter, out totalPrice, out totalCnt);
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

        [BusinessLog]
        public ResponseModel<PartExchangeReport> GetPartExchangeReport(UserInfo user, PartExchangeFilterRequest filter, out int totalCnt)
        {
            var response = new ResponseModel<PartExchangeReport>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetPartExchangeReport(user, filter, out totalCnt);
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

        [BusinessLog]
        public ResponseModel<PartInfoModel> ListPartInfo(UserInfo user, PartInfoRequest filter, out int totalCnt)
        {
            var response = new ResponseModel<PartInfoModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListPartInfo(user, filter, out totalCnt);
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

        [BusinessLog]
        public ResponseModel<SaleReport> ListSaleReport(UserInfo user, SaleReportFilterRequest filter, out int totalCnt)
        {
            var response = new ResponseModel<SaleReport>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetSaleReport(user, filter, out totalCnt);
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

        [BusinessLog]
        public ResponseModel<FleetPeriodReport> ListFleetPeriodReport(UserInfo user, FleetPeriodReportFilterRequest filter, out int totalCnt)
        {
            var response = new ResponseModel<FleetPeriodReport>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetFleetPeriodReport(user, filter, out totalCnt);
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

        [BusinessLog]
        public ResponseModel<CycleCountResultReport> GetCycleCountResultReport(UserInfo user, CycleCountResultReportFilterRequest filter, out int totalCnt)
        {
            var response = new ResponseModel<CycleCountResultReport>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetCycleCountResultReport(user, filter, out totalCnt);
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

        [BusinessLog]
        public ResponseModel<WorkOrderPartHistoryReport> GetWorkOrderPartHistoryReport(UserInfo user, WorkOrderPartHistoryReportFilterRequest filter, out int totalCnt)
        {
            var response = new ResponseModel<WorkOrderPartHistoryReport>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetWorkOrderPartHistoryReport(user, filter, out totalCnt);
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

        [BusinessLog]
        public ResponseModel<InvoiceWebServiceResult> GetInvoicesAsXml(string userName, string password, DateTime startDate, DateTime endDate, long customerId, string invoiceNo)
        {
            var response = new ResponseModel<InvoiceWebServiceResult>();
            try
            {
                response.Model = data.GetInvoicesAsXml(userName, password, startDate, endDate, customerId, invoiceNo);
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

        [BusinessLog]
        public ResponseModel<SentPartUsageReport> GetSentPartUsageReport(UserInfo user, SentPartUsageReportFilterRequest filter, out int totalCnt)
        {
            var response = new ResponseModel<SentPartUsageReport>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetSentPartUsageReport(user, filter, out totalCnt);
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

        [BusinessLog]
        public ResponseModel<KilometerDistributionReport> GetKilometerDistributionReport(UserInfo user, KilometerDistributionFilterRequest filter, out int totalCnt)
        {
            var response = new ResponseModel<KilometerDistributionReport>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetKilometerDistributionReport(user, filter, out totalCnt);
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

        [BusinessLog]
        public ResponseModel<CampaignSummaryReport> GetCampaignSummaryReport(UserInfo user, CampaignSummaryReportFilterRequest filter, out int totalCnt)
        {
            var response = new ResponseModel<CampaignSummaryReport>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetCampaignSummaryReport(user, filter, out totalCnt);
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

        [BusinessLog]
        public ResponseModel<GuaranteeReport> GetGuaranteeReport(GuaranteeReportFilterRequest filter, out int totalCnt)
        {
            var response = new ResponseModel<GuaranteeReport>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetGuaranteeReport(filter, out totalCnt);
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

        [BusinessLog]
        public ResponseModel<WorkOrderPerformanceReport> GetWorkOrderPreformanceReport(WorkOrderPerformanceReportFilterRequest filter, out int totalCnt)
        {
            var response = new ResponseModel<WorkOrderPerformanceReport>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetWorkOrderPreformanceReport(filter, out totalCnt);
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

        [BusinessLog]
        public ResponseModel<PartStockReport> GetPartStockReport(UserInfo user, PartStockFilterRequest filter, out decimal totalPrice, out int totalCnt)
        {
            var response = new ResponseModel<PartStockReport>();
            totalCnt = 0;
            totalPrice = 0;
            try
            {
                response.Data = data.GetPartStockReport(user, filter, out totalPrice, out totalCnt);
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

        [BusinessLog]
        public ResponseModel<ChargePerCarReport> GetChargePerCarReport(UserInfo user, ChargePerCarFilterRequest filter, out int totalCnt)
        {
            var response = new ResponseModel<ChargePerCarReport>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetChargePerCarReport(user, filter, out totalCnt);
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

        [BusinessLog]
        public ResponseModel<WorkOrderDetailReport> GetWorkOrderDetailByWorkOrderParameters(ChargeWorkOrderDetailFilterRequest filter, out int totalCnt)
        {
            var response = new ResponseModel<WorkOrderDetailReport>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetWorkOrderDetailByWorkOrderParameters(filter, out totalCnt);
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

        [BusinessLog]
        public ResponseModel<PartStockActivityReport> GetPartStockActivityReport(PartStockActivityFilterRequest filter, out int totalCnt)
        {
            var response = new ResponseModel<PartStockActivityReport>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetPartStockActivityReport(filter, out totalCnt);
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

        [BusinessLog]
        public ResponseModel<SparePartControlReport> GetSparePartControlReport(UserInfo user, PartStockFilterRequest filter, out int totalCnt)
        {
            var response = new ResponseModel<SparePartControlReport>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetSparePartControlReport(user, filter, out totalCnt);
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

        [BusinessLog]
        public WebReportBase<WorkOrderProcessTypesTotalReportModel> GetWorkOrderProcessTypesTotalReport(UserInfo user, WorkOrderProcessTypesFilterRequest request)
        {
            var dto = new WebReportBase<WorkOrderProcessTypesTotalReportModel>();
            var dbHelper = new DbHelper(makeParamsDbNull: false);

            var dataTable = dbHelper.GetDataTable("P_RPT_WO_PROCESS_TYPES_TOTAL",300,
                MakeDbNull(request.GroupId),
                MakeDbNull(request.StartDate),
                MakeDbNull(request.EndDate),
                MakeDbNull(request.DealerIdList),
                MakeDbNull(request.RegionIdList),
                MakeDbNull(request.VehicleModel),
                MakeDbNull(request.VehicleType),
                MakeDbNull(request.WorkOrderStatus),
                MakeDbNull(request.CurrencyCode),
                MakeDbNull(request.InGuarantee),
                UserManager.LanguageCode,
                MakeDbNull(request.SortColumn),
                MakeDbNull(request.SortDirection),
                request.Offset,
                MakeDbNull(request.PageSize),
                MakeDbNull(null),
                MakeDbNull(null),
                MakeDbNull(null));


            dto.Items = new List<WorkOrderProcessTypesTotalReportModel>();

            var processList = CommonBL.ListProcessType(user).Data;

            foreach (DataRow row in dataTable.Rows)
            {
                var workOrderGroup = new WorkOrderProcessTypesTotalReportModel();
                workOrderGroup.CurrencyCode = row["CurrencyCode"].ToString();
                workOrderGroup.GroupName = row["GroupName"].ToString();
                workOrderGroup.TotalCarCount = Convert.ToInt32(row["TotalCarCount"]);
                workOrderGroup.TotalPrice = Convert.ToDecimal(row["TotalPrice"]);
                workOrderGroup.TotalWorkOrderCount = Convert.ToInt32(row["TotalWorkOrderCount"]);
                workOrderGroup.TotalWorkOrderDetailCount = Convert.ToInt32(row["TotalWorkOrderDetailCount"]);
                workOrderGroup.TotalIndicatorCount = Convert.ToInt32(row["TotalIndicatorCount"]);
                workOrderGroup.ProcessTypes = new List<WorkOrderProcessTypeValue>();

                foreach (var processType in processList)
                {
                    if (row.Table.Columns.Contains(processType.Value + "_Tutar"))
                    {
                        workOrderGroup.ProcessTypes.Add(new WorkOrderProcessTypeValue
                        {
                            Code = processType.Value,
                            Price = Convert.ToDecimal(row[processType.Value + "_Tutar"]),
                            Count = Convert.ToDecimal(row[processType.Value + "_Adet"]),
                            Percent = Convert.ToDecimal(row[processType.Value + "_Yuzde"]),
                            TotalPercent = Convert.ToDecimal(row[processType.Value + "_ToplamYuzde"])
                        });
                    }
                    else
                        workOrderGroup.ProcessTypes.Add(new WorkOrderProcessTypeValue
                        {
                            Code = processType.Value
                        });
                }
                dto.Items.Add(workOrderGroup);
            }

            dto.FilteredTotal = int.Parse(dbHelper.GetOutputValue("TOTAL_COUNT").ToString());
            dto.Total = int.Parse(dbHelper.GetOutputValue("TOTAL_COUNT").ToString());
            return dto;
        }

        [BusinessLog]
        public ResponseModel<FixAssetInventoryReport> GetFixAssetInventoryReport(UserInfo user, FixAssetInventoryReportFilterRequest filter, out int totalCnt)
        {
            var response = new ResponseModel<FixAssetInventoryReport>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetFixAssetInventoryReport(user, filter, out totalCnt);
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

        [BusinessLog]
        public ResponseModel<WorkOrderPartsTotalReport> GetWorkOrderPartsTotalReport(UserInfo user, WorkOrderPartsTotalReportFilterRequest filter, out int totalCnt)
        {
            var response = new ResponseModel<WorkOrderPartsTotalReport>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetWorkOrderPartsTotalReport(user, filter, out totalCnt);
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

        [BusinessLog]
        public ResponseModel<SelectListItem> ListInvoiceNoForAutoComplete(string invoiceNo, string dealerId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListInvoiceNoForAutoComplete(invoiceNo, dealerId);
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

        [BusinessLog]
        public ResponseModel<InvoiceInfoReport> ListInvoiceInfoReport(UserInfo user, InvoiceInfoFilterRequest filter, out int totalCnt)
        {
            var response = new ResponseModel<InvoiceInfoReport>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListInvoiceInfoReport(user, filter, out totalCnt);
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

        [BusinessLog]
        public ResponseModel<PersonnelInfoReport> ListPersonnelInfoReport(UserInfo user, PersonnelInfoReportFilterRequest filter, out int totalCnt)
        {
            var response = new ResponseModel<PersonnelInfoReport>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListPersonnelInfoReport(user, filter, out totalCnt);
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

        [BusinessLog]
        public ResponseModel<SshReport> ListSshReport(UserInfo user, SshReportFilterRequest filter, out int totalCnt)
        {
            var response = new ResponseModel<SshReport>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListSSHReport(user, filter, out totalCnt);
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

        [BusinessLog]
        public ResponseModel<SelectListItem> ListIdentityNoForAutoComplete(string identityNo)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListIdentityNoForAutoComplete(identityNo);
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

        [BusinessLog]
        public ResponseModel<SelectListItem> ListDefectNoForAutoComplete(string defectNo)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListDefectIdForAutoComplete(defectNo);
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

        [BusinessLog]
        public ResponseModel<SelectListItem> ListContractNameForAutoComplete(string contractName)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListContractNameForAutoComplete(contractName);
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

        [BusinessLog]
        public ResponseModel<SelectListItem> ListUserCodeForAutoComplete(string userNo)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListUserCodeForAutoComplete(userNo);
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

        [BusinessLog]
        public ResponseModel<SelectListItem> ListUserNameForAutoComplete(string userName)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListUserNameForAutoComplete(userName);
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

        [BusinessLog]
        public ResponseModel<SelectListItem> ListDealerCustomersForAutoComplete(string custName, string dealerId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListDealerCustomersForAutoComplete(custName, dealerId);
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

        [BusinessLog]
        public ResponseModel<VehicleServiceDurationReport> GetCarServiceDurationReport(UserInfo user, VehicleServiceDurationFilterRequest filter, out int totalCnt)
        {
            var response = new ResponseModel<VehicleServiceDurationReport>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetCarServiceDurationReport(user, filter, out totalCnt);
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

        [BusinessLog]
        public ResponseModel<WorkOrderInfoModel> ListWorkOrderInfo(UserInfo user, WorkOrderInfoRequest filter, out int totalCnt)
        {
            var response = new ResponseModel<WorkOrderInfoModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListWorkOrderInfo(user, filter, out totalCnt);
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

        [BusinessLog]
        public ResponseModel<LaborCostPerVehicleReport> GetLaborCostPerVehicleReport(UserInfo user, LaborCostPerVehicleReportFilterRequest filter, out int totalCnt)
        {
            var response = new ResponseModel<LaborCostPerVehicleReport>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetLaborCostPerVehicleReport(user, filter, out totalCnt);
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

        [BusinessLog]
        public ResponseModel<KilometerReportInfoModel> ListWorkOrderDetailKilometer(UserInfo user, WorkOrderDetailKilometerRequest filter, out int totalCnt)
        {
            var response = new ResponseModel<KilometerReportInfoModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListWorkOrderDetailKilometer(user, filter, out totalCnt);
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

        [BusinessLog]
        public ResponseModel<CampaignSummaryInfoModel> ListCampaignSummaryInfo(UserInfo user, CampaignSummaryInfoRequest filter, out int totalCnt)
        {
            var response = new ResponseModel<CampaignSummaryInfoModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListCampaignSummaryInfo(user, filter, out totalCnt);
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

        [BusinessLog]
        public ResponseModel<bool> CheckDay27Data()
        {
            var response = new ResponseModel<bool>();
            try
            {
                data.CheckDay27Data();
                response.Model = true;
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

        [BusinessLog]
        public ResponseModel<string> CheckWorkOrderDayData()
        {
            var response = new ResponseModel<string>();
            try
            {
                response.Model = data.CheckWorkOrderDayData();
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

        [BusinessLog]
        public ResponseModel<string> CheckWorkOrderHourData()
        {
            var response = new ResponseModel<string>();
            try
            {
                response.Model = data.CheckWorkOrderHourData();
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

        [BusinessLog]
        public ResponseModel<AlternatePartReport> GetAlternatePartReport(UserInfo user, AlternatePartReportFilterRequest filter, out int totalCnt)
        {
            var response = new ResponseModel<AlternatePartReport>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetAlternatePartReport(user, filter, out totalCnt);
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
