using ODMSModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using ODMSCommon;

namespace ODMSData
{
    public class BusinessLogData : DataAccessBase
    {
        public BusinessLogModel GetBusinessLog(int? logId, string businessName, string name)
        {
            var model = new BusinessLogModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_BUSINESS_LOG");
                db.AddInParameter(cmd, "ID_LOG", DbType.String, MakeDbNull(logId));
                db.AddInParameter(cmd, "NAME", DbType.String, MakeDbNull(name));
                db.AddInParameter(cmd, "BUSINESS_NAME", DbType.String, MakeDbNull(businessName));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.LogId = reader["ID_LOG"].GetValue<int>();
                        model.Name = reader["NAME"].GetValue<string>();
                        model.BusinessName = reader["BUSINESS_NAME"].GetValue<string>();
                        model.Parameters = reader["PARAMETERS"].GetValue<string>();
                        model.StartDate = reader["START_DATE"].GetValue<DateTime>();
                        model.EndDate = reader["END_DATE"].GetValue<DateTime?>();
                        model.IsSuccess = reader["IS_SUCCESS"].GetValue<bool?>();
                        model.ErrorMessage = reader["ERROR_MESSAGE"].GetValue<string>();
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

            return model;
        }

        public void DMLBusinessLog(BusinessLogModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_BUSINESS_LOG");
                db.AddParameter(cmd, "ID_LOG", DbType.Int32, ParameterDirection.InputOutput, "ID_LOG", DataRowVersion.Default, model.LogId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "BUSINESS_NAME", DbType.String, MakeDbNull(model.BusinessName));
                db.AddInParameter(cmd, "NAME", DbType.String, MakeDbNull(model.Name));
                db.AddInParameter(cmd, "PARAMETERS", DbType.String, MakeDbNull(model.Parameters));
                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, MakeDbNull(model.StartDate));
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, MakeDbNull(model.EndDate));
                db.AddInParameter(cmd, "IS_SUCCESS", DbType.Boolean, MakeDbNull(model.IsSuccess));
                db.AddInParameter(cmd, "ERROR_MESSAGE", DbType.String, MakeDbNull(model.ErrorMessage));

                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                cmd.ExecuteNonQuery();

                model.LogId = db.GetParameterValue(cmd, "ID_LOG").GetValue<int>();
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
    }
}
