using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.Scrap;
using ODMSCommon.Resources;

namespace ODMSData
{
    public class ScrapData : DataAccessBase
    {
        public List<ScrapListModel> ListScraps(UserInfo user,ScrapListModel filter, out int total)
        {
            var retVal = new List<ScrapListModel>();
            total = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SCRAP");
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(filter.ScrapDealerId));
                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, MakeDbNull(filter.StartDate));
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, MakeDbNull(filter.EndDate));
                db.AddInParameter(cmd, "PART_CODE", DbType.String, MakeDbNull(filter.PartCode));
                db.AddInParameter(cmd, "PART_NAME", DbType.String, MakeDbNull(filter.PartName));
                db.AddInParameter(cmd, "STOCK_TYPE_ID", DbType.Int32, MakeDbNull(filter.ScrapStockTypeId));
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
                        var supplier = new ScrapListModel
                        {
                            ScrapId = reader["SCRAP_ID"].GetValue<int>(),
                            ScrapDealerId = reader["DEALER_ID"].GetValue<int>(),
                            ScrapDealerName = reader["DEALER_NAME"].GetValue<string>(),
                            ScrapDate = reader["SCRAP_DATE"].GetValue<DateTime>(),
                            DocId = reader["DOC_ID"].GetValue<int>(),
                            DocName = reader["DOC_NAME"].GetValue<string>(),
                            ScrapReasonId = reader["SCRAP_REASON_ID"].GetValue<int>(),
                            ScrapReasonName = reader["SCRAP_REASON_NAME"].GetValue<string>(),
                            ScrapReasonDesc = reader["SCRAP_REASON_DESC"].GetValue<string>()
                        };

                        retVal.Add(supplier);
                    }
                    reader.Close();
                }
                total = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
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

        public void DMLScrap(UserInfo user,ScrapViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_SCRAP");
                db.AddParameter(cmd, "SCRAP_ID", DbType.Int32, ParameterDirection.InputOutput, "SCRAP_ID", DataRowVersion.Default, model.ScrapId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(model.DealerId));
                db.AddInParameter(cmd, "SCRAP_DATE", DbType.DateTime, MakeDbNull(model.ScrapDate));
                db.AddInParameter(cmd, "DOC_ID", DbType.Int32, MakeDbNull(model.DocId));
                db.AddInParameter(cmd, "SCRAP_REASON_ID", DbType.Int32, model.ScrapReasonId);
                db.AddInParameter(cmd, "SCRAP_REASON_DESC", DbType.String, MakeDbNull(model.ScrapReasonDesc));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ScrapId = db.GetParameterValue(cmd, "SCRAP_ID").GetValue<int>();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
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

        public void GetScrap(UserInfo user,ScrapViewModel model)
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_SCRAP");
                db.AddInParameter(cmd, "SCRAP_ID", DbType.Int32, MakeDbNull(model.ScrapId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    model.DealerId = dReader["DEALER_ID"].GetValue<int>();
                    model.DealerName = dReader["DEALER_NAME"].GetValue<string>();
                    model.ScrapDate = dReader["SCRAP_DATE"].GetValue<DateTime>();
                    model.DocId = dReader["DOC_ID"].GetValue<int>();
                    model.DocName = dReader["DOC_NAME"].GetValue<string>();
                    model.ScrapReasonId = dReader["SCRAP_REASON_ID"].GetValue<int?>();
                    model.ScrapReasonName = dReader["SCRAP_REASON_NAME"].GetValue<string>();
                    model.ScrapReasonDesc = dReader["SCRAP_REASON_DESC"].GetValue<string>();
                    model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                    model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>();
                    if (model.ErrorNo == 2)
                        model.ErrorMessage = MessageResource.Scrap_Error_NullId;
                    else if (model.ErrorNo == 1)
                        model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);

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
        }

    }
}
