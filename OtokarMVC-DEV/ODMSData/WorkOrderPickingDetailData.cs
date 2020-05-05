using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.WorkOrderPicking;
using ODMSModel.WorkOrderPickingDetail;

namespace ODMSData
{
    public class WorkOrderPickingDetailData : DataAccessBase
    {
        public List<WorkOrderPickingDetailListModel> ListWorkOrderPickingDetail(UserInfo user, WorkOrderPickingDetailListModel filter, out int totalCount)
        {
            var listModel = new List<WorkOrderPickingDetailListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_WORK_ORDER_PICKING_DET");
                db.AddInParameter(cmd, "ID_WORK_ORDER_PICKING_MST", DbType.Int64, filter.WOPMstId);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, MakeDbNull(0));
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(0));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 0);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var model = new WorkOrderPickingDetailListModel
                        {
                            WOPDetId = dr["ID_WORK_ORDER_PICKING_DET"].GetValue<Int64>(),
                            RequestQuantity = dr["REQ_QTY"].GetValue<string>(),
                            PickQuantity = dr["PICK_QTY"].GetValue<string>(),
                            PartName = dr["PART_NAME"].GetValue<string>(),
                            PartCodeName = dr["PART_CODE"].GetValue<string>(),
                            PartId = dr["ID_PART"].GetValue<int>(),
                            StockType = dr["MAINT_NAME"].GetValue<string>(),
                            AppIndicator = dr["SUB"].GetValue<string>() + " - " + dr["FAIL"].GetValue<string>(),
                            Unit = dr["UNIT"].GetValue<string>(),
                            UnitName = dr["UNIT_NAME"].GetValue<string>(),
                            StockTypeId = dr["ID_STOCK_TYPE"].GetValue<int>()
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

        public List<WOPDetSubListModel> ListWorkOrderPickingDetailSub(UserInfo user, WOPDetSubListModel filter, out int totalCount)
        {
            var listModel = new List<WOPDetSubListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_WORK_ORDER_PICKING_DET_SUB");
                db.AddInParameter(cmd, "ID_WORK_ORDER_PICKING_DET", DbType.Int64, filter.WOPDetId);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, null);
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, null);
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, null);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, null);
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var model = new WOPDetSubListModel
                        {
                            ResultId = dr["ID_WORK_ORDER_PICKING_RESULT"].GetValue<int>(),
                            WOPDetId = dr["ID_WORK_ORDER_PICKING_DET"].GetValue<int>(),
                            Text = dr["RACK_WAREHOUSE"].GetValue<string>(),
                            Value = dr["ID_RACK"].GetValue<string>(),
                            Quantity = dr["QTY"].GetValue<decimal>()
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

        public List<SelectListItem> ListRackWarehouseByDetId(UserInfo user, int id)
        {
            var listItem = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_RACK_AND_WAREHOUSE_BY_WOPDET_ID_COMBO");
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "WOPDET_ID", DbType.Int64, id);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var item = new SelectListItem
                        {
                            Text = dr["RACK_WAREHOUSE"].GetValue<string>(),
                            Value = dr["ID_RACK"].GetValue<string>()
                        };

                        listItem.Add(item);
                    }
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

            return listItem;
        }

        public void DeleteWOPDetSub(UserInfo user, WOPDetSubViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_WORK_ORDER_PICKING_DET_SUB");

                db.AddInParameter(cmd, "RESULT_ID", DbType.Int32, model.ResultId);
                db.AddInParameter(cmd, "QUANTITY", DbType.Int32, null);
                db.AddInParameter(cmd, "RACK_ID", DbType.Int32, null);
                db.AddInParameter(cmd, "WOP_DET_ID", DbType.Int64, null);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").ToString());

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

        public void DMLWOPDetSub(UserInfo user, WOPDetSubViewModel listModel)
        {

            bool isSuccess = true;
            CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("P_DML_WORK_ORDER_PICKING_DET_SUB");
            CreateConnection(cmd);
            DbTransaction transaction = connection.BeginTransaction();
            try
            {
                foreach (var model in listModel.ListSubModel)
                {
                    //Set Command Type 'Insert' or 'Update' by ResultId
                    listModel.CommandType = model.ResultId != -1 ? CommonValues.DMLType.Update : CommonValues.DMLType.Insert;

                    cmd.Parameters.Clear();
                    db.AddInParameter(cmd, "RESULT_ID", DbType.Int32, model.ResultId);
                    db.AddInParameter(cmd, "QUANTITY", DbType.Decimal, model.Quantity);
                    db.AddInParameter(cmd, "RACK_ID", DbType.Int32, model.Value);
                    db.AddInParameter(cmd, "WOP_DET_ID", DbType.Int64, model.WOPDetId);
                    db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, listModel.CommandType);
                    db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                    db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                    db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                    db.ExecuteNonQuery(cmd, transaction);
                    model.ErrorNo = listModel.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();

                    if (listModel.ErrorNo > 0)
                    {
                        isSuccess = false;
                        listModel.ErrorMessage = ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").ToString());
                        break;
                    }

                }

            }
            catch (Exception Ex)
            {
                isSuccess = false;
                listModel.ErrorNo = 1;
                listModel.ErrorMessage = MessageResource.Err_Generic_Unexpected + "System Message : (" + Ex.Message + ")";
            }
            finally
            {
                if (isSuccess)
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                }
                CloseConnection();
            }


        }

        public void CompleteWorkOrderPicking(UserInfo user, WorkOrderPickingViewModel model)
        {
            try
            {
                var dealerId = (user.GetUserDealerId() == 0) ? model.DealerId : user.GetUserDealerId();

                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_COMPLETE_WORK_ORDER_PICKING");
                db.AddInParameter(cmd, "WOP_MST_ID", DbType.Int64, model.WorkOrderPickingId);
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(dealerId));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>();
                if (model.ErrorNo == 1)
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

        public void StockCardDefaultRackReturn(UserInfo user, int partId, out int value, out string text)
        {
            value = 0;
            text = "";
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_DEFAULT_RACK_FOR_RETURN");
                db.AddInParameter(cmd, "PART_ID", DbType.Int64, partId);
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(user.GetUserDealerId()));

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        value = dr["VALUE"].GetValue<int>();
                        text = dr["TEXT"].GetValue<string>();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (value == 0)
                    text = MessageResource.Global_Display_Choose;
                CloseConnection();
            }
        }

        public void WorkOrderPickingDetailRack(UserInfo user, WorkOrderPickingViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_WORK_ORDER_PICKING_DET_SUB");

                db.AddInParameter(cmd, "RESULT_ID", DbType.Int32, 0);
                db.AddInParameter(cmd, "QUANTITY", DbType.Int32, 0);
                db.AddInParameter(cmd, "RACK_ID", DbType.Int32, 0);
                db.AddInParameter(cmd, "WOP_DET_ID", DbType.Int64, MakeDbNull(model.WorkOrderPickingId));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, "R");
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>();


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

        public void DMLWorkOrderPickingDetail(UserInfo user, WorkOrderPickingDetailViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_WORK_ORDER_PICKING_DET");
                db.AddParameter(cmd, "ID_WORK_ORDER_PICKING_DET", DbType.Int32, ParameterDirection.InputOutput, "ID_WORK_ORDER_PICKING_DET", DataRowVersion.Default, model.WorkOrderPickingDetailId);
                db.AddInParameter(cmd, "ID_WORK_ORDER_PICKING_MST", DbType.Int32, model.WorkOrderPickingMstId);
                db.AddInParameter(cmd, "ID_PART", DbType.String, model.PartId);
                db.AddInParameter(cmd, "ID_STOCK_TYPE", DbType.String, MakeDbNull(model.StockTypeId));
                db.AddInParameter(cmd, "REQ_QTY", DbType.Double, MakeDbNull(model.RequiredQuantity));
                db.AddInParameter(cmd, "PICK_QTY", DbType.Double, MakeDbNull(model.PickQuantity));
                db.AddInParameter(cmd, "PICK_CLOSURE_DESCRIPTION", DbType.Double, MakeDbNull(model.PickClosureDescription));
                db.AddInParameter(cmd, "ID_WORK_ORDER_DETAIL", DbType.Decimal, MakeDbNull(model.WorkOrderDetailId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.WorkOrderPickingDetailId = db.GetParameterValue(cmd, "ID_WORK_ORDER_PICKING_DET").GetValue<int>();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 2)
                    model.ErrorMessage = MessageResource.SparePartSaleDetail_Error_NullId;
                else if (model.ErrorNo == 1)
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
