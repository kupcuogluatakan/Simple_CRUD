using ODMSCommon.Security;
using ODMSModel.StockBlock;
using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;

namespace ODMSData
{
    public class StockBlockData : DataAccessBase
    {
        public List<StockBlockListModel> ListStockBlock(UserInfo user,StockBlockListModel filter, out int totalCnt)
        {
            var retVal = new List<StockBlockListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_STOCK_BLOCK");
                db.AddInParameter(cmd, "BLOCK_DATE", DbType.DateTime, filter.BlockDate);
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, filter.IdDealer);
                db.AddInParameter(cmd, "IS_BALANCE", DbType.Int32, filter.IsBalance);
                db.AddInParameter(cmd, "BLOCK_STATUS_ID", DbType.Int32, filter.BlockStatusId);

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
                        var stockBlockListModel = new StockBlockListModel
                            {
                                IdStockBlock = reader["ID_STOCK_BLOCK"].GetValue<Int64>(),
                                BlockDate = reader["BLOCK_DATE"].GetValue<DateTime>(),
                                BlockReasonDesc = reader["BLOCK_REASON_DESC"].GetValue<string>(),
                                BlockStatusId = reader["BLOCK_STATUS_ID"].GetValue<int>(),
                                BlockStatusName = reader["BLOCK_STATUS_NAME"].GetValue<string>()
                            };

                        retVal.Add(stockBlockListModel);
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

            return retVal;
        }

        public StockBlockViewModel GetStockBlock(UserInfo user,StockBlockViewModel filter)
        {
            //var retVal = new List<StockBlockViewModel>();

            System.Data.Common.DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_STOCK_BLOCK");

                db.AddInParameter(cmd, "ID_STOCK_BLOCK", DbType.Int64, MakeDbNull(filter.IdStockBlock));

                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.IdDealer = dReader["ID_DEALER"].GetValue<Int32>();
                    filter.BlockDate = dReader["BLOCK_DATE"].GetValue<DateTime>();
                    filter.BlockReasonDesc = dReader["BLOCK_REASON_DESC"].GetValue<string>();
                    filter.BlockedStatusId = dReader["BLOCKED_STATUS_ID"].GetValue<int>();
                    filter.BlockedStatusName = dReader["BLOCKED_STATUS_NAME"].GetValue<string>();
                }
                if (dReader.NextResult())
                {
                    var table = new DataTable();
                    table.Load(dReader);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dReader != null && !dReader.IsClosed)
                {
                    dReader.Close();
                    dReader.Dispose();
                }
                CloseConnection();
            }


            return filter;
        }

        public void DMLStockBlock(UserInfo user,StockBlockViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_STOCK_BLOCK");
                db.AddInParameter(cmd, "ID_STOCK_BLOCK", DbType.Int64, model.IdStockBlock);//Update

                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, model.IdDealer);
                db.AddInParameter(cmd, "BLOCK_DATE", DbType.DateTime, model.BlockDate);
                db.AddInParameter(cmd, "BLOCK_REASON_DESC", DbType.String, model.BlockReasonDesc);
                db.AddInParameter(cmd, "BLOCK_STATUS_ID", DbType.Int32, model.BlockedStatusId);

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
