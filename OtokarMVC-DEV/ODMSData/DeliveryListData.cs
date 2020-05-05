using ODMSCommon.Security;
using ODMSModel.DeliveryList;
using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;

namespace ODMSData
{
    public class DeliveryListData : DataAccessBase
    {
        public List<DeliveryListListModel> ListDeliveryList(UserInfo user, DeliveryListListModel filter, out int totalCount)
        {
            var retVal = new List<DeliveryListListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DELIVERY_MST_LIST");
                db.AddInParameter(cmd, "VAYBILL_NO", DbType.String, filter.WaybillNo);
                db.AddInParameter(cmd, "SAP_DELIVERY_NO", DbType.String, filter.SapDeliveryNo);
                db.AddInParameter(cmd, "STATUS", DbType.Int32, filter.DeliveryStatus);
                db.AddInParameter(cmd, "ID_DELIVERY", DbType.Int64, MakeDbNull(filter.IdDelivery));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int64, MakeDbNull(user.GetUserDealerId()));
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var deliveryListListModelListModel = new DeliveryListListModel
                        {
                            IdDelivery = reader["ID_DELIVERY"].GetValue<Int64>(),
                            WaybillNo = reader["VAYBILL_NO"].ToString(),
                            WaybillDate = reader["WAYBILL_DATE"].GetValue<DateTime>(),
                            SapDeliveryNo = reader["SAP_DELIVERY_NO"].ToString(),
                            DeliveryStatus = reader["DELIVERY_STATUS"].GetValue<int>(),
                            DeliveryStatusName = reader["DELIVERY_STATUS_NAME"].ToString(),
                            Sender = reader["SENDER"].ToString()
                        };

                        retVal.Add(deliveryListListModelListModel);
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
        public int IsDeliveryIdExist(UserInfo user, DeliveryListListModel filter, out int sonuc)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_CHECK_EXIST_DELIVERYNO_ON_DELIVERY_MST");
                db.AddInParameter(cmd, "deliveryNo", DbType.String, filter.SapDeliveryNo);
                db.AddOutParameter(cmd, "result", DbType.Int32, 4);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.String, user.UserId);
                db.AddInParameter(cmd, "ERROR_DESC", DbType.Int32, null);
                db.AddInParameter(cmd, "ERROR_NO", DbType.Int32, null);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    sonuc = db.GetParameterValue(cmd, "result").GetValue<int>();
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

            return sonuc;
        }
    }
}
