using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using ODMSCommon;
using ODMSModel.WorkHour;
using ODMSCommon.Security;

namespace ODMSData
{
    public class WorkHourData : DataAccessBase
    {
        public List<TeaBreakModel> ListTeaBreaks(int workHourId)
        {
            var result = new List<TeaBreakModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_WORK_HOUR_TEA_BREAKS");
                db.AddInParameter(cmd, "ID_WORK_HOUR", DbType.Int32, workHourId);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new TeaBreakModel
                        {
                            StartTime = reader["TEA_BREAK_START"] != null
                                ? reader["TEA_BREAK_START"].ToString()
                                : "",
                            EndTime = reader["TEA_BREAK_END"] != null
                            ? reader["TEA_BREAK_END"].ToString()
                            : "",
                            Text = reader["TEXT"].GetValue<string>(),
                            Value = reader["VALUE"].GetValue<int>()
                        };
                        result.Add(item);
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

        public void DMLWorkHour(UserInfo user, WorkHourViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_WORK_HOURS");
                db.AddParameter(cmd, "ID_WORK_HOUR", DbType.Int64, ParameterDirection.InputOutput, "ID_WORK_HOUR", DataRowVersion.Default, model.WorkHourId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(model.DealerId));
                db.AddInParameter(cmd, "VALIDITY_DATE_START", DbType.DateTime, MakeDbNull(model.StartDate));
                db.AddInParameter(cmd, "VALIDITY_DATE_END", DbType.DateTime, MakeDbNull(model.EndDate));
                db.AddInParameter(cmd, "WORK_STAT", DbType.Boolean, MakeDbNull(model.StatusOfWork));
                db.AddInParameter(cmd, "DESCRIPTION", DbType.String, MakeDbNull(model.Description));
                db.AddInParameter(cmd, "APPOINTMENT_PERIOD", DbType.Int32, MakeDbNull(model.MeetingInterval));
                db.AddInParameter(cmd, "PRIORITY", DbType.Int32, MakeDbNull(model.Priority));
                db.AddInParameter(cmd, "APPLY_MONDAY", DbType.Boolean, model.CommandType == CommonValues.DMLType.Delete ? false : model.WeekDays[0]);
                db.AddInParameter(cmd, "APPLY_TUESDAY", DbType.Boolean, model.CommandType == CommonValues.DMLType.Delete ? false : model.WeekDays[1]);
                db.AddInParameter(cmd, "APPLY_WEDNESDAY", DbType.Boolean, model.CommandType == CommonValues.DMLType.Delete ? false : model.WeekDays[2]);
                db.AddInParameter(cmd, "APPLY_THURSDAY", DbType.Boolean, model.CommandType == CommonValues.DMLType.Delete ? false : model.WeekDays[3]);
                db.AddInParameter(cmd, "APPLY_FRIDAY", DbType.Boolean, model.CommandType == CommonValues.DMLType.Delete ? false : model.WeekDays[4]);
                db.AddInParameter(cmd, "APPLY_SATURDAY", DbType.Boolean, model.CommandType == CommonValues.DMLType.Delete ? false : model.WeekDays[5]);
                db.AddInParameter(cmd, "APPLY_SUNDAY", DbType.Boolean, model.CommandType == CommonValues.DMLType.Delete ? false : model.WeekDays[6]);

                db.AddInParameter(cmd, "WORKHOUR_START", DbType.DateTime, MakeDbNull(Convert.ToDateTime(model.WorkStartHour.ToString())));
                db.AddInParameter(cmd, "WORKHOUR_END", DbType.Time, MakeDbNull(Convert.ToDateTime(model.WorkEndHour.ToString())));
                db.AddInParameter(cmd, "LAUNCHBREAK_START", DbType.Time, MakeDbNull(Convert.ToDateTime(model.LaunchBreakStartHour.ToString())));
                db.AddInParameter(cmd, "LAUNCHBREAK_END", DbType.Time, MakeDbNull(Convert.ToDateTime(model.LaunchBreakEndHour.ToString())));

                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, model.IsActive);

                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.WorkHourId = db.GetParameterValue(cmd, "ID_WORK_HOUR").GetValue<int>();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
                }
                else
                {
                    //aynı cay molasından 2 tane eklemesin
                    var teaBreaks = model.TeaBreaks.DistinctBy(c => c.StartTime);
                    cmd.Parameters.Clear();
                    //önce silmesi için çalıştırıyorum sp yi
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "P_DML_WORK_HOURS_TEA_BREAKS";
                    db.AddInParameter(cmd, "ID_WORK_HOUR", DbType.Int32, model.WorkHourId);
                    db.AddInParameter(cmd, "DELETE_ALL", DbType.Boolean, true);
                    db.AddInParameter(cmd, "TEABREAK_START", DbType.Time, null);
                    db.AddInParameter(cmd, "TEABREAK_END", DbType.Time, null);
                    //aynı command nesnesini üzerinden devam...
                    cmd.ExecuteNonQuery();
                    teaBreaks.ForEach(c =>
                    {
                        cmd.Parameters.Clear();
                        db.AddInParameter(cmd, "ID_WORK_HOUR", DbType.Int32, model.WorkHourId);
                        db.AddInParameter(cmd, "TEABREAK_START", DbType.Time, MakeDbNull(c.StartTime.GetValue<DateTime>()));
                        db.AddInParameter(cmd, "TEABREAK_END", DbType.Time, MakeDbNull(c.EndTime.GetValue<DateTime>()));
                        db.AddInParameter(cmd, "DELETE_ALL", DbType.Boolean, false);
                        cmd.ExecuteNonQuery();
                    });
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

        public List<WorkHourListModel> ListWorkHours(UserInfo user, WorkHourListModel filter, out int total)
        {
            var retVal = new List<WorkHourListModel>();
            total = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_WORK_HOURS");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(filter.DealerId));
                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, MakeDbNull(filter.StartDate));
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, MakeDbNull(filter.EndDate));
                db.AddInParameter(cmd, "PRIORITY", DbType.Int32, MakeDbNull(filter.Priority));
                db.AddInParameter(cmd, "STATUS_OF_WORK", DbType.Int32, filter.StatusOfWork);
                db.AddInParameter(cmd, "MEETING_INTERVAL", DbType.Int32, MakeDbNull(filter.MeetingInterval));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, filter.IsActive);

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
                        var item = new WorkHourListModel
                        {
                            WorkHourId = reader["ID_WORK_HOUR"].GetValue<int>(),
                            ValidityStartDate = reader["VALIDITY_DATE_START"].GetValue<DateTime>(),
                            ValidityEndDate = reader["VALIDITY_DATE_END"].GetValue<DateTime>(),
                            Priority = reader["PRIORITY"].GetValue<int>(),
                            AppointmentPeriod = reader["APPOINTMENT_PERIOD"].GetValue<int>(),
                            WorkStatusString = reader["WORK_STAT_STRING"].GetValue<string>(),
                            IsActive = reader["IS_ACTIVE"].GetValue<int?>(),
                            IsActiveString = reader["IS_ACTIVE_STRING"].GetValue<string>(),
                            WorkStartHour = reader["WORKHOUR_START"].GetValue<DateTime>().ToTimeSpan(),
                            WorkEndHour = reader["WORKHOUR_END"].GetValue<DateTime>().ToTimeSpan()
                    };

