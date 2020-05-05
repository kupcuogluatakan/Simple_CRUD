using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using ODMSCommon;
using ODMSModel.VehicleType;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class VehicleTypeData : DataAccessBase
    {

        private const string sp_getVehicleTypeList = "P_LIST_VEHICLE_TYPES";
        private const string sp_getVehicleType = "P_GET_VEHICLE_TYPE";
        private const string sp_dmlVehicleType = "P_DML_VEHICLE_TYPE_MAIN";
        private const string sp_getVehicleTypeCombo = "P_LIST_VEHICLE_TYPES_COMBO";

        public List<VehicleTypeListModel> GetVehicleTypeList(UserInfo user, VehicleTypeListModel filter, out int totalCount)
        {
            List<VehicleTypeListModel> list_VehicleT = new List<VehicleTypeListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getVehicleTypeList);

                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, filter.IsActiveSearch);
                db.AddInParameter(cmd, "MODEL_KOD", DbType.String, MakeDbNull(filter.ModelKod));
                db.AddInParameter(cmd, "TYPE_SSID", DbType.String, filter.TypeSSID);
                db.AddInParameter(cmd, "TYPE_ID", DbType.String, MakeDbNull(filter.TypeId));
                db.AddInParameter(cmd, "ID_VEHICLE_GROUP", DbType.String, MakeDbNull(filter.VehicleGroupId));
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
                        var model = new VehicleTypeListModel
                        {
                            TypeId = dr["ID_TYPE"].GetValue<int>(),
                            ModelName = dr["MODEL_NAME"].GetValue<string>(),
                            TypeName = dr["TYPE_NAME"].GetValue<string>(),
                            TypeSSID = dr["TYPE_SSID"].GetValue<string>(),
                            IsActive = dr["IS_ACTIVE"].GetValue<bool>(),
                            IsActiveS = dr["IS_ACTIVE_STRING"].GetValue<string>(),
                            VehicleGroup = dr["VHCL_GRP_NAME"].GetValue<string>()
                        };

                        list_VehicleT.Add(model);
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

            return list_VehicleT;
        }

        public VehicleTypeIndexViewModel GetVehicleType(UserInfo user, VehicleTypeIndexViewModel filter)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getVehicleType);

                db.AddInParameter(cmd, "TYPE_ID", DbType.Int32, filter.TypeId);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        filter.ModelKod = dr["MODEL_KOD"].GetValue<string>();
                        filter.ModelName = dr["MODEL_NAME"].GetValue<string>();
                        filter.TypeName = dr["TYPE_NAME"].GetValue<string>();
                        filter.TypeSSID = dr["TYPE_SSID"].GetValue<string>();
                        filter.IsActive = dr["IS_ACTIVE"].GetValue<bool>();
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
            return filter;
        }

        //TODO : Id set edilmeli
        public void DMLVehicleType(UserInfo user, VehicleTypeIndexViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_dmlVehicleType);
                db.AddInParameter(cmd, "TYPE_ID", DbType.Int32, model.TypeId);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, model.IsActive);
                db.AddInParameter(cmd, "TYPE_SSID", DbType.String, model.TypeSSID);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
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

        public List<SelectListItem> ListVehicleTypeAsSelectList(UserInfo user, string vehicleModel)
        {
            List<SelectListItem> list_SelectItem;
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getVehicleTypeCombo);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "VEHICLE_MODEL", DbType.String, MakeDbNull(vehicleModel));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    list_SelectItem = new List<SelectListItem>();
                    while (dr.Read())
                    {
                        SelectListItem item = new SelectListItem
                        {
                            Value = dr["ID_TYPE"].GetValue<string>(),
                            Text = dr["MODEL_KOD"].GetValue<string>() + " " + dr["TYPE_NAME"].GetValue<string>()
                        };
                        list_SelectItem.Add(item);
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

            return list_SelectItem;
        }

        public List<VehicleTypeXMLViewModel> GetVehicleTypeListForXML()
        {
            var listModel = new List<VehicleTypeXMLViewModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_VEHICLE_TYPE_XML");

                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var model = new VehicleTypeXMLViewModel
                        {
                            ModelSSID = dr["MODEL_KOD"].GetValue<string>(),
                            TypeName = dr["TYPE_NAME"].GetValue<string>(),
                            TypeSSID = dr["TYPE_SSID"].GetValue<string>()
                        };
                        listModel.Add(model);
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
            return listModel;
        }

        public void XMLtoDBVehicleType(List<VehicleTypeXMLViewModel> listModel)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_VEHICLE_TYPE_XML");

                CreateConnection(cmd);
                foreach (var model in listModel)
                {
                    cmd.Parameters.Clear();
                    db.AddInParameter(cmd, "MODEL_SSID", DbType.String, MakeDbNull(model.ModelSSID));
                    db.AddInParameter(cmd, "TYPE_SSID", DbType.String, MakeDbNull(model.TypeSSID));
                    db.AddInParameter(cmd, "TYPE_NAME", DbType.String, MakeDbNull(model.TypeName));
                    db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                    db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                    db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                    cmd.ExecuteNonQuery();

                    model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                    if (model.ErrorNo > 0)
                        model.ErrorMessage = ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>());
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
