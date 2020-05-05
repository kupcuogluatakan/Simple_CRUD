using System;
using ODMSCommon.Security;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSModel.Reports;
using System.Data.SqlClient;
using ODMSCommon.Resources;

namespace ODMSData
{
    public class ReportCreateData : DataAccessBase
    {
        public List<ReportCreateModel> GetAllReportCreate(ReportCreateModel filter, out int totalCount)
        {
            var result = new List<ReportCreateModel>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_RPT_REPORT_CREATE");

                db.AddInParameter(cmd, "ID_REPORT", DbType.Int32, MakeDbNull(filter.Id));
                db.AddInParameter(cmd, "IS_COMPLETE", DbType.Boolean, filter.IsComplete);
                db.AddInParameter(cmd, "IS_SUCCESS", DbType.Boolean, filter.IsSuccess);

                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                cmd.CommandTimeout = 1440;
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new ReportCreateModel
                        {
                            Id = reader["ID_REPORT"].GetValue<int>(),
                            FilePath = reader["FILE_PATH"].GetValue<string>(),
                            ReportType = reader["REPORT_TYPE"].GetValue<int>(),
                            Columns = reader["COLUMNS"].GetValue<string>(),
                            ParametersString = reader["PARAMETERS"].GetValue<string>(),
                            IsComplete = reader["IS_COMPLETE"].GetValue<bool>(),
                            IsSuccess = reader["IS_SUCCESS"].GetValue<bool>(),
                            ErrorMessage = reader["ERROR_MESSAGE"].GetValue<string>(),
                            UpdateDate = reader["UPDATE_DATE"].GetValue<DateTime?>(),
                            CreateUser = reader["CREATE_USER"].GetValue<string>(),
                            CreateUserDealerId = reader["CREATE_USER_DEALER_ID"].GetValue<int?>(),
                            CreateUserId = reader["CREATE_USER_ID"].GetValue<int?>(),
                            CreateUserLanguageCode = reader["CREATE_USER_LANGUAGE_CODE"].GetValue<string>()
                        };
                        result.Add(listModel);
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

            return result;
        }

        public void DMLReportCreate(ReportCreateModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_REPORT_CREATE");
                db.AddParameter(cmd, "ID_REPORT", DbType.Int32, ParameterDirection.InputOutput, "ID_REPORT", DataRowVersion.Default, model.Id);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "FILE_PATH", DbType.String, MakeDbNull(model.FilePath));
                db.AddInParameter(cmd, "REPORT_TYPE", DbType.Int16, MakeDbNull(model.ReportType));
                db.AddInParameter(cmd, "COLUMNS", DbType.String, MakeDbNull(model.Columns));
                db.AddInParameter(cmd, "ERROR_MESSAGE", DbType.String, MakeDbNull(model.ErrorMessage));
                db.AddInParameter(cmd, "PARAMETERS", DbType.String, MakeDbNull(model.Parameters));
                db.AddInParameter(cmd, "IS_COMPLETE", DbType.Boolean, MakeDbNull(model.IsComplete));
                db.AddInParameter(cmd, "IS_SUCCESS", DbType.Boolean, MakeDbNull((model.IsSuccess ?? false)));
                db.AddInParameter(cmd, "CREATE_USER", DbType.String, MakeDbNull(model.CreateUser));
                db.AddInParameter(cmd, "CREATE_USER_DEALER_ID", DbType.Int32, MakeDbNull(model.CreateUserDealerId));
                db.AddInParameter(cmd, "CREATE_USER_LANGUAGE_CODE", DbType.String, MakeDbNull(model.CreateUserLanguageCode));
                db.AddInParameter(cmd, "CREATE_USER_ID", DbType.Int32, MakeDbNull(model.CreateUserId));

                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.Id = db.GetParameterValue(cmd, "ID_REPORT").GetValue<int>();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 2)
                    model.ErrorMessage = MessageResource.Bank_Error_NullBankId;
                else if (model.ErrorNo > 0)
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

    }
}
