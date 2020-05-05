using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSModel.Appointment;
using ODMSCommon.Resources;
using System.Dynamic;
using ODMSCommon.Security;

namespace ODMSData
{
    public class AppointmentData : DataAccessBase
    {
        public List<AppointmentListModel> ListAppointments(UserInfo user, AppointmentListModel filter, out int totalCount)
        {
            DbDataReader dbDataReader = null;
            var retVal = new List<AppointmentListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_APPOINTMENTS");
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "APPOINTMENT_TYPE_ID", DbType.Int32, MakeDbNull(filter.AppointmentTypeId));
                db.AddInParameter(cmd, "APPOINTMENT_STATUS_LOOK_VAL", DbType.Int32, MakeDbNull(filter.AppointmentStatus));
                db.AddInParameter(cmd, "CUSTOMER_FULL_NAME", DbType.String, MakeDbNull(filter.CustomerFullName));
                db.AddInParameter(cmd, "VEHICLE_PLATE", DbType.String, MakeDbNull(filter.VehiclePlate));
                db.AddInParameter(cmd, "APPOINTMENT_DATE_START", DbType.DateTime, MakeDbNull(filter.AppointmentDate));
                db.AddInParameter(cmd, "APPOINTMENT_DATE_END", DbType.DateTime, MakeDbNull(filter.AppointmentDateEnd));
                AddPagingParametersWithLanguage(user, cmd, filter);

                CreateConnection(cmd);

