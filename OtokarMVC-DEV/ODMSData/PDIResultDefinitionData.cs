using ODMSModel.PDIResultDefinition;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using System.Web.Mvc;
using ODMSCommon.Security;

namespace ODMSData
{
    public class PDIResultDefinitionData : DataAccessBase
    {
        public List<PDIResultDefinitionListModel> ListPDIResultDefinition(UserInfo user, PDIResultDefinitionListModel filter, out int totalCount)
        {
            List<PDIResultDefinitionListModel> listPDIResultDefinition = new List<PDIResultDefinitionListModel>();

            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_PDI_RESULT_DEFINITION");
                db.AddInParameter(cmd, "PDI_RESULT_CODE", DbType.String,
                    filter.PDIResultCode);
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, filter.AdminDesc);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, filter.IsActive);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        PDIResultDefinitionListModel model = new PDIResultDefinitionListModel
                        {
                            PDIResultCode = dr["PDI_RESULT_CODE"].GetValue<string>(),
                            AdminDesc = dr["ADMIN_DESC"].GetValue<string>(),
                            IsActive = dr["IS_ACTIVE"].GetValue<Boolean>(),
                            IsActiveName = dr["IS_ACTIVE_NAME"].GetValue<string>()
                        };

                        listPDIResultDefinition.Add(model);
                    }
                    dr.Close();
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

            return listPDIResultDefinition;
        }

        public void DMLPDIResultDefinition(UserInfo user, PDIResultDefinitionViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_PDI_RESULT_DEFINITION");

                db.AddInParameter(cmd, "PDI_RESULT_CODE", DbType.String, model.PDIResultCode);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, model.IsActive);
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, model.AdminDesc);
                db.AddInParameter(cmd, "ML_CONTENT", DbType.String, model.MultiLanguageContentAsText);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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

        public void GetPDIResultDefinition(UserInfo user, PDIResultDefinitionViewModel filter)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_PDI_RESULT_DEFINITION");

                db.AddInParameter(cmd, "PDI_RESULT_CODE", DbType.String, filter.PDIResultCode);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        filter.PDIResultCode = dr["PDI_RESULT_CODE"].GetValue<string>();
                        filter.IsActive = dr["IS_ACTIVE"].GetValue<bool>();
                        filter.IsActiveName = dr["IS_ACTIVE_NAME"].GetValue<string>();
                        filter.AdminDesc = dr["ADMIN_DESC"].GetValue<string>();
                    }
                    if (dr.NextResult())
                    {
                        var table = new DataTable();
                        table.Load(dr);
                        filter.ResultNameML.MultiLanguageContentAsText = filter.MultiLanguageContentAsText
                                                                          = GetLanguageContentFromDataSet(table, "RESULT_NAME");
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

        public List<SelectListItem> ListPDIResultCodeAsSelectListItem()
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PDI_RESULT_DEFINITION_COMBO");
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["PDI_RESULT_CODE"].GetValue<string>(),
                            Text = reader["PDI_RESULT_CODE"].GetValue<string>()
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
