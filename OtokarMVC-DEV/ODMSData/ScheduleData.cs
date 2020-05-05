using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSData.DataContracts;
using ODMSModel.Schedule;


namespace ODMSData
{
    public class ScheduleData : DataAccessBase
    {
        public List<ScheduleViewModel> List(UserInfo user,ScheduleViewModel filter, out int totalCnt)
        {
            var appList = AppointmentList(user,filter, out totalCnt);

            var groupedList = appList.GroupBy(x => new { x.Start, x.End }).Select(g => new ScheduleListViewModel
            {
                Start = g.Key.Start,
                End = g.Key.End,
                Qty = g.Count(),
            }).ToList();

            appList = appList.OrderBy(x => x.Start).ToList();
            groupedList = groupedList.OrderBy(x => x.Start).ToList();

            List<ScheduleViewModel> scheduleList = new List<ScheduleViewModel>();
            foreach (var x in appList)
            {
                foreach (var y in groupedList)
                {
                    if (x.Start == y.Start && x.End == y.End)
                    {
                        ScheduleViewModel viewModel = x;
                        viewModel.Qty = y.Qty;

                        scheduleList.Add(viewModel);
                    }
                }
            }

            var unAppList = UnAppointmentList(filter, out totalCnt);

            scheduleList.AddRange(unAppList);

            return scheduleList;
        }

        public void Insert(UserInfo user,ScheduleViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_APPOINTMENT_NON_WORKHOURS");
                db.AddInParameter(cmd, "ID_NON_WORKHOURS", DbType.Int32, model.NonAppId.GetValue<int>());
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, model.DealerId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, DateTime.Parse(model.AppointmentStartDate.ToString("yyyy-MM-dd ") + model.AppointmentStartTime));
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, DateTime.Parse(model.AppointmentEndDate.ToString("yyyy-MM-dd ") + model.AppointmentEndTime));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.NonAppId = db.GetParameterValue(cmd, "ID_NON_WORKHOURS").ToString();
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

