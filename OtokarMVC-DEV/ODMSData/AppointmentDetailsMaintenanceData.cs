using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using ODMSCommon;
using ODMSModel.AppointmentDetailsMaintenance;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class AppointmentDetailsMaintenanceData : DataAccessBase
    {
        private const string sp_getAppDetailMaintList = "P_LIST_APPOINTMENT_DETAILS_MAINTENANCE";
        private const string sp_dmlAppIndPartsLabours = "P_DML_APPOINTMENT_INDICATOR_MAINT_PARTS_LABOURS";
        private const string sp_getSparePartNameByAppIndId = "P_GET_SPARE_PART_BY_APPOINTMENT_INDICATOR_ID";
        private const string sp_replaceSparePart = "P_REPLACE_APPOINTMENT_INDICATOR_MAINT_PARTS";
        private const string sp_deleteAppIndMaint = "P_DML_APPOINTMENT_INDICATOR_MAINTS";
        private const string sp_getMainCombo = "P_LIST_APPOINTMENT_MAINTENANCE_COMBO";

        public List<AppointmentDetailsMaintenanceListModel> GetAppDetailMaintList(UserInfo user,AppointmentDetailsMaintenanceListModel model, out int totalCount)
        {
            List<AppointmentDetailsMaintenanceListModel> listModel = new List<AppointmentDetailsMaintenanceListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getAppDetailMaintList);
                db.AddInParameter(cmd, "APPOINTMENT_INDICATOR_ID", DbType.Int32, model.AppIndicId);
                db.AddInParameter(cmd, "MAINT_ID", DbType.Int32, model.MaintId);
                db.AddInParameter(cmd, "OBJ_TYPE", DbType.Int32, model.ObjType);
                if (model.IsRemoved.HasValue)
                    db.AddInParameter(cmd, "IS_REMOVED", DbType.Boolean, model.IsRemoved.Value);
                else
                    db.AddInParameter(cmd, "IS_REMOVED", DbType.Boolean, null);

                AddPagingParametersWithLanguage(user,cmd, model);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        AppointmentDetailsMaintenanceListModel item = new AppointmentDetailsMaintenanceListModel
                        {
                            AppId = model.AppId,
                            Id = dr["ID"].GetValue<int>(),
                            MaintId = dr["ID_MAINT"].GetValue<int>(),
                            AppIndicId = dr["APPOINTMENT_INDICATOR_ID"].GetValue<int>(),
                            IsRemoved = dr["IS_REMOVED"].GetValue<bool>(),
                            Quantity = dr["QUANTITY"].GetValue<int>(),
                            Type = dr["TYPE"].GetValue<string>(),
                            Name = dr["NAME"].GetValue<string>(),
                            ObjType = dr["OBJ_TYPE"].GetValue<int>(),
                            IsMust = dr["IS_MUST"].GetValue<bool>(),
                            LabourPartId = dr["LABOUR_PART_ID"].GetValue<int>(),
                            Price = dr["PRICE"].GetValue<string>()
                        };

                        listModel.Add(item);
                    }
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
            return listModel;
        }

        public void DMLAppIndMainPartsLabours(UserInfo user, AppointmentDetailsMaintenanceViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_dmlAppIndPartsLabours);

                db.AddInParameter(cmd, "LABOUR_PART_ID", DbType.Int32, model.LabourPartId);
                db.AddInParameter(cmd, "OBJ_TYPE", DbType.Int32, model.ObjType);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
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

        public void GetSparePart(UserInfo user, ChangePartViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getSparePartNameByAppIndId);
                db.AddInParameter(cmd, "APPOINTMENT_INDICATOR_CONTENT_ID", DbType.Int32, model.AppIndPartId);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        model.PartId = dr["ID_PART"].GetValue<int>();
                        model.PartName = dr["PART_NAME"].GetValue<string>();
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

        public void ReplaceAppIndicatorPart(UserInfo user, ChangePartViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_replaceSparePart);
                db.AddInParameter(cmd, "APPOINTMENT_INDICATOR_PART_ID", DbType.Int32, model.AppIndPartId);
                db.AddInParameter(cmd, "QUANTITY", DbType.Double, model.Quantity);
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, model.PartId);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
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

        public void DMLAppoinmentIndicatorMaint(UserInfo user, AppointmentDetailsMaintenanceViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_deleteAppIndMaint);

                db.AddInParameter(cmd, "MAINT_ID", DbType.Int32, model.MaintId);
                db.AddInParameter(cmd, "APPOINTMENT_INDICATOR_ID", DbType.Int32, model.AppIndicId);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
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

        public List<SelectListItem> ListAppMaintenanceAsSelectItem(UserInfo user, int AppIndicId)
        {
            List<SelectListItem> listMainItems = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getMainCombo);

                db.AddInParameter(cmd, "APPOINTMENT_INDICATOR_ID", DbType.Int32, AppIndicId);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var item = new SelectListItem
                        {
                            Text = dr["MAINT_NAME"].GetValue<string>(),
                            Value = dr["ID_MAINT"].GetValue<string>()
                        };

                        listMainItems.Add(item);
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
            return listMainItems;
        }

        public void GetMaintIdByAppIndicId(UserInfo user, AppointmentDetailsMaintenanceViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_APPOINTMENT_MAINTENANCE_ID_FROM_INDICATOR");
                db.AddInParameter(cmd, "APPOINTMENT_INDICATOR_ID", DbType.Int32, model.AppIndicId);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        model.MaintId = dr["ID_MAINT"].GetValue<int>();
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
    }
}
