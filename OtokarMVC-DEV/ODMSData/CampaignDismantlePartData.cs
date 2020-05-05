using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.CampaignDismantlePart;

namespace ODMSData
{
    public class CampaignDismantlePartData : DataAccessBase
    {
        public void DMLCampaignDismantlePart(UserInfo user, CampaignDismantlePartViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_CAMPAIGN_DISMANTLE_PART");
                db.AddInParameter(cmd, "CAMPAIGN_CODE", DbType.String, model.CampaignCode);
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, model.PartId);
                db.AddInParameter(cmd, "NOTE", DbType.String, model.Note);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>());
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

        public List<CampaignDismantlePartListModel> ListCampaignDismantlePart(UserInfo user,CampaignDismantlePartListModel filter, out int totalCount)
        {
            var listModels = new List<CampaignDismantlePartListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_CAMPAIGN_DISMANTLE_PART");
                db.AddInParameter(cmd, "CAMPAIGN_CODE", DbType.String, filter.CampaignCode);
                AddPagingParametersWithLanguage(user, cmd, filter);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var model = new CampaignDismantlePartListModel
                        {
                            CampaignCode = dr["CAMPAIGN_CODE"].GetValue<string>(),
                            PartCode = dr["PART_CODE"].GetValue<string>(),
                            PartName = dr["PART_NAME"].GetValue<string>(),
                            Note = dr["NOTE"].GetValue<string>(),
                            PartId = dr["ID_PART"].GetValue<int>(),
                            CreateDate = dr["CREATE_DATE"].GetValue<DateTime>()
                        };

                        listModels.Add(model);
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
            return listModels;
        }

        public CampaignDismantlePartViewModel GetCampaignDismantlePart(UserInfo user, CampaignDismantlePartViewModel filter)
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_CAMPAIGN_DISMANTLE_PART");
                db.AddInParameter(cmd, "CAMPAIGN_CODE", DbType.String, MakeDbNull(filter.CampaignCode));
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, MakeDbNull(filter.PartId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.CampaignCode = dReader["CAMPAIGN_CODE"].GetValue<string>();
                    filter.PartId = dReader["PART_ID"].GetValue<int>();
                    filter.PartName = dReader["PART_NAME"].GetValue<string>();
                    filter.Note = dReader["NOTE"].GetValue<string>();
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
    }
}
