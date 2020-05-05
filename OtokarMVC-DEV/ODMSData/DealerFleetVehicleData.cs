using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSModel.DealerFleetVehicle;
using ODMSCommon.Security;

namespace ODMSData
{
    public class DealerFleetVehicleData : DataAccessBase
    {

        public List<DealerFleetVehicleListModel> ListDealerFleetVehicle(UserInfo user, DealerFleetVehicleListModel filter, out int totalCount)
        {
            var listModel = new List<DealerFleetVehicleListModel>();

            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_DEALER_FLEET_VEHICLE");

                db.AddInParameter(cmd, "FLEET_ID", DbType.Int32, MakeDbNull(filter.FleetId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, MakeDbNull(filter.Offset));
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var model = new DealerFleetVehicleListModel()
                        {
                            CustomerId = dr["ID_CUSTOMER"].GetValue<int>(),
                            CustomerName = dr["CUSTOMER_NAME"].GetValue<string>(),
                            DealerId = dr["ID_DEALER"].GetValue<int>(),
                            DealerName = dr["DEALER_NAME"].GetValue<string>(),
                            FleetRequestId = dr["ID_FLEET_REQUEST"].GetValue<int>(),
                            VehicleId = dr["ID_VEHICLE"].GetValue<int>(),
                            VinNo = dr["VIN_NO"].GetValue<string>(),
                            WorkOrderId = dr["ID_WORK_ORDER"].GetValue<Int64>()
                        };

                        listModel.Add(model);
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

            return listModel;
        }
    }
}
