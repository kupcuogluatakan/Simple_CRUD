using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.CycleCountResult;
using ODMSModel.CycleCountStockDiff;

namespace ODMSData
{
    public class CycleCountStockDiffData : DataAccessBase
    {

        public List<CycleCountStockDiffSearchListModel> SearchCycleCountStockDiffs(UserInfo user, CycleCountStockDiffSearchListModel filter, out int totalCount)
        {
            var retVal = new List<CycleCountStockDiffSearchListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_SEARCH_CYCLE_COUNT_STOCK_DIFFS");
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "PART_NAME", DbType.String, MakeDbNull(filter.PartName));
                db.AddInParameter(cmd, "IS_STOCK_CHANGED", DbType.Boolean, MakeDbNull(filter.IsStockChanged));
                db.AddInParameter(cmd, "PART_CODE", DbType.String, MakeDbNull(filter.PartCode));
                db.AddInParameter(cmd, "STOCK_TYPE_ID", DbType.Int32, MakeDbNull(filter.StockTypeId));
                db.AddInParameter(cmd, "START_DATE", DbType.Date, MakeDbNull(filter.StartDate));
                db.AddInParameter(cmd, "END_DATE", DbType.Date, MakeDbNull(filter.EndDate));
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var cycleCountStockDiffListModel = new CycleCountStockDiffSearchListModel
                        {
                            AfterCountTotal = reader["AFTER_COUNT_TOTAL"].GetValue<decimal>(),
                            BeforeCountTotal = reader["BEFORE_COUNT_TOTAL"].GetValue<decimal>(),
                            DiffCount = reader["DIFF_COUNT_QUANTITY"].GetValue<decimal>(),
                            DealerId = reader["DEALER_ID"].GetValue<int>(),
                            StockCardId = reader["STOCK_CARD_ID"].GetValue<int?>(),
                            StockCardName = reader["STOCK_CARD_NAME"].GetValue<string>(),
                            PartCode = reader["PART_CODE"].GetValue<string>(),
                            PartName = reader["PART_NAME"].GetValue<string>(),
                            DealerName = reader["DEALER_NAME"].GetValue<string>(),
                            StockTypeId = reader["STOCK_TYPE_ID"].GetValue<int>(),
                            StockTypeName = reader["STOCK_TYPE_NAME"].GetValue<string>(),
                            CreateDate = reader["CREATE_DATE"].GetValue<DateTime>()
                        };
                        retVal.Add(cycleCountStockDiffListModel);
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

        public List<CycleCountStockDiffListModel> ListCycleCountStockDiffs(UserInfo user,CycleCountStockDiffListModel filter, out int totalCount)
        {
            var retVal = new List<CycleCountStockDiffListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CYCLE_COUNT_STOCK_DIFFS");
                db.AddInParameter(cmd, "CYCLE_COUNT_ID", DbType.Int32, MakeDbNull(filter.CycleCountId));
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var cycleCountStockDiffListModel = new CycleCountStockDiffListModel
                        {
                            CycleCountStockDiffId = reader["CYCLE_COUNT_STOCK_DIFF_ID"].GetValue<int>(),
                            CycleCountId = reader["CYCLE_COUNT_ID"].GetValue<int>(),
                            WarehouseId = reader["WAREHOUSE_ID"].GetValue<int>(),
                            WarehouseName = reader["WAREHOUSE_NAME"].GetValue<string>(),
                            StockCardId = reader["STOCK_CARD_ID"].GetValue<int?>(),
                            StockCardName = reader["STOCK_CARD_NAME"].GetValue<string>(),
                            AfterCount = reader["AFTER_COUNT_QUANTITY"].GetValue<decimal>(),
                            BeforeCount = reader["BEFORE_COUNT_QUANTITY"].GetValue<decimal>()
                        };
                        retVal.Add(cycleCountStockDiffListModel);
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
        public void DMLCycleCountStockDiff(UserInfo user, CycleCountStockDiffViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_CYCLE_COUNT_STOCK_DIFF");
                db.AddParameter(cmd, "CYCLE_COUNT_STOCK_DIFF_ID", DbType.Int32, ParameterDirection.InputOutput, "CYCLE_COUNT_STOCK_DIFF_ID", DataRowVersion.Default, model.CycleCountStockDiffId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "WAREHOUSE_ID", DbType.Int32, MakeDbNull(model.WarehouseId));
                db.AddInParameter(cmd, "STOCK_CARD_ID", DbType.Int32, MakeDbNull(model.StockCardId));
                db.AddInParameter(cmd, "CYCLE_COUNT_ID", DbType.Int32, MakeDbNull(model.CycleCountId));
                db.AddInParameter(cmd, "AFTER_COUNT_QUANTITY", DbType.Int32, model.AfterCount);
                db.AddInParameter(cmd, "BEFORE_COUNT_QUANTITY", DbType.Int32, model.BeforeCount);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.CycleCountStockDiffId = db.GetParameterValue(cmd, "CYCLE_COUNT_STOCK_DIFF_ID").GetValue<int>();
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

        public void DMLCycleCountStockDiff(UserInfo user, CycleCountResultAuditViewModel model)
        {
            bool isSuccess = true;
            CreateDatabase();
            var cmd = db.GetStoredProcCommand("P_DML_CYCLE_COUNT_RESULT_AUDIT_STOCK_DIFF");
            CreateConnection(cmd);
            DbTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                foreach (var item in model.CycleCountAuditList)
                {
                    cmd.Parameters.Clear();
                    db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                    db.AddInParameter(cmd, "CYCLE_COUNT_ID", DbType.Int32, MakeDbNull(model.CycleCountId));
                    db.AddInParameter(cmd, "WAREHOUSE_ID", DbType.Int32, MakeDbNull(model.WarehouseId));
                    db.AddInParameter(cmd, "STOCK_CARD_ID", DbType.Int32, MakeDbNull(model.StockCardId));
                    db.AddInParameter(cmd, "BEFORE_COUNT_QUANTITY", DbType.Decimal, item.BeforeQty);
                    db.AddInParameter(cmd, "AFTER_COUNT_QUANTITY", DbType.Decimal, item.AfterQty);
                    db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                    db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                    db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                    db.ExecuteNonQuery(cmd, transaction);

                    model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                    model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                    if (model.ErrorNo > 0)
                    {
                        model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
                        isSuccess = false;
                    }
                }

            }
            catch (Exception ex)
            {
                isSuccess = false;
                model.ErrorNo = 1;
                model.ErrorMessage = ex.Message;
            }
            finally
            {
                if (isSuccess)
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                }
                CloseConnection();
            }
        }

