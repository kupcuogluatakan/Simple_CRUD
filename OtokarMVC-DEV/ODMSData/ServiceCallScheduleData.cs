using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData.DataContracts;
using ODMSModel.ServiceCallSchedule;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class ServiceCallScheduleData : DataAccessBase
    {
        public List<ServiceCallScheduleListModel> List(UserInfo user, ServiceCallScheduleListModel filter, out int totalCnt)
        {
            var result = new List<ServiceCallScheduleListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SERVICE_CALL_SCHEDULE");
                db.AddInParameter(cmd, "ID_SERVICE", DbType.Int32, MakeDbNull(filter.ServiceId));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, true);
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
                        var listModel = new ServiceCallScheduleListModel
                        {
                            ServiceId = reader["ID_SERVICE"].GetValue<int>(),
                            ServiceDescription = reader["SERVICE_DESCRIPTION"].GetValue<string>(),
                            CallIntervalMinute = string.Format("{0:0,0} {1} {2}", reader["CALL_INTERVAL_MINUTE"].GetValue<double>(), string.Empty, MessageResource.ServiceCallScheduleViewModel_Display_Minute),
                            LastCallDate = reader["LAST_CALL_DATE_KS"].GetValue<DateTime?>().HasValue ? reader["LAST_CALL_DATE_KS"].GetValue<DateTime>().ToString("dd.MM.yyyy HH:mm") : string.Empty,
                            NextCallDate = reader["NEXT_CALL_DATE_KS"].GetValue<DateTime>().ToString("dd.MM.yyyy HH:mm"),
                            IsTriggerService = reader["TRIGGER_SERVICE"].GetValue<bool>(),
                            TriggerServiceName = reader["TRIGGER_SERVICE_NAME"].GetValue<string>(),
                            IsResponseLogged = reader["IS_RESPONSE_LOGGED"].GetValue<bool>(),
                            IsResponseLoggedString = reader["IS_RESPONSE_LOGGED_STRING"].GetValue<string>()
                        };
                        result.Add(listModel);
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

            return result;
        }

        public void Update(ServiceCallScheduleViewModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_DML_SERVICE_CALL_SCHEDULE");
                db.AddInParameter(cmd, "ID_SERVICE", DbType.Int32, MakeDbNull(model.ServiceId));
                db.AddInParameter(cmd, "NEXT_CALL_DATE", DbType.DateTime, MakeDbNull(model.NextCallDate));
                db.AddInParameter(cmd, "LAST_SUCCESS_CALL_DATE", DbType.DateTime, MakeDbNull(model.LastSuccessCallDate));
                db.AddInParameter(cmd, "LAST_CALL_DATE_KS", DbType.DateTime, MakeDbNull(model.LastCallDate));
                db.AddInParameter(cmd, "SERVICE_DESCRIPTION", DbType.String, MakeDbNull(model.ServiceDescription));
                db.AddInParameter(cmd, "CALL_INTERVAL_MINUTE", DbType.String, MakeDbNull(model.CallIntervalMinute));
                db.AddInParameter(cmd, "TRIGGER_SERVICE", DbType.Boolean, MakeDbNull(model.IsTriggerService));
                db.AddInParameter(cmd, "IS_RESPONSE_LOGGED", DbType.Boolean, MakeDbNull(model.IsResponseLogged));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

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


        public void Start(int serviceId, DateTime startDate)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_DML_SERVICE_CALL_SCHEDULE");
                db.AddInParameter(cmd, "ID_SERVICE", DbType.Int32, MakeDbNull(serviceId));
                db.AddInParameter(cmd, "LAST_CALL_DATE_KS", DbType.DateTime, startDate);

                db.AddInParameter(cmd, "NEXT_CALL_DATE", DbType.DateTime, MakeDbNull(null));
                db.AddInParameter(cmd, "LAST_SUCCESS_CALL_DATE", DbType.DateTime, MakeDbNull(null));
                db.AddInParameter(cmd, "SERVICE_DESCRIPTION", DbType.String, MakeDbNull(null));
                db.AddInParameter(cmd, "CALL_INTERVAL_MINUTE", DbType.String, MakeDbNull(null));
                db.AddInParameter(cmd, "TRIGGER_SERVICE", DbType.Boolean, MakeDbNull(null));
                db.AddInParameter(cmd, "IS_RESPONSE_LOGGED", DbType.Boolean, MakeDbNull(null));

                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, "S");
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
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

        public ServiceCallScheduleViewModel Get(ServiceCallScheduleViewModel model)
        {
            System.Data.Common.DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_SERVICE_CALL_SCHEDULE");
                db.AddInParameter(cmd, "ID_SERVICE", DbType.Int32, MakeDbNull(model.ServiceId));
                db.AddInParameter(cmd, "SERVICE_NAME", DbType.String, MakeDbNull(model.ServiceDescription));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    model.ServiceId = dReader["ID_SERVICE"].GetValue<int>();
                    model.ServiceDescription = dReader["SERVICE_DESCRIPTION"].GetValue<string>();
                    model.TriggerServiceName = dReader["SERVICE_NAME"].GetValue<string>();
                    model.CallIntervalMinute = dReader["CALL_INTERVAL_MINUTE"].GetValue<decimal>();
                    model.LastCallDate = dReader["LAST_CALL_DATE_KS"].GetValue<DateTime?>().HasValue ? dReader["LAST_CALL_DATE_KS"].GetValue<DateTime>().ToString("yyyy-MM-dd HH:mm:ss.fff") : string.Empty;
                    model.NextCallDate = dReader["NEXT_CALL_DATE_KS"].GetValue<DateTime>();
                    model.LastSuccessCallDate = dReader["LAST_SUCCESS_CALL_DATE_KS"].GetValue<DateTime?>();
                    model.IsTriggerService = dReader["TRIGGER_SERVICE"].GetValue<bool>();
                    model.IsResponseLogged = dReader["IS_RESPONSE_LOGGED"].GetValue<bool>();
                    model.IsActive = dReader["IS_ACTIVE"].GetValue<bool>();
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


            return model;
        }

        public string GetTimeOnly(string p)
        {
            string rValue = string.Empty;
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_CALL_SCHEDULE_TIME_ONLY");
                db.AddInParameter(cmd, "SERVICE_NAME", DbType.String, p);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        rValue = dr["LAST_CALL_DATE_KS"].GetValue<DateTime>().ToString("yyyyMMdd");
                    }
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

            return rValue;
        }

        public void LogService(ServiceCallLogModel model)
        {
            try
            {
                SqlXml sqlXmlLog = model.LogXml != null ? new SqlXml(model.LogXml.CreateReader()) : null;
                SqlXml sqlXmlErrorLog = model.LogErrorXml != null ? new SqlXml(model.LogErrorXml.CreateReader()) : null;

                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LOG_SERVICE_CALL");
                db.AddParameter(cmd, "LOG_ID", DbType.Int64, ParameterDirection.InputOutput, "LOG_ID", DataRowVersion.Default, model.LogId);
                db.AddInParameter(cmd, "XML_LOG", DbType.Xml, sqlXmlLog);
                db.AddInParameter(cmd, "XML_ERROR", DbType.Xml, sqlXmlErrorLog);
                db.AddInParameter(cmd, "SERVICE_NAME", DbType.String, model.ServiceName);
                db.AddInParameter(cmd, "LOG_TYPE", DbType.String, model.LogType);
                db.AddInParameter(cmd, "LOG_ERROR_DESC", DbType.String, string.Format("{0} {1}", model.LogErrorDesc, model.ErrorMessage));
                db.AddInParameter(cmd, "IS_MANUEL", DbType.Boolean, model.IsManuel);
                db.AddInParameter(cmd, "USER_ID", DbType.Int32, 1);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>();
                }
                else
                {
                    model.LogId = db.GetParameterValue(cmd, "LOG_ID").GetValue<Int64>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                model.LogXml = null;
                CloseConnection();
            }
        }

        public void RefreshCallSchedule(ServiceScheduleModel mainModel)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_SERVICE_SCHEDULE_TEST");
                db.AddInParameter(cmd, "SERVICE_NAME", DbType.String, MakeDbNull(mainModel.Name));
                db.AddInParameter(cmd, "LAST_CALL_KS", DbType.DateTime, mainModel.LastCallKS);
                db.AddInParameter(cmd, "LAST_SUCCESS_CALL", DbType.DateTime, mainModel.LastSuccessCall);
                db.AddParameter(cmd, "NEXT_CALL_KS", DbType.DateTime, ParameterDirection.InputOutput, "NEXT_CALL_KS", DataRowVersion.Default, mainModel.NextCallKS);
                db.AddOutParameter(cmd, "CALL_INTERVAL", DbType.String, 200);
                db.AddOutParameter(cmd, "TRIGGER_SERVICE", DbType.Boolean, 1);
                db.AddOutParameter(cmd, "IS_RESPONSE_LOGGED", DbType.Boolean, 1);
                db.AddOutParameter(cmd, "IS_ACTIVE", DbType.Boolean, 1);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                mainModel.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                if (mainModel.ErrorNo > 0)
                {
                    mainModel.ErrorDesc = db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>();
                }
                else
                {
                    mainModel.IsLogged = db.GetParameterValue(cmd, "IS_RESPONSE_LOGGED").GetValue<Boolean>();
                    mainModel.IsTrigger = db.GetParameterValue(cmd, "TRIGGER_SERVICE").GetValue<Boolean>();
                    mainModel.CallInterval = db.GetParameterValue(cmd, "CALL_INTERVAL").GetValue<int>();
                    mainModel.IsActive = db.GetParameterValue(cmd, "IS_ACTIVE").GetValue<Boolean>();
                    mainModel.NextCallKS = db.GetParameterValue(cmd, "NEXT_CALL_KS").GetValue<DateTime>();
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
    }
}
