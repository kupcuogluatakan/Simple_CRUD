using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSModel.MaintenancePart;
using ODMSModel.ViewModel;
using ODMSModel.Maintenance;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class MaintenancePartData : DataAccessBase
    {
        private const string sp_getMaintPartList = "P_LIST_MAINTENANCE_PARTS";
        private const string sp_getMaintPart = "P_GET_MAINTENANCE_PART";
        private const string sp_dmlMaintPart = "P_DML_MAINTENANCE_PART";
        public List<MaintenancePartListModel> GetMaintenancePartList(UserInfo user, MaintenancePartListModel filter, out int totalCount)
        {
            List<MaintenancePartListModel> list_MaintP = new List<MaintenancePartListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getMaintPartList);
                db.AddInParameter(cmd, "MAINT_ID", DbType.Int32, filter.MaintId);
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, filter.PartId);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, filter.IsActiveSearch);
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
                        MaintenancePartListModel model = new MaintenancePartListModel
                        {
                            IsAlernateAllowS = dr["ALTERNATE_ALLOW_STRING"].GetValue<string>(),
                            IsDifBrandAllowS = dr["DIF_BRAND_ALLOW_STRING"].GetValue<string>(),
                            IsMustS = dr["IS_MUST_STRING"].GetValue<string>(),
                            MaintId = dr["ID_MAINT"].GetValue<int>(),
                            MaintName = dr["MAINT_NAME"].GetValue<string>(),
                            PartId = dr["ID_PART"].GetValue<int>(),
                            Unit = dr["UNIT"].GetValue<string>(),
                            PartCode = dr["PART_CODE"].GetValue<string>(),
                            PartName = dr["PART_NAME"].GetValue<string>(),
                            Quantity = dr["QUANTITY"].GetValue<string>(),
                            IsActiveString = dr["IS_ACTIVE_STRING"].GetValue<string>()
                        };

                        list_MaintP.Add(model);
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

            return list_MaintP;
        }
        public List<MaintenancePartListModel> GetMaintenancePartListForExcel(UserInfo user, MaintenancePartListModel filter, MaintenanceListModel maintModel, out int totalCount)
        {
            List<MaintenancePartListModel> list_MaintP = new List<MaintenancePartListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getMaintPartList);
                db.AddInParameter(cmd, "MAINT_ID", DbType.Int32, filter.MaintId);
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, filter.PartId);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, filter.IsActiveSearch);
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
                        MaintenancePartListModel model = new MaintenancePartListModel
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
                            IsAlernateAllowS = dr["ALTERNATE_ALLOW_STRING"].GetValue<string>(),
                            IsDifBrandAllowS = dr["DIF_BRAND_ALLOW_STRING"].GetValue<string>(),
                            IsMustS = dr["IS_MUST_STRING"].GetValue<string>(),
                            MaintId = dr["ID_MAINT"].GetValue<int>(),
                            MaintName = dr["MAINT_NAME"].GetValue<string>(),
                            PartId = dr["ID_PART"].GetValue<int>(),
                            Unit = dr["UNIT"].GetValue<string>(),
                            PartCode = dr["PART_CODE"].GetValue<string>(),
                            PartName = dr["PART_NAME"].GetValue<string>(),
                            Quantity = dr["QUANTITY"].GetValue<string>(),
                            IsActiveString = dr["IS_ACTIVE_STRING"].GetValue<string>()
                        };

                        list_MaintP.Add(model);
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

            return list_MaintP;
        }
        public void GetMaintenancePart(UserInfo user, MaintenancePartViewModel filter)
        {
            AutocompleteSearchViewModel x = new AutocompleteSearchViewModel();

            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_getMaintPart);
                db.AddInParameter(cmd, "MAINT_ID", DbType.Int32, filter.MaintId);
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, filter.PartId);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {

                        filter.IsAlternateAllow = dr["ALTERNATE_ALLOW"].GetValue<bool>();
                        filter.IsDifBrandAllow = dr["DIF_BRAND_ALLOW"].GetValue<bool>();
                        filter.IsMust = dr["IS_MUST"].GetValue<bool>();
                        filter.MaintName = dr["MAINT_NAME"].GetValue<string>();
                        filter.PartName = dr["PART_NAME"].GetValue<string>();
                        filter.Quantity = dr["QUANTITY"].GetValue<decimal>();
                        filter.IsActive = dr["IS_ACTIVE"].GetValue<bool>();
                        x.DefaultValue = dr["ID_PART"].GetValue<string>();
                        x.DefaultText = dr["PART_NAME"].GetValue<string>();
                        filter.PartSearch = x;
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
        }
        public bool CheckMaintId(int maintId)
        {
            bool result = false;

            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("CHECK_MAINTENANCE_FOR_MAINT_ID");
                db.AddInParameter(cmd, "MAINT_ID", DbType.Int32, maintId);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result = dr["RESULT"].GetValue<bool>();
                    }
                    dr.Close();
                }
                return result;
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
        public void DMLMaintenancePart(UserInfo user, MaintenancePartViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand(sp_dmlMaintPart);
                db.AddInParameter(cmd, "MAINT_ID", DbType.Int32, model.MaintId);
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, model.PartId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "IS_MUST", DbType.Int32, model.IsMust);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, model.IsActive);
                db.AddInParameter(cmd, "QUANTITY", DbType.Decimal, model.Quantity);
                db.AddInParameter(cmd, "ALTERNATE_ALLOW", DbType.Int32, model.IsAlternateAllow);
                db.AddOutParameter(cmd, "PART_NAME", DbType.String, 200);
                db.AddInParameter(cmd, "DIF_BRAND_ALLOW", DbType.Int32, model.IsDifBrandAllow);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
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
                else
                {
                    model.PartName = db.GetParameterValue(cmd, "PART_NAME").GetValue<string>();
                    var ac = new AutocompleteSearchViewModel
                    {
                        DefaultText = model.PartName,
                        DefaultValue = model.PartId.ToString()
                    };

                    model.PartSearch = ac;
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
