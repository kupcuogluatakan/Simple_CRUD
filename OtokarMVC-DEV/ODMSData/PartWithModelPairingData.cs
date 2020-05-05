using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.SparePartClassCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ODMSData
{
    public class PartWithModelPairingData : DataAccessBase
    {
        public List<SparePartClassCodeListModel> ListCodes(UserInfo user)
        {
            var result = new List<SparePartClassCodeListModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SPARE_PART_CLASS_CODE");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var roleListModel = new SparePartClassCodeListModel
                        {
                            Code = reader["SPARE_PART_CLASS_CODE"].ToString(),
                            Desc = reader["SPARE_PART_CLASS_CODE"].ToString() + " " + reader["ADMIN_DESC"].ToString(),
                            IsJanpol = reader["IS_JANPOL"].GetValue<bool>()
                        };
                        result.Add(roleListModel);
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
        public List<SelectListItem> ListCodesCombo(UserInfo user)
        {
            var result = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SPARE_PART_CLASS_CODE");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var roleListModel = new SelectListItem
                        {
                            Value = reader["SPARE_PART_CLASS_CODE"].ToString(),
                            Text = reader["SPARE_PART_CLASS_CODE"].ToString() + " / " + reader["ADMIN_DESC"].ToString()
                        };
                        result.Add(roleListModel);
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

        public List<VehicleListModel> ListIncludedCode(UserInfo user, string code)
        {
            var result = new List<VehicleListModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_INCLUDED_CODE");
                db.AddInParameter(cmd, "CODE", DbType.String, code);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new VehicleListModel
                        {
                            VehicleSSID = reader["MODEL_SSID"].ToString(),
                            VehicleModel = reader["MODEL_NAME"].ToString()
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
        public List<VehicleListModel> ListNotIncludedCode(UserInfo user, string code)
        {
            var result = new List<VehicleListModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_NOT_INCLUDED_CODE");
                db.AddInParameter(cmd, "CODE", DbType.String, code);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new VehicleListModel
                        {
                            VehicleSSID = reader["MODEL_SSID"].ToString(),
                            VehicleModel = reader["MODEL_NAME"].ToString()
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
        public void Save(UserInfo user, SaveModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_SAVE_PAIRING_VEHICLE_PART_CLASS");
                db.AddInParameter(cmd, "CODE", DbType.String, model.Code);
                db.AddInParameter(cmd, "VEHICLE_IDS", DbType.String, model.SerializedVehicleIds);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
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
        public void JanpolRegister(UserInfo user,string classCode,bool IsJanpol)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_SAVE_JANPOL_FOR_PART_CLASS_CODE");
                db.AddInParameter(cmd, "CODE", DbType.String, classCode);
                db.AddInParameter(cmd, "IS_JANPOL", DbType.Boolean, IsJanpol);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));               
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();          
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
