using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSModel.MaintenanceLabour;
using ODMSModel.Maintenance;
using System.Data.Common;
using ODMSCommon.Security;

namespace ODMSData
{
    public class MaintenanceLabourData : DataAccessBase
    {
        public List<MaintenanceLabourListModel> ListMaintenanceLabours(UserInfo user, MaintenanceLabourListModel filter, out int total)
        {
            var retVal = new List<MaintenanceLabourListModel>();
            total = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_MAINTENANCE_LABOUR");
                db.AddInParameter(cmd, "ID_MAINT", DbType.Int32, MakeDbNull(filter.MaintenanceId));
                db.AddInParameter(cmd, "IS_MUST", DbType.Boolean, MakeDbNull(filter.IsMust));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, MakeDbNull(filter.IsActive));
                db.AddInParameter(cmd, "QUANTITY", DbType.Decimal, MakeDbNull(filter.Quantity));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
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
                        var item = new MaintenanceLabourListModel
                        {
                            Quantity = reader["QUANTITY"] != null ? Decimal.Parse(reader["QUANTITY"].ToString()) : 0,
                            IsMust = reader["IS_MUST"].GetValue<bool>(),
                            IsMustString = reader["IS_MUST_STRING"].GetValue<string>(),
                            LabourId = reader["LABOUR_ID"].GetValue<int>(),
                            LabourCode = reader["LABOUR_CODE"].GetValue<string>(),
                            LabourName = reader["LABOUR_TYPE_DESC"].GetValue<string>(),
                            MaintenanceId = filter.MaintenanceId,
                            MaintenanceName = reader["MAINT_NAME"].GetValue<string>(),
                            IsActive = reader["IS_ACTIVE"].GetValue<bool>(),
                            IsActiveS = reader["IS_ACTIVE_STRING"].GetValue<string>()
                        };
                        retVal.Add(item);
                    }
                    reader.Close();
                }
                total = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
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
        public List<MaintenanceLabourListModel> ListMaintenanceLaboursForExcel(UserInfo user, MaintenanceLabourListModel filter, MaintenanceListModel maintModel, out int total)
        {
            var retVal = new List<MaintenanceLabourListModel>();
            total = 0;
            try
            {


                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_MAINTENANCE_LABOUR");
                db.AddInParameter(cmd, "ID_MAINT", DbType.Int32, MakeDbNull(filter.MaintenanceId));
                db.AddInParameter(cmd, "IS_MUST", DbType.Boolean, MakeDbNull(filter.IsMust));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, MakeDbNull(filter.IsActive));
                db.AddInParameter(cmd, "QUANTITY", DbType.Decimal, MakeDbNull(filter.Quantity));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
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
                        var item = new MaintenanceLabourListModel
                        {
                            VehicleModel = maintModel.VehicleModel,
                            VehicleTypeName = maintModel.VehicleTypeName,
                            EngineType = maintModel.EngineType,
                            MaintTypeName = maintModel.MaintTypeName,
                            MaintKM = maintModel.MaintKM,
                            MaintMonth = maintModel.MaintMonth,
                            MainCategoryName = maintModel.MainCategoryName,
                            CategoryName = maintModel.CategoryName,
                            SubCategoryName = maintModel.SubCategoryName,
                            FailureCode = maintModel.FailureCode,
                            IsActiveS = maintModel.IsActiveS,
                            Quantity = reader["QUANTITY"] != null
                                ? Decimal.Parse(reader["QUANTITY"].ToString())
                                : 0,
                            IsMust = reader["IS_MUST"].GetValue<bool>(),
                            IsMustString = reader["IS_MUST_STRING"].GetValue<string>(),
                            LabourId = reader["LABOUR_ID"].GetValue<int>(),
                            LabourCode = reader["LABOUR_CODE"].GetValue<string>(),
                            LabourName = reader["LABOUR_TYPE_DESC"].GetValue<string>(),
                            MaintenanceId = filter.MaintenanceId,
                            MaintenanceName = reader["MAINT_NAME"].GetValue<string>(),
                        };

                        retVal.Add(item);
                    }
                    reader.Close();
                }
                total = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
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

        public void DMLMaintenanceLabour(UserInfo user, MaintenanceLabourViewModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_DML_MAINTENANCE_LABOUR");
                db.AddInParameter(cmd, "ID_MAINT", DbType.Int32, MakeDbNull(model.MaintenanceId));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, MakeDbNull(model.IsActive));
                db.AddInParameter(cmd, "QUANTITY", DbType.Decimal, MakeDbNull(model.Quantity));
                db.AddInParameter(cmd, "LABOUR_ID", DbType.String, MakeDbNull(model.LabourId));
                db.AddInParameter(cmd, "IS_MUST", DbType.Boolean, MakeDbNull(model.IsMust ?? false));
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

        public MaintenanceLabourViewModel GetMaintenanceLabour(UserInfo user, int maintenanceId, int labourId)
        {
            var retVal = new MaintenanceLabourViewModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_MAINTENANCE_LABOUR");
                db.AddInParameter(cmd, "ID_MAINT", DbType.Int32, MakeDbNull(maintenanceId));
                db.AddInParameter(cmd, "LABOUR_ID", DbType.Int32, MakeDbNull(labourId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new MaintenanceLabourViewModel
                        {
                            Quantity = reader["QUANTITY"].GetValue<decimal>(),
                            IsMust = reader["IS_MUST"].GetValue<bool>(),
                            IsActive = reader["IS_ACTIVE"].GetValue<bool>(),
                            IsMustString = reader["IS_MUST_STRING"].GetValue<string>(),
                            LabourId = labourId,
                            LabourName = reader["LABOUR_TYPE_DESC"].GetValue<string>(),
                            MaintenanceId = maintenanceId,
                            MaintenanceName = reader["MAINT_NAME"].GetValue<string>(),
                        };
                        retVal = item;
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

        public MaintenanceLabourViewModel GetLabourByLabourCode(UserInfo user, MaintenanceLabourViewModel filter)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_MAINTENANCE_LABOUR_BY_LABOUR_CODE");
                db.AddInParameter(cmd, "VALUE", DbType.String, MakeDbNull(filter.LabourCode));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        filter.LabourId = dr["ID_LABOUR"].GetValue<int>();
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

    }
}
