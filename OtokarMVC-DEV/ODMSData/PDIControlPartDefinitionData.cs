using ODMSModel.PDIControlPartDefinition;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSCommon.Security;

namespace ODMSData
{
    public class PDIControlPartDefinitionData : DataAccessBase
    {
        public List<PDIControlPartDefinitionListModel> ListPDIControlPartDefinition(UserInfo user, PDIControlPartDefinitionListModel filter, out int totalCount)
        {
            List<PDIControlPartDefinitionListModel> listPDIControlPartDefinition = new List<PDIControlPartDefinitionListModel>();

            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_PDI_CONTROL_PART_DEFINITION");
                db.AddInParameter(cmd, "ID_PDI_CONTROL_DEFINITION", DbType.Int32,
                    MakeDbNull(filter.IdPDIControlDefinition));
                db.AddInParameter(cmd, "PDI_PART_CODE", DbType.String, MakeDbNull(filter.PDIPartCode));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, MakeDbNull(filter.IsActive));
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
                        PDIControlPartDefinitionListModel model = new PDIControlPartDefinitionListModel
                        {
                            IdPDIControlDefinition = dr["ID_PDI_CONTROL_DEFINITION"].GetValue<int>(),
                            PDIControlCode = dr["PDI_CONTROL_CODE"].GetValue<string>(),
                            PDIModelCode = dr["MODEL_KOD"].GetValue<string>()
                        };

                        model.PDIControlModelCode = model.PDIControlCode + CommonValues.MinusWithSpace + model.PDIModelCode;
                        model.PDIPartCode = dr["PDI_PART_CODE"].GetValue<string>();
                        model.IsActive = dr["IS_ACTIVE"].GetValue<Boolean>();
                        model.IsActiveName = dr["IS_ACTIVE_NAME"].GetValue<string>();

                        listPDIControlPartDefinition.Add(model);
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

            return listPDIControlPartDefinition;
        }

        public void DMLPDIControlPartDefinition(UserInfo user, PDIControlPartDefinitionViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_PDI_CONTROL_PART_DEFINITION");

                db.AddInParameter(cmd, "ID_PDI_CONTROL_DEFINITION", DbType.Int32, model.IdPDIControlDefinition);
                db.AddInParameter(cmd, "PDI_PART_CODE", DbType.String, model.PDIPartCode);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, model.IsActive);
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

        public void GetPDIControlPartDefinition(UserInfo user, PDIControlPartDefinitionViewModel filter)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_PDI_CONTROL_PART_DEFINITION");

                db.AddInParameter(cmd, "ID_PDI_CONTROL_DEFINITION", DbType.Int32, MakeDbNull(filter.IdPDIControlDefinition));
                db.AddInParameter(cmd, "PDI_PART_CODE", DbType.String, MakeDbNull(filter.PDIPartCode));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        filter.IdPDIControlDefinition = dr["ID_PDI_CONTROL_DEFINITION"].GetValue<int>();
                        filter.IsActive = dr["IS_ACTIVE"].GetValue<bool>();
                        filter.IsActiveName = dr["IS_ACTIVE_NAME"].GetValue<string>();
                        filter.PDIPartCode = dr["PDI_PART_CODE"].GetValue<string>();
                        filter.PDIControlCode = dr["PDI_CONTROL_CODE"].GetValue<string>();
                        filter.PDIModelCode = dr["MODEL_KOD"].GetValue<string>();
                        filter.PDIControlModelCode = filter.PDIControlCode + CommonValues.MinusWithSpace + filter.PDIModelCode;
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
    }
}
