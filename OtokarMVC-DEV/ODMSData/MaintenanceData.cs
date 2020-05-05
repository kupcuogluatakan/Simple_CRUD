using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSModel.Maintenance;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class MaintenanceData : DataAccessBase
    {
        private const string sp_getMaintList = "P_LIST_MAINTENANCES";
        private const string sp_getMaintListAll = "P_LIST_MAINTENANCES_AND_DETAILS";
        private const string sp_getMaint = "P_GET_MAINTENANCE";
        private const string sp_dmlMaint = "P_DML_MAINTENANCE_MAIN";


        public List<MaintenanceListModel> GetMaintenanceList(UserInfo user, MaintenanceListModel filter, out int totalCount)
        {
            List<MaintenanceListModel> list_MaintModel = new List<MaintenanceListModel>();

            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getMaintList);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, filter.IsActiveSearch);
                db.AddInParameter(cmd, "VEHICLE_TYPE_ID", DbType.Int32, filter.VehicleTypeId);
                db.AddInParameter(cmd, "MAIN_CAT_ID", DbType.Int32, MakeDbNull(filter.MainCategoryId));
                db.AddInParameter(cmd, "CAT_ID", DbType.Int32, MakeDbNull(filter.CategoryId));
                db.AddInParameter(cmd, "MAINT_NAME", DbType.String, MakeDbNull(filter.MaintName));
                db.AddInParameter(cmd, "ENGINE_TYPE", DbType.String, MakeDbNull(filter.EngineType));
                db.AddInParameter(cmd, "SUB_CAT_ID", DbType.Int32, MakeDbNull(filter.SubCategoryId));
                db.AddInParameter(cmd, "MAINT_TYPE_ID", DbType.String, filter.MaintTypeId);
                db.AddInParameter(cmd, "FAILURE_CODE_ID", DbType.Int32, filter.FailureCodeId);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
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
                        MaintenanceListModel model = new MaintenanceListModel
                        {
                            IsActiveS = dr["IS_ACTIVE_STRING"].GetValue<string>(),
                            MaintId = dr["ID_MAINT"].GetValue<int>(),
                            MaintKM = dr["MAINT_KM"].GetValue<string>(),
                            MaintMonth = dr["MAINT_MONTH"].GetValue<string>(),
                            MaintName = dr["MAINT_NAME"].GetValue<string>(),
                            VehicleTypeName = dr["TYPE_NAME"].GetValue<string>(),
                            EngineType = dr["ENGINE_TYPE"].GetValue<string>(),
                            VehicleModel = dr["MODEL_KOD"].GetValue<string>(),
                            MaintTypeName = dr["MAINT_TYPE_NAME"].GetValue<string>(),
                            SubCategoryId = dr["APPOINTMENT_INDICATOR_SUB_CATEGORY_ID"].GetValue<int>(),
                            CategoryId = dr["APPOINTMENT_INDICATOR_CATEGORY_ID"].GetValue<int>(),
                            MainCategoryId = dr["APPOINTMENT_INDICATOR_MAIN_CATEGORY_ID"].GetValue<int>(),
                            MainCategoryName = dr["MAIN_CATEGORY_NAME"].GetValue<string>(),
                            CategoryName = dr["CATEGORY_NAME"].GetValue<string>(),
                            SubCategoryName = dr["SUB_CATEGORY_NAME"].GetValue<string>(),
                            FailureCode = dr["FAILURE_CODE"].GetValue<string>()
                        };

                        list_MaintModel.Add(model);
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
            return list_MaintModel;
        }
        public List<MaintenanceListModel> GetMaintenanceListAll(UserInfo user, MaintenanceListModel filter, out int totalCount)
        {
            List<MaintenanceListModel> list_MaintModel = new List<MaintenanceListModel>();

            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_MAINTENANCES_ALL");
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, filter.IsActiveSearch);
                db.AddInParameter(cmd, "VEHICLE_TYPE_ID", DbType.Int32, filter.VehicleTypeId);
                db.AddInParameter(cmd, "MAIN_CAT_ID", DbType.Int32, MakeDbNull(filter.MainCategoryId));
                db.AddInParameter(cmd, "CAT_ID", DbType.Int32, MakeDbNull(filter.CategoryId));
                db.AddInParameter(cmd, "MAINT_NAME", DbType.String, MakeDbNull(filter.MaintName));
                db.AddInParameter(cmd, "ENGINE_TYPE", DbType.String, MakeDbNull(filter.EngineType));
                db.AddInParameter(cmd, "SUB_CAT_ID", DbType.Int32, MakeDbNull(filter.SubCategoryId));
                db.AddInParameter(cmd, "MAINT_TYPE_ID", DbType.String, filter.MaintTypeId);
                db.AddInParameter(cmd, "FAILURE_CODE_ID", DbType.Int32, filter.FailureCodeId);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
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
                        MaintenanceListModel model = new MaintenanceListModel
                        {
                            IsActiveS = dr["IS_ACTIVE_STRING"].GetValue<string>(),
                            MaintId = dr["ID_MAINT"].GetValue<int>(),
                            MaintKM = dr["MAINT_KM"].GetValue<string>(),
                            MaintMonth = dr["MAINT_MONTH"].GetValue<string>(),
                            MaintName = dr["MAINT_NAME"].GetValue<string>(),
                            VehicleTypeName = dr["TYPE_NAME"].GetValue<string>(),
                            EngineType = dr["ENGINE_TYPE"].GetValue<string>(),
                            VehicleModel = dr["MODEL_KOD"].GetValue<string>(),
                            MaintTypeName = dr["MAINT_TYPE_NAME"].GetValue<string>(),
                            SubCategoryId = dr["APPOINTMENT_INDICATOR_SUB_CATEGORY_ID"].GetValue<int>(),
                            CategoryId = dr["APPOINTMENT_INDICATOR_CATEGORY_ID"].GetValue<int>(),
                            MainCategoryId = dr["APPOINTMENT_INDICATOR_MAIN_CATEGORY_ID"].GetValue<int>(),
                            MainCategoryName = dr["MAIN_CATEGORY_NAME"].GetValue<string>(),
                            CategoryName = dr["CATEGORY_NAME"].GetValue<string>(),
                            SubCategoryName = dr["SUB_CATEGORY_NAME"].GetValue<string>(),
                            FailureCode = dr["FAILURE_CODE"].GetValue<string>(),
                            Part_LabourID = dr["PART-LABOUR_ID"].GetValue<string>(),
                            Part_LabourCode = dr["PART-LABOUR_CODE"].GetValue<string>(),
                            IsMustString = dr["IS_MUST_STRING"].GetValue<string>(),
                            Quantity = dr["QUANTITY"].GetValue<decimal>(),
                            Part_LabourName = dr["PART-LABOUR_NAME"].GetValue<string>(),
                            Unit = dr["UNIT"].GetValue<string>(),
                            AlternateAllowString = dr["ALTERNATE_ALLOW_STRING"].GetValue<string>(),
                            DifBrandAllowString = dr["DIF_BRAND_ALLOW_STRING"].GetValue<string>(),
                            Type = dr["TYPE"].GetValue<string>(),
                        };

                        list_MaintModel.Add(model);
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

            return list_MaintModel;
        }

        public MaintenanceListModel GetMaintenanceForMaintId(UserInfo user, MaintenanceListModel filter)
        {
            MaintenanceListModel maintRModel = new MaintenanceListModel();

            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_MAINTENANCES");
                db.AddInParameter(cmd, "MAINT_ID", DbType.Int32, MakeDbNull(filter.MaintId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        maintRModel.IsActiveS = dr["IS_ACTIVE_STRING"].GetValue<string>();
                        maintRModel.MaintId = dr["ID_MAINT"].GetValue<int>();
                        maintRModel.MaintKM = dr["MAINT_KM"].GetValue<string>();
                        maintRModel.MaintMonth = dr["MAINT_MONTH"].GetValue<string>();
                        maintRModel.MaintName = dr["MAINT_NAME"].GetValue<string>();
                        maintRModel.VehicleTypeName = dr["TYPE_NAME"].GetValue<string>();
                        maintRModel.EngineType = dr["ENGINE_TYPE"].GetValue<string>();
                        maintRModel.VehicleModel = dr["MODEL_KOD"].GetValue<string>();
                        maintRModel.MaintTypeName = dr["MAINT_TYPE_NAME"].GetValue<string>();
                        maintRModel.SubCategoryId = dr["APPOINTMENT_INDICATOR_SUB_CATEGORY_ID"].GetValue<int>();
                        maintRModel.CategoryId = dr["APPOINTMENT_INDICATOR_CATEGORY_ID"].GetValue<int>();
                        maintRModel.MainCategoryId = dr["APPOINTMENT_INDICATOR_MAIN_CATEGORY_ID"].GetValue<int>();
                        maintRModel.MainCategoryName = dr["MAIN_CATEGORY_NAME"].GetValue<string>();
                        maintRModel.CategoryName = dr["CATEGORY_NAME"].GetValue<string>();
                        maintRModel.SubCategoryName = dr["SUB_CATEGORY_NAME"].GetValue<string>();
                        maintRModel.FailureCode = dr["FAILURE_CODE"].GetValue<string>();
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
            return maintRModel;
        }
        public MaintenanceListModel GetMaintenanceForPkColumns(MaintenanceListModel filter)
        {
            MaintenanceListModel maintRModel = new MaintenanceListModel();

            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_MAINTENANCES_FOR_PK_COLUMS");
                db.AddInParameter(cmd, "VEHICLE_TYPE_ID", DbType.Int32, MakeDbNull(filter.VehicleTypeId));
                db.AddInParameter(cmd, "ENGINE_TYPE", DbType.String, MakeDbNull(filter.EngineType));
                db.AddInParameter(cmd, "KM", DbType.String, MakeDbNull(filter.MaintKM));
                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        maintRModel.MaintId = dr["ID_MAINT"].GetValue<int>();
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
            return maintRModel;
        }
        public void DMLMaintenance(UserInfo user, MaintenanceViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_dmlMaint);
                db.AddParameter(cmd, "MAINT_ID", DbType.Int32, ParameterDirection.InputOutput, "MAINT_ID", DataRowVersion.Default, model.MaintId);
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, model.AdminDesc);
                db.AddInParameter(cmd, "MAINT_KM", DbType.Int32, model.MaintKM);
                db.AddInParameter(cmd, "MAINT_TYPE_LOOKVAL", DbType.String, model.MaintTypeId);
                db.AddInParameter(cmd, "MAINT_MONTH", DbType.String, model.MaintMonth);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, model.IsActive);
                db.AddInParameter(cmd, "VEHICLE_TYPE_ID", DbType.Int32, model.VehicleTypeId);
                db.AddInParameter(cmd, "ENGINE_TYPE", DbType.String, model.EngineType);
                db.AddInParameter(cmd, "APPOINTMENT_INDICATOR_SUB_CATEGORY_ID", DbType.Int32, model.SubCategoryId);
                db.AddInParameter(cmd, "FAILURE_CODE_ID", DbType.Int32, model.FailureCodeId);
                db.AddInParameter(cmd, "ML_CONTENT", DbType.String, model.MultiLanguageContentAsText);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.MaintId = db.GetParameterValue(cmd, "MAINT_ID").GetValue<int>();
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

        public void GetMaintenance(UserInfo user, MaintenanceViewModel filter)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getMaint);
                db.AddInParameter(cmd, "MAINT_ID", DbType.Int32, filter.MaintId);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        filter.MaintKM = dr["MAINT_KM"].GetValue<int?>();
                        filter.MaintMonth = dr["MAINT_MONTH"].GetValue<int?>();
                        filter.VehicleTypeId = dr["ID_TYPE"].GetValue<int>();
                        filter.VehicleTypeName = dr["TYPE_NAME"].GetValue<string>();
                        filter.EngineType = dr["ENGINE_TYPE"].GetValue<string>();
                        filter.MaintTypeId = dr["MAINT_TYPE_LOOKVAL"].GetValue<string>();
                        filter.AdminDesc = dr["ADMIN_DESC"].GetValue<string>();
                        filter.IsActive = dr["IS_ACTIVE"].GetValue<bool>();
                        filter.MaintTypeName = dr["MAINT_TYPE_NAME"].GetValue<string>();
                        filter.SubCategoryId = dr["APPOINTMENT_INDICATOR_SUB_CATEGORY_ID"].GetValue<int>();
                        filter.CategoryId = dr["APPOINTMENT_INDICATOR_CATEGORY_ID"].GetValue<int>();
                        filter.MainCategoryId = dr["APPOINTMENT_INDICATOR_MAIN_CATEGORY_ID"].GetValue<int>();
                        filter.MainCategoryName = dr["MAIN_CATEGORY_NAME"].GetValue<string>();
                        filter.CategoryName = dr["CATEGORY_NAME"].GetValue<string>();
                        filter.SubCategoryName = dr["SUB_CATEGORY_NAME"].GetValue<string>();
                        filter.FailureCodeId = dr["FAILURE_CODE_ID"].GetValue<int>();
                        filter.FailureCodeName = dr["FAILURE_CODE"].GetValue<string>();
                    }
                    if (dr.NextResult())
                    {
                        var table = new DataTable();
                        table.Load(dr);
                        filter.MaintName.MultiLanguageContentAsText = filter.MultiLanguageContentAsText = GetLanguageContentFromDataSet(table, "MAINT_NAME");
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
