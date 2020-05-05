using ODMSCommon.Security;
using ODMSModel.StockCardYearly;
using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;

namespace ODMSData
{
    public class StockCardYearlyData : DataAccessBase
    {
        public List<StockCardYearlyListModel> ListStockCardYearly(UserInfo user, StockCardYearlyListModel filter)
        {
            var retVal = new List<StockCardYearlyListModel>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_STOCK_CARD_YEARLY");
                db.AddInParameter(cmd, "ID_STOCK_TYPE", DbType.Int32, filter.IdStockType);
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, filter.IdDealer);
                db.AddInParameter(cmd, "ID_PART", DbType.Int64, filter.IdPart);
                db.AddInParameter(cmd, "DATE", DbType.DateTime, filter.Date);

                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var stockCardYearlyListModel = new StockCardYearlyListModel
                        {
                            Month = reader["DATE_MONTH"].GetValue<int>(),
                            Year = reader["DATE_YEAR"].GetValue<int>(),
                            Quantity = reader["QUANTITY"].GetValue<decimal>(),
                            IdDealer = filter.IdDealer
                        };

                        retVal.Add(stockCardYearlyListModel);
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


        public void StartUpdateMonthlyStock(UserInfo user, StockCardYearlyListModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_UPDATE_MONTHLY_STOCK");
                db.AddInParameter(cmd, "ID_STOCK_TYPE", DbType.Int32, model.IdStockType);
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, model.IdDealer);
                db.AddInParameter(cmd, "ID_PART", DbType.Int64, model.IdPart);
                db.AddInParameter(cmd, "CURR_DATE", DbType.DateTime, DateTime.Now);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.String, MakeDbNull(user.UserId));

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                CreateConnection(cmd);

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
