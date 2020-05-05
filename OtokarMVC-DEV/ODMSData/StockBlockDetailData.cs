using ODMSCommon.Security;
using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSModel.StockBlockDetail;
using System.Data.Common;

namespace ODMSData
{
    public class StockBlockDetailData : DataAccessBase
    {
        public List<StockBlockDetailListModel> ListStockBlockDetail(UserInfo user,StockBlockDetailListModel filter, out int totalCnt)
        {
            var retVal = new List<StockBlockDetailListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_STOCK_BLOCK_DETAIL");
                db.AddInParameter(cmd, "ID_STOCK_BLOCK", DbType.Int64, filter.IdStockBlock);

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
                        var stockBlockListModel = new StockBlockDetailListModel
                            {
                                IdStockBlockDet = reader["ID_STOCK_BLOCK_DET"].GetValue<Int64>(),
                                IdStockBlock = reader["ID_STOCK_BLOCK"].GetValue<Int64>(),
                                IdPart = reader["ID_PART"].GetValue<Int64>(),
                                IdStockType = reader["ID_STOCK_TYPE"].GetValue<Int32>(),
                                PartName = reader["PART_NAME"].GetValue<string>(),
                                PartCode = reader["PART_CODE"].GetValue<string>(),
                                StockBlockStatusId = reader["STOCK_BLOCK_STATUS_ID"].GetValue<int>(),
                                StockBlockStatusName = reader["STOCK_BLOCK_STATUS_NAME"].GetValue<string>(),
                                StockTypeName = reader["MAINT_NAME"].GetValue<string>(),
                                BlockQty = reader["BLCK_QTY"].GetValue<decimal>(),
                                UnBlockQty = reader["UNBLCK_QTY"].GetValue<decimal>(),
                                RemovableQty =
                                    (reader["UNBLCK_QTY"].GetValue<decimal>() - reader["BLCK_QTY"].GetValue<decimal>())                              
                            };
                        stockBlockListModel.BlockReasonDesc = filter.BlockReasonDesc;
                        retVal.Add(stockBlockListModel);
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

        public StockBlockDetailViewModel GetStockBlockDetail(UserInfo user,StockBlockDetailViewModel filter)
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_STOCK_BLOCK_DET");

                db.AddInParameter(cmd, "ID_STOCK_BLOCK", DbType.Int64, MakeDbNull(filter.IdStockBlock));
                db.AddInParameter(cmd, "ID_PART", DbType.Int64, MakeDbNull(filter.IdPart));
                db.AddInParameter(cmd, "ID_STOCK_TYPE", DbType.Int32, MakeDbNull(filter.IdStockType));

                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.IdStockBlockDet = dReader["ID_STOCK_BLOCK_DET"].GetValue<Int64>();

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

        public StockBlockDetailViewModel GetStockBlockDetails(UserInfo user,StockBlockDetailViewModel stockBlockDetailModel)
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_STOCK_BLOCK_DETAIL");

                db.AddInParameter(cmd, "ID_STOCK_BLOCK_DET", DbType.Int64, MakeDbNull(stockBlockDetailModel.IdStockBlockDet));

                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    stockBlockDetailModel.IdStockBlock = dReader["ID_STOCK_BLOCK"].GetValue<Int64>();
                    stockBlockDetailModel.IdPart = dReader["ID_PART"].GetValue<Int64>();
                    stockBlockDetailModel.IdStockType = dReader["ID_STOCK_TYPE"].GetValue<int>();
                    stockBlockDetailModel.PartName = dReader["PART_NAME"].GetValue<string>();
                    stockBlockDetailModel.PartCode = dReader["PART_CODE"].GetValue<string>();
                    stockBlockDetailModel.BlockQty = dReader["BLCK_QTY"].GetValue<decimal>();
                    stockBlockDetailModel.UnBlockQty = dReader["UNBLCK_QTY"].GetValue<decimal>();
                    stockBlockDetailModel.StockTypeName = dReader["MAINT_NAME"].GetValue<string>();
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


            return stockBlockDetailModel;
        }

        public List<StockBlockDetailViewModel> GetStockBlockDetailList(UserInfo user,StockBlockDetailViewModel filter)
        {
            var retVal = new List<StockBlockDetailViewModel>();

            //DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_STOCK_BLOCK_DETAIL_LIST");

                db.AddInParameter(cmd, "ID_STOCK_BLOCK", DbType.Int64, MakeDbNull(filter.IdStockBlock));

                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var returnModel = new StockBlockDetailViewModel()
                        {
                            IdStockBlockDet = reader["ID_STOCK_BLOCK_DET"].GetValue<Int64>(),
                            BlockQty = reader["BLCK_QTY"].GetValue<decimal>(),
                            UnBlockQty = reader["UNBLCK_QTY"].GetValue<decimal>(),
                            IdPart = reader["ID_PART"].GetValue<Int64>(),
                            IdStockType = reader["ID_STOCK_TYPE"].GetValue<int>(),
                            BlockReasonDesc = reader["BLOCK_REASON_DESC"].GetValue<string>(),
                            IdDealer = filter.IdDealer,
                            IdStockBlock = filter.IdStockBlock
                        };

                        retVal.Add(returnModel);
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

        public void DMLStockBlockDetail(UserInfo user,StockBlockDetailViewModel stockBlockDetailModel)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_STOCK_BLOCK_DET");
                db.AddInParameter(cmd, "ID_STOCK_BLOCK", DbType.Int64, stockBlockDetailModel.IdStockBlock);//Update
                db.AddInParameter(cmd, "ID_STOCK_BLOCK_DET", DbType.Int64, stockBlockDetailModel.IdStockBlockDet);

                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, stockBlockDetailModel.IdDealer);
                db.AddInParameter(cmd, "ID_PART", DbType.Int64, stockBlockDetailModel.IdPart);
                db.AddInParameter(cmd, "ID_STOCK_TYPE", DbType.Int32, stockBlockDetailModel.IdStockType);
                db.AddInParameter(cmd, "BLOCK_QTY", DbType.Decimal, stockBlockDetailModel.BlockQty);
                db.AddInParameter(cmd, "UNBLOCK_QTY", DbType.Decimal, stockBlockDetailModel.UnBlockQty);

