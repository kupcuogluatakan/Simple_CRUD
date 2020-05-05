using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.StockCardTransaction;

namespace ODMSData
{
    public class StockCardTransactionData : DataAccessBase
    {
        public List<StockCardTransactionListModel> ListStockCardTransaction(UserInfo user, StockCardTransactionListModel filter, out int totalCount)
        {
            var listModel = new List<StockCardTransactionListModel>();

            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_STOCK_CARD_TRANSACTION");

                db.AddInParameter(cmd, "DEALER_ID", DbType.String, filter.DealerId);
                db.AddInParameter(cmd, "PART_ID", DbType.String, filter.PartId);
                db.AddInParameter(cmd, "ID_STOCK_TYPE", DbType.Int32, filter.StockTypeId);
                db.AddInParameter(cmd, "TRANSACTION_TYPE", DbType.String, filter.TransactionType);
                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, filter.StartDate);
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, filter.EndDate);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, filter.SortColumn);
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, filter.SortDirection);
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 1000);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var model = new StockCardTransactionListModel
                        {
                            FromWareRack = dr["FROM_WARE_RACK"].GetValue<string>(),
                            StockType = dr["STOCK_TYPE"].GetValue<string>(),
                            ToWareRack = dr["TO_WARE_RACK"].GetValue<string>(),
                            TransactionDesc = dr["TRANSACTION_DESC"].GetValue<string>(),
                            TransactionType = dr["TRANSACTION_TYPE"].GetValue<string>(),
                            CreateDate = dr["CREATE_DATE"].GetValue<DateTime>(),
                            Quantity = dr["QNTY"].GetValue<string>()
                        };

                        model.DateTimeS = model.CreateDate.ToString("d").Replace('.', '/');

                        listModel.Add(model);
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
            return listModel;
        }
    }
}
