using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using ODMSCommon;
using ODMSModel.FleetVehicle;
using ODMSCommon.Security;

namespace ODMSData
{
    public class FleetVehicleData : DataAccessBase
    {
        public List<FleetVehicleListModel> ListFleetVehicle(FleetVehicleListModel filter, out int total)
        {
            var retVal = new List<FleetVehicleListModel>();
            total = 0;
            try
            {


                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_FLEET_VEHICLE");
                db.AddInParameter(cmd, "FLEET_ID", DbType.Int32, MakeDbNull(filter.FleetId));
                db.AddInParameter(cmd, "CUSTOMER_ID", DbType.Int32, MakeDbNull(filter.CustomerId));
                db.AddInParameter(cmd, "VEHICLE_ID", DbType.Int32, MakeDbNull(filter.VehicleId));
                AddPagingParameters(cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new FleetVehicleListModel
                        {
                            FleetVehicleId = reader["FLEET_VEHICLE_ID"].GetValue<int>(),
                            CustomerId = reader["ID_CUSTOMER"].GetValue<int>(),
                            CustomerName = reader["CUSTOMER_NAME"].GetValue<string>(),
                            FleetId = reader["FLEET_ID"].GetValue<int>(),
                            VehicleId = reader["ID_VEHICLE"].GetValue<int>(),
                            VehicleName = reader["VEHICLE_NAME"].GetValue<string>()
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

        public FleetVehicleViewModel GetFleetVehicle(int fleetVehicleId)
        {
            DbDataReader dReader = null;
            var fleetVehicle = new FleetVehicleViewModel { FleetVehicleId = fleetVehicleId };
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_FLEET_VEHICLE");
                db.AddInParameter(cmd, "FLEET_VEHICLE_ID", DbType.Int32, MakeDbNull(fleetVehicleId));
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    fleetVehicle.FleetId = dReader["FLEET_ID"].GetValue<int>();
                    fleetVehicle.CustomerId = dReader["ID_CUSTOMER"].GetValue<int>();
                    fleetVehicle.CustomerName = dReader["CUSTOMER_NAME"].GetValue<string>();
                    fleetVehicle.VehicleId = dReader["ID_VEHICLE"].GetValue<int>();
                    fleetVehicle.VehicleName = dReader["VEHICLE_NAME"].GetValue<string>();
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
            return fleetVehicle;
        }

        public bool IsFleetVehicle(int vehicleId, int customerId)
        {
            return ExecSqlFunction<bool>("FN_IS_FLEET_VEHICLE", vehicleId, customerId);
        }

        public void GetVehicleCustomerList(List<FleetVehicleViewModel> filter, string vinList, int fleetId)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_VEHICLE_CUSTOMER_WITH_VIN_NO");
                db.AddInParameter(cmd, "VEHICLE_VIN_LIST", DbType.String, MakeDbNull(vinList));

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {

                        filter.Where(p => p.VehicleVinNo == dr["VIN_NO"].GetValue<string>())
                                .ForEach(p =>
                                {
                                    p.VehicleId = dr["ID_VEHICLE"].GetValue<int>();
                                    p.CustomerId = dr["ID_CUSTOMER"].GetValue<int>();
                                    p.VehicleVinNo = dr["VIN_NO"].GetValue<string>();
                                    p.CustomerName = dr["CUSTOMER_NAME"].GetValue<string>();
                                    p.FleetId = fleetId;
                                });
                    }
                    dr.Close();
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

        public void DMLFleetVehicleWithList(UserInfo user, FleetVehicleViewModel model, List<FleetVehicleViewModel> filter)
        {

            bool isSuccess = true;
            CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("P_DML_FLEET_VEHICLE_WITH_EXCEL");

            CreateConnection(cmd);
            DbTransaction tran = connection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                foreach (var submodel in filter)
                {
                    cmd.Parameters.Clear();
                    db.AddInParameter(cmd, "FLEET_ID", DbType.Int32, submodel.FleetId);
                    db.AddInParameter(cmd, "CUSTOMER_ID", DbType.Int32, submodel.CustomerId);
                    db.AddInParameter(cmd, "VEHICLE_ID", DbType.Int32, submodel.VehicleId);
                    db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                    db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                    db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                    db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                    db.ExecuteNonQuery(cmd, tran);
                    submodel.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();

                    if (submodel.ErrorNo > 0)
                    {
                        isSuccess = false;
                        submodel.ErrorMessage = ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").ToString());
                    }
                }

            }
            catch (Exception Ex)
            {
                isSuccess = false;
                model.ErrorNo = 1;
                model.ErrorMessage = Ex.Message;
            }
            finally
            {
                if (isSuccess)
                {
                    tran.Commit();
                }
                else
                {
                    tran.Rollback();
                }
                CloseConnection();
            }

        }

        public void DMLFleetVehicle(UserInfo user, FleetVehicleViewModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_DML_FLEET_VEHICLE");
                db.AddParameter(cmd, "FLEET_VEHICLE_ID", DbType.Int32, ParameterDirection.InputOutput, "FLEET_VEHICLE_ID", DataRowVersion.Default, MakeDbNull(model.FleetVehicleId));
                db.AddInParameter(cmd, "FLEET_ID", DbType.Int32, MakeDbNull(model.FleetId));
                db.AddInParameter(cmd, "ID_VEHICLE", DbType.Int32, MakeDbNull(model.VehicleId));
                db.AddInParameter(cmd, "ID_CUSTOMER", DbType.Int64, MakeDbNull(model.CustomerId));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
                }

                else
                    model.FleetVehicleId = db.GetParameterValue(cmd, "FLEET_VEHICLE_ID").GetValue<int>();
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