                db.AddInParameter(cmd, "REMOVE_BLOCK_QTY", DbType.Decimal, stockBlockDetailModel.RemovedBlockQty);
                db.AddInParameter(cmd, "BLOCK_REASON_DESC", DbType.String, stockBlockDetailModel.BlockReasonDesc);

                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, stockBlockDetailModel.CommandType);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddInParameter(cmd, "OPERATING_DATE", DbType.Date, MakeDbNull(DateTime.Now));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                stockBlockDetailModel.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                stockBlockDetailModel.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (stockBlockDetailModel.ErrorNo > 0)
                    stockBlockDetailModel.ErrorMessage = ResolveDatabaseErrorXml(stockBlockDetailModel.ErrorMessage);
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

        public StockBlockDetailViewModel DMLStockBlockDetailList(UserInfo user,List<StockBlockDetailViewModel> stockBlockListModel, StockBlockDetailViewModel errorModel)
        {
            string errorMessage = string.Empty;
            bool isSuccess = true;
            CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("P_DML_STOCK_BLOCK_DET");
            CreateConnection(cmd);
            DbTransaction transaction = connection.BeginTransaction();
            try
            {
                foreach (StockBlockDetailViewModel stockBlockDetailModel in stockBlockListModel)
                {
                    stockBlockDetailModel.CommandType = CommonValues.BlockType.Block;
                    cmd.Parameters.Clear();
                    db.AddInParameter(cmd, "ID_STOCK_BLOCK", DbType.Int64, stockBlockDetailModel.IdStockBlock);//Update
                    db.AddInParameter(cmd, "ID_STOCK_BLOCK_DET", DbType.Int64, stockBlockDetailModel.IdStockBlockDet);

                    db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, stockBlockDetailModel.IdDealer);
                    db.AddInParameter(cmd, "ID_PART", DbType.Int64, stockBlockDetailModel.IdPart);
                    db.AddInParameter(cmd, "ID_STOCK_TYPE", DbType.Int32, stockBlockDetailModel.IdStockType);
                    db.AddInParameter(cmd, "BLOCK_QTY", DbType.Decimal, stockBlockDetailModel.BlockQty);
                    db.AddInParameter(cmd, "UNBLOCK_QTY", DbType.Decimal, stockBlockDetailModel.UnBlockQty);

                    db.AddInParameter(cmd, "REMOVE_BLOCK_QTY", DbType.Decimal, stockBlockDetailModel.RemovedBlockQty);
                    db.AddInParameter(cmd, "BLOCK_REASON_DESC", DbType.String, stockBlockDetailModel.BlockReasonDesc);

                    db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, stockBlockDetailModel.CommandType);
                    db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                    db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                    db.AddInParameter(cmd, "OPERATING_DATE", DbType.Date, MakeDbNull(DateTime.Now));
                    db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                    db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                    //CreateConnection(cmd);
                    db.ExecuteNonQuery(cmd, transaction);
                    //cmd.ExecuteNonQuery();
                    stockBlockDetailModel.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                    stockBlockDetailModel.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                    if (stockBlockDetailModel.ErrorNo == 2)
                    {
                        errorMessage += errorMessage.Length == 0
                                           ? ODMSCommon.Resources.MessageResource.StockBlockDetail_Error_BlockQuantity +
                                             "..: "
                                           : ",";
                        errorMessage += stockBlockDetailModel.ErrorMessage;
                        isSuccess = false;
                    }
                    else if (stockBlockDetailModel.ErrorNo > 0)
                    {
                        errorMessage = ResolveDatabaseErrorXml(stockBlockDetailModel.ErrorMessage);
                        isSuccess = false;
                    }
                }
            }
            catch (Exception Ex)
            {
                string dbErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>();
                isSuccess = false;
                errorMessage = string.IsNullOrEmpty(dbErrorMessage) ? Ex.Message : ResolveDatabaseErrorXml(dbErrorMessage);
            }
            finally
            {
                if (isSuccess)
                {
                    transaction.Commit();
                }
                else
                {
                    errorModel.ErrorNo = 1;
                    errorModel.ErrorMessage = errorMessage;
                    transaction.Rollback();
                }
                CloseConnection();
            }

            return errorModel;
        }
    }
}
