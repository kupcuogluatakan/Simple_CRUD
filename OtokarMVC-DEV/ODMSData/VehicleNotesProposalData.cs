using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.VehicleNoteProposal;
using System;

namespace ODMSData
{
    public class VehicleNotesProposalData : DataAccessBase

    {
        public List<VehicleNotesProposalListModel> ListVehicleNotesProposal(UserInfo user, VehicleNotesProposalListModel filter, out int totalCount)
        {
            var retVal = new List<VehicleNotesProposalListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_VEHICLE_NOTES_PROPOSAL");
                db.AddInParameter(cmd, "IS_APPROVE_DATE", DbType.Int32, true);
                db.AddInParameter(cmd, "ID_VEHICLE_NOTE_PROPOSAL", DbType.Int32, MakeDbNull(filter.VehicleNotesId));
                db.AddInParameter(cmd, "ID_VEHICLE", DbType.Int32, MakeDbNull(filter.VehicleId));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(0));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "NOTE", DbType.String, MakeDbNull(filter.Note));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, MakeDbNull(filter.SearchIsActive));
                db.AddInParameter(cmd, "VIN_NO", DbType.String, MakeDbNull(0));
                db.AddInParameter(cmd, "APPROVE_DATE", DbType.DateTime, MakeDbNull(filter.ApproveDate));
                db.AddInParameter(cmd, "CREATE_DATE", DbType.DateTime, MakeDbNull(0));
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
                        var vehicleNotesProposalListModel = new VehicleNotesProposalListModel
                        {
                            VehicleNotesId = reader["ID_VEHICLE_NOTE_PROPOSAL"].GetValue<int>(),
                            DealerName = string.IsNullOrEmpty(reader["DEALER_NAME"].GetValue<string>()) ? "Otokar" : reader["DEALER_NAME"].GetValue<string>(),
                            Note = reader["NOTE"].GetValue<string>(),
                            IsActiveName = reader["IS_ACTIVE_STRING"].GetValue<string>(),
                            VehicleKm = reader["VEHICLE_KM"].GetValue<decimal>(),
                        };
                        retVal.Add(vehicleNotesProposalListModel);
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


        public void GetVehicleNotesProposal(UserInfo user, VehicleNotesProposalModel filter)
        {
            System.Data.Common.DbDataReader dr = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_VEHICLE_NOTES_PROPOSAL");
                db.AddInParameter(cmd, "ID_VEHICLE_NOTE_PROPOSAL", DbType.Int32, MakeDbNull(filter.VehicleNotesId));

                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    filter.DealerName = dr["DEALER_NAME"].GetValue<string>();
                    filter.Note = dr["NOTE"].GetValue<string>();
                    filter.IsActive = dr["IS_ACTIVE"].GetValue<bool>();
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

        //TODO : Id set edilemli
        public void DMLVehicleNotesProposal(UserInfo user, VehicleNotesProposalModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_DML_VEHICLE_NOTES_PROPOSAL");
                db.AddInParameter(cmd, "VEHICLE_ID_NOTES_PROPOSAL", DbType.Int32, model.VehicleNotesId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "ID_VEHICLE", DbType.Int32, MakeDbNull(model.VehicleId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(model.DealerId));
                db.AddInParameter(cmd, "NOTE", DbType.String, MakeDbNull(model.Note));
                db.AddInParameter(cmd, "APPROVE_DATE", DbType.DateTime, MakeDbNull(model.ApproveDate));
                db.AddInParameter(cmd, "APPROVE_USER", DbType.Int32, MakeDbNull(model.ApproveUser));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, model.IsActive);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.VehicleId = db.GetParameterValue(cmd, "VEHICLE_ID_NOTES").GetValue<int>();
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
