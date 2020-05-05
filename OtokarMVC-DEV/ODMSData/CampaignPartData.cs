using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSModel.CampaignPart;
using ODMSCommon.Security;

namespace ODMSData
{
    public class CampaignPartData : DataAccessBase
    {
        public List<CampaignPartListModel> ListCampaignParts(UserInfo user,CampaignPartListModel filter, out int totalCount)
        {
            var retVal = new List<CampaignPartListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CAMPAIGN_PART");
                db.AddInParameter(cmd, "CAMPAIGN_CODE", DbType.String, MakeDbNull(filter.CampaignCode));
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var campaignPartListModel = new CampaignPartListModel
                        {
                            CampaignCode = reader["CAMPAIGN_CODE"].GetValue<string>(),
                            PartCode = reader["PART_CODE"].GetValue<string>(),
                            PartId = reader["PART_ID"].GetValue<Int64>(),
                            PartTypeDesc = reader["PART_NAME"].GetValue<string>(),
                            Quantity = reader["QUANTITY"].GetValue<decimal>(),
                            SupplyType = reader["SUPPLY_TYPE"].GetValue<int>(),
                            SupplyTypeName = reader["SUPPLY_TYPE"].GetValue<int>() == 0 ? "Servis" : "Otokar"
                        };
                        retVal.Add(campaignPartListModel);
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

        public void DMLCampaignPart(UserInfo user, CampaignPartViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_CAMPAIGN_PART");
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "PART_ID", DbType.Int64, MakeDbNull(model.PartId));
                db.AddInParameter(cmd, "QUANTITY", DbType.Decimal, MakeDbNull(model.Quantity));
                db.AddInParameter(cmd, "SUPPLY_TYPE", DbType.Int32, model.SupplyType);
                db.AddInParameter(cmd, "CAMPAIGN_CODE", DbType.String, MakeDbNull(model.CampaignCode));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.CampaignCode = db.GetParameterValue(cmd, "CAMPAIGN_CODE").ToString();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();

                if ((model.ErrorNo > 0) && (model.ErrorNo != 2))
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

        public CampaignPartViewModel GetCampaignPart(UserInfo user, CampaignPartViewModel filter)
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_CAMPAIGN_PART");
                db.AddInParameter(cmd, "CAMPAIGN_CODE", DbType.String, MakeDbNull(filter.CampaignCode));
                db.AddInParameter(cmd, "PART_ID", DbType.Int64, MakeDbNull(filter.PartId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.CampaignCode = dReader["CAMPAIGN_CODE"].GetValue<string>();
                    filter.PartCode = dReader["PART_CODE"].GetValue<string>();
                    filter.PartId = dReader["PART_ID"].GetValue<int>();
                    filter.PartTypeDesc = dReader["PART_NAME"].GetValue<string>();
                    filter.Quantity = dReader["QUANTITY"].GetValue<decimal>();
                    filter.SupplyType = dReader["SUPPLY_TYPE"].GetValue<int>();
                    filter.SupplyTypeName = dReader["SUPPLY_TYPE_NAME"].GetValue<string>();
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
