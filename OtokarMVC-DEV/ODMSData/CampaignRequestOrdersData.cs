using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.CampaignRequestOrders;
namespace ODMSData
{
    public class CampaignRequestOrdersData : DataAccessBase
    {
        public List<CampaignRequestOrdersListModel> ListCampaignRequestOrders(UserInfo user,CampaignRequestOrdersListModel filter, out int totalCnt)
        {
            var retVal = new List<CampaignRequestOrdersListModel>();
            totalCnt = 0;
            System.Data.Common.DbDataReader reader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CAMPAIGN_REQUEST_ORDER");
                db.AddInParameter(cmd, "ID_CAMPAIGN_REQUEST", DbType.Int32, MakeDbNull(filter.CampaignRId));
                AddPagingParametersWithLanguage(user,cmd,filter);
                CreateConnection(cmd);

                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var campaignRequestOrdersListModel = new CampaignRequestOrdersListModel
                            {
                                CampaignRId = reader["ID_CAMPAIGN_REQUEST"].GetValue<int>(),
                                CampaignCode = reader["CAMPAIGN_CODE"].GetValue<string>(),
                                ModelName = reader["MODEL_NAME"].GetValue<string>(),
                                CampaignName = reader["CAMPAIGN_NAME"].GetValue<string>(),
                                Quantity = reader["QUANTITY"].GetValue<int>(),
                                RequestNote = reader["REQUEST_NOTE"].GetValue<string>(),
                                StatusString = reader["REQUEST_STATUS"].GetValue<string>(),
                                RequestDate = reader["PREFERRED_ORDER_DATE"].GetValue<DateTime>()
                            };
                        retVal.Add(campaignRequestOrdersListModel);

                    }
                    reader.Close();
                }
                totalCnt = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                CloseConnection();
            }
            return retVal;
        }

        public void DeleteCampaignRequestOrders(CampaignRequestOrdersModel filter)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_DELETE_CAMPAIGN_REQUEST_ORDER");
                db.AddInParameter(cmd, "ID_CAMPAIGN_REQUEST", DbType.Int32, MakeDbNull(filter.CampaignRId));
               
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

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
