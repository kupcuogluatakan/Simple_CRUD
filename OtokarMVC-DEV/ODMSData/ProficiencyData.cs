using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using ODMSCommon;
using ODMSModel.Proficiency;
using ODMSModel.ViewModel;
using ODMSCommon.Resources;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class ProficiencyData : DataAccessBase
    {
        public List<ProficiencyListModel> ListProficiencies(UserInfo user,ProficiencyListModel referenceModel, out int totalCount)
        {
            var result = new List<ProficiencyListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PROFICIENCIES");
                db.AddInParameter(cmd, "PROFICIENCY_CODE", DbType.String, MakeDbNull(referenceModel.ProficiencyCode));
                db.AddInParameter(cmd, "PROFICIENCY_NAME", DbType.String, MakeDbNull(referenceModel.ProficiencyName));
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, MakeDbNull(referenceModel.Description));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(referenceModel.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(referenceModel.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, referenceModel.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(referenceModel.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new ProficiencyListModel
                        {
                            ProficiencyCode = reader["PROFICIENCY_CODE"].GetValue<string>(),
                            ProficiencyName = reader["PROFICIENCY_NAME"].GetValue<string>(),
                            Description = reader["ADMIN_DESC"].GetValue<string>()
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

        public ProficiencyDetailModel GetProficiency(UserInfo user,ProficiencyDetailModel referenceModel)
        {
            System.Data.Common.DbDataReader reader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_PROFICIENCY");
                db.AddInParameter(cmd, "PROFICIENCY_CODE", DbType.String, MakeDbNull(referenceModel.ProficiencyCode));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    referenceModel.ProficiencyCode = reader["PROFICIENCY_CODE"].GetValue<string>();
                    referenceModel.ProficiencyName = reader["PROFICIENCY_NAME"].GetValue<string>();
                    referenceModel.Description = reader["ADMIN_DESC"].GetValue<string>();
                }

                if (reader.NextResult())
                {
                    var table = new DataTable();
                    table.Load(reader);
                    referenceModel.MultiLanguageContentAsText = GetLanguageContentFromDataSet(table, "PROFICIENCY_NAME");
                    referenceModel.MultiLanguageName = (MultiLanguageModel)CommonUtility.DeepClone(referenceModel.MultiLanguageName);
                    referenceModel.MultiLanguageName.MultiLanguageContentAsText = referenceModel.MultiLanguageContentAsText;
                }
                reader.Close();
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
            return referenceModel;
        }

        public void DMLProficiency(UserInfo user,ProficiencyDetailModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_PROFICIENCY");
                db.AddInParameter(cmd, "PROFICIENCY_CODE", DbType.String, MakeDbNull(model.ProficiencyCode));
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
                    model.ErrorMessage = MessageResource.Proficiency_Error_NullId;
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

        public List<ProficiencyListModel> ListProficienciesOfDealer(UserInfo user,ProficiencyListModel model, out int totalCount)
        {
            var result = new List<ProficiencyListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PROFICIENCIES_OF_DEALER");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(model.DealerId));
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
                        var listModel = new ProficiencyListModel
                        {
                            ProficiencyCode = reader["PROFICIENCY_CODE"].GetValue<string>(),
                            ProficiencyName = reader["PROFICIENCY_NAME"].GetValue<string>()
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

        public void DMLProficiencyDealer(UserInfo user,ProficiencyDetailModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_DEALER_PROFICIENCY");
                db.AddInParameter(cmd, "PROFICIENCY_CODE", DbType.String, MakeDbNull(model.ProficiencyCode));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(model.DealerId));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 2)
                    model.ErrorMessage = MessageResource.Proficiency_Error_NullCode;
                else if (model.ErrorNo > 0)
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

        public List<SelectListItem> ListProficiesAsSelectListItem(UserInfo user)
        {
            var result = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PROFICIENCIES_COMBO");
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
                            Selected = false,
                            Value = reader["PROFICIENCY_CODE"].GetValue<string>(),
                            Text = reader["PROFICIENCY_NAME"].GetValue<string>()
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
