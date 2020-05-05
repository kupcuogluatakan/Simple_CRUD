using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ODMSCommon;
using ODMSModel.Announcement;
using ODMSCommon.Security;

namespace ODMSData
{
    public class ActiveAnnouncementsData:DataAccessBase
    {
        Func<List<int>, string> getRoleIdsString = list =>
        {
            var sb = new StringBuilder();
            foreach (var roleId in list)
            {
                sb.Append(roleId).Append(",");
            }
            var result = sb.ToString();
            return result.Length > 0 ? result.Substring(0, result.Length - 1) : string.Empty;
        };
        public List<AnnouncementListModel> ListActiveAnnouncements(UserInfo user, bool IsSlide)
        {
          
            var retVal = new List<AnnouncementListModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_ACTIVE_ANNOUNCEMENTS");
                db.AddInParameter(cmd, "ROLE_IDS", DbType.String, MakeDbNull(getRoleIdsString(user.Roles)));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.DealerID));
                db.AddInParameter(cmd, "IS_SLIDE", DbType.Boolean, MakeDbNull(IsSlide));

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var announcementListModel = new AnnouncementListModel
                            {
                                AnnouncementId = reader["ANNOUNCEMENT_ID"].GetValue<int>(),
                                DocId = reader["DOC_ID"].GetValue<int>(),
                                DocName = reader["DOC_NAME"].GetValue<string>(),
                                Body = reader["BODY"].GetValue<string>(),
                                EndDate = reader["END_DATE"].GetValue<DateTime>(),
                                IsUrgent = reader["IS_URGENT"].GetValue<int>(),
                                StartDate = reader["START_DATE"].GetValue<DateTime>(),
                                Title = reader["TITLE"].GetValue<string>()
                            };

                        retVal.Add(announcementListModel);
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

        public int GetActiveAnnouncementCount(UserInfo user,out int newMessageCount)
        {
            int retVal;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_ACTIVE_ANNOUNCEMENT_COUNT");
                db.AddInParameter(cmd, "ROLE_IDS", DbType.String, MakeDbNull(getRoleIdsString(user.Roles)));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.DealerID));
                db.AddInParameter(cmd, "ID_DMS_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "COUNT", DbType.Int32, 30);
                db.AddOutParameter(cmd, "NEW_MESSAGE_COUNT", DbType.Int32, 30);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                retVal = db.GetParameterValue(cmd, "COUNT").GetValue<int>();
                newMessageCount =  db.GetParameterValue(cmd, "NEW_MESSAGE_COUNT").GetValue<int>();
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
