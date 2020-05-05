using ODMSCommon.Resources;
using ODMSData.DataContracts;
using ODMSModel.WebServiceErrorDisplay;
using System;
using System.Data.Common;
using ODMSCommon;
using System.Data;

namespace ODMSData
{
    public class WebServiceErrorDisplayData : DataAccessBase, IWebServiceErrorDisplay<WebServiceErrorDisplayViewModel>
    {
        public WebServiceErrorDisplayViewModel Get(WebServiceErrorDisplayViewModel filter)
        {
            DbDataReader reader = null;
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_GET_WEB_SERVICE_ERROR_DISPLAY");
                db.AddInParameter(cmd, "ID_LOG", DbType.Int32, MakeDbNull(filter.ServiceLogId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);

                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    filter.ServiceLogId = reader["ID_LOG"].GetValue<int>();
                    filter.ServiceName = reader["SERVICE_NAME"].GetValue<string>();
                    filter.IsManuelCall = reader["IS_MANUEL_CALL"].GetValue<bool>();
                    filter.CallStartDate = reader["CALL_START_DATE"].GetValue<DateTime>();
                    filter.CallFinishDate = reader["CALL_FINISH_DATE"].GetValue<DateTime>();
                    filter.ManuelCallUser = reader["MANUEL_CALL_USER"].GetValue<string>();
                }
                else
                {
                    filter.ErrorNo = 1;
                    filter.ErrorMessage = MessageResource.Error_DB_NoRecordFound;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                    reader.Dispose();
                }
                CloseConnection();
            }
            return filter;

        }

        public WebServiceErrorDisplayViewModel GetDetail(WebServiceErrorDisplayViewModel filter, XmlErrorType type)
        {
            DbDataReader reader = null;
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_GET_WEB_SERVICE_ERROR_DISPLAY_DETAIL");
                db.AddInParameter(cmd, "ID_LOG", DbType.Int32, MakeDbNull(filter.ServiceLogId));
                db.AddInParameter(cmd, "TYPE", DbType.Int32, (int)type);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);

                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    filter.Value = reader["VALUE"].GetValue<string>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                    reader.Dispose();
                }
                CloseConnection();
            }
            return filter;
        }
    }
}
