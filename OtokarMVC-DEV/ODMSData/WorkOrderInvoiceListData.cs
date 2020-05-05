using ODMSCommon.Security;
using ODMSModel.WorkOrderInvoiceList;
using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;

namespace ODMSData
{
    public class WorkOrderInvoiceListData : DataAccessBase
    {
        public List<WorkOrderInvoiceListListModel> ListWorkOrderInvoiceList(UserInfo user, WorkOrderInvoiceListListModel filter, out int totalCount)
        {
            var retVal = new List<WorkOrderInvoiceListListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_WORK_ORDER_INVOICE_LIST");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, filter.IdWorkOrder);
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int64, filter.IdDealer);
                db.AddInParameter(cmd, "CUSTOMER_NAME", DbType.String, filter.CustomerName);
                db.AddInParameter(cmd, "CUSTOMER_TAX_NO", DbType.String, filter.TaxNo);
                db.AddInParameter(cmd, "INVOICE_NO", DbType.String, filter.InvoiceNo);
                db.AddInParameter(cmd, "CUSTOMER_TC_IDENTITY", DbType.String, filter.TCNo);
                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, filter.StartDate);
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, filter.EndDate);
                db.AddInParameter(cmd, "PLATE", DbType.String, filter.Plate);

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
                        var customerDiscountListModel = new WorkOrderInvoiceListListModel
                        {
                            IdWorkOrderInv = reader["ID_WORK_ORDER_INV"].GetValue<Int64>(),
                            IdWorkOrder = reader["ID_WORK_ORDER"].GetValue<Int64>(),
                            IdCustomer = reader["CUSTOMER_ID"].GetValue<Int64>(),
                            CustomerLastName = reader["CUSTOMER_LAST_NAME"].GetValue<string>(),
                            CustomerName = reader["CUSTOMER_NAME"].GetValue<string>(),
                            TCNo=reader["TC_NO"].GetValue<string>(),
                            TaxNo=reader["CUSTOMER_TAX_NO"].GetValue<string>(),
                            InvoiceNo = reader["INVOICE_NO"].GetValue<string>(),
                            InvoiceDate = reader["INVOICE_DATE"].GetValue<DateTime>(),
                            Plate = reader["PLATE"].GetValue<string>(),
                            TotalAmount = reader["SUB_TOTAL"].GetValue<decimal>(),
                            VatAmount = reader["VAT"].GetValue<decimal>(),
                            GeneralAmount = reader["TOTAL"].GetValue<decimal>()
                        };
                        retVal.Add(customerDiscountListModel);
                    }
                    reader.Close();
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

        public List<WorkOrderInvoiceListListModel> ListWorkOrderInvoiceListCancelled(UserInfo user, WorkOrderInvoiceListListModel filter, out int totalCount)
        {
            var retVal = new List<WorkOrderInvoiceListListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_WORK_ORDER_INVOICE_LIST_CANCELLED");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, filter.IdWorkOrder);
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int64, filter.IdDealer);
                db.AddInParameter(cmd, "CUSTOMER_NAME", DbType.String, filter.CustomerName);
                db.AddInParameter(cmd, "CUSTOMER_TAX_NO", DbType.String, filter.TaxNo);
                db.AddInParameter(cmd, "INVOICE_NO", DbType.String, filter.InvoiceNo);
                db.AddInParameter(cmd, "CUSTOMER_TC_IDENTITY", DbType.String, filter.TCNo);
                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, filter.StartDate);
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, filter.EndDate);
                db.AddInParameter(cmd, "PLATE", DbType.String, filter.Plate);

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
                        var customerDiscountListModel = new WorkOrderInvoiceListListModel
                        {
                            IdWorkOrderInv = reader["ID_WORK_ORDER_INV"].GetValue<Int64>(),
                            IdWorkOrder = reader["ID_WORK_ORDER"].GetValue<Int64>(),
                            IdCustomer = reader["CUSTOMER_ID"].GetValue<Int64>(),
                            CustomerLastName = reader["CUSTOMER_LAST_NAME"].GetValue<string>(),
                            CustomerName = reader["CUSTOMER_NAME"].GetValue<string>(),
                            TCNo = reader["TC_NO"].GetValue<string>(),
                            TaxNo = reader["CUSTOMER_TAX_NO"].GetValue<string>(),
                            InvoiceNo = reader["INVOICE_NO"].GetValue<string>(),
                            InvoiceDate = reader["INVOICE_DATE"].GetValue<DateTime>(),
                            Plate = reader["PLATE"].GetValue<string>(),
                            TotalAmount = reader["SUB_TOTAL"].GetValue<decimal>(),
                            VatAmount = reader["VAT"].GetValue<decimal>(),
                            GeneralAmount = reader["TOTAL"].GetValue<decimal>()
                        };
                        retVal.Add(customerDiscountListModel);
                    }
                    reader.Close();
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


        public void DMLWorkOrderInvoiceList(UserInfo user, WorkOrderInvoiceListViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_WORK_ORDER_INVOICE_LIST");
                db.AddInParameter(cmd, "ID_WORK_ORDER_INV", DbType.Int64, model.IdWorkOrderInv);
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, model.IdWorkOrder);

                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddInParameter(cmd, "OPERATING_DATE", DbType.Date, MakeDbNull(DateTime.Now));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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
    }
}
