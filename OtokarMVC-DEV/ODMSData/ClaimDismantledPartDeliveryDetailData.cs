using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSData.DataContracts;
using ODMSModel.ClaimDismantledPartDeliveryDetails;

namespace ODMSData
{
    public class ClaimDismantledPartDeliveryDetailData : DataAccessBase, IClaimDismantledPartDeliveryDetail<ClaimDismantledPartDeliveryDetailListModel>
    {
        public ClaimDismantledPartDeliveryDetailListModel Exists(UserInfo user, ClaimDismantledPartDeliveryDetailListModel filter)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_EXISTS_CLAIM_RECALL_PERIOD");
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        filter.IsSelected = reader["IS_EXISTS"].GetValue<bool>();
                    }
                    reader.Close();
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
            return filter;
        }

        public void SetClaimWayBill(UserInfo user, int claimDismantledPartId, int claimWayBillId)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_SET_CLAIM_WAY_BILL");
                db.AddInParameter(cmd, "ID_CLAIM_DISMANTLED_PARTS", DbType.Int32, MakeDbNull(claimDismantledPartId));
                db.AddInParameter(cmd, "ID_CLAIM_WAY_BILL", DbType.Int32, MakeDbNull(claimWayBillId));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

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

        public List<ClaimDismantledPartDeliveryDetailListModel> List(UserInfo user, ClaimDismantledPartDeliveryDetailListModel filter, out int totalCnt)
        {
            var selectectedList = ListSelected(user, filter, out totalCnt);
            var unSelectectedList = ListUnSelected(user, filter, out totalCnt);

            selectectedList.AddRange(unSelectectedList);

            return selectectedList;
        }

        #region Private Methods

        private List<ClaimDismantledPartDeliveryDetailListModel> ListSelected(UserInfo user, ClaimDismantledPartDeliveryDetailListModel filter, out int totalCnt)
        {
            var result = new List<ClaimDismantledPartDeliveryDetailListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CLAIM_WAYBILL_SELECTED_DETAILS");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new ClaimDismantledPartDeliveryDetailListModel
                        {
                            ClaimDismantledPartId = reader["ID_CLAIM_DISMANTLED_PARTS"].GetValue<int>(),
                            Id = reader["ID_CLAIM_DISMANTLED_PARTS"].GetValue<int>(),
                            PartName = reader["PART_NAME"].GetValue<string>(),
                            PartCode = reader["PART_CODE"].GetValue<string>(),
                            DismantledDate = reader["CREATE_DATE"].GetValue<DateTime>(),
                            Barcode = reader["BARCODE"].GetValue<string>(),
                            Qty = reader["QUANTITY"].GetValue<decimal>(),
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

        private IEnumerable<ClaimDismantledPartDeliveryDetailListModel> ListUnSelected(UserInfo user, ClaimDismantledPartDeliveryDetailListModel filter, out int totalCnt)
        {
            var result = new List<ClaimDismantledPartDeliveryDetailListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CLAIM_WAYBILL_UNSELECTED_DETAILS");
                db.AddInParameter(cmd, "ID_CLAIM_WAYBILL", DbType.Int32, MakeDbNull(filter.ClaimWayBillId));
                db.AddInParameter(cmd, "BARCODE", DbType.String, MakeDbNull(filter.Barcode));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new ClaimDismantledPartDeliveryDetailListModel
                        {
                            ClaimDismantledPartId = reader["ID_CLAIM_DISMANTLED_PARTS"].GetValue<int>(),
                            Id = reader["ID_CLAIM_DISMANTLED_PARTS"].GetValue<int>(),
                            PartName = reader["PART_NAME"].GetValue<string>(),
                            PartCode = reader["PART_CODE"].GetValue<string>(),
                            Barcode = reader["BARCODE"].GetValue<string>(),
                            DismantledDate = reader["CREATE_DATE"].GetValue<DateTime>(),
                            Qty = reader["QUANTITY"].GetValue<decimal>(),
                            IsSelected = true
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

        #endregion
    }
}
