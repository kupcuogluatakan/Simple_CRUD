using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.Common;
using ODMSModel.PrivateMessage;

namespace ODMSData
{
    public class PrivateMessageData:DataAccessBase
    {
        public List<PrivateMessageListModel> ListMessages(UserInfo user,PrivateMessageListModel model, out int total)
        {
            var retVal = new List<PrivateMessageListModel>();
            total = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PRIVATE_MESSAGES");
                db.AddInParameter(cmd, "ID_DMS_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddInParameter(cmd, "TYPE", DbType.String, MakeDbNull(model.Type));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(model.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(model.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, model.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(model.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new PrivateMessageListModel
                        {
                            MessageId = reader["ID_MESSAGE"].GetValue<Int64>(),
                            Message = reader["MESSAGE"].GetValue<string>(),
                            Title = reader["TITLE"].GetValue<string>(),
                            Sender = reader["SENDER"].GetValue<string>(),
                            SenderId = reader["SENDER_ID_DMS_USER"].GetValue<int>(),
                            Reciever = reader["RECIEVER"].GetValue<string>(),
                            RecieverId = reader["RECIEVER_ID_DMS_USER"].GetValue<int>(),
                            CreateDate = reader["CREATE_DATE"].GetValue<DateTime>(),
                            ReadDate = reader["READ_DATE"].GetValue<DateTime?>(),
                            IsUrgent = reader["IS_URGENT"].GetValue<bool>()
                        };
                        retVal.Add(item);
                    }
                    reader.Close();
                }

                total = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
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

        public List<PrivateMessageListModel> GetMessageHistory(UserInfo user,int messageId, int currentPage, out int total)
        {
            var retVal = new List<PrivateMessageListModel>();
            total = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PRIVATE_MESSAGES_WITH_PARENTS");
                db.AddInParameter(cmd, "ID_MESSAGE", DbType.Int64, MakeDbNull(messageId));
                db.AddInParameter(cmd, "CURRENT_PAGE", DbType.Int32, currentPage);                
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, CommonValues.GridPageSize.Short);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, user.UserId);
                db.AddOutParameter(cmd, "TOTAL", DbType.Int32, 10);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new PrivateMessageListModel
                        {
                            MessageId = reader["ID_MESSAGE"].GetValue<Int64>(),
                            Sender = reader["SENDER"].GetValue<String>(),
                            Message = reader["MESSAGE"].GetValue<string>(),
                            SenderId = reader["SENDER_ID_DMS_USER"].GetValue<int>(),
                            CreateDate = reader["CREATE_DATE"].GetValue<DateTime>(),
                            ReadDate = reader["READ_DATE"].GetValue<DateTime?>(),
                            IsUrgent = reader["IS_URGENT"].GetValue<bool>(),
                            RecieverId = reader["PHOTO_DOC_ID"].GetValue<int>()
                        };
                        retVal.Add(item);
                    }
                    reader.Close();
                }

                total = db.GetParameterValue(cmd, "TOTAL").GetValue<int>();
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

        public void SendMessage(UserInfo user,PrivateMessageModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_SEND_PRIVATE_MESSAGE");
                db.AddParameter(cmd, "ID_MESSAGE", DbType.Int64, ParameterDirection.InputOutput, "ID_MESSAGE", DataRowVersion.Default, model.MessageId);
                db.AddInParameter(cmd, "MESSAGE", DbType.String, MakeDbNull(model.Message));
                db.AddInParameter(cmd, "TITLE", DbType.String, MakeDbNull(model.Title));
                db.AddInParameter(cmd, "RECIEVER_ID", DbType.String, MakeDbNull(model.RecieverId));
                db.AddInParameter(cmd, "IS_URGENT", DbType.Boolean, model.IsUrgent);
                db.AddInParameter(cmd, "SENDER_ID", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.MessageId = db.GetParameterValue(cmd, "ID_MESSAGE").GetValue<int>();
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

        public List<ComboBoxModel> ListRecievers(UserInfo user,string searchText)
        {
            var retVal = new List<ComboBoxModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PRIVATE_MESSAGES_RECIEVERS_AUTO_COMPLETE");
                db.AddInParameter(cmd, "ID_SENDER", DbType.Int32, user.UserId);
                db.AddInParameter(cmd, "SEARCH_TEXT", DbType.String, MakeDbNull(searchText));
                
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new ComboBoxModel
                        {
                            Value = reader["ID_DMS_USER"].GetValue<int>(),
                            Text = reader["FULL_NAME"].GetValue<string>()
                        };
                        retVal.Add(item);
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
