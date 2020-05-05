using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.VehicleGroup;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;

namespace ODMSData
{
    public class VehicleGroupData : DataAccessBase
    {
        private const string sp_getVehicleGroupList = "P_LIST_VEHICLE_GROUPS";
        private const string sp_getVehicleGroup = "P_GET_VEHICLE_GROUP";
        private const string sp_dmlVehicleGroup = "P_DML_VEHICLE_GROUP_MAIN";
        private const string sp_getVehicleGroupCombo = "P_LIST_VEHICLE_GROUPS_COMBO";

        public List<VehicleGroupListModel> GetVehicleGroupList(UserInfo user, VehicleGroupListModel filter, out int totalCount)
        { 
            List<VehicleGroupListModel> list_VehicleG = new List<VehicleGroupListModel>();

            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getVehicleGroupList);
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, MakeDbNull(filter.AdminDesc));
                db.AddInParameter(cmd, "VHCL_GRP_NAME", DbType.String, MakeDbNull(filter.VehicleGroupName));
                db.AddInParameter(cmd, "VEHICLE_GROUP_SSID", DbType.String, MakeDbNull(filter.VehicleGroupSSID));
                db.AddInParameter(cmd, "VEHICLE_GROUP_ID", DbType.Int32, MakeDbNull(filter.VehicleGroupId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, filter.IsActiveSearch);
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
                        var model = new VehicleGroupListModel
                            {
                                VehicleGroupId = reader["ID_VEHICLE_GROUP"].GetValue<int>(),
                                IsActiveS = reader["IS_ACTIVE_STRING"].GetValue<string>(),
                                IsActive = reader["IS_ACTIVE"].GetValue<bool>(),
                                AdminDesc = reader["ADMIN_DESC"].GetValue<string>(),
                                VehicleGroupName = reader["VHCL_GRP_NAME"].GetValue<string>()
                            };

                        list_VehicleG.Add(model);
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

            return list_VehicleG;
        }

        public VehicleGroupIndexViewModel GetVehicleGroup(UserInfo user, VehicleGroupIndexViewModel filter)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getVehicleGroup);
                db.AddInParameter(cmd, "VEHICLE_GROUP_ID", DbType.Int32, MakeDbNull(filter.VehicleGroupId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        filter.AdminDesc = dr["ADMIN_DESC"].GetValue<string>();
                        filter.IsActive = dr["IS_ACTIVE"].GetValue<bool>();
                    }
                    if (dr.NextResult())
                    {
                        var table = new DataTable();
                        table.Load(dr);
                        filter.VehicleGroupName.MultiLanguageContentAsText = filter.MultiLanguageContentAsText = GetLanguageContentFromDataSet(table,
                                                                                                          "VHCL_GRP_NAME");
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

            return filter;
        }

        //TODO: Id set edilmeli
        public void DMLVehicleGroup(UserInfo user, VehicleGroupIndexViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_dmlVehicleGroup);
                db.AddInParameter(cmd, "VEHICLE_GROUP_ID", DbType.Int32, MakeDbNull(model.VehicleGroupId));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, model.AdminDesc);
                db.AddInParameter(cmd, "ML_CONTENT", DbType.String, MakeDbNull(model.MultiLanguageContentAsText));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, model.IsActive);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
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

        public List<SelectListItem> ListVehicleGroupAsSelectList(UserInfo user)
        {
            List<SelectListItem> list_VehicleGroup;
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getVehicleGroupCombo);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    list_VehicleGroup = new List<SelectListItem>();
                    while (dr.Read())
                    {
                        SelectListItem item = new SelectListItem
                            {
                                Value = dr["ID_VEHICLE_GROUP"].GetValue<string>(),
                                Text = dr["VHCL_GRP_NAME"].GetValue<string>()
                            };

                        list_VehicleGroup.Add(item);
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
            return list_VehicleGroup;
        }

        public void XMLtoDBVehicleGroup(List<VehicleGroupXMLViewModel> filter)
        {
            try
            {
                var isSuccess = true;
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_VEHICLE_GROUP_XML");

                CreateConnection(cmd);
                foreach (var model in filter)
                {
                    cmd.Parameters.Clear();
                    db.AddInParameter(cmd, "VEHICLE_GROUP_ID", DbType.Int32, MakeDbNull(model.VehicleGroupId));
                    db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(model.LanguageCode));
                    db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                    db.AddInParameter(cmd, "SSID", DbType.String, MakeDbNull(model.VehicleGroupSSID));
                    db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, MakeDbNull(model.AdminDesc));
                    db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                    db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                    cmd.ExecuteNonQuery();

                    model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                    if (model.ErrorNo > 0)
                    {
                        model.ErrorMessage = ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>());
                        isSuccess = false;
                    }
                }
                if (!isSuccess)
                {
                    throw new Exception("XMLtoDBVehicleGroup Method Process Error");
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

        public List<VehicleGroupXMLViewModel> GetVehicleGroupListForXML()
        {
            var listModel = new List<VehicleGroupXMLViewModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_VEHICLE_MODEL_XML");

                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var model = new VehicleGroupXMLViewModel
                        {
                            VehicleGroupId = dr["ID_VEHICLE_GROUP"].GetValue<int>(),
                            AdminDesc = dr["ADMIN_DESC"].GetValue<string>(),
                            VehicleGroupSSID = dr["VEHICLE_GROUP_SSID"].GetValue<string>()
                        };

                        listModel.Add(model);
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
            return listModel;
        }
    }
}