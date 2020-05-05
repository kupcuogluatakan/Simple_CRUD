using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using ODMSCommon;
using ODMSModel.PdiGifApproveGroup;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class PdiGifApproveGroupData : DataAccessBase
    {
        public List<PdiGifApproveGroupListModel> ListPdiGifApproveGroups(UserInfo user, PdiGifApproveGroupListModel filter, out int totalCnt)
        {
            var result = new List<PdiGifApproveGroupListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PDI_GOS_APPROVE_GROUP");
                db.AddInParameter(cmd, "GROUP_NAME", DbType.String, MakeDbNull(filter.GroupName));
                db.AddInParameter(cmd, "MAIL_LIST", DbType.String, MakeDbNull(filter.MailList));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, MakeDbNull(filter.SearchIsActive));
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
                        var listModel = new PdiGifApproveGroupListModel
                        {
                            GroupId = reader["ID_GROUP"].GetValue<int>(),
                            GroupName = reader["GROUP_NAME"].GetValue<string>(),
                            MailList = reader["MAIL_LIST"].GetValue<string>(),
                            IsActiveString = reader["IS_ACTIVE_STRING"].GetValue<string>(),
                        };
                        result.Add(listModel);
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

            return result;
        }

        public void DMLPdiGifApproveGroup(UserInfo user, PdiGifApproveGroupViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_PDI_GOS_APPROVE_GROUP");
                db.AddParameter(cmd, "ID_GROUP", DbType.Int32, ParameterDirection.InputOutput, "ID_GROUP", DataRowVersion.Default, model.GroupId);
                db.AddInParameter(cmd, "GROUP_NAME", DbType.String, MakeDbNull(model.GroupName));
                db.AddInParameter(cmd, "MAIL_LIST", DbType.String, MakeDbNull(model.MailList));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, model.IsActive);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.GroupId = db.GetParameterValue(cmd, "ID_GROUP").GetValue<int>();
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

        public PdiGifApproveGroupViewModel GetPdiGifApproveGroup(UserInfo user, int id)
        {
            var result = new PdiGifApproveGroupViewModel();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_PDI_GOS_APPROVE_GROUP");
                db.AddInParameter(cmd, "ID_GROUP", DbType.Int32, MakeDbNull(id));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        result.GroupId = id;
                        result.GroupName = reader["GROUP_NAME"].GetValue<string>();
                        result.MailList = reader["MAIL_LIST"].GetValue<string>();
                        result.IsActive = reader["IS_ACTIVE"].GetValue<bool>();
                        result.IsActiveString = reader["IS_ACTIVE_STRING"].GetValue<string>();
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
        public List<SelectListItem> ListPdiGifApproveGroupsAsSelectItem(UserInfo user)
        {
            List<SelectListItem> listMainItems = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PDI_GOS_APPROVE_GROUP");
                db.AddInParameter(cmd, "GROUP_NAME", DbType.String, MakeDbNull(null));
                db.AddInParameter(cmd, "MAIL_LIST", DbType.String, MakeDbNull(null));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, MakeDbNull(true));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(null));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(null));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, 0);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(null));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var item = new SelectListItem
                        {
                            Text = dr["GROUP_NAME"].GetValue<string>(),
                            Value = dr["ID_GROUP"].GetValue<string>()
                        };

                        listMainItems.Add(item);
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
            return listMainItems;
        }
    }
}
