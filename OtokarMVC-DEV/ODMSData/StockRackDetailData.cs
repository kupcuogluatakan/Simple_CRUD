using System.Collections.Generic;
using ODMSCommon.Resources;
using ODMSModel.StockRackDetail;
using System.Data;
using ODMSCommon;
using ODMSCommon.Security;
using System;
using System.Data.Common;

namespace ODMSData
{
    public class StockRackDetailData : DataAccessBase
    {
        public List<StockRackDetailListModel> ListEmptyStockRackDetail(UserInfo user, StockRackDetailListModel filter, out int totalCount)
        {
            var retVal = new List<StockRackDetailListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_EMPTY_STOCK_RACK_DETAIL");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, filter.IdDealer);
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
                        var stockRackDetailListModel = new StockRackDetailListModel
                        {
                            RackName = reader["RACK_NAME"].GetValue<string>(),
                            WarehouseName = reader["WAREHOUSE_NAME"].GetValue<string>()
                        };

                        retVal.Add(stockRackDetailListModel);
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

        public List<StockRackDetailListModel> ListStockRackDetail(UserInfo user, StockRackDetailListModel filter, out int totalCount)
        {
            var retVal = new List<StockRackDetailListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_STOCK_RACK_DETAIL");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, filter.IdDealer);
                db.AddInParameter(cmd, "ID_PART", DbType.Int32, filter.PartId);
                db.AddInParameter(cmd, "ID_RACK", DbType.Int32, filter.RackId);
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
                        var stockRackDetailListModel = new StockRackDetailListModel
                        {
                            RackId = reader["RACK_ID"].GetValue<int>(),
                            RackName = reader["RACK_NAME"].GetValue<string>(),
                            WarehouseId = reader["WAREHOUSE_ID"].GetValue<int>(),
                            WarehouseName = reader["WAREHOUSE_NAME"].GetValue<string>(),
                            PartId = reader["PART_ID"].GetValue<int>(),
                            PartCode = reader["PART_CODE"].GetValue<string>(),
                            PartName = reader["PART_NAME"].GetValue<string>(),
                            IdDealer = reader["DEALER_ID"].GetValue<int>(),
                            Quantity = reader["QUANTITY"].GetValue<decimal>()
                        };

                        retVal.Add(stockRackDetailListModel);
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

        public List<StockRackTypeDetailListModel> ListStockRackTypeDetail(UserInfo user, StockRackTypeDetailListModel filter, out int totalCount)
        {
            var retVal = new List<StockRackTypeDetailListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_STOCK_RACK_TYPE_DETAIL");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, filter.IdDealer);
                db.AddInParameter(cmd, "ID_PART", DbType.Int32, filter.PartId);
                db.AddInParameter(cmd, "ID_RACK", DbType.Int32, filter.RackId);
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
                        var stockRackTypeDetailListModel = new StockRackTypeDetailListModel
                        {
                            RackId = reader["RACK_ID"].GetValue<int>(),
                            RackName = reader["RACK_NAME"].GetValue<string>(),
                            WarehouseId = reader["WAREHOUSE_ID"].GetValue<int>(),
                            WarehouseName = reader["WAREHOUSE_NAME"].GetValue<string>(),
                            StockTypeName = reader["STOCK_TYPE_NAME"].GetValue<string>(),
                            PartId = reader["PART_ID"].GetValue<int>(),
                            PartCode = reader["PART_CODE"].GetValue<string>(),
                            PartName = reader["PART_NAME"].GetValue<string>(),
                            IdDealer = reader["DEALER_ID"].GetValue<int>(),
                            Quantity = reader["QUANTITY"].GetValue<decimal>()
                        };

                        retVal.Add(stockRackTypeDetailListModel);
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

        public decimal GetMovableQuantity(int dealerId, long partId, int stockTypeId, int fromRackId)
        {
            decimal result = 0;
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_STOCK_MOVABLE_QUANTITY");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(dealerId));
                db.AddInParameter(cmd, "ID_PART", DbType.Int64, MakeDbNull(partId));
                db.AddInParameter(cmd, "ID_STOCK_TYPE", DbType.Int32, MakeDbNull(stockTypeId));
                db.AddInParameter(cmd, "ID_FROM_RACK", DbType.Int32, MakeDbNull(fromRackId));

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        result = dr[0].GetValue<decimal>();
                    }
                    dr.Close();
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

        public void DMLStockExchange(UserInfo user, StockExchangeViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_STOCK_EXCHANGE");
                db.AddParameter(cmd, "STOCK_TRANSACTION_ID", DbType.Int32, ParameterDirection.InputOutput, "STOCK_TRANSACTION_ID",
                    DataRowVersion.Default, model.StockTransactionId);
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, MakeDbNull(model.PartId));
                db.AddInParameter(cmd, "QUANTITY", DbType.Decimal, MakeDbNull(model.Quantity));
                db.AddInParameter(cmd, "FROM_RACK_ID", DbType.Int32, MakeDbNull(model.FromRackId));
                db.AddInParameter(cmd, "FROM_WAREHOUSE_ID", DbType.Int32, MakeDbNull(model.FromWarehouseId));
                db.AddInParameter(cmd, "FROM_STOCK_TYPE_ID", DbType.Int32, MakeDbNull(model.StockTypeId));
                db.AddInParameter(cmd, "TO_RACK_ID", DbType.Int32, MakeDbNull(model.ToRackId));
                db.AddInParameter(cmd, "TO_WAREHOUSE_ID", DbType.Int32, MakeDbNull(model.ToWarehouseId));
                db.AddInParameter(cmd, "TO_STOCK_TYPE_ID", DbType.Int32, MakeDbNull(model.StockTypeId));
                db.AddInParameter(cmd, "DESCRIPTION", DbType.String, MakeDbNull(model.Description));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.StockTransactionId = db.GetParameterValue(cmd, "STOCK_TRANSACTION_ID").GetValue<int>();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 1)
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

        public decimal GetQuantity(long rackId, long partId)
        {
            decimal result = 0;
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_STOCK_REACK_DETAIL_QUANTITY");
                db.AddInParameter(cmd, "RACK_ID", DbType.String, MakeDbNull(rackId));
                db.AddInParameter(cmd, "PART_ID", DbType.String, MakeDbNull(partId));

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        result = dr[0].GetValue<decimal>();
                    }
                    dr.Close();
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


    }
}
