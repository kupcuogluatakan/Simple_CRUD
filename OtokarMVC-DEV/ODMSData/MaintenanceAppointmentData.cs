using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.MaintenanceAppointment;
using System;

namespace ODMSData
{
    public class MaintenanceAppointmentData : DataAccessBase
    {
        public List<MaintenanceAppointmentListModel> GetMaintenanceAppointmentList(UserInfo user, MaintenanceAppointmentListModel filter, out int totalCount)
        {
            List<MaintenanceAppointmentListModel> list_MaintModel = new List<MaintenanceAppointmentListModel>();

            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_MAINTENANCE_APPOINTMENT");
                db.AddInParameter(cmd, "PLATE", DbType.String, MakeDbNull(filter.Plate));
                db.AddInParameter(cmd, "VIN_NO", DbType.String, MakeDbNull(filter.VinNo));
                db.AddInParameter(cmd, "VEHICLE_TYPE_ID", DbType.Int32, MakeDbNull(filter.VehicleTypeId));
                db.AddInParameter(cmd, "MODEL_KOD", DbType.String, filter.VehicleModelKod);
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, user.GetUserDealerId());
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        MaintenanceAppointmentListModel model = new MaintenanceAppointmentListModel
                        {
                            AdminDesc = dr["ADMIN_DESC"].GetValue<string>(),
                            MaintenanceId = dr["MAINTENANCE_ID"].GetValue<int>(),
                            MaintKM = dr["MAINT_KM"].GetValue<string>(),
                            MaintMonth = dr["MAINT_MONTH"].GetValue<string>(),
                            MaintenanceModelKod = dr["MAINTENANCE_MODEL_KOD"].GetValue<string>(),
                            VehicleTypeName = dr["TYPE_NAME"].GetValue<string>(),
                            MaintenanceVehicleTypeId = dr["MAINTENANCE_VEHICLE_TYPE_ID"].GetValue<int>(),
                            VehicleId = dr["VEHICLE_ID"].GetValue<int>(),
                            Plate = dr["PLATE"].GetValue<string>(),
                            VinNo = dr["VIN_NO"].GetValue<string>(),
                            VehicleModelKod = dr["VEHICLE_MODEL_KOD"].GetValue<string>(),
                            VehicleTypeId = dr["VEHICLE_VEHICLE_TYPE_ID"].GetValue<int>(),
                            VehicleVehicleTypeId = dr["VEHICLE_VEHICLE_TYPE_ID"].GetValue<int>(),
                            Price = dr["SUM_PARTS"].GetValue<decimal>() + dr["SUM_LABOURS"].GetValue<decimal>()
                        };

                        list_MaintModel.Add(model);
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
            return list_MaintModel;
        }

        public void DMLMaintenanceAppointment(UserInfo user, MaintenanceAppointmentViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_MAINTENANCE_APPOINTMENT");
                db.AddParameter(cmd, "APPOINTMENT_ID", DbType.Int32, ParameterDirection.InputOutput, "APPOINTMENT_ID",
                    DataRowVersion.Default, model.AppointmentId);
                db.AddInParameter(cmd, "MAINTENANCE_ID", DbType.Int32, model.MaintenanceId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "CUSTOMER_ID", DbType.Int32, MakeDbNull(model.CustomerId));
                db.AddInParameter(cmd, "VEHICLE_ID", DbType.Int32, MakeDbNull(model.VehicleId));
                db.AddInParameter(cmd, "APPOINTMENT_TYPE_ID", DbType.Int32, model.AppointmentTypeId);
                db.AddInParameter(cmd, "COMPLAINT_DESC", DbType.String, MakeDbNull(model.ComplaintDescription));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(model.DealerId));
                db.AddInParameter(cmd, "APPOINTMENT_DATE", DbType.Date, model.AppointmentDate);
                db.AddInParameter(cmd, "APPOINTMENT_TIME", DbType.String, MakeDbNull(model.AppointmentTimeFormatted));
                db.AddInParameter(cmd, "CONTACT_NAME", DbType.String, MakeDbNull(model.ContactName));
                db.AddInParameter(cmd, "CONTACT_SURNAME", DbType.String, MakeDbNull(model.ContactLastName));
                db.AddInParameter(cmd, "CONTACT_PHONE", DbType.String, MakeDbNull(model.ContactPhone));
                db.AddInParameter(cmd, "CONTACT_ADDRESS", DbType.String, MakeDbNull(model.ContactAddress));
                db.AddInParameter(cmd, "VEHICLE_TYPE", DbType.String, MakeDbNull(model.VehicleTypeId));
                db.AddInParameter(cmd, "VEHICLE_PLATE", DbType.String, MakeDbNull(model.VehiclePlate));
                db.AddInParameter(cmd, "VEHICLE_COLOR", DbType.String, MakeDbNull(model.VehicleColor));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.AppointmentId = db.GetParameterValue(cmd, "APPOINTMENT_ID").GetValue<int>();
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
    }
}