        public void Delete(UserInfo user,ScheduleViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_APPOINTMENT_NON_WORKHOURS");
                db.AddInParameter(cmd, "ID_NON_WORKHOURS", DbType.Int32, model.NonAppId.GetValue<int>());
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, model.DealerId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, DateTime.Parse(model.AppointmentStartDate.ToString("yyyy-MM-dd ") + model.AppointmentStartTime));
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, DateTime.Parse(model.AppointmentEndDate.ToString("yyyy-MM-dd ") + model.AppointmentEndTime));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.NonAppId = db.GetParameterValue(cmd, "ID_NON_WORKHOURS").ToString();
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

        public void Update(UserInfo user,ScheduleViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_APPOINTMENT_NON_WORKHOURS");
                db.AddInParameter(cmd, "ID_NON_WORKHOURS", DbType.Int32, model.NonAppId.GetValue<int>());
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, model.DealerId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, DateTime.Parse(model.AppointmentStartDate.ToString("yyyy-MM-dd ") + model.AppointmentStartTime));
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, DateTime.Parse(model.AppointmentEndDate.ToString("yyyy-MM-dd ") + model.AppointmentEndTime));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.NonAppId = db.GetParameterValue(cmd, "ID_NON_WORKHOURS").ToString();
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

        public List<ScheduleViewModel> AppointmentList(UserInfo user,ScheduleViewModel filter, out int totalCnt)
        {
            var result = new List<ScheduleViewModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SCHEDULE");
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(filter.DealerId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(0));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(0));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, MakeDbNull(0));
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(0));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new ScheduleViewModel
                        {
                            AppointmentId = reader["AppointmentId"].GetValue<int>(),
                            NonAppId = "0",
                            VehiclePlate = reader["VehiclePlate"].GetValue<string>(),
                            AppointmentTypeName = reader["Type_Name"].GetValue<string>(),
                            Start =
                                new DateTime(reader["Date"].GetValue<DateTime>().Year,
                                    reader["Date"].GetValue<DateTime>().Month, reader["Date"].GetValue<DateTime>().Day,
                                    !string.IsNullOrEmpty(reader["Time"].GetValue<string>())
                                        ? TimeSpan.Parse(reader["Time"].GetValue<string>()).Hours
                                        : TimeSpan.Zero.Hours,
                                    !string.IsNullOrEmpty(reader["Time"].GetValue<string>())
                                        ? TimeSpan.Parse(reader["Time"].GetValue<string>()).Minutes
                                        : TimeSpan.Zero.Minutes, 0, DateTimeKind.Utc),
                            End =
                                new DateTime(reader["Date"].GetValue<DateTime>().Year,
                                    reader["Date"].GetValue<DateTime>().Month, reader["Date"].GetValue<DateTime>().Day,
                                    !string.IsNullOrEmpty(reader["Time"].GetValue<string>())
                                        ? TimeSpan.Parse(reader["Time"].GetValue<string>()).Hours
                                        : TimeSpan.Zero.Hours,
                                    !string.IsNullOrEmpty(reader["Time"].GetValue<string>())
                                        ? TimeSpan.Parse(reader["Time"].GetValue<string>()).Minutes
                                        : TimeSpan.Zero.Minutes, 0, DateTimeKind.Utc),
                            DeliveryEstimateDateString = reader["ESTIMATED_DELIVERY_DATE"].GetValue<string>(),
                            OptionValue = ((int) SchedulerOptionValue.Appointment).ToString(),
                            CustomerName = reader["CUSTOMER_NAME"].GetValue<string>(),
                            VinNo = reader["VIN_NO"].GetValue<string>()
                        };


                        result.Add(listModel);
                    }
                    totalCnt = result.Count();
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

        public List<ScheduleViewModel> UnAppointmentList(ScheduleViewModel filter, out int totalCnt)
        {
            var result = new List<ScheduleViewModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SCHEDULE_NON_WORKHOURS");
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(filter.DealerId));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(0));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(0));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, MakeDbNull(0));
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(0));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new ScheduleViewModel
                        {
                            AppointmentId = 0,
                            NonAppId = reader["NonAppId"].GetValue<string>(),
                            Start = new DateTime(
                                    reader["StartDate"].GetValue<DateTime>().Year,
                                    reader["StartDate"].GetValue<DateTime>().Month,
                                    reader["StartDate"].GetValue<DateTime>().Day,
                                    reader["StartDate"].GetValue<DateTime>().Hour,
                                    reader["StartDate"].GetValue<DateTime>().Minute,
                                    0, DateTimeKind.Utc),
                            End = new DateTime(
                                    reader["EndDate"].GetValue<DateTime>().Year,
                                    reader["EndDate"].GetValue<DateTime>().Month,
                                    reader["EndDate"].GetValue<DateTime>().Day,
                                    reader["EndDate"].GetValue<DateTime>().Hour,
                                    reader["EndDate"].GetValue<DateTime>().Minute,
                                    0, DateTimeKind.Utc),
                            OptionValue = ((int)SchedulerOptionValue.UnAppoinment).ToString()
                        };

                        result.Add(listModel);
                    }
                    totalCnt = result.Count();
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

        public ScheduleViewModel Get(ScheduleViewModel model)
        {
            System.Data.Common.DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_APPOINTMENT_NON_WORKS");
                db.AddInParameter(cmd, "ID_NON_WORKHOURS", DbType.Int32, MakeDbNull(model.NonAppId.GetValue<int>()));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (dReader = cmd.ExecuteReader())
                {
                    while (dReader.Read())
                    {
                        model.AppointmentStartDate = dReader["START_DATE"].GetValue<DateTime>().ToString("yyyy-MM-dd").GetValue<DateTime>();
                        model.AppointmentStartTime = TimeSpan.Parse(dReader["START_DATE"].GetValue<DateTime>().ToString("HH:mm"));
                        model.AppointmentEndDate = dReader["END_DATE"].GetValue<DateTime>().ToString("yyyy-MM-dd").GetValue<DateTime>();
                        model.AppointmentEndTime = TimeSpan.Parse(dReader["END_DATE"].GetValue<DateTime>().ToString("HH:mm"));
                        model.DealerId = dReader["DEALER_ID"].GetValue<int>();
                    }
                    dReader.Close();
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

        public List<ScheduleViewModel> WorkHourList(DateTime date, int dealerId)
        {
            var result = new List<ScheduleViewModel>();
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_EXISTS_WORK_HOUR");

                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, MakeDbNull(date));
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, MakeDbNull(date));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(dealerId));

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new ScheduleViewModel
                        {
                            WorkStat = reader["WORK_STAT"].GetValue<bool>(),
                            Monday = reader["APPLY_MONDAY"].GetValue<bool>(),
                            Tuesday = reader["APPLY_TUESDAY"].GetValue<bool>(),
                            Wednesday = reader["APPLY_WEDNESDAY"].GetValue<bool>(),
                            Thursday = reader["APPLY_THURSDAY"].GetValue<bool>(),
                            Friday = reader["APPLY_FRIDAY"].GetValue<bool>(),
                            Saturday = reader["APPLY_SATURDAY"].GetValue<bool>(),
                            Sunday = reader["APPLY_SUNDAY"].GetValue<bool>(),

                            #region Setting Date

                            WorkHourStart = new DateTime(reader["WORKHOUR_START"].GetValue<DateTime>().Year, reader["WORKHOUR_START"].GetValue<DateTime>().Month, reader["WORKHOUR_START"].GetValue<DateTime>().Day,
                                reader["WORKHOUR_START"].GetValue<DateTime>().Hour, reader["WORKHOUR_START"].GetValue<DateTime>().Minute, reader["WORKHOUR_START"].GetValue<DateTime>().Second, DateTimeKind.Utc),

                            WorkHourEnd = new DateTime(reader["WORKHOUR_END"].GetValue<DateTime>().Year, reader["WORKHOUR_END"].GetValue<DateTime>().Month, reader["WORKHOUR_END"].GetValue<DateTime>().Day,
                                reader["WORKHOUR_END"].GetValue<DateTime>().Hour, reader["WORKHOUR_END"].GetValue<DateTime>().Minute, reader["WORKHOUR_END"].GetValue<DateTime>().Second, DateTimeKind.Utc),

                            LunchBreakStart = new DateTime(reader["LUNCHBREAK_START"].GetValue<DateTime>().Year, reader["LUNCHBREAK_START"].GetValue<DateTime>().Month, reader["LUNCHBREAK_START"].GetValue<DateTime>().Day,
                                reader["LUNCHBREAK_START"].GetValue<DateTime>().Hour, reader["LUNCHBREAK_START"].GetValue<DateTime>().Minute, reader["LUNCHBREAK_START"].GetValue<DateTime>().Second, DateTimeKind.Utc),

                            LunchBreakEnd = new DateTime(reader["LUNCHBREAK_END"].GetValue<DateTime>().Year, reader["LUNCHBREAK_END"].GetValue<DateTime>().Month, reader["LUNCHBREAK_END"].GetValue<DateTime>().Day,
                                reader["LUNCHBREAK_END"].GetValue<DateTime>().Hour, reader["LUNCHBREAK_END"].GetValue<DateTime>().Minute, reader["LUNCHBREAK_END"].GetValue<DateTime>().Second, DateTimeKind.Utc),

                            #endregion
                        };

                        result.Add(listModel);
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

        public bool Exists(ScheduleViewModel model)
        {
            var result = false;
            try
            {
                CreateDatabase();

                var startDate = new DateTime();
                var endDate = new DateTime();

                if (model.OptionValue == ((int)SchedulerOptionValue.Appointment).ToString())
                {
                    startDate = new DateTime(model.AppointmentDate.Year, model.AppointmentDate.Month, model.AppointmentDate.Day, TimeSpan.Parse(model.AppointmentTime.Value.ToString()).Hours, TimeSpan.Parse(model.AppointmentTime.Value.ToString()).Minutes, 0);
                    endDate = new DateTime(model.AppointmentDate.Year, model.AppointmentDate.Month, model.AppointmentDate.Day, TimeSpan.Parse(model.AppointmentTime.Value.ToString()).Hours, TimeSpan.Parse(model.AppointmentTime.Value.ToString()).Minutes, 0);
                }
                else if (model.OptionValue == ((int)SchedulerOptionValue.UnAppoinment).ToString())
                {
                    startDate = new DateTime(model.AppointmentStartDate.Year, model.AppointmentStartDate.Month, model.AppointmentStartDate.Day, TimeSpan.Parse(model.AppointmentStartTime.Value.ToString()).Hours, TimeSpan.Parse(model.AppointmentStartTime.Value.ToString()).Minutes, 0);
                    endDate = new DateTime(model.AppointmentEndDate.Year, model.AppointmentEndDate.Month, model.AppointmentEndDate.Day, TimeSpan.Parse(model.AppointmentEndTime.Value.ToString()).Hours, TimeSpan.Parse(model.AppointmentEndTime.Value.ToString()).Minutes, 0);
                }

                #region ALL APPOINTMENT
                var cmd = db.GetStoredProcCommand("P_EXISTS_NONE_WORKHOUR");

                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, MakeDbNull(startDate));
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, MakeDbNull(endDate));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(model.DealerId));

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = true;
                    }

                    reader.Close();
                }
                #endregion

                #region IF CLOSED APPOINMENT
                if (model.OptionValue == ((int)SchedulerOptionValue.UnAppoinment).ToString())
                {
                    var cmd2 = db.GetStoredProcCommand("P_EXISTS_APPOINTMENT");

                    db.AddInParameter(cmd2, "START_DATE", DbType.DateTime, MakeDbNull(startDate));
                    db.AddInParameter(cmd2, "@END_DATE", DbType.DateTime, MakeDbNull(endDate));
                    db.AddInParameter(cmd2, "@ID_DEALER", DbType.Int32, MakeDbNull(model.DealerId));

                    CreateConnection(cmd2);

                    using (var reader = cmd2.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = true;
                        }

                        reader.Close();
                    }
                }
                #endregion

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
