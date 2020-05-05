using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSModel.CampaignLabour;
using System.Web.Mvc;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class CampaignLabourData : DataAccessBase
    {
        public List<CampaignLabourListModel> ListCampaignLabours(UserInfo user,CampaignLabourListModel filter, out int totalCount)
        {
            var retVal = new List<CampaignLabourListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CAMPAIGN_LABOUR");
                db.AddInParameter(cmd, "CAMPAIGN_CODE", DbType.String, MakeDbNull(filter.CampaignCode));
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var campaignLabourListModel = new CampaignLabourListModel
                        {
                            CampaignCode = reader["CAMPAIGN_CODE"].GetValue<string>(),
                            LabourCode = reader["LABOUR_CODE"].GetValue<string>(),
                            LabourId = reader["LABOUR_ID"].GetValue<int>(),
                            LabourTypeDesc = reader["LABOUR_TYPE_DESC"].GetValue<string>(),
                            Quantity = reader["QUANTITY"].GetValue<decimal>()
                        };
                        retVal.Add(campaignLabourListModel);
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

        public void DMLCampaignLabour(UserInfo user, CampaignLabourViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_CAMPAIGN_LABOUR");
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "CAMPAIGN_CODE", DbType.String, model.CampaignCode);
                db.AddInParameter(cmd, "QUANTITY", DbType.Decimal, MakeDbNull(model.Quantity));
                db.AddInParameter(cmd, "LABOUR_ID", DbType.Int32, MakeDbNull(model.LabourId));
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

        public CampaignLabourViewModel GetCampaignLabour(UserInfo user, CampaignLabourViewModel filter)
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_CAMPAIGN_LABOUR");
                db.AddInParameter(cmd, "CAMPAIGN_CODE", DbType.String, MakeDbNull(filter.CampaignCode));
                db.AddInParameter(cmd, "LABOUR_ID", DbType.Int32, MakeDbNull(filter.LabourId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.CampaignCode = dReader["CAMPAIGN_CODE"].GetValue<string>();
                    filter.LabourCode = dReader["LABOUR_CODE"].GetValue<string>();
                    filter.LabourId = dReader["LABOUR_ID"].GetValue<int>();
                    filter.LabourTypeDesc = dReader["LABOUR_TYPE_DESC"].GetValue<string>();
                    filter.Quantity = dReader["QUANTITY"].GetValue<decimal>();
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

        public List<SelectListItem> ListLabourTimeAsSelectList(UserInfo user, string campaignCode, Int64 idLabour)
        {
            List<SelectListItem> listLabourTime = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_LABOUR_DURATION_TOOLTIP");
                db.AddInParameter(cmd, "CAMPAIGN_CODE", DbType.String, campaignCode);
                db.AddInParameter(cmd, "ID_LABOUR", DbType.Int64, idLabour);

                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SelectListItem item = new SelectListItem
                        {
                            Value = dr["TYPE_NAME"].GetValue<string>() + " - " + dr["ENGINE_TYPE"].GetValue<string>(),
                            Text = dr["DURATION"].GetValue<string>()
                        };

                        listLabourTime.Add(item);
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

            return listLabourTime;
        }
    }
}
