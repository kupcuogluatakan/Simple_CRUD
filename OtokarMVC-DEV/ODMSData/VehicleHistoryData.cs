using ODMSCommon;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSCommon.Security;

namespace ODMSData
{
    public class VehicleHistoryData : DataAccessBase
    {
        public List<VehicleHistoryListModel> ListVehicleHistory(UserInfo user,VehicleHistoryListModel filter, out int totalCount)
        {
            var retVal = new List<VehicleHistoryListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_VEHICLE_HISTORY_SCREEN");
                db.AddInParameter(cmd, "VIN_NO", DbType.String, MakeDbNull(filter.VinNo));
                db.AddInParameter(cmd, "PLATE", DbType.String, MakeDbNull(filter.Plate));
                db.AddInParameter(cmd, "CUSTOMER_IDS", DbType.String, MakeDbNull(filter.CustomerIds));
                db.AddInParameter(cmd, "DEALER_ID", DbType.String, MakeDbNull(filter.DealerName));
                db.AddInParameter(cmd, "PROCESS_TYPE_CODE", DbType.String, MakeDbNull(filter.ProcessType));
                db.AddInParameter(cmd, "INDICATOR_TYPE_NAME", DbType.String, MakeDbNull(filter.IndicatorType));
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var vehicleListModel = new VehicleHistoryListModel
                        {


                            Plate = reader["Plate"].ToString(),
                            VinNo = reader["VinNo"].ToString(),
                            VehicleId = reader["VEHICLE_ID"].GetValue<int>(),
                            VehicleHistoryId = reader["VEHICLE_HISTORY_ID"].GetValue<int>(),
                            ProcessType = reader["PROCESS_TYPE_NAME"].ToString(),
                            IndicatorType = reader["INDICATOR_TYPE_NAME"].ToString(),
                            DealerId = reader["DEALER_ID"].GetValue<int>(),
                            DealerName = reader["DEALER_NAME"].ToString(),
                            WorkOrderDetailId = reader["WORK_ORDER_DETAIL_ID"].GetValue<int>(),
                            IndicatorDate = reader["INDICATOR_DATE"].GetValue<DateTime>(),
                            VehicleKM = reader["VEHICLE_KM"].GetValue<long>(),
                            WorkOrderDate = reader["WORK_ORDER_DATE"].GetValue<DateTime>(),
                            WorkOrderId = reader["ID_WORK_ORDER"].GetValue<Int64>(),
                            AppIndicCode = reader["APP_INDIC_CODE"].GetValue<string>(),
                            AppIndicName = reader["APP_INDIC_NAME"].GetValue<string>(),
                            CampaignNameCode = reader["CAMPAIGN_CODE_NAME"].GetValue<string>()
                        };
                        retVal.Add(vehicleListModel);
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
    }
}
