using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.FleetRequestVehicleApprove;
using ODMSCommon.Security;

namespace ODMSData
{
    public class FleetRequestVehicleApproveData : DataAccessBase
    {
        public FleetRequestApproveViewModel GetFleetRequestData(UserInfo user,int fleetRequestId)
        {
            var model = new FleetRequestApproveViewModel();
            DbDataReader reader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_FLEET_REQUEST_VEHICLE_APPROVE");
                db.AddInParameter(cmd, "ID_FLEET_REQUEST", DbType.String, fleetRequestId);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    model.Description = reader["DESCRIPTION"].GetValue<string>();
                    model.FleetRequestStatus = reader["FLEET_REQUEST_STATUS"].GetValue<string>();
                    model.FleetRequestId = fleetRequestId;
                }
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        var item = new FleetRequestApproveListModel
                        {
                            FleetRequestVehicleId = reader["FLEET_REQUEST_VEHICLE_ID"].GetValue<int>(),
                            FleetRequestId = reader["ID_FLEET_REQUEST"].GetValue<int>(),
                            FleetId = reader["FLEET_ID_ASSIGNED"].GetValue<int>(),
                            FleetName = reader["FLEET_NAME"].GetValue<string>(),
                            VehicleId = reader["ID_VEHICLE"].GetValue<int>(),
                            VehicleName = reader["VEHICLE_NAME"].GetValue<string>(),
                            CustomerId = reader["ID_CUSTOMER"].GetValue<int>(),
                            CutomerName = reader["CUSTOMER_NAME"].GetValue<string>(),
                            DocumentId = reader["DOC_ID"].GetValue<int>(),
                            DocumentName = reader["DOC_NAME"].GetValue<string>()
                        };
                        model.Requests.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                    reader.Dispose();
                }
                CloseConnection();
            }

            return model;
        }

        public void SaveRequests(UserInfo user, List<FleetRequestApproveListModel> list, out int errorNo, out string errorMessage)
        {
            errorNo = 0;
            errorMessage = string.Empty;
            CreateDatabase();
            bool isError = true;
            DbCommand cmd = db.GetStoredProcCommand("P_SAVE_FLEET_REQUEST_VEHICLE");
            CreateConnection(cmd);
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                foreach (var item in list)
                {
                    cmd.Parameters.Clear();
                    db.AddInParameter(cmd, "FLEET_REQUEST_VEHICLE_ID", DbType.String, item.FleetRequestVehicleId);
                    db.AddInParameter(cmd, "ID_FLEET", DbType.Decimal, item.FleetId);
                    db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                    db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                    db.AddOutParameter(cmd, "ERROR_DESC", DbType.String,4000);
                    db.ExecuteNonQuery(cmd, transaction);

                    item.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                    if (item.ErrorNo > 0)
                    {
                        isError = false;
                        item.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();//Her birini ayrı ayrı yansıtmak için çekildi ama şimdilik pasif genel 1 tane msj donuyor.
                        errorMessage = ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").ToString());
                        break;
                    }
                }

                if (!isError)
                {
                    transaction.Rollback();
                    errorNo = 1;
                    //errorMessage = MessageResource.Err_Generic_Unexpected;
                }
                else
                {
                    transaction.Commit();
                }
            }
            catch
            {
                transaction.Rollback();
                errorNo = 1;
                errorMessage = MessageResource.Err_Generic_Unexpected;
            }
            finally
            {
                CloseConnection();
            }

        }

        public string GetVehicleHistoryFleet(int p)
        {
            var rValue = string.Empty;
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_VEHICLE_FLEET_HISTORY");
                db.AddInParameter(cmd,"VEHICLE_ID",DbType.Int32,p);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        rValue += ", " + dr["FLEET_NAME"].GetValue<string>();
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
            return rValue == string.Empty ? string.Empty : rValue.Substring(1);/* kayıt gelmezse hata veriyor */
        }
    }
}
