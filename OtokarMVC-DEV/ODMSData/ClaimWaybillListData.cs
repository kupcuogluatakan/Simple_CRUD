using ODMSModel.ClaimWaybillList;
using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSCommon.Security;

namespace ODMSData
{
    public class ClaimWaybillListData : DataAccessBase
    {
        public List<ClaimWaybillListListModel> ListClaimWaybillList(UserInfo user,ClaimWaybillListListModel filter, out int totalCount)
        {
            var retVal = new List<ClaimWaybillListListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CLAIM_WAYBILL_LIST");
                db.AddInParameter(cmd, "IS_ACCEPTED", DbType.Int64, filter.IsAccepted);
                db.AddInParameter(cmd, "WAYBILL_NO", DbType.String, filter.WaybillNo);
                db.AddInParameter(cmd, "WAYBILL_SERIAL_NO", DbType.String, filter.WaybillSerialNo);
                db.AddInParameter(cmd, "WAYBILL_TEXT", DbType.String, filter.WaybillText);
                db.AddInParameter(cmd, "WAYBILL_DATE", DbType.DateTime, MakeDbNull(filter.WaybillDate));
                db.AddInParameter(cmd, "ACCEPT_USER", DbType.String, filter.AcceptUser);
                db.AddInParameter(cmd, "ACCEPT_DATE", DbType.DateTime, MakeDbNull(filter.AcceptDate));

                AddPagingParametersWithLanguage(user,cmd,filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var claimWaybillListListModel = new ClaimWaybillListListModel
                        {
                            IdClaimWaybill = reader["ID_CLAIM_WAYBILL"].GetValue<Int32>(),
                            DealerName = reader["DEALER_NAME"].GetValue<string>(),
                            WaybillText = reader["WAYBILL_TEXT"].GetValue<string>(),
                            WaybillNo = reader["WAYBILL_NO"].GetValue<string>(),
                            WaybillDate = reader["WAYBILL_DATE"].GetValue<DateTime>(),
                            WaybillSerialNo = reader["WAYBILL_SERIAL_NO"].GetValue<string>(),
                            AcceptDate = reader["ACCEPT_DATE"].GetValue<DateTime?>(),
                            AcceptUser = reader["ACCEPT_USER"].GetValue<string>()
                        };

                        retVal.Add(claimWaybillListListModel);
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
