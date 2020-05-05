using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.ScrapDetail;
using ODMSCommon.Resources;

namespace ODMSData
{
    public class ScrapDetailData : DataAccessBase
    {
        public List<ScrapDetailListModel> ListScrapDetails(UserInfo user,ScrapDetailListModel filter, out int totalCnt)
        {
            var retVal = new List<ScrapDetailListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SCRAP_DETAIL");
                db.AddInParameter(cmd, "SCRAP_ID", DbType.Int32, MakeDbNull(filter.ScrapId));
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
                        var scrapDetail = new ScrapDetailListModel
                            {
                                ScrapDetailId = reader["SCRAP_DETAIL_ID"].GetValue<int>(),
                                ScrapId = reader["SCRAP_ID"].GetValue<int>(),
                                PartId = reader["PART_ID"].GetValue<int>(),
                                PartCode = reader["PART_CODE"].GetValue<string>(),
                                PartName = reader["PART_NAME"].GetValue<string>(),
                                Barcode = reader["BARCODE"].GetValue<string>(),
                                StockTypeId = reader["STOCK_TYPE_ID"].GetValue<int>(),
                                StockTypeName = reader["STOCK_TYPE_NAME"].GetValue<string>(),
                                WarehouseId = reader["WAREHOUSE_ID"].GetValue<int>(),
                                WarehouseName = reader["WAREHOUSE_NAME"].GetValue<string>(),
                                RackId = reader["RACK_ID"].GetValue<int>(),
                                RackName = reader["RACK_NAME"].GetValue<string>(),
                                StockQuantity = reader["STOCK_QUANTITY"].GetValue<decimal>(),
                                Quantity = reader["QUANTITY"].GetValue<string>(),
                                ConfirmIdStockTransaction = reader["CONFIRM_ID_STOCK_TRANSACTION"].GetValue<int>(),
                                CancelIdStockTransaction = reader["CANCEL_ID_STOCK_TRANSACTION"].GetValue<int>(),
                                ConfirmUserId = reader["CONFIRM_USER_ID"].GetValue<int>(),
                                ConfirmUserName = reader["CONFIRM_USER_NAME"].GetValue<string>(),
                                CancelUserId = reader["CANCEL_USER_ID"].GetValue<int>(),
                                CancelUserName = reader["CANCEL_USER_NAME"].GetValue<string>(),
                                Unit = reader["UNIT"].GetValue<string>()
                            };

                        retVal.Add(scrapDetail);
                    }
                    reader.Close();
                }
                totalCnt = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
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