        public Dictionary<bool, decimal> Exists(int cycleCountId, int stockCardId, int warehouseId)
        {
            var result = new Dictionary<bool, decimal>();

            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_EXISTS_STOCK_DIFF");
                db.AddInParameter(cmd, "ID_CYCLE_COUNT", DbType.Int64, MakeDbNull(cycleCountId));
                db.AddInParameter(cmd, "ID_STOCK_CARD", DbType.Int64, MakeDbNull(stockCardId));
                db.AddInParameter(cmd, "ID_WAREHOUSE", DbType.Int64, MakeDbNull(warehouseId));

                CreateConnection(cmd);

                if (cmd.ExecuteScalar() == DBNull.Value)
                    result.Add(false, 0);
                else
                    result.Add(true, Convert.ToDecimal(cmd.ExecuteScalar()));

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

        public List<CycleCountResultPrototypeViewModel> ListStokTypeAudit(UserInfo user, CycleCountResultAuditViewModel filter)
        {
            var retVal = new List<CycleCountResultPrototypeViewModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CYCLE_COUNT_STOCK_DIFF_AUDIT");
                db.AddInParameter(cmd, "ID_CYCLE_COUNT", DbType.Int64, MakeDbNull(filter.CycleCountId));
                db.AddInParameter(cmd, "STOCK_CARD_ID", DbType.Int32, MakeDbNull(filter.StockCardId));
                db.AddInParameter(cmd, "WAREHOUSE_ID", DbType.Int32, MakeDbNull(filter.WarehouseId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, DBNull.Value);
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, DBNull.Value);
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, 10);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, 10);
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 0);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var cycleCountResultPrototypeViewModel = new CycleCountResultPrototypeViewModel
                        {
                            StockName = reader["StockType"].GetValue<string>(),
                            BeforeQty = reader["BeforeQty"].GetValue<decimal>(),
                            AfterQty = reader["AfterQty"].GetValue<decimal>(),
                            StockTypeId = reader["StockTypeId"].GetValue<int>(),
                            CycleCountDiffAllow = reader["DiffAllow"].GetValue<bool>()
                        };

                        retVal.Add(cycleCountResultPrototypeViewModel);
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

        public void ApproveCycleCountProcess(UserInfo user, CycleCountStockDiffViewModel filter)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_CYCLE_COUNT_STOCK_TRANSACTION");
                db.AddInParameter(cmd, "CYCLE_COUNT_ID", DbType.Int32, filter.CycleCountId);
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, filter.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();


                filter.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                filter.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();

                if (filter.ErrorNo == 1)
                    filter.ErrorMessage = ResolveDatabaseErrorXml(filter.ErrorMessage);
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

        public decimal GetCycleCountStockDiffTotalQuantity(int warehouseId, int stockCardId, int cycleCountId)
        {
            decimal quantity = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_CYCLE_COUNT_STOCK_DIFF_TOTAL_QTY");
                db.AddInParameter(cmd, "ID_CYCLE_COUNT", DbType.Int32, cycleCountId);
                db.AddInParameter(cmd, "WAREHOUSE_ID", DbType.Int32, warehouseId);
                db.AddInParameter(cmd, "ID_STOCK_CARD", DbType.Int32, stockCardId);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        quantity = reader["TOTAL_QTY"].GetValue<decimal>();
                    }
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

            return quantity;
        }
    }
}
