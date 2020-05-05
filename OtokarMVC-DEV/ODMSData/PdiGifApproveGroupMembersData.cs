using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Mvc;
using ODMSCommon;
using ODMSModel.PdiGifApproveGroupMembers;
using ODMSCommon.Security;

namespace ODMSData
{
    public class PdiGifApproveGroupMembersData : DataAccessBase
    {
        public List<SelectListItem> ListPdiGifApproveGroupMembersIncluded(int groupId)
        {
            var list = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PDI_GOS_APPROVE_GROUP_MEMBERS_INCLUDED");
                db.AddInParameter(cmd, "ID_GROUP", DbType.Int64, groupId);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new SelectListItem
                        {
                            Value = reader["ID_USER"].GetValue<string>(),
                            Text = reader["USER_FULL_NAME"].GetValue<string>()
                        };
                        list.Add(item);
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

        public List<SelectListItem> ListPdiGifApproveGroupMembersExcluded(int groupId)
        {
            var list = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PDI_GOS_APPROVE_GROUP_MEMBERS_EXCLUDED");
                db.AddInParameter(cmd, "ID_GROUP", DbType.Int64, groupId);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new SelectListItem
                        {
                            Value = reader["ID_USER"].GetValue<string>(),
                            Text = reader["USER_FULL_NAME"].GetValue<string>()
                        };
                        list.Add(item);
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

        public void Save(UserInfo user, PdiGifApproveGroupMembersModel model)
        {
            Func<List<int>, string> getSerializedString = c =>
            {
                if (c == null || c.Count == 0) return String.Empty;
                var sb = new StringBuilder();
                c.ForEach(x => sb.Append(x).Append(","));
                var result = sb.ToString();
                return result.Length > 0 ? result.Substring(0, result.Length - 1) : string.Empty;
            };

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_SAVE_PDI_GOS_APPROVE_GROUP_MEMBERS");
                db.AddInParameter(cmd, "ID_GROUP", DbType.Int32, MakeDbNull(model.GroupId));
                db.AddInParameter(cmd, "USER_IDS", DbType.String, MakeDbNull(getSerializedString(model.UserList)));
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
