using ODMSCommon.Security;
using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSModel.DealerGuaranteeControl;

namespace ODMSData
{
    public class DealerGuaranteeControlListData : DataAccessBase
    {

        public List<DealerGuaranteeControlListModel> ListDealerGuaranteeControl(UserInfo user, DealerGuaranteeControlListModel filter, out int totalCount)
        {
            var retVal = new List<DealerGuaranteeControlListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_GUARANTEE_REQUEST_APPROVE_FOR_DEALER");

                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, filter.DealerId);
                db.AddInParameter(cmd, "WARRANTY_STATUS", DbType.Int32, MakeDbNull(filter.WarrantyStatus));
                db.AddInParameter(cmd, "CATEGORY", DbType.String, filter.CategoryId);
                db.AddInParameter(cmd, "PROCESS_TYPE", DbType.String, filter.ProcessType);
                db.AddInParameter(cmd, "INDICATOR_TYPE", DbType.String, filter.IndicatorType);
                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, MakeDbNull(filter.StartDate));
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, MakeDbNull(filter.EndDate));
                db.AddInParameter(cmd, "APPROVE_DATE_START", DbType.DateTime, MakeDbNull(filter.ApproveStartDate));
                db.AddInParameter(cmd, "APPROVE_DATE_END", DbType.DateTime, MakeDbNull(filter.ApproveEndDate));
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(filter.WorkOrderId));
                db.AddInParameter(cmd, "ID_WORK_ORDER_DETAIL", DbType.Int64, MakeDbNull(filter.WorkOrderDetailId));
                db.AddInParameter(cmd, "VIN_NO", DbType.String, filter.VinNo);
                db.AddInParameter(cmd, "MODEL_KOD_LIST", DbType.String, filter.ModelKodList);
                db.AddInParameter(cmd, "VEHICLE_TYPE", DbType.String, filter.VehicleType);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 0);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                db.AddInParameter(cmd, "sql", DbType.String, null);

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var dealerGuaranteeControlListItem = new DealerGuaranteeControlListModel
                        {
                            GuaranteeId = reader["GuaranteeId"].GetValue<int>(),
                            GuaranteeSeq = reader["GuaranteeSeq"].GetValue<long>(),
                            ConfirmDesc = reader["ConfirmDesc"].GetValue<string>(),
                            WarrantyStatus = reader["WarrantyStatus"].GetValue<string>(),
                            RequestDescription = reader["RequestDescription"].GetValue<string>(),
                            Category = reader["CATEGORY"].GetValue<string>(),
                            ProcessType = reader["ProcessType"].GetValue<string>(),
                            IndicatorType = reader["IndicatorType"].GetValue<string>(),
                            WorkOrderId = reader["WorkOrderId"].GetValue<long>(),
                            WorkOrderDetailId = reader["WorkOrderDetailId"].GetValue<long>(),
                            VinNo = reader["VinNo"].GetValue<string>(),
                            IsPerm = reader["IsPerm"].GetValue<bool>(),
                            FailCode = reader["FailCode"].GetValue<string>(),
                            FailCodeDesc = reader["FailCodeDesc"].GetValue<string>(),
                            RequestDate = reader["RequestDate"].GetValue<DateTime>(),
                            ApproveDate = reader["ApproveDate"].GetValue<DateTime?>(),
                            GifNo = reader["GifNo"].GetValue<long?>(),
                            VehicleId = reader["VehicleId"].GetValue<int>(),
                            Dealer = reader["Dealer"].GetValue<string>()
                        };

                        retVal.Add(dealerGuaranteeControlListItem);
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
