using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSModel.VehicleCode;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class VehicleCodeData : DataAccessBase
    {
        private const string sp_getVehicleCodeList = "P_LIST_VEHICLE_CODES";
        private const string sp_getVehicleCode = "P_GET_VEHICLE_CODE";
        private const string sp_dmlVehicleCode = "P_DML_VEHICLE_CODE_MAIN";

        public List<VehicleCodeListModel> GetVehicleCodeList(UserInfo user, VehicleCodeListModel filter, out int totalCount)
        {
            List<VehicleCodeListModel> list_VehicleC;
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getVehicleCodeList);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, filter.IsActiveSearch);
                db.AddInParameter(cmd, "VEHICLE_TYPE_ID", DbType.Int32, MakeDbNull(filter.VehicleTypeId));
                db.AddInParameter(cmd, "ENGINE_TYPE", DbType.String, MakeDbNull(filter.EngineType));
                db.AddInParameter(cmd, "CODE_SSID", DbType.String, MakeDbNull(filter.VehicleCodeSSID));
                db.AddInParameter(cmd, "ID_VEHICLE_GROUP", DbType.Int32, MakeDbNull(filter.VehicleGroupId));
                db.AddInParameter(cmd, "MODEL_KOD", DbType.String, MakeDbNull(filter.ModelName));
                db.AddInParameter(cmd, "VEHICLE_CODE", DbType.String, MakeDbNull(filter.VehicleCode));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);

                using (var dr = cmd.ExecuteReader())
                {
                    list_VehicleC = new List<VehicleCodeListModel>();
                    while (dr.Read())
                    {
                        VehicleCodeListModel model = new VehicleCodeListModel
                        {
                            VehicleCodeKod = dr["V_CODE_KOD"].GetValue<string>(),
                            VehicleCodeName = dr["CODE_NAME"].GetValue<string>(),
                            VehicleCodeSSID = dr["CODE_SSID"].GetValue<string>(),
                            VehicleTypeName = dr["TYPE_NAME"].GetValue<string>(),
                            EngineType = dr["ENGINE_TYPE"].GetValue<string>(),
                            ModelName = dr["MODEL_NAME"].GetValue<string>(),
                            VehicleGroupId = dr["VHCL_GRP_NAME"].GetValue<string>(),
                            IsActive = dr["IS_ACTIVE"].GetValue<bool>(),
                            IsActiveS = dr["IS_ACTIVE_STRING"].GetValue<string>()
                        };

                        list_VehicleC.Add(model);
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

            return list_VehicleC;

        }

        public void GetVehicleCode(UserInfo user, VehicleCodeIndexViewModel filter)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getVehicleCode);
                db.AddInParameter(cmd, "CODE_KOD", DbType.String, MakeDbNull(filter.VehicleCodeKod));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        filter.VehicleCodeSSID = dr["CODE_SSID"].GetValue<string>();
                        filter.VehicleTypeId = dr["ID_TYPE"].GetValue<int>();
                        filter.VehicleTypeName = dr["TYPE_NAME"].GetValue<string>();
                        filter.AdminDesc = dr["ADMIN_DESC"].GetValue<string>();
                        filter.IsActive = dr["IS_ACTIVE"].GetValue<bool>();
                        filter.EngineType = dr["ENGINE_TYPE"].GetValue<string>();
                    }
                    if (dr.NextResult())
                    {
                        var table = new DataTable();
                        table.Load(dr);
                        filter.VehicleCodeName.MultiLanguageContentAsText = filter.MultiLanguageContentAsText = GetLanguageContentFromDataSet(table, "CODE_NAME");

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

        //TODO : Id set edilmeli
        public void DMLVehicleCode(UserInfo user, VehicleCodeIndexViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_dmlVehicleCode);

                db.AddInParameter(cmd, "CODE_KOD", DbType.String, model.VehicleCodeKod);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, model.IsActive);
                db.AddInParameter(cmd, "ENGINE_TYPE", DbType.String, model.EngineType);
                db.AddInParameter(cmd, "CODE_SSID", DbType.String, model.VehicleCodeSSID);
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, model.AdminDesc);
                db.AddInParameter(cmd, "ML_CONTENT", DbType.String, MakeDbNull(model.MultiLanguageContentAsText));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
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
        public List<VehicleCodeXMLViewModel> GetVehicleCodeListForXML()
        {
            var listModel = new List<VehicleCodeXMLViewModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_VEHICLE_CODE_XML");

                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var model = new VehicleCodeXMLViewModel
                        {
                            CodeSSID = dr["CODE_SSID"].GetValue<string>(),
                            AdminDesc = dr["ADMIN_DESC"].GetValue<string>()
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


        public void XMLtoDBVehicleCode(List<VehicleCodeXMLViewModel> listModel)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_VEHICLE_CODE_XML");

                CreateConnection(cmd);
                foreach (var model in listModel)
                {
                    cmd.Parameters.Clear();
                    db.AddInParameter(cmd, "CODE_SSID", DbType.String, MakeDbNull(model.CodeSSID));
                    db.AddInParameter(cmd, "TYPE_SSID", DbType.String, MakeDbNull(model.TypeSSID));
                    db.AddInParameter(cmd, "MODEL_SSID", DbType.String, MakeDbNull(model.ModelSSID));
                    db.AddInParameter(cmd, "ENGINE_TYPE", DbType.String, MakeDbNull(model.EngineType));
                    db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, MakeDbNull(model.AdminDesc));
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

        public void XMLtoDBVehicleCodeLang(List<VehicleCodeLangXMLModel> listLangModel)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_VEHICLE_CODE_LANG_XML");

                CreateConnection(cmd);
                foreach (var model in listLangModel)
                {
                    cmd.Parameters.Clear();
                    db.AddInParameter(cmd, "CODE_SSID", DbType.String, MakeDbNull(model.CodeSSID));
                    db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(model.LanguageCode));
                    db.AddInParameter(cmd, "CODE_NAME", DbType.String, MakeDbNull(model.CodeName));
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
