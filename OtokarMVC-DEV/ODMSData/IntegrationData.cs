using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.Integration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSData
{
    public class IntegrationData : DataAccessBase
    {
        public List<IntegrationListModel> GetIntegrationList(UserInfo user,IntegrationListModel filter)
        {
            var list = new List<IntegrationListModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SERVICE_CALL_SCHEDULE");
                AddPagingParametersWithLanguage(user, cmd, filter);

                db.AddInParameter(cmd, "ID_SERVICE", DbType.Int32, MakeDbNull(filter.IntegrationTypeId));
                if (!filter.IsActive.HasValue)
                    db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, null);
                else
                    db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, (filter.IsActive.Value == true ? 1 : 0));

                CreateConnection(cmd);

                using (var dReader = cmd.ExecuteReader())
                {
                    while (dReader.Read())
                    {
                        var model = new IntegrationListModel();
                        model.ServiceId = dReader["ID_SERVICE"].GetValue<int>();
                        model.ServiceName = dReader["SERVICE_NAME"].GetValue<string>();
                        model.ServiceDescription = dReader["SERVICE_DESCRIPTION"].GetValue<string>();
                        model.LastCallDate = dReader["LAST_CALL_DATE_KS"] == DBNull.Value ? null : dReader["LAST_CALL_DATE_KS"].GetValue<DateTime?>();
                        model.LastSuccessDate = dReader["LAST_SUCCESS_CALL_DATE_KS"] == DBNull.Value ? null : dReader["LAST_SUCCESS_CALL_DATE_KS"].GetValue<DateTime?>();
                        list.Add(model);
                    }
                    dReader.Close();
                }
                filter.TotalCount = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return list;
        }

        public List<IntegrationDetailListModel> GetIntegrationDetailList(UserInfo user,IntegrationDetailListModel filter)
        {
            var list = new List<IntegrationDetailListModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SERVICE_CALL_LOG");

                AddPagingParametersWithLanguage(user, cmd, filter);

                db.AddInParameter(cmd, "ID_SERVICE", DbType.Int32, MakeDbNull(filter.IntegrationTypeId));
                if (filter.LogId.HasValue)
                    db.AddInParameter(cmd, "ID_LOG", DbType.Int32, MakeDbNull(filter.LogId));
                else
                    db.AddInParameter(cmd, "ID_LOG", DbType.Int32, null);

                CreateConnection(cmd);

                using (var dReader = cmd.ExecuteReader())
                {
                    while (dReader.Read())
                    {
                        var model = new IntegrationDetailListModel();
                        model.LogId = dReader["ID_LOG"].GetValue<int>();
                        model.RequestParams = dReader["REQUEST_PARAMS"].GetValue<string>();
                        model.Response = dReader["RESPONSE"].GetValue<string>();
                        model.CallStartDate = dReader["CALL_START_DATE"].GetValue<DateTime>();
                        model.CallFinishDate = dReader["CALL_FINISH_DATE"].GetValue<DateTime>();

                        model.ErrorContent = dReader["ERROR_CONTENT"].GetValue<string>();
                        model.Error = dReader["ERROR_CONTENT_XML"].GetValue<string>();

                        list.Add(model);
                    }
                    dReader.Close();
                }
                filter.TotalCount = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return list;
        }
    }
}
