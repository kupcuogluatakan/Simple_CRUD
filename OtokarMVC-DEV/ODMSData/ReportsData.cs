using System;
using ODMSCommon.Security;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSModel.Reports;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Data.Common;
using System.Transactions;
using ODMSCommon.Resources;

namespace ODMSData
{
    public class ReportsData : DataAccessBase
    {
        public List<WorkOrderDetailReportListModel> ListWorkOrderDetailReport(UserInfo user, WorkOrderDetailReportListModel filter, int commandTimeout, out int totalCount, out int totalVehicle)
        {
            var retVal = new List<WorkOrderDetailReportListModel>();
            totalCount = 0;
            totalVehicle = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_WORK_ORDER_DETAIL_REPORT");
                cmd.CommandTimeout = commandTimeout;
                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, MakeDbNull(filter.StartDate));
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, MakeDbNull(filter.EndDate));
                db.AddInParameter(cmd, "WARRANTY_START_DATE", DbType.DateTime, filter.WarrantyStartDate);
                db.AddInParameter(cmd, "WARRANTY_END_DATE", DbType.DateTime, filter.WarrantyEndDate);
                db.AddInParameter(cmd, "DEALER_ID_LIST", DbType.String, MakeDbNull(filter.DealerIdList));
                db.AddInParameter(cmd, "DEALER_REGION_ID_LIST", DbType.String, MakeDbNull(filter.DealerRegionIdList));
                db.AddInParameter(cmd, "MODEL_KOD_LIST", DbType.String, MakeDbNull(filter.ModelKodList));
                db.AddInParameter(cmd, "STATUS_ID_LIST", DbType.String, MakeDbNull(filter.StatusIdList));
                db.AddInParameter(cmd, "WORK_ORDER_ID_LIST", DbType.String, MakeDbNull(filter.WorkOrderIdList));
                db.AddInParameter(cmd, "VIN_NO", DbType.String, MakeDbNull(filter.VinNo));
                db.AddInParameter(cmd, "PLATE", DbType.String, MakeDbNull(filter.Plate));
                db.AddInParameter(cmd, "PART_CODE", DbType.String, MakeDbNull(filter.PartCode));
                db.AddInParameter(cmd, "LABOUR_CODE", DbType.String, MakeDbNull(filter.LabourCode));
                db.AddInParameter(cmd, "VEHICLE_TYPE", DbType.String, MakeDbNull(filter.VehicleType));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddInParameter(cmd, "IS_DAY_TIME", DbType.Int32, (filter.IsDayTime ?? -1));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "TOTAL_VEHICLE", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var workOrderDetailReportListModel = new WorkOrderDetailReportListModel()
                        {
                            WorkOrderNo = reader["WORK_ORDER_NO"].GetValue<string>(),
                            WorkOrderDate = reader["WORK_ORDER_DATE"].GetValue<DateTime?>(),
                            CurrencyCode = reader["CURRENCY_CODE"].GetValue<string>(),
                            CustomerLabourPrice = reader["CUSTOMER_LABOUR_PRICE"].GetValue<decimal>(),
                            CustomerNameLastName = reader["CUSTOMER_NAME_LASTNAME"].GetValue<string>(),
                            CustomerPartPrice = reader["CUSTOMER_PART_PRICE"].GetValue<decimal>(),
                            DealerName = reader["DEALER_NAME"].GetValue<string>(),
                            SAPCode = reader["DEALER_SSID"].GetValue<string>(),
                            DealerRegionName = reader["DEALER_REGION_NAME"].GetValue<string>(),
                            FleetCode = reader["FLEET_CODE"].GetValue<string>(),
                            ModelKod = reader["MODEL_KOD"].GetValue<string>(),
                            Plate = reader["PLATE"].GetValue<string>(),
                            StatusName = reader["STATUS_NAME"].GetValue<string>(),
                            VehicleKM = reader["VEHICLE_KM"].GetValue<string>(),
                            VehicleLeaveDate = reader["VEHICLE_LEAVE_DATE"].GetValue<DateTime?>(),
                            VinNo = reader["VIN_NO"].GetValue<string>(),
                            WarrantyStartDate = reader["WARRANTY_START_DATE"].GetValue<DateTime?>(),
                            WarrantyEndDate = reader["WARRANTY_END_DATE"].GetValue<DateTime?>(),
                            WarrantyLabourPrice = reader["WARRANTY_LABOUR_PRICE"].GetValue<decimal>(),
                            WarrantyPartPrice = reader["WARRANTY_PART_PRICE"].GetValue<decimal>(),
                            VehicleId = reader["ID_VEHICLE"].GetValue<int>(),
                            VehicleType = reader["VEHICLE_TYPE"].ToString(),
                            ClosedDate = reader["CLOSED_DATE"].GetValue<DateTime?>(),
                            DayTime = reader["DAY_TIME"].GetValue<string>(),
                            WoTime = reader["WO_DATE"].GetValue<string>()

                        };
                        workOrderDetailReportListModel.MaintDayCount = (workOrderDetailReportListModel.VehicleLeaveDate.GetValueOrDefault(DateTime.Now) - workOrderDetailReportListModel.WorkOrderDate.GetValueOrDefault()).Days;
                        retVal.Add(workOrderDetailReportListModel);
                    }
                    reader.Close();

                    totalCount = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
                    totalVehicle = db.GetParameterValue(cmd, "TOTAL_VEHICLE").GetValue<int>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                CloseConnection();
            }

            return retVal;
        }

        public List<WorkOrderMaintReportListModel> ListWorkOrderMaintReport(UserInfo user, WorkOrderMaintReportListModel filter, out int totalCount)
        {
            var retVal = new List<WorkOrderMaintReportListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_WORK_ORDER_MAINT_REPORT");
                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, MakeDbNull(filter.StartDate));
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, MakeDbNull(filter.EndDate));
                db.AddInParameter(cmd, "INVOICE_START_DATE", DbType.DateTime, MakeDbNull(filter.InvoiceStartDate));
                db.AddInParameter(cmd, "INVOICE_END_DATE", DbType.DateTime, MakeDbNull(filter.InvoiceEndDate));
                db.AddInParameter(cmd, "DEALER_ID_LIST", DbType.String, MakeDbNull(filter.DealerIdList));
                db.AddInParameter(cmd, "DEALER_REGION_ID_LIST", DbType.String, MakeDbNull(filter.DealerRegionIdList));
                db.AddInParameter(cmd, "MODEL_KOD_LIST", DbType.String, MakeDbNull(filter.ModelKodList));
                db.AddInParameter(cmd, "PERIODIC_MAINT", DbType.String, MakeDbNull(filter.PeriodicMaint));
                db.AddInParameter(cmd, "WARRANTY", DbType.Int32, MakeDbNull(filter.InGuarantee));
                db.AddInParameter(cmd, "INVOICE_NO", DbType.String, MakeDbNull(filter.InvoiceNo));


                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var workOrderMaintReportListModel = new WorkOrderMaintReportListModel()
                        {
                            DealerName = reader["DEALER_NAME"].GetValue<string>(),
                            DealerRegionName = reader["DEALER_REGION_NAME"].GetValue<string>(),
                            MaintName = reader["MAINT_NAME"].GetValue<string>(),
                            ModelKod = reader["MODEL_NAME"].GetValue<string>(),
                            TotalInvoiceAmount = reader["TOTAL_INVOICE_AMOUNT"].GetValue<decimal>(),
                            TotalDiscountPrice = reader["TOTAL_DISCOUNT_PRICE"].GetValue<decimal>(),
                            TotalLabourPrice = reader["TOTAL_LABOUR_PRICE"].GetValue<decimal>(),
                            TotalPartPrice = reader["TOTAL_PART_PRICE"].GetValue<decimal>(),
                            InvoiceDate = reader["INVOICE_DATE"].GetValue<DateTime>(),
                            InvoiceNo = reader["INVOICE_NO"].GetValue<string>(),
                            VinNo = reader["VIN_NO"].GetValue<string>(),
                            IdWorkOrder = reader["ID_WORK_ORDER"].GetValue<int>(),
                            Customer = reader["CUSTOMER"].GetValue<string>(),
                            WorkOrderInvoiceId = reader["ID_WORK_ORDER_INV"].GetValue<long>(),
                            VehicleId = reader["VEHICLE_ID"].GetValue<int>()
                        };
                        retVal.Add(workOrderMaintReportListModel);
                    }
                    reader.Close();

                    totalCount = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
                }
                totalCount = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                CloseConnection();
            }

            return retVal;
        }

        public List<SaleReport> GetSaleReport(UserInfo user, SaleReportFilterRequest filter, out int total)
        {
            var result = new List<SaleReport>();
            total = 0;

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_RPT_SALE");
                db.AddInParameter(cmd, "P_SALE_TYPE", DbType.Int32, MakeDbNull(filter.SaleType));
                db.AddInParameter(cmd, "P_INVOICE_BEGIN_DATE", DbType.DateTime, MakeDbNull(filter.InvoiceBeginDate));
                db.AddInParameter(cmd, "P_INVOICE_END_DATE", DbType.DateTime, MakeDbNull(filter.InvoiceEndDate));
                db.AddInParameter(cmd, "P_CREATE_BEGIN_DATE", DbType.DateTime, MakeDbNull(filter.CreateBeginDate));
                db.AddInParameter(cmd, "P_CREATE_END_DATE", DbType.DateTime, MakeDbNull(filter.CreateEndDate));
                db.AddInParameter(cmd, "P_CUSTOMER_ID", DbType.String, MakeDbNull(filter.CustomerId));
                db.AddInParameter(cmd, "P_DEALER_ID", DbType.String, MakeDbNull(filter.DealerId));
                db.AddInParameter(cmd, "P_INVOICE_NO", DbType.String, MakeDbNull(filter.InvoiceNo));
                db.AddInParameter(cmd, "P_SALE_STATUS", DbType.Boolean, MakeDbNull(filter.PurchaseStatus));
                db.AddInParameter(cmd, "P_VIN_NO", DbType.String, MakeDbNull(filter.VinNo));
                db.AddInParameter(cmd, "P_PART_CODE", DbType.String, MakeDbNull(filter.PartCode));
                db.AddInParameter(cmd, "P_PART_CODE_LIST", DbType.String, MakeDbNull(filter.PartCodeList));
                db.AddInParameter(cmd, "P_VEHICLE_MODEL", DbType.String, MakeDbNull(filter.VehicleModel));
                db.AddInParameter(cmd, "P_VEHICLE_TYPE", DbType.String, MakeDbNull(filter.VehicleType));
                db.AddInParameter(cmd, "P_SALE_TYPE_LOOOK_VAL", DbType.String, MakeDbNull(filter.SaleTypeLookVal));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "P_TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "P_ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "P_ERROR_DESC", DbType.String, 200);

                cmd.CommandTimeout = 1440;
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new SaleReport
                        {
                            SaleType = reader["SALE_TYPE"].GetValue<int>(),
                            SaleTypeName = reader["SALE_TYPE_NAME"].ToString(),
                            PartCode = reader["PART_CODE"].ToString(),
                            PartName = reader["PART_NAME"].ToString(),
                            DealerName = reader["DEALER_NAME"].ToString(),
                            DealerRegionName = reader["DEALER_REGION_NAME"].ToString(),
                            WorkOrderId = reader["WORK_ORDER_ID"].GetValue<int>(),
                            InvoiceNo = reader["INVOICE_NO"].ToString(),
                            InvoiceDate = reader["INVOICE_DATE"].GetValue<DateTime>(),
                            WayBillNo = reader["WAYBILL_NO"].ToString(),
                            WayBillDate = reader["WAYBILL_DATE"].GetValue<DateTime>(),
                            OriginalPartCode = reader["ORIGINAL_PART_CODE"].ToString(),
                            OriginalPartName = reader["ORIGINAL_PART_NAME"].ToString(),
                            Unit = reader["UNIT_VAL"].ToString(),
                            Quantity = reader["AMOUNT"].GetValue<decimal>(),
                            DiscountRatio = reader["DISCOUNT_RATIO"].GetValue<decimal>(),
                            RecordDate = reader["RECORD_DATE"].GetValue<DateTime>(),
                            CustomerId = reader["CUSTOMER_ID"].GetValue<int>(),
                            DiscountPrice = reader["DISCOUNT_AMOUNT"].GetValue<decimal>(),
                            VatRatio = reader["VAT_RATIO"].GetValue<decimal>(),
                            VatPrice = reader["VAT_AMOUNT"].GetValue<decimal>(),
                            DealerPrice = reader["DEALER_PRICE"].GetValue<decimal>(),
                            CustomerName = reader["CUSTOMER"].ToString(),
                            VehicleLeaveDate = reader["VEHICLE_LEAVE_DATE"].GetValue<DateTime>(),
                            InvoiceUpdateDate = reader["INVOICE_UPDATE_DATE"].GetValue<DateTime>(),
                            WithOld = reader["WITHOLD"].ToString(),
                            WithOldPrice = reader["WITHOLD_AMOUNT"].GetValue<decimal>(),
                            CurrencyCode = reader["CURRENCY_CODE"].ToString(),
                            VinNo = reader["VIN_NO"].ToString(),
                            VehicleCode = reader["V_CODE_KOD"].ToString(),
                            VehicleModel = reader["MODEL_NAME"].ToString(),
                            VehicleType = reader["TYPE_NAME"].ToString(),
                            TotalDealerPrice = reader["TOTAL_DEALER_PRICE"].GetValue<decimal>(),
                            Invoiced = reader["INVOICED"].GetValue<int>() == 1,
                            ListPrice = reader["LIST_PRICE"].GetValue<decimal>()
                        };
                        result.Add(listModel);
                    }
                    reader.Close();
                    total = db.GetParameterValue(cmd, "P_TOTAL_COUNT").GetValue<int>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return result;
        }



        public List<PurchaseOrderReport> GetPurchaseOrderReport(UserInfo user, PurchaseOrderFilterRequest filter, out decimal totalPrice, out int totalCnt)
        {
            var dto = new List<PurchaseOrderReport>();
            totalPrice = 0;
            totalCnt = 0;

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_RPT_PURCHASE_ORDER");
                db.AddInParameter(cmd, "DEALER_ID_LIST", DbType.String, MakeDbNull(filter.DealerIdList));
                db.AddInParameter(cmd, "DEALER_REGION_ID_LIST", DbType.String, MakeDbNull(filter.DealerRegionIdList));
                db.AddInParameter(cmd, "SUPPLIER_ID_LIST", DbType.String, MakeDbNull(filter.SupplierIdList));
                db.AddInParameter(cmd, "PURCHASE_ORDER_START_DATE", DbType.DateTime, MakeDbNull(filter.PurchaseOrderStartDate));
                db.AddInParameter(cmd, "PURCHASE_ORDER_END_DATE", DbType.DateTime, MakeDbNull(filter.PurchaseOrderEndDate));
                db.AddInParameter(cmd, "DELIVERY_START_DATE", DbType.DateTime, MakeDbNull(filter.DeliveryStartDate));
                db.AddInParameter(cmd, "DELIVERY_END_DATE", DbType.DateTime, MakeDbNull(filter.DeliveryEndDate));
                db.AddInParameter(cmd, "INVOICE_START_DATE", DbType.DateTime, MakeDbNull(filter.InvoiceStartDate));
                db.AddInParameter(cmd, "INVOICE_END_DATE", DbType.DateTime, MakeDbNull(filter.InvoiceEndDate));
                db.AddInParameter(cmd, "PART_CODE", DbType.String, MakeDbNull(filter.PartCode));
                db.AddInParameter(cmd, "IS_ORIGINAL", DbType.Int32, filter.IsOriginal ?? (object)DBNull.Value);
                db.AddInParameter(cmd, "ID_STOCK_TYPE_LIST", DbType.String, MakeDbNull(filter.StockTypeId));
                db.AddInParameter(cmd, "PO_LOCATION", DbType.Int32, MakeDbNull(filter.PoLocation));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, MakeDbNull(filter.Offset));
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "FILTERED_TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "TOTAL_PRICE", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 120);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var purchaseOrderReport = new PurchaseOrderReport
                        {
                            DealerId = reader["DealerId"].GetValue<int>(),
                            SupplierId = reader["SupplierId"].GetValue<int>(),
                            DealerName = reader["DealerName"].GetValue<string>(),
                            DealerRegionName = reader["DealerRegionName"].GetValue<string>(),
                            SupplierName = reader["SupplierName"].GetValue<string>(),
                            TaxNo = reader["TaxNo"].GetValue<string>(),
                            WaybillNo = reader["WaybillNo"].GetValue<string>(),
                            WaybillDate = reader["WaybillDate"].GetValue<DateTime>(),
                            PurchaseOrderNumber = reader["PurchaseOrderNumber"].GetValue<long>(),
                            DeliveryId = reader["DeliveryId"].GetValue<long>(),
                            InvoiceSerialNumber = reader["InvoiceSerialNumber"].GetValue<string>(),
                            InvoiceNumber = reader["InvoiceNumber"].GetValue<string>(),
                            PartCode = reader["PartCode"].GetValue<string>(),
                            PartName = reader["PartName"].GetValue<string>(),
                            IsOriginal = reader["IsOriginal"].GetValue<string>(),
                            RecievedQuantity = reader["RecievedQuantity"].GetValue<decimal>(),
                            Unit = reader["Unit"].GetValue<string>(),
                            Price = reader["Price"].GetValue<decimal>(),
                            OrderPrice = reader["OrderPrice"].GetValue<decimal>(),
                            InvoiceDate = reader["InvoiceDate"].GetValue<DateTime>(),
                            OrderDate = reader["OrderDate"].GetValue<DateTime>(),
                            StockType = reader["StockType"].GetValue<string>(),
                            ShipQuant = reader["ShipQuant"].GetValue<decimal>()
                        };
                        dto.Add(purchaseOrderReport);

                    }
                    reader.Close();
                    totalPrice = db.GetParameterValue(cmd, "TOTAL_PRICE").GetValue<int>();
                    totalCnt = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return dto;

        }

        public List<PartExchangeReport> GetPartExchangeReport(UserInfo user, PartExchangeFilterRequest filter, out int total)
        {
            var dto = new List<PartExchangeReport>();
            total = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_RPT_PART_EXCHANGE");
                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, MakeDbNull(filter.StartDate));
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, MakeDbNull(filter.EndDate));
                db.AddInParameter(cmd, "DEALER_ID_LIST", DbType.String, MakeDbNull(filter.DealerIdList));
                db.AddInParameter(cmd, "DEALER_REGION_ID_LIST", DbType.String, MakeDbNull(filter.DealerRegionIdList));
                db.AddInParameter(cmd, "VEHICLE_MODEL_LIST", DbType.String, MakeDbNull(filter.VehicleModelList));
                db.AddInParameter(cmd, "PROCESS_TYPE_LIST", DbType.String, MakeDbNull(filter.ProcessTypeList));
                db.AddInParameter(cmd, "CURRENCY_CODE_LIST", DbType.String, MakeDbNull(filter.CurrencyCodeList));
                db.AddInParameter(cmd, "GIF_COST_CENTER_LIST", DbType.String, MakeDbNull(filter.GifCostCenterList));
                db.AddInParameter(cmd, "MAX_PRICE", DbType.Decimal, MakeDbNull(filter.MaxPrice));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                db.AddInParameter(cmd, "PART_CODE", DbType.String, MakeDbNull(filter.PartCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "FILTERED_TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 400);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var partExchangeReport = new PartExchangeReport
                        {
                            DealerRegionName = reader["DealerRegionName"].GetValue<string>(),
                            VehicleModel = reader["VehicleModel"].GetValue<string>(),
                            DealerId = reader["DealerId"].GetValue<string>(),
                            DealerRegionId = reader["DealerRegionId"].GetValue<string>(),
                            DealerName = reader["DealerName"].GetValue<string>(),
                            PartCode = reader["PartCode"].GetValue<string>(),
                            PartName = reader["PartName"].GetValue<string>(),
                            TotalPrice = reader["TotalPrice"].GetValue<decimal>(),
                            TotalQuantity = reader["TotalQuantity"].GetValue<decimal>(),
                            FreePartQuantity = reader["FreePartQuantity"].GetValue<decimal>(),
                            NonFreePartQuantity = reader["NonFreePartQuantity"].GetValue<decimal>(),
                            Currency = reader["Currency"].GetValue<string>(),
                            MaxKM = reader["MaxKM"].GetValue<string>(),
                            MinKM = reader["MinKM"].GetValue<string>(),
                            AverageKM = reader["AverageKM"].GetValue<string>(),
                            PartId = reader["PartId"].GetValue<long>(),
                            AvgPrice = reader["AvgPrice"].GetValue<decimal>()
                        };
                        dto.Add(partExchangeReport);

                    }
                    reader.Close();

                    total = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return dto;


        }

        public List<PartInfoModel> ListPartInfo(UserInfo user, PartInfoRequest filter, out int total)
        {
            var dto = new List<PartInfoModel>();
            total = 0;

            try
            {

                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_WO_PART_INFO");
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, MakeDbNull(filter.PartId));
                db.AddInParameter(cmd, "DEALER_ID", DbType.String, MakeDbNull(filter.DealerId));
                db.AddInParameter(cmd, "DEALER_REGION_ID", DbType.String, MakeDbNull(filter.DealerRegionId));
                db.AddInParameter(cmd, "PROCESS_TYPE", DbType.String, MakeDbNull(filter.ProcessType));
                db.AddInParameter(cmd, "CATEGORY", DbType.String, MakeDbNull(filter.Category));
                db.AddInParameter(cmd, "START_DATE", DbType.Date, MakeDbNull(filter.StartDate));
                db.AddInParameter(cmd, "END_DATE", DbType.Date, MakeDbNull(filter.EndDate));
                db.AddInParameter(cmd, "MAXPRICE", DbType.Decimal, MakeDbNull(filter.MaxPrice));
                db.AddInParameter(cmd, "CURRENCY", DbType.String, MakeDbNull(filter.Currency));
                db.AddInParameter(cmd, "VEHICLE_MODEL", DbType.String, MakeDbNull(filter.VehicleModel));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, MakeDbNull(filter.Offset));
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 400);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var PartInfoModel = new PartInfoModel
                        {
                            WorkOrderId = reader["WorkOrderId"].GetValue<long>(),
                            WorkOrderDate = reader["WorkOrderDate"].GetValue<DateTime>(),
                            TotalPrice = reader["TotalPrice"].GetValue<decimal>(),
                            TotalQuantity = reader["TotalQuantity"].GetValue<decimal>(),
                            VehicleKilometer = reader["VehicleKilometer"].GetValue<string>(),
                            DealerName = reader["DealerName"].GetValue<string>(),
                            PartCode = reader["PartCode"].GetValue<string>(),
                            PartName = reader["PartName"].GetValue<string>(),
                            Currency = reader["Currency"].GetValue<string>(),
                            VehicleModel = reader["VehicleModel"].GetValue<string>(),
                            //VehicleType = reader["TYPE_NAME"].GetValue<string>(),
                            Customer = reader["Customer"].GetValue<string>(),
                            EngineNo = reader["EngineNo"].GetValue<string>(),
                            VinNo = reader["VinNo"].GetValue<string>(),
                            DealerRegionName = reader["DealerRegionName"].GetValue<string>()
                        };
                        dto.Add(PartInfoModel);
                    }
                    reader.Close();
                    total = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return dto;
        }

        public List<CycleCountResultReport> GetCycleCountResultReport(UserInfo user, CycleCountResultReportFilterRequest filter, out int total)
        {
            var dto = new List<CycleCountResultReport>();
            total = 0;

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_RPT_CYCLE_COUNT_RESULT");
                db.AddInParameter(cmd, "DEALER_ID_LIST", DbType.String, MakeDbNull(filter.DealerIdList));
                db.AddInParameter(cmd, "COUNT_BEGIN_DATE", DbType.DateTime, MakeDbNull(filter.CountBeginDate));
                db.AddInParameter(cmd, "COUNT_END_DATE", DbType.DateTime, MakeDbNull(filter.CountEndDate));
                db.AddInParameter(cmd, "COUNT_APPROVE_BEGIN_DATE", DbType.DateTime, MakeDbNull(filter.CountApproveStartDate));
                db.AddInParameter(cmd, "COUNT_APPROVE_END_DATE", DbType.DateTime, MakeDbNull(filter.CountApproveEndDate));
                db.AddInParameter(cmd, "ID_CYCLE_COUNT_STOCK_DIFF_LIST", DbType.String, MakeDbNull(filter.CycleCountDiffIdList));
                db.AddInParameter(cmd, "CYCLE_COUNT_STATUS", DbType.String, MakeDbNull(filter.CountStatusList));
                db.AddInParameter(cmd, "IS_ORIGINAL", DbType.Int32, filter.IsOriginal ?? (object)DBNull.Value);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "FILTERED_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var cycleCountResultReport = new CycleCountResultReport
                        {

                            WareHouseName = reader["WareHouseName"].GetValue<string>(),
                            DealerName = reader["DealerName"].GetValue<string>(),
                            RackName = reader["RackName"].GetValue<string>(),
                            PartCode = reader["PartCode"].GetValue<string>(),
                            PartName = reader["PartName"].GetValue<string>(),
                            Unit = reader["Unit"].GetValue<string>(),
                            BeforeCountQuantity = reader["BeforeCountQuantity"].GetValue<decimal>(),
                            AfterCountQuantity = reader["AfterCountQuantity"].GetValue<decimal>(),
                            ApprovedCountQuantity = reader["ApprovedCountQuantity"].GetValue<decimal>(),
                            StockDifference = reader["StockDifference"].GetValue<decimal>(),
                            Cost = reader["Cost"].GetValue<decimal>(),
                            UnitCost = reader["UnitCost"].GetValue<decimal>(),
                            CycleName = reader["CycleName"].GetValue<string>(),
                            StartDate = reader["StartDate"].GetValue<DateTime>()
                        };
                        dto.Add(cycleCountResultReport);
                    }
                    reader.Close();
                }
                total = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return dto;

        }

        public List<WorkOrderPartHistoryReport> GetWorkOrderPartHistoryReport(UserInfo user, WorkOrderPartHistoryReportFilterRequest filter, out int total)
        {
            var dto = new List<WorkOrderPartHistoryReport>();
            total = 0;

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_RPT_WO_PART_HISTORY");
                db.AddInParameter(cmd, "DEALER_ID_LIST", DbType.String, MakeDbNull(filter.DealerIdList));
                db.AddInParameter(cmd, "WO_BEGIN_DATE", DbType.DateTime, MakeDbNull(filter.BeginDate));
                db.AddInParameter(cmd, "WO_END_DATE", DbType.DateTime, MakeDbNull(filter.EndDate));
                db.AddInParameter(cmd, "PART_CODE", DbType.String, MakeDbNull(filter.PartCode));
                db.AddInParameter(cmd, "STOCK_TYPE_LIST", DbType.String, MakeDbNull(filter.StockTypeList));
                db.AddInParameter(cmd, "WORK_ORDER_CARD_STAT_LIST", DbType.String, MakeDbNull(filter.WorkOrderCardStatList));
                db.AddInParameter(cmd, "INDICATOR_STAT", DbType.String, MakeDbNull(filter.IndicatorList));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "FILTERED_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var workOrderPartHistoryReport = new WorkOrderPartHistoryReport
                        {
                            PartCode = reader["PartCode"].GetValue<string>(),
                            PartName = reader["PartName"].GetValue<string>(),
                            WorkOrderId = reader["WorkOrderId"].GetValue<long>(),
                            AddedQuantity = reader["AddedQuantity"].GetValue<decimal>(),
                            ReturnedQuantity = reader["ReturnedQuantity"].GetValue<decimal>(),
                            OriginalQuantity = reader["OriginalQuantity"].GetValue<decimal>(),
                            AvailableQuantity = reader["AvailableQuantity"].GetValue<decimal>(),
                            IsAllAdded = reader["IsAllAdded"].GetValue<string>(),
                            IsRemoved = reader["IsRemoved"].GetValue<string>(),
                            AddDate = reader["AddDate"].GetValue<DateTime>(),
                            StockType = reader["StockType"].GetValue<string>(),
                            WorkOrderStatus = reader["WorkOrderStatus"].GetValue<string>(),
                            IndicatorType = reader["IndicatorType"].GetValue<string>(),
                            CustomerAmount = reader["CustomerAmount"].GetValue<decimal>(),
                            PaidAmount = reader["PaidAmount"].GetValue<decimal>(),
                            Price = reader["Price"].GetValue<decimal>(),
                            PartId = reader["PartId"].GetValue<long>(),
                            DealerId = reader["DealerId"].GetValue<int>(),
                            Dealer = reader["Dealer"].GetValue<string>()
                        };
                        dto.Add(workOrderPartHistoryReport);
                    }
                    reader.Close();

                }
                total = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return dto;
        }

        public List<SentPartUsageReport> GetSentPartUsageReport(UserInfo user, SentPartUsageReportFilterRequest filter, out int totalCnt)
        {
            var dto = new List<SentPartUsageReport>();
            totalCnt = 0;

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_RPT_SENT_PART_USAGE_REPORT");
                db.AddInParameter(cmd, "DEALER_ID_LIST", DbType.String, MakeDbNull(filter.DealerIdList));
                db.AddInParameter(cmd, "DEALER_REGION_ID_LIST", DbType.String, MakeDbNull(filter.DealerRegionIdList));
                db.AddInParameter(cmd, "PO_ORDER_START_DATE", DbType.Date, MakeDbNull(filter.OrderStartDate));
                db.AddInParameter(cmd, "PO_ORDER_END_DATE", DbType.Date, MakeDbNull(filter.OrderEndDate));
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, MakeDbNull(filter.PartId));
                db.AddInParameter(cmd, "PURCHASE_ORDER_TYPE_LIST", DbType.String, MakeDbNull(filter.PurchaseOrderType));
                db.AddInParameter(cmd, "SALE_TYPE", DbType.String, MakeDbNull(filter.SaleType));
                db.AddInParameter(cmd, "STOCK_TYPE_ID", DbType.Int32, MakeDbNull(filter.StockTypeId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "FILTERED_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var sentPartUsageReport = new SentPartUsageReport
                        {

                            PoNumber = reader["PoNumber"].GetValue<long>(),
                            PartId = reader["PartId"].GetValue<long>(),
                            PurchaseOrderType = reader["PurchaseOrderType"].GetValue<string>(),
                            VinNo = reader["VinNo"].GetValue<string>(),
                            ActualVinNo = reader["ActualVinNo"].GetValue<string>(),
                            OrderDate = reader["OrderDate"].GetValue<DateTime>(),
                            PlacementDate = reader["PlacementDate"].GetValue<DateTime>(),
                            OrderQuantity = reader["OrderQuantity"].GetValue<decimal>(),
                            VehicleUseDate = reader["VehicleUseDate"].GetValue<DateTime>(),
                            WorkOrderCloseDate = reader["WorkOrderCloseDate"].GetValue<DateTime>(),
                            WorkOrderOpenDate = reader["WorkOrderOpenDate"].GetValue<DateTime>(),
                            DealerName = reader["DealerName"].GetValue<string>(),
                            WaybillDate = reader["WaybillDate"].GetValue<DateTime>(),
                            ShippedQuantity = reader["ShippedQuantity"].GetValue<decimal>(),
                            WorkOrderNo = reader["WorkOrderNo"].GetValue<string>(),
                            IntervalDate = reader["IntervalDate"].GetValue<long>(),

                        };
                        dto.Add(sentPartUsageReport);
                    }
                    reader.Close();

                    totalCnt = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return dto;
        }

        public List<KilometerDistributionReport> GetKilometerDistributionReport(UserInfo user, KilometerDistributionFilterRequest filter, out int total)
        {
            var dto = new List<KilometerDistributionReport>();
            total = 0;

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_RPT_KM_DISTRIBUTION");
                db.AddInParameter(cmd, "P_GROUP_TYPE", DbType.Int32, MakeDbNull(filter.GroupType));
                db.AddInParameter(cmd, "P_WO_BEGIN_DATE", DbType.DateTime, MakeDbNull(filter.BeginDate));
                db.AddInParameter(cmd, "P_WO_END_DATE", DbType.DateTime, MakeDbNull(filter.EndDate));
                db.AddInParameter(cmd, "P_DEALER_ID_LIST", DbType.String, MakeDbNull(filter.DealerIdList));
                db.AddInParameter(cmd, "P_REGION_ID_LIST", DbType.String, MakeDbNull(filter.RegionIdList));
                db.AddInParameter(cmd, "P_VEHICLE_MODEL_LIST", DbType.String, MakeDbNull(filter.VehicleModelList));
                db.AddInParameter(cmd, "P_INDICATOR_TYPE_LIST", DbType.String, MakeDbNull(filter.ProcessTypeList));
                db.AddInParameter(cmd, "P_CUST_TYPE_LIST", DbType.String, MakeDbNull(filter.CustTypeList));
                db.AddInParameter(cmd, "P_CUSTOMER_ID_LIST", DbType.String, MakeDbNull(filter.CustomerIdList));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "FILTERED_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var kilometerDistributionReport = new KilometerDistributionReport
                        {

                            GroupName = reader["GroupName"].GetValue<string>(),
                            TotalWorkOrderCardNumber = reader["TotalWorkOrderCardNumber"].GetValue<int>(),
                            TotalWinNumber = reader["TotalWinNumber"].GetValue<int>(),
                            AverageKilometer = reader["AverageKilometer"].GetValue<double>(),
                            GroupType = reader["GroupType"].GetValue<int>(),
                            GROUPCODE = reader["GROUPCODE"].GetValue<string>(),
                            MaxKM = reader["MaxKM"].GetValue<int>()
                        };
                        dto.Add(kilometerDistributionReport);
                    }
                    reader.Close();

                    total = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return dto;

        }

        public List<CampaignSummaryReport> GetCampaignSummaryReport(UserInfo user, CampaignSummaryReportFilterRequest filter, out int totalCnt)
        {
            var dto = new List<CampaignSummaryReport>();
            totalCnt = 0;

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_RPT_CAMPAIGN_SUMMARY");
                cmd.CommandTimeout = 1200;
                db.AddInParameter(cmd, "P_GROUP_TYPE", DbType.Int32, MakeDbNull(filter.GroupType));
                db.AddInParameter(cmd, "P_CAMP_BEGIN_DATE", DbType.DateTime, MakeDbNull(filter.StartDate));
                db.AddInParameter(cmd, "P_CAMP_END_DATE", DbType.DateTime, MakeDbNull(filter.EndDate));
                db.AddInParameter(cmd, "P_DEALER_ID_LIST", DbType.String, MakeDbNull(filter.DealerIdList));
                db.AddInParameter(cmd, "P_REGION_ID_LIST", DbType.String, MakeDbNull(filter.RegionIdList));
                db.AddInParameter(cmd, "P_VEHICLE_MODEL_LIST", DbType.String, MakeDbNull(filter.VehicleModelList));
                db.AddInParameter(cmd, "P_CAMP_CODE_LIST", DbType.String, MakeDbNull(filter.CampaignCode));
                db.AddInParameter(cmd, "P_CURRENCY_CODE_LIST", DbType.String, MakeDbNull(filter.Currency));
                db.AddInParameter(cmd, "P_GUARANTEE_STAT", DbType.Int32, MakeDbNull(filter.IsWarranty));
                db.AddInParameter(cmd, "P_GUARANTEE_CONFIRM_START_DATE", DbType.DateTime, MakeDbNull(filter.GuaranteeConfirmStartDate));
                db.AddInParameter(cmd, "P_GUARANTEE_CONFIRM_END_DATE", DbType.DateTime, MakeDbNull(filter.GuaranteeConfirmEndDate));
                db.AddInParameter(cmd, "P_CAMPAIGN_STATUS", DbType.Int32, MakeDbNull(filter.CampaignStatus));
                db.AddInParameter(cmd, "P_IS_MUST", DbType.Int32, MakeDbNull(filter.IsMust));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var campaignSummaryReport = new CampaignSummaryReport
                        {

                            GroupCode = reader["GroupCode"].GetValue<string>(),
                            GroupName = reader["GroupName"].GetValue<string>(),
                            CampaignCode = reader["CampaignCode"].GetValue<string>(),
                            Description = reader["Description"].GetValue<string>(),
                            StartDate = reader["StartDate"].GetValue<DateTime>(),
                            EndDate = reader["EndDate"].GetValue<DateTime>(),
                            TotalCampaignInternalVehicle = reader["TotalCampaignInternalVehicle"].GetValue<int>(),
                            TotalCampaignUseVehicle = reader["TotalCampaignUseVehicle"].GetValue<int>(),
                            CampaignUseVehicle = reader["CampaignUseVehicle"].GetValue<int>(),
                            Application = reader["Application"].GetValue<float>(),
                            WorkerAmount = reader["WorkerAmount"].GetValue<double>(),
                            BitMount = reader["BitMount"].GetValue<double>(),
                            Currency = reader["Currency"].GetValue<string>(),
                            GroupType = reader["GroupType"].GetValue<int>(),
                            //Orderqty = reader["Orderqty"].GetValue<int>(),
                            ReturnedQty = reader["ReturnedQty"].GetValue<int>()
                        };
                        dto.Add(campaignSummaryReport);
                    }
                    reader.Close();

                }
                totalCnt = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return dto;

        }

        public List<GuaranteeReport> GetGuaranteeReport(GuaranteeReportFilterRequest filter, out int total)
        {
            var dto = new List<GuaranteeReport>();
            total = 0;

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_RPT_GUARANTEE");
                db.AddInParameter(cmd, "YEAR", DbType.String, MakeDbNull(filter.Year));
                db.AddInParameter(cmd, "MONTH", DbType.String, MakeDbNull(filter.Month));
                db.AddInParameter(cmd, "SERVICE_IDS", DbType.String, MakeDbNull(filter.DealerIdList));
                db.AddInParameter(cmd, "REGION_IDS", DbType.String, MakeDbNull(filter.RegionIdList));
                db.AddInParameter(cmd, "INDICATOR_TYPES", DbType.String, MakeDbNull(filter.IndicatorType));
                db.AddInParameter(cmd, "PROCESS_TYPES", DbType.String, MakeDbNull(filter.ProcessType));
                db.AddInParameter(cmd, "GUARANTEE_CATEGORIES", DbType.String, MakeDbNull(filter.GuaranteeCategory));
                db.AddInParameter(cmd, "VIN_NO", DbType.String, MakeDbNull(filter.VinNo));
                db.AddInParameter(cmd, "COMPLETE_USER", DbType.String, MakeDbNull(filter.ConfirmedUser));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var guaranteeReport = new GuaranteeReport
                        {

                            DealerName = reader["DealerName"].GetValue<string>(),
                            RegionName = reader["RegionName"].GetValue<string>(),
                            DEALER_SSID = reader["DEALER_SSID"].GetValue<string>(),
                            VEHICLE_VIN_NO = reader["VEHICLE_VIN_NO"].GetValue<string>(),
                            ID_WORK_ORDER = reader["ID_WORK_ORDER"].GetValue<string>(),
                            ID_WORK_ORDER_DETAIL = reader["ID_WORK_ORDER_DETAIL"].GetValue<string>(),
                            SSID_GUARANTEE = reader["SSID_GUARANTEE"].GetValue<string>(),
                            GuaranteeId = reader["GuaranteeId"].GetValue<string>(),
                            GuaranteeSeqNo = reader["GuaranteeSeqNo"].GetValue<string>(),
                            CATEGORY_LOOKVAL = reader["CATEGORY_LOOKVAL"].GetValue<string>(),
                            VEHICLE_KM = reader["VEHICLE_KM"].GetValue<string>(),
                            CAMPAIGN_CODE = reader["CAMPAIGN_CODE"].GetValue<string>(),
                            ID_VEHICLE = reader["ID_VEHICLE"].GetValue<string>(),
                            VEHICLE_PLATE = reader["VEHICLE_PLATE"].GetValue<string>(),
                            VEHICLE_NOTES = reader["VEHICLE_NOTES"].GetValue<string>(),
                            CENTER_NOTES = reader["CENTER_NOTES"].GetValue<string>(),
                            SPECIAL_NOTES = reader["SPECIAL_NOTES"].GetValue<string>(),
                            CustomerName = reader["CustomerName"].GetValue<string>(),
                            CODE = reader["CODE"].GetValue<string>(),
                            CODEDESC = reader["CODEDESC"].GetValue<string>(),
                            CREATE_DATE = reader["CREATE_DATE"].GetValue<DateTime>(),
                            VEHICLE_LEAVE_DATE = reader["VEHICLE_LEAVE_DATE"].GetValue<DateTime>(),
                            PDI_GIF = reader["PDI_GIF"].GetValue<string>(),
                            CRM_CUSTOMER_CUSTOMER_NAME = reader["CRM_CUSTOMER_CUSTOMER_NAME"].GetValue<string>(),
                            CRM_CUSTOMER_CUSTOMER_LASTNAME = reader["CRM_CUSTOMER_CUSTOMER_LASTNAME"].GetValue<string>(),
                            LABOUR_TOTAL_PRICE = reader["LABOUR_TOTAL_PRICE"].GetValue<decimal>(),
                            PARTS_TOTAL_PRICE = reader["PARTS_TOTAL_PRICE"].GetValue<decimal>(),
                            INDICATOR_TYPE_NAME = reader["INDICATOR_TYPE_NAME"].GetValue<string>(),
                            PROCESS_TYPE_NAME = reader["PROCESS_TYPE_NAME"].GetValue<string>(),
                            WEB_SERVICE_LABOUR_TOTAL_PRICE = reader["WEB_SERVICE_WARRANTY_LABOUR_PRICE"].GetValue<decimal>(),
                            WEB_SERVICE_PARTS_TOTAL_PRICE = reader["WEB_SERVICE_WARRANTY_PART_PRICE"].GetValue<decimal>(),
                        };
                        dto.Add(guaranteeReport);
                    }
                    reader.Close();

                    total = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return dto;


        }

        public List<WorkOrderPerformanceReport> GetWorkOrderPreformanceReport(WorkOrderPerformanceReportFilterRequest filter, out int total)
        {
            var dto = new List<WorkOrderPerformanceReport>();
            total = 0;

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_RPT_WORK_ORDER_CARD_PERFORMANCE");
                db.AddInParameter(cmd, "YEAR", DbType.String, MakeDbNull(filter.Year));
                db.AddInParameter(cmd, "MONTH", DbType.String, MakeDbNull(filter.Month));
                db.AddInParameter(cmd, "SERVICE_IDS", DbType.String, MakeDbNull(filter.DealerIdList));
                db.AddInParameter(cmd, "REGION_IDS", DbType.String, MakeDbNull(filter.RegionIdList));
                db.AddInParameter(cmd, "INDICATOR_TYPES", DbType.String, MakeDbNull(filter.IndicatorType));
                db.AddInParameter(cmd, "PROCESS_TYPES", DbType.String, MakeDbNull(filter.ProcessType));
                db.AddInParameter(cmd, "SEND_STATUS", DbType.String, MakeDbNull(filter.SendStatus));
                db.AddInParameter(cmd, "CONFIRM_STATUS", DbType.String, MakeDbNull(filter.ConfirmStatus));
                db.AddInParameter(cmd, "VIN_NO", DbType.String, MakeDbNull(filter.VinNo));
                db.AddInParameter(cmd, "USER", DbType.String, MakeDbNull(filter.User));
                db.AddInParameter(cmd, "WORK_ORDER_NO", DbType.String, MakeDbNull(filter.WorkOrderNo));
                db.AddInParameter(cmd, "GIF_NO", DbType.String, MakeDbNull(filter.GifNo));
                db.AddInParameter(cmd, "VEHICLE_LEAVE_DATE", DbType.DateTime, MakeDbNull(filter.VehicleLeaveDate));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var workOrderPerformanceReport = new WorkOrderPerformanceReport
                        {

                            DealerSSID = reader["DealerSSID"].GetValue<string>(),
                            DealerName = reader["DealerName"].GetValue<string>(),
                            RegionName = reader["RegionName"].GetValue<string>(),
                            VinNo = reader["VinNo"].GetValue<string>(),
                            VehicleId = reader["VehicleId"].GetValue<string>(),
                            WorkOderID = reader["WorkOderID"].GetValue<string>(),
                            WorkOrderDetailID = reader["WorkOrderDetailID"].GetValue<string>(),
                            WorkOrderOpenedDate = reader["WorkOrderOpenedDate"].GetValue<DateTime>(),
                            GuaranteeSeq = reader["GuaranteeSeq"].GetValue<string>(),
                            RequestDate = reader["RequestDate"].GetValue<DateTime>(),
                            ApproveDate = reader["ApproveDate"].GetValue<DateTime>(),
                            RequestApproveBetweenMinute = reader["RequestApproveBetweenMinute"].GetValue<string>(),
                            IndicatorType = reader["IndicatorType"].GetValue<string>(),
                            ProcessType = reader["ProcessType"].GetValue<string>(),
                            RequestWarrantyStatu = reader["RequestWarrantyStatu"].GetValue<string>(),
                            ApprovaWarrantyStatu = reader["ApprovaWarrantyStatu"].GetValue<string>(),
                            GifNo = reader["GifNo"].GetValue<string>(),
                            ProcessOfUser = reader["ProcessOfUser"].GetValue<string>()
                        };
                        dto.Add(workOrderPerformanceReport);

                    }
                    reader.Close();

                    total = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return dto;

        }

        public List<PartStockReport> GetPartStockReport(UserInfo user, PartStockFilterRequest filter, out decimal totalPrice, out int totalCnt)
        {
            var dto = new List<PartStockReport>();
            totalCnt = 0;
            totalPrice = 0;

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_RPT_PART_STOCK_BY_DATE");
                db.AddInParameter(cmd, "STOCK_TYPE_ID_LIST", DbType.String, MakeDbNull(filter.StockTypeIdList));
                db.AddInParameter(cmd, "DEALER_ID_LIST", DbType.String, MakeDbNull(filter.DealerIdList));
                db.AddInParameter(cmd, "REGION_ID_LIST", DbType.String, MakeDbNull(filter.RegionIdList));
                db.AddInParameter(cmd, "IS_ORIGINAL", DbType.String, filter.IsOriginal);
                db.AddInParameter(cmd, "DATE", DbType.DateTime, MakeDbNull(filter.Date));
                db.AddInParameter(cmd, "PART_ID", DbType.String, MakeDbNull(filter.PartId));
                db.AddInParameter(cmd, "VEHICLE_GROUP_ID_LIST", DbType.String, MakeDbNull(filter.VehicleGroupIdList));
                db.AddInParameter(cmd, "PART_CODE_LIST", DbType.String, MakeDbNull(filter.PartCodeList));
                db.AddInParameter(cmd, "CURRENTY_CODE", DbType.String, MakeDbNull(filter.Currency));
                db.AddInParameter(cmd, "PART_CLASS_LIST", DbType.String, MakeDbNull(filter.PartClassCodes));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "FILTERED_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "TOTAL_PRICE", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var partStockReport = new PartStockReport
                        {

                            DealerRegionName = reader["DealerRegionName"].GetValue<string>(),
                            DealerName = reader["DealerName"].GetValue<string>(),
                            PartCode = reader["PartCode"].GetValue<string>(),
                            StockTypeName = reader["StockTypeName"].GetValue<string>(),
                            IsOriginalPart = reader["IsOriginalPart"].GetValue<bool>(),
                            PartSectionName = reader["PartSectionName"].GetValue<string>(),
                            PartClassName = reader["PartClassName"].GetValue<string>(),
                            PackageQuantity = reader["PackageQuantity"].GetValue<decimal>(),
                            CriticalStockQuantity = reader["CriticalStockQuantity"].GetValue<decimal>(),
                            MinStockQuantity = reader["MinStockQuantity"].GetValue<decimal>(),
                            MaxStockQunatity = reader["MaxStockQunatity"].GetValue<decimal>(),
                            StartupQuantity = reader["StartupQuantity"].GetValue<decimal>(),
                            AvgDealerPrice = reader["AvgDealerPrice"].GetValue<decimal>(),
                            StockQuantity = reader["StockQuantity"].GetValue<decimal>(),
                            StockAge = reader["StockAge"].GetValue<decimal>(),
                            PartName = reader["PartName"].GetValue<string>(),
                            Currency = reader["Currency"].GetValue<string>(),
                            PartId = reader["PartId"].GetValue<long>(),
                            TotalAmount = reader["TotalAmount"].GetValue<decimal>()
                        };
                        dto.Add(partStockReport);
                    }
                    reader.Close();

                    totalCnt = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
                    totalPrice = db.GetParameterValue(cmd, "TOTAL_PRICE").GetValue<int>();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return dto;

        }

        public List<ChargePerCarReport> GetChargePerCarReport(UserInfo user, ChargePerCarFilterRequest filter, out int totalCnt)
        {
            var dto = new List<ChargePerCarReport>();
            totalCnt = 0;
            dynamic inGuaratee = filter.InGuarantee == null ? (dynamic)DBNull.Value : filter.InGuarantee == 1;

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_RPT_CHARGE_PER_CAR");
                db.AddInParameter(cmd, "DEALER_ID_LIST", DbType.String, MakeDbNull(filter.DealerIdList));
                db.AddInParameter(cmd, "REGION_ID_LIST", DbType.String, MakeDbNull(filter.RegionIdList));
                db.AddInParameter(cmd, "CUSTOMER_ID_LIST", DbType.String, MakeDbNull(filter.CustomerIdList));
                db.AddInParameter(cmd, "VEHICLE_TYPE_LIST", DbType.String, MakeDbNull(filter.VehicleTypeIdList));
                db.AddInParameter(cmd, "VEHICLE_MODEL_LIST", DbType.String, MakeDbNull(filter.VehicleModelIdList));
                db.AddInParameter(cmd, "START_DATE", DbType.Date, MakeDbNull(filter.StartDate));
                db.AddInParameter(cmd, "END_DATE", DbType.Date, MakeDbNull(filter.EndDate));
                db.AddInParameter(cmd, "VIN_NO", DbType.String, MakeDbNull(filter.VinNo));
                db.AddInParameter(cmd, "PROCESS_TYPE", DbType.String, MakeDbNull(filter.ProcessTypeIdList));
                db.AddInParameter(cmd, "IN_GUARANTEE", DbType.Int32, inGuaratee);
                db.AddInParameter(cmd, "GUARANTEE_CATEGORIES", DbType.String, MakeDbNull(filter.GuaranteeCategories));
                db.AddInParameter(cmd, "GUARANTEE_CONFIRM_DATE", DbType.Date, MakeDbNull(filter.GuaranteeConfirmDate));
                db.AddInParameter(cmd, "CURRENTY_CODE", DbType.String, MakeDbNull(filter.Currency));
                db.AddInParameter(cmd, "MIN_KM", DbType.Int32, (filter.MinKM == null) ? MakeDbNull(null) : filter.MinKM);
                db.AddInParameter(cmd, "MAX_KM", DbType.Int32, (filter.MaxKM == null) ? MakeDbNull(null) : filter.MaxKM);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "FILTERED_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var chargePerCarReport = new ChargePerCarReport
                        {

                            NameSurname = reader["NameSurname"].GetValue<string>(),
                            DealerName = reader["DealerName"].GetValue<string>(),
                            DealerRegionName = reader["DealerRegionName"].GetValue<string>(),
                            TypeName = reader["TypeName"].GetValue<string>(),
                            ModelName = reader["ModelName"].GetValue<string>(),
                            VinNo = reader["VinNo"].GetValue<string>(),
                            ProcessTypeCode = reader["ProcessTypeCode"].GetValue<string>(),
                            CurrencyCode = reader["CurrencyCode"].GetValue<string>(),
                            InGuarantee = reader["InGuarantee"].GetValue<string>(),
                            WorkOrderCount = reader["WorkOrderCount"].GetValue<int>(),
                            WorkOrderDetailCount = reader["WorkOrderDetailCount"].GetValue<int>(),
                            CarCount = reader["CarCount"].GetValue<int>(),
                            PartPrice = reader["PartPrice"].GetValue<decimal>(),
                            LabourPrice = reader["LabourPrice"].GetValue<decimal>(),
                            CategoryLookval = reader["CategoryLookval"].GetValue<string>(),
                            ApproveDate = reader["ApproveDate"].GetValue<DateTime>(),
                            TotalAmount = reader["TotalAmount"].GetValue<decimal>(),
                            WorkOrderId = reader["WorkOrderId"].GetValue<int>(),
                            WorkOrderDetId = reader["WorkOrderDetId"].GetValue<int>(),
                            DealerId = reader["DealerId"].GetValue<int>(),
                            DealerRegionId = reader["DealerRegionId"].GetValue<int>(),
                            CustomerId = reader["CustomerId"].GetValue<int>(),
                            VehicleTypeId = reader["VehicleTypeId"].GetValue<int>(),
                            VehicleModelCode = reader["VehicleModelCode"].GetValue<string>(),
                            IdVehicle = reader["IdVehicle"].GetValue<int>()
                        };
                        dto.Add(chargePerCarReport);
                    }
                    reader.Close();

                    totalCnt = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return dto;
        }

        public List<WorkOrderDetailReport> GetWorkOrderDetailByWorkOrderParameters(ChargeWorkOrderDetailFilterRequest filter, out int totalCnt)
        {
            var dto = new List<WorkOrderDetailReport>();
            totalCnt = 0;

            try
            {

                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_WORK_ORDER_DETAIL_BY_WORK_ORDER_PARAMETERS");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(filter.DealerId));
                db.AddInParameter(cmd, "ID_DEALER_REGION", DbType.Int32, MakeDbNull(filter.DealerRegionId));
                db.AddInParameter(cmd, "CUSTOMER_ID", DbType.Int32, MakeDbNull(filter.CustomerId));
                db.AddInParameter(cmd, "VEHICLE_TYPE_ID", DbType.Int32, MakeDbNull(filter.VehicleTypeId));
                db.AddInParameter(cmd, "MODEL_CODE", DbType.String, MakeDbNull(filter.ModelCode));
                db.AddInParameter(cmd, "PROCESS_TYPE_LIST", DbType.String, MakeDbNull(filter.ProcessTypeIdList));
                db.AddInParameter(cmd, "START_DATE", DbType.Date, MakeDbNull(filter.StartDate));
                db.AddInParameter(cmd, "END_DATE", DbType.Date, MakeDbNull(filter.EndDate));
                db.AddInParameter(cmd, "CURRENCY_CODE_LIST", DbType.String, MakeDbNull(filter.Currency));
                db.AddInParameter(cmd, "VIN_NO", DbType.String, MakeDbNull(filter.VinNo));
                db.AddInParameter(cmd, "IN_GUARANTEE", DbType.Int32, MakeDbNull(filter.InGuarantee));
                db.AddInParameter(cmd, "GUARANTEE_CATEGORIES", DbType.String, MakeDbNull(filter.GuaranteeCategories));
                db.AddInParameter(cmd, "GUARANTEE_CONFIRM_DATE", DbType.Date, MakeDbNull(filter.GuaranteeConfirmDate));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize)); ;
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var workOrderDetailReport = new WorkOrderDetailReport
                        {

                            DealerName = reader["DealerName"].GetValue<string>(),
                            DealerRegionName = reader["DealerRegionName"].GetValue<string>(),
                            WorkOrderId = reader["WorkOrderId"].GetValue<int>(),
                            WorkOrderDetailId = reader["WorkOrderDetailId"].GetValue<int>(),
                            GIF = reader["GIF"].GetValue<string>(),
                            VinNo = reader["VinNo"].GetValue<string>(),                        
                            EngineNo = reader["EngineNo"].GetValue<string>(),
                            VehicleWarrantyStartDate = reader["VehicleWarrantyStartDate"].GetValue<DateTime>(),
                            CreateDate = reader["CreateDate"].GetValue<DateTime>(),
                            VehicleKm = reader["VehicleKm"].GetValue<decimal>(),
                            DealerId = reader["DeailerId"].GetValue<int>(),
                            DealerRegionId = reader["DealerRegionId"].GetValue<int>(),
                            CustomerId = reader["CustomerId"].GetValue<int>(),
                            PartPrice = reader["PartPrice"].GetValue<decimal>(),
                            LabourPrice = reader["LabourPrice"].GetValue<decimal>(),
                            CurrencyCode = reader["CurrencyCode"].GetValue<string>(),
                            PartCost = reader["PartCost"].GetValue<decimal>(),
                            ApproveDate = reader["ApproveDate"].GetValue<DateTime>(),
                            Category = reader["Category"].GetValue<string>(),
                            TotalAmount = reader["TotalAmount"].GetValue<decimal>()

                        };
                        dto.Add(workOrderDetailReport);
                    }
                    reader.Close();

                    totalCnt = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return dto;

        }


        public List<PartStockActivityReport> GetPartStockActivityReport(PartStockActivityFilterRequest filter, out int totalCnt)
        {
            var dto = new List<PartStockActivityReport>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_RPT_PART_ACTIVITY_REPORT");
                db.AddInParameter(cmd, "YEAR", DbType.Int32, MakeDbNull(filter.Year));
                db.AddInParameter(cmd, "ID_DEALER", DbType.String, MakeDbNull(filter.DealerIdList));
                db.AddInParameter(cmd, "IN_GUARANTEE", DbType.Int32, MakeDbNull(filter.InGuarantee));
                db.AddInParameter(cmd, "PART_CODE", DbType.String, MakeDbNull(filter.PartCode));
                db.AddInParameter(cmd, "PROCESS_TYPE", DbType.String, MakeDbNull(filter.ProcessType));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "FILTERED_TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var partStockActivityReport = new PartStockActivityReport
                        {
                            ProcessType = reader["ProcessType"].GetValue<int>(),
                            PartCode = reader["PartCode"].GetValue<string>(),
                            Quantity = reader["Quantity"].GetValue<decimal>(),
                            MonthIndex = reader["MonthIndex"].GetValue<int>(),
                            M1 = reader["M1"].GetValue<decimal>(),
                            M2 = reader["M2"].GetValue<decimal>(),
                            M3 = reader["M3"].GetValue<decimal>(),
                            M4 = reader["M4"].GetValue<decimal>(),
                            M5 = reader["M5"].GetValue<decimal>(),
                            M6 = reader["M6"].GetValue<decimal>(),
                            M7 = reader["M7"].GetValue<decimal>(),
                            M8 = reader["M8"].GetValue<decimal>(),
                            M9 = reader["M9"].GetValue<decimal>(),
                            M10 = reader["M10"].GetValue<decimal>(),
                            M11 = reader["M11"].GetValue<decimal>(),
                            M12 = reader["M12"].GetValue<decimal>()

                        };
                        dto.Add(partStockActivityReport);
                    }
                    reader.Close();

                    totalCnt = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return dto;
        }

        public List<SparePartControlReport> GetSparePartControlReport(UserInfo user, PartStockFilterRequest filter, out int totalCnt)
        {
            var dto = new List<SparePartControlReport>();
            totalCnt = 0;

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_RPT_SPARE_PART_CONTROL");
                db.AddInParameter(cmd, "P_DEALER_ID_LIST", DbType.String, MakeDbNull(filter.DealerIdList));
                db.AddInParameter(cmd, "P_REGION_ID_LIST", DbType.String, MakeDbNull(filter.RegionIdList));
                db.AddInParameter(cmd, "P_PART_CODE", DbType.String, MakeDbNull(filter.PartCode));
                db.AddInParameter(cmd, "P_PART_NAME", DbType.String, MakeDbNull(filter.PartName));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                db.AddInParameter(cmd, "IS_ORIGINAL", DbType.Int32, filter.IsOriginal);
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "FILTERED_TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var sparePartControlReport = new SparePartControlReport
                        {

                            DealerName = reader["DealerName"].GetValue<string>(),
                            RegionName = reader["RegionName"].GetValue<string>(),
                            PartCode = reader["PartCode"].GetValue<string>(),
                            PartName = reader["PartName"].GetValue<string>(),
                            UsableQuantity = reader["UsableQuantity"].GetValue<decimal>(),
                            ReserveQuantity = reader["ReserveQuantity"].GetValue<decimal>(),
                            BlockQuantity = reader["BlockQuantity"].GetValue<decimal>(),
                            OpenOrderCount = reader["OpenOrderCount"].GetValue<int>(),
                            LastOpenOrderDate = reader["LastOpenOrderDate"].GetValue<DateTime>(),
                            TotalPrice = reader["TotalPrice"].GetValue<decimal>(),
                            CurrencyCode = reader["CurrencyCode"].GetValue<string>()
                        };
                        dto.Add(sparePartControlReport);
                    }
                    reader.Close();

                    totalCnt = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return dto;

        }


        public List<FixAssetInventoryReport> GetFixAssetInventoryReport(UserInfo user, FixAssetInventoryReportFilterRequest filter, out int totalCnt)
        {
            var dto = new List<FixAssetInventoryReport>();
            totalCnt = 0;

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_RPT_FIX_ASSET_INVENTORY");
                db.AddInParameter(cmd, "PART_CODE", DbType.String, MakeDbNull(filter.PartCode));
                db.AddInParameter(cmd, "PART_NAME", DbType.String, MakeDbNull(filter.PartName));
                db.AddInParameter(cmd, "EQUIPMENT_TYPE_ID", DbType.Int32, MakeDbNull(filter.EquipmentTypeId));
                db.AddInParameter(cmd, "DEALER_ID_LIST", DbType.String, MakeDbNull(filter.DealerIdList));
                db.AddInParameter(cmd, "DEALER_REGION_ID_LIST", DbType.String, MakeDbNull(filter.RegionIdList));
                db.AddInParameter(cmd, "VEHICLE_GROUP_ID", DbType.Int32, MakeDbNull(filter.VehicleGroupId));
                db.AddInParameter(cmd, "STATUS_ID", DbType.Int32, MakeDbNull(filter.StatusId));
                db.AddInParameter(cmd, "FIX_ASSET_NAME", DbType.String, MakeDbNull(null));
                db.AddInParameter(cmd, "SERIAL_NO", DbType.String, MakeDbNull(null));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var fixAssetInventoryReport = new FixAssetInventoryReport
                        {
                            EquipmentTypeName = reader["EquipmentTypeName"].GetValue<string>(),
                            DealerName = reader["DealerName"].GetValue<string>(),
                            RegionName = reader["RegionName"].GetValue<string>(),
                            PartCode = reader["PartCode"].GetValue<string>(),
                            PartName = reader["PartName"].GetValue<string>(),
                            FixAssetCode = reader["FixAssetCode"].GetValue<string>(),
                            FixAssetName = reader["FixAssetName"].GetValue<string>(),
                            SerialNo = reader["SerialNo"].GetValue<string>(),
                            VehicleGroupName = reader["VehicleGroupName"].GetValue<string>(),
                            StatusName = reader["StatusName"].GetValue<string>(),
                            CreateDate = reader["CreateDate"].GetValue<DateTime>()
                        };
                        dto.Add(fixAssetInventoryReport);
                    }
                    reader.Close();

                    totalCnt = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return dto;

        }

        public List<WorkOrderPartsTotalReport> GetWorkOrderPartsTotalReport(UserInfo user, WorkOrderPartsTotalReportFilterRequest filter, out int totalCnt)
        {
            var dto = new List<WorkOrderPartsTotalReport>();
            totalCnt = 0;

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_RPT_WO_PARTS_TOTAL");
                db.AddInParameter(cmd, "P_VEHICLE_LEAVE_YEAR", DbType.Int32, MakeDbNull(filter.Year));
                db.AddInParameter(cmd, "P_VEHICLE_LEAVE_MONTH", DbType.Int32, MakeDbNull(filter.Month));
                db.AddInParameter(cmd, "P_DEALER_ID_LIST", DbType.String, MakeDbNull(filter.DealerIdList));
                db.AddInParameter(cmd, "P_REGION_ID_LIST", DbType.String, MakeDbNull(filter.RegionIdList));
                db.AddInParameter(cmd, "P_INDICATOR_TYPE_LIST", DbType.String, MakeDbNull(filter.IndicatorType));
                db.AddInParameter(cmd, "P_GUARANTEE_STAT", DbType.Int32, MakeDbNull(filter.InGuarantee));
                db.AddInParameter(cmd, "P_CURRENCY_CODE_LIST", DbType.String, MakeDbNull(filter.Currency));
                db.AddInParameter(cmd, "P_STOCK_TYPE", DbType.Int32, MakeDbNull(filter.IsPaid));
                db.AddInParameter(cmd, "P_IS_ORIGINAL", DbType.Int32, MakeDbNull(filter.IsOriginal));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "PART_CODE", DbType.String, MakeDbNull(filter.IndicatorType));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var workOrderPartsTotalReport = new WorkOrderPartsTotalReport()
                        {
                            PartCode = reader["PartCode"].GetValue<string>(),
                            PartName = reader["PartName"].GetValue<string>(),
                            PaidCount = reader["PaidCount"].GetValue<decimal>(),
                            PaidAmount = reader["PaidAmount"].GetValue<decimal>(),
                            FreeCount = reader["FreeCount"].GetValue<decimal>(),
                            FreeAmount = reader["FreeAmount"].GetValue<decimal>(),
                            Currency = reader["Currency"].GetValue<string>(),
                            AvgKm = reader["AvgKm"].GetValue<string>(),
                            MinKm = reader["MinKm"].GetValue<string>(),
                            MaxKm = reader["MaxKm"].GetValue<string>(),
                            AmountPercent = reader["AmountPercent"].GetValue<decimal>(),
                            CountPercent = reader["CountPercent"].GetValue<decimal>(),
                            CustomerAmount = reader["CustomerAmount"].GetValue<decimal>()
                        };
                        dto.Add(workOrderPartsTotalReport);
                    }
                    reader.Close();

                    totalCnt = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return dto;

        }

        public List<InvoiceInfoReport> ListInvoiceInfoReport(UserInfo user, InvoiceInfoFilterRequest filter, out int total)
        {
            var dto = new List<InvoiceInfoReport>();
            total = 0;

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_RPT_INVOICE_INFO");
                db.AddInParameter(cmd, "DEALER__REGION_ID_LIST", DbType.String, MakeDbNull(filter.RegionIdList));
                db.AddInParameter(cmd, "DEALER_ID_LIST", DbType.String, MakeDbNull(filter.DealerIdList));
                db.AddInParameter(cmd, "INVOICE_CREATE_BEGIN_DATE", DbType.Date, MakeDbNull(filter.InvoiceCreateBeginDate));
                db.AddInParameter(cmd, "INVOICE_CREATE_END_DATE", DbType.Date, MakeDbNull(filter.InvoiceCreateEndDate));
                db.AddInParameter(cmd, "INVOICE_TYPE_LIST", DbType.String, MakeDbNull(filter.InvoiceType));
                db.AddInParameter(cmd, "INVOICE_NO", DbType.String, MakeDbNull(filter.InvoiceNo));
                db.AddInParameter(cmd, "INVOICE_DOCUMENT_LIST", DbType.String, MakeDbNull(filter.InvoiceDocumentType));
                db.AddInParameter(cmd, "CUSTOMER_TYPE_LIST", DbType.String, MakeDbNull(filter.CustomerType));
                db.AddInParameter(cmd, "GOVERMENT_TYPE_LIST", DbType.String, MakeDbNull(filter.GovermentType));
                db.AddInParameter(cmd, "COMPANY_TYPE_LIST", DbType.String, MakeDbNull(filter.CompanyType));
                db.AddInParameter(cmd, "CUSTOMER_ID_LIST", DbType.String, MakeDbNull(filter.CustomerNameList));
                db.AddInParameter(cmd, "HAS_WITHOLD", DbType.Int32, filter.HasWithold);
                db.AddInParameter(cmd, "VAT_TYPE_LIST", DbType.String, MakeDbNull(filter.VatType));
                db.AddInParameter(cmd, "LABOUR_ID_LIST", DbType.String, MakeDbNull(filter.LabourIdList));
                db.AddInParameter(cmd, "SHOW_INVOICE_DETAILS", DbType.Int32, MakeDbNull(filter.ShowInvoiceDetails));
                db.AddInParameter(cmd, "PART_CODE", DbType.String, MakeDbNull(filter.PartCode));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "FILTERED_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var invoiceInfoReport = new InvoiceInfoReport
                        {

                            WorkOrderInvoiceId = reader["WorkOrderInvoiceId"].GetValue<long>(),
                            SAPCode = reader["SAPCode"].GetValue<string>(),
                            DealerName = reader["DealerName"].GetValue<string>(),
                            RegionName = reader["RegionName"].GetValue<string>(),
                            CompanyType = reader["CompanyType"].GetValue<string>(),
                            GovermentType = reader["GovermentType"].GetValue<string>(),
                            TCIdentityNo = reader["TCIdentityNo"].GetValue<string>(),
                            PassportNo = reader["PassportNo"].GetValue<string>(),
                            TaxOffice = reader["TaxOffice"].GetValue<string>(),
                            TaxNo = reader["TaxNo"].GetValue<string>(),
                            CustomerName = reader["CustomerName"].GetValue<string>(),
                            CustomerSurname = reader["CustomerSurname"].GetValue<string>(),
                            CustomerType = reader["CustomerType"].GetValue<string>(),
                            HasWithold = reader["HasWithold"].GetValue<string>(),
                            AddressType = reader["AddressType"].GetValue<string>(),
                            City = reader["City"].GetValue<string>(),
                            Town = reader["Town"].GetValue<string>(),
                            Country = reader["Country"].GetValue<string>(),
                            ZipCode = reader["ZipCode"].GetValue<string>(),
                            VehicleGroup = reader["VehicleGroup"].GetValue<string>(),
                            VehicleModel = reader["VehicleModel"].GetValue<string>(),
                            VehicleCode = reader["VehicleCode"].GetValue<string>(),
                            VehicleType = reader["VehicleType"].GetValue<string>(),
                            VinNo = reader["VinNo"].GetValue<string>(),
                            EngineType = reader["EngineType"].GetValue<string>(),
                            EngineNo = reader["EngineNo"].GetValue<string>(),
                            Plate = reader["Plate"].GetValue<string>(),
                            ModelYear = reader["ModelYear"].GetValue<string>(),
                            VehicleCustomer = reader["VehicleCustomer"].GetValue<string>(),
                            Description = reader["Description"].GetValue<string>(),
                            WorkOrderId = reader["WorkOrderId"].GetValue<long>(),
                            WorkOrderDetailId = reader["WorkOrderDetailId"].GetValue<long>(),
                            InvoiceSerialNo = reader["InvoiceSerialNo"].GetValue<string>(),
                            InvoiceNo = reader["InvoiceNo"].GetValue<string>(),
                            InvoiceDate = reader["InvoiceDate"].GetValue<DateTime>(),
                            InvoiceType = reader["InvoiceType"].GetValue<string>(),
                            PartCode = reader["PartCode"].GetValue<string>(),
                            PartName = reader["PartName"].GetValue<string>(),
                            Quantity = reader["Quantity"].GetValue<decimal>(),
                            Unit = reader["Unit"].GetValue<string>(),
                            VatRatio = reader["VatRatio"].GetValue<decimal>(),
                            DiscountRatio = reader["DiscountRatio"].GetValue<decimal>(),
                            UnitPrice = reader["UnitPrice"].GetValue<decimal>(),
                            TotalPrice = reader["TotalPrice"].GetValue<decimal>(),
                            InvoiceLabel = reader["InvoiceLabel"].GetValue<string>(),
                            LabourName = reader["LabourName"].GetValue<string>(),
                            LabourCode = reader["LabourCode"].GetValue<string>(),
                            LabourPrice = reader["LabourPrice"].GetValue<decimal>(),
                            Type = reader["Type"].GetValue<int>(),
                            DealerId = reader["DealerId"].GetValue<int>()

                        };
                        dto.Add(invoiceInfoReport);
                    }
                    reader.Close();
                }

                total = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return dto;

        }

        public List<PersonnelInfoReport> ListPersonnelInfoReport(UserInfo user, PersonnelInfoReportFilterRequest filter, out int totalCnt)
        {
            var dto = new List<PersonnelInfoReport>();
            totalCnt = 0;

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_RPT_PERSONNEL_INFO");
                db.AddInParameter(cmd, "DEALER_ID_LIST", DbType.String, MakeDbNull(filter.DealerIdList));
                db.AddInParameter(cmd, "USER_CODE_LIST", DbType.String, MakeDbNull(filter.UserCodeList));
                db.AddInParameter(cmd, "IDENTITY_NO_LIST", DbType.String, MakeDbNull(filter.IdentityNoList));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, filter.IsActive);
                db.AddInParameter(cmd, "TITLE_LIST", DbType.String, MakeDbNull(filter.TitleList));
                db.AddInParameter(cmd, "SHOW_EDUCATION_DETAILS", DbType.Byte, filter.ShowEducationDetails);
                db.AddInParameter(cmd, "EDUCATION_CODE_LIST", DbType.String, MakeDbNull(filter.EducationCode));
                db.AddInParameter(cmd, "EDUCATION_NAME_LIST", DbType.String, MakeDbNull(filter.EducationName));
                db.AddInParameter(cmd, "MODEL_CODE_LIST", DbType.String, MakeDbNull(filter.VehicleModel));
                db.AddInParameter(cmd, "EDUCATION_TYPE_LIST", DbType.String, MakeDbNull(filter.EducationType));
                db.AddInParameter(cmd, "EDUCATION_BEGIN_DATE", DbType.Date, MakeDbNull(filter.EducationBeginDate));
                db.AddInParameter(cmd, "EDUCATION_END_DATE", DbType.Date, MakeDbNull(filter.EducationEndDate));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "FILTERED_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var personnelInfoReport = new PersonnelInfoReport
                        {

                            RegionName = reader["RegionName"].GetValue<string>(),
                            DealerName = reader["DealerName"].GetValue<string>(),
                            DealerSAPCode = reader["DealerSAPCode"].GetValue<string>(),
                            CreateDate = reader["CreateDate"].GetValue<DateTime>(),
                            UserCode = reader["UserCode"].GetValue<string>(),
                            FirstName = reader["FirstName"].GetValue<string>(),
                            MidName = reader["MidName"].GetValue<string>(),
                            LastName = reader["LastName"].GetValue<string>(),
                            IdentityNo = reader["IdentityNo"].GetValue<string>(),
                            IsActive = reader["IsActive"].GetValue<string>(),
                            HireDate = reader["HireDate"].GetValue<DateTime>(),
                            LeaveDate = reader["LeaveDate"].GetValue<DateTime?>(),
                            Gender = reader["Gender"].GetValue<string>(),
                            BirthDate = reader["BirthDate"].GetValue<DateTime>(),
                            Title = reader["Title"].GetValue<string>(),
                            EducationCode = reader["EducationCode"].GetValue<string>(),
                            EducationName = reader["EducationName"].GetValue<string>(),
                            VehicleModel = reader["VehicleModel"].GetValue<string>(),
                            EducationType = reader["EducationType"].GetValue<string>(),
                            EducationDate = reader["EducationDate"].GetValue<DateTime>(),
                            EducationGrade = reader["EducationGrade"].GetValue<string>(),
                            Phone = reader["Phone"].GetValue<string>(),
                            Mobile = reader["Mobile"].GetValue<string>(),
                            Extension = reader["Extension"].GetValue<string>(),
                            Email = reader["Email"].GetValue<string>(),
                            MaritialStatus = reader["MaritalStatus"].GetValue<string>(),
                            Address = reader["Address"].GetValue<string>(),
                            EducationDurationDay = reader["EducationDurationDay"].GetValue<int>(),
                            EducationDurationHour = reader["EducationDurationHour"].GetValue<int>(),
                            UserId = reader["UserId"].GetValue<int>()

                        };
                        dto.Add(personnelInfoReport);
                    }
                    reader.Close();

                    totalCnt = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return dto;

        }

        public List<VehicleServiceDurationReport> GetCarServiceDurationReport(UserInfo user, VehicleServiceDurationFilterRequest filter, out int totalCnt)
        {
            var dto = new List<VehicleServiceDurationReport>();
            totalCnt = 0;

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_RPT_DEALER_SERVICE_DURATION_DETAIL");
                db.AddInParameter(cmd, "P_GROUP_TYPE", DbType.Int32, MakeDbNull(filter.GroupType));
                db.AddInParameter(cmd, "P_WO_BEGIN_DATE", DbType.DateTime, MakeDbNull(filter.BeginDate));
                db.AddInParameter(cmd, "P_WO_END_DATE", DbType.DateTime, MakeDbNull(filter.EndDate));
                db.AddInParameter(cmd, "P_DEALER_ID_LIST", DbType.String, MakeDbNull(filter.DealerIdList));
                db.AddInParameter(cmd, "P_REGION_ID_LIST", DbType.String, MakeDbNull(filter.RegionIdList));
                db.AddInParameter(cmd, "P_VEHICLE_MODEL_LIST", DbType.String, MakeDbNull(filter.VehicleModelList));
                db.AddInParameter(cmd, "P_VIN_NO", DbType.String, MakeDbNull(filter.VinNo));
                db.AddInParameter(cmd, "P_CUST_TYPE_LIST", DbType.String, MakeDbNull(filter.CustTypeList));
                db.AddInParameter(cmd, "P_CUSTOMER_ID_LIST", DbType.String, MakeDbNull(filter.CustomerIdList));
                db.AddInParameter(cmd, "P_WO_STATUS_LIST", DbType.String, MakeDbNull(filter.StatusIdList));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddInParameter(cmd, "IS_DETAIL", DbType.Byte, MakeDbNull(filter.IsDetail));
                db.AddInParameter(cmd, "group_type_VAL", DbType.String, MakeDbNull(filter.GroupTypeVal));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var vehicleServiceDurationReport = new VehicleServiceDurationReport
                        {
                            VinNo = reader["VinNo"].GetValue<string>(),
                            StartDate = reader["StartDate"].GetValue<DateTime>(),
                            EndDate = reader["EndDate"].GetValue<DateTime>(),
                            GroupName = reader["GroupName"].GetValue<string>(),
                            TotalWorkOrderNumber = reader["TotalWorkOrderNumber"].GetValue<int>(),
                            TotalWaitingHour = reader["TotalWaitingHour"].GetValue<int>(),
                            TotalDay = reader["TotalDay"].GetValue<int>(),
                            AverageWaitingHour = reader["AverageWaitingHour"].GetValue<int>(),
                            WoDate = reader["WoDate"].GetValue<int>(),
                            WoTotalDay = reader["WoTotalDay"].GetValue<int>(),
                            WoAverageWaitingHour = reader["WoAverageWaitingHour"].GetValue<int>(),
                            group_type = reader["group_type"].GetValue<int>(),
                            KM = reader["VehicleKilometer"].GetValue<int>(),
                            VehicleId = reader["VehicleId"].GetValue<int>(),
                            WoDateTotal = reader["WoDateTotal"].GetValue<int>(),
                            AverageDay = reader["AverageDay"].GetValue<int>(),
                            TotalServiceHour = reader["TotalServiceHour"].GetValue<int>(),
                            TotalServiceDay = reader["TotalServiceDay"].GetValue<int>(),
                            AverageServiceHour = reader["AverageServiceHour"].GetValue<int>(),
                            AverageServiceDay = reader["AverageServiceDay"].GetValue<int>()
                        };
                        dto.Add(vehicleServiceDurationReport);
                    }
                    reader.Close();

                    totalCnt = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return dto;


        }

        public List<WorkOrderInfoModel> ListWorkOrderInfo(UserInfo user, WorkOrderInfoRequest filter, out int totalCnt)
        {
            var dto = new List<WorkOrderInfoModel>();
            totalCnt = 0;

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_RPT_DEALER_SERVICE_DURATION_DETAIL");
                db.AddInParameter(cmd, "P_GROUP_TYPE", DbType.Int32, MakeDbNull(filter.GroupType));
                db.AddInParameter(cmd, "P_WO_BEGIN_DATE", DbType.DateTime, MakeDbNull(filter.BeginDate));
                db.AddInParameter(cmd, "P_WO_END_DATE", DbType.DateTime, MakeDbNull(filter.EndDate));
                db.AddInParameter(cmd, "P_DEALER_ID_LIST", DbType.String, MakeDbNull(filter.DealerIdList));
                db.AddInParameter(cmd, "P_REGION_ID_LIST", DbType.String, MakeDbNull(filter.RegionIdList));
                db.AddInParameter(cmd, "P_VEHICLE_MODEL_LIST", DbType.String, MakeDbNull(filter.VehicleModelList));
                db.AddInParameter(cmd, "P_VIN_NO", DbType.String, MakeDbNull(filter.VinNo));
                db.AddInParameter(cmd, "P_CUST_TYPE_LIST", DbType.String, MakeDbNull(filter.CustTypeList));
                db.AddInParameter(cmd, "P_CUSTOMER_ID_LIST", DbType.String, MakeDbNull(filter.CustomerIdList));
                db.AddInParameter(cmd, "P_WO_STATUS_LIST", DbType.String, MakeDbNull(filter.StatusIdList));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddInParameter(cmd, "IS_DETAIL", DbType.Byte, MakeDbNull(filter.IsDetail));
                db.AddInParameter(cmd, "group_type_VAL", DbType.String, MakeDbNull(filter.GroupTypeVal));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                cmd.CommandTimeout = 300;
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var workOrderInfoModel = new WorkOrderInfoModel
                        {

                            DealerName = reader["DealerName"].GetValue<string>(),
                            GroupName = reader["GroupName"].GetValue<string>(),
                            WorkOrderId = reader["WorkOrderId"].GetValue<int>(),
                            VinNo = reader["VinNo"].GetValue<string>(),
                            WarrantyStartDate = reader["WarrantyStartDate"].GetValue<DateTime?>(),
                            WarrantyEndDate= reader["WarrantyEndDate"].GetValue<DateTime?>(),
                            StartDate = reader["StartDate"].GetValue<DateTime>(),
                            CloseDate = reader["CloseDate"].GetValue<DateTime>(),
                            EndDate = reader["EndDate"].GetValue<DateTime>(),
                            TotalWaitingHour = reader["TotalWaitingHour"].GetValue<int>(),
                            VehicleKilometer = reader["VehicleKilometer"].GetValue<string>(),
                            WoDate = reader["WoDate"].GetValue<int>(),
                            TotalWorkOrderNumber = reader["TotalWorkOrderNumber"].GetValue<int>(),
                            TotalDay = reader["TotalDay"].GetValue<int>(),
                            AverageWaitingHour = reader["AverageWaitingHour"].GetValue<int>(),
                            VehicleId = reader["VehicleId"].GetValue<int>()
                        };
                        dto.Add(workOrderInfoModel);
                    }
                    reader.Close();

                    totalCnt = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return dto;
        }

        public List<LaborCostPerVehicleReport> GetLaborCostPerVehicleReport(UserInfo user, LaborCostPerVehicleReportFilterRequest filter, out int total)
        {
            var dto = new List<LaborCostPerVehicleReport>();
            total = 0;

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_RPT_VEHICLE_LABOUR");
                dynamic inGuaratee = filter.InGuarantee == null ? (dynamic)DBNull.Value : filter.InGuarantee == 1;
                db.AddInParameter(cmd, "DEALER_ID_LIST", DbType.String, MakeDbNull(filter.DealerIdList));
                db.AddInParameter(cmd, "REGION_ID_LIST", DbType.String, MakeDbNull(filter.RegionIdList));
                db.AddInParameter(cmd, "CUSTOMER_ID_LIST", DbType.String, MakeDbNull(filter.CustomerIdList));
                db.AddInParameter(cmd, "VEHICLE_TYPE_LIST", DbType.String, MakeDbNull(filter.VehicleTypeIdList));
                db.AddInParameter(cmd, "VEHICLE_MODEL_LIST", DbType.String, MakeDbNull(filter.VehicleModelIdList));
                db.AddInParameter(cmd, "START_DATE", DbType.Date, MakeDbNull(filter.StartDate));
                db.AddInParameter(cmd, "END_DATE", DbType.Date, MakeDbNull(filter.EndDate));
                db.AddInParameter(cmd, "VIN_NO", DbType.String, MakeDbNull(filter.VinNo));
                db.AddInParameter(cmd, "PROCESS_TYPE", DbType.String, MakeDbNull(filter.ProcessTypeIdList));
                db.AddInParameter(cmd, "IN_GUARANTEE", DbType.Byte, inGuaratee);
                db.AddInParameter(cmd, "GUARANTEE_CATEGORIES", DbType.String, MakeDbNull(filter.GuaranteeCategories));
                db.AddInParameter(cmd, "GUARANTEE_CONFIRM_DATE", DbType.Date, MakeDbNull(filter.GuaranteeConfirmDate));
                db.AddInParameter(cmd, "CURRENTY_CODE", DbType.String, MakeDbNull(filter.Currency));
                db.AddInParameter(cmd, "MIN_KM", DbType.Int32, (filter.MinKM == null) ? MakeDbNull(null) : filter.MinKM);
                db.AddInParameter(cmd, "MAX_KM", DbType.Int32, (filter.MaxKM == null) ? MakeDbNull(null) : filter.MaxKM);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "FILTERED_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var laborCostPerVehicleReport = new LaborCostPerVehicleReport
                        {

                            NameSurname = reader["NameSurname"].GetValue<string>(),
                            DealerName = reader["DealerName"].GetValue<string>(),
                            DealerRegionName = reader["DealerRegionName"].GetValue<string>(),
                            TypeName = reader["TypeName"].GetValue<string>(),
                            ModelName = reader["ModelName"].GetValue<string>(),
                            VinNo = reader["VinNo"].GetValue<string>(),
                            ProcessTypeCode = reader["ProcessTypeCode"].GetValue<string>(),
                            CurrencyCode = reader["CurrencyCode"].GetValue<string>(),
                            InGuarantee = reader["InGuarantee"].GetValue<string>(),
                            WorkOrderCount = reader["WorkOrderCount"].GetValue<int>(),
                            WorkOrderDetailCount = reader["WorkOrderDetailCount"].GetValue<int>(),
                            CarCount = reader["CarCount"].GetValue<int>(),
                            PartPrice = reader["PartPrice"].GetValue<int>(),
                            LabourPrice = reader["LabourPrice"].GetValue<int>(),
                            CategoryLookval = reader["CategoryLookval"].GetValue<string>(),
                            ApproveDate = reader["ApproveDate"].GetValue<DateTime>(),
                            TotalAmount = reader["TotalAmount"].GetValue<int>(),
                            DealerId = reader["DealerId"].GetValue<int>(),
                            DealerRegionId = reader["DealerRegionId"].GetValue<int>(),
                            CustomerId = reader["CustomerId"].GetValue<int>(),
                            VehicleTypeId = reader["VehicleTypeId"].GetValue<int>(),
                            VehicleModelCode = reader["VehicleModelCode"].GetValue<string>(),
                            IdVehicle = reader["IdVehicle"].GetValue<int>()

                        };
                        dto.Add(laborCostPerVehicleReport);
                    }
                    reader.Close();

                    total = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return dto;
        }


        public List<KilometerReportInfoModel> ListWorkOrderDetailKilometer(UserInfo user, WorkOrderDetailKilometerRequest filter, out int totalCnt)
        {
            var dto = new List<KilometerReportInfoModel>();
            totalCnt = 0;

            try
            {

                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_RPT_WORK_ORDER_DETAIL_BY_WORK_ORDER_PARAMETERS");
                db.AddInParameter(cmd, "P_GROUP_TYPE", DbType.Int32, MakeDbNull(filter.GroupType));
                db.AddInParameter(cmd, "P_GROUP_KOD", DbType.Int32, MakeDbNull(filter.GroupCode));
                db.AddInParameter(cmd, "CUSTOMER_ID", DbType.Int32, MakeDbNull(filter.CustomerId));
                db.AddInParameter(cmd, "P_VEHICLE_MODEL_LIST", DbType.Int32, MakeDbNull(filter.VehicleModelList));
                db.AddInParameter(cmd, "PROCESS_TYPE_LIST", DbType.Int32, MakeDbNull(filter.ProcessTypeList));
                db.AddInParameter(cmd, "P_CUST_TYPE_LIST", DbType.Int32, MakeDbNull(filter.CustTypeList));
                db.AddInParameter(cmd, "P_CUSTOMER_ID_LIST", DbType.Int32, MakeDbNull(filter.CustomerIdList));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var kilometerReportInfoModel = new KilometerReportInfoModel
                        {

                            DEALER_NAME = reader["DEALER_NAME"].GetValue<string>(),
                            DEALER_REGION_NAME = reader["DEALER_REGION_NAME"].GetValue<string>(),
                            MODEL_KOD = reader["MODEL_KOD"].GetValue<string>(),
                            ID_WORK_ORDER = reader["ID_WORK_ORDER"].GetValue<string>(),
                            ID_WORK_ORDER_DETAIL = reader["ID_WORK_ORDER_DETAIL"].GetValue<string>(),
                            SSID_GUARANTEE = reader["SSID_GUARANTEE"].GetValue<decimal>(),
                            VIN_NO = reader["VIN_NO"].GetValue<string>(),
                            ID_VEHICLE = reader["ID_VEHICLE"].GetValue<string>(),
                            ENGINE_NO = reader["ENGINE_NO"].GetValue<string>(),
                            VEHICLE_WARRANTY_START_DATE = reader["WARRANTY_START_DATE"].GetValue<string>(),
                            CREATE_DATE = reader["CREATE_DATE"].GetValue<string>(),
                            VEHICLE_KM = reader["VEHICLE_KM"].GetValue<string>(),
                            ID_DEALER = reader["ID_DEALER"].GetValue<string>(),
                            ID_DEALER_REGION = reader["ID_DEALER_REGION"].GetValue<string>(),
                            CUSTOMER_ID = reader["CUSTOMER_ID"].GetValue<string>(),
                            ID_TYPE = reader["ID_TYPE"].GetValue<string>(),
                            PartPrice = reader["PartPrice"].GetValue<string>(),
                            LaborPrice = reader["LabourPrice"].GetValue<decimal>(),
                            CurrencyCode = reader["CurrencyCode"].GetValue<string>()

                        };
                        dto.Add(kilometerReportInfoModel);
                    }
                    reader.Close();

                    totalCnt = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return dto;
        }

        public List<CampaignSummaryInfoModel> ListCampaignSummaryInfo(UserInfo user, CampaignSummaryInfoRequest filter, out int total)
        {
            var dto = new List<CampaignSummaryInfoModel>();
            total = 0;

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_RPT_CAMPAIGN_VEHICLE_DETAIL");
                db.AddInParameter(cmd, "P_GROUP_TYPE", DbType.Int32, filter.GroupType);
                db.AddInParameter(cmd, "P_GROUP_KOD", DbType.String, filter.GroupCode);
                db.AddInParameter(cmd, "P_CAMPAIGN_CODE", DbType.String, MakeDbNull(filter.CampaignCode));
                db.AddInParameter(cmd, "P_CAMP_BEGIN_DATE", DbType.DateTime, MakeDbNull(filter.StartDate));
                db.AddInParameter(cmd, "P_CAMP_END_DATE", DbType.DateTime, MakeDbNull(filter.EndDate));
                db.AddInParameter(cmd, "P_CURRENCY_CODE", DbType.String, MakeDbNull(filter.Currency));
                db.AddInParameter(cmd, "P_GUARANTEE_STAT", DbType.Int32, filter.GuaranteeStat);
                db.AddInParameter(cmd, "P_IS_MUST", DbType.Int32, filter.IsMust);
                db.AddInParameter(cmd, "P_CAMPAIGN_STATUS", DbType.Int32, filter.CampaignStatus);
                db.AddInParameter(cmd, "P_GUARANTEE_CONFIRM_START_DATE", DbType.DateTime, MakeDbNull(filter.GuaranteeConfirmStartDate));
                db.AddInParameter(cmd, "P_GUARANTEE_CONFIRM_END_DATE", DbType.DateTime, MakeDbNull(filter.GuaranteeConfirmEndDate));
                db.AddInParameter(cmd, "P_DEALER_ID_LIST", DbType.String, MakeDbNull(filter.DealerIdList));
                db.AddInParameter(cmd, "P_REGION_ID_LIST", DbType.String, MakeDbNull(filter.RegionIdList));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, MakeDbNull(filter.Offset));
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var campaignSummaryInfoModel = new CampaignSummaryInfoModel
                        {

                            VehicleModel = reader["VehicleModel"].GetValue<string>(),
                            VehicleType = reader["VehicleType"].GetValue<string>(),
                            VinNo = reader["VinNo"].GetValue<string>(),
                            IsActive = reader["IsActive"].GetValue<string>(),
                            Region = reader["Region"].GetValue<string>(),
                            DealerName = reader["DealerName"].GetValue<string>(),
                            WorkOrderCardNumber = reader["WorkOrderCardNumber"].GetValue<string>(),
                            WorkOrderStartDate = reader["WorkOrderStartDate"].GetValue<DateTime>(),
                            Kilometer = reader["Kilometer"].GetValue<string>(),
                            WarrantyStartDate = reader["WarrantyStartDate"].GetValue<DateTime>(),
                            WorkerMount = reader["WorkerMount"].GetValue<string>(),
                            BitMount = reader["BitMount"].GetValue<string>(),
                            AvgPrice = reader["AvgPrice"].GetValue<decimal>(),
                            GifCost = reader["GifCost"].GetValue<decimal>()
                        };
                        dto.Add(campaignSummaryInfoModel);
                    }
                    reader.Close();
                    total = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return dto;
        }

        public List<SelectListItem> ListInvoiceNoForAutoComplete(string invoiceNo, string dealerId)
        {
            List<SelectListItem> retVal = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_SEARCH_INVOICE_NO_FOR_AUTOCOMPLETE");
                db.AddInParameter(cmd, "SEARCH_TEXT", DbType.String, invoiceNo);
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(dealerId));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {

                            Value = reader["Value"].GetValue<string>(),
                            Text = reader["Text"].GetValue<string>()
                        };
                        retVal.Add(lookupItem);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return retVal;
        }

        public List<SelectListItem> ListIdentityNoForAutoComplete(string identityNo)
        {
            List<SelectListItem> retVal = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_SEARCH_IDENTITY_NO_FOR_AUTOCOMPLETE");
                db.AddInParameter(cmd, "IDENTITY_NO", DbType.String, identityNo);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {

                            Value = reader["Value"].GetValue<string>(),
                            Text = reader["Text"].GetValue<string>()
                        };
                        retVal.Add(lookupItem);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return retVal;
        }

        public List<SelectListItem> ListDefectIdForAutoComplete(string defectNo)
        {
            List<SelectListItem> retVal = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_SEARCH_DEFECT_NO_FOR_AUTOCOMPLETE");
                db.AddInParameter(cmd, "DEFECT_NO", DbType.String, defectNo);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {

                            Value = reader["Value"].GetValue<string>(),
                            Text = reader["Text"].GetValue<string>()
                        };
                        retVal.Add(lookupItem);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return retVal;
        }

        public List<SelectListItem> ListContractNameForAutoComplete(string contractName)
        {
            List<SelectListItem> retVal = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_SEARCH_CONTRACT_NAME_FOR_AUTOCOMPLETE");
                db.AddInParameter(cmd, "CONTRACT_NAME", DbType.String, contractName);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {

                            Value = reader["Value"].GetValue<string>(),
                            Text = reader["Text"].GetValue<string>()
                        };
                        retVal.Add(lookupItem);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return retVal;
        }

        public List<SelectListItem> ListUserCodeForAutoComplete(string userNo)
        {
            List<SelectListItem> retVal = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_SEARCH_USER_CODE_FOR_AUTOCOMPLETE");
                db.AddInParameter(cmd, "USER_CODE", DbType.String, userNo);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {

                            Value = reader["Value"].GetValue<string>(),
                            Text = reader["Text"].GetValue<string>()
                        };
                        retVal.Add(lookupItem);
                    }
                    reader.Close();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return retVal;
        }

        public List<SelectListItem> ListUserNameForAutoComplete(string userName)
        {
            List<SelectListItem> retVal = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_SEARCH_USER_FULL_NAME_FOR_AUTOCOMPLETE");
                db.AddInParameter(cmd, "USER_NAME", DbType.String, userName);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {

                            Value = reader["Value"].GetValue<string>(),
                            Text = reader["Text"].GetValue<string>()
                        };
                        retVal.Add(lookupItem);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return retVal;
        }
        public List<SelectListItem> ListDealerCustomersForAutoComplete(string custName, string dealerId)
        {
            List<SelectListItem> retVal = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_SEARCH_CUSTOMER_FULL_NAME_FOR_AUTOCOMPLETE");
                db.AddInParameter(cmd, "USER_NAME", DbType.String, custName);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {

                            Value = reader["Value"].GetValue<string>(),
                            Text = reader["Text"].GetValue<string>()
                        };
                        retVal.Add(lookupItem);
                    }
                    reader.Close();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return retVal;
        }

        public InvoiceWebServiceResult GetInvoicesAsXml(string userName, string password, DateTime startDate, DateTime endDate, long customerId, string invoiceNo)
        {
            DbDataReader dReader = null;
            var result = new InvoiceWebServiceResult();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER_INVOICE_XML");
                db.AddInParameter(cmd, "USER_CODE", DbType.String, userName);
                db.AddInParameter(cmd, "PASSWORD", DbType.String, new PasswordSecurityProvider().GenerateHashedPassword(password));
                db.AddInParameter(cmd, "BEGIN_DATE", DbType.DateTime, startDate);
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, endDate);
                db.AddInParameter(cmd, "CUSTOMER_ID", DbType.Int32, customerId);
                db.AddInParameter(cmd, "INVOICE_NO", DbType.String, invoiceNo);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 10);
                CreateConnection(cmd);
                dReader = cmd.ExecuteReader();

                dReader.Close();

                result.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                result.ErrorDesc = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return result;

        }

        public void CheckDay27Data()
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    CreateDatabase();
                    var cmd = db.GetStoredProcCommand("P_LIST_WORKORDER_TO_DAY27");
                    CreateConnection(cmd);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        public string CheckWorkOrderDayData()
        {
            string errorMessage = string.Empty;

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_WORKORDER_TO_DAY");
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Close();
                    errorMessage = db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return errorMessage;
        }

        public string CheckWorkOrderHourData()
        {
            string errorMessage = string.Empty;

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_WORKORDER_TO_HOUR");
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Close();
                    errorMessage = db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return errorMessage;
        }

        public List<SshReport> ListSSHReport(UserInfo user, SshReportFilterRequest filter, out int totalCnt)
        {
            var dto = new List<SshReport>();
            totalCnt = 0;

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_RPT_SSH");
                db.AddInParameter(cmd, "DEALER_ID_LIST", DbType.String, MakeDbNull(filter.DealerIdList));
                db.AddInParameter(cmd, "DEFECT_ID_LIST", DbType.String, MakeDbNull(filter.DefectIdList));
                db.AddInParameter(cmd, "CONTRACT_ID_LIST", DbType.String, MakeDbNull(filter.ContractIdList));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, MakeDbNull(filter.IsActive));
                db.AddInParameter(cmd, "CONTRACT_START_DATE", DbType.Date, MakeDbNull(filter.ContractStartDate));
                db.AddInParameter(cmd, "CONTRACT_END_DATE", DbType.Date, MakeDbNull(filter.ContractEndDate));
                db.AddInParameter(cmd, "VIN_NO", DbType.String, filter.VehicleVinNo);
                db.AddInParameter(cmd, "PO_STATUS", DbType.Int32, MakeDbNull(filter.PoStatus));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "FILTERED_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var sshReport = new SshReport
                        {

                            DefectNo = reader["DEFECT_NO"].GetValue<string>(),
                            ContractName = reader["CONTRACT_NAME"].GetValue<string>(),
                            PoNumber = reader["PO_NUMBER"].GetValue<string>(),
                            PartCode = reader["PART_CODE"].GetValue<string>(),
                            PartName = reader["PART_NAME"].GetValue<string>(),
                            Duration = reader["DURATION"].GetValue<string>(),
                            DealerName = reader["DEALER_SHRT_NAME"].GetValue<string>(),
                            VinNo = reader["VIN_NO"].GetValue<string>(),
                            DeclarationDate = reader["DECLARATION_DATE"].GetValue<DateTime>(),
                            DealerDeclarationDate = reader["DEALER_DECLARATION_DATE"].GetValue<DateTime>(),
                            OrderDate = reader["ORDER_DATE"].GetValue<DateTime?>(),
                            DurationDate = reader["DURATION_DATE"].GetValue<int?>(),
                            PoStatus = reader["PO_STATUS"].GetValue<string>(),
                            CreateDate = reader["CREATE_DATE"].GetValue<DateTime>(),
                            OrderPeriod = reader["ORDER_PERIOD"].GetValue<int?>()

                        };
                        dto.Add(sshReport);
                    }
                    reader.Close();

                    totalCnt = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return dto;

        }

        public List<FleetPeriodReport> GetFleetPeriodReport(UserInfo user, FleetPeriodReportFilterRequest filter, out int total)
        {
            var result = new List<FleetPeriodReport>();
            total = 0;

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_FLEET_PERIOD_REPORT");

                var list = new List<SqlParameter>();
                list.Add(new SqlParameter() { ParameterName = "PERIOD ", DbType = DbType.Int32, Value = MakeDbNull(filter.Period) });
                list.Add(new SqlParameter() { ParameterName = "FLEET_CODE", DbType = DbType.String, Value = MakeDbNull(filter.FleetCode) });
                list.Add(new SqlParameter() { ParameterName = "FLEET_NAME", DbType = DbType.String, Value = MakeDbNull(filter.FleetName) });
                list.Add(new SqlParameter() { ParameterName = "CUSTOMER_NAME", DbType = DbType.String, Value = MakeDbNull(filter.WorkOrderCustomerName) });
                list.Add(new SqlParameter() { ParameterName = "VIN_NO", DbType = DbType.String, Value = MakeDbNull(filter.WorkOrderCustomerVehicleVinNo) });
                list.Add(new SqlParameter() { ParameterName = "PLATE", DbType = DbType.String, Value = MakeDbNull(filter.WorkOrderCustomerVehiclePlate) });
                list.Add(new SqlParameter() { ParameterName = "WORK_ORDER_ID", DbType = DbType.String, Value = MakeDbNull(filter.WorkOrderNo) });
                list.Add(new SqlParameter() { ParameterName = "WORK_ORDER_OPENDATE", DbType = DbType.DateTime, Value = MakeDbNull(filter.WorkOrderOpenDate) });
                list.Add(new SqlParameter() { ParameterName = "WORK_ORDER_CLOSEDATE", DbType = DbType.Boolean, Value = MakeDbNull(filter.WorkOrderCloseDate) });
                list.Add(new SqlParameter() { ParameterName = "LANGUAGE_CODE", DbType = DbType.String, Value = MakeDbNull(user.LanguageCode) });
                list.Add(new SqlParameter() { ParameterName = "SORT_COLUMN", DbType = DbType.String, Value = MakeDbNull(filter.SortColumn) });
                list.Add(new SqlParameter() { ParameterName = "SORT_DIRECTION", DbType = DbType.String, Value = MakeDbNull(filter.SortDirection) });
                list.Add(new SqlParameter() { ParameterName = "OFFSET", DbType = DbType.Int32, Value = filter.Offset });
                list.Add(new SqlParameter() { ParameterName = "PAGE_SIZE", DbType = DbType.Int32, Value = MakeDbNull(filter.PageSize) });

                db.AddOutParameter(cmd, "P_TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "P_ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "P_ERROR_DESC", DbType.String, 200);

                cmd.Parameters.AddRange(list.ToArray());
                cmd.CommandTimeout = 1440;
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new FleetPeriodReport
                        {
                            Period = reader["ID_CLAIM_RECALL_PERIOD"].GetValue<int>(),
                            FleetCode = reader["FLEET_CODE"].ToString(),
                            FleetName = reader["FLEET_NAME"].ToString(),
                            StartDate = reader["VALIDITY_START_DATE"].GetValue<DateTime>(),
                            FinishDate = reader["VALIDITY_END_DATE"].GetValue<DateTime>(),
                            WorkOrderOpenDate = reader["CREATE_DATE"].GetValue<DateTime>(),
                            WorkOrderVehicleLeaveDate = reader["VEHICLE_LEAVE_DATE"].GetValue<DateTime?>(),
                            WorkOrderCloseDate = reader["CLOSED_DATE"].GetValue<DateTime?>(),
                            WorkOrderNo = reader["ID_WORK_ORDER"].GetValue<int>(),
                            WorkOrderStatus = reader["WORK_ORDER_STATUS"].GetValue<string>(),
                            WorkOrderDealerRegion = reader["DEALER_REGION_NAME"].ToString(),
                            WorkOrderDealerCode = reader["DEALER_SSID"].ToString(),
                            WorkOrderDealer = reader["DEALER_NAME"].ToString(),

                            WorkOrderCustomerType = reader["CUSTOMER_TYPE_NAME"].GetValue<string>(),
                            WorkOrderCustomerId = reader["ID_CUSTOMER"].GetValue<int>(),
                            WorkOrderCustomerName = reader["CUSTOMER_NAME"].GetValue<string>(),
                            WorkOrderCustomerIdentity = reader["TC_IDENTITY_NO"].GetValue<string>(),
                            WorkOrderCustomerTaxNo = reader["TAX_NO"].GetValue<string>(),
                            WorkOrderCustomerAddress = reader["CUSTOMER_ADDRESS"].GetValue<string>(),
                            WorkOrderCustomerMobile = reader["CUSTOMER_MOBILE"].GetValue<string>(),
                            WorkOrderCustomerTelephone = reader["CUSTOMER_TELEPHONE"].GetValue<string>(),
                            WorkOrderCustomerWithHolding = reader["CUSTOMER_WITHHOLD"].GetValue<bool>() ? MessageResource.Global_Display_Yes : MessageResource.Global_Display_No,
                            WorkOrderCustomerVatExclude = reader["CUSTOMER_VAT_EXCLUDE"].GetValue<bool>() ? MessageResource.Global_Display_Yes : MessageResource.Global_Display_No,
                            WorkOrderCustomerVehiclePlate = reader["VEHICLE_PLATE"].GetValue<string>(),
                            WorkOrderCustomerVehicleModel = reader["VEHICLE_MODEL"].GetValue<string>(),
                            WorkOrderCustomerVehicleVinNo = reader["VIN_NO"].GetValue<string>(),
                            WorkOrderCustomerVehicleKm = reader["VEHICLE_KM"].GetValue<string>(),
                            WorkOrderCustomerRequest = reader["CUSTOMER_NOTES"].GetValue<string>(),
                            WorkOrderDetailId = reader["ID_WORK_ORDER_DETAIL"].GetValue<int>(),
                            WorkOrderIndicatorType = reader["INDICATOR_TYPE_NAME"].GetValue<string>(),
                            WorkOrderIndicatorCode = reader["INDICATOR_TYPE_CODE"].GetValue<string>(),
                            WorkOrderIndicatorDescription = reader["INDICATOR_DESCRIPTION"].GetValue<string>(),

                            PartCode = reader["PART_CODE"].GetValue<string>(),
                            PartName = reader["PART_NAME"].GetValue<string>(),
                            PartQuantity = reader["PART_QUANTITY"].GetValue<decimal?>(),
                            PartUnitPrice = reader["PART_UNIT_PRICE"].GetValue<decimal?>(),
                            OtokarPartDiscountRate = reader["OTOKAR_PART_DISCOUNT_RATE"].GetValue<decimal?>(),
                            OtokarPartDiscountPrice = reader["OTOKAR_PART_DISCOUNT_PRICE"].GetValue<decimal?>(),
                            DealerPartDiscountRate = reader["DEALER_PART_DISCOUNT_RATE"].GetValue<decimal?>(),
                            DealerPartDiscountPrice = reader["DEALER_PART_DISCOUNT_PRICE"].GetValue<decimal?>(),
                            PartDiscountPrice = reader["PART_DISCOUNT_PRICE"].GetValue<decimal?>(),
                            PartDiscountedPrice = reader["PART_DISCOUNTED_PRICE"].GetValue<decimal?>(),
                            PartPrice = reader["PART_PRICE"].GetValue<decimal?>(),

                            LabourCode = reader["LABOUR_CODE"].GetValue<string>(),
                            LabourName = reader["LABOUR_NAME"].GetValue<string>(),
                            LabourDuration = reader["LABOUR_DURATION"].GetValue<decimal?>(),
                            LabourQuantity = reader["LABOUR_QUANTITY"].GetValue<decimal?>(),
                            LabourUnitPrice = reader["LABOUR_UNIT_PRICE"].GetValue<decimal?>(),
                            OtokarLabourDiscountRate = reader["OTOKAR_LABOUR_DISCOUNT_RATE"].GetValue<decimal?>(),
                            OtokarLabourDiscountPrice = reader["OTOKAR_LABOUR_DISCOUNT_PRICE"].GetValue<decimal?>(),
                            DealerLabourDiscountRate = reader["DEALER_LABOUR_DISCOUNT_RATE"].GetValue<decimal?>(),
                            DealerLabourDiscountPrice = reader["DEALER_LABOUR_DISCOUNT_PRICE"].GetValue<decimal?>(),
                            LabourDiscountPrice = reader["LABOUR_DISCOUNT_PRICE"].GetValue<decimal?>(),
                            LabourDiscountedPrice = reader["LABOUR_DISCOUNTED_PRICE"].GetValue<decimal?>(),
                            LabourPrice = reader["LABOUR_PRICE"].GetValue<decimal?>(),

                            CurrencyCode = reader["CURRENCY_CODE"].GetValue<string>(),
                        };
                        result.Add(listModel);
                    }
                    reader.Close();
                    total = db.GetParameterValue(cmd, "P_TOTAL_COUNT").GetValue<int>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return result;
        }

        public List<AlternatePartReport> GetAlternatePartReport(UserInfo user, AlternatePartReportFilterRequest filter, out int total)
        {
            var retVal = new List<AlternatePartReport>();
            total = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_RPT_ALTERNATE_PARTS");
                cmd.CommandTimeout = 14440;
                db.AddInParameter(cmd, "ID_DEALER", DbType.String, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "PART_CODE", DbType.String, MakeDbNull(filter.PartCode));
                db.AddInParameter(cmd, "PART_NAME", DbType.String, MakeDbNull(filter.PartName));
                db.AddInParameter(cmd, "ALTERNATE_PART_CODE", DbType.String, MakeDbNull(filter.AlternatePartCode));
                db.AddInParameter(cmd, "ALTERNATE_PART_NAME", DbType.String, MakeDbNull(filter.AlternatePartName));

                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var stockTypeDetailListModel = new AlternatePartReport
                        {
                            PartCode = reader["PART_CODE"].GetValue<string>(),
                            PartName = reader["PART_NAME"].GetValue<string>(),
                            AlternatePartCode = reader["ALTERNATE_PART_CODE"].GetValue<string>(),
                            AlternatePartName = reader["ALTERNATE_PART_NAME"].GetValue<string>(),
                            AlternatePartListPrice = reader["ALTERNATE_PART_LIST_PRICE"].GetValue<string>().Replace(".", ",").GetValue<decimal>().ToString("N", new System.Globalization.CultureInfo("tr-TR"))
                    };
                        retVal.Add(stockTypeDetailListModel);
                    }
                    reader.Close();
                }
                total = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return retVal;
        }
    }
}
