using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using ODMSCommon;
using ODMSModel.Role;
using ODMSModel.Shared;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSData
{
    public class RoleData : DataAccessBase
    {
        public List<SelectListItem> ListRoleTypeAsSelectListItem(UserInfo user,bool? isSystemRole)
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_ROLES");
                db.AddInParameter(cmd, "IS_SYSTEM_ROLE", DbType.Int32, MakeDbNull(isSystemRole));
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, MakeDbNull(null));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, MakeDbNull(null));
                db.AddInParameter(cmd, "ROLE_TYPE_NAME", DbType.String, MakeDbNull(null));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(0));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(0));
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
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["ROLE_TYPE_ID"].GetValue<string>(),
                            Text = reader["ROLE_TYPE_NAME"].GetValue<string>()
                        };
                        retVal.Add(lookupItem);
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

        public List<RoleListModel> ListRoles(UserInfo user,RoleListModel filter, out int totalCount)
        {
            var retVal = new List<RoleListModel>();
            System.Data.Common.DbDataReader dbDataReader = null;
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_ROLES");
                db.AddInParameter(cmd, "IS_SYSTEM_ROLE", DbType.Int32, 1);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, filter.IsActive);
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, MakeDbNull(filter.AdminDesc));
                db.AddInParameter(cmd, "ROLE_TYPE_NAME", DbType.String, MakeDbNull(filter.RoleTypeName));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (dbDataReader = cmd.ExecuteReader())
                {
                    while (dbDataReader.Read())
                    {
                        var roleListModel = new RoleListModel
                        {
                            RoleId = dbDataReader["ROLE_TYPE_ID"].GetValue<int>(),
                            AdminDesc = dbDataReader["ADMIN_DESC"].GetValue<string>(),
                            IsSystemRole = dbDataReader["IS_SYSTEM_ROLE"].GetValue<int?>(),
                            IsSystemRoleName = dbDataReader["IS_SYSTEM_ROLE_NAME"].GetValue<string>(),
                            RoleTypeName = dbDataReader["ROLE_TYPE_NAME"].GetValue<string>(),
                            IsActive = dbDataReader["IS_ACTIVE"].GetValue<int>(),
                            IsActiveString = dbDataReader["IS_ACTIVE_STRING"].GetValue<string>()
                        };
                        retVal.Add(roleListModel);
                    }
                    dbDataReader.Close();
                }
                totalCount = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbDataReader != null && !dbDataReader.IsClosed)
                    dbDataReader.Close();
                CloseConnection();
            }

            return retVal;
        }

        public void DMLRole(UserInfo user,RoleIndexViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_ROLE_TYPE_MAIN");
                db.AddParameter(cmd, "ROLE_TYPE_ID", DbType.Int32, ParameterDirection.InputOutput, "ROLE_TYPE_ID", DataRowVersion.Default, model.RoleId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, MakeDbNull(model.AdminDesc));
                db.AddInParameter(cmd, "IS_SYSTEM_ROLE", DbType.Boolean, true);
                db.AddInParameter(cmd, "STATUS", DbType.Int32, MakeDbNull(model.Status));
                db.AddInParameter(cmd, "ML_CONTENT", DbType.String, MakeDbNull(model.MultiLanguageContentAsText));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.RoleId = db.GetParameterValue(cmd, "ROLE_TYPE_ID").GetValue<int>();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 2)
                    model.ErrorMessage = MessageResource.RoleTypeMain_Error_NullId;
                else if (model.ErrorNo == 1)
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

        public RoleIndexViewModel GetRole(UserInfo user,RoleIndexViewModel filter)
        {
            System.Data.Common.DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_ROLE_TYPE");
                db.AddInParameter(cmd, "ROLE_TYPE_ID", DbType.Int32, MakeDbNull(filter.RoleId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.AdminDesc = dReader["ADMIN_DESC"].GetValue<string>();
                    filter.IsSystemRole = dReader["IS_SYSTEM_ROLE"].GetValue<bool>();
                    filter.IsSystemRoleName = dReader["IS_SYSTEM_ROLE_NAME"].GetValue<string>();
                }
                if (dReader.NextResult())
                {
                    var table = new DataTable();
                    table.Load(dReader);
                    filter.RoleTypeName.MultiLanguageContentAsText = filter.MultiLanguageContentAsText = GetLanguageContentFromDataSet(table, "ROLE_TYPE_NAME");
                }
                dReader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dReader != null && !dReader.IsClosed)
                {
                    dReader.Close();
                    dReader.Dispose();
                }
                CloseConnection();
            }


            return filter;
        }

        public List<SelectListItem> ListRoleTypeCombo(UserInfo user,bool? isSystemRole)
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_ROLE_TYPE_COMBO");
                db.AddInParameter(cmd, "IS_SYSTEM_ROLE", DbType.Int32, MakeDbNull(isSystemRole));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["Value"].GetValue<string>(),
                            Text = reader["Text"].GetValue<string>()
                        };
                        retVal.Add(lookupItem);
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
        public List<SelectListItem> ListRoleTypeComboByUserType(UserInfo user,bool? isSystemRole, bool isTech)
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_ROLE_TYPE_COMBO_USER_TYPE");
                db.AddInParameter(cmd, "IS_SYSTEM_ROLE", DbType.Int32, MakeDbNull(isSystemRole));
                db.AddInParameter(cmd, "IS_TECH", DbType.Int16, MakeDbNull(isTech));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["Value"].GetValue<string>(),
                            Text = reader["Text"].GetValue<string>()
                        };
                        retVal.Add(lookupItem);
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
    }
}
