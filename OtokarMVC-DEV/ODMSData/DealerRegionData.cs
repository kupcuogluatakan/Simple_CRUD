using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSModel.DealerRegion;
using ODMSModel.ListModel;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class DealerRegionData : DataAccessBase
    {
        public List<DealerRegionListModel> ListDealerRegions(UserInfo user,DealerRegionListModel filter, out int totalCount)
        {
            var retVal = new List<DealerRegionListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER_REGIONS");
                db.AddInParameter(cmd, "DEALER_REGION_NAME", DbType.String, MakeDbNull(filter.DealerRegionName));
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var dealerRegionListModel = new DealerRegionListModel
                        {
                            DealerRegionId = reader["DEALER_REGION_ID"].GetValue<int>(),
                            DealerRegionName = reader["DEALER_REGION_NAME"].ToString()
                        };
                        retVal.Add(dealerRegionListModel);
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

        public void DMLDealerRegion(UserInfo user, DealerRegionIndexViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_DEALER_REGION_MAIN");
                db.AddParameter(cmd, "DEALER_REGION_ID", DbType.Int32, ParameterDirection.InputOutput, "DEALER_REGION_ID", DataRowVersion.Default, model.DealerRegionId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "DEALER_REGION_NAME", DbType.String, MakeDbNull(model.DealerRegionName));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.DealerRegionId = db.GetParameterValue(cmd, "DEALER_REGION_ID").GetValue<int>();
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

        public DealerRegionIndexViewModel GetDealerRegion(UserInfo user, DealerRegionIndexViewModel filter)
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_DEALER_REGION");
                db.AddInParameter(cmd, "DEALER_REGION_ID", DbType.Int32, MakeDbNull(filter.DealerRegionId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.DealerRegionId = dReader["DEALER_REGION_ID"].GetValue<int>();
                    filter.DealerRegionName = dReader["DEALER_REGION_NAME"].GetValue<string>();
                }
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
