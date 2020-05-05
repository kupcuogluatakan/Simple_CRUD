using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSModel.AppointmentDetailsLabours;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class AppointmentDetailsLaboursData : DataAccessBase
    {
        public void DMLAppointmentDetailLabours(UserInfo user, AppointmentDetailsLaboursViewModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_DML_APPOINTMENT_INDICATOR_LABOURS");
                db.AddParameter(cmd, "APPOINTMENT_INDICATOR_LABOUR_ID", DbType.Int32, ParameterDirection.InputOutput, "APPOINTMENT_INDICATOR_LABOUR_ID", DataRowVersion.Default, model.AppointmentIndicatorLabourId);
                db.AddInParameter(cmd, "APPOINTMENT_INDICATOR_ID", DbType.Int32, MakeDbNull(model.AppointmentIndicatorId));
                db.AddInParameter(cmd, "LABOUR_ID", DbType.Int32, MakeDbNull(model.LabourId));
                db.AddInParameter(cmd, "QUANTITY", DbType.Int32, model.Quantity);
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, user.GetUserDealerId());
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.AppointmentIndicatorLabourId = db.GetParameterValue(cmd, "APPOINTMENT_INDICATOR_LABOUR_ID").GetValue<int>();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
            }
            finally
            {
                CloseConnection();
            }
        }

        public List<AppointmentDetailsLaboursListModel> ListAppointmentIndicatorLabours(UserInfo user,AppointmentDetailsLaboursListModel filter, out int totalCount)
        {
            var retVal = new List<AppointmentDetailsLaboursListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_APPOINTMENT_INDICATOR_LABOURS");
                db.AddInParameter(cmd, "APPOINTMENT_INDICATOR_ID", DbType.Int32, MakeDbNull(filter.AppointmentIndicatorId));
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var laboursListModel = new AppointmentDetailsLaboursListModel
                        {
                            AppointmentIndicatorId = filter.AppointmentIndicatorId,
                            AppointmentIndicatorLabourId = reader["APPOINTMENT_INDICATOR_LABOUR_ID"].GetValue<int>(),
                            LabourName = reader["LABOUR_TYPE_DESC"].GetValue<string>(),
                            Quantity = reader["QUANTITY"].GetValue<int>(),
                            LabourCode = reader["LABOUR_CODE"].ToString(),
                            ListPrice = reader["LIST_PRICE"].GetValue<string>() + " " + reader["CURRENCY_CODE"].GetValue<string>(),
                        };
                        retVal.Add(laboursListModel);
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

            return retVal;
        }

        public AppointmentDetailsLaboursViewModel GetAppointmentDetailsLabour(UserInfo user, int id)
        {
            var detailsLaboursViewModel = new AppointmentDetailsLaboursViewModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_APPOINTMENT_INDICATOR_LABOUR");
                db.AddInParameter(cmd, "APPOINTMENT_INDICATOR_LABOUR_ID", DbType.Int32, MakeDbNull(id));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        detailsLaboursViewModel.AppointmentIndicatorLabourId = id;
                        detailsLaboursViewModel.LabourId = reader["LABOUR_ID"].GetValue<int>();
                        detailsLaboursViewModel.AppointmentId = reader["APPOINTMENT_ID"].GetValue<int>();
                        detailsLaboursViewModel.AppointmentIndicatorId = reader["APPOINTMENT_INDICATOR_ID"].GetValue<int>();
                        detailsLaboursViewModel.txtLabourId = reader["LABOUR_TYPE_DESC"].GetValue<string>();
                        detailsLaboursViewModel.Quantity = reader["QUANTITY"].GetValue<int>();
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

            return detailsLaboursViewModel;
        }

        public void GetAppIndicType(AppointmentDetailsLaboursViewModel filter)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("GET_APPOINTMENT_INDICATOR_TYPE_BY_ID");
                db.AddInParameter(cmd, "APP_INDIC_ID", DbType.Int64, filter.AppointmentIndicatorId);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        filter.IndicType = dr["INDICATOR_TYPE_CODE"].GetValue<string>();
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
        }
    }
}
