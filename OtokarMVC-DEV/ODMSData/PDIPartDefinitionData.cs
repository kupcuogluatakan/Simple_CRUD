using ODMSCommon.Security;
using ODMSModel.PDIPartDefinition;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using System.Web.Mvc;
using System;

namespace ODMSData
{
    public class PDIPartDefinitionData : DataAccessBase
    {
        public List<PDIPartDefinitionListModel> ListPDIPartDefinition(UserInfo user, PDIPartDefinitionListModel filter, out int totalCount)
        {
            var retVal = new List<PDIPartDefinitionListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PDI_PART_DEFINITION");
                db.AddInParameter(cmd, "PDI_PART_CODE", DbType.String, MakeDbNull(filter.PdiPartCode));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, MakeDbNull(filter.IsActive));
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, MakeDbNull(filter.AdminDesc));

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
                        var pdiPartDefinitionListModel = new PDIPartDefinitionListModel
                        {
                            PdiPartCode = reader["PDI_PART_CODE"].GetValue<string>(),
                            AdminDesc = reader["ADMIN_DESC"].GetValue<string>(),
                            PartName = reader["PART_NAME"].GetValue<string>(),
                            IsActive = reader["IS_ACTIVE"].GetValue<bool>(),
                            HasActiveControlPartMatch = reader["HAS_ACTIVE_CONTROL_PART_MATCH"].GetValue<bool>()
                        };

                        retVal.Add(pdiPartDefinitionListModel);
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

        public void GetPDIPartDefinition(UserInfo user, PDIPartDefinitionViewModel filter)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_PDI_PART_DEFINITION");
                db.AddInParameter(cmd, "PDI_PART_CODE", DbType.String, MakeDbNull(filter.PdiPartCode));

                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        filter.AdminDesc = dr["ADMIN_DESC"].GetValue<string>();
                        filter.PartDesc = dr["PART_NAME"].GetValue<string>();
                        filter.IsActive = dr["IS_ACTIVE"].GetValue<bool>();
                        filter.IsActiveName = dr["IS_ACTIVE_NAME"].GetValue<string>();
                        filter.HasActiveControlPartMatch =
                            dr["HAS_ACTIVE_CONTROL_PART_MATCH"].GetValue<bool>();
                    }
                    if (dr.NextResult())
                    {
                        var table = new DataTable();
                        table.Load(dr);
                        filter.PartName.MultiLanguageContentAsText = filter.MultiLanguageContentAsText
                                                      = GetLanguageContentFromDataSet(table, "PART_NAME");
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
        }

        public void DMLPDIPartDefinition(UserInfo user, PDIPartDefinitionViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_PDI_PART_DEFINITION");
                db.AddInParameter(cmd, "PDI_PART_CODE", DbType.String, model.PdiPartCode);
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

        public List<SelectListItem> ListPdiPartCodeAsSelectListItem()
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PDI_PART_CODE_COMBO");
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["PDI_PART_CODE"].GetValue<string>(),
                            Text = reader["PDI_PART_CODE"].GetValue<string>()
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
