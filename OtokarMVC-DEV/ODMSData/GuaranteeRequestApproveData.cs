using ODMSCommon.Resources;
using ODMSModel.GuaranteeRequestApprove;
using System;
using System.Collections.Generic;
using ODMSCommon;
using System.Data;
using ODMSCommon.Security;

namespace ODMSData
{
    public class GuaranteeRequestApproveData : DataAccessBase
    {
        public List<GuaranteeRequestApproveListModel> ListGuaranteeRequestApprove(UserInfo user,GuaranteeRequestApproveListModel filter, out int totalCount)
        {
            var retVal = new List<GuaranteeRequestApproveListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_GUARANTEE_REQUEST_APPROVE");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, filter.IdDealer);
                db.AddInParameter(cmd, "ID_USER", DbType.Int32, filter.IdUser);

                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(filter.WorkOrderId));
                db.AddInParameter(cmd, "ID_WORK_ORDER_DETAIL", DbType.Int64, MakeDbNull(filter.WorkOrderDetailId));

                db.AddInParameter(cmd, "APPROVE_START_DATE", DbType.DateTime, MakeDbNull(filter.ApproveStartDate));
                db.AddInParameter(cmd, "APPROVE_END_DATE", DbType.DateTime, MakeDbNull(filter.ApproveEndDate));
                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, MakeDbNull(filter.StartDate));
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, MakeDbNull(filter.EndDate));
                db.AddInParameter(cmd, "WARRANTY_STATUS", DbType.String, filter.WarrantyStatus);
                db.AddInParameter(cmd, "DEALER_REGION_ID", DbType.String, MakeDbNull(filter.DealerRegionId));
                db.AddInParameter(cmd, "CATEGORY", DbType.String, MakeDbNull(filter.CategoryId));
                db.AddInParameter(cmd, "PROCESS_TYPE", DbType.String, MakeDbNull(filter.ProcessType));
                db.AddInParameter(cmd, "INDICATOR_TYPE", DbType.String, MakeDbNull(filter.IndicatorType));

                db.AddInParameter(cmd, "VIN_NO", DbType.String, MakeDbNull(filter.VehicleVinNo));
                db.AddInParameter(cmd, "IS_EDITABLE", DbType.Int32, filter.IsEditable);
                db.AddInParameter(cmd, "VEHICLE_TYPE", DbType.String, MakeDbNull(filter.VehicleType));
                db.AddInParameter(cmd, "MODEL_KOD", DbType.String, filter.ModelKodList);

                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (filter.IsEditable == null) filter.IsEditable = 1;//Yama:(

                        var guaranteeRequestApproveListModel = new GuaranteeRequestApproveListModel
                        {
                            IdGuarantee = reader["ID_GUARANTEE"].GetValue<Int64>(),
                            GuaranteeSeq = reader["GUARANTEE_SEQ"].GetValue<Int16>(),
                            WarrantyStatusName = reader["WARRANTY_STATUS_NAME"].GetValue<string>(),
                            WorkOrderId = reader["ID_WORK_ORDER"].GetValue<Int64>(),
                            CreateDate = reader["CREATE_DATE"].GetValue<DateTime>(),
                            DealerName = reader["DEALER_NAME"].GetValue<string>(),
                            VehicleVinNo = reader["VEHICLE_VIN_NO"].GetValue<string>(),
                            VehicleCode = reader["VEHICLE_V_CODE_KOD"].GetValue<string>(),
                            GifNo = reader["SSID_GUARANTEE"].GetValue<Int64>(),
                            IsPerm = reader["IS_PERM"].GetValue<string>()=="1" ? MessageResource.Global_Display_Exist : MessageResource.Global_Display_DoesntExist,
                            ProcessType = reader["PROCESS_TYPE_NAME"].GetValue<string>(),
                            CustName = reader["CUST_NAME"].GetValue<string>(),
                            FailCode = reader["FAIL_CODE"].GetValue<string>(),
                            ApproveUserName = reader["APPROVE_USER_NAME"].GetValue<string>(),
                            RequestDate = reader["REQUEST_DATE"].GetValue<DateTime>(),
                            ApproveDate = reader["APPROVE_DATE"].GetValue<DateTime?>(),
                            CategoryName = reader["CATEGORY_NAME"].GetValue<string>(),
                            WorkOrderDetailId = reader["ID_WORK_ORDER_DETAIL"].GetValue<Int64>(),
                            IndicatorTypeName = reader["INDICATOR_TYPE_NAME"].GetValue<string>(),
                            IdVehicle = reader["ID_VEHICLE"].GetValue<int>(),
                            IsEditable = filter.IsEditable
                        };
                        retVal.Add(guaranteeRequestApproveListModel);
                    }
                    reader.Close();
                }

                totalCount = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();

                int errorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                string errorDesc = db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>();

                if (errorNo > 0)
                {
                    var testModel = new GuaranteeRequestApproveListModel();
                    testModel.ErrorNo = errorNo;
                    testModel.ErrorMessage = ResolveDatabaseErrorXml(errorDesc);
                    retVal.Add(testModel);
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

            return retVal;
        }
    }
}
