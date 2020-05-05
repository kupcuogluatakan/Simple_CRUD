using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.VehicleBodywork;
using ODMSCommon.Resources;

namespace ODMSData
{
    public class VehicleBodyworkData : DataAccessBase
    {
        public List<VehicleBodyworkListModel> ListVehicleBodywork(UserInfo user, VehicleBodyworkListModel filter, out int totalCount)
        {
            var retVal = new List<VehicleBodyworkListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_VEHICLE_BODYWORK");
                db.AddInParameter(cmd, "BODYWORK_CODE", DbType.String, MakeDbNull(filter.BodyworkCode));
                db.AddInParameter(cmd, "VEHICLE_ID", DbType.Int32, MakeDbNull(filter.VehicleId));
                db.AddInParameter(cmd, "CITY_ID", DbType.Int32, MakeDbNull(filter.CityId));
                db.AddInParameter(cmd, "WORK_ORDER_ID", DbType.Int32, MakeDbNull(filter.WorkOrderId));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(filter.DealerId));

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
                        var vehicleBodyworkListModel = new VehicleBodyworkListModel
                        {
                            VehicleBodyworkId = reader["VEHICLE_BODYWORK_ID"].GetValue<Int32>(),
                            BodyworkCode = reader["BODYWORK_CODE"].GetValue<string>(),
                            BodyworkName = reader["BODYWORK_NAME"].GetValue<string>(),
                            CityId = reader["CITY_ID"].GetValue<Int32>(),
                            CityName = reader["CITY_NAME"].GetValue<string>(),
                            CountryId = reader["COUNTRY_ID"].GetValue<Int32>(),
                            CountryName = reader["COUNTRY_NAME"].GetValue<string>(),
                            DealerId = reader["DEALER_ID"].GetValue<Int32>(),
                            DealerName = reader["DEALER_NAME"].GetValue<string>(),
                            Manufacturer = reader["MANUFACTURER"].GetValue<string>(),
                            VehicleId = reader["VEHICLE_ID"].GetValue<Int32>(),
                            VehiclePlate = reader["VEHICLE_PLATE"].GetValue<string>(),
                            VinNo = reader["VIN_NO"].GetValue<string>(),
                            WorkOrderId = reader["WORK_ORDER_ID"].GetValue<Int32>(),
                            WorkOrderName = reader["WORK_ORDER_NAME"].GetValue<string>()
                        };

                        retVal.Add(vehicleBodyworkListModel);
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

        public VehicleBodyworkViewModel GetVehicleBodywork(UserInfo user, VehicleBodyworkViewModel filter)
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_VEHICLE_BODYWORK");
                db.AddInParameter(cmd, "VEHICLE_BODYWORK_ID", DbType.Int32, MakeDbNull(filter.VehicleBodyworkId));

                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.VehicleBodyworkId = dReader["VEHICLE_BODYWORK_ID"].GetValue<Int32>();
                    filter.BodyworkCode = dReader["BODYWORK_CODE"].GetValue<string>();
                    filter.BodyworkName = dReader["BODYWORK_NAME"].GetValue<string>();
                    filter.CityId = dReader["CITY_ID"].GetValue<Int32>();
                    filter.CityName = dReader["CITY_NAME"].GetValue<string>();
                    filter.CountryId = dReader["COUNTRY_ID"].GetValue<Int32>();
                    filter.CountryName = dReader["COUNTRY_NAME"].GetValue<string>();
                    filter.DealerId = dReader["DEALER_ID"].GetValue<Int32>();
                    filter.DealerName = dReader["DEALER_NAME"].GetValue<string>();
                    filter.Manufacturer = dReader["MANUFACTURER"].GetValue<string>();
                    filter.VehicleId = dReader["VEHICLE_ID"].GetValue<Int32>();
                    filter.VehiclePlate = dReader["VEHICLE_PLATE"].GetValue<string>();
                    filter.WorkOrderId = dReader["WORK_ORDER_ID"].GetValue<Int32>();
                    filter.WorkOrderName = dReader["WORK_ORDER_NAME"].GetValue<string>();
                }
                if (dReader.NextResult())
                {
                    var table = new DataTable();
                    table.Load(dReader);
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


            return filter;
        }

        //TODO : Id set edilmeli
        public void DMLVehicleBodywork(UserInfo user, VehicleBodyworkViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_VEHICLE_BODYWORK");
                db.AddInParameter(cmd, "VEHICLE_BODYWORK_ID", DbType.Int32, model.VehicleBodyworkId);
                db.AddInParameter(cmd, "BODYWORK_CODE", DbType.String, model.BodyworkCode);
                db.AddInParameter(cmd, "VEHICLE_ID", DbType.Int32, model.VehicleId);
                db.AddInParameter(cmd, "CITY_ID", DbType.Int32, model.CityId);
                db.AddInParameter(cmd, "WORK_ORDER_ID", DbType.Int32, model.WorkOrderId);
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, model.DealerId);
                db.AddInParameter(cmd, "MANUFACTURER", DbType.String, model.Manufacturer);

                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();

                if (model.ErrorNo == 3)
                    model.ErrorMessage = MessageResource.VehicleBodywork_Error_NullId;
                else if (model.ErrorNo == 2)
                    model.ErrorMessage = MessageResource.VehicleBodywork_Error_RecordUsed;
                else if (model.ErrorNo == 1)
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


    }
}
