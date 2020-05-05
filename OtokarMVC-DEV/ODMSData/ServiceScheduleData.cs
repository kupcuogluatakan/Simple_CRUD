using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSModel.ServiceCallSchedule;

namespace ODMSData
{
    public class ServiceScheduleData : DataAccessBase
    {
        public List<ServiceScheduleModel> GetServiceScheduleList(out int totalCnt)
        {
            var listModel = new List<ServiceScheduleModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_SERVICE_SCHEDULE");

                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var model = new ServiceScheduleModel
                            {
                                CallInterval = dr["CALL_INTERVAL_MINUTE"].GetValue<int>(),
                                Desc = dr["SERVICE_DESCRIPTION"].GetValue<string>(),
                                Id = dr["ID_SERVICE"].GetValue<int>(),
                                IsTrigger = dr["TRIGGER_SERVICE"].GetValue<bool>(),
                                IsLogged = dr["IS_RESPONSE_LOGGED"].GetValue<bool>(),
                                IsActive = dr["IS_ACTIVE"].GetValue<bool>(),
                                LastCallKS = dr["LAST_CALL_DATE_KS"].GetValue<DateTime>(),
                                LastSuccessCall = dr["LAST_SUCCESS_CALL_DATE_KS"].GetValue<DateTime>(),
                                Name = dr["SERVICE_NAME"].GetValue<string>(),
                                NextCallKS = dr["NEXT_CALL_DATE_KS"].GetValue<DateTime>()
                            };

                        listModel.Add(model);
                    }
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

            return listModel;
        }
    }
}
