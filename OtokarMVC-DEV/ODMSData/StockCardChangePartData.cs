using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSModel.StockCardChangePart;

namespace ODMSData
{
    public class StockCardChangePartData : DataAccessBase
    {
        public List<StockCardChangePartListModel> ListStockCardChangePart(StockCardChangePartListModel filter, out int totalCount)
        {
            var listModel = new List<StockCardChangePartListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_STOCK_CARD_CHANGE_PART");
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, filter.PartId);
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
                        var model = new StockCardChangePartListModel
                            {
                                FirstPartCode = dr["FIRST_PART"].GetValue<string>(),
                                LastPartCode = dr["LAST_PART"].GetValue<string>()
                            };

                        listModel.Add(model);
                    }
                    dr.Close();
                }
                totalCount = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<Int32>();
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
