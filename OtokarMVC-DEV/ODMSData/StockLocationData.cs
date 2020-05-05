using ODMSCommon.Security;
using ODMSModel.StockLocation;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using System;

namespace ODMSData
{
    public class StockLocationData : DataAccessBase
    {
        public List<StockLocationListModel> ListStockLocation(UserInfo user, StockLocationListModel filter, out int totalCount)
        {
            var retVal = new List<StockLocationListModel>();
            System.Data.Common.DbDataReader dbDataReader = null;
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_STOCK_LOCATION");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(filter.IdDealer));
                db.AddInParameter(cmd, "ID_PART", DbType.Int64, MakeDbNull(filter.IdPart));
                db.AddInParameter(cmd, "PART_CODE", DbType.String, MakeDbNull(filter.PartCode));
                db.AddInParameter(cmd, "PART_NAME", DbType.String, MakeDbNull(filter.PartName));
                db.AddInParameter(cmd, "ID_RACK", DbType.Int32, MakeDbNull(filter.IdRack));
                db.AddInParameter(cmd, "ID_WAREHOUSE", DbType.Int32, MakeDbNull(filter.IdWarehouse));
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
                        var stockLocationListModel = new StockLocationListModel
                        {
                            DealerName = dbDataReader["DEALER_SHRT_NAME"].GetValue<string>(),
                            PartCode = dbDataReader["PART_CODE"].GetValue<string>(),
                            PartName = dbDataReader["PART_NAME"].GetValue<string>(),
                            WarehouseCode = dbDataReader["WAREHOUSE_CODE"].GetValue<string>(),
                            WarehouseName = dbDataReader["WAREHOUSE_NAME"].GetValue<string>(),
                            RackCode = dbDataReader["RACK_CODE"].GetValue<string>(),
                            RackName = dbDataReader["RACK_NAME"].GetValue<string>(),
                            Quantity = dbDataReader["QUANTITY"].GetValue<decimal>(),
                            Unit = dbDataReader["UNIT"].GetValue<string>(),
                        };

                        retVal.Add(stockLocationListModel);
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
    }
}
