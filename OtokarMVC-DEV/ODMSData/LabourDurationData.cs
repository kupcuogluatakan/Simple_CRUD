using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using ODMSCommon;
using ODMSModel.LabourDuration;
using ODMSModel.ListModel;
using ODMSCommon.Resources;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class LabourDurationData : DataAccessBase
    {
        public List<LabourDurationListModel> ListLabourDurations(UserInfo user, LabourDurationListModel filter, out int totalCnt)
        {
            var result = new List<LabourDurationListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_LABOUR_DURATIONS");
                db.AddInParameter(cmd, "ID_LABOUR", DbType.Int64, MakeDbNull(filter.LabourId));
                db.AddInParameter(cmd, "MODEL_KOD", DbType.String, MakeDbNull(filter.VehicleModelId));
                db.AddInParameter(cmd, "ENGINE_TYPE", DbType.String, MakeDbNull(filter.EngineType));
                db.AddInParameter(cmd, "ID_TYPE", DbType.Int32, MakeDbNull(filter.VehicleTypeId));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, MakeDbNull(filter.SearchIsActive));
                db.AddInParameter(cmd, "DURATION", DbType.Double, MakeDbNull(filter.Duration));
                db.AddInParameter(cmd, "VEHICLE_TYPE", DbType.String, MakeDbNull(filter.VehicleTypeName));
                db.AddInParameter(cmd, "VEHICLE_MODEL", DbType.String, MakeDbNull(filter.VehicleModelName));

                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
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
                        var listModel = new LabourDurationListModel
                        {
                            LabourId = reader["ID_LABOUR"].GetValue<int>(),
                            VehicleModelId = reader["MODEL_KOD"].GetValue<string>(),
                            VehicleTypeId = reader["ID_TYPE"].GetValue<int>(),
                            VehicleModelName = reader["MODEL_NAME"].GetValue<string>(),
                            EngineType = reader["ENGINE_TYPE"].GetValue<string>(),
                            VehicleTypeName = reader["TYPE_NAME"].GetValue<string>(),
                            Duration = reader["DURATION"].GetValue<double>(),
                            IsActive = reader["IS_ACTIVE"].GetValue<bool>(),
                            LabourCode = reader["LABOUR_CODE"].GetValue<string>(),
                            LabourName = reader["LABOUR_NAME"].GetValue<string>(),
                            VehicleModelSSID = reader["MODEL_SSID"].GetValue<string>(),
                            VehicleTypeSSID = reader["TYPE_SSID"].GetValue<string>(),
                            EngineTypeID = reader["ENGINE_TYPE_ID"].GetValue<string>(),
                        };
                        result.Add(listModel);
                    }
                    reader.Close();
                }
                totalCnt = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
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

        public LabourDurationDetailModel GetLabourDuration(UserInfo user, LabourDurationDetailModel filter)
        {
            DbDataReader reader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_LABOUR_DURATION");
                db.AddInParameter(cmd, "ID_LABOUR", DbType.Int32, MakeDbNull(filter.LabourId));
                db.AddInParameter(cmd, "MODEL_KOD", DbType.String, MakeDbNull(filter.VehicleModelId));
                db.AddInParameter(cmd, "ID_TYPE", DbType.Int32, MakeDbNull(filter.VehicleTypeId));
                db.AddInParameter(cmd, "ENGINE_TYPE", DbType.String, MakeDbNull(filter.EngineType));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    filter.LabourId = reader["ID_LABOUR"].GetValue<int>();
                    filter.LabourCode = reader["LABOUR_CODE"].GetValue<string>();
                    filter.LabourName = reader["LABOUR_TYPE_DESC"].GetValue<string>();
                    filter.VehicleModelId = reader["MODEL_KOD"].GetValue<string>();
                    filter.VehicleModelName = reader["MODEL_NAME"].GetValue<string>();
                    filter.VehicleTypeId = reader["ID_TYPE"].GetValue<int>();
                    filter.EngineType = reader["ENGINE_TYPE"].GetValue<string>();
                    filter.VehicleTypeName = reader["TYPE_NAME"].GetValue<string>();
                    filter.Duration = int.Parse(reader["DURATION"].GetValue<double>().ToString());
                    filter.IsActive = reader["IS_ACTIVE"].GetValue<bool>();
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
            return filter;
        }

        //TODO : Id set edilmeli
        public void DMLLabourDuration(UserInfo user, LabourDurationDetailModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_DML_LABOUR_DURATION");
                db.AddInParameter(cmd, "ID_LABOUR", DbType.Int64, model.LabourId);
                db.AddInParameter(cmd, "MODEL_KOD", DbType.String, model.VehicleModelId);
                db.AddInParameter(cmd, "ID_TYPE", DbType.Int32, model.VehicleTypeId);
                db.AddInParameter(cmd, "ENGINE_TYPE", DbType.String, model.EngineType);
                db.AddInParameter(cmd, "DURATION", DbType.Double, MakeDbNull(model.Duration));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, model.IsActive);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 2)
                    model.ErrorMessage = MessageResource.LabourDuration_Error_NullId;
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

        public LabourDurationIndexModel GetLabourDurationIndexModel(UserInfo user)
        {
            return new LabourDurationIndexModel
            {
                LabourList = GetLabourList(user),
                VehicleModelList = GetVehicleModelList(user)
            };
        }

        public List<SelectListItem> GetLabourList(UserInfo user)
        {
            var result = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_LABOUR_SHORT");
                db.AddInParameter(cmd, "LABOUR_TYPE_DESC", DbType.String, MakeDbNull(string.Empty));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, true);
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(null));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(null));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new SelectListItem
                        {
                            Value = reader["ID_LABOUR"].GetValue<string>(),
                            Text = reader["LABOUR_NAME"].GetValue<string>(),
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

        public List<AutocompleteSearchListModel> GetLabourList(UserInfo user, string strSearch)
        {
            var result = new List<AutocompleteSearchListModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_LABOUR_SHORT");
                db.AddInParameter(cmd, "LABOUR_TYPE_DESC", DbType.String, MakeDbNull(strSearch));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, true);
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(null));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(null));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new AutocompleteSearchListModel
                        {
                            Id = reader["ID_LABOUR"].GetValue<int>(),
                            Column1 = reader["LABOUR_NAME"].GetValue<string>(),
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
        public List<SelectListItem> GetVehicleTypeEngineTypeList(UserInfo user, string vehicleModelId, string labourId)
        {
            var result = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_VEHICLE_ENGINE_TYPES_OF_MODEL_COMBO");
                db.AddInParameter(cmd, "LABOUR_ID", DbType.Int32, MakeDbNull(labourId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, MakeDbNull(true));
                db.AddInParameter(cmd, "MODEL_KOD", DbType.String, MakeDbNull(vehicleModelId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new SelectListItem
                        {
                            Value = reader["ID_TYPE"].GetValue<string>() + "$" + reader["ENGINE_TYPE"].GetValue<string>(),
                            Text = reader["TYPE_NAME"].GetValue<string>() + " - " + reader["ENGINE_TYPE"].GetValue<string>(),
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
        public List<SelectListItem> GetVehicleTypeEngineTypeListSearch(UserInfo user, string labourId)
        {
            var result = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_VEHICLE_ENGINE_SEARCH");
                db.AddInParameter(cmd, "LABOUR_ID", DbType.Int32, MakeDbNull(labourId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, MakeDbNull(true));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new SelectListItem
                        {
                            Value = reader["ENGINE_TYPE"].GetValue<string>(),
                            Text = reader["ENGINE_TYPE"].GetValue<string>(),
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
        public List<SelectListItem> GetVehicleTypeList(UserInfo user, string vehicleModelId, string labourId)
        {
            var result = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_VEHICLE_TYPES_OF_MODEL_COMBO");
                db.AddInParameter(cmd, "LABOUR_ID", DbType.Int32, MakeDbNull(labourId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, MakeDbNull(true));
                db.AddInParameter(cmd, "MODEL_KOD", DbType.String, MakeDbNull(vehicleModelId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new SelectListItem
                        {
                            Value = reader["ID_TYPE"].GetValue<string>(),
                            Text = reader["TYPE_NAME"].GetValue<string>(),
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

        public List<SelectListItem> GetVehicleModelList(UserInfo user)
        {
            var result = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_VEHICLE_MODELS_COMBO");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new SelectListItem
                        {
                            Value = reader["MODEL_KOD"].GetValue<string>(),
                            Text = reader["MODEL_NAME"].GetValue<string>(),
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
    }
}
