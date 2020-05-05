using ODMSCommon.Security;
using ODMSModel.AnnouncementRole;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using System;

namespace ODMSData
{
    public class AnnouncementRoleData : DataAccessBase
    {
        public List<AnnouncementRoleListModel> ListAnnouncementRole(UserInfo user, AnnouncementRoleListModel filter)
        {
            var retVal = new List<AnnouncementRoleListModel>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_ANNOUNCEMENT_ROLE");
                db.AddInParameter(cmd, "ID_ANNOUNCEMENT", DbType.Int64, filter.IdAnnouncement);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var announcementRoleListModel = new AnnouncementRoleListModel
                        {
                            IdRoleType = reader["ROLE_TYPE_ID"].GetValue<int>(),
                            RoleTypeName = reader["ROLE_TYPE_NAME"].GetValue<string>()
                        };

                        retVal.Add(announcementRoleListModel);
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

        public List<AnnouncementRoleListModel> ListRoleTypeWithoutAnnouncement(UserInfo user, AnnouncementRoleListModel filter)
        {
            var retVal = new List<AnnouncementRoleListModel>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_ROLE_TYPE_WITHOUT_ANNOUNCEMENT");
                db.AddInParameter(cmd, "ID_ANNOUNCEMENT", DbType.Int64, filter.IdAnnouncement);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var announcementRoleListModel = new AnnouncementRoleListModel
                        {
                            IdRoleType = reader["ROLE_TYPE_ID"].GetValue<int>(),
                            RoleTypeName = reader["ROLE_TYPE_NAME"].GetValue<string>()
                        };

                        retVal.Add(announcementRoleListModel);
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

        public void SaveAnnouncementRole(UserInfo user, AnnouncementRoleSaveModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_SAVE_ANNOUNCEMENT_ROLE");
                db.AddInParameter(cmd, "ID_ANNOUNCEMENT", DbType.Int32, model.IdAnnouncement);
                db.AddInParameter(cmd, "ROLE_TYPE_IDS", DbType.String, model.SerializedRoleTypeIds);
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
