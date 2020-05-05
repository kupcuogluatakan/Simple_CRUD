using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSData.DataContracts;
using ODMSModel.ClaimDismantledPartDelivery;

namespace ODMSData
{
    public class ClaimDismantledPartDeliveryData : DataAccessBase, IClaimDismantledPartDelivery<ClaimDismantledPartDeliveryViewModel>
    {
        public void Delete(UserInfo user, ClaimDismantledPartDeliveryViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_CLAIM_WAYBILL");
                db.AddInParameter(cmd, "ID_CLAIM_WAYBILL", DbType.Int32, MakeDbNull(model.ClaimWayBillId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "WAYBILL_SERIAL_NO", DbType.String, MakeDbNull(model.ClaimWayBillSerialNo));
                db.AddInParameter(cmd, "WAYBILL_NO", DbType.String, MakeDbNull(model.ClaimWayBillNo));
                db.AddInParameter(cmd, "WAYBILL_DATE", DbType.DateTime, MakeDbNull(model.ClaimWayBillDate));
                db.AddInParameter(cmd, "WAYBILL_TEXT", DbType.String, MakeDbNull(null));
                db.AddInParameter(cmd, "ACCEPT_USER", DbType.String, MakeDbNull(null));
                db.AddInParameter(cmd, "ACCEPT_DATE", DbType.DateTime, MakeDbNull(null));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
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

        public ClaimDismantledPartDeliveryViewModel Get(UserInfo user, ClaimDismantledPartDeliveryViewModel filter)
        {
            DbDataReader reader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_CLAIM_WAYBILL");
                db.AddInParameter(cmd, "ID_CLAIM_WAYBILL", DbType.Int32, MakeDbNull(filter.ClaimWayBillId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    filter.ClaimWayBillId = reader["ID_CLAIM_WAYBILL"].GetValue<int>();
                    filter.ClaimWayBillNo = reader["WAYBILL_NO"].GetValue<string>();
                    filter.ClaimWayBillSerialNo = reader["WAYBILL_SERIAL_NO"].GetValue<string>();
                    filter.ClaimWayBillDate = reader["WAYBILL_DATE"].GetValue<DateTime>();
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                    reader.Dispose();
                }
                CloseConnection();
            }
            return filter;
        }

        public void Insert(UserInfo user, ClaimDismantledPartDeliveryViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_CLAIM_WAYBILL");
                db.AddInParameter(cmd, "ID_CLAIM_WAYBILL", DbType.Int32, MakeDbNull(model.ClaimWayBillId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "WAYBILL_SERIAL_NO", DbType.String, MakeDbNull(model.ClaimWayBillSerialNo));
                db.AddInParameter(cmd, "WAYBILL_NO", DbType.String, MakeDbNull(model.ClaimWayBillNo));
                db.AddInParameter(cmd, "WAYBILL_DATE", DbType.DateTime, MakeDbNull(model.ClaimWayBillDate));
                db.AddInParameter(cmd, "WAYBILL_TEXT", DbType.String, MakeDbNull(null));
                db.AddInParameter(cmd, "ACCEPT_USER", DbType.String, MakeDbNull(null));
                db.AddInParameter(cmd, "ACCEPT_DATE", DbType.DateTime, MakeDbNull(null));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
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

        public List<ClaimDismantledPartDeliveryListModel> List(UserInfo user, ClaimDismantledPartDeliveryListModel filter, out int totalCnt)
        {
            var result = new List<ClaimDismantledPartDeliveryListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CLAIM_WAYBILL");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                AddPagingParameters(cmd,filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new ClaimDismantledPartDeliveryListModel
                        {
                            ClaimWayBillId = reader["ID_CLAIM_WAYBILL"].GetValue<int>(),
                            ClaimWayBillNo = reader["WAYBILL_NO"].GetValue<string>(),
                            ClaimWayBillSerialNo = reader["WAYBILL_SERIAL_NO"].GetValue<string>(),
                            ClaimWayBillDate = reader["WAYBILL_DATE"].GetValue<DateTime>()
                        };
                        result.Add(listModel);
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
                CloseConnection();
            }

            return result;
        }

        public void Update(UserInfo user, ClaimDismantledPartDeliveryViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_CLAIM_WAYBILL");
                db.AddInParameter(cmd, "ID_CLAIM_WAYBILL", DbType.Int32, MakeDbNull(model.ClaimWayBillId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "WAYBILL_SERIAL_NO", DbType.String, MakeDbNull(model.ClaimWayBillSerialNo));
                db.AddInParameter(cmd, "WAYBILL_NO", DbType.String, MakeDbNull(model.ClaimWayBillNo));
                db.AddInParameter(cmd, "WAYBILL_DATE", DbType.DateTime, MakeDbNull(model.ClaimWayBillDate));
                db.AddInParameter(cmd, "WAYBILL_TEXT", DbType.String, MakeDbNull(null));
                db.AddInParameter(cmd, "ACCEPT_USER", DbType.String, MakeDbNull(null));
                db.AddInParameter(cmd, "ACCEPT_DATE", DbType.DateTime, MakeDbNull(null));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
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
