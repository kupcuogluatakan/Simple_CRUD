using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSModel.WorkorderListInvoices;
using ODMSCommon.Security;

namespace ODMSData
{
    public class WorkorderListInvoicesData : DataAccessBase
    {
        private const string sp_getWorkorderInvList = "P_LIST_WORKORDER_INVOICE";
        public List<WorkorderListInvoicesListModel> GetWorkorderInvoicesList(UserInfo user, WorkorderListInvoicesListModel filter, out int totalCount)
        {
            List<WorkorderListInvoicesListModel> list_WorkorderInv = new List<WorkorderListInvoicesListModel>();

            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getWorkorderInvList);
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int32, filter.WorkOrderId);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var model = new WorkorderListInvoicesListModel
                        {
                            WorkOrderInvId = dr["ID_WORK_ORDER_INV"].GetValue<int>(),
                            Address = dr["ADDRESS_1"].GetValue<string>(),
                            InvSerialNo = dr["INVOICE_SERIAL_NO"].GetValue<string>(),
                            DueDuration = dr["DUE_DURATION"].GetValue<string>(),
                            InvAmount = dr["INVOICE_AMOUNT"].GetValue<string>(),
                            InvDate = dr["INVOICE_DATE"].GetValue<DateTime>().ToShortDateString(),
                            InvNo = dr["INVOICE_NO"].GetValue<string>(),
                            InvVatAmount = dr["INVOICE_VAT_AMOUNT"].GetValue<string>()
                        };

                        list_WorkorderInv.Add(model);
                    }
                    dr.Close();
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

            return list_WorkorderInv;
        }
    }
}
