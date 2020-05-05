using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.StockCard;
using ODMSCommon.Resources;
using System.Data.SqlClient;

namespace ODMSData
{
    public class StockCardData : DataAccessBase
    {

        public decimal GetDealerPriceByDealerAndPart(int partId, int dealerId)
        {
            var value = new decimal();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_STOCK_CARD_DEALER_PRICE");
                db.AddInParameter(cmd, "ID_PART", DbType.String, MakeDbNull(partId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.String, MakeDbNull(dealerId));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        value = reader["VALUE"].GetValue<decimal>();
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
            return value;
        }
        public List<StockCardSearchListModel> ListStockCardSearch(UserInfo user, StockCardSearchListModel filter, out int totalCount)
        {
            var retVal = new List<StockCardSearchListModel>();
            totalCount = 0;
            try
            {

                using (var table = new DataTable())
                {
                    table.Columns.Add("Item", typeof(string));

                    if (!String.IsNullOrEmpty(filter.PartCodeList))
                        foreach (string partCode in filter.PartCodeList.Split(','))
                            table.Rows.Add(partCode);

                    var pList = new SqlParameter("PART_CODE_LIST", table);
                    pList.SqlDbType = SqlDbType.Structured;

                    CreateDatabase();
                    var cmd = db.GetStoredProcCommand("P_SEARCH_STOCK_CARD");
                    cmd.CommandTimeout = 14440;
                    db.AddInParameter(cmd, "PART_CODE", DbType.String, MakeDbNull(filter.PartCode));
                    db.AddInParameter(cmd, "PART_NAME", DbType.String, MakeDbNull(filter.PartName));
                    db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(filter.DealerId));
                    db.AddInParameter(cmd, "CURRENT_DEALER_ID", DbType.Int32, MakeDbNull(filter.CurrentDealerId));
                    db.AddInParameter(cmd, "CENTRAL_DEALER_IDS", DbType.String, MakeDbNull(filter.DealerIdsForCentral));
                    db.AddInParameter(cmd, "DEALER_REGION_IDS", DbType.String, MakeDbNull(filter.DealerRegionIds));
                    db.AddInParameter(cmd, "DEALER_REGION_TYPE", DbType.Int32, MakeDbNull(filter.DealerRegionType));
                    db.AddInParameter(cmd, "IS_HQ", DbType.Boolean, MakeDbNull(filter.IsHq));
                    db.AddInParameter(cmd, "STOCK_TYPE_ID", DbType.Int32, MakeDbNull(filter.StockTypeId));
                    db.AddInParameter(cmd, "STOCK_LOCATION_ID", DbType.Int32, MakeDbNull(filter.StockLocationId));
                    db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                    db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                    db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                    db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                    db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                    db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                    db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                    db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                    cmd.Parameters.Add(pList);

                    CreateConnection(cmd);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var stockTypeDetailListModel = new StockCardSearchListModel
                            {
                                PartId = reader["PART_ID"].GetValue<int>(),
                                PartCode = reader["PART_CODE"].GetValue<string>(),
                                PartName = reader["PART_NAME"].GetValue<string>(),
                                DealerId = reader["DEALER_ID"].GetValue<int>(),
                                DealerName = reader["DEALER_NAME"].GetValue<string>(),
                                StockTypeId = reader["STOCK_TYPE_ID"].GetValue<int>(),
                                StockTypeName = reader["STOCK_TYPE_NAME"].GetValue<string>(),
                                StockQuantity = reader["STOCK_QUANTITY"].GetValue<decimal?>(),
                                StockQuantityString = reader["STOCK_QUANTITY"].GetValue<decimal>() + " " + reader["UNIT"].GetValue<string>(),
                                UnitName = reader["UNIT"].GetValue<string>(),
                                SalePrice = reader["SALE_PRICE"].GetValue<decimal?>(),
                                AveragePrice = reader["AVERAGE_PRICE"].GetValue<decimal?>()
                            };
                            retVal.Add(stockTypeDetailListModel);

                        }
                        reader.Close();
                    }
                    totalCount = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
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

        public List<StockCardListModel> ListStockCards(UserInfo user, StockCardListModel filter, out int totalCount)
        {
            var retVal = new List<StockCardListModel>();
            System.Data.Common.DbDataReader dbDataReader = null;
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_STOCK_CARDS");
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(filter.DealerId));
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, MakeDbNull(filter.PartId));
                db.AddInParameter(cmd, "RACK_ID", DbType.Int32, MakeDbNull(filter.RackId));
                db.AddInParameter(cmd, "IS_ORIGINAL", DbType.Int32, MakeDbNull(filter.IsOriginalPart));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (dbDataReader = cmd.ExecuteReader())
                {
                    while (dbDataReader.Read())
                    {
                        var roleListModel = new StockCardListModel
                        {
                            AutoOrder = dbDataReader["AUTO_ORDER"].GetValue<bool>(),
                            AutoOrderName = dbDataReader["AUTO_ORDER_NAME"].GetValue<string>(),
                            AvgDealerPrice = dbDataReader["AVG_DEALER_PRICE"].GetValue<decimal>(),
                            CriticalStockQuantity = dbDataReader["CRITICAL_STOCK_QUANTITY"].GetValue<decimal>(),
                            DealerId = dbDataReader["DEALER_ID"].GetValue<int?>(),
                            DealerName = dbDataReader["DEALER_NAME"].GetValue<string>(),
                            IsOriginalPart = dbDataReader["IS_ORIGINAL_PART"].GetValue<bool>(),
                            IsOriginalPartName = dbDataReader["IS_ORIGINAL_PART_NAME"].GetValue<string>(),
                            MaxStockQuantity = dbDataReader["MAX_STOCK_QUANTITY"].GetValue<decimal>(),
                            MinSaleQuantity = dbDataReader["MIN_SALE_QUANTITY"].GetValue<decimal>(),
                            MinStockQuantity = dbDataReader["MIN_STOCK_QUANTITY"].GetValue<decimal>(),
                            PartCode = dbDataReader["PART_CODE"].GetValue<string>(),
                            PartDealerId = dbDataReader["PART_DEALER_ID"].GetValue<int?>(),
                            PartDealerName = dbDataReader["PART_DEALER_NAME"].GetValue<string>(),
                            PartId = dbDataReader["PART_ID"].GetValue<int?>(),
                            PartName = dbDataReader["PART_NAME"].GetValue<string>(),
                            ProfitMarginRatio = dbDataReader["PROFIT_MARGIN_RATIO"].GetValue<decimal>(),
                            RackId = dbDataReader["RACK_ID"].GetValue<int?>(),
                            RackName = dbDataReader["RACK_NAME"].GetValue<string>(),
                            SalePrice = dbDataReader["SALE_PRICE"].GetValue<decimal>(),
                            StockCardId = dbDataReader["STOCK_CARD_ID"].GetValue<int>(),
                            VatRatio = 0,
                            Volume = dbDataReader["VOLUME"].GetValue<decimal>(),
                            WarehouseId = dbDataReader["WAREHOUSE_ID"].GetValue<int>(),
                            WarehouseName = dbDataReader["WAREHOUSE_NAME"].GetValue<string>(),
                            Weight = dbDataReader["WEIGHT"].GetValue<decimal>()
                        };

                        retVal.Add(roleListModel);
                    }
                    dbDataReader.Close();
                }
                totalCount = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbDataReader != null && !dbDataReader.IsClosed)
                    dbDataReader.Close();
                CloseConnection();
            }

            return retVal;
        }

        public void DMLStockCard(UserInfo user, StockCardViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_STOCK_CARD");
                db.AddParameter(cmd, "STOCK_CARD_ID", DbType.Int32, ParameterDirection.InputOutput, "STOCK_CARD_ID", DataRowVersion.Default, model.StockCardId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(model.DealerId));
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, MakeDbNull(model.PartId));
                db.AddInParameter(cmd, "RACK_ID", DbType.Int32, MakeDbNull(model.RackId));
                db.AddInParameter(cmd, "AUTO_ORDER", DbType.Int32, MakeDbNull(model.AutoOrder));
                db.AddInParameter(cmd, "CRITICAL_STOCK_QUANTITY", DbType.Decimal, MakeDbNull(model.CriticalStockQuantity));
                db.AddInParameter(cmd, "MIN_STOCK_QUANTITY", DbType.Decimal, MakeDbNull(model.MinStockQuantity));
                db.AddInParameter(cmd, "MAX_STOCK_QUANTITY", DbType.Decimal, MakeDbNull(model.MaxStockQuantity));
                db.AddInParameter(cmd, "AVG_DEALER_PRICE", DbType.Decimal, model.AvgDealerPrice);
                db.AddInParameter(cmd, "MIN_SALE_QUANTITY", DbType.Decimal, MakeDbNull(model.MinSaleQuantity));
                db.AddInParameter(cmd, "SALE_PRICE", DbType.Decimal, MakeDbNull(model.SalePrice));
                db.AddInParameter(cmd, "PROFIT_MARGIN_RATIO", DbType.Decimal, MakeDbNull(model.ProfitMarginRatio));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.StockCardId = db.GetParameterValue(cmd, "STOCK_CARD_ID").GetValue<int>();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 2)
                    model.ErrorMessage = MessageResource.StockCard_Error_NullId;
                else if (model.ErrorNo == 1)
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

        public StockCardViewModel GetStockCard(UserInfo user, StockCardViewModel filter)
        {
            System.Data.Common.DbDataReader reader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_STOCK_CARD");
                //db.AddInParameter(cmd, "STOCK_CARD_ID", DbType.Int32, MakeDbNull(referenceModel.StockCardId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(filter.DealerId));
                db.AddInParameter(cmd, "ID_PART", DbType.Int64, MakeDbNull(filter.PartId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    filter.AutoOrder = reader["AUTO_ORDER"].GetValue<bool>();
                    filter.AutoOrderName = reader["AUTO_ORDER_NAME"].GetValue<string>();
                    filter.AvgDealerPrice = reader["AVG_DEALER_PRICE"].GetValue<decimal>();
                    filter.CriticalStockQuantity = reader["CRITICAL_STOCK_QUANTITY"].GetValue<decimal>();
                    filter.DealerId = reader["DEALER_ID"].GetValue<int?>();
                    filter.DealerName = reader["DEALER_NAME"].GetValue<string>();
                    filter.IsOriginalPart = reader["IS_ORIGINAL_PART"].GetValue<bool>();
                    filter.IsOriginalPartName = reader["IS_ORIGINAL_PART_NAME"].GetValue<string>();
                    filter.LeadTime = reader["LEAD_TIME"].GetValue<int>();
                    filter.MaxStockQuantity = reader["MAX_STOCK_QUANTITY"].GetValue<decimal>();
                    filter.MinSaleQuantity = reader["MIN_SALE_QUANTITY"].GetValue<decimal>();
                    filter.MinStockQuantity = reader["MIN_STOCK_QUANTITY"].GetValue<decimal>();
                    filter.PartCode = reader["PART_CODE"].GetValue<string>();
                    filter.PartDealerId = reader["PART_DEALER_ID"].GetValue<int?>();
                    filter.PartDealerName = reader["PART_DEALER_NAME"].GetValue<string>();
                    filter.PartId = reader["PART_ID"].GetValue<int?>();
                    filter.PartName = reader["PART_NAME"].GetValue<string>();
                    filter.ProfitMarginRatio = reader["PROFIT_MARGIN_RATIO"].GetValue<decimal>();
                    filter.RackId = reader["RACK_ID"].GetValue<int?>();
                    filter.RackName = reader["RACK_NAME"].GetValue<string>();
                    filter.SalePrice = reader["SALE_PRICE"].GetValue<decimal>();
                    filter.StockCardId = reader["STOCK_CARD_ID"].GetValue<int>();
                    filter.VatRatio = reader["VAT_RATIO"].GetValue<decimal>();
                    filter.Volume = reader["VOLUME"].GetValue<decimal>();
                    filter.WarehouseId = reader["WAREHOUSE_ID"].GetValue<int?>();
                    filter.WarehouseName = reader["WAREHOUSE_NAME"].GetValue<string>();
                    filter.Weight = reader["WEIGHT"].GetValue<decimal>();
                    filter.SaleChannelDiscountRatio = reader["DISCOUNT_RATIO"].GetValue<decimal?>();
                    filter.StartupStockQty = reader["STARTUP_STOCK_QTY"].GetValue<decimal?>();
                    filter.StockQuantity = reader["STOCK_QUANTITY"].GetValue<decimal?>();
                    filter.LastPrice = reader["LAST_PRICE"].GetValue<decimal?>();
                    filter.AlternatePart = reader["ALTERNATE_PART"].GetValue<string>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                    reader.Dispose();
                }
                CloseConnection();
            }
            return filter;
        }

        /// <summary>
        /// STOK ARAMA EKRANI TOPLAM STOK DEĞERLERİ
        /// </summary>
        /// <param name="user"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public StockCardStockValueModel GetStockCardStockValues(UserInfo user, StockCardStockValueModel filter)
        {
            System.Data.Common.DbDataReader reader = null;
            try
            {

                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_SERVICE_PRICED_STOCK_TOTAL");
                cmd.CommandTimeout = 14440;
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(filter.DealerId));
                db.AddInParameter(cmd, "CURRENT_DEALER_ID", DbType.Int32, MakeDbNull(filter.CurrentDealerId));
                db.AddInParameter(cmd, "CENTRAL_DEALER_IDS", DbType.String, MakeDbNull(filter.DealerIdsForCentral));
                db.AddInParameter(cmd, "DEALER_REGION_IDS", DbType.String, MakeDbNull(filter.DealerRegionIds));
                db.AddInParameter(cmd,"DEALER_REGION_TYPE",DbType.Int32, MakeDbNull(filter.DealerRegionType));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "IS_HQ", DbType.Boolean, MakeDbNull(filter.IsHq));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    filter.OriginalPrice = reader["ORIGINAL_PRICE"].GetValue<decimal>();
                    filter.TrCodePrice = reader["TR_CODE_PRICE"].GetValue<decimal>();
                    filter.TotalPrice = reader["TOTAL_PRICE"].GetValue<decimal>();
                    filter.OriginalAveragePrice = reader["ORIGINAL_AVERAGE_PRICE"].GetValue<decimal>();
                    filter.TrAveragePrice = reader["TR_AVERAGE_PRICE"].GetValue<decimal>();
                    filter.AverageTotalPrice = reader["AVERAGE_TOTAL_PRICE"].GetValue<decimal>();
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return filter;
        }


        public StockCardViewModel GetStockCardById(StockCardViewModel filter)
        {
            System.Data.Common.DbDataReader reader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_STOCK_CARD_BY_ID");
                db.AddInParameter(cmd, "STOCK_CARD_ID", DbType.Int32, MakeDbNull(filter.StockCardId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    filter.PartId = reader["ID_PART"].GetValue<int?>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                    reader.Dispose();
                }
                CloseConnection();
            }
            return filter;
        }
    }
}
