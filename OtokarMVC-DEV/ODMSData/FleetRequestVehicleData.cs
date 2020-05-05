using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.FleetRequestVehicle;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class FleetRequestVehicleData : DataAccessBase
    {
        public List<FleetRequestVehicleListModel> ListFleetRequestVehicle(FleetRequestVehicleListModel filter, out int total)
        {
            var retVal = new List<FleetRequestVehicleListModel>();
            total = 0;
            try
            {


                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_FLEET_REQUEST_VEHICLE");
                db.AddInParameter(cmd, "FLEET_REQUEST_ID", DbType.Int32, filter.FleetRequestId);
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
                        var item = new FleetRequestVehicleListModel
                        {
                            FleetRequestVehicleId = reader["FLEET_REQUEST_VEHICLE_ID"].GetValue<int>(),
                            CustomerId = reader["ID_CUSTOMER"].GetValue<int>(),
                            CustomerName = reader["CUSTOMER_NAME"].GetValue<string>(),
                            FleetRequestId = reader["ID_FLEET_REQUEST"].GetValue<int>(),
                            VehicleId = reader["ID_VEHICLE"].GetValue<int>(),
                            VehicleName = reader["VEHICLE_NAME"].GetValue<string>(),
                            DocumentId = reader["DOC_ID"].GetValue<int>(),
                            DocumentName = reader["DOC_NAME"].GetValue<string>(),
                            FleetName = reader["FLEET_NAME"].GetValue<string>(),
                            ModelName = reader["MODEL"].GetValue<string>(),
                            IsWarranty = reader["IS_WARRANTY"].GetValue<int>() == 0 ? MessageResource.Global_Display_No : MessageResource.Global_Display_Yes
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

        public void DMLFleetRequestVehicle(UserInfo user, FleetRequestVehicleViewModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_DML_FLEET_REQUEST_VEHICLE");
                db.AddParameter(cmd, "FLEET_REQUEST_VEHICLE_ID", DbType.Int32, ParameterDirection.InputOutput, "FLEET_REQUEST_VEHICLE_ID", DataRowVersion.Default, MakeDbNull(model.FleetRequestVehicleId));
                db.AddInParameter(cmd, "FLEET_REQUEST_ID", DbType.Int32, MakeDbNull(model.FleetRequestId));
                db.AddInParameter(cmd, "ID_VEHICLE", DbType.Int32, MakeDbNull(model.VehicleId));
                db.AddInParameter(cmd, "ID_CUSTOMER", DbType.Int64, MakeDbNull(model.CustomerId));
                db.AddInParameter(cmd, "DOC_ID", DbType.Int64, MakeDbNull(model.DocumentId));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
                else
                    model.FleetRequestVehicleId = db.GetParameterValue(cmd, "FLEET_REQUEST_VEHICLE_ID").GetValue<int>();

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

        public FleetRequestVehicleViewModel GetFleetRequestVehicle(int fleetRequestVehicleId)
        {
            DbDataReader dReader = null;
            var FleetRequestVehicle = new FleetRequestVehicleViewModel { FleetRequestVehicleId = fleetRequestVehicleId };
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_FLEET_REQUEST_VEHICLE");
                db.AddInParameter(cmd, "FLEET_REQUEST_VEHICLE_ID", DbType.Int32, MakeDbNull(fleetRequestVehicleId));
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    FleetRequestVehicle.FleetRequestId = dReader["ID_FLEET_REQUEST"].GetValue<int>();
                    FleetRequestVehicle.CustomerId = dReader["ID_CUSTOMER"].GetValue<int>();
                    FleetRequestVehicle.CustomerName = dReader["CUSTOMER_NAME"].GetValue<string>();
                    FleetRequestVehicle.VehicleId = dReader["ID_VEHICLE"].GetValue<int>();
                    FleetRequestVehicle.VehicleName = dReader["VEHICLE_NAME"].GetValue<string>();
                    FleetRequestVehicle.DocumentId = dReader["DOC_ID"].GetValue<int>();
                    FleetRequestVehicle.DocumentName = dReader["DOC_NAME"].GetValue<string>();
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
            return FleetRequestVehicle;
        }

        public int? GetFleetRequestStatus(int fleetRequestId)
        {
            int? retvat;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_FLEET_REQUEST_STATUS");
                db.AddInParameter(cmd, "ID_FLEET_REQUEST", DbType.Int32, MakeDbNull(fleetRequestId));
                db.AddOutParameter(cmd, "STATUS", DbType.Int32, 0);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                retvat = db.GetParameterValue(cmd, "STATUS").GetValue<int?>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return retvat;
        }

    }
}
