using ODMSCommon;
using ODMSModel.WebServiceLogs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSData
{
    public class WebServiceLogData : DataAccessBase
    {
        public List<InvoiceListLogItem> ListInvoiceLogs(InvoiceListLogItem request, InvoiceListFilter filter)
        {
            var retVal = new List<InvoiceListLogItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_INVIOCE_LOG");
                db.AddInParameter(cmd, "USER_CODE", DbType.String, MakeDbNull(filter.UserCode));
                db.AddInParameter(cmd, "START_DATE", DbType.Date, MakeDbNull(filter.StartDate));
                db.AddInParameter(cmd, "END_DATE", DbType.Date, MakeDbNull(filter.EndDate));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(request.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(request.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, request.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(request.PageSize));
                db.AddInParameter(cmd, "FILTERED_TOTAL_COUNT", DbType.Int32, 10);
                db.AddInParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddInParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddInParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var invoiceListLogItem = new InvoiceListLogItem
                        {

                            InvoiceServiceLogId = reader["InvoiceServiceLogId"].GetValue<long>(),
                            CallUserCode = reader["CallUserCode"].GetValue<string>(),
                            CallDate = reader["CallDate"].GetValue<DateTime>(),
                            StartDate = reader["StartDate"].GetValue<DateTime>(),
                            EndDate = reader["EndDate"].GetValue<DateTime>(),
                            IsSuccess = reader["IsSuccess"].GetValue<bool>(),
                            ErrorDesc = reader["ErrorDesc"].GetValue<string>()

                        };
                        retVal.Add(invoiceListLogItem);
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

        public string GetInvoiceLogResponse(long id)
        {
            string result;

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_INVOCE_SERVICE_LOG_RESPONSE");
                db.AddInParameter(cmd, "ID_INVOICE_SERVICE_LOG", DbType.Int32, id);
                CreateConnection(cmd);
                result = cmd.ExecuteScalar().GetValue<string>();
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
    }
}
