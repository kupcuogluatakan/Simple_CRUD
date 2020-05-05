using ODMSCommon.Security;
using ODMSModel.BreakdownDefinition;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using System.Web.Mvc;
using System;

namespace ODMSData
{
    public class BreakdownDefinitionData : DataAccessBase
    {
        public List<BreakdownDefinitionListModel> ListBreakdownDefinition(UserInfo user,BreakdownDefinitionListModel filter, out int totalCount)
        {
            var retVal = new List<BreakdownDefinitionListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_BREAKDOWN_DEFINITION");
                db.AddInParameter(cmd, "PDI_BREAKDOWN_CODE", DbType.String, filter.PdiBreakdownCode);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, filter.IsActive);
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, filter.AdminDesc);
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var breakdownDefinitionListModel = new BreakdownDefinitionListModel
                        {
                            PdiBreakdownCode = reader["PDI_BREAKDOWN_CODE"].GetValue<string>(),
                            AdminDesc = reader["ADMIN_DESC"].GetValue<string>(),
                            BreakdownName = reader["BREKDOWN_NAME"].GetValue<string>(),
                            IsActive = reader["IS_ACTIVE"].GetValue<int>()
                        };

                        retVal.Add(breakdownDefinitionListModel);
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

        public void GetBreakdownDefinition(UserInfo user, BreakdownDefinitionViewModel filter)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_BREAKDOWN_DEFINITION");
                db.AddInParameter(cmd, "PDI_BREAKDOWN_CODE", DbType.String, MakeDbNull(filter.PdiBreakdownCode));

                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        filter.AdminDesc = dr["ADMIN_DESC"].GetValue<string>();
                        filter.BreakdownDesc = dr["BREKDOWN_NAME"].GetValue<string>();
                        filter.IsActive = dr["IS_ACTIVE"].GetValue<bool>();
                        filter.IsActiveName = dr["IS_ACTIVE_NAME"].GetValue<string>();
                    }
                    if (dr.NextResult())
                    {
                        var table = new DataTable();
                        table.Load(dr);
                        filter.BreakdownName.MultiLanguageContentAsText = filter.MultiLanguageContentAsText
                                                      = GetLanguageContentFromDataSet(table, "BREKDOWN_NAME");
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
        }

        public void DMLBreakdownDefinition(UserInfo user, BreakdownDefinitionViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_BREAKDOWN_DEFINITION");
                db.AddInParameter(cmd, "PDI_BREAKDOWN_CODE", DbType.String, model.PdiBreakdownCode);
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, model.AdminDesc);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, model.IsActive);
                db.AddInParameter(cmd, "ML_CONTENT", DbType.String, model.MultiLanguageContentAsText);

                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
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

        public List<SelectListItem> ListBreakdownCodeAsSelectListItem()
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PDI_BREAKDOWN_CODE_COMBO");
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["PDI_BREAKDOWN_CODE"].GetValue<string>(),
                            Text = reader["PDI_BREAKDOWN_CODE"].GetValue<string>()
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
