using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.WorkOrderPicking;

namespace ODMSData
{
    public class WorkOrderPickingData : DataAccessBase
    {
        public List<WorkOrderPickingListModel> ListWorkOrderPicking(UserInfo user, WorkOrderPickingListModel filter, out int totalCount)
        {
            var listModel = new List<WorkOrderPickingListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_WORK_ORDER_PICKING");
                db.AddInParameter(cmd, "WORK_ORDER_PICKING_ID", DbType.Int64, MakeDbNull(filter.WorkOrderPickingId));
                db.AddInParameter(cmd, "PART_SALE_ID", DbType.Int64, MakeDbNull(filter.PartSaleId));
                db.AddInParameter(cmd, "IS_RETURN", DbType.Int32, filter.IsReturn);
                db.AddInParameter(cmd, "STATUS", DbType.String, filter.StatusIds);
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, user.GetUserDealerId());
                db.AddInParameter(cmd, "WORK_ORDER_PLATE", DbType.String, filter.WorkOrderPlate);
                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, filter.StartDate);
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, filter.EndDate);
                db.AddInParameter(cmd, "NO", DbType.Int64, MakeDbNull(filter.No));
                db.AddInParameter(cmd, "CUSTOMER_ID", DbType.Int64, MakeDbNull(filter.CustomerId));
                db.AddInParameter(cmd, "PART_NAME", DbType.String, MakeDbNull(filter.PartName));
                db.AddInParameter(cmd, "PART_CODE", DbType.String, MakeDbNull(filter.PartCode));
                db.AddInParameter(cmd, "PICK_SOURCE", DbType.Int32, MakeDbNull(filter.SourceType));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var model = new WorkOrderPickingListModel
                        {
                            WorkOrderPickingId = dr["ID_WORK_ORDER_PICKING_MST"].GetValue<int>(),
                            Status = dr["STATUS"].GetValue<string>(),
                            No = dr["NO"].GetValue<string>(),
                            IsReturn = dr["IS_RETURN"].GetValue<int>(),
                            IsReturnS =
                                    dr["IS_RETURN"].GetValue<int>() == 0
                                        ? MessageResource.Global_Display_Picking
                                        : MessageResource.Global_Display_Return,
                            StatusId = dr["STATUS_ID"].GetValue<int>(),
                            WorkOrderPlate = dr["PLATE"].GetValue<string>(),
                            OrderSource = dr["ORDER_SOURCE"].GetValue<int>() == 1 ? MessageResource.Global_Display_CustomerSale :
                                dr["ORDER_SOURCE"].GetValue<int>() == 2 ? MessageResource.Global_Display_OtokarReturnSale :
                                MessageResource.Global_Display_WorkOrder,// "Müşteri Satış" : "İş emri" : "Otokar İade Çıkış",
                            CreateDate = dr["CREATE_DATE"].GetValue<DateTime>()
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

        public void ChangeWorkOrderPickingStatus(UserInfo user, long workOrderPickingId, int statusId, out int errorNo, out string errorMessage)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_CHANGE_WORK_ORDER_PICKING_MST_STATUS");
                db.AddInParameter(cmd, "WORK_ORDER_MST_ID", DbType.Int64, MakeDbNull(workOrderPickingId));
                db.AddInParameter(cmd, "STATUS_ID", DbType.Int32, MakeDbNull(statusId));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);

                cmd.ExecuteNonQuery();

                errorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                errorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();

                if (errorNo > 0)
                    errorMessage = ResolveDatabaseErrorXml(errorMessage);

                cmd.Dispose();

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

        public void DMLWorkOrderPicking(UserInfo user, WorkOrderPickingViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_WORK_ORDER_PICKING_MST");
                db.AddParameter(cmd, "ID_WORK_ORDER_PICKING_MST", DbType.Int32, ParameterDirection.InputOutput, "ID_WORK_ORDER_PICKING_MST", DataRowVersion.Default, model.WorkOrderPickingId);
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int32, MakeDbNull(model.WorkOrderId));
                db.AddInParameter(cmd, "ID_DEALER ", DbType.Int32, user.GetUserDealerId());
                db.AddInParameter(cmd, "ID_PART_SALE ", DbType.Int32, MakeDbNull(model.PartSaleId));
                db.AddInParameter(cmd, "STATUS_ID ", DbType.Int32, model.StatusId);
                db.AddInParameter(cmd, "IS_RETURN ", DbType.Int32, model.IsReturn);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.WorkOrderPickingId = db.GetParameterValue(cmd, "ID_WORK_ORDER_PICKING_MST").GetValue<int>();
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

        public WorkOrderPickingViewModel GetWorkOrderPicking(WorkOrderPickingViewModel filter)
        {
            DbDataReader reader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_WORK_ORDER_PICKING");
                db.AddInParameter(cmd, "ID_WORK_ORDER_PICKING", DbType.Int32, MakeDbNull(filter.WorkOrderPickingId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    filter.WorkOrderPickingId = reader["ID_WORKSHOP"].GetValue<int>();
                    filter.WorkOrderId = reader["ID_WORK_ORDER"].GetValue<int>();
                    filter.PartSaleId = reader["ID_PART_SALE"].GetValue<int>();
                    filter.DealerId = reader["ID_DEALER"].GetValue<int>();
                    filter.StatusId = reader["ID_STATUS"].GetValue<int>();
                }
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
    }
}
