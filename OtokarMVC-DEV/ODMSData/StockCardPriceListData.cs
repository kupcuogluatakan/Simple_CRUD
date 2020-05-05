using ODMSCommon;
using ODMSCommon.Security;
using ODMSData.DataContracts;
using ODMSModel.StockCardPriceListModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace ODMSData
{
    public class StockCardPriceListData : DataAccessBase, IStockCardPriceList<StockCardPriceListModel>
    {
        public StockCardPriceListModel Select(UserInfo user, StockCardPriceListModel filter)
        {
            var priceList = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("[P_LIST_DEALER_PRICE_LIST_COMBO]");
                db.AddInParameter(cmd, "DealerId", DbType.Int32, MakeDbNull(filter.DealerId));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Selected = true,
                            Value = reader["PriceValue"].GetValue<string>(),
                            Text = reader["PriceText"].GetValue<string>()
                        };
                        priceList.Add(lookupItem);
                    }
                    reader.Close();
                }
                filter.PriceList = priceList;
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

        public StockCardPriceListModel Get(UserInfo user, StockCardPriceListModel filter, CommonValues.StockCardPriceType stockCardPriceType)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_SPARE_PART_PRICE");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(filter.DealerId));
                db.AddInParameter(cmd, "ID_PART", DbType.Int32, MakeDbNull(filter.PartId));
                db.AddInParameter(cmd, "TYPE", DbType.String, MakeDbNull(stockCardPriceType));
                db.AddInParameter(cmd, "ID_VEHICLE", DbType.Int32, MakeDbNull(0)); 
                db.AddInParameter(cmd, "PRICE_LIST_DATE", DbType.Date, MakeDbNull(0));
                db.AddInParameter(cmd, "ID_PRICE_LIST", DbType.Int32, MakeDbNull(0));


                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        switch (stockCardPriceType)
                        {
                            case CommonValues.StockCardPriceType.C:
                                filter.CompanyPrice = reader["VALUE"].GetValue<decimal>();
                                break;
                            case CommonValues.StockCardPriceType.D:
                                filter.CostPrice = reader["VALUE"].GetValue<decimal>();
                                break;
                        }
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

            return filter;
        }

        public StockCardPriceListModel GetListPrice(UserInfo user, StockCardPriceListModel filter, CommonValues.StockCardPriceType stockCardPriceType)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_SPARE_PART_PRICE");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(filter.DealerId));
                db.AddInParameter(cmd, "ID_PART", DbType.Int32, MakeDbNull(filter.PartId));
                db.AddInParameter(cmd, "TYPE", DbType.String, MakeDbNull(stockCardPriceType));
                db.AddInParameter(cmd, "ID_VEHICLE", DbType.Int32, MakeDbNull(0)); 
                db.AddInParameter(cmd, "PRICE_LIST_DATE", DbType.Date, MakeDbNull(0));
                db.AddInParameter(cmd, "ID_PRICE_LIST", DbType.Int32, MakeDbNull(filter.PriceId));

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        filter.ListPrice = reader["VALUE"].GetValue<decimal>();
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

            return filter;
        }

        public StockCardPriceListModel Get(UserInfo user, StockCardPriceListModel filter)
        {
            try 
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_STOCK_CARD_BY_ID");
                db.AddInParameter(cmd, "STOCK_CARD_ID", DbType.Int32, MakeDbNull(filter.StockCardId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        filter.PartId = reader["ID_PART"].GetValue<int>();
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

            return filter;
        }
    }
}
