using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSModel.RolePermission;
using ODMSModel.Shared;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    /// <summary>
    /// Data Accessor Class for RolePermission Action Views. Each method simply executes stored procedures
    /// </summary>
    public class RolePermissionData : DataAccessBase
    {
        public List<RoleListModel> ListRoles(UserInfo user)
        {
            var result = new List<RoleListModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_ROLESSHORT");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var roleListModel = new RoleListModel
                        {
                            RoleId = reader["ROLE_TYPE_ID"].GetValue<int>(),
                            RoleTypeName = reader["ROLE_TYPE_NAME"].GetValue<string>()
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

        public List<PermissionListModel> ListPermissionsIncludedInRole(UserInfo user,int roleId)
        {
            var result = new List<PermissionListModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PERMISSIONS_INCLUDED_IN_ROLE");
                db.AddInParameter(cmd, "ROLE_ID", DbType.Int32, roleId);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var permissionListModel = new PermissionListModel
                        {
                            PermissionId = reader["PERMISSION_ID"].GetValue<int>(),
                            PermissionName = reader["PERMISSION_NAME"].GetValue<string>()
                        };
                        result.Add(permissionListModel);
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

        public List<PermissionListModel> ListPermissionsNotIncludedInRole(UserInfo user,int roleId)
        {
            var result = new List<PermissionListModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PERMISSIONS_NOT_INCLUDED_IN_ROLE");
                db.AddInParameter(cmd, "ROLE_ID", DbType.Int32, roleId);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var permissionListModel = new PermissionListModel
                        {
                            PermissionId = reader["PERMISSION_ID"].GetValue<int>(),
                            PermissionName = reader["PERMISSION_NAME"].GetValue<string>()
                        };
                        result.Add(permissionListModel);
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

        public void Save(UserInfo user,SaveModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_SAVE_ROLE_PERMISSION_RELATIONS");
                db.AddInParameter(cmd, "ROLE_TYPE_ID", DbType.Int32, model.RoleId);
                db.AddInParameter(cmd, "PERMISSION_IDS", DbType.String, model.SerializedPermissionIds);
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