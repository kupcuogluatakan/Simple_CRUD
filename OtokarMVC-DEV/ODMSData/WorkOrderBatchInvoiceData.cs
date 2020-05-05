using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSModel.WorkOrderBatchInvoice;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public  class WorkOrderBatchInvoiceData:DataAccessBase
    {
        public List<WorkOrderBatchInvoiceList> List(UserInfo user,WorkOrderBatchInvoiceList filter)
        {
            var retVal = new List<WorkOrderBatchInvoiceList>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_WORK_ORDER_BATCH_INVOICES");
                db.AddInParameter(cmd, "DEALER_ID", DbType.String, MakeDbNull(filter.DealerId));
                db.AddInParameter(cmd, "CUSTOMER_ID", DbType.Int32, MakeDbNull(filter.CustomerId));
                db.AddOutParameter(cmd, "CURRENCY", DbType.String,3);
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new WorkOrderBatchInvoiceList
                        {
                            CustomerFirstName = reader["CUSTOMER_NAME"].ToString(),
                            CustomerId = reader["CUSTOMER_ID"].GetValue<int>(),
                            CustomerLastName = reader["CUSTOMER_LAST_NAME"].ToString(),
                            DealerId = reader["ID_DEALER"].GetValue<int>(),
                            IndicatorName = reader["DESCRIPTION"].ToString(),
                            IndicatorTypeName = reader["INDICATOR_TYPE_NAME"].ToString(),
                            ProcessTypeName = reader["INDICATOR_TYPE_NAME"].ToString(),
                            Plate = reader["PLATE"].ToString(),
                            Price = reader["PRICE"].GetValue<decimal>(),
                            VinNo = reader["VIN_NO"].ToString(),
                            WorkOrderDetailId = reader["ID_WORK_ORDER_DETAIL"].GetValue<long>(),
                            WorkOrderId = reader["ID_WORK_ORDER"].GetValue<long>()
                        };
                        retVal.Add(item);
                    }
                    reader.Close();
                }
                filter.TotalCount = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
                filter.Currency = db.GetParameterValue(cmd, "CURRENCY").ToString();
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
