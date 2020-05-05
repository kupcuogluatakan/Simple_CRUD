using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.EducationRequest;

namespace ODMSData
{
    public class EducationRequestData : DataAccessBase
    {
        public EducationRequestIndexModel GetEducationRequestIndexModel(UserInfo user)
        {
            return new EducationRequestIndexModel { EducationList = GetEducationList(user) };
        }

        public void SaveEducationRequest(UserInfo user, EducationRequestDetailModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_SAVE_EDUCATION_REQUEST");
                db.AddInParameter(cmd, "EDUCATION_CODE", DbType.String, model.EducationCode);
                db.AddInParameter(cmd, "WORKER_IDS", DbType.String, model.SerializedWorkerIds);
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

        public void DeleteWorkerFromEducationRequest(EducationRequestDetailModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DELETE_WORKER_FROM_EDUCATION_REQUEST");
                db.AddInParameter(cmd, "ID_EDUCATION_REQUEST", DbType.Int64, MakeDbNull(model.Id));
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

        public EducationRequestDetailModel GetEducationRequest(UserInfo user, EducationRequestDetailModel filter)
        {
            DbDataReader reader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_EDUCATION_REQUEST_WORKER");
                db.AddInParameter(cmd, "ID_EDUCATION_REQUEST", DbType.Int32, MakeDbNull(filter.Id));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    filter.Id = reader["ID_EDUCATION_REQUEST"].GetValue<long>();
                    filter.EducationCode = reader["EDUCATION_CODE"].GetValue<string>();
                    filter.EducationName = reader["EDUCATION_NAME"].GetValue<string>();
                    filter.WorkerId = reader["ID_DMS_USER"].GetValue<int>();
                    filter.WorkerName = reader["WORKER_NAME"].GetValue<string>();
                    filter.CreateDate = reader["CREATE_DATE"].GetValue<DateTime>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                    reader.Dispose();
                }
                CloseConnection();
            }
            return filter;
        }

        public List<EducationRequestListModel> ListEducationRequests(UserInfo user, EducationRequestListModel filter, out int totalCount)
        {
            var result = new List<EducationRequestListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_EDUCATION_REQUEST_WORKER");
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "EDUCATION_CODE", DbType.String, MakeDbNull(filter.EducationCode));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
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
                        var listModel = new EducationRequestListModel
                        {
                            Id = reader["ID_EDUCATION_REQUEST"].GetValue<long>(),
                            EducationCode = reader["EDUCATION_CODE"].GetValue<string>(),
                            EducationName = reader["EDUCATION_NAME"].GetValue<string>(),
                            WorkerId = reader["ID_DMS_USER"].GetValue<int>(),
                            WorkerName = reader["WORKER_NAME"].GetValue<string>(),
                            CreateDate = reader["CREATE_DATE"].GetValue<DateTime>()
                        };
                        result.Add(listModel);
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

            return result;
        }

        public List<SelectListItem> GetEducationList(UserInfo user)
        {
            var result = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_EDUCATION_COMBO");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new SelectListItem
                        {
                            Value = reader["EDUCATION_CODE"].GetValue<string>(),
                            Text = reader["EDUCATION_NAME"].GetValue<string>(),
                        };
                        result.Add(listModel);
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

        public List<SelectListItem> GetWorkerList(UserInfo user)
        {
            var result = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_WORKERS");
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "IS_DEALER", DbType.Int32, user.IsDealer);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new SelectListItem
                        {
                            Value = reader["ID_DMS_USER"].GetValue<string>(),
                            Text = reader["WORKER_NAME"].GetValue<string>(),
                        };
                        result.Add(listModel);
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


    }
}
