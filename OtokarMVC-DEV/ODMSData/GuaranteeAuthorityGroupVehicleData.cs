using ODMSModel.GuaranteeAuthorityGroupVehicleModels;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class GuaranteeAuthorityGroupVehicleData : DataAccessBase
    {
        //Left 
        public List<GuaranteeAuthorityGroupVehicleListModel> ListGuaranteeAuthorityGroupVehicleNotInclude(GuaranteeAuthorityGroupVehicleListModel filter)
        {
            var retVal = new List<GuaranteeAuthorityGroupVehicleListModel>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_GUARANTEE_AUTHORITY_GROUP_VEHICLE_NOT_INCLUDE");
                db.AddInParameter(cmd, "ID_GROUP", DbType.Int32, filter.IdGroup);

                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var guaranteeAuthorityGroupVehicleListModel = new GuaranteeAuthorityGroupVehicleListModel
                        {
                            ModelKod = reader["MODEL_KOD"].GetValue<string>(),
                            ModelName = reader["MODEL_NAME"].GetValue<string>()
                        };

                        retVal.Add(guaranteeAuthorityGroupVehicleListModel);
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

            return retVal;
        }

        //Right - selected
        public List<GuaranteeAuthorityGroupVehicleListModel> ListGuaranteeAuthorityGroupVehicle(GuaranteeAuthorityGroupVehicleListModel filter)
        {
            var retVal = new List<GuaranteeAuthorityGroupVehicleListModel>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_GUARANTEE_AUTHORITY_GROUP_VEHICLE");
                db.AddInParameter(cmd, "ID_GROUP", DbType.Int32, filter.IdGroup);

                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var guaranteeAuthorityGroupVehicleListModel = new GuaranteeAuthorityGroupVehicleListModel
                        {
                            ModelKod = reader["MODEL_KOD"].GetValue<string>(),
                            ModelName = reader["MODEL_NAME"].GetValue<string>()
                        };

                        retVal.Add(guaranteeAuthorityGroupVehicleListModel);
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

            return retVal;
        }

        public void SaveGuaranteeAuthorityGroupVehicle(UserInfo user, GuaranteeAuthorityGroupVehicleSaveModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_SAVE_GUARANTEE_AUTO_GR_VEHICLE");//GUARANTEE_AUTHORITY_GROUP_VEHICLE
                db.AddInParameter(cmd, "ID_GROUP", DbType.Int32, model.id);
                db.AddInParameter(cmd, "VEHICLE_IDS", DbType.String, model.SerializedModelKods);
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
    }
}
