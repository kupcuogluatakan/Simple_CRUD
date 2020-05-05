using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSModel.CriticalStockQuantity;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class CriticalStockQuantityData : DataAccessBase
    {
        public List<CriticalStockQuantityListModel> ListCriticalStockQuantity(UserInfo user,CriticalStockQuantityListModel filter, out int totalCount)
        {
            var retVal = new List<CriticalStockQuantityListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CRITICAL_STOCK_QUANTITY");
                db.AddInParameter(cmd, "PART_ID", DbType.Int64, MakeDbNull(filter.PartId));
                db.AddInParameter(cmd, "PART_CODE", DbType.String, MakeDbNull(filter.PartCode));
                db.AddInParameter(cmd, "PART_NAME", DbType.String, MakeDbNull(filter.PartName));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32,  MakeDbNull(filter.DealerId));
                db.AddInParameter(cmd, "STOCK_TYPE_ID", DbType.Int32,  MakeDbNull(filter.StockTypeId));
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var stockTypeDetailListModel = new CriticalStockQuantityListModel
                            {
                                PartId = reader["PART_ID"].GetValue<int>(),
                                PartCode = reader["PART_CODE"].GetValue<string>(),
                                PartName = reader["PART_NAME"].GetValue<string>(),
                                DealerId = reader["DEALER_ID"].GetValue<int>(),
                                DealerName = reader["DEALER_NAME"].GetValue<string>(),
                                CriticalStockLevel = reader["CRITICAL_STOCK_LEVEL"].GetValue<decimal>(),
                                StockQuantity = reader["STOCK_QUANTITY"].GetValue<decimal>()
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

    }
}
