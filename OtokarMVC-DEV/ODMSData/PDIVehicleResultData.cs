using System;
using ODMSCommon.Security;
using ODMSModel.PDIVehicleResult;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;

namespace ODMSData
{
    public class PDIVehicleResultData : DataAccessBase
    {
        public List<PDIVehicleResultListModel> ListPDIVehicleResult(UserInfo user,PDIVehicleResultListModel referenceListModel,
                                                                    out int totalCount)
        {
            var retVal = new List<PDIVehicleResultListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PDI_VEHICLE_RESULT_MST");
                db.AddInParameter(cmd, "GROUP_ID", DbType.Int32, MakeDbNull(referenceListModel.GroupId));
                db.AddInParameter(cmd, "TYPE_ID", DbType.Int32, MakeDbNull(referenceListModel.TypeId));
                db.AddInParameter(cmd, "MODEL_KOD", DbType.String, MakeDbNull(referenceListModel.ModelKod));
                db.AddInParameter(cmd, "VIN_NO", DbType.String, MakeDbNull(referenceListModel.VinNo));
                db.AddInParameter(cmd, "CUSTOMER_ID", DbType.Int32, MakeDbNull(referenceListModel.CustomerId));
                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, MakeDbNull(referenceListModel.StartDate));
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, MakeDbNull(referenceListModel.EndDate));
                db.AddInParameter(cmd, "STATUS_ID", DbType.Int32, referenceListModel.StatusId);
                db.AddInParameter(cmd, "USER_ID", DbType.Int32, MakeDbNull(user.UserId));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(referenceListModel.DealerId));
                db.AddInParameter(cmd, "APPROVAL_USER_ID", DbType.Int32, MakeDbNull(referenceListModel.ApprovalUserId));

                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(referenceListModel.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(referenceListModel.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, referenceListModel.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(referenceListModel.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var pdiVehicleResultListModel = new PDIVehicleResultListModel
                        {
                            ApprovalNote = reader["APPROVAL_NOTE"].GetValue<string>(),
                            ApprovalUserId = reader["APPROVAL_USER_ID"].GetValue<int>(),
                            ApprovalUserName = reader["APPROVAL_USER_NAME"].GetValue<string>(),
                            CustomerId = reader["CUSTOMER_ID"].GetValue<int>(),
                            CustomerName = reader["CUSTOMER_NAME"].GetValue<string>(),
                            DealerId = reader["DEALER_ID"].GetValue<int>(),
                            DealerName = reader["DEALER_NAME"].GetValue<string>(),
                            DifferentialSerialNo = reader["DIFFERENTAIL_SERIAL_NO"].GetValue<string>(),
                            EngineNo = reader["ENGINE_NO"].GetValue<string>(),
                            GroupId = reader["GROUP_ID"].GetValue<int>(),
                            GroupName = reader["GROUP_NAME"].GetValue<string>(),
                            ModelKod = reader["MODEL_KOD"].GetValue<string>(),
                            PDICheckNote = reader["PDI_CHECK_NOTE"].GetValue<string>(),
                            PDIVehicleResultId = reader["PDI_VEHICLE_RESULT_ID"].GetValue<int>(),
                            StatusId = reader["STATUS_ID"].GetValue<int>(),
                            StatusName = reader["STATUS_NAME"].GetValue<string>(),
                            TransmissionSerialNo = reader["TRANSMISSION_SERIAL_NO"].GetValue<string>(),
                            TypeId = reader["TYPE_ID"].GetValue<int>(),
                            TypeName = reader["TYPE_NAME"].GetValue<string>(),
                            VehicleId = reader["VEHICLE_ID"].GetValue<int>(),
                            VinNo = reader["VIN_NO"].GetValue<string>(),
                            WorkOrderDetailId = reader["WORK_ORDER_DETAIL_ID"].GetValue<int>(),
                            WorkOrderId = reader["WORK_ORDER_ID"].GetValue<int>(),
                            CreateDate = reader["CREATE_DATE"].GetValue<DateTime>()
                        };

                        retVal.Add(pdiVehicleResultListModel);
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

        public void GetPDIVehicleResult(UserInfo user,PDIVehicleResultViewModel pdiVehicleResultModel)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_PDI_VEHICLE_RESULT_MST");
                db.AddInParameter(cmd, "PDI_VEHICLE_RESULT_ID", DbType.Int32,
                                  MakeDbNull(pdiVehicleResultModel.PDIVehicleResultId));

                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        pdiVehicleResultModel.ApprovalNote = dr["APPROVAL_NOTE"].GetValue<string>();
                        pdiVehicleResultModel.ApprovalUserId = dr["APPROVAL_USER_ID"].GetValue<int>();
                        pdiVehicleResultModel.ApprovalUserName = dr["APPROVAL_USER_NAME"].GetValue<string>();
                        pdiVehicleResultModel.DealerId = dr["DEALER_ID"].GetValue<int>();
                        pdiVehicleResultModel.DealerName = dr["DEALER_NAME"].GetValue<string>();
                        pdiVehicleResultModel.DifferentialSerialNo = dr["DIFFERENTAIL_SERIAL_NO"].GetValue<string>();
                        pdiVehicleResultModel.EngineNo = dr["ENGINE_NO"].GetValue<string>();
                        pdiVehicleResultModel.PDICheckNote = dr["PDI_CHECK_NOTE"].GetValue<string>();
                        pdiVehicleResultModel.PDIVehicleResultId = dr["PDI_VEHICLE_RESULT_ID"].GetValue<int>();
                        pdiVehicleResultModel.StatusId = dr["STATUS_ID"].GetValue<int>();
                        pdiVehicleResultModel.StatusName = dr["STATUS_NAME"].GetValue<string>();
                        pdiVehicleResultModel.TransmissionSerialNo = dr["TRANSMISSION_SERIAL_NO"].GetValue<string>();
                        pdiVehicleResultModel.VehicleId = dr["VEHICLE_ID"].GetValue<int>();
                        pdiVehicleResultModel.VinNo = dr["VIN_NO"].GetValue<string>();
                        pdiVehicleResultModel.WorkOrderDetailId = dr["WORK_ORDER_DETAIL_ID"].GetValue<int>();
                        pdiVehicleResultModel.WorkOrderId = dr["WORK_ORDER_ID"].GetValue<int>();
                        pdiVehicleResultModel.CreateDate = dr["CREATE_DATE"].GetValue<DateTime>();

                        pdiVehicleResultModel.GroupId = dr["GROUP_ID"].GetValue<int>();
                        pdiVehicleResultModel.GroupName = dr["GROUP_NAME"].GetValue<string>();
                        pdiVehicleResultModel.TypeId = dr["TYPE_ID"].GetValue<int>();
                        pdiVehicleResultModel.TypeName = dr["TYPE_NAME"].GetValue<string>();
                        pdiVehicleResultModel.ModelKod = dr["MODEL_KOD"].GetValue<string>();
                        pdiVehicleResultModel.CustomerId = dr["CUSTOMER_ID"].GetValue<int>();
                        pdiVehicleResultModel.CustomerName = dr["CUSTOMER_NAME"].GetValue<string>();
                    }
                    dr.Close();
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
        }

        public void DMLPDIVehicleResult(UserInfo user,PDIVehicleResultViewModel pdiVehicleResultModel)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_PDI_VEHICLE_RESULT_MST");
                db.AddInParameter(cmd, "PDI_VEHICLE_RESULT_ID", DbType.Int32,
                    pdiVehicleResultModel.PDIVehicleResultId);
                db.AddInParameter(cmd, "STATUS_ID", DbType.String, pdiVehicleResultModel.StatusId);
                db.AddInParameter(cmd, "APPROVAL_NOTE", DbType.String, pdiVehicleResultModel.ApprovalNote);

                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, pdiVehicleResultModel.CommandType);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                pdiVehicleResultModel.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                pdiVehicleResultModel.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (pdiVehicleResultModel.ErrorNo > 0)
                    pdiVehicleResultModel.ErrorMessage = ResolveDatabaseErrorXml(pdiVehicleResultModel.ErrorMessage);
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
