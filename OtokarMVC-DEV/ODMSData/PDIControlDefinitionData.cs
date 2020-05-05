using ODMSModel.PDIControlDefinition;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using System.Web.Mvc;
using ODMSCommon.Security;

namespace ODMSData
{
    public class PDIControlDefinitionData : DataAccessBase
    {
        public List<PDIControlDefinitionListModel> ListPDIControlDefinition(UserInfo user, PDIControlDefinitionListModel filter, out int totalCount)
        {
            List<PDIControlDefinitionListModel> listPDIControlDefinition = new List<PDIControlDefinitionListModel>();

            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_PDI_CONTROL_DEFINITION");
                db.AddInParameter(cmd, "PDI_CONTROL_CODE", DbType.String, filter.PDIControlCode);
                db.AddInParameter(cmd, "MODEL_KOD", DbType.String, filter.ModelKod);
                db.AddInParameter(cmd, "IS_GROUP_CODE", DbType.Boolean, filter.IsGroupCode);
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
                        PDIControlDefinitionListModel model = new PDIControlDefinitionListModel
                        {
                            IdPDIControlDefinition = dr["ID_PDI_CONTROL_DEFINITION"].GetValue<int>(),
                            PDIControlCode = dr["PDI_CONTROL_CODE"].GetValue<string>(),
                            PDIControlName = dr["CONTROL_NAME"].GetValue<string>(),
                            _PDIControlCode = dr["CONTROL_NAME"].GetValue<string>(),
                            _PDIControlName = dr["CONTROL_NAME"].GetValue<string>(),
                            ModelKod = dr["MODEL_KOD"].GetValue<string>(),
                            RowNo = dr["ROW_NO"].GetValue<int>(),
                            IsActive = dr["IS_ACTIVE"].GetValue<Boolean>(),
                            IsActiveName = dr["IS_ACTIVE_NAME"].GetValue<string>(),
                            IsGroupCode = dr["IS_GROUP_CODE"].GetValue<Boolean>(),
                            IsGroupCodeName = dr["IS_GROUP_CODE_NAME"].GetValue<string>(),
                            HasActiveControlPartMatch = dr["HAS_ACTIVE_CONTROL_PART_MATCH"].GetValue<bool>()
                        };

                        listPDIControlDefinition.Add(model);
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

            return listPDIControlDefinition;
        }

        public void DMLPDIControlDefinition(UserInfo user, PDIControlDefinitionViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_PDI_CONTROL_DEFINITION");

                db.AddParameter(cmd, "ID_PDI_CONTROL_DEFINITION", DbType.Int32, ParameterDirection.InputOutput, "ID_PDI_CONTROL_DEFINITION", DataRowVersion.Default, model.IdPDIControlDefinition);
                db.AddInParameter(cmd, "PDI_CONTROL_CODE", DbType.String, model.PDIControlCode);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "IS_GROUP_CODE", DbType.Boolean, model.IsGroupCode);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, model.IsActive);
                db.AddInParameter(cmd, "ROW_NO", DbType.Int32, model.RowNo);
                db.AddInParameter(cmd, "MODEL_KOD", DbType.String, model.ModelKod);
                db.AddInParameter(cmd, "ML_CONTENT", DbType.String, model.MultiLanguageContentAsText);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.IdPDIControlDefinition = db.GetParameterValue(cmd, "ID_PDI_CONTROL_DEFINITION").GetValue<int>();
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

        public void GetPDIControlDefinition(UserInfo user, PDIControlDefinitionViewModel filter)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_PDI_CONTROL_DEFINITION");

                db.AddInParameter(cmd, "ID_PDI_CONTROL_DEFINITION", DbType.Int32, MakeDbNull(filter.IdPDIControlDefinition));
                db.AddInParameter(cmd, "PDI_CONTROL_CODE", DbType.String, MakeDbNull(filter.PDIControlCode));
                db.AddInParameter(cmd, "MODEL_KOD", DbType.String, MakeDbNull(filter.ModelKod));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        filter.PDIControlCode = dr["PDI_CONTROL_CODE"].GetValue<string>();
                        filter.IsActive = dr["IS_ACTIVE"].GetValue<bool>();
                        filter.IsActiveName = dr["IS_ACTIVE_NAME"].GetValue<string>();
                        filter.IdPDIControlDefinition = dr["ID_PDI_CONTROL_DEFINITION"].GetValue<int>();
                        filter.ModelKod = dr["MODEL_KOD"].GetValue<string>();
                        filter.RowNo = dr["ROW_NO"].GetValue<int>();
                        filter.IsGroupCode = dr["IS_GROUP_CODE"].GetValue<bool>();
                        filter.IsGroupCodeName = dr["IS_GROUP_CODE_NAME"].GetValue<string>();
                        filter.HasActiveControlPartMatch =
                            dr["HAS_ACTIVE_CONTROL_PART_MATCH"].GetValue<bool>();
                    }
                    if (dr.NextResult())
                    {
                        var table = new DataTable();
                        table.Load(dr);
                        filter.ControlNameML.MultiLanguageContentAsText = filter.MultiLanguageContentAsText
                                                                          = GetLanguageContentFromDataSet(table, "CONTROL_NAME");
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

        public List<SelectListItem> ListPDIControlRowNoAsSelectListItem()
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PDI_CONTROL_DEFINITION_COMBO");
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["ID_PDI_CONTROL_DEFINITION"].GetValue<string>(),
                            Text = reader["ROW_NO"].GetValue<string>()
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
        public List<SelectListItem> ListPDIControlCodeAsSelectListItem()
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PDI_CONTROL_DEFINITION_COMBO");
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["ID_PDI_CONTROL_DEFINITION"].GetValue<string>(),
                            Text = reader["PDI_CONTROL_CODE"].GetValue<string>()
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
        public List<SelectListItem> ListPDIControlModelCodeAsSelectListItem()
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PDI_CONTROL_DEFINITION_COMBO");
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["ID_PDI_CONTROL_DEFINITION"].GetValue<string>(),
                            Text =
                                    reader["PDI_CONTROL_CODE"].GetValue<string>() + CommonValues.MinusWithSpace +
                                    reader["MODEL_KOD"].GetValue<string>()
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
