using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSCommon.Logging;
using ODMSCommon.Security;
using ODMSModel.CycleCountResult;
using ODMSModel.StockTypeDetail;

namespace ODMSData
{
    public class StockTypeDetailData : DataAccessBase
    {
        public decimal GetStockTypeDetailQuantity(UserInfo user, int stockCardId, int warehouseId, int stockTypeId)
        {
            decimal quantity = 0;
            int errorNo = 0;
            string errorMessage = string.Empty;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_STOCK_TYPE_DETAIL");
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, user.GetUserDealerId());
                db.AddInParameter(cmd, "STOCK_CARD_ID", DbType.Int32, stockCardId);
                db.AddInParameter(cmd, "WAREHOUSE_ID", DbType.Int32, warehouseId);
                db.AddInParameter(cmd, "STOCK_TYPE_ID", DbType.Int32, stockTypeId);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        quantity = reader["QUANTITY"].GetValue<decimal>();
                    }
                    reader.Close();
                }
                errorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                errorMessage = errorNo > 0 ? db.GetParameterValue(cmd, "ERROR_DESC").ToString() : string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();

                if (errorNo > 0)
                {
                    //logging error message
                    var logger = new Loggable();
                    logger.ErrorAsync(ResolveDatabaseErrorXml(errorMessage));
                }
            }
            return quantity;
        }

        public decimal GetStockTypeDetailTotalQuantity(int warehouseId, int partId)
        {
            decimal quantity = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_STOCK_TYPE_DETAIL_TOTAL_QTY");
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, partId);
                db.AddInParameter(cmd, "WAREHOUSE_ID", DbType.Int32, warehouseId);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        quantity = reader["TOTAL_QTY"].GetValue<decimal>();
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

            return quantity;
        }

        public List<StockTypeDetailListModel> ListStockTypeDetail(UserInfo user, StockTypeDetailListModel filter, out int totalCount)
        {
            var retVal = new List<StockTypeDetailListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_STOCK_TYPE_DETAIL");
                db.AddInParameter(cmd, "ID_PART", DbType.Int64, MakeDbNull(filter.IdPart));
                db.AddInParameter(cmd, "PART_CODE", DbType.String, MakeDbNull(filter.PartCode));
                db.AddInParameter(cmd, "PART_NAME", DbType.String, MakeDbNull(filter.PartName));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(filter.IdDealer));
                db.AddInParameter(cmd, "ID_STOCK_TYPE", DbType.Int32, MakeDbNull(filter.IdStockType));

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
                        var stockTypeDetailListModel = new StockTypeDetailListModel
                        {
                            IdPart = reader["ID_PART"].GetValue<Int64>(),
                            IdDealer = reader["ID_DEALER"].GetValue<int>(),
                            DealerName = reader["DEALER_NAME"].GetValue<string>(),
                            IdWarehouse = reader["WAREHOUSE_ID"].GetValue<int>(),
                            WarehouseName = reader["WAREHOUSE_NAME"].GetValue<string>(),
                            PartName = reader["PART_NAME"].GetValue<string>(),
                            StockQuantity = reader["STOCK_QUANTITY"].GetValue<decimal>(),
                            PartCode = reader["PART_CODE"].GetValue<string>(),
                            IdStockType = reader["ID_STOCK_TYPE"].GetValue<int>(),
                            StockTypeName = reader["STOCK_TYPE_NAME"].GetValue<string>(),
                            AdminDesc = reader["ADMIN_DESC"].GetValue<string>(),
                            TypeQuantity = reader["TYPE_QUANTITY"].GetValue<decimal>(),
                            BlockQuantity = reader["BLOCK_QUANTITY"].GetValue<decimal>(),
                            ReserveQuantity = reader["RESERVE_QUANTITY"].GetValue<decimal>(),
                            OpenQuantity = reader["OPEN_QUANTITY"].GetValue<decimal>(),
                            UnitName = reader["UNIT_NAME"].GetValue<string>(),
                            AllowServiceEquipment = reader["ALLOW_SERVICE_EQUIPMENT"].GetValue<bool>()
                        };
                        retVal.Add(stockTypeDetailListModel);
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

        public List<CycleCountResultPrototypeViewModel> ListStokTypeAudit(UserInfo user, CycleCountResultAuditViewModel filter)
        {
            var retVal = new List<CycleCountResultPrototypeViewModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_STOCK_TYPE_DETAIL_AUDIT");
                db.AddInParameter(cmd, "WAREHOUSE_ID", DbType.Int64, MakeDbNull(filter.WarehouseId));
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, MakeDbNull(filter.PartId));
                db.AddInParameter(cmd, "ID_CYCLE_COUNT", DbType.Int32, MakeDbNull(filter.CycleCountId));
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
    }
}
