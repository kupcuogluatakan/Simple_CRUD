using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using ODMSCommon;
using ODMSModel.VehicleNoteApprove;
using ODMSCommon.Security;

namespace ODMSData
{
    public class VehicleNoteApproveData : DataAccessBase
    {
        public List<VehicleNoteApproveListModel> ListVehicleNoteApprove(UserInfo user, VehicleNoteApproveListModel filter, out int totalCnt)
        {
            var retVal = new List<VehicleNoteApproveListModel>();
            totalCnt = 0;

            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_LIST_VEHICLE_NOTES");
                db.AddInParameter(cmd, "IS_APPROVE_DATE", DbType.Int32, false);
                db.AddInParameter(cmd, "ID_VEHICLE", DbType.Int32, MakeDbNull(0));
                db.AddInParameter(cmd, "ID_VEHICLE_NOTE", DbType.Int32, MakeDbNull(filter.VehicleNotesId));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(filter.DealerId));
                db.AddInParameter(cmd, "NOTE", DbType.String, MakeDbNull(filter.Note));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, MakeDbNull(0));
                db.AddInParameter(cmd, "VIN_NO", DbType.String, MakeDbNull(filter.VinId));
                db.AddInParameter(cmd, "APPROVE_DATE", DbType.DateTime, MakeDbNull(0));
                db.AddInParameter(cmd, "CREATE_DATE", DbType.DateTime, MakeDbNull(filter.CreateDate));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, MakeDbNull(filter.Offset));
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var vehicleNoteApproveListModel = new VehicleNoteApproveListModel
                        {
                            VehicleNotesId = reader["ID_VEHICLE_NOTE"].GetValue<int>(),
                            DealerName = string.IsNullOrEmpty(reader["DEALER_NAME"].GetValue<string>()) ? "Otokar" : reader["DEALER_NAME"].GetValue<string>(),
                            Note = reader["NOTE"].GetValue<string>(),
                            CreateDate = reader["CREATE_DATE"].GetValue<DateTime>(),
                            IsActiveString = reader["IS_ACTIVE_STRING"].GetValue<string>(),
                            VinId = reader["VIN_NO"].GetValue<string>(),
                            WarrantlyStartDate = reader["WARRANTY_START_DATE"].GetValue<DateTime>(),
                            VehicleKm = reader["VEHICLE_KM"].GetValue<decimal>()
                        };


                        retVal.Add(vehicleNoteApproveListModel);
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

            return retVal;

        }
        public List<SelectListItem> ListVehicleNoteApproveAsSelectListItem(UserInfo user)
        {
            List<SelectListItem> retVal = new List<SelectListItem>();


            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_VEHICLE_NOTES");

                db.AddInParameter(cmd, "IS_APPROVE_DATE", DbType.Int32, false);
                db.AddInParameter(cmd, "ID_VEHICLE_NOTE", DbType.Int32, MakeDbNull(0));
                db.AddInParameter(cmd, "ID_VEHICLE", DbType.Int32, MakeDbNull(0));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(0));
                db.AddInParameter(cmd, "NOTE", DbType.String, DBNull.Value);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, 0);
                db.AddInParameter(cmd, "VIN_NO", DbType.String, MakeDbNull(0));
                db.AddInParameter(cmd, "APPROVE_DATE", DbType.DateTime, MakeDbNull(0));
                db.AddInParameter(cmd, "CREATE_DATE", DbType.DateTime, MakeDbNull(0));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, DBNull.Value);
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, DBNull.Value);
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, 0);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, DBNull.Value);
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);


                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        SelectListItem item = new SelectListItem
                        {
                            Value = reader["ID_VEHICLE_NOTE"].GetValue<string>(),
                            Text = reader["NOTE"].GetValue<string>()
                        };
                        retVal.Add(item);
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


        public void GetVehicleNoteApprove(UserInfo user, VehicleNoteApproveModel filter)
        {
            System.Data.Common.DbDataReader dr = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_VEHICLE_NOTES");
                db.AddInParameter(cmd, "ID_VEHICLE_NOTE", DbType.Int32, MakeDbNull(filter.VehicleNotesId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    filter.DealerName = dr["DEALER_NAME"].GetValue<string>();
                    filter.Note = dr["NOTE"].GetValue<string>();
                    filter.CreateDate = dr["CREATE_DATE"].GetValue<DateTime>();
                    filter.IsActive = dr["IS_ACTIVE"].GetValue<bool>();
                    filter.VehicleKm = dr["VEHICLE_KM"].GetValue<decimal>();
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

        public void DeleteVehicleNoteApprove(VehicleNoteApproveModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_DELETE_VEHICLE_NOTE");
                db.AddInParameter(cmd, "ID_VEHICLE_NOTE", DbType.Int32, MakeDbNull(model.VehicleNotesId));

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

        public void ApproveVehicleNote(UserInfo user, VehicleNoteApproveModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_APPROVE_VEHICLE_NOTE");
                db.AddInParameter(cmd, "ID_VEHICLE_NOTE", DbType.Int32, MakeDbNull(model.VehicleNotesId));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, model.IsActive);
                db.AddInParameter(cmd, "NOTE", DbType.String, MakeDbNull(model.Note));
                db.AddInParameter(cmd, "VEHICLE_KM", DbType.Decimal, MakeDbNull(model.VehicleKm));
                db.AddInParameter(cmd, "APPROVE_DATE", DbType.DateTime, MakeDbNull(model.ApproveDate));
                db.AddInParameter(cmd, "APPROVE_USER", DbType.Int32, MakeDbNull(model.ApproveUser));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.VehicleNotesId = db.GetParameterValue(cmd, "ID_VEHICLE_NOTE").GetValue<int>();
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