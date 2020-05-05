using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.Campaign;
using ODMSModel.ViewModel;

namespace ODMSData
{
    public class CampaignData : DataAccessBase
    {
        public List<SelectListItem> ListAllCampaignAsSelectListItem()
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_ALL_CAMPAIGN_COMBO");
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["CAMPAIGN_CODE"].GetValue<string>(),
                            Text = reader["CDescription"].GetValue<string>()
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
        public List<SelectListItem> ListCampaignAsSelectListItem(UserInfo user, string modelCode)
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CAMPAIGN_COMBO");
                db.AddInParameter(cmd, "MODEL_KOD", DbType.String, MakeDbNull(modelCode));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                            {
                                Value = reader["CAMPAIGN_CODE"].GetValue<string>(),
                                Text = reader["CAMPAIGN_NAME"].GetValue<string>()
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
        public List<CampaignListModel> ListCampaign(UserInfo user,CampaignListModel filter, out int totalCount)
        {
            var retVal = new List<CampaignListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CAMPAIGN");
                db.AddInParameter(cmd, "CAMPAIGN_CODE", DbType.String, MakeDbNull(filter.CampaignCode));
                db.AddInParameter(cmd, "CAMPAIGN_NAME", DbType.String, MakeDbNull(filter.CampaignName));
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, MakeDbNull(filter.AdminDesc));
                db.AddInParameter(cmd, "MAIN_FAILURE_CODE", DbType.String, MakeDbNull(filter.MainFailureCode));
                db.AddInParameter(cmd, "SUB_CATEGORY_CODE", DbType.String, MakeDbNull(filter.IndicatorCode));
                db.AddInParameter(cmd, "MODEL_KOD", DbType.String, MakeDbNull(filter.ModelKod));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, filter.IsActive);
                db.AddInParameter(cmd, "IS_MUST", DbType.Int32, filter.IsMust);
                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, MakeDbNull(filter.StartDate));
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, MakeDbNull(filter.EndDate));
                db.AddInParameter(cmd, "SUB_CATEGORY_ID", DbType.Int32, MakeDbNull(filter.SubCategoryId));
                AddPagingParametersWithLanguage(user,cmd,filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var campaignListModel = new CampaignListModel
                            {
                                CampaignCode = reader["CAMPAIGN_CODE"].GetValue<string>(),
                                AdminDesc = reader["ADMIN_DESC"].GetValue<string>(),
                                CampaignName = reader["CAMPAIGN_NAME"].GetValue<string>(),
                                EndDate = reader["END_DATE"].GetValue<DateTime>(),
                                IsActive = reader["IS_ACTIVE"].GetValue<int>(),
                                IsActiveName = reader["IS_ACTIVE_NAME"].GetValue<string>(),
                                IsValidAbroad = reader["IS_VALID_ABROAD"].GetValue<int>(),
                                IndicatorCode = reader["MAIN_INDICATOR_CODE"].GetValue<string>(),
                                IsValidAbroadName = reader["IS_VALID_ABROAD_NAME"].GetValue<string>(),
                                ModelKod = reader["MODEL_KOD"].GetValue<string>(),
                                StartDate = reader["START_DATE"].GetValue<DateTime>(),
                                IsMust = reader["IS_MUST"].GetValue<bool>(),
                                IsMustName = reader["IS_MUST_NAME"].GetValue<string>()
                            };
                        retVal.Add(campaignListModel);
                    }
                    reader.Close();
                }
                totalCount = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

                CloseConnection();
            }

            return retVal;
        }

        public CampaignViewModel GetCampaign(UserInfo user, CampaignViewModel filter)
        {
            System.Data.Common.DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_CAMPAIGN");
                db.AddInParameter(cmd, "CAMPAIGN_CODE", DbType.String, MakeDbNull(filter.CampaignCode));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.CampaignCode = dReader["CAMPAIGN_CODE"].GetValue<string>();
                    filter.AdminDesc = dReader["ADMIN_DESC"].GetValue<string>();
                    filter.EndDate = dReader["END_DATE"].GetValue<DateTime>();
                    filter.IsActive = dReader["IS_ACTIVE"].GetValue<bool>();
                    filter.IsActiveName = dReader["IS_ACTIVE_NAME"].GetValue<string>();
                    filter.IsValidAbroad = dReader["IS_VALID_ABROAD"].GetValue<bool?>();
                    filter.IsValidAbroadName = dReader["IS_VALID_ABROAD_NAME"].GetValue<string>();
                    filter.MainFailureCode = dReader["MAIN_FAILURE_CODE"].GetValue<Int32>();
                    filter.MainFailureDesc = dReader["MAIN_FAILURE_DESC"].GetValue<string>();                    
                    filter.ModelKod = dReader["MODEL_KOD"].GetValue<string>();
                    filter.StartDate = dReader["START_DATE"].GetValue<DateTime>();
                    filter.SubCategoryId = dReader["APPOINTMENT_INDICATOR_SUB_CATEGORY_ID"].GetValue<int>();
                    filter.SubCategoryName = dReader["SUB_CATEGORY_NAME"].GetValue<string>();
                    filter.IsMust = dReader["IS_MUST"].GetValue<bool>();
                    filter.IsMustName = dReader["IS_MUST_NAME"].GetValue<string>();
                }
                if (dReader.NextResult())
                {
                    var table = new DataTable();
                    table.Load(dReader);
                    filter.MultiLanguageContentAsText = GetLanguageContentFromDataSet(table, "CAMPAIGN_NAME");
                    filter.CampaignName = (MultiLanguageModel)CommonUtility.DeepClone(filter.CampaignName);
                    filter.CampaignName.MultiLanguageContentAsText = filter.MultiLanguageContentAsText;
                }
                dReader.Close();
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

        public void DMLCampaign(UserInfo user, CampaignViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_CAMPAIGN_MAIN");
                db.AddParameter(cmd, "CAMPAIGN_CODE", DbType.String, ParameterDirection.InputOutput, "CAMPAIGN_CODE", DataRowVersion.Default, model.CampaignCode);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, MakeDbNull(model.AdminDesc));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, MakeDbNull(model.IsActive));
                db.AddInParameter(cmd, "IS_MUST", DbType.Int32, MakeDbNull(model.IsMust));
                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, MakeDbNull(model.StartDate));
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, MakeDbNull(model.EndDate));
                db.AddInParameter(cmd, "MODEL_KOD", DbType.String, MakeDbNull(model.ModelKod));
                db.AddInParameter(cmd, "FAILURE_CODE", DbType.Int64, MakeDbNull(model.MainFailureCode));
                db.AddInParameter(cmd, "IS_VALID_ABROAD", DbType.Int32, MakeDbNull(model.IsValidAbroad));
                db.AddInParameter(cmd, "ML_CONTENT", DbType.String, MakeDbNull(model.MultiLanguageContentAsText));
                db.AddInParameter(cmd, "SUB_CATEGORY_ID", DbType.Int32, MakeDbNull(model.SubCategoryId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.CampaignCode = db.GetParameterValue(cmd, "CAMPAIGN_CODE").ToString();
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
    }
}
