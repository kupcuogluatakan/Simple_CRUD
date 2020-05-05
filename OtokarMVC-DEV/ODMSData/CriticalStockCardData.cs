using ODMSCommon.Security;
using ODMSModel.CriticalStockCard;
using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSModel.ViewModel;

namespace ODMSData
{
    public class CriticalStockCardData : DataAccessBase
    {
        public List<CriticalStockCardListModel> ListCriticalStockCard(UserInfo user,CriticalStockCardListModel filter, out int totalCount)
        {
            var retVal = new List<CriticalStockCardListModel>();
            System.Data.Common.DbDataReader dbDataReader = null;
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_STOCK_CARD_CRITICAL");
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(filter.IdDealer));
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, MakeDbNull(filter.IdPart));
                db.AddInParameter(cmd, "PART_NAME", DbType.String, MakeDbNull(filter.PartName));
                db.AddInParameter(cmd, "PART_CODE", DbType.String, MakeDbNull(filter.PartCode));
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (dbDataReader = cmd.ExecuteReader())
                {
                    while (dbDataReader.Read())
                    {
                        var roleListModel = new CriticalStockCardListModel
                        {
                            CriticalStockQuantity = dbDataReader["CRITICAL_STOCK_QUANTITY"].GetValue<decimal>(),
                            IdDealer = dbDataReader["DEALER_ID"].GetValue<int?>(),
                            DealerName = dbDataReader["DEALER_NAME"].GetValue<string>(),
                            PartCode = dbDataReader["PART_CODE"].GetValue<string>(),
                            IdPart = dbDataReader["PART_ID"].GetValue<int?>(),
                            PartName = dbDataReader["PART_NAME"].GetValue<string>(),
                            StockCardId = dbDataReader["STOCK_CARD_ID"].GetValue<int>(),
                            ShipQty = dbDataReader["SHIP_QUANT"].GetValue<decimal>(),
                            Unit = dbDataReader["UNIT"].GetValue<string>()
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

        public void DMLCriticalStockCard(UserInfo user, CriticalStockCardViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_STOCK_CARD_CRITICAL");
                //db.AddParameter(cmd, "STOCK_CARD_ID", DbType.Int32, ParameterDirection.InputOutput, "STOCK_CARD_ID", DataRowVersion.Default, model.StockCardId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(model.IdDealer));
                db.AddInParameter(cmd, "ID_PART", DbType.Int32, MakeDbNull(model.IdPart));
                db.AddInParameter(cmd, "CRITICAL_STOCK_QUANTITY", DbType.Decimal, MakeDbNull(model.CriticalStockQuantity));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
                else
                {
                    var modelAS = new AutocompleteSearchViewModel
                    {
                        DefaultText = model.PartName,
                        DefaultValue = model.IdPart.ToString()
                    };
                    model.PartSearch = modelAS;
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
        }

        public CriticalStockCardViewModel GetCriticalStockCard(UserInfo user, CriticalStockCardViewModel filter)
        {
            System.Data.Common.DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_STOCK_CARD_CRITICAL");
                db.AddInParameter(cmd, "ID_PART", DbType.Int64, MakeDbNull(filter.IdPart));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(filter.IdDealer));

                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.IdDealer = dReader["ID_DEALER"].GetValue<int>();
                    filter.IdPart = dReader["ID_PART"].GetValue<Int64>();
                    filter.DealerName = dReader["DEALER_NAME"].GetValue<string>();
                    filter.PartName = dReader["PART_NAME"].GetValue<string>();
                    filter.PartCode = dReader["PART_CODE"].GetValue<string>();
                    filter.CriticalStockQuantity = dReader["CRITICAL_STOCK_QUANTITY"].GetValue<decimal>();
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
    }
}
