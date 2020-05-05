using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.ClaimDismantledParts;

namespace ODMSData
{
    public class ClaimDismantledPartsData : DataAccessBase
    {
        public List<ClaimDismantledPartsListModel> ListClaimDismantledParts(UserInfo user,ClaimDismantledPartsListModel filter, out int totalCount)
        {
            var retVal = new List<ClaimDismantledPartsListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CLAIM_DISMANTLED_PARTS");
                db.AddInParameter(cmd, "CLAIM_WAYBILL_ID", DbType.Int32, MakeDbNull(filter.ClaimWaybillId));
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var claimDismantledPartListModel = new ClaimDismantledPartsListModel
                        {
                            Barcode = reader["BARCODE"].GetValue<string>(),
                            ClaimWaybillId = reader["CLAIM_WAYBILL_ID"].GetValue<int>(),
                            CreateDate = reader["CREATE_DATE"].GetValue<DateTime>(),
                            DismantledPartId = reader["DISMANTLED_PART_ID"].GetValue<int>(),
                            DismantledPartName = reader["DISMANTLED_PART_NAME"].GetValue<string>(),
                            DismantledPartSerialNo = reader["DISMANTLED_PART_SERIAL_NO"].GetValue<string>(),
                            FirmActionId = reader["FIRM_ACTION_ID"].GetValue<int>(),
                            FirmActionName = reader["FIRM_ACTION_NAME"].GetValue<string>(),
                            FirmActionExplanation = reader["FIRM_EXPLANATION"] == DBNull.Value ? null : reader["FIRM_EXPLANATION"].GetValue<string>(),
                            PartId = reader["PART_ID"].GetValue<int>(),
                            PartName = reader["PART_NAME"].GetValue<string>(),
                            Quantity = reader["QUANTITY"].GetValue<decimal>(),
                            RackCode = reader["RACK_CODE"].GetValue<string>(),
                            ClaimDismantledPartId = reader["CLAIM_DISMANTLED_PART_ID"].GetValue<int>()
                        };
                        retVal.Add(claimDismantledPartListModel);
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

        public ClaimDismantledPartsViewModel GetClaimDismantledParts(UserInfo user, ClaimDismantledPartsViewModel filter)
        {
            System.Data.Common.DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_CLAIM_DISMANTLED_PART");
                db.AddInParameter(cmd, "CLAIM_DISMANTLED_PARTS_ID", DbType.Int32, MakeDbNull(filter.ClaimDismantledPartId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.Barcode = dReader["BARCODE"].GetValue<string>();
                    filter.ClaimWaybillId = dReader["CLAIM_WAYBILL_ID"].GetValue<int>();
                    filter.CreateDate = dReader["CREATE_DATE"].GetValue<DateTime>();
                    filter.DismantledPartId = dReader["DISMANTLED_PART_ID"].GetValue<int>();
                    filter.DismantledPartName = dReader["DISMANTLED_PART_NAME"].GetValue<string>();
                    filter.DismantledPartSerialNo = dReader["DISMANTLED_PART_SERIAL_NO"].GetValue<string>();
                    filter.FirmActionId = dReader["FIRM_ACTION_ID"].GetValue<int>();
                    filter.FirmActionName = dReader["FIRM_ACTION_NAME"].GetValue<string>();
                    filter.PartId = dReader["PART_ID"].GetValue<int>();
                    filter.PartName = dReader["PART_NAME"].GetValue<string>();
                    filter.Quantity = dReader["QUANTITY"].GetValue<decimal>();
                    filter.RackCode = dReader["RACK_CODE"].GetValue<string>();
                    filter.ClaimDismantledPartId = dReader["CLAIM_DISMANTLED_PART_ID"].GetValue<int>();


                    filter.DealerId = dReader["DEALER_ID"].GetValue<int>();
                    filter.WorkOrderDetailId = dReader["WORK_ORDER_DETAIL_ID"].GetValue<int>();
                    filter.FirmActionDate = dReader["FIRM_ACTION_DATE"].GetValue<DateTime>();
                    filter.ClaimRecallPeriodId = dReader["CLAIM_RECALL_PERIOD_ID"].GetValue<int>();
                    filter.SupplierCode = dReader["SUPPLIER_CODE"].GetValue<string>();
                    filter.GuaranteeId = dReader["GUARANTEE_ID"].GetValue<int>();
                    filter.GuaranteeSeq = dReader["GUARANTEE_SEQ"].GetValue<int>();
                    filter.FirmActionExplanation = dReader["FIRM_EXPLANATION"].GetValue<string>();
                    filter.Barcode = dReader["BARCODE"].GetValue<string>();
                    filter.BarcodeFirstPrintDate = dReader["BARCODE_FIRST_PRINT_DATE"].GetValue<DateTime>();
                    filter.DealerScrapDate = dReader["DEALER_SCRAP_DATE"].GetValue<DateTime>();
                    filter.IsApproved = dReader["IS_APPROVED"].GetValue<bool>();
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

        public void DMLClaimDismantledParts(UserInfo user, ClaimDismantledPartsViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_CLAIM_DISMANTLED_PARTS");
                db.AddParameter(cmd, "CLAIM_DISMANTLED_PARTS_ID", DbType.Int32, ParameterDirection.InputOutput, "CLAIM_DISMANTLED_PARTS_ID", DataRowVersion.Default, model.ClaimDismantledPartId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "BARCODE", DbType.String, MakeDbNull(model.Barcode));
                db.AddInParameter(cmd, "CLAIM_WAYBILL_ID", DbType.Int32, model.ClaimWaybillId);
                db.AddInParameter(cmd, "DISMANTLED_PART_ID", DbType.Int32, MakeDbNull(model.DismantledPartId));
                db.AddInParameter(cmd, "DISMANTLED_PART_SERIALNO", DbType.String, MakeDbNull(model.DismantledPartSerialNo));
                db.AddInParameter(cmd, "FIRM_ACTION_ID", DbType.Int32, model.FirmActionId);
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, MakeDbNull(model.PartId));
                db.AddInParameter(cmd, "QUANTITY", DbType.Decimal, MakeDbNull(model.Quantity));
                db.AddInParameter(cmd, "RACK_CODE", DbType.String, MakeDbNull(model.RackCode));
                db.AddInParameter(cmd, "FIRM_EXPLANATION", DbType.String, model.FirmActionExplanation);

                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(model.DealerId));
                db.AddInParameter(cmd, "WORK_ORDER_DETAIL_ID", DbType.Int32, MakeDbNull(model.WorkOrderDetailId));
                db.AddInParameter(cmd, "FIRM_ACTION_DATE", DbType.DateTime, MakeDbNull(model.FirmActionDate));
                db.AddInParameter(cmd, "CLAIM_RECALL_PERIOD_ID", DbType.Int32, MakeDbNull(model.ClaimRecallPeriodId));
                db.AddInParameter(cmd, "SUPPLIER_CODE", DbType.String, MakeDbNull(model.SupplierCode));
                db.AddInParameter(cmd, "GUARANTEE_ID", DbType.Int32, MakeDbNull(model.GuaranteeId));
                db.AddInParameter(cmd, "GUARANTEE_SEQ", DbType.Int32, MakeDbNull(model.GuaranteeSeq));
                db.AddInParameter(cmd, "BARCODE_FIRST_PRINT_DATE", DbType.DateTime, MakeDbNull(model.BarcodeFirstPrintDate));
                db.AddInParameter(cmd, "DEALER_SCRAP_DATE", DbType.DateTime, MakeDbNull(model.DealerScrapDate));
                db.AddInParameter(cmd, "IS_APPROVED", DbType.Int32, MakeDbNull(model.IsApproved));

                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ClaimDismantledPartId = db.GetParameterValue(cmd, "CLAIM_DISMANTLED_PARTS_ID").GetValue<int>();
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

        public ClaimWaybillViewModel GetClaimWaybill(ClaimWaybillViewModel filter)
        {
            System.Data.Common.DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_CLAIM_WAYBILL");
                db.AddInParameter(cmd, "ID_CLAIM_WAYBILL", DbType.Int32, MakeDbNull(filter.ClaimWaybillId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.ClaimWaybillId = dReader["ID_CLAIM_WAYBILL"].GetValue<int>();
                    filter.WaybillNo = dReader["WAYBILL_NO"].GetValue<string>();
                    filter.WaybillSerialNo = dReader["WAYBILL_SERIAL_NO"].GetValue<string>();
                    filter.WaybillDate = dReader["WAYBILL_DATE"].GetValue<DateTime>();
                    filter.WaybillText = dReader["WAYBILL_TEXT"].GetValue<string>();
                    filter.AcceptDate = dReader["ACCEPT_DATE"].GetValue<DateTime>();
                    filter.AcceptUser = dReader["ACCEPT_USER"].GetValue<string>();
                    filter.DealerId = dReader["ID_DEALER"].GetValue<int>();
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

        public void DMLClaimWaybill(UserInfo user, ClaimWaybillViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_CLAIM_WAYBILL");
                db.AddInParameter(cmd, "ID_CLAIM_WAYBILL", DbType.Int32, model.ClaimWaybillId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(model.DealerId));
                db.AddInParameter(cmd, "WAYBILL_TEXT", DbType.String, MakeDbNull(model.WaybillText));
                db.AddInParameter(cmd, "WAYBILL_SERIAL_NO", DbType.String, MakeDbNull(model.WaybillSerialNo));
                db.AddInParameter(cmd, "WAYBILL_NO", DbType.String, MakeDbNull(model.WaybillNo));
                db.AddInParameter(cmd, "WAYBILL_DATE", DbType.DateTime, MakeDbNull(model.WaybillDate));
                db.AddInParameter(cmd, "ACCEPT_USER", DbType.String, MakeDbNull(model.AcceptUser));
                db.AddInParameter(cmd, "ACCEPT_DATE", DbType.DateTime, MakeDbNull(model.AcceptDate));

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

        public void UpdateClaimDismantledParts(UserInfo user, ClaimDismantledPartsViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_UPDATE_CLAIM_DISMANTLED_PARTS");
                db.AddInParameter(cmd, "CLAIM_DISMANTLED_PARTS_ID", DbType.String, model.IdList);
                db.AddInParameter(cmd, "BARCODE_FIRST_PRINT_DATE", DbType.DateTime, model.BarcodeFirstPrintDate);
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