                        retVal.Add(item);
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

        public WorkHourViewModel GetWorkHour(UserInfo user, int id)
        {
            var result = new WorkHourViewModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_WORK_HOUR");
                db.AddInParameter(cmd, "ID_WORK_HOUR", DbType.Int32, id);
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, user.GetUserDealerId());
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.WorkHourId = id;
                        result.StartDate = reader["VALIDITY_DATE_START"].GetValue<DateTime>();
                        result.EndDate = reader["VALIDITY_DATE_END"].GetValue<DateTime>();
                        result.StatusOfWork = reader["WORK_STAT"].GetValue<bool>()
                            ? WorkHourViewModel.WorkStatus.Works
                            : WorkHourViewModel.WorkStatus.NotWorks;
                        result.Description = reader["DECSRIPTION"].GetValue<string>();
                        result.MeetingInterval = reader["APPOINTMENT_PERIOD"].GetValue<int>();
                        result.Priority = reader["PRIORITY"].GetValue<int>();
                        result.WorkStartHour = reader["WORKHOUR_START"].GetValue<DateTime>().ToTimeSpan();
                        result.WorkEndHour = reader["WORKHOUR_END"].GetValue<DateTime>().ToTimeSpan();
                        result.LaunchBreakStartHour = reader["LUNCHBREAK_START"].GetValue<DateTime>().ToTimeSpan();
                        result.LaunchBreakEndHour = reader["LUNCHBREAK_END"].GetValue<DateTime>().ToTimeSpan();
                        result.WeekDays.Add(reader["APPLY_MONDAY"].GetValue<bool>());
                        result.WeekDays.Add(reader["APPLY_TUESDAY"].GetValue<bool>());
                        result.WeekDays.Add(reader["APPLY_WEDNESDAY"].GetValue<bool>());
                        result.WeekDays.Add(reader["APPLY_THURSDAY"].GetValue<bool>());
                        result.WeekDays.Add(reader["APPLY_FRIDAY"].GetValue<bool>());
                        result.WeekDays.Add(reader["APPLY_SATURDAY"].GetValue<bool>());
                        result.WeekDays.Add(reader["APPLY_SUNDAY"].GetValue<bool>());
                        result.DealerId = reader["DEALER_ID"].GetValue<int>();
                        result.DealerName = reader["DEALER_NAME"].GetValue<string>();
                        result.IsActive = reader["IS_ACTIVE"].GetValue<bool>();
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
    }
}
