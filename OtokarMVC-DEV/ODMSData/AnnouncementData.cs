using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.Announcement;
using ODMSModel.ListModel;

namespace ODMSData
{
    public class AnnouncementData : DataAccessBase
    {
        public List<AnnouncementListModel> ListAnnouncements(UserInfo user,AnnouncementListModel filter, out int totalCount)
        {
            var retVal = new List<AnnouncementListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_ANNOUNCEMENTS");
                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, MakeDbNull(filter.StartDate));
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, MakeDbNull(filter.EndDate));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, filter.IsActive);
                db.AddInParameter(cmd, "IS_URGENT", DbType.Int32, filter.IsUrgent);
                db.AddInParameter(cmd, "PUBLISH_USER", DbType.Int32, MakeDbNull(filter.PublishUser));
                db.AddInParameter(cmd, "PUBLISH_DATE", DbType.DateTime, MakeDbNull(filter.PublishDate));
                AddPagingParametersWithLanguage(user, cmd, filter);
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
                            IsActive = reader["IS_ACTIVE"].GetValue<int>(),
                            IsActiveName = reader["IS_ACTIVE_NAME"].GetValue<string>(),
                            IsUrgent = reader["IS_URGENT"].GetValue<int>(),
                            IsUrgentName = reader["IS_URGENT_NAME"].GetValue<string>(),
                            StartDate = reader["START_DATE"].GetValue<DateTime>(),
                            Title = reader["TITLE"].GetValue<string>(),
                            SendMail = reader["SEND_MAIL"].GetValue<int>(),
                            SendMailName = reader["SEND_MAIL_NAME"].GetValue<string>(),
                            PublishDate = reader["PUBLISH_DATE"].GetValue<DateTime?>(),
                            PublishUser = reader["PUBLISH_USER"].GetValue<int>(),
                            PublishUserName = reader["PUBLISH_USER_NAME"].GetValue<string>(),
                            SumDealer = reader["SUM_ROLE"].GetValue<int>(),
                            SumRole = reader["SUM_DEALER"].GetValue<int>()
                        };

                        retVal.Add(announcementListModel);
                    }
                    reader.Close();
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

            return retVal;
        }

        public void DMLAnnouncement(UserInfo user, AnnouncementViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_ANNOUNCEMENT");
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddParameter(cmd, "ANNOUNCEMENT_ID", DbType.Int64, ParameterDirection.InputOutput, "ANNOUNCEMENT_ID", DataRowVersion.Default, model.AnnouncementId);
                db.AddInParameter(cmd, "TITLE", DbType.String, MakeDbNull(model.Title));
                db.AddInParameter(cmd, "BODY", DbType.String, MakeDbNull(model.Body));
                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, MakeDbNull(model.StartDate));
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, MakeDbNull(model.EndDate));
                db.AddInParameter(cmd, "IS_URGENT", DbType.Int32, model.IsUrgent);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, model.IsActive);
                db.AddInParameter(cmd, "SEND_MAIL", DbType.Int32, model.SendMail);
                db.AddInParameter(cmd, "PUBLISH_USER", DbType.Int32, MakeDbNull(model.PublishUser));
                db.AddInParameter(cmd, "PUBLISH_DATE", DbType.DateTime, MakeDbNull(model.PublishDate));
                db.AddInParameter(cmd, "DOCUMENT_ID", DbType.Int32, MakeDbNull(model.DocId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                db.AddInParameter(cmd, "IS_SLIDE", DbType.Int32, model.IsSlide);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.AnnouncementId = db.GetParameterValue(cmd, "ANNOUNCEMENT_ID").GetValue<int>();
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

        public AnnouncementViewModel GetAnnouncement(UserInfo user, AnnouncementViewModel filter)
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_ANNOUNCEMENT");
                db.AddInParameter(cmd, "ANNOUNCEMENT_ID", DbType.String, MakeDbNull(filter.AnnouncementId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.AnnouncementId = dReader["ANNOUNCEMENT_ID"].GetValue<int>();
                    filter.DocId = dReader["DOC_ID"].GetValue<int>();
                    filter.DocName = dReader["DOC_NAME"].GetValue<string>();
                    filter.Body = dReader["BODY"].GetValue<string>();
                    filter.EndDate = dReader["END_DATE"].GetValue<DateTime>();
                    filter.IsActive = dReader["IS_ACTIVE"].GetValue<int>();
                    filter.IsActiveName = dReader["IS_ACTIVE_NAME"].GetValue<string>();
                    filter.IsUrgent = dReader["IS_URGENT"].GetValue<bool>();
                    filter.IsUrgentName = dReader["IS_URGENT_NAME"].GetValue<string>();
                    filter.StartDate = dReader["START_DATE"].GetValue<DateTime>();
                    filter.Title = dReader["TITLE"].GetValue<string>();
                    filter.SendMail = dReader["SEND_MAIL"].GetValue<bool>();
                    filter.SendMailName = dReader["SEND_MAIL_NAME"].GetValue<string>();
                    filter.PublishUser = dReader["PUBLISH_USER"].GetValue<int>();
                    filter.PublishUserName = dReader["PUBLISH_USER_NAME"].GetValue<string>();
                    filter.PublishDate = dReader["PUBLISH_DATE"].GetValue<DateTime>();
                    filter.IsSlide = dReader["IS_SLIDE"].GetValue<bool>();
                    filter.IsSlideName = dReader["IS_SLIDE_NAME"].GetValue<string>();
                }
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

        public List<UserListModel> ListMailUsers(Int64 annId)
        {
            var listModel = new List<UserListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_ANNOUNCEMENT_MAIL");

                db.AddInParameter(cmd, "ANNOUNCEMENT_ID", DbType.Int64, annId);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var model = new UserListModel()
                        {
                            EMail = dr["EMAIL"].GetValue<string>()
                        };
                        listModel.Add(model);
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
            return listModel;
        }
    }
}
