using System.Collections.Generic;
using System.Data;
using System.Linq;
using ODMSCommon;
using ODMSModel.Common;
using ODMSModel.UserRole;
using ODMSCommon.Resources;
using System;
using ODMSModel.RolePermission;
using ODMSCommon.Security;

namespace ODMSData
{
    public class UserRoleData : DataAccessBase
    {

        private List<ComboBoxModel> GetList(UserInfo user, string storeProcedureName, string userId)
        {
            var list = new List<ComboBoxModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand(storeProcedureName);
                db.AddInParameter(cmd, "USER_ID", DbType.Int32, int.Parse(userId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user1 = new ComboBoxModel
                        {
                            Value = reader["ROLE_TYPE_ID"].GetValue<int>(),
                            Text = reader["ROLE_TYPE_NAME"].GetValue<string>()
                        };
                        list.Add(user1);
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
            return list;
        }

        public List<ComboBoxModel> GetUserRolesExcluded(UserInfo user, string userId)
        {
            return GetList(user, "P_LIST_ROLES_EXCLUDED_IN_USER", userId);
        }
        public List<ComboBoxModel> GetUserRolesIncluded(UserInfo user, string userId)
        {
            return GetList(user, "P_LIST_ROLES_INCLUDED_IN_USER", userId);
        }

        public void Save(UserInfo user, SaveModel model)
        {
            if (!model.PermissionIdList.Any())
                return;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_SAVE_USER_ROLE_RELATIONS");
                db.AddInParameter(cmd, "USER_ID", DbType.Int32, model.RoleId);
                db.AddInParameter(cmd, "ROLE_IDS", DbType.String, model.SerializedPermissionIds);
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
        public void DLMUserRole(UserInfo user, UserRoleViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_USER_ROLE");
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "ID_DMS_USER", DbType.String, MakeDbNull(model.UserId));
                db.AddInParameter(cmd, "ROLE_TYPE_ID", DbType.String, MakeDbNull(model.RoleTypeId));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 2)
                    model.ErrorMessage = MessageResource.UserRole_Error_NullId;
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


    }
}