        public List<ScrapDetailListModel> ListScrapDetailsPartByBarcode(UserInfo user,ScrapDetailListModel filter, out int totalCnt)
        {
            var retVal = new List<ScrapDetailListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SCRAP_DETAIL_PART_BY_BARCODE");
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(filter.DealerId));
                db.AddInParameter(cmd, "BARCODE", DbType.String, MakeDbNull(filter.Barcode));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, DBNull.Value);
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                int count = 1;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var scrapDetail = new ScrapDetailListModel
                        {
                            ScrapDetailId = count,
                            IsNew = true,
                            PartId = reader["PART_ID"].GetValue<int?>(),
                            PartCode = reader["PART_CODE"].GetValue<string>(),
                            PartName = reader["PART_NAME"].GetValue<string>(),
                            StockTypeId = reader["STOCK_TYPE_ID"].GetValue<int>(),
                            StockTypeName = reader["STOCK_TYPE_NAME"].GetValue<string>(),
                            WarehouseId = reader["WAREHOUSE_ID"].GetValue<int>(),
                            WarehouseName = reader["WAREHOUSE_NAME"].GetValue<string>(),
                            RackId = reader["RACK_ID"].GetValue<int>(),
                            RackName = reader["RACK_NAME"].GetValue<string>(),
                            StockQuantity = reader["STOCK_QUANTITY"].GetValue<decimal>(),
                            Quantity = reader["RACK_QUANTITY"].GetValue<string>(),
                            Unit = reader["UNIT"].GetValue<string>()
                        };
                        count++;
                        retVal.Add(scrapDetail);
                    }
                    reader.Close();
                }
                totalCnt = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
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

        public List<ScrapDetailListModel> ListScrapDetailsPart(UserInfo user,ScrapDetailListModel filter, out int totalCnt)
        {
            var retVal = new List<ScrapDetailListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SCRAP_DETAIL_PART");
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, MakeDbNull(filter.PartId));
                db.AddInParameter(cmd, "STOCK_TYPE_ID", DbType.Int32, MakeDbNull(filter.StockTypeId));
                db.AddInParameter(cmd, "WAREHOUSE_ID", DbType.Int32, MakeDbNull(filter.WarehouseId));
                db.AddInParameter(cmd, "RACK_ID", DbType.Int32, MakeDbNull(filter.RackId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                int count = 1;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var scrapDetail = new ScrapDetailListModel
                        {
                            ScrapDetailId = count,
                            IsNew = true,
                            PartId = reader["PART_ID"].GetValue<int?>(),
                            PartCode = reader["PART_CODE"].GetValue<string>(),
                            PartName = reader["PART_NAME"].GetValue<string>(),
                            StockTypeId = reader["STOCK_TYPE_ID"].GetValue<int>(),
                            StockTypeName = reader["STOCK_TYPE_NAME"].GetValue<string>(),
                            WarehouseId = reader["WAREHOUSE_ID"].GetValue<int>(),
                            WarehouseName = reader["WAREHOUSE_NAME"].GetValue<string>(),
                            RackId = reader["RACK_ID"].GetValue<int>(),
                            RackName = reader["RACK_NAME"].GetValue<string>(),
                            StockQuantity = reader["STOCK_QUANTITY"].GetValue<decimal>(),
                            Quantity = reader["RACK_QUANTITY"].GetValue<string>(),
                            Unit = reader["UNIT"].GetValue<string>()
                        };
                        count ++;
                        retVal.Add(scrapDetail);
                    }
                    reader.Close();
                }
                totalCnt = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
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

        public void DMLScrapDetail(UserInfo user,ScrapDetailViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_SCRAP_DETAIL");
                db.AddParameter(cmd, "SCRAP_DETAIL_ID", DbType.Int32, ParameterDirection.InputOutput, "SCRAP_DETAIL_ID",
                                DataRowVersion.Default, model.ScrapDetailId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "SCRAP_ID", DbType.Int32, MakeDbNull(model.ScrapId));
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, MakeDbNull(model.PartId));
                db.AddInParameter(cmd, "STOCK_TYPE_ID", DbType.Int32, MakeDbNull(model.StockTypeId));
                db.AddInParameter(cmd, "WAREHOUSE_ID", DbType.Int32, MakeDbNull(model.WarehouseId));
                db.AddInParameter(cmd, "RACK_ID", DbType.Int32, MakeDbNull(model.RackId));
                db.AddInParameter(cmd, "QUANTITY", DbType.Decimal, MakeDbNull(model.Quantity));
                db.AddInParameter(cmd, "CONFIRM_USER", DbType.Int32, MakeDbNull(model.ConfirmUserId));
                db.AddInParameter(cmd, "CANCEL_USER", DbType.Int32, MakeDbNull(model.CancelUserId));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ScrapDetailId = db.GetParameterValue(cmd, "SCRAP_DETAIL_ID").GetValue<int>();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 2)
                    model.ErrorMessage = MessageResource.ScrapDetail_Error_NullId;
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

        public ScrapDetailViewModel GetScrapDetail(UserInfo user,int scrapDetailId)
        {
            DbDataReader dReader = null;
            var scrap = new ScrapDetailViewModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_SCRAP_DETAIL");
                db.AddInParameter(cmd, "SCRAP_DETAIL_ID", DbType.Int32, MakeDbNull(scrapDetailId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    scrap.ScrapDetailId = dReader["SCRAP_DETAIL_ID"].GetValue<int>();
                    scrap.ScrapId = dReader["SCRAP_ID"].GetValue<int>();
                    scrap.PartId = dReader["PART_ID"].GetValue<int>();
                    scrap.PartCode = dReader["PART_CODE"].GetValue<string>();
                    scrap.PartName = dReader["PART_NAME"].GetValue<string>();
                    scrap.StockTypeId = dReader["STOCK_TYPE_ID"].GetValue<int>();
                    scrap.StockTypeName = dReader["STOCK_TYPE_NAME"].GetValue<string>();
                    scrap.WarehouseId = dReader["WAREHOUSE_ID"].GetValue<int>();
                    scrap.WarehouseName = dReader["WAREHOUSE_NAME"].GetValue<string>();
                    scrap.RackId = dReader["RACK_ID"].GetValue<int>();
                    scrap.RackName = dReader["RACK_NAME"].GetValue<string>();
                    scrap.Quantity = dReader["QUANTITY"].GetValue<decimal>();
                    scrap.ConfirmIdStockTransaction = dReader["CONFIRM_ID_STOCK_TRANSACTION"].GetValue<int>();
                    scrap.CancelIdStockTransaction = dReader["CANCEL_ID_STOCK_TRANSACTION"].GetValue<int>();
                    scrap.ConfirmUserId = dReader["CONFIRM_USER_ID"].GetValue<int>();
                    scrap.ConfirmUserName = dReader["CONFIRM_USER_NAME"].GetValue<string>();
                    scrap.CancelUserId = dReader["CANCEL_USER_ID"].GetValue<int>();
                    scrap.CancelUserName = dReader["CANCEL_USER_NAME"].GetValue<string>();
                    scrap.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();

                    if (scrap.ErrorNo > 0)
                    {
                        scrap.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                        scrap.ErrorMessage = ResolveDatabaseErrorXml(scrap.ErrorMessage);
                    }
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
            return scrap;
        }

    }
}
