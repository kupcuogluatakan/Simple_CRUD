using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.CycleCountResult;
using System;
using System.Data.SqlClient;

namespace ODMSData
{
    public class CycleCountResultData : DataAccessBase
    {

        public List<CycleCountResultListModel> ListCycleCountResults(UserInfo user, CycleCountResultListModel filter, out int totalCount)
        {
            var retVal = new List<CycleCountResultListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CYCLE_COUNT_RESULTS");
                db.AddInParameter(cmd, "CYCLE_COUNT_ID", DbType.Int32, MakeDbNull(filter.CycleCountId));
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var cycleCountResultListModel = new CycleCountResultListModel
                        {
                            CycleCountResultId = reader["CYCLE_COUNT_RESULT_ID"].GetValue<int>(),
                            IS_ONRACK = reader["IS_ONRACK"].GetValue<int>(),
                            CycleCountId = reader["CYCLE_COUNT_ID"].GetValue<int>(),
                            WarehouseId = reader["WAREHOUSE_ID"].GetValue<int>(),
                            WarehouseName = reader["WAREHOUSE_NAME"].GetValue<string>(),
                            RackId = reader["RACK_ID"].GetValue<int?>(),
                            RackName = reader["RACK_NAME"].GetValue<string>(),
                            StockCardId = reader["STOCK_CARD_ID"].GetValue<int?>(),//Real value is partId
                            StockCardName = reader["STOCK_CARD_NAME"].GetValue<string>(),
                            AfterCountQuantity = reader["AFTER_COUNT_QUANTITY"].GetValue<decimal?>(),
                            BeforeFreeOfChargeCountQuantity = reader["BEFORE_FREEOFCHARGE_COUNT_QUANTITY"].GetValue<decimal>(),
                            BeforePaidCountQuantity = reader["BEFORE_PAID_COUNT_QUANTITY"].GetValue<decimal>(),
                            BeforeCampaignCountQuantity = reader["BEFORE_CAMPAIGN_COUNT_QUANTITY"].GetValue<decimal>(),
                            BeforeCountQuantity = reader["BEFORE_COUNT_QUANTITY"].GetValue<decimal>(),
                            ApprovedCountQuantity = reader["APPROVED_COUNT_QUANTITY"].GetValue<decimal>(),
                            CountUser = reader["COUNT_USER"].GetValue<int>(),
                            CountUserName = reader["COUNT_USER_NAME"].GetValue<string>(),
                            RealStockCardId = reader["ID_STOCK_CARD"].GetValue<int>(),
                            CycleCountStatus = reader["STATUS"].GetValue<int>(),
                            Price = reader["PRICE"].GetValue<decimal>(),
                            PartCode = reader["PART_CODE"].GetValue<string>(),
                            CurrencyCode = reader["CURRENCY_CODE"].GetValue<string>(),
                            Unit = reader["UNIT"].GetValue<string>(),
                            StockDiffQty = reader["STOCK_DIFF_QUANTITY"].GetValue<string>(),
                            PriceString = reader["PRICE_STRING"].GetValue<string>(),
                            TotalQty = reader["DETAIL_TOTAL_QUANTITY"].GetValue<decimal?>(),
                            DetailTotalQty =
                            reader["DETAIL_TOTAL_QUANTITY"].GetValue<decimal?>() == null ?
                                reader["TOTAL_QUANTITY"].GetValue<decimal>() :
                                reader["STOCK_TOTAL_QUANTITY"].GetValue<decimal>()
                        };

                        retVal.Add(cycleCountResultListModel);
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
        public static bool ChangeColumnDataType(DataTable table, string columnname, Type newtype)
        {
            if (table.Columns.Contains(columnname) == false)
                return false;

            DataColumn column = table.Columns[columnname];
            if (column.DataType == newtype)
                return true;

            try
            {
                DataColumn newcolumn = new DataColumn("temporary", newtype);
                table.Columns.Add(newcolumn);
                foreach (DataRow row in table.Rows)
                {
                    try
                    {
                        row["temporary"] = Convert.ChangeType(row[columnname], newtype);
                    }
                    catch
                    {
                    }
                }
                table.Columns.Remove(columnname);
                newcolumn.ColumnName = columnname;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

      
        public static bool ChangeColumnDataTypes(DataTable table, string[] columnnames, Type[] newtypes)
        {
            int tpCount = 0, clCount = 0;
            for (int i = 0; i < columnnames.Length; i++)
            {
                string columnname = columnnames[i];
                Type newtype = newtypes[i];

                if (table.Columns.Contains(columnname) == false)
                    clCount++;

                DataColumn column = table.Columns[columnname];
                if (column.DataType == newtype)
                    tpCount++;
            }

            if (clCount > 0)
                return false;

            if (tpCount == newtypes.Length)
                return true;

            try
            {
                for (int i = 0; i < columnnames.Length; i++)
                {
                    DataColumn oldColumn = table.Columns[columnnames[i]];
                    DataColumn newcolumn = new DataColumn("temporary", newtypes[i]);

                    table.Columns.Add(newcolumn);

                    foreach (DataRow row in table.Rows)
                    {
                        try
                        {
                            row["temporary"] = Convert.ChangeType(row[columnnames[i]], newtypes[i]);
                        }
                        catch
                        {
                        }
                    }

                    int oldcolIndex = oldColumn.Ordinal;
                    table.Columns.Remove(oldColumn);

                    newcolumn.ColumnName = columnnames[i];
                    newcolumn.SetOrdinal(oldcolIndex);
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }





        public string DMLCycleCountResultBulk(UserInfo user, int cycleCountId, DataTable cycleCountResultList)
        {
            string errorMessage = string.Empty;
            try
            {
                CreateDatabase();

                // columnrename
                cycleCountResultList.Columns[0].ColumnName = "WAREHOUSE_CODE";
                cycleCountResultList.Columns[1].ColumnName = "RACK_CODE";
                cycleCountResultList.Columns[2].ColumnName = "PART_CODE";
                cycleCountResultList.Columns[3].ColumnName = "QUANTITY";
                
                ChangeColumnDataTypes(
                    cycleCountResultList,
                    new string[] {
                        "PART_CODE",
                        "QUANTITY"
                    }, 
                    new Type[] {
                        typeof(string),
                        typeof(string)
                    });
                

                var pList = new SqlParameter("CYCLE_COUNT_RESULT_LIST", cycleCountResultList);
                pList.SqlDbType = SqlDbType.Structured;

                var cmd = db.GetStoredProcCommand("P_DML_CYCLE_COUNT_RESULT_BULK");
                db.AddInParameter(cmd, "CYCLE_COUNT_ID", DbType.Int32, cycleCountId);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_MESSAGE", DbType.String, 1000);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                cmd.Parameters.Add(pList);

                CreateConnection(cmd);

                cmd.ExecuteNonQuery();

                errorMessage = db.GetParameterValue(cmd, "ERROR_MESSAGE").GetValue<string>();
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
        public void DMLCycleCountResult(UserInfo user, CycleCountResultViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_CYCLE_COUNT_RESULT");
                db.AddParameter(cmd, "CYCLE_COUNT_RESULT_ID", DbType.Int32, ParameterDirection.InputOutput, "CYCLE_COUNT_RESULT_ID", DataRowVersion.Default, model.CycleCountResultId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "RACK_ID", DbType.String, MakeDbNull(model.RackId));
                db.AddInParameter(cmd, "WAREHOUSE_ID", DbType.Int32, MakeDbNull(model.WarehouseId));
                db.AddInParameter(cmd, "STOCK_CARD_ID", DbType.Int32, MakeDbNull(model.StockCardId));
                db.AddInParameter(cmd, "CYCLE_COUNT_ID", DbType.Int32, MakeDbNull(model.CycleCountId));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "BEFORE_COUNT_QUANTITY", DbType.Int32, MakeDbNull(model.BeforeCountQuantity));

                db.AddInParameter(cmd, "AFTER_COUNT_QUANTITY", DbType.Decimal, model.AfterCountQuantity);
                db.AddInParameter(cmd, "APPROVED_COUNT_QUANTITY", DbType.Decimal, model.ApprovedCountQuantity);

                db.AddInParameter(cmd, "REJECT_DESCRIPTION", DbType.Int32, MakeDbNull(model.RejectDescription));
                db.AddInParameter(cmd, "COUNT_USER", DbType.Int32, MakeDbNull(model.CountUser));
                db.AddInParameter(cmd, "APPROVE_USER", DbType.Int32, MakeDbNull(model.ApproveUser));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.CycleCountResultId = db.GetParameterValue(cmd, "CYCLE_COUNT_RESULT_ID").GetValue<int>();
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

        public CycleCountResultViewModel GetCycleCountResult(UserInfo user, CycleCountResultViewModel filter)
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_CYCLE_COUNT_RESULT");
                db.AddInParameter(cmd, "ID_CYCLE_COUNT_RESULT", DbType.Int32, MakeDbNull(filter.CycleCountResultId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.CycleCountResultId = dReader["CYCLE_COUNT_RESULT_ID"].GetValue<int>();
                    filter.CycleCountId = dReader["CYCLE_COUNT_ID"].GetValue<int>();
                    filter.RackId = dReader["RACK_ID"].GetValue<int>();
                    filter.RackName = dReader["RACK_NAME"].GetValue<string>();
                    filter.WarehouseId = dReader["WAREHOUSE_ID"].GetValue<int>();
                    filter.WarehouseName = dReader["WAREHOUSE_NAME"].GetValue<string>();
                    filter.StockCardId = dReader["STOCK_CARD_ID"].GetValue<int>();
                    filter.StockCardName = dReader["STOCK_CARD_NAME"].GetValue<string>();
                    filter.AfterCountQuantity = dReader["AFTER_COUNT_QUANTITY"].GetValue<decimal>();
                    filter.BeforeCountQuantity = dReader["BEFORE_COUNT_QUANTITY"].GetValue<decimal>();
                    filter.BeforeFreeOfChargeCountQuantity = dReader["BEFORE_FREEOFCHARGE_COUNT_QUANTITY"].GetValue<decimal>();
                    filter.BeforePaidCountQuantity = dReader["BEFORE_PAID_COUNT_QUANTITY"].GetValue<decimal>();
                    filter.BeforeCampaignCountQuantity = dReader["BEFORE_CAMPAIGN_COUNT_QUANTITY"].GetValue<decimal>();
                    filter.CountUser = dReader["COUNT_USER"].GetValue<int>();
                    filter.CountUserName = dReader["COUNT_USER_NAME"].GetValue<string>();
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

    }
}
