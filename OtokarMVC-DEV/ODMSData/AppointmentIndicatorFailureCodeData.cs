using ODMSModel.AppointmentIndicatorFailureCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using System.Web.Mvc;
using ODMSCommon.Security;

namespace ODMSData
{
    public class AppointmentIndicatorFailureCodeData : DataAccessBase
    {
        public List<AppointmentIndicatorFailureCodeListModel> ListAppointmentIndicatorFailureCode(UserInfo user,AppointmentIndicatorFailureCodeListModel filter, out int totalCount)
        {
            List<AppointmentIndicatorFailureCodeListModel> listAppointmentIndicatorFailureCode = new List<AppointmentIndicatorFailureCodeListModel>();

            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_APPOINTMENT_INDICATOR_FAILURE_CODE");
                db.AddInParameter(cmd, "ID_APPOINTMENT_INDICATOR_FAILURE_CODE", DbType.Int32, filter.IdAppointmentIndicatorFailureCode);
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, filter.AdminDesc);
                db.AddInParameter(cmd, "DESCRIPTION", DbType.String, filter.Description);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, filter.IsActive);

                AddPagingParametersWithLanguage(user, cmd, filter);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        AppointmentIndicatorFailureCodeListModel model = new AppointmentIndicatorFailureCodeListModel
                        {
                            IdAppointmentIndicatorFailureCode =
                                    dr["ID_APPOINTMENT_INDICATOR_FAILURE_CODE"].GetValue<int>(),
                            AdminDesc = dr["ADMIN_DESC"].GetValue<string>(),
                            Code = dr["CODE"].GetValue<string>(),
                            IsActive = dr["IS_ACTIVE"].GetValue<int>(),
                            Description = dr["DESCRIPTION"].GetValue<string>()
                        };

                        listAppointmentIndicatorFailureCode.Add(model);
                    }
                    dr.Close();
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

            return listAppointmentIndicatorFailureCode;
        }

        public void DMLAppointmentIndicatorFailureCode(UserInfo user, AppointmentIndicatorFailureCodeViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_APPOINTMENT_INDICATOR_FAILURE_CODE");

                db.AddParameter(cmd, "ID_APPOINTMENT_INDICATOR_FAILURE_CODE", DbType.Int32, ParameterDirection.InputOutput, "ID_APPOINTMENT_INDICATOR_FAILURE_CODE", DataRowVersion.Default, model.IdAppointmentIndicatorFailureCode);
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, model.AdminDesc);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, model.IsActive);
                db.AddInParameter(cmd, "CODE", DbType.String, model.Code);
                db.AddInParameter(cmd, "ML_CONTENT", DbType.String, model.MultiLanguageContentAsText);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.IdAppointmentIndicatorFailureCode = db.GetParameterValue(cmd, "ID_APPOINTMENT_INDICATOR_FAILURE_CODE").GetValue<int>();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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

        public void GetAppointmentIndicatorFailureCode(UserInfo user, AppointmentIndicatorFailureCodeViewModel filter)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_APPOINTMENT_INDICATOR_FAILURE_CODE");

                db.AddInParameter(cmd, "CODE", DbType.String, filter.Code);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        filter.AdminDesc = dr["ADMIN_DESC"].GetValue<string>();
                        filter.IsActive = dr["IS_ACTIVE"].GetValue<bool>();
                        filter.IdAppointmentIndicatorFailureCode = dr["ID_APPOINTMENT_INDICATOR_FAILURE_CODE"].GetValue<int>();
                        filter.Code = dr["CODE"].GetValue<string>();
                        filter.Description = dr["DESCRIPTION"].GetValue<string>();
                        filter.CreateDate = dr["CREATE_DATE"].GetValue<DateTime>();
                    }
                    if (dr.NextResult())
                    {
                        var table = new DataTable();
                        table.Load(dr);
                        filter.DescriptionML.MultiLanguageContentAsText = filter.MultiLanguageContentAsText
                                                                          = GetLanguageContentFromDataSet(table, "DESCRIPTION");
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

        public List<SelectListItem> ListAppointmentCodeAsSelectListItem()
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_APPOINTMENT_INDICATOR_FAILURE_CODE_COMBO");
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["ID_APPOINTMENT_INDICATOR_FAILURE_CODE"].GetValue<string>(),
                            Text = reader["CODE"].GetValue<string>()
                        };
                        retVal.Add(lookupItem);
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
    }
}
