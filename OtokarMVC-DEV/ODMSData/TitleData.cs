using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.Title;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ODMSData
{
    public class TitleData : DataAccessBase
    {
        public List<TitleListModel> ListTitle(UserInfo user, TitleListModel filter, out int totalCount)
        {
            var retVal = new List<TitleListModel>();
            System.Data.Common.DbDataReader dbDataReader = null;
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_TITLES");

                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, filter.IsActive);
                db.AddInParameter(cmd, "IS_TECHNICIAN", DbType.Int32, filter.IsTechnician);
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, MakeDbNull(filter.AdminDesc));
                db.AddInParameter(cmd, "TITLE_NAME", DbType.String, MakeDbNull(filter.TitleName));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (dbDataReader = cmd.ExecuteReader())
                {
                    while (dbDataReader.Read())
                    {
                        var roleListModel = new TitleListModel
                        {
                            TitleId = dbDataReader["TITLE_ID"].GetValue<int>(),
                            AdminDesc = dbDataReader["ADMIN_DESC"].GetValue<string>(),
                            TitleName = dbDataReader["TITLE_NAME"].ToString(),
                            IsActive = dbDataReader["IS_ACTIVE"].GetValue<int>(),
                            IsActiveString = dbDataReader["IS_ACTIVE_STRING"].GetValue<string>(),
                            IsTechnician = dbDataReader["IS_TECHNICIAN"].GetValue<int>(),
                            IsTechnicianName = dbDataReader["IS_TECHNICIAN_STRING"].GetValue<string>()
                        };
                        retVal.Add(roleListModel);
                    }
                    dbDataReader.Close();
                }
                totalCount = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbDataReader != null && !dbDataReader.IsClosed)
                    dbDataReader.Close();
                CloseConnection();
            }

            return retVal;
        }
        public List<SelectListItem> ListTitleTypeAsSelectListItem(UserInfo user)
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_TITLES");
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, MakeDbNull(null));
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, MakeDbNull(null));
                db.AddInParameter(cmd, "TITLE_NAME", DbType.String, MakeDbNull(null));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(0));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(0));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, 0);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, DBNull.Value);
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["TITLE_ID"].GetValue<string>(),
                            Text = reader["TITLE_NAME"].GetValue<string>()
                        };
                        retVal.Add(lookupItem);
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


        public void DMLTitle(UserInfo user, TitleIndexViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_TITLE");
                db.AddParameter(cmd, "TITLE_ID", DbType.Int32, ParameterDirection.InputOutput, "TITLE_ID", DataRowVersion.Default, model.TitleId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, MakeDbNull(model.AdminDesc));
                db.AddInParameter(cmd, "STATUS", DbType.Boolean, MakeDbNull(true));
                db.AddInParameter(cmd, "IS_TECHNICIAN", DbType.Boolean, model.IsTechnician);
                db.AddInParameter(cmd, "ML_CONTENT", DbType.String, MakeDbNull(model.MultiLanguageContentAsText));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.TitleId = db.GetParameterValue(cmd, "TITLE_ID").GetValue<int>();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 2)
                    model.ErrorMessage = MessageResource.Title_Error_NullId;
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

        public TitleIndexViewModel GetTitle(UserInfo user, TitleIndexViewModel filter)
        {
            System.Data.Common.DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_TITLE");
                db.AddInParameter(cmd, "TITLE_ID", DbType.Int32, MakeDbNull(filter.TitleId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {

                    filter.AdminDesc = dReader["ADMIN_DESC"].GetValue<string>();
                    filter._Status = dReader["STATUS"].GetValue<bool>();
                    filter.StatusName = dReader["STATUS_NAME"].GetValue<string>();
                    filter.IsTechnician = dReader["IS_TECHNICIAN"].GetValue<int>();
                    filter.IsTechnicianName = dReader["IS_TECHNICIAN_NAME"].GetValue<string>();
                }
                if (dReader.NextResult())
                {
                    var table = new DataTable();
                    table.Load(dReader);
                    filter.TitleName.MultiLanguageContentAsText = filter.MultiLanguageContentAsText = GetLanguageContentFromDataSet(table, "TITLE_NAME");
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

        public List<TitleListModel> ListUserIncludedInTitle(UserInfo user, int roleId)
        {
            var result = new List<TitleListModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_TITLE_INCLUDED_IN_USER");
                db.AddInParameter(cmd, "TITLE_ID", DbType.Int32, roleId);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var permissionListModel = new TitleListModel
                        {
                            TitleId = reader["TITLE_ID"].GetValue<int>(),
                            TitleName = reader["TITLE_NAME"].GetValue<string>()
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

        public List<SelectListItem> ListTitleTypeCombo(UserInfo user)
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_TITLE_COMBO");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["Value"].GetValue<string>(),
                            Text = reader["Text"].GetValue<string>()
                        };
                        retVal.Add(lookupItem);
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
