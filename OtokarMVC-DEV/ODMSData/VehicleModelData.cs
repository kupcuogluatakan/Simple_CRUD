using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using ODMSCommon;
using ODMSModel.VehicleModel;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class VehicleModelData : DataAccessBase
    {
        private const string sp_getVehicleModelList = "P_LIST_VEHICLE_MODELS";
        private const string sp_getVehicleModel = "P_GET_VEHICLE_MODEL";
        private const string sp_dmlVehicleModel = "P_DML_VEHICLE_MODEL_MAIN";
        private const string sp_getVehicleModelCombo = "P_LIST_VEHICLE_MODELS_COMBO";


        public List<VehicleModelListModel> GetVehicleModelList(UserInfo user, VehicleModelListModel filter, out int totalCount)
        {
            List<VehicleModelListModel> list_VehicleM = new List<VehicleModelListModel>();
            totalCount = 0;


            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getVehicleModelList);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, filter.IsActiveSearch);
                db.AddInParameter(cmd, "MODEL_KOD", DbType.String, filter.VehicleModelKod);
                db.AddInParameter(cmd, "MODEL_NAME", DbType.String, filter.VehicleModelName);
                db.AddInParameter(cmd, "MODEL_SSID", DbType.String, filter.VehicleModelSSID);
                db.AddInParameter(cmd, "VEHICLE_GROUP_ID", DbType.Int32, MakeDbNull(filter.VehicleGroupId));
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
                        var model = new VehicleModelListModel
                        {
                            VehicleModelKod = dr["MODEL_KOD"].GetValue<string>(),
                            VehicleGroupName = dr["VHCL_GRP_NAME"].GetValue<string>(),
                            VehicleModelName = dr["MODEL_NAME"].GetValue<string>(),
                            VehicleModelSSID = dr["MODEL_SSID"].GetValue<string>(),
                            IsActive = dr["IS_ACTIVE"].GetValue<bool>(),
                            IsActiveS = dr["IS_ACTIVE_STRING"].GetValue<string>(),
                            IsPdiCheck = dr["IS_PDI_CHECK"].GetValue<string>(),
                            IsCouponCheck = dr["IS_COUPON_CHCK"].GetValue<string>(),
                            IsBodyWorkDetailCheck = dr["IS_BODYWORK_DETAIL_REQUIRED"].GetValue<string>()

                        };

                        list_VehicleM.Add(model);
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

            return list_VehicleM;
        }

        public void GetVehicleModel(UserInfo user, VehicleModelIndexViewModel filter)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getVehicleModel);
                db.AddInParameter(cmd, "MODEL_KOD", DbType.String, filter.VehicleModelKod);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        filter.VehicleGroupName = dr["VHCL_GRP_NAME"].GetValue<string>();
                        filter.VehicleGroupId = dr["ID_VEHICLE_GROUP"].GetValue<int>();
                        filter.VehicleModelName = dr["MODEL_NAME"].GetValue<string>();
                        filter.VehicleModelSSID = dr["MODEL_SSID"].GetValue<string>();
                        filter.IsActive = dr["IS_ACTIVE"].GetValue<bool>();
                        filter.IsCouponCheck = dr["COUPON_CHCK"].GetValue<bool>();
                        filter.IsPDICheck = dr["PDI_CHCK"].GetValue<bool>();
                        filter.IsBodyWorkDetailCheck = dr["BODYWORK_DETAIL_REQUIRED"].GetValue<bool>();
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
        public void DMLVehicleModel(UserInfo user, VehicleModelIndexViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_dmlVehicleModel);
                db.AddInParameter(cmd, "MODEL_KOD", DbType.String, model.VehicleModelKod);
                db.AddInParameter(cmd, "MODEL_SSID", DbType.String, model.VehicleModelSSID);
                db.AddInParameter(cmd, "PDI_CHCK", DbType.Boolean, model.IsPDICheck);
                db.AddInParameter(cmd, "COUPON_CHCK", DbType.Boolean, model.IsCouponCheck);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, model.IsActive);
                db.AddInParameter(cmd, "IS_BODYWORKDETAIL", DbType.Boolean, model.IsBodyWorkDetailCheck);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
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



        public List<SelectListItem> ListVehicleModelAsSelectList(UserInfo user)
        {
            List<SelectListItem> list_VehicleM;

            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getVehicleModelCombo);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    list_VehicleM = new List<SelectListItem>();
                    while (dr.Read())
                    {
                        SelectListItem item = new SelectListItem
                        {
                            Value = dr["MODEL_KOD"].GetValue<string>(),
                            Text = dr["MODEL_NAME"].GetValue<string>()
                        };

                        list_VehicleM.Add(item);

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
            return list_VehicleM;
        }


        public List<SelectListItem> ListVehicleModelAsSelectList(UserInfo user, int vehicleGroupId)
        {
            List<SelectListItem> list_VehicleM;

            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_VEHICLE_MODEL_BY_ID_VEHICLE_GROUP");

                db.AddInParameter(cmd, "ID_VEHICLE_GROUP", DbType.Int32, MakeDbNull(vehicleGroupId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    list_VehicleM = new List<SelectListItem>();
                    while (dr.Read())
                    {
                        SelectListItem item = new SelectListItem
                        {
                            Value = dr["MODEL_KOD"].GetValue<string>(),
                            Text = dr["MODEL_NAME"].GetValue<string>()
                        };

                        list_VehicleM.Add(item);

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
            return list_VehicleM;
        }

        public List<VehicleModelXMLViewModel> GetVehicleModelListForXML()
        {
            var listModel = new List<VehicleModelXMLViewModel>();
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
                        var model = new VehicleModelXMLViewModel
                        {
                            ModelSSID = dr["MODEL_KOD"].GetValue<string>(),
                            ModelName = dr["MODEL_NAME"].GetValue<string>()

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

        public void XMLtoDBVehicleModel(List<VehicleModelXMLViewModel> filter)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_VEHICLE_MODEL_XML");

                CreateConnection(cmd);
                foreach (var model in filter)
                {
                    cmd.Parameters.Clear();
                    db.AddInParameter(cmd, "VEHICLE_GROUP_SSID", DbType.String, MakeDbNull(model.VehicleGroupSSID));
                    db.AddInParameter(cmd, "MODEL_SSID", DbType.String, MakeDbNull(model.ModelSSID));
                    db.AddInParameter(cmd, "MODEL_NAME", DbType.String, MakeDbNull(model.ModelName));
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

