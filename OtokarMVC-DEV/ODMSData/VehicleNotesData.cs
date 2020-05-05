using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.VehicleNote;
using System;

namespace ODMSData
{
    public class VehicleNotesData : DataAccessBase
    {
        public List<VehicleNotesListModel> ListVehicleNotes(UserInfo user,VehicleNotesListModel referenceListModel, out int totalCount)
        {
            var retVal = new List<VehicleNotesListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_VEHICLE_NOTES");
                db.AddInParameter(cmd, "IS_APPROVE_DATE", DbType.Int32, true);
                db.AddInParameter(cmd, "ID_VEHICLE_NOTE", DbType.Int32, MakeDbNull(referenceListModel.VehicleNotesId));
                db.AddInParameter(cmd, "ID_VEHICLE", DbType.Int32, MakeDbNull(referenceListModel.VehicleId));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(0));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "NOTE", DbType.String, MakeDbNull(referenceListModel.Note));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, MakeDbNull(referenceListModel.SearchIsActive));
                db.AddInParameter(cmd, "VIN_NO", DbType.String, MakeDbNull(0));
                db.AddInParameter(cmd, "APPROVE_DATE", DbType.DateTime, MakeDbNull(referenceListModel.ApproveDate));
                db.AddInParameter(cmd, "CREATE_DATE", DbType.DateTime, MakeDbNull(0));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(referenceListModel.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(referenceListModel.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, referenceListModel.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(referenceListModel.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var vehicleNotesListModel = new VehicleNotesListModel
                        {
                            VehicleNotesId = reader["ID_VEHICLE_NOTE"].GetValue<int>(),
                            DealerName = string.IsNullOrEmpty(reader["DEALER_NAME"].GetValue<string>()) ? "Otokar" : reader["DEALER_NAME"].GetValue<string>(),
                            Note = reader["NOTE"].GetValue<string>(),
                            IsActiveName = reader["IS_ACTIVE_STRING"].GetValue<string>(),
                            VehicleKm = reader["VEHICLE_KM"].GetValue<decimal>(),
                        };
                        retVal.Add(vehicleNotesListModel);
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
        public void GetVehicleNotes(UserInfo user,VehicleNotesModel vehicleNotesModel)
        {
            System.Data.Common.DbDataReader dr = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_VEHICLE_NOTES");
                db.AddInParameter(cmd, "ID_VEHICLE_NOTE", DbType.Int32, MakeDbNull(vehicleNotesModel.VehicleNotesId));

                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    vehicleNotesModel.DealerName = dr["DEALER_NAME"].GetValue<string>();
                    vehicleNotesModel.Note = dr["NOTE"].GetValue<string>();
                    vehicleNotesModel.IsActive = dr["IS_ACTIVE"].GetValue<bool>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                    dr.Dispose();
                }
                CloseConnection();
            }

        }

        //TODO : Id set edilmeli
        public void DMLVehicleNotes(UserInfo user,VehicleNotesModel vehicleNotesModel)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_DML_VEHICLE_NOTES");
                db.AddInParameter(cmd, "VEHICLE_ID_NOTES", DbType.Int32, vehicleNotesModel.VehicleNotesId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, vehicleNotesModel.CommandType);
                db.AddInParameter(cmd, "ID_VEHICLE", DbType.Int32, MakeDbNull(vehicleNotesModel.VehicleId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(vehicleNotesModel.DealerId));
                db.AddInParameter(cmd, "NOTE", DbType.String, MakeDbNull(vehicleNotesModel.Note));
                db.AddInParameter(cmd, "APPROVE_DATE", DbType.DateTime, MakeDbNull(vehicleNotesModel.ApproveDate));
                db.AddInParameter(cmd, "APPROVE_USER", DbType.Int32, MakeDbNull(vehicleNotesModel.ApproveUser));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, vehicleNotesModel.IsActive);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                vehicleNotesModel.VehicleId = db.GetParameterValue(cmd, "VEHICLE_ID_NOTES").GetValue<int>();
                vehicleNotesModel.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                vehicleNotesModel.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (vehicleNotesModel.ErrorNo > 0)
                    vehicleNotesModel.ErrorMessage = ResolveDatabaseErrorXml(vehicleNotesModel.ErrorMessage);

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
