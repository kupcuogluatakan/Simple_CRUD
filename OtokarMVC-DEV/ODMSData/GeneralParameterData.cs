using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSModel.GeneralParameter;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class GeneralParameterData : DataAccessBase
    {
        public List<GeneralParameterListModel> ListGeneralParameters(GeneralParameterListModel filter, out int totalCount)
        {
            var result = new List<GeneralParameterListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_GENERAL_PARAMETER");
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
                        var listModel = new GeneralParameterListModel
                        {
                            ParameterId = reader["PARAMETER_ID"].GetValue<string>(),
                            Description = reader["DESCRIPTION"].GetValue<string>(),
                            Type = reader["TYPE"].GetValue<string>(),
                            Value = reader["VALUE"].GetValue<string>(),
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

        public GeneralParameterViewModel GetGeneralParameter(string id)
        {
            var result = new GeneralParameterViewModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_GENERAL_PARAMETER");
                db.AddInParameter(cmd, "PARAMETER_ID", DbType.String, MakeDbNull(id));

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        result.ParameterId = id;
                        result.Description = reader["DESCRIPTION"].GetValue<string>();
                        result.Type = reader["TYPE"].GetValue<string>();
                        result.Value = reader["VALUE"].GetValue<string>();
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
            return result;
        }

        //TODO : Id set edilmeli
        public void DMLGeneralParameter(UserInfo user, GeneralParameterViewModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_DML_GENERAL_PARAMETER");
                db.AddInParameter(cmd, "PARAMETER_ID", DbType.String, MakeDbNull(model.ParameterId));
                db.AddInParameter(cmd, "VALUE", DbType.String, MakeDbNull(model.Value));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 400);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));

                CreateConnection(cmd);

                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
                cmd.Dispose();
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
