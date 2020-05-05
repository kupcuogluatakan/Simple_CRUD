using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using ODMSCommon;
using ODMSModel.EducationType;
using ODMSModel.ViewModel;
using ODMSCommon.Resources;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class EducationTypeData : DataAccessBase
    {
        public List<SelectListItem> ListEducationTypeAsSelectList(UserInfo user)
        {
            var listItems = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_EDUCATION_TYPE_COMBO");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var item = new SelectListItem
                        {
                            Value = dr["ID_EDUCATION_TYPE"].GetValue<string>(),
                            Text = dr["EDUCATION_TYPE_NAME"].GetValue<string>()
                        };

                        listItems.Add(item);
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

            return listItems;
        }

        public void DMLEducationType(UserInfo user, EducationTypeDetailModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_EDUCATION_TYPE");
                db.AddInParameter(cmd, "ID_EDUCATION_TYPE", DbType.Int32, MakeDbNull(model.Id));
                db.AddInParameter(cmd, "ML_CONTENT", DbType.String, model.MultiLanguageContentAsText);
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, MakeDbNull(model.Description));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 2)
                    model.ErrorMessage = MessageResource.EducationType_Error_NullId;
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

        public EducationTypeDetailModel GetEducationType(UserInfo user, EducationTypeDetailModel filter)
        {
            DbDataReader reader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_EDUCATION_TYPE");
                db.AddInParameter(cmd, "ID_EDUCATION_TYPE", DbType.Int32, MakeDbNull(filter.Id));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    filter.Id = reader["ID_EDUCATION_TYPE"].GetValue<int>();
                    filter.Name = reader["EDUCATION_TYPE_NAME"].GetValue<string>();
                    filter.Description = reader["ADMIN_DESC"].GetValue<string>();
                }

                if (reader.NextResult())
                {
                    var table = new DataTable();
                    table.Load(reader);
                    filter.MultiLanguageContentAsText = GetLanguageContentFromDataSet(table, "EDUCATION_TYPE_NAME");
                    filter.MultiLanguageName = (MultiLanguageModel)CommonUtility.DeepClone(filter.MultiLanguageName);
                    filter.MultiLanguageName.MultiLanguageContentAsText = filter.MultiLanguageContentAsText;
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

        public List<EducationTypeListModel> ListEducationTypes(UserInfo user, EducationTypeListModel filter, out int totalCnt)
        {
            var result = new List<EducationTypeListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_EDUCATION_TYPE");
                db.AddInParameter(cmd, "EDUCATION_TYPE_NAME", DbType.String, MakeDbNull(filter.Name));
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
                        var listModel = new EducationTypeListModel
                        {
                            Id = reader["ID_EDUCATION_TYPE"].GetValue<int>(),
                            Name = reader["EDUCATION_TYPE_NAME"].GetValue<string>(),
                            Description = reader["ADMIN_DESC"].GetValue<string>()
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
    }
}