                using (dbDataReader = cmd.ExecuteReader())
                {
                    while (dbDataReader.Read())
                    {
                        var appointmentListModel = new AppointmentListModel
                        {
                            AppointmentId = dbDataReader["APPOINTMENT_ID"].GetValue<int>(),
                            CustomerFullName = dbDataReader["CUSTOMER_FULL_NAME"].GetValue<string>(),
                            VehicleType = dbDataReader["VEHICLE_TYPE"].GetValue<string>(),
                            VehiclePlate = dbDataReader["VEHICLE_PLATE"].GetValue<string>().Replace(" ", ""),
                            AppointmentTypeName = dbDataReader["APPOINTMENT_TYPE_NAME"].GetValue<string>(),
                            AppointmentDate = dbDataReader["APPOINTMENT_DATE"].GetValue<DateTime>(),
                            AppointmentStatus = dbDataReader["APPOINTMENT_STATUS_LOOKVAL"].GetValue<int>(),
                            AppStatus = dbDataReader["APPOINTMENT_STATUS"].GetValue<string>(),
                            AppointmentTime = dbDataReader["APPOINTMENT_TIME"].GetValue<string>()
                        };
                        retVal.Add(appointmentListModel);
                    }
                    dbDataReader.Close();

                }
                totalCount = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbDataReader != null && !dbDataReader.IsClosed)
                    dbDataReader.Close();
                CloseConnection();
            }

            return retVal;
        }

        public void DMLAppointment(UserInfo user, AppointmentViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_APPOINTMENT");
                db.AddParameter(cmd, "APPOINTMENT_ID", DbType.Int64, ParameterDirection.InputOutput, "APPOINTMENT_ID", DataRowVersion.Default, model.AppointmentId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "CUSTOMER_ID", DbType.Int64, MakeDbNull(model.CustomerId));
                db.AddInParameter(cmd, "VEHICLE_ID", DbType.Int32, MakeDbNull(model.VehicleId));
                db.AddInParameter(cmd, "APPOINTMENT_TYPE_ID", DbType.Int32, model.AppointmentTypeId);
                db.AddInParameter(cmd, "COMPLAINT_DESC", DbType.String, MakeDbNull(model.ComplaintDescription));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(model.DealerId));
                db.AddInParameter(cmd, "APPOINTMENT_DATE", DbType.String, MakeDbNull(model.AppointmentDate));
                db.AddInParameter(cmd, "APPOINTMENT_TIME", DbType.String, MakeDbNull(model.AppointmentTime.ToString()));
                db.AddInParameter(cmd, "ESTIMATED_DELIVERY_DATE", DbType.String, MakeDbNull(model.DeliveryEstimateDate));
                db.AddInParameter(cmd, "ESTIMATED_DELIVERY_TIME", DbType.String, MakeDbNull(model.DeliveryEstimateTime.ToString()));
                db.AddInParameter(cmd, "CONTACT_NAME", DbType.String, MakeDbNull(model.ContactFirstName));
                db.AddInParameter(cmd, "CONTACT_SURNAME", DbType.String, MakeDbNull(model.ContactLastName));
                db.AddInParameter(cmd, "CONTACT_PHONE", DbType.String, MakeDbNull(model.ContactPhone));
                db.AddInParameter(cmd, "CONTACT_ADDRESS", DbType.String, MakeDbNull(model.ContactAddress));
                db.AddInParameter(cmd, "VEHICLE_TYPE", DbType.String, MakeDbNull(model.VehicleType));
                db.AddInParameter(cmd, "VEHICLE_PLATE", DbType.String, MakeDbNull(string.IsNullOrEmpty(model.VehiclePlate) ? string.Empty : model.VehiclePlate.ToUpper()));
                db.AddInParameter(cmd, "VEHICLE_COLOR", DbType.String, MakeDbNull(model.VehicleColor));
                db.AddInParameter(cmd, "STATUS_ID", DbType.String, MakeDbNull(model.StatusId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.AppointmentId = db.GetParameterValue(cmd, "APPOINTMENT_ID").GetValue<int>();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 2)
                    model.ErrorMessage = MessageResource.Appointment_Error_NullAppointmentId;
                else if (model.ErrorNo > 0)
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

        public AppointmentViewModel GetAppointment(UserInfo user, int appointmentId)
        {
            DbDataReader dReader = null;
            var appointmentModel = new AppointmentViewModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_APPOINTMENT");
                db.AddInParameter(cmd, "APPOINTMENT_ID", DbType.Int32, MakeDbNull(appointmentId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (dReader = cmd.ExecuteReader())
                {
                    while (dReader.Read())
                    {
                        appointmentModel.AppointmentId = dReader["APPOINTMENT_ID"].GetValue<int>();
                        appointmentModel.CustomerId = dReader["CUSTOMER_ID"].GetValue<int>();
                        appointmentModel.CustomerName = dReader["CUSTOMER_NAME"].GetValue<string>();
                        appointmentModel.VehicleId = dReader["VEHICLE_ID"].GetValue<int>();
                        appointmentModel.AppointmentTypeId = dReader["APPOINTMENT_TYPE_ID"].GetValue<int>();
                        appointmentModel.AppointmentTypeName = dReader["APPOINTMENT_TYPE_NAME"].GetValue<string>();
                        appointmentModel.ComplaintDescription = dReader["COMPLAINT_DESC"].GetValue<string>();
                        appointmentModel.DealerId = dReader["DEALER_ID"].GetValue<int>();
                        appointmentModel.DealerName = dReader["DEALER_SHRT_NAME"].GetValue<string>();

                        appointmentModel.AppointmentDate = dReader["APPOINTMENT_DATE"].GetValue<DateTime>();
                        appointmentModel.AppointmentDateFormatted = appointmentModel.AppointmentDate != null ? String.Format("{0:dd/MM/yyyy}", appointmentModel.AppointmentDate) : null;
                        var appointmentTime = dReader["APPOINTMENT_TIME"].GetValue<string>();
                        if (!string.IsNullOrWhiteSpace(appointmentTime) && appointmentTime.IndexOf(":") > 0)
                        {
                            var splittedAppointmentTime = appointmentTime.Split(':');
                            appointmentModel.AppointmentTime = new TimeSpan(int.Parse(splittedAppointmentTime[0]), int.Parse(splittedAppointmentTime[1]), 0);
                        }

                        appointmentModel.DeliveryEstimateDate = dReader["ESTIMATED_DELIVERY_DATE"].GetValue<DateTime?>();
                        var estimatedDeliveryTime = dReader["ESTIMATED_DELIVERY_TIME"].GetValue<string>();
                        if (!string.IsNullOrWhiteSpace(estimatedDeliveryTime) && estimatedDeliveryTime.IndexOf(":") > 0)
                        {
                            var splittedEstimatedDeliveryTime = estimatedDeliveryTime.Split(':');
                            appointmentModel.DeliveryEstimateTime = new TimeSpan(int.Parse(splittedEstimatedDeliveryTime[0]), int.Parse(splittedEstimatedDeliveryTime[1]), 0);
                        }

                        appointmentModel.ContactName = dReader["CONTACT_NAME"].GetValue<string>();
                        appointmentModel.ContactLastName = dReader["CONTACT_SURNAME"].GetValue<string>();
                        appointmentModel.ContactPhone = dReader["CONTACT_PHONE"].GetValue<string>();
                        appointmentModel.ContactAddress = dReader["CONTACT_ADDRESS"].GetValue<string>();
                        appointmentModel.VehicleModelCode = dReader["VEHICLE_MODEL_CODE"].GetValue<string>();
                        appointmentModel.VehicleType = dReader["VEHICLE_TYPE"].GetValue<string>();
                        appointmentModel.VehicleTypeName = dReader["VEHICLE_TYPE_NAME"].GetValue<string>();
                        appointmentModel.VehicleModelName = dReader["VEHICLE_MODEL_NAME"].GetValue<string>();
                        appointmentModel.VehiclePlate = dReader["VEHICLE_PLATE"].GetValue<string>();
                        appointmentModel.VehicleColor = dReader["VEHICLE_COLOR"].GetValue<string>();
                        appointmentModel.VehicleIdVehiclePlate = dReader["VEHICLE_ID_VEHICLE_PLATE"].GetValue<string>();
                        appointmentModel.VehicleVin = dReader["VIN_NO"].GetValue<string>();
                        appointmentModel.StatusId = dReader["STATUS_ID"].GetValue<int>();
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


            return appointmentModel;
        }

        public void GetAppointmentCustomer(UserInfo user, AppointmentCustomerViewModel filter)
        {

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_APPOINTMENT_CUSTOMER");
                db.AddInParameter(cmd, "ID_CUSTOMER", DbType.Int32, MakeDbNull(filter.CustomerId));
                db.AddInParameter(cmd, "ID_APPOINTMENT", DbType.Int32, MakeDbNull(filter.AppointmentId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var dReader = cmd.ExecuteReader())
                {
                    while (dReader.Read())
                    {
                        filter.Name = dReader["CUSTOMER_NAME"].GetValue<string>();
                        filter.FName = dReader["FIRST_NAME"].GetValue<string>();
                        filter.LName = dReader["LAST_NAME"].GetValue<string>();
                    }
                    if (dReader.NextResult())
                    {
                        while (dReader.Read())
                        {
                            filter.Address = dReader["ADDRESS_1"].GetValue<string>();
                        }
                    }
                    if (dReader.NextResult())
                    {
                        while (dReader.Read())
                        {
                            filter.Phone = dReader["CONTACT_TYPE_VALUE"].GetValue<string>();
                        }
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

        public AppointmentVehicleInfo GetAppointmentVehicleInfo(int vehicleId, int appointmentId)
        {
            var model = new AppointmentVehicleInfo();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_APPOINTMENT_VEHICLE_INFO");
                db.AddInParameter(cmd, "VEHICLE_ID", DbType.Int32, vehicleId);
                db.AddInParameter(cmd, "APPOINTMENT_ID", DbType.Int32, appointmentId);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        model.VehicleModel = dr["MODEL_NAME"].GetValue<string>();
                        model.VehicleType = dr["TYPE_NAME"].GetValue<string>();
                        model.VehicleColor = dr["COLOR"].GetValue<string>();
                        model.VehicleVin = dr["VIN_NO"].GetValue<string>();
                        model.VehiclePlate = dr["PLATE"].GetValue<string>();
                        model.CustomerId = dr["ID_CUSTOMER"].GetValue<int>();
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
            return model;
        }

        public dynamic GetAppointmentData(int id, string type, int? appointmentId)
        {
            dynamic obj = new ExpandoObject();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_APPOINTMENT_DATA");
                db.AddInParameter(cmd, "ID", DbType.Int32, id);
                db.AddInParameter(cmd, "TYPE", DbType.String, type);
                db.AddInParameter(cmd, "APPOINTMENT_ID", DbType.Int64, appointmentId);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        switch (type)
                        {
                            case "Appointment":

                                obj.CustomerId = reader["CUSTOMER_ID"].ToString();
                                obj.CustomerName = reader["CUSTOMER_NAME"].ToString();
                                obj.CustomerSurName = reader["CUSTOMER_SURNAME"].ToString();
                                obj.CustomerPhone = reader["CUSTOMER_PHONE"].ToString();
                                obj.CustomerAddress = reader["CUSTOMER_ADDRESS"].ToString();
                                obj.VehicleId = reader["VEHICLE_ID"].GetValue<int>();
                                obj.VehicleModel = reader["MODEL_NAME"].ToString();
                                obj.VehiclePlate = reader["PLATE"].ToString();
                                obj.VehicleType = reader["TYPE_NAME"].ToString();
                                obj.VehicleVin = reader["VIN_NO"].ToString();
                                obj.Complaint = reader["COMPLAINT_DESC"].ToString();
                                obj.AppointmentTypeId = reader["APPOINTMENT_TYPE_ID"].GetValue<int>();

                                break;
                            case "Customer":

                                obj.CustomerId = reader["CUSTOMER_ID"].ToString();
                                obj.CustomerName = reader["CUSTOMER_NAME"].ToString();
                                obj.CustomerSurName = reader["CUSTOMER_SURNAME"].ToString();
                                obj.CustomerPhone = reader["CUSTOMER_PHONE"].ToString();
                                obj.CustomerAddress = reader["CUSTOMER_ADDRESS"].ToString();

                                break;
                            case "Vehicle":

                                obj.VehicleModel = reader["MODEL_NAME"].ToString();
                                obj.VehiclePlate = reader["PLATE"].ToString();
                                obj.VehicleType = reader["TYPE_NAME"].ToString();
                                obj.CustomerId = reader["CUSTOMER_ID"].ToString();
                                obj.CustomerName = reader["CUSTOMER_NAME"].ToString();
                                obj.CustomerSurName = reader["CUSTOMER_SURNAME"].ToString();
                                obj.CustomerPhone = reader["CUSTOMER_PHONE"].ToString();
                                obj.VehicleColor = reader["COLOR"].ToString();
                                obj.VehicleVin = reader["VIN_NO"].ToString();
                                obj.CustomerAddress = reader["CUSTOMER_ADDRESS"].ToString();

                                break;
                        }
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

            return obj;
        }

        public int GetAppointmentPeriod(int dealerId, DateTime appointmentDate, TimeSpan appointmentTime)
        {
            int appointmentInterval = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_INTERVAL_FROM_WORK_HOUR");
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(dealerId));
                db.AddInParameter(cmd, "PDATE", DbType.String, MakeDbNull(appointmentDate));
                db.AddInParameter(cmd, "TIME", DbType.String, MakeDbNull(appointmentTime.ToString()));
                db.AddOutParameter(cmd, "APPOINTMENT_PERIOD", DbType.Int32, 0);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        appointmentInterval = reader["APPOINTMENT_PERIOD"].GetValue<int>();
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
            return appointmentInterval;
        }
    }
}
